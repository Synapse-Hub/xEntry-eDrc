using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_agent
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private string id_agent;
        private string agent;
        //***DataTables***
        public DataTable clstbl_agentTables()
        {
            return clsMetier.GetInstance().getAllClstbl_agent();
        }
        public DataTable clstbl_agentTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_agent(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_agent(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_agent(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_agent(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_agent()
        {
        }

        //***Accesseur de id_agent***
        public string Id_agent
        {
            get { return id_agent; }
            set { id_agent = value; }
        }  //***Accesseur de agent***
        public string Agent
        {
            get { return agent; }
            set { agent = value; }
        }
    } //***fin class
} //***fin namespace