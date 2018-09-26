using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_grp_c_fiche_ident_pepi
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private double? count;
        private double? dimension_planche_a;
        private double? dimension_planche_b;
        private int? capacite_planche;
        private int? capacite_totale_planche;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_grp_c_fiche_ident_pepiTables()
        {
            return clsMetier.GetInstance().getAllClstbl_grp_c_fiche_ident_pepi();
        }
        public DataTable clstbl_grp_c_fiche_ident_pepiTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_grp_c_fiche_ident_pepi(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_grp_c_fiche_ident_pepi(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_grp_c_fiche_ident_pepi(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_grp_c_fiche_ident_pepi(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_grp_c_fiche_ident_pepi()
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
        }  //***Accesseur de count***
        public double? Count
        {
            get { return count; }
            set { count = value; }
        }  //***Accesseur de dimension_planche_a***
        public double? Dimension_planche_a
        {
            get { return dimension_planche_a; }
            set { dimension_planche_a = value; }
        }  //***Accesseur de dimension_planche_b***
        public double? Dimension_planche_b
        {
            get { return dimension_planche_b; }
            set { dimension_planche_b = value; }
        }  //***Accesseur de capacite_planche***
        public int? Capacite_planche
        {
            get { return capacite_planche; }
            set { capacite_planche = value; }
        }  //***Accesseur de capacite_totale_planche***
        public int? Capacite_totale_planche
        {
            get { return capacite_totale_planche; }
            set { capacite_totale_planche = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace