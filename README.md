# PROYECTO SOLICITUDES
#### Author: Jéimar Arias Vélez  jeimararias@yahoo.com
#### Date: 2023-04-21

## Challenge requerido Backend Develper Engineer

Este proyecto corresponde a el procesamiento de un Flujo de Solicitudes. Servicio de captura y procesamiento de datos para cualquier entidad

### Requerimientos

* Framework .Net core

* Microsoft SQL SERVER

* Entity Framework Core

### Creacion de la base de datos SQL SERVER. Nombre: SOLICITUDES

1. Cargar el proyecto en Visual Studio
2. Modificar el archivo appsetting.json. En conection string: Server=[SERVIDOR DE SQL]
3. Ejecutar el Migration de creacion de la base de datos
4. Verificar que la base de datos este creada con sus tablas respectivas.
5. Correr el script por SSMS anexo (ConfiguracionAdicional.sql)
> El script, crea unas Llaves foraneas y vistas, que no alcance a realizar mediante Entity Framework

### Modelo

Este es el modelo Entidad-Relacion de SOLICITUDES:
![Modelo ER](https://drive.google.com/file/d/1Nd4S4YaFahLJcrTD1zpD93NLDgSaQV9j/view?usp=sharing)

#### Tablas de parámteros

Existen 5 tablas de parametros:
> Flujo: Se pueden crear n Flujos
> Paso: Se pueden crear n Pasos
> Campo: Se pueden crear n Campos
> FlujoPaso: Permite asociar Pasos definidos en Paso a Flujos definidos en Flujo
> PasoCampo: Permite asociar Campos definidos en Campo a Pasos definidos en Paso 
> User: Se pueden crear n Usuarios (Tabla muy basica)





**Note**:  I will provide the scripts to create the database if I have time.

### Execution
I used Visual Studio Code as IDE for python. In order to get up the server for API, you must execute the next sentence:
> python .\src\api_erp.py

##### 1. Loading database - Populating tables
You must put the csv file in a folder call files\ and upload the files in the following order: 1.jobs.csv, 2.departments.csv and 3.hired_employees.csv  

> Method:  [POST]   
> http://127.0.0.1:5000/erpjav/TABLE/N   
Where:   
> **TABLE**= jobs, departments, hired_employees   
> **N** = Number of records between 1 and 1000 to load per block   
   
If the process was executed successfully, the following message is returned:   
> {
> "Message": "TABLES file was loaded. See errors in
> files\\hired_employees.csv.log"
>}

The process generates an error log of those records that were not loaded correctly in the same folder as the file, with the same name as the file appended with the ".log" extension.
**Ex**: hired_employees.csv generates an error file like hired_employees.csv.log

##### 2. Generate reports
Two required reports can be generated:

a) Number of employees hired for each job and department in 2021 divided by quarter. Execute like follows:
   
> Method: GET   
> http://127.0.0.1:5000/erpjav/employees_quarters   
   
b) List of ids, name and number of employees hired of each department that hired more employees than the mean of employees hired in 2021 for all the departments, ordered by the number of employees hired (descending). Execute like follows:

> Method: GET   
> http://127.0.0.1:5000/erpjav/employees_average   
   
##### 3. Backup tables in AVRO Format
You also can generate a backup of each table in avro format using the next sentence:   
> Method: GET   
> http://127.0.0.1:5000/erpjav1/backup/TABLE  
Where:   
> **TABLE**= jobs, departments, hired_employees   
   
The backup of each table is saved in a folder with the name backup like this: **backup\jobs.avro**

##### 4. Restore tables from AVRO Format files
You can restore each table from avro format files under the next call to API:   
> Method: PUT   
> http://127.0.0.1:5000/erpjav1/restore/TABLE   
Where:   
> **TABLE**= jobs, departments, hired_employees   

##### 5. Dashboard
I did a visual report for each requirement in point 2 using Power BI. You can use it in the next file. It is in the folder dashboards:   
> Name: **DashboardEmployees.pbix**
