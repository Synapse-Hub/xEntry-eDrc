using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_germoir_fiche_suivi_pepi
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private string germoir_essence;
        private string germoir_essence_autre;
        private string provenance;
        private int? qte_semee;
        private DateTime? date_semis;
        private DateTime? date_premiere_levee;
        private string type_de_semis;
        private string bien_plat;
        private string arrosage;
        private string desherbage;
        private string qualite_semis;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_germoir_fiche_suivi_pepiTables()
        {
            return clsMetier.GetInstance().getAllClstbl_germoir_fiche_suivi_pepi();
        }
        public DataTable clstbl_germoir_fiche_suivi_pepiTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_germoir_fiche_suivi_pepi(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_germoir_fiche_suivi_pepi(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_germoir_fiche_suivi_pepi(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_germoir_fiche_suivi_pepi(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_germoir_fiche_suivi_pepi()
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
        }  //***Accesseur de germoir_essence***
        public string Germoir_essence
        {
            get { return germoir_essence; }
            set { germoir_essence = value; }
        }  //***Accesseur de germoir_essence_autre***
        public string Germoir_essence_autre
        {
            get { return germoir_essence_autre; }
            set { germoir_essence_autre = value; }
        }  //***Accesseur de provenance***
        public string Provenance
        {
            get { return provenance; }
            set { provenance = value; }
        }  //***Accesseur de qte_semee***
        public int? Qte_semee
        {
            get { return qte_semee; }
            set { qte_semee = value; }
        }  //***Accesseur de date_semis***
        public DateTime? Date_semis
        {
            get { return date_semis; }
            set { date_semis = value; }
        }  //***Accesseur de date_premiere_levee***
        public DateTime? Date_premiere_levee
        {
            get { return date_premiere_levee; }
            set { date_premiere_levee = value; }
        }  //***Accesseur de type_de_semis***
        public string Type_de_semis
        {
            get { return type_de_semis; }
            set { type_de_semis = value; }
        }  //***Accesseur de bien_plat***
        public string Bien_plat
        {
            get { return bien_plat; }
            set { bien_plat = value; }
        }  //***Accesseur de arrosage***
        public string Arrosage
        {
            get { return arrosage; }
            set { arrosage = value; }
        }  //***Accesseur de desherbage***
        public string Desherbage
        {
            get { return desherbage; }
            set { desherbage = value; }
        }  //***Accesseur de qualite_semis***
        public string Qualite_semis
        {
            get { return qualite_semis; }
            set { qualite_semis = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace