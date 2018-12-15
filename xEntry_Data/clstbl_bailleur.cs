using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_bailleur
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private string id_bailleur;
        private string bailleur;
        //***DataTables***
        public DataTable clstbl_bailleurTables()
        {
            return clsMetier.GetInstance().getAllClstbl_bailleur();
        }
        public DataTable clstbl_bailleurTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_bailleur(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_bailleur(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_bailleur(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_bailleur(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_bailleur()
        {
        }

        //***Accesseur de id_bailleur***
        public string Id_bailleur
        {
            get { return id_bailleur; }
            set { id_bailleur = value; }
        }  //***Accesseur de bailleur***
        public string Bailleur
        {
            get { return bailleur; }
            set { bailleur = value; }
        }
    } //***fin class
} //***fin namespace