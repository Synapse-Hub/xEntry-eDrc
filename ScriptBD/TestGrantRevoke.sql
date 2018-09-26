--tbl_fiche_ident_pepi,
--tbl_grp_c_fiche_ident_pepi
--tbl_fiche_suivi_pepi
--tbl_germoir_fiche_suivi_pepi
--tbl_plant_repiq_fiche_suivi_pepi
--tbl_territoire
--tbl_groupement
--tbl_localite
--tbl_chefferie
--tbl_village
--tbl_saison
--tbl_agent
--tbl_association
--tbl_bailleur
--tbl_saison_assoc
--tbl_essence_plant
--tbl_utilisateur
--tbl_groupe

--Admin
grant select,insert,update on tbl_fiche_tar to test1 
grant select,insert,update on tbl_geopoint to test1 
grant select,insert,update on tbl_fiche_menage to test1 
grant select,insert,update on tbl_fiche_pr to test1 
grant select,insert,update on tbl_groupement to test1 
grant select,insert,update on tbl_localite to test1 
grant select,insert,update on tbl_chefferie to test1 
grant select,insert,update on village to test1 
grant select,insert,update on tbl_saison to test1 
grant select,insert,update on tbl_agent to test1 
grant select,insert,update on tbl_association to test1 
grant select,insert,update on tbl_bailleur to test1 
grant select,insert,update on tbl_saison_assoc to test1 
grant select,insert,update on tbl_essence_plant to test1 
grant select,insert,update on tbl_utilisateur to test1 
grant select on tbl_groupe to test1

--User
grant select,insert,update on tbl_fiche_tar to test2 
grant select,insert,update on tbl_geopoint to test2 
grant select,insert,update on tbl_fiche_menage to test2 
grant select,insert,update on tbl_fiche_pr to test2 
grant select,insert,update on tbl_groupement to test2 
grant select,insert,update on tbl_localite to test2 
grant select,insert,update on tbl_chefferie to test2 
grant select,insert,update on village to test2 
grant select,insert,update on tbl_saison to test2 
grant select on tbl_agent to test2 
grant select,insert,update on tbl_association to test2 
grant select,insert,update on tbl_bailleur to test2 
grant select,insert,update on tbl_saison_assoc to test2 
grant select,insert,update on tbl_essence_plant to test2
grant select on tbl_utilisateur to o 

--Admin
revoke select,insert,update on tbl_fiche_tar to test1 
revoke select,insert,update on tbl_geopoint to test1 
revoke select,insert,update on tbl_fiche_menage to test1 
revoke select,insert,update on tbl_fiche_pr to test1 
revoke select,insert,update on tbl_groupement to test1 
revoke select,insert,update on tbl_localite to test1 
revoke select,insert,update on tbl_chefferie to test1 
revoke select,insert,update on village to test1 
revoke select,insert,update on tbl_saison to test1 
revoke select,insert,update on tbl_agent to test1 
revoke select,insert,update on tbl_association to test1 
revoke select,insert,update on tbl_bailleur to test1 
revoke select,insert,update on tbl_saison_assoc to test1 
revoke select,insert,update on tbl_essence_plant to test1 
revoke select,insert,update on tbl_utilisateur to test1 
revoke select on tbl_groupe to test1

--User
revoke select,insert,update on tbl_fiche_tar to test2 
revoke select,insert,update on tbl_geopoint to test2 
revoke select,insert,update on tbl_fiche_menage to test2 
revoke select,insert,update on tbl_fiche_pr to test2 
revoke select,insert,update on tbl_groupement to test2 
revoke select,insert,update on tbl_localite to test2 
revoke select,insert,update on tbl_chefferie to test2 
revoke select,insert,update on village to test2 
revoke select,insert,update on tbl_saison to test2 
revoke select on tbl_agent to test2 
revoke select,insert,update on tbl_association to test2 
revoke select,insert,update on tbl_bailleur to test2 
revoke select,insert,update on tbl_saison_assoc to test2 
revoke select,insert,update on tbl_essence_plant to test2 