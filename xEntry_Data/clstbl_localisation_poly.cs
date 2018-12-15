using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_localisation_poly
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private string name_point;
        private string localisation_poly;
        //***DataTables***
        public DataTable clstbl_localisation_polyTables()
        {
            return clsMetier.GetInstance().getAllClstbl_localisation_poly();
        }
        public DataTable clstbl_localisation_polyTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_localisation_poly(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_localisation_poly(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_localisation_poly(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_localisation_poly(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_localisation_poly()
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
        }  //***Accesseur de name_point***
        public string Name_point
        {
            get { return name_point; }
            set { name_point = value; }
        }  //***Accesseur de localisation_poly***
        public string Localisation_poly
        {
            get { return localisation_poly; }
            set { localisation_poly = value; }
        }
    } //***fin class
} //***fin namespace