using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_territoire
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int idt;
        private string territoire;
        //***DataTables***
        public DataTable clstbl_territoireTables()
        {
            return clsMetier.GetInstance().getAllClstbl_territoire();
        }
        public DataTable clstbl_territoireTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_territoire(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_territoire(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_territoire(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_territoire(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_territoire()
        {
        }

        //***Accesseur de idt***
        public int Idt
        {
            get { return idt; }
            set { idt = value; }
        }  //***Accesseur de territoire***
        public string Territoire
        {
            get { return territoire; }
            set { territoire = value; }
        }
    } //***fin class
} //***fin namespace