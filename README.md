[<img align="right" width="500" height="300" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiIrpEsvjM4G1U0tpctRCQ_ktSY-lY2NMZeg&s">](https://qlab.pucp.edu.pe/)



[Escuela Nacional de Estadística e Informática ](https://www.gob.pe/inei)  
[Programa de Extensión Universitaria Ciencia del Dato (PEU-CD)](https://peucd.inei.gob.pe/peucd/)

# **Proyecto Integrador**: Delimitación Automática de Centros Poblados Rurales mediante Inteligencia Artificial

## Autores

* [**Caballero Chocano, Rodrigo**](https://github.com/Fergos14)
* [**Llaro Castro, Diego**](https://github.com/DLlaro)
* [**Ocón Tovar, Sara**](https://github.com/saraocon)
* [**Sebastián Ríos, Wilder**](https://github.com/WilderSr99)

## Descripcion General

El proyecto se dividió en dos entornos de acuedo a la distribución de los centros poblados de la Costa y Selva del Perú.

## **1. Entorno Controlado (Selva)**  
Los centros poblados rurales de la selva son visibles a simple vista ya que se agrupan en conglomerados rodeados de árboles

En este Entorno:
* Se desarrollo un modelo que identifique dichos centros pobladosy se implementó un prototipo funcional que exporte las delimitaciones inferidas.
  * Para este enfoque se desarrollo un modelo de [segmentación semántica](https://github.com/DLlaro/Delimitacion-Automatica-de-Centros-Poblados/wiki/SI-Entrenamiento-Deteccion-Casas-Selva)**(referido en la documentación como SS)**.
* Se entreno un modelo para la identificacion de casas en dichos centros poblados a fin de obtener un recuento del número de casas.
  * Para este enfoque se desarrollo un modelo de [segmentación por instancias](https://github.com/DLlaro/Delimitacion-Automatica-de-Centros-Poblados/wiki/SS-Entrenamiento-Delimitacion-CCPP-Selva)**(referido en la documentación como SI)**.

## **2. Entorno NO Controlado (Costa)**  
Los centros poblados rurales de la costa no son identificados a simple vista, existe dispersión de casas y conglomerados al mismo tiempo.
* Se desarrollo un modelo que identifique dichos centros pobladosy se implementó un prototipo funcional que exporte las delimitaciones inferidas.
  * Para este enfoque se desarrollo un modelo de [segmentación semántica](https://github.com/DLlaro/Delimitacion-Automatica-de-Centros-Poblados/wiki/SSCosta-Entrenamiento-Delimitacion-CCPP-Costa) **(referido en la documentación como SSCosta)**.
  * Se desarrollo un modelo regresión logística, que permitiese vincular los polígonos formados con sus respectivos centros poblados como etapa de posprocesamiento.
