import os
from sklearn.model_selection import train_test_split

def split_dataset_por_ccpp(
        carpeta_imgs="",
        carpeta_masks="",
        test_size=0.10,
        val_size=0.20,       # Porción del train que será val
        random_state=2025,
        agregar_empty_a_train=True):
    """
    Divide las imágenes en train/val/test agrupando por CCPP.
    Devuelve 6 listas:
    - train_imgs, train_masks
    - val_imgs, val_masks
    - test_imgs, test_masks
    """

    # ------------------------------
    # Función auxiliar: extraer ID
    # ------------------------------
    def obtener_ccpp(nombre):
        """
        Devuelve:
         - ID  (si empieza con CCPP_ o NEAR_)
         - None (si es EMPTY)
        """
        if nombre.startswith("CCPP_"):
            return nombre.split("_")[1]

    # ------------------------------
    # Leer imágenes
    # ------------------------------
    all_imgs = sorted(os.listdir(carpeta_imgs))

    grupos = {}       # id_ccpp → [tiles]
    empty_tiles = []  # tiles sin CCPP
    for f in all_imgs:
        idccpp = obtener_ccpp(f)
        if idccpp is None:
            empty_tiles.append(f)
        else:
            if idccpp not in grupos:
                grupos[idccpp] = []
            grupos[idccpp].append(f)

    # ------------------------------
    # Separar IDs (no tiles)
    # ------------------------------
    ccpp_ids = list(grupos.keys())

    # 10% para test
    train_ids, test_ids = train_test_split(
        ccpp_ids, test_size=test_size, random_state=random_state
    )

    # 20% del entrenamiento será validación
    val_rel_size = val_size / (1 - test_size)   # para mantener proporciones exactas

    train_ids, val_ids = train_test_split(
        train_ids, test_size=val_rel_size, random_state=random_state
    )

    # ------------------------------
    # Construir listas finales
    # ------------------------------
    train_imgs = []
    val_imgs = []
    test_imgs = []

    for cid in train_ids:
        train_imgs.extend(grupos[cid])

    for cid in val_ids:
        val_imgs.extend(grupos[cid])

    for cid in test_ids:
        test_imgs.extend(grupos[cid])

    # Agregar EMPTY a train
    #if agregar_empty_a_train:
    #    train_imgs.extend(empty_tiles)

    # ------------------------------
    # Convertir a rutas completas
    # ------------------------------
    train_imgs_full = [os.path.join(carpeta_imgs, f) for f in train_imgs]
    val_imgs_full   = [os.path.join(carpeta_imgs, f) for f in val_imgs]
    test_imgs_full  = [os.path.join(carpeta_imgs, f) for f in test_imgs]

    train_masks_full = [os.path.join(carpeta_masks, f) for f in train_imgs]
    val_masks_full   = [os.path.join(carpeta_masks, f) for f in val_imgs]
    test_masks_full  = [os.path.join(carpeta_masks, f) for f in test_imgs]

    # ------------------------------
    # Verificar existencia de máscaras
    # ------------------------------
    errores = False
    for f in train_masks_full + val_masks_full + test_masks_full:
        if not os.path.exists(f):
            print("ERROR: Máscara no encontrada:", f)
            errores = True

    if errores:
        print("Hay errores. Revisa los nombres o regenera los tiles.")
    else:
        print("\nDivisión correcta sin errores de archivos.")

    # ------------------------------
    # Estadísticas
    # ------------------------------
    print("-" * 50)
    print(f"TRAIN     → imágenes: {len(train_imgs_full)} | máscaras: {len(train_masks_full)}")
    print(f"VALIDATION → imágenes: {len(val_imgs_full)} | máscaras: {len(val_masks_full)}")
    print(f"TEST      → imágenes: {len(test_imgs_full)} | máscaras: {len(test_masks_full)}")
    print("-" * 50)
    print(f"TOTAL IMGS: {len(train_imgs_full) + len(val_imgs_full) + len(test_imgs_full)}")
    print(f"TOTAL MASKS: {len(train_masks_full) + len(val_masks_full) + len(test_masks_full)}")
    print("-" * 50)



    return (
        train_imgs_full, train_masks_full,
        val_imgs_full, val_masks_full,
        test_imgs_full, test_masks_full
    )
