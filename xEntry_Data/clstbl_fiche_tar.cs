using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_fiche_tar
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private string deviceid;
        private DateTime date;
        private string agent;
        private string saison;
        private string association;
        private string association_autre;
        private string bailleur;
        private string bailleur_autre;
        private int? n_plantation;
        private string deja_participe;
        private int? n_plantations;
        private string nom;
        private string postnom;
        private string prenom;
        private string sexes;
        private string nom_lieu_plantation;
        private string village;
        private string localite;
        private string territoire;
        private string chefferie;
        private string groupement;
        private string type_id;
        private string type_id_autre;
        private string nombre_id;
        private Byte[] photo_id;
        private Byte[] photo_planteur;
        private Byte[] photo_terrain;
        private string emplacement;
        private string essence_principale;
        private string essence_principale_autre;
        private double? superficie_totale;
        private string objectifs_planteur;
        private string objectifs_planteur_autre;
        private string utilisation_precedente;
        private string autre_precedente_preciser;
        private DateTime? utilisation_precedente_depuis;
        private string arbres_existants;
        private int? ombre_arbres;
        private string situation;
        private string pente;
        private string sol;
        private string eucalyptus;
        private string point_deau_a_proximite;
        private int? env_point_deau_a_proximite;
        private string chef_de_localite;
        private string chef_nom;
        private string chef_postnom;
        private string chef_prenom;
        private string autre;
        private string autre_fonction;
        private string autre_nom;
        private string autre_postnom;
        private string autre_prenom;
        private string document_de_propriete;
        private string preciser_document;
        private string autre_document;
        private Byte[] photo_document_de_propriet;
        private string observations;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_fiche_tarTables()
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_tar();
        }
        public DataTable clstbl_fiche_tarTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_tar(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_fiche_tar(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_fiche_tar(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_fiche_tar(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_fiche_tar()
        {
        }

        //***Accesseur de id***
        public int Id
        {
            get { return id; }
            set { id = value; }
        }  //***Accesseur de uuid***
        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }  //***Accesseur de deviceid***
        public string Deviceid
        {
            get { return deviceid; }
            set { deviceid = value; }
        }  //***Accesseur de date***
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }  //***Accesseur de agent***
        public string Agent
        {
            get { return agent; }
            set { agent = value; }
        }  //***Accesseur de saison***
        public string Saison
        {
            get { return saison; }
            set { saison = value; }
        }  //***Accesseur de association***
        public string Association
        {
            get { return association; }
            set { association = value; }
        }  //***Accesseur de association_autre***
        public string Association_autre
        {
            get { return association_autre; }
            set { association_autre = value; }
        }  //***Accesseur de bailleur***
        public string Bailleur
        {
            get { return bailleur; }
            set { bailleur = value; }
        }  //***Accesseur de bailleur_autre***
        public string Bailleur_autre
        {
            get { return bailleur_autre; }
            set { bailleur_autre = value; }
        }  //***Accesseur de n_plantation***
        public int? N_plantation
        {
            get { return n_plantation; }
            set { n_plantation = value; }
        }  //***Accesseur de deja_participe***
        public string Deja_participe
        {
            get { return deja_participe; }
            set { deja_participe = value; }
        }  //***Accesseur de n_plantations***
        public int? N_plantations
        {
            get { return n_plantations; }
            set { n_plantations = value; }
        }  //***Accesseur de nom***
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }  //***Accesseur de postnom***
        public string Postnom
        {
            get { return postnom; }
            set { postnom = value; }
        }  //***Accesseur de prenom***
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }  //***Accesseur de sexes***
        public string Sexes
        {
            get { return sexes; }
            set { sexes = value; }
        }  //***Accesseur de nom_lieu_plantation***
        public string Nom_lieu_plantation
        {
            get { return nom_lieu_plantation; }
            set { nom_lieu_plantation = value; }
        }  //***Accesseur de village***
        public string Village
        {
            get { return village; }
            set { village = value; }
        }  //***Accesseur de localite***
        public string Localite
        {
            get { return localite; }
            set { localite = value; }
        }  //***Accesseur de territoire***
        public string Territoire
        {
            get { return territoire; }
            set { territoire = value; }
        }  //***Accesseur de chefferie***
        public string Chefferie
        {
            get { return chefferie; }
            set { chefferie = value; }
        }  //***Accesseur de groupement***
        public string Groupement
        {
            get { return groupement; }
            set { groupement = value; }
        }  //***Accesseur de type_id***
        public string Type_id
        {
            get { return type_id; }
            set { type_id = value; }
        }  //***Accesseur de type_id_autre***
        public string Type_id_autre
        {
            get { return type_id_autre; }
            set { type_id_autre = value; }
        }  //***Accesseur de nombre_id***
        public string Nombre_id
        {
            get { return nombre_id; }
            set { nombre_id = value; }
        }  //***Accesseur de photo_id***
        public Byte[] Photo_id
        {
            get { return photo_id; }
            set { photo_id = value; }
        }  //***Accesseur de photo_planteur***
        public Byte[] Photo_planteur
        {
            get { return photo_planteur; }
            set { photo_planteur = value; }
        }  //***Accesseur de photo_terrain***
        public Byte[] Photo_terrain
        {
            get { return photo_terrain; }
            set { photo_terrain = value; }
        }  //***Accesseur de emplacement***
        public string Emplacement
        {
            get { return emplacement; }
            set { emplacement = value; }
        }  //***Accesseur de essence_principale***
        public string Essence_principale
        {
            get { return essence_principale; }
            set { essence_principale = value; }
        }  //***Accesseur de essence_principale_autre***
        public string Essence_principale_autre
        {
            get { return essence_principale_autre; }
            set { essence_principale_autre = value; }
        }  //***Accesseur de superficie_totale***
        public double? Superficie_totale
        {
            get { return superficie_totale; }
            set { superficie_totale = value; }
        }  //***Accesseur de objectifs_planteur***
        public string Objectifs_planteur
        {
            get { return objectifs_planteur; }
            set { objectifs_planteur = value; }
        }  //***Accesseur de objectifs_planteur_autre***
        public string Objectifs_planteur_autre
        {
            get { return objectifs_planteur_autre; }
            set { objectifs_planteur_autre = value; }
        }  //***Accesseur de utilisation_precedente***
        public string Utilisation_precedente
        {
            get { return utilisation_precedente; }
            set { utilisation_precedente = value; }
        }  //***Accesseur de autre_precedente_preciser***
        public string Autre_precedente_preciser
        {
            get { return autre_precedente_preciser; }
            set { autre_precedente_preciser = value; }
        }  //***Accesseur de utilisation_precedente_depuis***
        public DateTime? Utilisation_precedente_depuis
        {
            get { return utilisation_precedente_depuis; }
            set { utilisation_precedente_depuis = value; }
        }  //***Accesseur de arbres_existants***
        public string Arbres_existants
        {
            get { return arbres_existants; }
            set { arbres_existants = value; }
        }  //***Accesseur de ombre_arbres***
        public int? Ombre_arbres
        {
            get { return ombre_arbres; }
            set { ombre_arbres = value; }
        }  //***Accesseur de situation***
        public string Situation
        {
            get { return situation; }
            set { situation = value; }
        }  //***Accesseur de pente***
        public string Pente
        {
            get { return pente; }
            set { pente = value; }
        }  //***Accesseur de sol***
        public string Sol
        {
            get { return sol; }
            set { sol = value; }
        }  //***Accesseur de eucalyptus***
        public string Eucalyptus
        {
            get { return eucalyptus; }
            set { eucalyptus = value; }
        }  //***Accesseur de point_deau_a_proximite***
        public string Point_deau_a_proximite
        {
            get { return point_deau_a_proximite; }
            set { point_deau_a_proximite = value; }
        }  //***Accesseur de env_point_deau_a_proximite***
        public int? Env_point_deau_a_proximite
        {
            get { return env_point_deau_a_proximite; }
            set { env_point_deau_a_proximite = value; }
        }  //***Accesseur de chef_de_localite***
        public string Chef_de_localite
        {
            get { return chef_de_localite; }
            set { chef_de_localite = value; }
        }  //***Accesseur de chef_nom***
        public string Chef_nom
        {
            get { return chef_nom; }
            set { chef_nom = value; }
        }  //***Accesseur de chef_postnom***
        public string Chef_postnom
        {
            get { return chef_postnom; }
            set { chef_postnom = value; }
        }  //***Accesseur de chef_prenom***
        public string Chef_prenom
        {
            get { return chef_prenom; }
            set { chef_prenom = value; }
        }  //***Accesseur de autre***
        public string Autre
        {
            get { return autre; }
            set { autre = value; }
        }  //***Accesseur de autre_fonction***
        public string Autre_fonction
        {
            get { return autre_fonction; }
            set { autre_fonction = value; }
        }  //***Accesseur de autre_nom***
        public string Autre_nom
        {
            get { return autre_nom; }
            set { autre_nom = value; }
        }  //***Accesseur de autre_postnom***
        public string Autre_postnom
        {
            get { return autre_postnom; }
            set { autre_postnom = value; }
        }  //***Accesseur de autre_prenom***
        public string Autre_prenom
        {
            get { return autre_prenom; }
            set { autre_prenom = value; }
        }  //***Accesseur de document_de_propriete***
        public string Document_de_propriete
        {
            get { return document_de_propriete; }
            set { document_de_propriete = value; }
        }  //***Accesseur de preciser_document***
        public string Preciser_document
        {
            get { return preciser_document; }
            set { preciser_document = value; }
        }  //***Accesseur de autre_document***
        public string Autre_document
        {
            get { return autre_document; }
            set { autre_document = value; }
        }  //***Accesseur de photo_document_de_propriet***
        public Byte[] Photo_document_de_propriet
        {
            get { return photo_document_de_propriet; }
            set { photo_document_de_propriet = value; }
        }  //***Accesseur de observations***
        public string Observations
        {
            get { return observations; }
            set { observations = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace