use master
go

if EXISTS(select * from sys.databases where name='xEntryGlobalDb')
	drop database xEntryGlobalDb
go

create database xEntryGlobalDb
go

USE xEntryGlobalDb
GO

/****** Table TAR ******/
-- Table TAR (Terrain à Reboiser)
CREATE TABLE tbl_fiche_tar(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	deviceid varchar(100) NOT NULL,
	date datetime NOT NULL,
	agent varchar(255),
	saison varchar(255),
	association varchar(255),
	association_autre varchar(255),
	bailleur varchar(255),
	bailleur_autre varchar(255),
	n_plantation integer,
	deja_participe varchar(20),
	n_plantations integer,
	nom varchar(255) NOT NULL,
	postnom varchar(255),
	prenom varchar(255),
	sexes varchar(255),
	nom_lieu_plantation varchar(255),
	village varchar(255),
	localite varchar(255),
	territoire varchar(255),
	chefferie varchar(255),
	groupement varchar(255),
	type_id  varchar(255),
	type_id_autre varchar(255),
	nombre_id varchar(255),
	photo_id image,
	photo_planteur image,
	photo_terrain image,
	emplacement  varchar(255), --geopoint ou geolocalisation
	essence_principale varchar(255),
	essence_principale_autre varchar(255),
	superficie_totale float,
	objectifs_planteur varchar(255),
	objectifs_planteur_autre varchar(255),
	utilisation_precedente varchar(255),
	autre_precedente_preciser varchar(255),
	utilisation_precedente_depuis datetime,
	arbres_existants varchar(255),
	ombre_arbres integer,
	situation varchar(255),
	pente varchar(255),
	sol varchar(255),
	eucalyptus varchar(255),
	point_deau_a_proximite  varchar(255),
	env_point_deau_a_proximite INT,
	chef_de_localite varchar(255),
	chef_nom varchar(255),
	chef_postnom varchar(255),
	chef_prenom varchar(255),
	autre varchar(255),
	autre_fonction varchar(255),
	autre_nom varchar(255),
	autre_postnom varchar(255),
	autre_prenom varchar(255),
	document_de_propriete varchar(255),
	preciser_document varchar(255),
	autre_document varchar(255),
	photo_document_de_propriet image,
	observations varchar(255),
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_fiche_tar primary key(id)
)
GO

-- City varchar(255) DEFAULT 'Sandnes'
-- OrderDate date DEFAULT GETDATE()

-- ALTER TABLE Persons
-- ADD CONSTRAINT df_City 
-- DEFAULT 'Sandnes' FOR City;

-- ALTER TABLE Persons
-- ALTER COLUMN City DROP DEFAULT;

/****** Table GEO ******/
-- Table geopoint (Permet de stocker tout les points gps du terrain)
CREATE TABLE tbl_geopoint(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	deviceid varchar(100) NOT NULL,
	latitude varchar(50) NOT NULL,
	longitude varchar(50) NOT NULL,
	altitude varchar(50) NOT NULL,
	EPE varchar(50) NOT NULL,
	geo_type varchar(50),
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_geopoint primary key(id)
)
GO

/****** Table Menage ******/
-- Table Fiche Menage (juste pour le Test)
CREATE TABLE tbl_fiche_menage(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	deviceid varchar(100) NOT NULL,
	date datetime NOT NULL,
	questionnaire_id varchar(50) NOT NULL,
	name varchar(50),
	id_menage varchar(50) NOT NULL,
	nom_menage varchar(50),
	deuxio_representant varchar(50) NOT NULL,
	taille_menage integer NOT NULL,
	village_menage varchar(50) NOT NULL,
	province varchar(50) NOT NULL,
	groupement varchar(50) NOT NULL,
	territoire varchar(50) NOT NULL,
	zs varchar(50) NOT NULL,
	camps varchar(50) NOT NULL,
	localisation varchar(255) NOT NULL,
	rpt_gps varchar(10),-- pour le repeat des valeurs
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_fiche_menage primary key(id),
	constraint uk_tbl_fiche_menage_uuid unique(uuid)
)
GO

/****** Sous-Table Menage ******/
CREATE TABLE tbl_localisation_poly(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	name_point varchar(50) NOT NULL,
	localisation_poly varchar(255) NOT NULL,
	constraint pk_tbl_localisation_poly primary key(id)
)
GO

--photo_idp image NOT NULL,

-- drop table tbl_fiche_pr;

/****** Table PR ******/
-- Table PR (Plantation réalisée)
CREATE TABLE tbl_fiche_pr
(
	id int identity(1,1),
	uuid varchar(255) NOT NULL,
	deviceid varchar(255) NOT NULL,
	date DateTime NOT NULL,
	nom_agent varchar(255),
	saison varchar(255),
	association varchar(255),
	association_autre varchar(255),
	bailleur varchar(255),
	bailleur_autre varchar(255),
	n_visite varchar(255),
	contreverification varchar(255),
	n_visite_2 integer,
	n_viste_3 integer,
	visite_calculate integer,
	n_plantation varchar(255),
	n_bloc varchar(255),
	noms_planteur varchar(255),
	nom varchar(255),
	post_nom varchar(255),
	prenom varchar(255),
	sexes varchar(100),
	planteur_present varchar(255),
	changement_surface varchar(255),
	titre_trace_gps varchar(255),
	superficie float,
	superficie_non_plantee float,
	periode_debut varchar(255),
	preiode_debut_annee varchar(255),
	periode_fin varchar(255),
	period_fin_annee varchar(255),
	essence_principale varchar(255),
	essence_principale_autre varchar(255),
	melanges varchar(255),
	rpt_b varchar(100), -- Concerne Repeat node rpt_b 
	pente_1 integer,
	pente_2 integer,
	pente_3 integer,
	pente_4 integer,
	encartement_type varchar(255),
	ecartement_dim_1 varchar(255),
	ecartement_dim_2 varchar(255),
	alignement varchar(255),
	causes varchar(255),
	piquets varchar(255),
	pourcentage_insuffisants varchar(255),
	eucalyptus_deau varchar(255),
	n_vides integer,
	n_zero_demi_metre integer,
	n_demi_un_metre integer,
	n_un_deux_metre integer,
	n_deux_plus_metre integer,
	p_zero_demi_metre_calc integer,
	p_demi_un_metre_calc integer,
	p_un_deux_metre_calc integer,
	p_deux_plus_metre_calc integer,
	degats_calc integer,
	type_degats varchar(255),
	n_vaches varchar(255),
	n_chevres varchar(255),
	n_rats varchar(255),
	n_termites varchar(255),
	n_elephants varchar(255),
	n_cultures_vivrieres varchar(255),
	n_erosion varchar(255),
	n_eboulement varchar(255),
	n_feu varchar(255),
	n_secheresse varchar(255),
	n_hommes varchar(255),
	n_plante_avec_sachets varchar(255),
	n_plante_trop_tard varchar(255),
	n_guerren varchar(255),
	degats_total integer,
	regarnissage varchar(255),
	regarnissage_suffisant varchar(255),
	entretien varchar(255),
	etat varchar(255),
	cultures_vivrieres varchar(255),
	type_cultures_vivieres varchar(255),
	type_cultures_vivieres_autr varchar(255),
	n_haricots varchar(255),
	n_manioc varchar(255),
	n_soja varchar(255),
	n_sorgho varchar(255),
	n_arachides varchar(255),
	n_patates_douces varchar(255),
	n_mais varchar(255),
	n_autres varchar(255),
	type_cultures_total integer,
	canopee_fermee varchar(255),
	superficie_canopee_fermee float,
	croissance_arbres varchar(255),
	arbres_existants varchar(255),
	rpt_c varchar(10), -- Concerne Repeat node rpt_c
	emplacement varchar(255), --geopoint pour photo 1
	emplacement_2 varchar(255), --geopoint pour photo 2
	localisation varchar(255), --geopoint PR
	commentaire_wwf varchar(255),
	commentaire_planteur varchar(255),
	commentaire_association varchar(255),
	eucalyptus_deau_non varchar(255),
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_fiche_pr primary key(id),
	constraint uk_tbl_fiche_pr_uuid unique(uuid)
)
GO

/****** Sous-Table fiche PR rpt_b et rpt_c ******/
-- Recuperation des repeat pour la fiche PR
-- Repeat Node rpt_b (Autre essence melangee)
CREATE TABLE tbl_autre_essence_mel_fiche_pr
(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	autre_essence varchar(255),
	autre_essence_autre varchar(255),
	autre_essence_pourcentage integer,
	autre_essence_count integer,
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_autre_essence_mel_fiche_pr primary key(id)
)
GO

-- Repeat Node rpt_c (Arbre)
CREATE TABLE tbl_arbres_fiche_pr
(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	hauteur_total float,
	hauteur_tronc float,
	houppier_1 integer,
	houppier_2 integer,
	diametre float,
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_arbres_fiche_pr primary key(id)
)
GO

/****** Table Ident Pepi ******/
-- Table d'identification des pepinieres
-- id Odk : fiche_identification_pepiniere_v5
-- title odk : Fiche d’identification pépinière (v5)
CREATE TABLE tbl_fiche_ident_pepi
(
	pid int identity(1,1),
	uuid varchar(100) NOT NULL,
	deviceid varchar(100) NOT NULL,
	date datetime NOT NULL,
	agent  varchar(255),
	saison  varchar(255),
	association  varchar(255),
	association_autre  varchar(255),
	bailleur  varchar(255),
	bailleur_autre  varchar(255),
	id  varchar(255), -- Numero de pepiniere
	nom_site varchar(255),
	village varchar(255),
	localite varchar(255),
	territoire varchar(255),
	chefferie varchar(255),
	groupement varchar(255),
	date_installation_pepiniere datetime,
	grp_c varchar(255), -- pour le repeat des valeurs
	nb_pepinieristes integer,
	nb_pepinieristes_formes integer,
	contrat varchar(255),
	combien_pepinieristes integer,
	localisation  varchar(255), -- geopoint
	photo image,
	observations varchar(255),
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_fiche_ident_pepi primary key(pid),
	constraint uk_tbl_fiche_ident_pepi_uuid unique(uuid)
)
GO

/****** Sous-Table identification pepiniere grp_c ******/
-- Recupere les elements repetes dans grp_c de fiche_ident_pepiniere_v5
CREATE TABLE tbl_grp_c_fiche_ident_pepi
(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	count float, --c'est un champ calcule
	dimension_planche_a float,
	dimension_planche_b float,
	capacite_planche integer,
	capacite_totale_planche integer,
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_grp_c_fiche_ident_pepi primary key(id)
)
GO

/****** Table Suivi Pepiniere ******/
-- Table de suivi des pepinieres
-- id Odk : fiche_suivi_pepiniere_v5
-- title odk : Fiche de suivi pépinière (v5)
CREATE TABLE tbl_fiche_suivi_pepi
(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	deviceid varchar(100) NOT NULL,
	date datetime NOT NULL,
	agent varchar(255),
	saison varchar(255),
	association varchar(255),
	association_autre varchar(255),
	bailleur varchar(255),
	bailleur_autre varchar(255),
	nom_site varchar(255),
	identifiant_pepiniere varchar(255),
	ronde_suivi_pepiniere varchar(255),
	grp_c varchar(10),
	grp_f varchar(10),
	superficie_potentielle_note float, --- note
	superficie_potentielle_2 float, -- note 2mx2m
	superficie_potentielle_2_5 float, -- note 2.5x2.5m
	superficie_potentielle_3 float, -- note 3x3m
	tassement_sachet varchar(255),
	binage varchar(255),
	classement_taille varchar(255),
	classement_espece varchar(255),
	cernage varchar(255),
	etetage varchar(255),
	localisation varchar(255), -- geopoint
	photo image,
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_fiche_suivi_pepi primary key(id),
	constraint uk_tbl_fiche_suivi_pepi_uuid unique(uuid)
)
GO

/****** Sous-Table suivi pepiniere grp_c et grp_f ******/
-- Recupere les elements repetes dans grp_c de tbl_fiche_suivi_pepi
-- correspond au groupe repeat grp_c (Germoir)
CREATE TABLE tbl_germoir_fiche_suivi_pepi
(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	germoir_essence varchar(255),
	germoir_essence_autre varchar(255),
	provenance varchar(255),
	qte_semee integer,
	date_semis datetime,
	date_premiere_levee datetime,
	type_de_semis varchar(255),
	bien_plat varchar(255),
	arrosage varchar(255),
	desherbage varchar(255),
	qualite_semis varchar(255),
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_germoir_fiche_suivi_pepi primary key(id)
)
GO

-- correspond au groupe repeat grp_f (Plantules repiquees)
CREATE TABLE tbl_plant_repiq_fiche_suivi_pepi
(
	id int identity(1,1),
	uuid varchar(100) NOT NULL,
	planches_repiquage_essence varchar(255),
	planches_repiquage_essence_autre varchar(255),
	plantules_encore_repiques integer,
	plantules_deja_evacues integer,
	qte_observee integer,
	date_repiquage datetime,
	taille_moyenne integer,
	nbre_feuille_moyenne integer,
	planches_repiquage_count float, -- calculate
	observations varchar(255),
	synchronized_on datetime NOT NULL,
	constraint pk_tbl_plant_repiq_fiche_suivi_pepi primary key(id)
)
GO

-- Tables secondaires 

create table tbl_territoire
(
	idt int identity(1,1),
	territoire varchar(100),
	constraint pk_ter primary key(idt),
	constraint uk_ter unique(territoire)
)
GO

create table tbl_groupement
(
	idg int identity(1,1),
	idt int not null,
	groupement varchar(100),
	constraint pk_grou primary key(idg),
	constraint fk_grou_ter foreign key(idt) references tbl_territoire(idt),
	constraint uk_gr unique(groupement)
)
GO

create table tbl_localite
(
	idl int identity(1,1),
	idg int not null,
	localite varchar(100),
	constraint pk_loc primary key(idl),
	constraint fk_loc_grou foreign key(idg) references tbl_groupement(idg),
	constraint uk_loc unique(localite)
)
GO

create table tbl_chefferie
(
	idc int identity(1,1),
	idl int not null,
	chefferie varchar(100),
	constraint pk_chef primary key(idc),
	constraint fk_loc_chef foreign key(idl) references tbl_localite(idl),
	constraint uk_chef unique(chefferie)
)
GO

create table tbl_village
(
	idv int identity(1,1),
	idc int not null,
	village varchar(100),
	constraint pk_vill primary key(idv),
	constraint fk_vil_chef foreign key(idc) references tbl_chefferie(idc),
	constraint uk_vill unique (village)
)
GO

-- Table Saison
CREATE TABLE tbl_saison 
(
	id_saison varchar(6) NOT NULL,
	saison varchar(25) NOT NULL,
	constraint pk_saison PRIMARY KEY (id_saison),
	constraint uk_saison unique (saison)
)
GO

-- Table Agent WWF (Forestier)
CREATE TABLE tbl_agent 
(
	id_agent varchar(6) NOT NULL,
	agent varchar(100) NOT NULL,
	constraint pk_agent PRIMARY KEY (id_agent),
	constraint uk_agent unique (agent)
)
GO

CREATE TABLE tbl_association 
(
	id_asso varchar(25) NOT NULL,
	association varchar(50) NOT NULL,
	constraint pk_LSP PRIMARY KEY (id_asso),
	constraint uk_nameasso UNIQUE (association)
)
GO

CREATE TABLE tbl_bailleur 
(
	id_bailleur varchar(20) NOT NULL,
	bailleur varchar(75) NOT NULL,
	constraint pk_donor PRIMARY KEY (id_bailleur),
	constraint uk_namedonor UNIQUE (bailleur)
)
GO

CREATE TABLE tbl_saison_assoc
(
	id_asso varchar(25) NOT NULL,
	id_saison varchar(6) NOT NULL,
	id_saison_assoc varchar(32) NOT NULL,
	numero_contrat_asso varchar(50) NOT NULL,
	surf_contr float(4) default 0.00,
	constraint pk_season_asso primary key(id_saison_assoc),
	constraint fk_season foreign key(id_saison) references tbl_saison(id_saison),
	constraint fk_asso foreign key(id_asso) references tbl_association(id_asso),
	constraint uk_numeroContrat UNIQUE(numero_contrat_asso)
)
GO

CREATE TABLE tbl_essence_plant 
(
	id_essence varchar(20) NOT NULL,
	essence varchar(100) NOT NULL,
	constraint pk_essence PRIMARY KEY (id_essence),
	constraint uk_essence UNIQUE (essence)
)
GO

-- ---------------------------------------------------------------------------------------------------------------------------------------------------------
-- Insertion dans les tables 

-- Table Saison
INSERT INTO tbl_saison values ('2007-2','Grande Saison 2007'),('2008-2','Grande Saison 2008'),('2009-1','Petite Saison 2009'),('2009-2','Grande Saison 2009'),('2010-1','Petite Saison 2010'),('2010-2','Grande Saison 2010'),('2011-1','Petite Saison 2011'),('2011-2','Grande Saison 2011'),('2012-1','Petite Saison 2012'),('2012-2','Grande Saison 2012'),('2013-1','Petite Saison 2013'),
									  ('2013-2','Grande Saison 2013'),('2014-1','Petite Saison 2014'),('2014-2','Grande Saison 2014'),('2015-1','Petite Saison 2015'),('2015-2','Grande Saison 2015'),('2016-1','Petite Saison 2016'),('2016-2','Grande Saison 2016'),('2017-1','Petite Saison 2017'),('2017-2','Grande Saison 2017'),('2018-1','Petite Saison 2018'),('2018-2','Grande Saison 2018'),
									  ('2019-1','Petite Saison 2019'),('2019-2','Grande Saison 2019'),('2020-1','Petite Saison 2020'),('2020-2','Grande Saison 2020'),('2021-1','Petite Saison 2021'),('2021-2','Grande Saison 2021'),('2022-1','Petite Saison 2022'),('2022-2','Grande Saison 2022'),('2023-1','Petite Saison 2023'),('2023-2','Grande Saison 2023'),('2024-2','Grande Saison 2024'),
									  ('2024-1','Petite Saison 2024'),('2025-2','Grande Saison 2025'),('2025-1','Petite Saison 2025')
-- Table Bailleur
INSERT INTO tbl_bailleur values ('cbff','CBFF'),('cifor','CIFOR'),('dgd','GDG'),('sida','SIDA')
-- Table Essence
INSERT INTO tbl_essence_plant values ('ess01','Eucalyptus saligna'),('ess02','Eucalyptus maidenii'),('ess03','Eucalytptus camaldulensis'),('ess04','Grevillea robusta'),('ess05','Acacia mearnsii'),('ess06','Cedrela serrata'),('ess07','Senna siamea')
-- Table Agent
INSERT INTO tbl_agent values ('FV1','Bienfait Ntamirwa'),('FV2','Didier Weston'),('FV3','Archippe Sivaghanzana'),
									 ('FV4','Eloge Muhesi'),('FV5','Nestor Vuambale'),('FV6','Olivier Kalumuna'),
									 ('FV7','Paluku Vhosi'),('FV8','Paluku Vururu'),('FV9','Pierre Vutwire')
-- Table Association
INSERT INTO tbl_association values('PSL001','ASEEK'),('PSL002','ACODRI'),('PSL003','ONDE'),('PSL004','CECLAV'),('PSL005','PADA'),('PSL006','OPERL'),('PSL007','RPVA'),('PSL008','OSCUKA'),('PSL009','AAP'),('PSL010','JEAN'),('PSL011','CICEKI'),('PSL012','OPEGL'),('PSL013','AMEMU'),('PSL014','AFED'),('PSL015','ALCOODEBU'),('PSL016','PAFRDC'),
						('PSL017','SIPROFFA'),('PSL018','CIPSOPA'),('PSL019','SAUVE NATURE'),('PSL020','APCEN'),('PSL021','CENED'),('PSL022','CETEI'),('PSL023','MESADI'),('PSL024','ACLP'),('PSL025','FOD'),('PSL026','RAPNAV'),('PSL027','MUMALUKU'),('PSL028','ADESEC'),('PSL029','ACUCOBA'),('PSL030','AFEDER'),('PSL031','UCOPAD'),
						('PSL032','APIPA'),('PSL033','APRPE'),('PSL034','APROLERU'),('PSL035','COFODI'),('PSL036','PAF-RDC'),('PSL037','PDL'),('PSL038','PIED'),('PSL039','GROS_PROPRIO'),('PSL040','ESF'),('PSL041','SOS_NATURE'),('PSL042','CODESA'),('PSL043','CTFC'),('PSL044','CDR'),('PSL045','OAN'),('PSL046','FACF'),('PSL047','ASAF'),('PSL048','LCDP'),('PSL049','PAEDE')
-- Table saison_association 
-- en fonction des contrats signés par le WWF avec les associations par saison
INSERT INTO tbl_saison_assoc values('PSL001','2007-2','ASEEK2007-2','FY7/ECOMAKA/001',50),('PSL020','2010-2','APCEN2010-2','FY10/ARMFEP/ECOMAKALA/P36',20),('PSL021','2010-2','CENED2010-2','FY10/ARMFEP/ECOMAKALA/P37',20),('PSL011','2010-2','CICEKI2010-2','FY10/ARMFEP/ECOMAKALA/P38',30),('PSL023','2010-2','MESADI2010-2','FY10/ARMFEP/ECOMAKALA/P39',30),
						('PSL036','2010-2','PAFR2010-2','FY10/ARMFEP/ECOMAKALA/P31',15),('PSL037','2010-2','PDL2010-2','FY10/ARMFEP/ECOMAKALA/P42',15),('PSL012','2010-2','OPEGL2010-2','FY10/ARMFEP/ECOMAKALA/P40',30),('PSL003','2011-2','ONDE2011-2','FY12/ARMFEP/ECOMAKALA/P16',30),('PSL006','2011-2','OPERL2011-2','FY12/ARMFEP/ECOMAKALA/P8',100),('PSL011','2011-2','CICEKI2011-2','FY12/ARMFEP/ECOMAKALA/P5',100),
						('PSL012','2011-2','OPEGL2011-2','FY12/ARMFEP/ECOMAKALA/P6',20),('PSL049','2011-2','PAEDE2011-2','FY12/ARMFEP/ECOMAKALA/P17',30),('PSL019','2011-2','SN2011-2','FY12/ARMFEP/ECOMAKALA/P19',20),('PSL021','2011-2','CENED2011-2','FY12/ARMFEP/ECOMAKALA/P4',80),('PSL022','2011-2','CETEI2011-2','FY12/ARMFEP/ECOMAKALA/P21',20),('PSL024','2011-2','ACLP2011-2','FY12/ARMFEP/ECOMAKALA/P1',15),
						('PSL024','2011-2','FOD2011-2','FY12/ARMFEP/ECOMAKALA/P15',80),('PSL027','2011-2','MUMALUKU2011-2','FY12/ARMFEP/ECOMAKALA/P9',40),('PSL029','2011-2','ACUCOBA2011-2','FY12/ARMFEP/ECOMAKALA/P2',20),('PSL033','2011-2','APRPE2011-2','FY12/ARMFEP/ECOMAKALA/P18',60),('PSL034','2011-2','APROLERU2011-2','FY12/ARMFEP/ECOMAKALA/P12',20),('PSL035','2011-2','COFODI2011-2','FY12/ARMFEP/ECOMAKALA/P14',15),
						('PSL037','2011-2','PDL2011-2','FY12/ARMFEP/ECOMAKALA/P10',60),('PSL038','2011-2','PIED2011-2','FY12/ARMFEP/ECOMAKALA/P11',40),('PSL039','2011-2','GP2011-2','FY12/ARMFEP/ECOMAKALA/P00',200),('PSL040','2011-2','ESF2011-2','FY12/ARMFEP/ECOMAKALA/P13',20),('PSL041','2011-2','SOSNAT2011-2','FY12/ARMFEP/ECOMAKALA/P3',30),('PSL042','2011-2','CODESA2011-2','FY12/ARMFEP/ECOMAKALA/P27',15),
						('PSL043','2011-2','CTFC2011-2','FY12/ARMFEP/ECOMAKALA/P26',15),('PSL044','2011-2','CDR2011-2','FY12/ARMFEP/ECOMAKALA/P25',15),('PSL045','2011-2','OAN2011-2','FY12/ARMFEP/ECOMAKALA/P24',15),('PSL046','2011-2','FACF2011-2','FY12/ARMFEP/ECOMAKALA/P23',15),('PSL047','2011-2','ASAF2011-2','FY12/ARMFEP/ECOMAKALA/P22',15),('PSL048','2011-2','LCDP2011-2','FY12/ARMFEP/ECOMAKALA/P20',30),
						---------------------------------------------------------
						('PSL003','2011-1','ONDE2011-1','FY11/ARMFEP/ECOMAKALA/P14',50),('PSL006','2011-1','OPERL2011-1','FY11/ARMFEP/ECOMAKALA/P16',80),('PSL011','2011-1','CICEKI2011-1','FY11/ARMFEP/ECOMAKALA/P8',60),
						('PSL012','2011-1','OPEGL2011-1','FY11/ARMFEP/ECOMAKALA/P15',30),('PSL049','2011-1','PAEDE2011-1','FY11/ARMFEP/ECOMAKALA/P17',60),('PSL019','2011-1','SN2011-1','FY11/ARMFEP/ECOMAKALA/P22',15),('PSL021','2011-1','CENED2011-1','FY11/ARMFEP/ECOMAKALA/P6',50),('PSL022','2011-1','CETEI2011-1','FY11/ARMFEP/ECOMAKALA/P7',10),('PSL024','2011-1','ACLP2011-1','FY11/ARMFEP/ECOMAKALA/P1',50),
						('PSL024','2011-1','FOD2011-1','FY11/ARMFEP/ECOMAKALA/P10',80),('PSL027','2011-1','MUMALUKU2011-1','FY11/ARMFEP/ECOMAKALA/P13',20),('PSL029','2011-1','ACUCOBA2011-1','FY11/ARMFEP/ECOMAKALA/P3',10),('PSL033','2011-1','APRPE2011-1','FY11/ARMFEP/ECOMAKALA/P23',15),('PSL034','2011-1','APROLERU2011-1','FY11/ARMFEP/ECOMAKALA/P5',15),('PSL035','2011-1','COFODI2011-1','FY11/ARMFEP/ECOMAKALA/P9',30),
						('PSL037','2011-1','PDL2011-1','FY11/ARMFEP/ECOMAKALA/P19',30),('PSL038','2011-1','PIED2011-1','FY11/ARMFEP/ECOMAKALA/P20',30),('PSL023','2011-1','MESADI2011-1','FY11/ARMFEP/ECOMAKALA/P12',30),('PSL036','2011-1','PAFRDC2011-1','FY11/ARMFEP/ECOMAKALA/P18',30),('PSL026','2011-1','RAPNAV2011-1','FY11/ARMFEP/ECOMAKALA/P21',30),('PSL010','2011-1','JEAN2011-1','FY11/ARMFEP/ECOMAKALA/P11',40),
						('PSL015','2011-1','ALCODEBU2011-1','FY11/ARMFEP/ECOMAKALA/P2',10),('PSL028','2011-1','ADESEC2011-1','FY11/ARMFEP/ECOMAKALA/P4',25),('PSL031','2011-1','UCOPAD2011-1','FY11/ARMFEP/ECOMAKALA/P24',15),
						-- ------------------------------------------------------
						('PSL003','2012-1','ONDE2012-1','FY12/ARMFEP/ECOMAKALA/P59',30),('PSL006','2012-1','OPERL2012-1','FY12/ARMFEP/ECOMAKALA/P60',90),('PSL011','2012-1','CICEKI2012-1','FY12/ARMFEP/ECOMAKALA/P55',15),
						('PSL049','2012-1','PAEDE2012-1','FY12/ARMFEP/ECOMAKALA/P61',30),('PSL019','2012-1','SN2012-1','FY12/ARMFEP/ECOMAKALA/P65',15),('PSL021','2012-1','CENED2012-1','FY12/ARMFEP/ECOMAKALA/P53',50),('PSL024','2012-1','FOD2012-1','FY12/ARMFEP/ECOMAKALA/P57',30),('PSL027','2012-1','MUMALUKU2012-1','FY12/ARMFEP/ECOMAKALA/P58',40),('PSL029','2012-1','ACUCOBA2012-1','FY12/ARMFEP/ECOMAKALA/P50',15),
						('PSL033','2012-1','APRPE2012-1','FY12/ARMFEP/ECOMAKALA/P52',50),('PSL034','2012-1','APROLERU2012-1','FY12/ARMFEP/ECOMAKALA/P51',25),('PSL035','2012-1','COFODI2012-1','FY12/ARMFEP/ECOMAKALA/P56',20),
						('PSL037','2012-1','PDL2012-1','FY12/ARMFEP/ECOMAKALA/P62',15),('PSL038','2012-1','PIED2012-1','FY12/ARMFEP/ECOMAKALA/P63',30),('PSL026','2012-1','RAPNAV2012-1','FY12/ARMFEP/ECOMAKALA/P64',15)					


-- ---------------------------------------------------------------------------------------------------------------------------------------------------------
-- CREATION DES UTILISATEURS
-- Table UTILISATEUR

create table tbl_utilisateur
(
	id_utilisateur int identity(1,1),
	id_agentuser varchar(6) not null,
	nomuser varchar(30) not null,
	motpass varchar(1000) not null,
	schema_user varchar(20) not null,
	droits varchar(300) DEFAULT 'Aucun',
	activation bit,
	constraint pk_utilisateur primary key(id_utilisateur),
	constraint fk_utilisateur_id_agent foreign key(id_agentuser) references tbl_agent(id_agent)
)
go

create table tbl_groupe
(
	id_groupe int identity(1,1),
	designation varchar(30) not null,
	niveau int,
	constraint pk_groupe primary key(id_groupe)
)
go

insert into tbl_groupe(designation,niveau) values
('Administrateur',0),
('Admin',1),
('User',2)

----Utilisateur Administrateur de la BD qui aura tous les droits comme sa
----ce script de création doit necéssairement être exécuter car l'utilisateur SA ne peut pas se
----connecter via l'application. C'est ce user qui sera le SA. Cet utilisateur peut être changé 
---- et pour cela changer le login et le user. Si possible aussi le password

--drop login wwfadmin
--drop user wwf_admin

--Add login and user
exec sp_addlogin 'wwfadmin','root//777Wwf'
exec sp_adduser 'wwfadmin','wwfadmin'
---Add role1
exec sp_addsrvrolemember 'wwfadmin','sysadmin' 
exec sp_addsrvrolemember 'wwfadmin','securityadmin' 
exec sp_addsrvrolemember 'wwfadmin','dbcreator' 
--Add roles2
exec sp_addrolemember 'db_owner','wwfadmin'
exec sp_addrolemember 'db_ddladmin','wwfadmin'
exec sp_addrolemember 'db_accessadmin','wwfadmin';

--CREATE LOGIN wwfadmin WITH PASSWORD='root//777Wwf';
--CREATE USER wwf_admin FOR LOGIN wwfadmin;
--GRANT select,insert,update,delete to wwf_admin

--Selections de test DELETE
--select * from tbl_agent
--select * from tbl_utilisateur

--drop schema ad1
--drop login ad1
--drop user ad1

--drop login test2
--drop user test2

--SELECT DB_NAME() AS bd_encours

--SELECT tbl_agent.id_agent AS id,tbl_agent.agent AS nom,tbl_utilisateur.activation AS activation,tbl_utilisateur.nomuser,tbl_utilisateur.droits AS droits,tbl_utilisateur.motpass FROM tbl_agent 
--LEFT OUTER JOIN tbl_utilisateur ON tbl_agent.id_agent=tbl_utilisateur.id_agentuser WHERE tbl_utilisateur.nomuser='{0}' AND tbl_utilisateur.motpass='{1}'

--DELETE
--select * from tbl_utilisateur

--SELECT tbl_utilisateur.id_utilisateur,tbl_utilisateur.id_agentuser,tbl_utilisateur.nomuser,tbl_utilisateur.motpass,tbl_utilisateur.schema_user,tbl_utilisateur.droits,tbl_utilisateur.activation,tbl_agent.id_agent, tbl_agent.agent AS nom FROM tbl_utilisateur 
--INNER JOIN tbl_agent ON tbl_agent.id_agent = tbl_utilisateur.id_agentuser ORDER BY tbl_utilisateur.nomuser ASC

--SELECT tbl_utilisateur.id_utilisateur AS idUser,tbl_utilisateur.id_agentuser,tbl_utilisateur.nomuser,tbl_utilisateur.motpass,tbl_utilisateur.schema_user,tbl_utilisateur.droits,tbl_utilisateur.activation,tbl_agent.id_agent, tbl_agent.agent AS nom FROM tbl_utilisateur 
--INNER JOIN tbl_agent ON tbl_agent.id_agent = tbl_utilisateur.id_agentuser WHERE tbl_utilisateur.id_utilisateur=1

--SELECT tbl_utilisateur.id_utilisateur AS idUser,tbl_utilisateur.id_agentuser,tbl_utilisateur.nomuser,tbl_utilisateur.motpass,tbl_utilisateur.schema_user,tbl_utilisateur.droits,tbl_utilisateur.activation,tbl_agent.id_agent, tbl_agent.agent AS nom FROM tbl_utilisateur 
--INNER JOIN tbl_agent ON tbl_agent.id_agent = tbl_utilisateur.id_agentuser WHERE tbl_utilisateur.nomuser=1

--SELECT nomuser,schema_user  FROM tbl_utilisateur WHERE id_utilisateur=1
