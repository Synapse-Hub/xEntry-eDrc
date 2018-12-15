using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_arbres_fiche_pr
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private double? hauteur_total;
        private double? hauteur_tronc;
        private int? houppier_1;
        private int? houppier_2;
        private double? diametre;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_arbres_fiche_prTables()
        {
            return clsMetier.GetInstance().getAllClstbl_arbres_fiche_pr();
        }
        public DataTable clstbl_arbres_fiche_prTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_arbres_fiche_pr(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_arbres_fiche_pr(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_arbres_fiche_pr(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_arbres_fiche_pr(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_arbres_fiche_pr()
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
        }  //***Accesseur de hauteur_total***
        public double? Hauteur_total
        {
            get { return hauteur_total; }
            set { hauteur_total = value; }
        }  //***Accesseur de hauteur_tronc***
        public double? Hauteur_tronc
        {
            get { return hauteur_tronc; }
            set { hauteur_tronc = value; }
        }  //***Accesseur de houppier_1***
        public int? Houppier_1
        {
            get { return houppier_1; }
            set { houppier_1 = value; }
        }  //***Accesseur de houppier_2***
        public int? Houppier_2
        {
            get { return houppier_2; }
            set { houppier_2 = value; }
        }  //***Accesseur de diametre***
        public double? Diametre
        {
            get { return diametre; }
            set { diametre = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace