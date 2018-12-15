using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_village
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int idv;
        private int idc;
        private string village;
        //***DataTables***
        public DataTable clstbl_villageTables()
        {
            return clsMetier.GetInstance().getAllClstbl_village();
        }
        public DataTable clstbl_villageTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_village(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_village(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_village(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_village(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_village()
        {
        }

        //***Accesseur de idv***
        public int Idv
        {
            get { return idv; }
            set { idv = value; }
        }  //***Accesseur de idc***
        public int Idc
        {
            get { return idc; }
            set { idc = value; }
        }  //***Accesseur de village***
        public string Village
        {
            get { return village; }
            set { village = value; }
        }
    } //***fin class
} //***fin namespace