using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_groupe
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id_groupe;
        private string designation;
        private int? niveau;
        //***DataTables***
        public DataTable clstbl_groupeTables()
        {
            return clsMetier.GetInstance().getAllClstbl_groupe();
        }
        public DataTable clstbl_groupeTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_groupe(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_groupe(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_groupe(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_groupe(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_groupe()
        {
        }

        //***Accesseur de id_groupe***
        public int Id_groupe
        {
            get { return id_groupe; }
            set { id_groupe = value; }
        }
        //***Accesseur de designation***
        public string Designation
        {
            get { return designation; }
            set { designation = value; }
        }
        //***Accesseur de niveau***
        public int? Niveau
        {
            get { return niveau; }
            set { niveau = value; }
        }
    } //***fin class
} //***fin namespace
