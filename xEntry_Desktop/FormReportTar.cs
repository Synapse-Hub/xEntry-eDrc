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
    public partial class FormReportTar : Form
    {
        IDbConnection conn = null;
        ResourceManager stringManager = null;

        public FormReportTar()
        {
            InitializeComponent();
            //Initialisation des Resources
            Assembly _assembly = Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);
        }

        private static void SetMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
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

                Rapports.LstPlanteurTarSaisonTerritoire rpt = null;

                try
                {
                    switch (cboIndex)
                    {
                        case 0:
                            rpt = new Rapports.LstPlanteurTarSaisonTerritoire();
                            //Liste des planteurs Tar par saison et par territoire
                            cmd.CommandText = @"select tbl_fiche_tar.uuid as 'Identifiant unique',ISNULL(tbl_fiche_tar.nom,'') + '' + ISNULL(tbl_fiche_tar.postnom,'') + ' ' + ISNULL(tbl_fiche_tar.prenom,'') AS 'Noms planteur',tbl_fiche_tar.nom_lieu_plantation as 'Lieu plantation',tbl_fiche_tar.territoire as 'Territoire',tbl_fiche_tar.groupement as 'Groupement',tbl_fiche_tar.association as 'Association',
                            tbl_fiche_tar.superficie_totale as 'Hectare à réaliser',tbl_fiche_tar.saison as 'Saison',tbl_fiche_tar.essence_principale as 'Essence principale',tbl_fiche_tar.essence_principale_autre as 'Autre essence',objectifs_planteur as 'Objectifs principal',tbl_fiche_tar.objectifs_planteur_autre as 'Autre objectif',
                            tbl_fiche_tar.utilisation_precedente as 'Utilisation précédente',tbl_fiche_tar.arbres_existants as 'Nbr arbre existants',tbl_fiche_tar.situation as 'Situation',tbl_fiche_tar.pente as 'Pente',tbl_fiche_tar.document_de_propriete as 'Documents propriétaire' 
                            from tbl_fiche_tar 
                            inner join tbl_territoire on tbl_territoire.territoire=tbl_fiche_tar.territoire
                            inner join tbl_saison on tbl_saison.saison=tbl_fiche_tar.saison 
                            where tbl_fiche_tar.territoire=@territoire and tbl_fiche_tar.saison=@saison";

                            SqlCommand sqlCmd = cmd as SqlCommand;
                            adapter = new SqlDataAdapter(sqlCmd);

                            cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@territoire", DbType.String, 255, cboTerritoire.SelectedValue)); 
                            cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@saison", DbType.String, 255, cboSaison.SelectedValue));

                            dataset = new DataSet();
                            dataset.Locale = CultureInfo.InvariantCulture;
                            adapter.Fill(dataset, "lstTable");

                            rpt.SetDataSource(dataset.Tables["lstTable"]);
                            crvReport.ReportSource = rpt;
                            crvReport.Refresh();
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
                    rpt.Dispose();
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
                if (!string.IsNullOrEmpty(cboItems.Text) && !string.IsNullOrEmpty(cboSaison.Text) && !string.IsNullOrEmpty(cboTerritoire.Text))
                {
                    if(index >= 0)
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

        private void frmReportTAR_Load(object sender, EventArgs e)
        {
            cboItems.Items.Add("Liste des planteurs (Hectares réalisés) par territoire et par saison");
            cboItems.Sorted = false;
            cboItems.SelectedIndex = 0;

            try
            {
                cboSaison.DataSource = clsMetier.GetInstance().getAllClstbl_saison();
                SetMembersallcbo(cboSaison, "Saison", "Saison");

                cboTerritoire.DataSource = clsMetier.GetInstance().getAllClstbl_territoire();
                SetMembersallcbo(cboTerritoire, "Territoire", "Territoire");

                if (cboSaison.Items.Count > 0)
                    cboSaison.SelectedIndex = 0;

                if (cboTerritoire.Items.Count > 0)
                    cboTerritoire.SelectedIndex = 0;
            }            
            catch (SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadComboBoxMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadComboBoxCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de chargement des listes déroulantes : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }
    }
}
