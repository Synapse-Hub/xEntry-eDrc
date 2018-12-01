USE [xEntryGlobalDb]
GO

-- All verifications of existing or old views

if EXISTS(select * from sys.views where name='vw_splits_Tar_geopoints')
	drop view vw_splits_Tar_geopoints
go

if EXISTS(select * from sys.views where name='vw_splits_PR_geopoints')
	drop view vw_splits_PR_geopoints
go

if EXISTS(select * from sys.views where name='vw_splits_ident_pepi_geopoints')
	drop view vw_splits_ident_pepi_geopoints
go

if EXISTS(select * from sys.views where name='vw_splits_suivi_pepi_geopoints')
	drop view vw_splits_suivi_pepi_geopoints
go

-- End Verif

-- TAR Table
create view [dbo].[vw_splits_Tar_geopoints] 
as 

WITH CTE
AS
(
	SELECT uuid, deviceid, CAST('<c>' + REPLACE(SUBSTRING(emplacement, 1, LEN(emplacement) ), ' ', '</c><c>') + '</c>' AS XML) AS V_xml
    FROM tbl_fiche_tar
)
SELECT 
    uuid,
    deviceid,
    V_xml.value('(/c/text())[1]', 'VARCHAR(50)') AS latitude,
    V_xml.value('(/c/text())[2]', 'VARCHAR(50)') AS longitude
    --V_xml.value('(/c/text())[3]', 'VARCHAR(50)') AS altitude,
    --V_xml.value('(/c/text())[4]', 'VARCHAR(50)') AS EPE
    --V_xml.value('(/c/text())[5]', 'VARCHAR(50)') AS NUMLIGPIECE
FROM CTE;

GO

-- Pr table
create view [dbo].[vw_splits_PR_geopoints] 
as 

WITH CTEPR
AS
(
	SELECT uuid, deviceid, CAST('<c>' + REPLACE(SUBSTRING(localisation, 1, LEN(localisation) ), ' ', '</c><c>') + '</c>' AS XML) AS V_xml, 'saison' as saison
    FROM tbl_fiche_pr
)
SELECT 
    uuid,
    deviceid,
    saison,
    V_xml.value('(/c/text())[1]', 'VARCHAR(50)') AS latitude,
    V_xml.value('(/c/text())[2]', 'VARCHAR(50)') AS longitude,
    V_xml.value('(/c/text())[3]', 'VARCHAR(50)') AS altitude,
    V_xml.value('(/c/text())[4]', 'VARCHAR(50)') AS EPE
    --V_xml.value('(/c/text())[5]', 'VARCHAR(50)') AS NUMLIGPIECE
FROM CTEPR;

GO

-- Identification Pepiniere Table
create view [dbo].[vw_splits_ident_pepi_geopoints] 
as 

WITH CTEPR
AS
(
	SELECT uuid, deviceid, CAST('<c>' + REPLACE(SUBSTRING(localisation, 1, LEN(localisation) ), ' ', '</c><c>') + '</c>' AS XML) AS V_xml
    FROM tbl_fiche_ident_pepi
)
SELECT 
    uuid,
    deviceid,
    V_xml.value('(/c/text())[1]', 'VARCHAR(50)') AS latitude,
    V_xml.value('(/c/text())[2]', 'VARCHAR(50)') AS longitude
    --V_xml.value('(/c/text())[5]', 'VARCHAR(50)') AS NUMLIGPIECE
FROM CTEPR;

GO


-- Suivi Pepiniere Table
create view [dbo].[vw_splits_suivi_pepi_geopoints] 
as 

WITH CTEPR
AS
(
	SELECT uuid, deviceid, CAST('<c>' + REPLACE(SUBSTRING(localisation, 1, LEN(localisation) ), ' ', '</c><c>') + '</c>' AS XML) AS V_xml, 'saison' as saison
    FROM tbl_fiche_suivi_pepi
)
SELECT 
    uuid,
    deviceid,
    saison,
    V_xml.value('(/c/text())[1]', 'VARCHAR(50)') AS latitude,
    V_xml.value('(/c/text())[2]', 'VARCHAR(50)') AS longitude,
    V_xml.value('(/c/text())[3]', 'VARCHAR(50)') AS altitude,
    V_xml.value('(/c/text())[4]', 'VARCHAR(50)') AS EPE
    --V_xml.value('(/c/text())[5]', 'VARCHAR(50)') AS NUMLIGPIECE
FROM CTEPR;

GO


-- For Testing purpose
-- Creation of table Geo for TAR and trying inserting data from a trigger
CREATE TABLE geo_fiche_tar(
	uuid varchar(100) ,
	deviceid varchar(100) ,
    saison varchar(100),
	latitude varchar(50),
	longitude varchar(50),
	altitude varchar(50),
	EPE varchar(50)
)
Go

CREATE TRIGGER tgr_InsertTarGeoData 
ON dbo.vw_splits_Tar_geopoints

INSTEAD OF INSERT
AS
   INSERT INTO geo_fiche_tar(uuid,deviceid,saison,latitude,longitude,altitude,EPE)
   SELECT uuid,deviceid,saison,latitude,longitude,altitude,EPE
   FROM inserted
GO

 



