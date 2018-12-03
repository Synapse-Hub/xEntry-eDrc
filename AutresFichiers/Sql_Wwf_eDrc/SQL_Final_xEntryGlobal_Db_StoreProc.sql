USE [xEntryGlobalDb]
GO

if EXISTS(select * from sys.procedures where name='ps_db_eDrc_wwf_Pull_Tar')
	drop proc ps_db_eDrc_wwf_Pull_Tar
go

if EXISTS(select * from sys.procedures where name='ps_db_eDrc_wwf_Pull_Pr')
	drop proc ps_db_eDrc_wwf_Pull_Pr
go

if EXISTS(select * from sys.procedures where name='ps_db_eDrc_wwf_Pull_Ident_Pepi')
	drop proc ps_db_eDrc_wwf_Pull_Ident_Pepi
go

if EXISTS(select * from sys.procedures where name='ps_db_eDrc_wwf_Pull_Suivi_Pepi')
	drop proc ps_db_eDrc_wwf_Pull_Suivi_Pepi
go

/****** Object ::  StoredProcedure [dbo].[ps_db_eDrc_wwf_Data_Pull]    Script Date: 09/17/2018 14:43:08 ******/
/** Server name :: WWF_SERVER12\SQLSERVWWFE18 **/
/** Server AppEngine :: https://wwfdrc-02.appspot.com **/


-- 1ere etape : Telecharger les donnees du serveur ODK a partir de ODKBriefcase # Odk vers Ordinateur
-- 2ème étape : exécuter l'application C# qui importe les données dans la bd # Ordinateur vers SqlServer
-- Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_enquete_menage_d5 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
-- Exec xp_cmdshell 'D:\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche d''enquête Menage (Draft 5)\instances" -g0'


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[ps_db_eDrc_wwf_Pull_Tar]
as
-- 1. TAR
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_terrain_reboiser_v5 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'D:\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche de terrain à reboiser (v5)\instances" -g1'
GO

CREATE proc [dbo].[ps_db_eDrc_wwf_Pull_Pr]
as
-- 2. Suivi Plantation
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_suivi_plantation_d7 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'D:\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche de suivi plantation (Draft 7)\instances" -g2'
GO


CREATE proc [dbo].[ps_db_eDrc_wwf_Pull_Ident_Pepi]
as
-- 3. Identification pepiniere
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_identification_pepiniere_v5 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'D:\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche d''identification pépinière (v5)\instances" -g3'
GO

CREATE proc [dbo].[ps_db_eDrc_wwf_Pull_Suivi_Pepi]
as
-- 4. Suivi Pepiniere
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_suivi_pepiniere_v5 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'D:\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche de suivi pépinière (v5)\instances" -g4'
GO


-- 1. TAR
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_terrain_reboiser_v5 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'D:\ODK\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche de terrain à reboiser (v5)\instances" -g1'
--2. IDENTIF PEPI
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_identification_pepiniere_v6 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'D:\ODK\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche identification pepiniere (v6)\instances" -g3'
--3. SUIVI PEPI
Exec xp_cmdshell 'java -jar D:\ODK\ODKBriefcaseProduction.jar -id fiche_suivi_pepiniere_v5 -sd D:\ODK -url https://wwfdrc-02.appspot.com -u bienfait -p @juillet2017'
Exec xp_cmdshell 'D:\ODK\Release\xEntryFmpOdkXmlImporter.exe -sWWF_SERVER12\SQLSERVWWFE18  -dxEntryGlobalDb -uwwfadmin -proot//777Wwf -f"D:\ODK\ODK Briefcase Storage\forms\Fiche de suivi pépinière (v5)\instances" -g4'

