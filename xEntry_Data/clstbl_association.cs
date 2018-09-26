using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_association
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private string id_asso;
        private string association;
        //***DataTables***
        public DataTable clstbl_associationTables()
        {
            return clsMetier.GetInstance().getAllClstbl_association();
        }
        public DataTable clstbl_associationTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_association(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_association(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_association(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_association(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_association()
        {
        }

        //***Accesseur de id_asso***
        public string Id_asso
        {
            get { return id_asso; }
            set { id_asso = value; }
        }  //***Accesseur de association***
        public string Association
        {
            get { return association; }
            set { association = value; }
        }
    } //***fin class
} //***fin namespace