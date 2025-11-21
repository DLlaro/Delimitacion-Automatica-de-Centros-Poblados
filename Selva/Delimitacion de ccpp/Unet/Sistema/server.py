from flask import Flask, request, jsonify
import signal
import rasterio
import numpy as np
from rasterio.features import shapes
from rasterio.windows import Window
from shapely.geometry import shape, Polygon
from shapely.ops import unary_union
import geopandas as gpd
from tensorflow.keras.models import load_model
import cv2
import sys
import os

app = Flask(__name__)



# ========= CARGAR MODELO UNA SOLA VEZ =========
print("Cargando modelo...")
sys.stdout.flush()
ruta = os.path.expandvars("$HOME/Unet/Sistema/model/unet_resnet34_v0.9sentinel_model")

nombre_modelo = os.path.splitext(os.path.basename(ruta))[0]

#model = load_model("/home/rezxch/unet/sistema/model/unet_resnet34_sentinel_model.keras", compile=False)
model = load_model(ruta, compile=False)

print(f"Modelo cargado y listo!")
sys.stdout.flush()
print(f"Modelo: {nombre_modelo}")
sys.stdout.flush()

umbral = 0.5
n_canales = 3
tile_size = 512  # Ajusta según tu modelo (debe ser múltiplo de 32)
overlap = 256     # Overlap para evitar artefactos en bordes


def pad_to_multiple(img, multiple=32):
    """Añade padding para que las dimensiones sean múltiplos de 'multiple'"""
    h, w = img.shape[:2]
    pad_h = (multiple - h % multiple) % multiple
    pad_w = (multiple - w % multiple) % multiple
    
    if pad_h == 0 and pad_w == 0:
        return img, 0, 0
    
    if len(img.shape) == 3:
        padded = np.pad(img, ((0, pad_h), (0, pad_w), (0, 0)), mode='reflect')
    else:
        padded = np.pad(img, ((0, pad_h), (0, pad_w)), mode='reflect')
    
    return padded, pad_h, pad_w

def process_small_image(img):
    """Procesa imagenes pequeñas directamente"""
    img_padded, pad_h, pad_w = pad_to_multiple(img, 32)
    img_norm = (img_padded - img_padded.min()) / (img_padded.max() - img_padded.min() + 1e-6)
    
    X = np.expand_dims(img_norm, axis=0)
    pred = model.predict(X, verbose=0)[0, :, :, 0]
    
    # Remover padding
    if pad_h > 0 or pad_w > 0:
        pred = pred[:img.shape[0], :img.shape[1]]
    
    return pred

def process_large_image(src):
    """Procesa imagenes grandes por tiles"""
    height = src.height
    width = src.width
    
    print(f"Imagen grande detectada: {width}x{height}")
    print(f"Procesando por tiles de {tile_size}x{tile_size}")
    sys.stdout.flush()
    
    # Crear mascara vacia
    mask_pred = np.zeros((height, width), dtype=np.float32)
    
    # Calcular número de tiles
    n_tiles_x = int(np.ceil(width / (tile_size - overlap)))
    n_tiles_y = int(np.ceil(height / (tile_size - overlap)))
    total_tiles = n_tiles_x * n_tiles_y
    
    print(f"Total de tiles a procesar: {total_tiles}")
    sys.stdout.flush()
    
    tile_count = 0
    
    for i in range(n_tiles_y):
        for j in range(n_tiles_x):
            # Calcular ventana
            col_off = j * (tile_size - overlap)
            row_off = i * (tile_size - overlap)
            
            # Ajustar tamaño si esta en el borde
            actual_width = min(tile_size, width - col_off)
            actual_height = min(tile_size, height - row_off)
            
            # Leer tile
            window = Window(col_off, row_off, actual_width, actual_height)
            tile = src.read(window=window)
            
            # Procesar tile
            tile = np.transpose(tile, (1, 2, 0)).astype(np.float32)
            tile = tile[..., :n_canales]
            
            # Aplicar padding al tile
            tile_padded, pad_h, pad_w = pad_to_multiple(tile, 32)
            tile_norm = (tile_padded - tile_padded.min()) / (tile_padded.max() - tile_padded.min() + 1e-6)
            
            # Predecir
            X = np.expand_dims(tile_norm, axis=0)
            pred_tile = model.predict(X, verbose=0)[0, :, :, 0]
            
            # Remover padding
            pred_tile = pred_tile[:actual_height, :actual_width]
            
            # Manejar overlap
            start_h = overlap//2 if i > 0 else 0
            start_w = overlap//2 if j > 0 else 0
            
            end_h = actual_height - overlap//2 if i < n_tiles_y - 1 else actual_height
            end_w = actual_width - overlap//2 if j < n_tiles_x - 1 else actual_width
            
            pred_tile_crop = pred_tile[start_h:end_h, start_w:end_w]
            
            dest_row = row_off + start_h
            dest_col = col_off + start_w
            
            # Insertar en mascara final
            mask_pred[dest_row:dest_row+pred_tile_crop.shape[0], 
                     dest_col:dest_col+pred_tile_crop.shape[1]] = pred_tile_crop
            
            tile_count += 1
            if tile_count % 5 == 0 or tile_count == total_tiles:
                print(f"Procesados {tile_count}/{total_tiles} tiles ({100*tile_count/total_tiles:.1f}%)")
                sys.stdout.flush()
    
    return mask_pred

