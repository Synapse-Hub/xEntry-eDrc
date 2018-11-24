--Report PR 
--1.Liste des planteurs (Hectares réalisé) par saison et par bailleur de fonds (OK)
select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + '' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur'
from tbl_fiche_pr 
where tbl_fiche_pr.saison='' and tbl_fiche_pr.bailleur=''

--2.Liste des planteurs (Hectares réalisé) par association et par bailleur de fonds (Ok)
select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + '' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur'
from tbl_fiche_pr 
where tbl_fiche_pr.association='' and tbl_fiche_pr.bailleur='' 

--3.Liste des essences plantées par saison et par association (OK)
select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + '' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur' 
from tbl_fiche_pr 
where tbl_fiche_pr.saison='' and tbl_fiche_pr.association=''

--4.Liste des plantations par nombre des visites et par agent (OK)
select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + '' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',tbl_fiche_pr.n_plantation as 'Nombre plantation',
tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
tbl_fiche_pr.localisation as 'Coordonnées géographiques', (select n_visite as n_visite from tbl_fiche_pr union select n_visite_2 as n_visite from tbl_fiche_pr union select n_viste_3 as n_visite from tbl_fiche_pr) as n_visite, tbl_fiche_pr.nom_agent as 'Agent' 
from tbl_fiche_pr 
where (tbl_fiche_pr.n_visite='' or tbl_fiche_pr.n_visite_2='' or tbl_fiche_pr.n_viste_3='') and tbl_fiche_pr.nom_agent=''


--Report TAR (OK)
--1.Liste des planteurs (Hectares réalisés) par territoire et par saison
select tbl_fiche_tar.uuid as 'Identifiant unique',ISNULL(tbl_fiche_tar.nom,'') + '' + ISNULL(tbl_fiche_tar.postnom,'') + ' ' + ISNULL(tbl_fiche_tar.prenom,'') AS 'Noms planteur',tbl_fiche_tar.nom_lieu_plantation as 'Lieu plantation',tbl_fiche_tar.territoire as 'Territoire',tbl_fiche_tar.groupement as 'Groupement',tbl_fiche_tar.association as 'Association',
tbl_fiche_tar.superficie_totale as 'Hectare à réaliser',tbl_fiche_tar.saison as 'Saison',tbl_fiche_tar.essence_principale as 'Essence principale',tbl_fiche_tar.essence_principale_autre as 'Autre essence',objectifs_planteur as 'Objectifs principal',tbl_fiche_tar.objectifs_planteur_autre as 'Autre objectif',
tbl_fiche_tar.utilisation_precedente as 'Utilisation précédente',tbl_fiche_tar.arbres_existants as 'Nbr arbre existants',tbl_fiche_tar.situation as 'Situation',tbl_fiche_tar.pente as 'Pente',tbl_fiche_tar.document_de_propriete as 'Documents propriétaire' 
from tbl_fiche_tar 
inner join tbl_territoire on tbl_territoire.territoire=tbl_fiche_tar.territoire
inner join tbl_saison on tbl_saison.saison=tbl_fiche_tar.saison
where tbl_fiche_tar.territoire='' and tbl_fiche_tar.saison=''

--Report Suivi Pepinière
--1.Liste des pépinières par agent et par saison (OK)
select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.deviceid as 'ID péripherique',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.saison as 'Saison',tbl_grp_c_fiche_ident_pepi.count as 'Comptage',tbl_fiche_ident_pepi.association as 'Association',tbl_fiche_ident_pepi.bailleur as 'Bailleur',
tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',
tbl_fiche_ident_pepi.nb_pepinieristes_formes as 'Nbr Pépinieristes formés',tbl_fiche_ident_pepi.contrat as 'Contrat',tbl_fiche_ident_pepi.combien_pepinieristes as 'Total pépinieristes',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité totale planche',
tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_fiche_suivi_pepi.superficie_potentielle_2 as 'Superficie potentielle 2',tbl_fiche_suivi_pepi.superficie_potentielle_3 as 'Superficie potentielle 3',tbl_fiche_suivi_pepi.superficie_potentielle_2_5 as 'Superficie potentielle 2-5',
tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Autre essence germoir',tbl_plant_repiq_fiche_suivi_pepi.date_repiquage as 'Date repiqauge',tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_plant_repiq_fiche_suivi_pepi.plantules_encore_repiques 'Planture encore repiqué',tbl_fiche_ident_pepi.observations as 'Observations'
from tbl_fiche_ident_pepi
inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
where tbl_fiche_ident_pepi.agent='' and tbl_fiche_ident_pepi.saison=''

--2.Liste des pépinières par Qte semee et par lieu de provenance
select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.nom_site as 'Nom site',
tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',
tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Essence germoir autre',
tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_fiche_ident_pepi.observations as 'Observations',tbl_germoir_fiche_suivi_pepi.qte_semee as 'Qte Semée',tbl_germoir_fiche_suivi_pepi.provenance as 'Provenance'
from tbl_fiche_ident_pepi
inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
where tbl_germoir_fiche_suivi_pepi.provenance=''

--3.Liste des pépinières Qte par planche_repiquage (OK)
select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',
tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',
tbl_fiche_ident_pepi.localisation as 'Géolocalisation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité totale planche',tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',
tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Autre essence germoir',tbl_plant_repiq_fiche_suivi_pepi.date_repiquage as 'Date repiqauge',tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_plant_repiq_fiche_suivi_pepi.plantules_encore_repiques 'Planture encore repiqué',
tbl_plant_repiq_fiche_suivi_pepi.plantules_deja_evacues as 'Planture déjà evacué',tbl_fiche_ident_pepi.observations as 'Observations',tbl_germoir_fiche_suivi_pepi.qte_semee as 'Qte Semée'
from tbl_fiche_ident_pepi
inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
where tbl_plant_repiq_fiche_suivi_pepi.planches_repiquage_essence='' or tbl_plant_repiq_fiche_suivi_pepi.planches_repiquage_essence_autre=''

--Report Identification pépinière
--1.Liste des pépinières identifiée
select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.saison as 'Saison',tbl_fiche_ident_pepi.association as 'Association',tbl_fiche_ident_pepi.bailleur as 'Bailleur',
tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',
tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité planche',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',observations as 'Observations'
from tbl_fiche_ident_pepi
inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid
