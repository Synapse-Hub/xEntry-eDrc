using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_plant_repiq_fiche_suivi_pepi
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private string planches_repiquage_essence;
        private string planches_repiquage_essence_autre;
        private int? plantules_encore_repiques;
        private int? plantules_deja_evacues;
        private int? qte_observee;
        private DateTime? date_repiquage;
        private int? taille_moyenne;
        private int? nbre_feuille_moyenne;
        private double? planches_repiquage_count;
        private string observations;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_plant_repiq_fiche_suivi_pepiTables()
        {
            return clsMetier.GetInstance().getAllClstbl_plant_repiq_fiche_suivi_pepi();
        }
        public DataTable clstbl_plant_repiq_fiche_suivi_pepiTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_plant_repiq_fiche_suivi_pepi(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_plant_repiq_fiche_suivi_pepi(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_plant_repiq_fiche_suivi_pepi(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_plant_repiq_fiche_suivi_pepi(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_plant_repiq_fiche_suivi_pepi()
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
        }  //***Accesseur de planches_repiquage_essence***
        public string Planches_repiquage_essence
        {
            get { return planches_repiquage_essence; }
            set { planches_repiquage_essence = value; }
        }  //***Accesseur de planches_repiquage_essence_autre***
        public string Planches_repiquage_essence_autre
        {
            get { return planches_repiquage_essence_autre; }
            set { planches_repiquage_essence_autre = value; }
        }  //***Accesseur de plantules_encore_repiques***
        public int? Plantules_encore_repiques
        {
            get { return plantules_encore_repiques; }
            set { plantules_encore_repiques = value; }
        }  //***Accesseur de plantules_deja_evacues***
        public int? Plantules_deja_evacues
        {
            get { return plantules_deja_evacues; }
            set { plantules_deja_evacues = value; }
        }  //***Accesseur de qte_observee***
        public int? Qte_observee
        {
            get { return qte_observee; }
            set { qte_observee = value; }
        }  //***Accesseur de date_repiquage***
        public DateTime? Date_repiquage
        {
            get { return date_repiquage; }
            set { date_repiquage = value; }
        }  //***Accesseur de taille_moyenne***
        public int? Taille_moyenne
        {
            get { return taille_moyenne; }
            set { taille_moyenne = value; }
        }  //***Accesseur de nbre_feuille_moyenne***
        public int? Nbre_feuille_moyenne
        {
            get { return nbre_feuille_moyenne; }
            set { nbre_feuille_moyenne = value; }
        }  //***Accesseur de planches_repiquage_count***
        public double? Planches_repiquage_count
        {
            get { return planches_repiquage_count; }
            set { planches_repiquage_count = value; }
        }  //***Accesseur de observations***
        public string Observations
        {
            get { return observations; }
            set { observations = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace