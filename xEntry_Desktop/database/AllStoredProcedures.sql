--exec sp_tbl_fiche_pr_criteria 'Grande Saison 2017'
--exec sp_tbl_fiche_pr_criteria '0001'

--tbl_fiche_pr
CREATE PROCEDURE sp_tbl_fiche_pr_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_fiche_pr  WHERE 1=1
	OR uuid LIKE @criteria
	OR deviceid LIKE @criteria
	OR nom_agent LIKE @criteria
	OR saison LIKE @criteria
	OR association LIKE @criteria
	OR association_autre LIKE @criteria
	OR bailleur LIKE @criteria
	OR bailleur_autre LIKE @criteria
	OR n_visite LIKE @criteria
	OR contreverification LIKE @criteria
	OR n_plantation LIKE @criteria
	OR n_bloc LIKE @criteria
	OR noms_planteur LIKE @criteria
	OR nom LIKE @criteria
	OR post_nom LIKE @criteria
	OR prenom LIKE @criteria
	OR sexes LIKE @criteria
	OR planteur_present LIKE @criteria
	OR changement_surface LIKE @criteria
	OR titre_trace_gps LIKE @criteria
	OR periode_debut LIKE @criteria
	OR preiode_debut_annee LIKE @criteria
	OR periode_fin LIKE @criteria
	OR period_fin_annee LIKE @criteria
	OR essence_principale LIKE @criteria
	OR essence_principale_autre LIKE @criteria
	OR melanges LIKE @criteria
	OR rpt_b LIKE @criteria
	OR encartement_type LIKE @criteria
	OR ecartement_dim_1 LIKE @criteria
	OR ecartement_dim_2 LIKE @criteria
	OR alignement LIKE @criteria
	OR causes LIKE @criteria
	OR piquets LIKE @criteria
	OR pourcentage_insuffisants LIKE @criteria
	OR eucalyptus_deau LIKE @criteria
	OR type_degats LIKE @criteria
	OR n_vaches LIKE @criteria
	OR n_chevres LIKE @criteria
	OR n_rats LIKE @criteria
	OR n_termites LIKE @criteria
	OR n_elephants LIKE @criteria
	OR n_cultures_vivrieres LIKE @criteria
	OR n_erosion LIKE @criteria
	OR n_eboulement LIKE @criteria
	OR n_feu LIKE @criteria
	OR n_secheresse LIKE @criteria
	OR n_hommes LIKE @criteria
	OR n_plante_avec_sachets LIKE @criteria
	OR n_plante_trop_tard LIKE @criteria
	OR n_guerren LIKE @criteria
	OR regarnissage LIKE @criteria
	OR regarnissage_suffisant LIKE @criteria
	OR entretien LIKE @criteria
	OR etat LIKE @criteria
	OR cultures_vivrieres LIKE @criteria
	OR type_cultures_vivieres LIKE @criteria
	OR type_cultures_vivieres_autr LIKE @criteria
	OR n_haricots LIKE @criteria
	OR n_manioc LIKE @criteria
	OR n_soja LIKE @criteria
	OR n_sorgho LIKE @criteria
	OR n_arachides LIKE @criteria
	OR n_patates_douces LIKE @criteria
	OR n_mais LIKE @criteria
	OR n_autres LIKE @criteria
	OR canopee_fermee LIKE @criteria
	OR croissance_arbres LIKE @criteria
	OR arbres_existants LIKE @criteria
	OR rpt_c LIKE @criteria
	OR emplacement LIKE @criteria
	OR photo_2 LIKE @criteria
	OR emplacement_2 LIKE @criteria
	OR localisation LIKE @criteria
	OR commentaire_wwf LIKE @criteria
	OR commentaire_planteur LIKE @criteria
	OR commentaire_association LIKE @criteria
	OR eucalyptus_deau_non LIKE @criteria
END
GO

--tbl_fiche_menage
CREATE PROCEDURE sp_tbl_fiche_menage_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_fiche_menage  WHERE 1=1
	OR uuid LIKE @criteria
	OR deviceid LIKE @criteria
	OR questionnaire_id LIKE @criteria
	OR name LIKE @criteria
	OR id_menage LIKE @criteria
	OR nom_menage LIKE @criteria
	OR deuxio_representant LIKE @criteria
	OR village_menage LIKE @criteria
	OR province LIKE @criteria
	OR groupement LIKE @criteria
	OR territoire LIKE @criteria
	OR zs LIKE @criteria
	OR camps LIKE @criteria
	OR localisation LIKE @criteria
	OR rpt_gps LIKE @criteria
END
GO

--tbl_localisation_poly
CREATE PROCEDURE sp_tbl_localisation_poly_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_localisation_poly  WHERE 1=1
	OR uuid LIKE @criteria
	OR name_point LIKE @criteria
	OR localisation_poly LIKE @criteria
END
GO

--tbl_autre_essence_mel_fiche_pr
CREATE PROCEDURE sp_tbl_autre_essence_mel_fiche_pr_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_autre_essence_mel_fiche_pr  WHERE 1=1
	OR uuid LIKE @criteria
	OR autre_essence LIKE @criteria
	OR autre_essence_autre LIKE @criteria
END
GO

--tbl_arbres_fiche_pr
CREATE PROCEDURE sp_tbl_arbres_fiche_pr_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_arbres_fiche_pr  WHERE 1=1
	OR uuid LIKE @criteria
END
GO

--tbl_fiche_ident_pepi
CREATE PROCEDURE sp_tbl_fiche_ident_pepi_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_fiche_ident_pepi  WHERE 1=1
	OR uuid LIKE @criteria
	OR deviceid LIKE @criteria
	OR agent LIKE @criteria
	OR saison LIKE @criteria
	OR association LIKE @criteria
	OR association_autre LIKE @criteria
	OR bailleur LIKE @criteria
	OR bailleur_autre LIKE @criteria
	OR id LIKE @criteria
	OR nom_site LIKE @criteria
	OR territoire LIKE @criteria
	OR village LIKE @criteria
	OR localite LIKE @criteria
	OR territoire LIKE @criteria
	OR chefferie LIKE @criteria
	OR groupement LIKE @criteria
	OR grp_c LIKE @criteria
	OR contrat LIKE @criteria
	OR localisation LIKE @criteria
	OR observations LIKE @criteria
END
GO

--tbl_grp_c_fiche_ident_pepi
CREATE PROCEDURE sp_tbl_grp_c_fiche_ident_pepi_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_grp_c_fiche_ident_pepi  WHERE 1=1
	OR uuid LIKE @criteria
END
GO

--tbl_fiche_suivi_pepi
CREATE PROCEDURE sp_tbl_fiche_suivi_pepi_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_fiche_suivi_pepi  WHERE 1=1
	OR uuid LIKE @criteria
	OR deviceid LIKE @criteria
	OR agent LIKE @criteria
	OR saison LIKE @criteria
	OR association LIKE @criteria
	OR association_autre LIKE @criteria
	OR bailleur LIKE @criteria
	OR bailleur_autre LIKE @criteria
	OR nom_site LIKE @criteria
	OR identifiant_pepiniere LIKE @criteria
	OR ronde_suivi_pepiniere LIKE @criteria
	OR grp_c LIKE @criteria
	OR grp_f LIKE @criteria
	OR tassement_sachet LIKE @criteria
	OR binage LIKE @criteria
	OR classement_taille LIKE @criteria
	OR classement_espece LIKE @criteria
	OR cernage LIKE @criteria
	OR etetage LIKE @criteria
	OR localisation LIKE @criteria
END
GO

--tbl_germoir_fiche_suivi_pepi
CREATE PROCEDURE sp_tbl_germoir_fiche_suivi_pepi_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_germoir_fiche_suivi_pepi  WHERE 1=1
	OR uuid LIKE @criteria
	OR germoir_essence LIKE @criteria
	OR germoir_essence_autre LIKE @criteria
	OR provenance LIKE @criteria
	OR type_de_semis LIKE @criteria
	OR bien_plat LIKE @criteria
	OR arrosage LIKE @criteria
	OR desherbage LIKE @criteria
	OR qualite_semis LIKE @criteria
END
GO

--tbl_plant_repiq_fiche_suivi_pepi
CREATE PROCEDURE sp_tbl_plant_repiq_fiche_suivi_pepi_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_plant_repiq_fiche_suivi_pepi  WHERE 1=1
	OR uuid LIKE @criteria
	OR planches_repiquage_essence LIKE @criteria
	OR planches_repiquage_essence_autre LIKE @criteria
	OR observations LIKE @criteria
END
GO

--tbl_territoire
CREATE PROCEDURE sp_tbl_territoire_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_territoire  WHERE 1=1
	OR territoire LIKE @criteria
END
GO

--tbl_groupement
CREATE PROCEDURE sp_tbl_groupement_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_groupement  WHERE 1=1
	OR groupement LIKE @criteria
END
GO

--tbl_localite
CREATE PROCEDURE sp_tbl_localite_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_localite  WHERE 1=1
	OR localite LIKE @criteria
END
GO

--tbl_chefferie
CREATE PROCEDURE sp_tbl_chefferie_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_chefferie  WHERE 1=1
	OR chefferie LIKE @criteria
END
GO

--tbl_village
CREATE PROCEDURE sp_tbl_village_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_village  WHERE 1=1
	OR village LIKE @criteria
END
GO

--tbl_saison
CREATE PROCEDURE sp_tbl_saison_criteria @criteria varchar(25)
AS 
BEGIN
	SELECT *  FROM tbl_saison  WHERE 1=1
	OR id_saison LIKE @criteria
	OR saison LIKE @criteria
END
GO

--tbl_agent
CREATE PROCEDURE sp_tbl_agent_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_agent  WHERE 1=1
	OR id_agent LIKE @criteria
	OR agent LIKE @criteria
END
GO

--tbl_association
CREATE PROCEDURE sp_tbl_association_criteria @criteria varchar(50)
AS 
BEGIN
	SELECT *  FROM tbl_association  WHERE 1=1
	OR id_asso LIKE @criteria
	OR association LIKE @criteria
END
GO

--tbl_bailleur
CREATE PROCEDURE sp_tbl_bailleur_criteria @criteria varchar(75)
AS 
BEGIN
	SELECT *  FROM tbl_bailleur  WHERE 1=1
	OR id_bailleur LIKE @criteria
	OR bailleur LIKE @criteria
END
GO

--tbl_saison_assoc
CREATE PROCEDURE sp_tbl_saison_assoc_criteria @criteria varchar(50)
AS 
BEGIN
	SELECT *  FROM tbl_saison_assoc  WHERE 1=1
	OR id_asso LIKE @criteria
	OR id_saison LIKE @criteria
	OR id_saison_assoc LIKE @criteria
	OR numero_contrat_asso LIKE @criteria
END
GO

--tbl_essence_plant
CREATE PROCEDURE sp_tbl_essence_plant_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_essence_plant  WHERE 1=1
	OR id_essence LIKE @criteria
	OR essence LIKE @criteria
END
GO

--tbl_utilisateur
CREATE PROCEDURE sp_tbl_utilisateur_criteria @criteria varchar(1000)
AS 
BEGIN
	SELECT *  FROM tbl_utilisateur  WHERE 1=1
	OR id_agentuser LIKE @criteria
	OR nomuser LIKE @criteria
	OR motpass LIKE @criteria
	OR schema_user LIKE @criteria
	OR droits LIKE @criteria
END
GO

