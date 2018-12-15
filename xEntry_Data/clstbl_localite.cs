using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_localite
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int idl;
        private int idg;
        private string localite;
        //***DataTables***
        public DataTable clstbl_localiteTables()
        {
            return clsMetier.GetInstance().getAllClstbl_localite();
        }
        public DataTable clstbl_localiteTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_localite(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_localite(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_localite(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_localite(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_localite()
        {
        }

        //***Accesseur de idl***
        public int Idl
        {
            get { return idl; }
            set { idl = value; }
        }  //***Accesseur de idg***
        public int Idg
        {
            get { return idg; }
            set { idg = value; }
        }  //***Accesseur de localite***
        public string Localite
        {
            get { return localite; }
            set { localite = value; }
        }
    } //***fin class
} //***fin namespace