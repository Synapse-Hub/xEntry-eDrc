using System;
using System.Collections.Generic;
using xEntry_Data;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xEntry_Desktop
{
    public partial class frmReportTAR : Form
    {
        IDbConnection conn = null;
        public frmReportTAR()
        {
            InitializeComponent();
        }

        private string SetQueryExecute(ComboBox cboItems)
        {
            string query = null;
            
            switch(cboItems.SelectedIndex)
            {
                case 0:
                    query = "";
                    break;
                case 1:
                    query = string.Format(@"select tbl_fiche_tar.uuid as 'Identifiant unique',ISNULL(tbl_fiche_tar.nom,'') + '' + ISNULL(tbl_fiche_tar.postnom,'') + ' ' + ISNULL(tbl_fiche_tar.prenom,'') AS 'Noms planteur',tbl_fiche_tar.nom_lieu_plantation as 'Lieu plantation',tbl_fiche_tar.territoire as 'Territoire',tbl_fiche_tar.groupement as 'Groupement',tbl_fiche_tar.association as 'Association',
                    tbl_fiche_tar.superficie_totale as 'Hectare à réaliser',tbl_fiche_tar.saison as 'Saison',tbl_fiche_tar.essence_principale as 'Essence principale',tbl_fiche_tar.essence_principale_autre as 'Autre essence',objectifs_planteur as 'Objectifs principal',tbl_fiche_tar.objectifs_planteur_autre as 'Autre objectif',
                    tbl_fiche_tar.utilisation_precedente as 'Utilisation précédente',tbl_fiche_tar.arbres_existants as 'Nbr arbre existants',tbl_fiche_tar.situation as 'Situation',tbl_fiche_tar.pente as 'Pente',tbl_fiche_tar.document_de_propriete as 'Documents propriétaire' 
                    from tbl_fiche_tar 
                    inner join tbl_territoire on tbl_territoire.territoire=tbl_fiche_tar.territoire
                    inner join tbl_saison on tbl_saison.saison=tbl_fiche_tar.saison 
                    where tbl_fiche_tar.territoire='{0}' and tbl_fiche_tar.saison='{1}'", cboTerritoire.SelectedValue, cboSaison.SelectedValue);
                    break;
            }
            return query;
        }

        private void LoadReport(string query, int cboIndex)
        {
            //Initialisation de la chaine de connexion
            conn = new SqlConnection(xEntry_Data.Properties.Settings.Default.strChaineConnexion);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                IDbDataAdapter adapter = new SqlDataAdapter((SqlCommand)cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                switch (cboIndex)
                {
                    case 0:
                        //Do
                        break;
                    case 1:
                        Rapports.LstPlanteurTAR_Saison_Territoire rpt = new Rapports.LstPlanteurTAR_Saison_Territoire();
                        rpt.SetDataSource(dataset.Tables["lstTable"]);
                        crvReport.ReportSource = rpt;
                        crvReport.Refresh();
                        dataset.Dispose();
                        break;
                }
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            try
            {
                LoadReport(SetQueryExecute(cboItems), cboItems.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de chargement du rapport, " + ex.Message, "Chargement rapport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
            }
        }

        private void frmReportTAR_Load(object sender, EventArgs e)
        {
            cboItems.Items.Add("Liste des planteurs");
            cboItems.Items.Add("Liste des planteurs (Hectares réalisés) par territoire et par saison");
            cboItems.Sorted = false;

            cboItems.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboItems.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboItems.SelectedIndex = 0;

            try
            {
                cboSaison.DataSource = clsMetier.GetInstance().getAllClstbl_saison();
                //cboSaison.ValueMember = "Id_saison";
                cboSaison.ValueMember = "Saison";
                cboSaison.DisplayMember = "Saison";

                cboTerritoire.DataSource = clsMetier.GetInstance().getAllClstbl_territoire();
                //cboTerritoire.ValueMember = "Idt";
                cboTerritoire.ValueMember = "Territoire";
                cboTerritoire.DisplayMember = "Territoire";

                if (cboSaison.Items.Count > 0)
                    cboSaison.SelectedIndex = 0;

                if (cboTerritoire.Items.Count > 0)
                    cboTerritoire.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Echec de chargement des listes déroulantes, " + ex.Message, "Chargement listes déroulantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