--tbl_groupe
CREATE PROCEDURE sp_tbl_groupe_criteria @criteria varchar(30)
AS 
BEGIN
	SELECT *  FROM tbl_groupe  WHERE 1=1
	OR designation LIKE @criteria
END
GO

--tbl_fiche_tar
CREATE PROCEDURE sp_tbl_fiche_tar_criteria @criteria varchar(255)
AS 
BEGIN
	SELECT *  FROM tbl_fiche_tar  WHERE 1=1
	OR uuid LIKE @criteria
	OR deviceid LIKE @criteria
	OR agent LIKE @criteria
	OR saison LIKE @criteria
	OR association LIKE @criteria
	OR association_autre LIKE @criteria
	OR bailleur LIKE @criteria
	OR bailleur_autre LIKE @criteria
	OR deja_participe LIKE @criteria
	OR nom LIKE @criteria
	OR postnom LIKE @criteria
	OR prenom LIKE @criteria
	OR sexes LIKE @criteria
	OR nom_lieu_plantation LIKE @criteria
	OR village LIKE @criteria
	OR localite LIKE @criteria
	OR territoire LIKE @criteria
	OR chefferie LIKE @criteria
	OR groupement LIKE @criteria
	OR type_id LIKE @criteria
	OR type_id_autre LIKE @criteria
	OR nombre_id LIKE @criteria
	OR emplacement LIKE @criteria
	OR essence_principale LIKE @criteria
	OR essence_principale_autre LIKE @criteria
	OR objectifs_planteur LIKE @criteria
	OR objectifs_planteur_autre LIKE @criteria
	OR utilisation_precedente LIKE @criteria
	OR autre_precedente_preciser LIKE @criteria
	OR arbres_existants LIKE @criteria
	OR situation LIKE @criteria
	OR pente LIKE @criteria
	OR sol LIKE @criteria
	OR eucalyptus LIKE @criteria
	OR point_deau_a_proximite LIKE @criteria
	OR chef_de_localite LIKE @criteria
	OR chef_nom LIKE @criteria
	OR chef_postnom LIKE @criteria
	OR chef_prenom LIKE @criteria
	OR autre LIKE @criteria
	OR autre_fonction LIKE @criteria
	OR autre_nom LIKE @criteria
	OR autre_postnom LIKE @criteria
	OR autre_prenom LIKE @criteria
	OR document_de_propriete LIKE @criteria
	OR preciser_document LIKE @criteria
	OR autre_document LIKE @criteria
	OR observations LIKE @criteria
END
GO

--tbl_geopoint
CREATE PROCEDURE sp_tbl_geopoint_criteria @criteria varchar(100)
AS 
BEGIN
	SELECT *  FROM tbl_geopoint  WHERE 1=1
	OR uuid LIKE @criteria
	OR deviceid LIKE @criteria
	OR latitude LIKE @criteria
	OR longitude LIKE @criteria
	OR altitude LIKE @criteria
	OR EPE LIKE @criteria
	OR geo_type LIKE @criteria
END
GO

--sp_login_user_bd
CREATE PROCEDURE sp_login_user_bd @username varchar(30),@password varchar(1000),@database varchar(255)
AS 
BEGIN
	EXEC sp_addlogin @username,@password,@database
	EXEC sp_grantdbaccess @username
END
GO

--sp_admin_permission_bd
CREATE PROCEDURE sp_admin_permission_bd @username varchar(30),@loginname varchar(30)
AS 
BEGIN
	EXEC sp_addsrvrolemember @loginname,'sysadmin' 
    EXEC sp_addsrvrolemember @loginname,'securityadmin' 
    EXEC sp_addsrvrolemember @loginname,'dbcreator' 
    EXEC sp_addrolemember 'db_owner', @username
    EXEC sp_addrolemember 'db_ddladmin',@username
    EXEC sp_addrolemember 'db_accessadmin',@username
END
GO

