SELECT * FROM [xEntryGlobalDb].[dbo].[vw_splits_Tar_geopoints]
GO

INSERT INTO [xEntryGlobalDb].[dbo].[tbl_geopoint] (uuid,deviceid,latitude,longitude)
SELECT uuid,deviceid,latitude,longitude
from [xEntryGlobalDb].[dbo].[vw_splits_Tar_geopoints] 
WHERE xEntryGlobalDb.dbo.vw_splits_Tar_geopoints.latitude is not null AND
xEntryGlobalDb.dbo.vw_splits_Tar_geopoints.longitude is not null


--[xEntryGlobalDb].[dbo].[vw_splits_ident_pepi_geopoints]
--tbl_geopoint_identif_pepin

INSERT INTO [xEntryGlobalDb].[dbo].[tbl_geopoint_identif_pepin] (uuid,deviceid,latitude,longitude)
SELECT uuid,deviceid,latitude,longitude
from [xEntryGlobalDb].[dbo].[vw_splits_ident_pepi_geopoints]
WHERE xEntryGlobalDb.dbo.vw_splits_ident_pepi_geopoints.latitude is not null AND
xEntryGlobalDb.dbo.vw_splits_ident_pepi_geopoints.longitude is not null