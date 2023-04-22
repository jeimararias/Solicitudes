use SOLICITUDES
go

/*
Drop table Flujo
Drop table Paso
Drop table Campo
*/

/*
Drop table TUTORIALES2..Flujo
Drop table TUTORIALES2..PasoCampo
*/

Select * 
--into TUTORIALES2..Flujo
from Flujo
Select * 
--into TUTORIALES2..Paso
from Paso
Select * 
--into TUTORIALES2..Campo
from Campo
Select * 
--into TUTORIALES2..FlujoPaso
from FlujoPaso
Select * 
--into TUTORIALES2..PasoCampo
from PasoCampo

Select * from Solicitud



/*
USE [SOLICITUDES]
GO

/****** Object:  View [dbo].[vwFlujos]    Script Date: 20/04/2023 4:40:47 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* Crea relaciones */
ALTER TABLE FlujoPaso
	ADD FOREIGN KEY (FlujoId) REFERENCES Flujo(Id);
ALTER TABLE FlujoPaso
	ADD FOREIGN KEY (PasoId) REFERENCES Paso(Id);

ALTER TABLE PasoCampo
	ADD FOREIGN KEY (PasoId) REFERENCES Paso(Id);
ALTER TABLE PasoCampo
	ADD FOREIGN KEY (CampoId) REFERENCES Campo(Id);

ALTER TABLE Solicitud
	ADD FOREIGN KEY (FlujoId) REFERENCES Flujo(Id);
ALTER TABLE Solicitud
	ADD FOREIGN KEY (UserId) REFERENCES [User](Id);

ALTER TABLE SolicitudData
	ADD FOREIGN KEY (SolicitudId) REFERENCES Solicitud(Id);
ALTER TABLE SolicitudData
	ADD FOREIGN KEY (PasoId) REFERENCES Paso(Id);
ALTER TABLE SolicitudData
	ADD FOREIGN KEY (CampoId) REFERENCES Campo(Id);

ALTER TABLE SolicitudControl
	ADD FOREIGN KEY (SolicitudId) REFERENCES Solicitud(Id);
ALTER TABLE SolicitudControl
	ADD FOREIGN KEY (PasoId) REFERENCES Paso(Id);

/* Crea vistas */
CREATE VIEW [dbo].[vwFlujos]
AS
SELECT 
   F.Id AS FlujoId, F.CodFlujo, F.Nombre AS NombreFlujo, F.Descripcion AS DescripcionFlujo, F.EntidadServicio, F.IDEstado AS IdEstadoFlujo, 
   P.Id AS PasoId, P.CodPaso, P.Nombre AS NombrePaso, P.Descripcion AS DescripcionPaso, FP.Prioridad, FP.IDEstadoPaso_OK, P.IDEstado AS IdEstadoPaso, 
   C.Id AS CampoId, C.Nombre AS NombreCampo, C.Descripcion AS DescripcionCampo, C.Tipo AS TipoCampo, C.IDEstado AS IdEstadoCampo, PC.EsRequerido, 
   PC.Tipo AS TipoOrigen, PC.ExpresionRegular
FROM     dbo.Flujo AS F WITH (NOLOCK) 
INNER JOIN dbo.FlujoPaso AS FP WITH (NOLOCK) ON FP.FlujoId = F.Id 
INNER JOIN dbo.Paso AS P WITH (NOLOCK) ON P.Id = FP.PasoId 
INNER JOIN dbo.PasoCampo AS PC WITH (NOLOCK) ON PC.PasoId = P.Id 
INNER JOIN dbo.Campo AS C WITH (NOLOCK) ON C.Id = PC.CampoId
GO

CREATE VIEW [dbo].[vwFlujosPasos]
AS
SELECT 
   F.Id AS FlujoId, F.CodFlujo, F.Nombre AS NombreFlujo, F.Descripcion AS DescripcionFlujo, F.EntidadServicio, F.IDEstado AS IdEstadoFlujo, 
   P.Id AS PasoId, P.CodPaso, P.Nombre AS NombrePaso, P.Descripcion AS DescripcionPaso, FP.Prioridad, FP.IDEstadoPaso_OK, P.IDEstado AS IdEstadoPaso
FROM     dbo.Flujo AS F WITH (NOLOCK) 
INNER JOIN dbo.FlujoPaso AS FP WITH (NOLOCK) ON FP.FlujoId = F.Id 
INNER JOIN dbo.Paso AS P WITH (NOLOCK) ON P.Id = FP.PasoId 

*/


/*
Insert into SOLICITUDES..Flujo
  (CodFlujo,	Nombre,	Descripcion,	EntidadServicio,	IDEstado)
Select --*
  CodFlujo,	Nombre,	Descripcion,	EntidadServicio,	IDEstado
from TUTORIALES2..Flujo

Insert into SOLICITUDES..Paso
  (CodPaso,	Nombre,	Descripcion,IDEstado)
Select
  CodPaso,	Nombre,	Descripcion,IDEstado
from TUTORIALES2..Paso

Insert into SOLICITUDES..Campo
  (Nombre,	Descripcion, Tipo, IDEstado)
Select --*
  Nombre,	Descripcion, Tipo, IDEstado
from TUTORIALES2..Campo

Insert into SOLICITUDES..FlujoPaso
  (FlujoId,	PasoId,	Prioridad,	IDEstadoPaso_OK,	IDEstado)
Select --*
  FlujoId,	PasoId,	Prioridad,	IDEstadoPaso_OK,	IDEstado
from TUTORIALES2..FlujoPaso

Insert into SOLICITUDES..PasoCampo
  (PasoId,	CampoId,	EsRequerido, Tipo,	ExpresionRegular,	IDEstado)
Select --*
  PasoId,	CampoId,	EsRequerido, Tipo,	ExpresionRegular,	IDEstado
from TUTORIALES2..PasoCampo

*/

Select F.*, FP.*, P.*, 
  PC.*, C.* 
from Flujo F with (NOLOCK)
Inner Join FlujoPaso FP with (NOLOCK) On FP.FlujoId = F.Id
Inner Join Paso P with (NOLOCK) On P.Id = FP.PasoId
Inner Join PasoCampo PC with (NOLOCK) On PC.PasoId = P.Id
Inner Join Campo C with (NOLOCK) On C.Id = PC.CampoId

/* Vista para procesar por hilos */
Select *
from vwFlujosPasos FP
where FP.FlujoId = 1
Order by FP.Prioridad, FP.PasoId

Select *
from vwFlujos FP
where FP.FlujoId = 1
Order by FP.Prioridad, FP.PasoId

Select * from Campo

Select * from SolicitudData















