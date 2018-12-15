using ManageUtilities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Xentry.Desktop
{
    public partial class FormReportIdentPepiniere : Form
    {
        IDbConnection conn = null;
        ResourceManager stringManager = null;

        public FormReportIdentPepiniere()
        {
            InitializeComponent();
            //Initialisation des Resources
            Assembly _assembly = Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);
        }

        private void LoadReport(int cboIndex)
        {
            //Initialisation de la chaine de connexion
            conn = new SqlConnection(Xentry.Data.Properties.Settings.Default.strChaineConnexion);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                Rapports.LstIdentPepiniere rpt = null;
                SqlDataAdapter adapter = null;
                DataSet dataset = null;

                try
                {
                    switch (cboIndex)
                    {
                        case 0:
                            rpt = new Rapports.LstIdentPepiniere();
                            //Liste des pépinières identifiées
                            cmd.CommandText = @"select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.saison as 'Saison',tbl_fiche_ident_pepi.association as 'Association',tbl_fiche_ident_pepi.bailleur as 'Bailleur',
                            tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',
                            tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité planche',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',observations as 'Observations'
                            from tbl_fiche_ident_pepi
                            inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid";

                            SqlCommand sqlCmd = cmd as SqlCommand;
                            adapter = new SqlDataAdapter(sqlCmd);
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
                if (!string.IsNullOrEmpty(cboItems.Text))
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

        private void frmReportTAR_Load(object sender, EventArgs e)
        {
            cboItems.Items.Add("Liste des pépinières identifiées");
            cboItems.Sorted = false;
            cboItems.SelectedIndex = 0;
        }
    }
}
