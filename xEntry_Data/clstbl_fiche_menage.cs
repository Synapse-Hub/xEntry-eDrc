using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_fiche_menage
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private string deviceid;
        private DateTime date;
        private string questionnaire_id;
        private string name;
        private string id_menage;
        private string nom_menage;
        private string deuxio_representant;
        private int taille_menage;
        private string village_menage;
        private string province;
        private string groupement;
        private string territoire;
        private string zs;
        private string camps;
        private string localisation;
        private string rpt_gps;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_fiche_menageTables()
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_menage();
        }
        public DataTable clstbl_fiche_menageTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_menage(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_fiche_menage(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_fiche_menage(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_fiche_menage(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_fiche_menage()
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
        }  //***Accesseur de questionnaire_id***
        public string Questionnaire_id
        {
            get { return questionnaire_id; }
            set { questionnaire_id = value; }
        }  //***Accesseur de name***
        public string Name
        {
            get { return name; }
            set { name = value; }
        }  //***Accesseur de id_menage***
        public string Id_menage
        {
            get { return id_menage; }
            set { id_menage = value; }
        }  //***Accesseur de nom_menage***
        public string Nom_menage
        {
            get { return nom_menage; }
            set { nom_menage = value; }
        }  //***Accesseur de deuxio_representant***
        public string Deuxio_representant
        {
            get { return deuxio_representant; }
            set { deuxio_representant = value; }
        }  //***Accesseur de taille_menage***
        public int Taille_menage
        {
            get { return taille_menage; }
            set { taille_menage = value; }
        }  //***Accesseur de village_menage***
        public string Village_menage
        {
            get { return village_menage; }
            set { village_menage = value; }
        }  //***Accesseur de province***
        public string Province
        {
            get { return province; }
            set { province = value; }
        }  //***Accesseur de groupement***
        public string Groupement
        {
            get { return groupement; }
            set { groupement = value; }
        }  //***Accesseur de territoire***
        public string Territoire
        {
            get { return territoire; }
            set { territoire = value; }
        }  //***Accesseur de zs***
        public string Zs
        {
            get { return zs; }
            set { zs = value; }
        }  //***Accesseur de camps***
        public string Camps
        {
            get { return camps; }
            set { camps = value; }
        }  //***Accesseur de localisation***
        public string Localisation
        {
            get { return localisation; }
            set { localisation = value; }
        }  //***Accesseur de rpt_gps***
        public string Rpt_gps
        {
            get { return rpt_gps; }
            set { rpt_gps = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace