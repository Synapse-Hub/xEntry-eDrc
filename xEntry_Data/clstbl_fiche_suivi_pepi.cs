using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_fiche_suivi_pepi
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
        private string nom_site;
        private string identifiant_pepiniere;
        private string ronde_suivi_pepiniere;
        private string grp_c;
        private string grp_f;
        private double? superficie_potentielle_note;
        private double? superficie_potentielle_2;
        private double? superficie_potentielle_2_5;
        private double? superficie_potentielle_3;
        private string tassement_sachet;
        private string binage;
        private string classement_taille;
        private string classement_espece;
        private string cernage;
        private string etetage;
        private string localisation;
        private Byte[] photo;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_fiche_suivi_pepiTables()
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_suivi_pepi();
        }
        public DataTable clstbl_fiche_suivi_pepiTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_suivi_pepi(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_fiche_suivi_pepi(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_fiche_suivi_pepi(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_fiche_suivi_pepi(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_fiche_suivi_pepi()
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
        }  //***Accesseur de nom_site***
        public string Nom_site
        {
            get { return nom_site; }
            set { nom_site = value; }
        }  //***Accesseur de identifiant_pepiniere***
        public string Identifiant_pepiniere
        {
            get { return identifiant_pepiniere; }
            set { identifiant_pepiniere = value; }
        }  //***Accesseur de ronde_suivi_pepiniere***
        public string Ronde_suivi_pepiniere
        {
            get { return ronde_suivi_pepiniere; }
            set { ronde_suivi_pepiniere = value; }
        }  //***Accesseur de grp_c***
        public string Grp_c
        {
            get { return grp_c; }
            set { grp_c = value; }
        }  //***Accesseur de grp_f***
        public string Grp_f
        {
            get { return grp_f; }
            set { grp_f = value; }
        }  //***Accesseur de superficie_potentielle_note***
        public double? Superficie_potentielle_note
        {
            get { return superficie_potentielle_note; }
            set { superficie_potentielle_note = value; }
        }  //***Accesseur de superficie_potentielle_2***
        public double? Superficie_potentielle_2
        {
            get { return superficie_potentielle_2; }
            set { superficie_potentielle_2 = value; }
        }  //***Accesseur de superficie_potentielle_2_5***
        public double? Superficie_potentielle_2_5
        {
            get { return superficie_potentielle_2_5; }
            set { superficie_potentielle_2_5 = value; }
        }  //***Accesseur de superficie_potentielle_3***
        public double? Superficie_potentielle_3
        {
            get { return superficie_potentielle_3; }
            set { superficie_potentielle_3 = value; }
        }  //***Accesseur de tassement_sachet***
        public string Tassement_sachet
        {
            get { return tassement_sachet; }
            set { tassement_sachet = value; }
        }  //***Accesseur de binage***
        public string Binage
        {
            get { return binage; }
            set { binage = value; }
        }  //***Accesseur de classement_taille***
        public string Classement_taille
        {
            get { return classement_taille; }
            set { classement_taille = value; }
        }  //***Accesseur de classement_espece***
        public string Classement_espece
        {
            get { return classement_espece; }
            set { classement_espece = value; }
        }  //***Accesseur de cernage***
        public string Cernage
        {
            get { return cernage; }
            set { cernage = value; }
        }  //***Accesseur de etetage***
        public string Etetage
        {
            get { return etetage; }
            set { etetage = value; }
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
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace