using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_chefferie
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int idc;
        private int idl;
        private string chefferie;
        //***DataTables***
        public DataTable clstbl_chefferieTables()
        {
            return clsMetier.GetInstance().getAllClstbl_chefferie();
        }
        public DataTable clstbl_chefferieTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_chefferie(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_chefferie(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_chefferie(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_chefferie(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_chefferie()
        {
        }

        //***Accesseur de idc***
        public int Idc
        {
            get { return idc; }
            set { idc = value; }
        }  //***Accesseur de idl***
        public int Idl
        {
            get { return idl; }
            set { idl = value; }
        }  //***Accesseur de chefferie***
        public string Chefferie
        {
            get { return chefferie; }
            set { chefferie = value; }
        }
    } //***fin class
} //***fin namespace