using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_saison
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private string id_saison;
        private string saison;
        //***DataTables***
        public DataTable clstbl_saisonTables()
        {
            return clsMetier.GetInstance().getAllClstbl_saison();
        }
        public DataTable clstbl_saisonTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_saison(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_saison(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_saison(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_saison(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_saison()
        {
        }

        //***Accesseur de id_saison***
        public string Id_saison
        {
            get { return id_saison; }
            set { id_saison = value; }
        }  //***Accesseur de saison***
        public string Saison
        {
            get { return saison; }
            set { saison = value; }
        }
    } //***fin class
} //***fin namespace