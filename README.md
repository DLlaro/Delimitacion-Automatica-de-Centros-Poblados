[<img align="right" width="500" height="300" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQiIrpEsvjM4G1U0tpctRCQ_ktSY-lY2NMZeg&s">](https://qlab.pucp.edu.pe/)



[Escuela Nacional de Estadística e Informática ](https://www.gob.pe/inei)  
[Programa de Extensión Universitaria Ciencia del Dato (PEU-CD)](https://peucd.inei.gob.pe/peucd/)

# **Proyecto Integrador**: Delimitación Automática de Centros Poblados Rurales mediante Inteligencia Artificial

## Autores

* [**Caballero Chocano, Rodrigo**]()
* [**Llaro Castro, Diego**]()
* [**Ocón Tovar, Sara**]()
* [**Sebastián Ríos, Wilder**]()

## Descripcion General

El proyecto se dividió en dos entornos de acuedo a la distribución de los centros poblados de la Costa y Selva del Perú.

## **1. Entorno Controlado (Selva)**  
Los centros poblados rurales de la selva son visibles a simple vista ya que se agrupan en conglomerados rodeados de árboles

En este Entorno:
* Se desarrollo un modelo que identifique dichos centros pobladosy se implementó un prototipo funcional que exporte las delimitaciones inferidas.
  * Para este enfoque se desarrollo un modelo de [segmentación semántica](Delimitacion_de_CCPP) **(referido en la documentación como SS)**.
* Se entreno un modelo para la identificacion de casas en dichos centros poblados a fin de obtener un recuento del número de casas.
  * Para este enfoque se desarrollo un modelo de [segmentación por instancias](SegmentacionInstancias) **(referido en la documentación como SI)**.

## **2. Entorno NO Controlado (Costa)**  
Los centros poblados rurales de la costa no son identificados a simple vista, existe dispersión de casas y conglomerados al mismo tiempo.
