using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_fiche_ident_pepi
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int pid;
        private string uuid;
        private string deviceid;
        private DateTime date;
        private string agent;
        private string saison;
        private string association;
        private string association_autre;
        private string bailleur;
        private string bailleur_autre;
        private string id;
        private string nom_site;
        private string village;
        private string localite;
        private string territoire;
        private string chefferie;
        private string groupement;
        private DateTime? date_installation_pepiniere;
        private string grp_c;
        private int? nb_pepinieristes;
        private int? nb_pepinieristes_formes;
        private string contrat;
        private int? combien_pepinieristes;
        private string localisation;
        private Byte[] photo;
        private string observations;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_fiche_ident_pepiTables()
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_ident_pepi();
        }
        public DataTable clstbl_fiche_ident_pepiTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_ident_pepi(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_fiche_ident_pepi(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_fiche_ident_pepi(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_fiche_ident_pepi(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_fiche_ident_pepi()
        {
        }

        //***Accesseur de pid***
        public int Pid
        {
            get { return pid; }
            set { pid = value; }
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
        }  //***Accesseur de id***
        public string Id
        {
            get { return id; }
            set { id = value; }
        }  //***Accesseur de nom_site***
        public string Nom_site
        {
            get { return nom_site; }
            set { nom_site = value; }
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
        }  //***Accesseur de date_installation_pepiniere***
        public DateTime? Date_installation_pepiniere
        {
            get { return date_installation_pepiniere; }
            set { date_installation_pepiniere = value; }
        }  //***Accesseur de grp_c***
        public string Grp_c
        {
            get { return grp_c; }
            set { grp_c = value; }
        }  //***Accesseur de nb_pepinieristes***
        public int? Nb_pepinieristes
        {
            get { return nb_pepinieristes; }
            set { nb_pepinieristes = value; }
        }  //***Accesseur de nb_pepinieristes_formes***
        public int? Nb_pepinieristes_formes
        {
            get { return nb_pepinieristes_formes; }
            set { nb_pepinieristes_formes = value; }
        }  //***Accesseur de contrat***
        public string Contrat
        {
            get { return contrat; }
            set { contrat = value; }
        }  //***Accesseur de combien_pepinieristes***
        public int? Combien_pepinieristes
        {
            get { return combien_pepinieristes; }
            set { combien_pepinieristes = value; }
        }  //***Accesseur de localisation***
        public string Localisation
        {
            get { return localisation; }
            set { localisation = value; }
        }  //***Accesseur de photo***
        public Byte[] Photo
        {
            get { return photo; }
            set { photo = value; }
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