def remove_holes(geom):
    """ 
        Eliminacion de los anillos de los poligonos, dependiendo del tipo de geometria se realiza
        cierto tipo de eliminacion
        Args:
            geom (GeoDataFrame): capa a eliminar poligonos
        Returns: 
            geom (GeoDataFrame): capa de poligonos sin anillos
    """
    if geom is None:
        return None
    if geom.geom_type == "Polygon":
        return Polygon(geom.exterior)
    elif geom.geom_type == "MultiPolygon":
        return type(geom)([Polygon(p.exterior) for p in geom.geoms])
    else:
        return geom

@app.route('/predict', methods=['POST'])
def predict():
    try:
        data = request.json
        tif_path = data['tif_path']
        puntos_path = data['puntos_path']
        gpkg_path = data['gpkg_path']
        mask_png_path = data['mask_png_path']
        AREA_MINIMA = data['area_minima']
        interseccion = data['interseccion']

        if AREA_MINIMA != "":
            AREA_MINIMA = int(data['area_minima'])
            print(f"AREA_MINIMA: {AREA_MINIMA}")
            sys.stdout.flush()

        print(f"\RAW Interseccion: {interseccion}, tipo {type(interseccion)}")
        sys.stdout.flush()

        #Opción de poligonos intersectados por punto
        if interseccion==True:
            interseccion = "inner"
        else:
            interseccion = "left"
        
        print(f"\Interseccion: {interseccion}")
        sys.stdout.flush()
        

        print(f"\nProcesando: {tif_path}")
        sys.stdout.flush()
        
        # === LEER TIFF ===
        with rasterio.open(tif_path) as src:
            height = src.height
            width = src.width
            transform = src.transform
            crs = src.crs
            
            
            # Decidir estrategia de procesamiento
            if width <= tile_size and height <= tile_size:
                print("Procesamiento directo (imagen pequeña)")
                sys.stdout.flush()
                
                img = src.read()
                img = np.transpose(img, (1, 2, 0)).astype(np.float32)
                img = img[..., :n_canales]
                
                pred = process_small_image(img)
            else:
                print("Procesamiento por tiles (imagen grande)")
                sys.stdout.flush()
                
                pred = process_large_image(src)
        
        # Convertir a mascara binaria
        mask_pred = (pred > umbral).astype(np.uint8)
        
        if AREA_MINIMA != "":
            # === FILTRAR MaSCARA POR aREA MiNIMA ===
            area_pixel = abs(transform.a * transform.e)
            AREA_MIN_PIXELS = AREA_MINIMA / area_pixel

            print(f"area minima en pixeles: {AREA_MIN_PIXELS}")
            sys.stdout.flush()

            num_labels, labels = cv2.connectedComponents(mask_pred.astype(np.uint8))

            clean_mask = np.zeros_like(mask_pred)

            for label in range(1, num_labels):
                component = (labels == label)
                area = np.sum(component)

                if area >= AREA_MIN_PIXELS:
                    clean_mask[component] = 1

            mask_pred = clean_mask

            print("Mascara filtrada por area minima.")
            sys.stdout.flush()

        print(f"Forma de la mascara: {mask_pred.shape}")
        print(f"Pixeles positivos: {np.sum(mask_pred)}")
        sys.stdout.flush()
        
        # === GUARDAR MaSCARA VISUAL ===
        print(f"Guardando mascara en: {mask_png_path}")
        sys.stdout.flush()
        
        success = cv2.imwrite(mask_png_path, (mask_pred * 255).astype("uint8"))
        
        if not success:
            print(f"ERROR: No se pudo guardar la mascara")
            sys.stdout.flush()
            return jsonify({"status": "ERROR", "message": "No se pudo guardar la mascara"}), 500
        
        print(f"Mascara guardada exitosamente")
        sys.stdout.flush()
        
        # === VECTORIZAR MaSCARA ===
        print("Vectorizando poligonos...")
        sys.stdout.flush()
        
        polys = []
        for geom, value in shapes(mask_pred.astype(np.uint8),
                                  mask=mask_pred.astype(np.uint8),
                                  transform=transform):
            if value == 1:
                polys.append(shape(geom))

        # === FILTRAR POR aREA MiNIMA ===

        if AREA_MINIMA !="":
            polys_filtrados = []
            for poly in polys:
                if poly.area >= AREA_MINIMA:
                    polys_filtrados.append(poly)

            print(f"Poligonos luego de filtrar area minima: {len(polys_filtrados)}")
            sys.stdout.flush()
        
            if len(polys) == 0:
                return jsonify({"status": "NO_POLYGONS_AFTER_AREA_FILTER"})
            
            gdf_pred = gpd.GeoDataFrame(geometry=polys_filtrados, crs=crs)
        else:
            gdf_pred = gpd.GeoDataFrame(geometry=polys, crs=crs)
    
        # === 1. Limpiando capa de poligonos ===
        gdf_pred["geometry"] = gdf_pred.buffer(0)
        #2. Suavizar bordes
        gdf_pred["geometry"] = gdf_pred.buffer(0.3).buffer(-0.3)
        #3. Simplificar puntos
        gdf_pred["geometry"] = gdf_pred.simplify(
            tolerance=0.7,
            preserve_topology=True
        )
        #4. Eliminar anillos
        gdf_pred["geometry"] = gdf_pred.geometry.apply(remove_holes)
            
        # === CARGAR CAPA DE PUNTOS ===
        print("Cargando puntos CCPP...")
        sys.stdout.flush()
        
        gdf_puntos = gpd.read_file(puntos_path)

        if gdf_puntos.crs != gdf_pred.crs:
            gdf_puntos = gdf_puntos.to_crs(gdf_pred.crs)
            print(f"La capa de puntos con crs : {gdf_puntos} al crs {gdf_pred.crs} de la capa tiff")
            sys.stdout.flush()


        # === UNIR POLIGONOS CON UNA DISTANCIA MINIMA A LOS PUNTOS 
        # distancia_minima = 50

        # result = []

        # for idx, row in gdf_puntos.iterrows():
        #     p = row.geometry

        #     # Poligonos que estan dentro de la distancia
        #     cerca = gdf_pred[gdf_pred.distance(p) <= distancia_minima]

        #     if len(cerca) > 0:
        #         union = unary_union(cerca.geometry)
        #         result.append({
        #             "point_id": row["IDCCPP_17"],   # o el nombre de tu campo
        #             "geometry": union
        #         })
        #     else:
        #         # Si ningún poligono esta cerca, opcionalmente incluir un vacio
        #         result.append({
        #             "point_id": row["IDCCPP_17"],
        #             "geometry": None
        #         })



        # --- Convertimos a GeoDataFrame
        #gdf_result = gpd.GeoDataFrame(result, geometry="geometry", crs=gdf_pred.crs)

        # === SPATIAL JOIN ===
        print("Realizando spatial join...")
        sys.stdout.flush()
        
        gdf_joined = gpd.sjoin(gdf_pred, gdf_puntos, how=interseccion, predicate="intersects")
        
        if gdf_joined.empty:
            print("No hay coincidencias con puntos CCPP")
            sys.stdout.flush()
            return jsonify({"status": "NO_MATCH"})
        
        gdf_joined = gdf_joined.drop(columns=["index_right"], errors="ignore")
        
        print(f"Poligonos con punto CCPP: {len(gdf_joined)}")
        sys.stdout.flush()
        
        # === EXPORTAR GPKG FINAL ===
        print("Exportando GPKG...")
        sys.stdout.flush()
        
        gdf_joined.to_file(gpkg_path, driver="GPKG")
        
        print("Proceso completado exitosamente!")
        sys.stdout.flush()
        
        return jsonify({
            "status": "OK",
            "polygons_detected": len(polys),
            "polygons_matched": len(gdf_joined)
        })
        
    except Exception as e:
        import traceback
        error_msg = str(e)
        print(f"ERROR: {error_msg}")
        print(traceback.format_exc())
        sys.stdout.flush()
        return jsonify({"status": "ERROR", "message": error_msg}), 500

@app.route('/health', methods=['GET'])
def health():
    return jsonify({"status": "running", "model_loaded": True})

@app.get("/shutdown")
def shutdown():
    os.kill(os.getpid(), signal.SIGTERM)
    return {"status": "stopping"}

if __name__ == '__main__':
    print("Servidor Flask iniciando en puerto 5000...")
    sys.stdout.flush()
    app.run(host='0.0.0.0', port=5000, debug=False)