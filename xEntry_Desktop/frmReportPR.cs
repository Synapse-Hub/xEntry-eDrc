using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using xEntry_Data;

namespace xEntry_Desktop
{
    public partial class frmReportPR : Form
    {
        IDbConnection conn = null;
        public frmReportPR()
        {
            InitializeComponent();
        }

        private string SetQueryExecute(ComboBox cboItems)
        {
            string query = null;

            switch (cboItems.SelectedIndex)
            {
                case 0:
                    if (rdLstPlanteur.Checked)
                    {
                        //par saison et par bailleur de fonds
                        query = string.Format(@"select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + '' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
                        tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
                        tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
                        tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur'
                        from tbl_fiche_pr 
                        where tbl_fiche_pr.saison='{0}' and tbl_fiche_pr.bailleur='{1}'", cboSaison.SelectedValue, cboBailleur.SelectedValue);
                    }
                    else if (rdLstEssence.Checked)
                    {
                        //par saison et par association
                        query = string.Format(@"select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + '' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
                        tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
                        tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
                        tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur' 
                        from tbl_fiche_pr 
                        where tbl_fiche_pr.saison='{0}' and tbl_fiche_pr.association='{1}'", cboSaison.SelectedValue, cboAssociation.SelectedValue);
                    }
                    else if (rdLstPlantation.Checked)
                    {
                        //par nombre visites et par agent
                        query = string.Format(@"select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + '' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',tbl_fiche_pr.n_plantation as 'Nombre plantation',
                        tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
                        tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
                        tbl_fiche_pr.localisation as 'Coordonnées géographiques', (select n_visite as n_visite from tbl_fiche_pr union select n_visite_2 as n_visite from tbl_fiche_pr union select n_viste_3 as n_visite from tbl_fiche_pr) as n_visite, tbl_fiche_pr.nom_agent as 'Agent' 
                        from tbl_fiche_pr 
                        where (tbl_fiche_pr.n_visite='{0}' or tbl_fiche_pr.n_visite_2='{1}' or tbl_fiche_pr.n_viste_3='{2}') and tbl_fiche_pr.nom_agent='{3}'", cboNbrVisite.SelectedValue, cboNbrVisite.SelectedValue, cboNbrVisite.SelectedValue, cboAgent.SelectedValue);
                    }

                    break;
                case 1:
                    if (rdLstPlanteur.Checked)
                    {
                        //par association et par bailleur de fonds
                        query = string.Format(@"select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + '' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
                        tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
                        tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
                        tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur'
                        from tbl_fiche_pr 
                        where tbl_fiche_pr.association='{0}' and tbl_fiche_pr.bailleur='{1}'", cboAssociation.SelectedValue, cboBailleur.SelectedValue);
                    }

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
                SqlDataAdapter adapter = new SqlDataAdapter((SqlCommand)cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "lstTable");

                switch (cboIndex)
                {
                    case 0:
                        if (rdLstPlanteur.Checked)
                        {
                            //par saison et par bailleur de fonds
                            Rapports.LstPlanteurPRSaison_Bailleur rpt = new Rapports.LstPlanteurPRSaison_Bailleur();
                            rpt.SetDataSource(dataset.Tables["lstTable"]);
                            crvReport.ReportSource = rpt;
                            crvReport.Refresh();
                            dataset.Dispose();
                        }
                        else if (rdLstEssence.Checked)
                        {
                            //par saison et par association
                            Rapports.LstEssencePlante_Saison_Assoc rpt = new Rapports.LstEssencePlante_Saison_Assoc();
                            rpt.SetDataSource(dataset.Tables["lstTable"]);
                            crvReport.ReportSource = rpt;
                            crvReport.Refresh();
                            dataset.Dispose();
                        }
                        else if (rdLstPlantation.Checked)
                        {
                            //par nombre visites et par agent
                            Rapports.LstPlantationPR_NbrVisite_Agent rpt = new Rapports.LstPlantationPR_NbrVisite_Agent();
                            rpt.SetDataSource(dataset.Tables["lstTable"]);
                            crvReport.ReportSource = rpt;
                            crvReport.Refresh();
                            dataset.Dispose(); ;
                        }

                        break;
                    case 1:
                        if (rdLstPlanteur.Checked)
                        {
                            //par association et par bailleur de fonds
                            Rapports.LstPlanteurPRAssocBailleur rpt = new Rapports.LstPlanteurPRAssocBailleur();
                            rpt.SetDataSource(dataset.Tables["lstTable"]);
                            crvReport.ReportSource = rpt;
                            crvReport.Refresh();
                            dataset.Dispose();
                        }

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

        private void setMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }

        private void frmReportTAR_Load(object sender, EventArgs e)
        {
            try
            {
                cboSaison.DataSource = clsMetier.GetInstance().getAllClstbl_saison();
                this.setMembersallcbo(cboSaison, "Saison", "Saison");
                cboBailleur.DataSource = clsMetier.GetInstance().getAllClstbl_bailleur();
                this.setMembersallcbo(cboBailleur, "Bailleur", "Bailleur");
                cboAssociation.DataSource = clsMetier.GetInstance().getAllClstbl_association();
                this.setMembersallcbo(cboAssociation, "Association", "Association");
                cboNbrVisite.DataSource = clsMetier.GetInstance().getAllClstbl_n_visite_fiche_pr();
                this.setMembersallcbo(cboNbrVisite, "n_visite", "n_visite");
                cboAgent.DataSource = clsMetier.GetInstance().getAllClstbl_agent();
                this.setMembersallcbo(cboAgent, "Agent", "Agent");

                if (cboSaison.Items.Count > 0)
                    cboSaison.SelectedIndex = 0;

                if (cboBailleur.Items.Count > 0)
                    cboBailleur.SelectedIndex = 0;

                if (cboAssociation.Items.Count > 0)
                    cboAssociation.SelectedIndex = 0;

                if (cboNbrVisite.Items.Count > 0)
                    cboNbrVisite.SelectedIndex = 0;

                if (cboAgent.Items.Count > 0)
                    cboAgent.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de chargement des listes déroulantes, " + ex.Message, "Chargement listes déroulantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rdLstPlanteur_CheckedChanged(object sender, EventArgs e)
        {
            cboItems.Items.Clear();
            cboItems.Items.Add("Par saison et par bailleur de fonds");
            cboItems.Items.Add("Par association et par bailleur de fonds");
            cboItems.Sorted = false;
            cboItems.SelectedIndex = 0;

            cboSaison.Enabled = true;
            cboBailleur.Enabled = true;
            cboAssociation.Enabled = true;

            cboNbrVisite.Enabled = false;
            cboAgent.Enabled = false;
        }

        private void rdLstEssence_CheckedChanged(object sender, EventArgs e)
        {
            cboItems.Items.Clear();
            cboItems.Items.Add("Par saison et par association");
            cboItems.Sorted = false;
            cboItems.SelectedIndex = 0;

            cboSaison.Enabled = true;
            cboBailleur.Enabled = false;
            cboAssociation.Enabled = true;

            cboNbrVisite.Enabled = false;
            cboAgent.Enabled = false;
        }

        private void rdLstPlantation_CheckedChanged(object sender, EventArgs e)
        {
            cboItems.Items.Clear();
            cboItems.Items.Add("Par nombre des visites et par agent");
            cboItems.Sorted = false;
            cboItems.SelectedIndex = 0;

            cboSaison.Enabled = false;
            cboBailleur.Enabled = false;
            cboAssociation.Enabled = false;

            cboNbrVisite.Enabled = true;
            cboAgent.Enabled = true;
        }
    }
}
