using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_autre_essence_mel_fiche_pr
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private string autre_essence;
        private string autre_essence_autre;
        private int? autre_essence_pourcentage;
        private int? autre_essence_count;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_autre_essence_mel_fiche_prTables()
        {
            return clsMetier.GetInstance().getAllClstbl_autre_essence_mel_fiche_pr();
        }
        public DataTable clstbl_autre_essence_mel_fiche_prTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_autre_essence_mel_fiche_pr(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_autre_essence_mel_fiche_pr(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_autre_essence_mel_fiche_pr(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_autre_essence_mel_fiche_pr(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_autre_essence_mel_fiche_pr()
        {
        }

        //***Accesseur de id***
        public int Id
        {
            get { return id; }
            set { id = value; }
        }  //***Accesseur de uuid***
        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }  //***Accesseur de autre_essence***
        public string Autre_essence
        {
            get { return autre_essence; }
            set { autre_essence = value; }
        }  //***Accesseur de autre_essence_autre***
        public string Autre_essence_autre
        {
            get { return autre_essence_autre; }
            set { autre_essence_autre = value; }
        }  //***Accesseur de autre_essence_pourcentage***
        public int? Autre_essence_pourcentage
        {
            get { return autre_essence_pourcentage; }
            set { autre_essence_pourcentage = value; }
        }  //***Accesseur de autre_essence_count***
        public int? Autre_essence_count
        {
            get { return autre_essence_count; }
            set { autre_essence_count = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace