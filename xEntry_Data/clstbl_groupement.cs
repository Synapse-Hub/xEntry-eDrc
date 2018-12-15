using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_groupement
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int idg;
        private int idt;
        private string groupement;
        //***DataTables***
        public DataTable clstbl_groupementTables()
        {
            return clsMetier.GetInstance().getAllClstbl_groupement();
        }
        public DataTable clstbl_groupementTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_groupement(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_groupement(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_groupement(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_groupement(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_groupement()
        {
        }

        //***Accesseur de idg***
        public int Idg
        {
            get { return idg; }
            set { idg = value; }
        }  //***Accesseur de idt***
        public int Idt
        {
            get { return idt; }
            set { idt = value; }
        }  //***Accesseur de groupement***
        public string Groupement
        {
            get { return groupement; }
            set { groupement = value; }
        }
    } //***fin class
} //***fin namespace