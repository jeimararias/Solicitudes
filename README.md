# PROYECTO SOLICITUDES
#### Author: Jéimar Arias Vélez  jeimararias@yahoo.com
#### Date: 2023-04-21

## Challenge requerido para: Senior Backend Developer Engineer

Este proyecto corresponde a el procesamiento de un Flujo de Solicitudes. Servicio de captura y procesamiento de datos para cualquier entidad

### Requerimientos

* Framework .Net core

* Microsoft SQL SERVER

* Entity Framework Core

### Creacion de la base de datos SQL SERVER. Nombre: SOLICITUDES

1. Cargar el proyecto en Visual Studio
2. Modificar el archivo appsetting.json. En conection string: Server=[SERVIDOR DE SQL]
3. Ejecutar el Migration de creación de la base de datos
4. Verificar que la base de datos este creada con sus tablas respectivas.
5. Correr el script por SSMS anexo (ConfiguracionAdicional.sql)
> El script, crea unas Llaves foráneas y vistas, que no alcance a realizar mediante Entity Framework

### Modelo

Este es el modelo Entidad-Relación de SOLICITUDES:
![Modelo ER](https://drive.google.com/file/d/1Nd4S4YaFahLJcrTD1zpD93NLDgSaQV9j/view?usp=sharing)

#### Tablas de parámetros

Existen 5 tablas de parámetros:
> Flujo: Se pueden crear n Flujos
> Paso: Se pueden crear n Pasos
> Campo: Se pueden crear n Campos
> FlujoPaso: Permite asociar Pasos definidos en Paso a Flujos definidos en Flujo
> PasoCampo: Permite asociar Campos definidos en Campo a Pasos definidos en Paso 
> User: Se pueden crear n Usuarios (Tabla muy basica)

#### Tablas de proceso

Existen 3 tablas de del proceso:
> Solicitud: Es un requerimiento de usuario para procesar su solicitud. Cada Solicitud está asociada a un tipo de Flujo.
> SolicitudData: Corresponde a los datos capturados para cada solicitud para los Campos definido por Flujo y Pasos.
> SolicitudControl: Tabla donde se registran los resultados de cada paso de cada una de las Solicitudes. Log resultado del proceso de la Solicitud

### Ejecución: Parametrización de flujos
Swagger provee los diferentes métodos para crear los Parámetros para los Flujos:
0. Crear Usuario (api/User)
1. Crear Flujos (api/Flujo), Pasos (api/Paso) y Campos (api/Campo)
2. Asociar Pasos a los Flujos (api/FlujoPaso)
3. Asociar Campos a los Pasos (api/PasoCampo)

### Ejecución: Procesamiento de Solicitudes
1. Crear Solicitudes (api/Solicitud)
2. Cargar campos (datos) asociados a la solicitud por cada paso del flujo. (api/SolicitudData)
3. Procesar solicitud: ejecución: api/Solicitud/{n} n: Id de la solicitud

### Proceso
Al ejecutar el Proceso, este procesa en forma asincrona los pasos cuya Prioridad sea la misma. 
La Prioridad es un número secuencial que inicia desde 0 hasta n.
Ej: Si los pasos 1 y 2 se pueden procesar paralelos, se configuran con prioridad 0, luego el paso3 con priorid 1, luego paso 4, 5, 6 con prioridad 2.
En este ejemplo, se procesan los pasos 1 y 2 asincroniamente, para continuar con los pasos de la prioridad 1, espera a que hayan terminado 
los hilos de la prioridad cero, y asi sucesivamente.
