using ManageUtilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Xentry.Data;

namespace Xentry.Desktop
{
    public partial class FormReportPr : Form
    {
        IDbConnection conn = null;
        ResourceManager stringManager = null;

        public FormReportPr()
        {
            InitializeComponent();
            //Initialisation des Resources
            Assembly _assembly = Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);
        }

        private void LoadReport(int cboIndex)
        {
            //Initialisation de la chaine de connexion
            conn = new SqlConnection(Data.Properties.Settings.Default.strChaineConnexion);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                SqlDataAdapter adapter = null;
                DataSet dataset = null;
                Rapports.LstPlanteurPrSaisonBailleur rpt1 = null;
                Rapports.LstEssencePlanteSaisonAssoc rpt2 = null;
                Rapports.LstPlantationPrNbrVisiteAgent rpt3 = null;
                Rapports.LstPlanteurPrAssocBailleur rpt4 = null;

                try
                {
                    switch (cboIndex)
                    {
                        case 0:
                            if (rdLstPlanteur.Checked)
                            {
                                rpt1 = new Rapports.LstPlanteurPrSaisonBailleur();
                                //par saison et par bailleur de fonds
                                cmd.CommandText = @"select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + ' ' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
                                tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
                                tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
                                tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur'
                                from tbl_fiche_pr 
                                where tbl_fiche_pr.saison=@saison and tbl_fiche_pr.bailleur=@bailleur";

                                SqlCommand sqlCmd = cmd as SqlCommand;
                                adapter = new SqlDataAdapter(sqlCmd);

                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@saison", DbType.String, 255, cboSaison.SelectedValue)); 
                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@bailleur", DbType.String, 255, cboBailleur.SelectedValue));

                                dataset = new DataSet();
                                dataset.Locale = CultureInfo.InvariantCulture;
                                adapter.Fill(dataset, "lstTable");

                                rpt1.SetDataSource(dataset.Tables["lstTable"]);
                                crvReport.ReportSource = rpt1;
                                crvReport.Refresh();
                            }
                            else if (rdLstEssence.Checked)
                            {
                                rpt2 = new Rapports.LstEssencePlanteSaisonAssoc();
                                //par saison et par association
                                cmd.CommandText = @"select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + ' ' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
                                tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
                                tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
                                tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur' 
                                from tbl_fiche_pr 
                                where tbl_fiche_pr.saison=@saison and tbl_fiche_pr.association=@association";

                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@saison", DbType.String, 255, cboSaison.SelectedValue));
                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@association", DbType.String, 255, cboAssociation.SelectedValue));

                                adapter = new SqlDataAdapter((SqlCommand)cmd);
                                dataset = new DataSet();
                                dataset.Locale = CultureInfo.InvariantCulture;
                                adapter.Fill(dataset, "lstTable");

                                rpt2.SetDataSource(dataset.Tables["lstTable"]);
                                crvReport.ReportSource = rpt2;
                                crvReport.Refresh();
                            }
                            else if (rdLstPlantation.Checked)
                            {
                                rpt3 = new Rapports.LstPlantationPrNbrVisiteAgent();
                                //par nombre visites et par agent
                                cmd.CommandText = @"select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + ' ' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',tbl_fiche_pr.n_plantation as 'Nombre plantation',
                                tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
                                tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
                                tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.n_visite as 'Visites1',tbl_fiche_pr.n_visite_2 as 'Visites2',tbl_fiche_pr.n_viste_3 as 'Visites3', tbl_fiche_pr.nom_agent as 'Agent',
                                SUM(CONVERT(int,tbl_fiche_pr.n_visite) + CONVERT(int,tbl_fiche_pr.n_visite_2) + CONVERT(int,tbl_fiche_pr.n_viste_3)) as somme_n_visite, tbl_fiche_pr.nom_agent as 'Nom agent' 
                                from tbl_fiche_pr where (tbl_fiche_pr.n_visite=@n_visite or tbl_fiche_pr.n_visite_2=@n_visite_2 or tbl_fiche_pr.n_viste_3=@n_viste_3) and tbl_fiche_pr.nom_agent=@nom_agent
                                group by tbl_fiche_pr.uuid,tbl_fiche_pr.nom,tbl_fiche_pr.post_nom,tbl_fiche_pr.prenom,tbl_fiche_pr.association,tbl_fiche_pr.n_plantation,tbl_fiche_pr.superficie,tbl_fiche_pr.saison,tbl_fiche_pr.essence_principale,
                                tbl_fiche_pr.ecartement_dim_1,tbl_fiche_pr.ecartement_dim_2,tbl_fiche_pr.regarnissage,tbl_fiche_pr.entretien,tbl_fiche_pr.etat,tbl_fiche_pr.croissance_arbres,tbl_fiche_pr.localisation,tbl_fiche_pr.nom_agent,tbl_fiche_pr.n_visite,
                                tbl_fiche_pr.n_visite_2,tbl_fiche_pr.n_viste_3";

                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@n_visite", DbType.String, 255, cboNbrVisite.SelectedValue)); 
                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@n_visite_2", DbType.Int32, 4, Convert.ToInt32(cboNbrVisite.SelectedValue, System.Globalization.CultureInfo.CurrentCulture)));
                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@n_viste_3", DbType.Int32, 4, Convert.ToInt32(cboNbrVisite.SelectedValue, System.Globalization.CultureInfo.CurrentCulture)));
                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@nom_agent", DbType.String, 255, cboAgent.SelectedValue));

                                adapter = new SqlDataAdapter((SqlCommand)cmd);

                                dataset = new DataSet();
                                dataset.Locale = CultureInfo.InvariantCulture;
                                adapter.Fill(dataset, "lstTable");

                                rpt3.SetDataSource(dataset.Tables["lstTable"]);
                                crvReport.ReportSource = rpt3;
                                crvReport.Refresh();
                            }

                            break;
                        case 1:
                            if (rdLstPlanteur.Checked)
                            {
                                rpt4 = new Rapports.LstPlanteurPrAssocBailleur();
                                //par association et par bailleur de fonds
                                cmd.CommandText = @"select tbl_fiche_pr.uuid as 'Identifiant unique',ISNULL(tbl_fiche_pr.nom,'') + ' ' + ISNULL(tbl_fiche_pr.post_nom,'') + ' ' + ISNULL(tbl_fiche_pr.prenom,'') AS 'Noms planteur',tbl_fiche_pr.association as 'Association',
                                tbl_fiche_pr.superficie as 'Hectares réalisé',tbl_fiche_pr.saison as 'Saison',tbl_fiche_pr.essence_principale as 'Essence principale',tbl_fiche_pr.essence_principale_autre as 'Autre essence',tbl_fiche_pr.ecartement_dim_1 as 'Ecartement 1',
                                tbl_fiche_pr.ecartement_dim_2 as 'Ecartement2',tbl_fiche_pr.regarnissage as 'Regarnissage',tbl_fiche_pr.entretien as 'Entretien',tbl_fiche_pr.etat as 'Etat plantation',tbl_fiche_pr.croissance_arbres as 'Croissance arbre',
                                tbl_fiche_pr.localisation as 'Coordonnées géographiques',tbl_fiche_pr.bailleur as 'Bailleur'
                                from tbl_fiche_pr 
                                where tbl_fiche_pr.association=@association and tbl_fiche_pr.bailleur=@bailleur";

                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@association", DbType.String, 255, cboAssociation.SelectedValue));
                                cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@bailleur", DbType.String, 255, cboBailleur.SelectedValue));

                                adapter = new SqlDataAdapter((SqlCommand)cmd);

                                dataset = new DataSet();
                                dataset.Locale = CultureInfo.InvariantCulture;
                                adapter.Fill(dataset, "lstTable");

                                rpt4.SetDataSource(dataset.Tables["lstTable"]);
                                crvReport.ReportSource = rpt4;
                                crvReport.Refresh();
                            }

                            break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(stringManager.GetString("StringFailedLoadReportMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadReportCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de chargement du rapport : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                    ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
                }
                finally
                {
                    if(rpt1 != null)
                        rpt1.Dispose();
                    if(rpt2 != null)
                        rpt2.Dispose();
                    if(rpt3 != null)
                        rpt3.Dispose();
                    if(rpt4 != null)
                        rpt4.Dispose();

                    dataset.Dispose();
                    adapter.Dispose();
                    conn.Close();
                }
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            try
            {
                int index = cboItems.SelectedIndex;
                if (!string.IsNullOrEmpty(cboItems.Text) && !string.IsNullOrEmpty(cboSaison.Text) && !string.IsNullOrEmpty(cboAssociation.Text) &&
                    !string.IsNullOrEmpty(cboBailleur.Text) && !string.IsNullOrEmpty(cboNbrVisite.Text) && !string.IsNullOrEmpty(cboAgent.Text))
                {
                    if (index >= 0)
                    {
                        LoadReport(index);
                    }
                    else
                        throw new XentryDesktopCustomException("La sélection de l'élément du rapport est invalide !!");
                }
                else
                    throw new XentryDesktopCustomException("Veuillez vérifier que toutes les listes déroulantes ne sont pas vides");
            }
            catch (XentryDesktopCustomException ex)
            {
                Properties.Settings.Default.StringLogFile = ex.Message;
                MessageBox.Show(Properties.Settings.Default.StringLogFile, stringManager.GetString("StringFailedLoadReportCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de chargement du rapport : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadReportMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadReportCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de chargement du rapport : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private static void SetMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }

        private void frmReportTAR_Load(object sender, EventArgs e)
        {
            try
            {
                cboSaison.DataSource = clsMetier.GetInstance().getAllClstbl_saison();
                FormReportPr.SetMembersallcbo(cboSaison, "Saison", "Saison");
                cboBailleur.DataSource = clsMetier.GetInstance().getAllClstbl_bailleur();
                FormReportPr.SetMembersallcbo(cboBailleur, "Bailleur", "Bailleur");
                cboAssociation.DataSource = clsMetier.GetInstance().getAllClstbl_association();
                FormReportPr.SetMembersallcbo(cboAssociation, "Association", "Association");
                cboNbrVisite.DataSource = clsMetier.GetInstance().getAllClstbl_n_visite_fiche_pr();
                FormReportPr.SetMembersallcbo(cboNbrVisite, "n_visite", "n_visite");
                cboAgent.DataSource = clsMetier.GetInstance().getAllClstbl_agent();
                FormReportPr.SetMembersallcbo(cboAgent, "Agent", "Agent");

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
            catch (ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadComboBoxMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadComboBoxCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de chargement des listes déroulantes : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadComboBoxMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadComboBoxCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de chargement des listes déroulantes : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
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
