using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_saison_assoc
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private string id_asso;
        private string id_saison;
        private string id_saison_assoc;
        private string numero_contrat_asso;
        private float? surf_contr;
        //***DataTables***
        public DataTable clstbl_saison_assocTables()
        {
            return clsMetier.GetInstance().getAllClstbl_saison_assoc();
        }
        public DataTable clstbl_saison_assocTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_saison_assoc(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_saison_assoc(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_saison_assoc(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_saison_assoc(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_saison_assoc()
        {
        }

        //***Accesseur de id_asso***
        public string Id_asso
        {
            get { return id_asso; }
            set { id_asso = value; }
        }  //***Accesseur de id_saison***
        public string Id_saison
        {
            get { return id_saison; }
            set { id_saison = value; }
        }  //***Accesseur de id_saison_assoc***
        public string Id_saison_assoc
        {
            get { return id_saison_assoc; }
            set { id_saison_assoc = value; }
        }  //***Accesseur de numero_contrat_asso***
        public string Numero_contrat_asso
        {
            get { return numero_contrat_asso; }
            set { numero_contrat_asso = value; }
        }  //***Accesseur de surf_contr***
        public float? Surf_contr
        {
            get { return surf_contr; }
            set { surf_contr = value; }
        }
    } //***fin class
} //***fin namespace