using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_utilisateur : clstbl_agent
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id_utilisateur;
        private string id_agentuser;
        private string nomuser;
        private string motpass;
        private string schema_user;
        private string droits;
        private bool? activation;
        //***DataTables***
        public DataTable clstbl_utilisateurTables()
        {
            return clsMetier.GetInstance().getAllClstbl_utilisateur();
        }
        public DataTable clstbl_utilisateurTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_utilisateur(criteria);
        }
        public new int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_utilisateur(this);
        }
        public new int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_utilisateur(varscls);
        }

        public new int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_utilisateur(varscls);
        }

        //***Le constructeur par defaut***
        public clstbl_utilisateur()
        {
        }

        //***Accesseur de id_utilisateur***
        public int Id_utilisateur
        {
            get { return id_utilisateur; }
            set { id_utilisateur = value; }
        }
        //***Accesseur de id_agentuser***
        public string Id_agentuser
        {
            get { return id_agentuser; }
            set { id_agentuser = value; }
        }
        //***Accesseur de nomuser***
        public string Nomuser
        {
            get { return nomuser; }
            set { nomuser = value; }
        }
        //***Accesseur de motpass***
        public string Motpass
        {
            get { return motpass; }
            set { motpass = value; }
        }
        //***Accesseur de schema_user***
        public string Schema_user
        {
            get { return schema_user; }
            set { schema_user = value; }
        }
        //***Accesseur de droits***
        public string Droits
        {
            get { return droits; }
            set { droits = value; }
        }
        //***Accesseur de activation***
        public bool? Activation
        {
            get { return activation; }
            set { activation = value; }
        }
    } //***fin class
} //***fin namespace
