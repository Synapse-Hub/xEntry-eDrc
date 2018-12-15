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
    public partial class FormReportSuiviPepiniere : Form
    {
        IDbConnection conn = null;
        ResourceManager stringManager = null;

        public FormReportSuiviPepiniere()
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
                SqlDataAdapter adapter = null;
                DataSet dataset = null;

                Rapports.LstSuiviPepiAgentSaison rpt1 = null;
                Rapports.LstSuiviPepiQteSemeProvenance rpt2 = null;
                Rapports.LstSuiviPepiQteSemeePlancheRepiq rpt3 = null;

                try
                {
                    switch (cboIndex)
                    {
                        case 0:
                            rpt1 = new Rapports.LstSuiviPepiAgentSaison();
                            //Liste des pépinières par agent et par saison
                            cmd.CommandText = @"select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.deviceid as 'ID péripherique',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.saison as 'Saison',tbl_grp_c_fiche_ident_pepi.count as 'Comptage',tbl_fiche_ident_pepi.association as 'Association',tbl_fiche_ident_pepi.bailleur as 'Bailleur',
                            tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',
                            tbl_fiche_ident_pepi.nb_pepinieristes_formes as 'Nbr Pépinieristes formés',tbl_fiche_ident_pepi.contrat as 'Contrat',tbl_fiche_ident_pepi.combien_pepinieristes as 'Total pépinieristes',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité totale planche',
                            tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_fiche_suivi_pepi.superficie_potentielle_2 as 'Superficie potentielle 2',tbl_fiche_suivi_pepi.superficie_potentielle_3 as 'Superficie potentielle 3',tbl_fiche_suivi_pepi.superficie_potentielle_2_5 as 'Superficie potentielle 2-5',
                            tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Autre essence germoir',tbl_plant_repiq_fiche_suivi_pepi.date_repiquage as 'Date repiqauge',tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_plant_repiq_fiche_suivi_pepi.plantules_encore_repiques 'Planture encore repiqué',tbl_fiche_ident_pepi.observations as 'Observations'
                            from tbl_fiche_ident_pepi
                            inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
                            inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
                            inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
                            inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
                            where tbl_fiche_ident_pepi.agent=@agent and tbl_fiche_ident_pepi.saison=@saison";

                            SqlCommand sqlCmd = cmd as SqlCommand;
                            adapter = new SqlDataAdapter(sqlCmd);

                            cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@agent", DbType.String, 255, cboAgent.SelectedValue)); 
                            cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@saison", DbType.String, 255, cboSaison.SelectedValue));

                            dataset = new DataSet();
                            dataset.Locale = CultureInfo.InvariantCulture;
                            adapter.Fill(dataset, "lstTable");

                            rpt1.SetDataSource(dataset.Tables["lstTable"]);
                            crvReport.ReportSource = rpt1;
                            crvReport.Refresh();
                            break;
                        case 1:
                            rpt2 = new Rapports.LstSuiviPepiQteSemeProvenance();
                            //Liste des pépinières par Qte semée et par lieu de provenance
                            cmd.CommandText = @"select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.nom_site as 'Nom site',
                            tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',
                            tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Essence germoir autre',
                            tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_fiche_ident_pepi.observations as 'Observations',tbl_germoir_fiche_suivi_pepi.qte_semee as 'Qte Semée',tbl_germoir_fiche_suivi_pepi.provenance as 'Provenance'
                            from tbl_fiche_ident_pepi
                            inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
                            inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
                            inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
                            inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
                            where tbl_germoir_fiche_suivi_pepi.provenance=@provenance and tbl_germoir_fiche_suivi_pepi.qte_semee=@qte_semee";

                            adapter = new SqlDataAdapter((SqlCommand)cmd);

                            cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@provenance", DbType.String, 255, cboLieuProvenance.SelectedValue)); 
                            cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@qte_semee", DbType.Int32, 4, Convert.ToInt16(cboQteSemee.SelectedValue, System.Globalization.CultureInfo.CurrentCulture)));

                            dataset = new DataSet();
                            dataset.Locale = CultureInfo.InvariantCulture;
                            adapter.Fill(dataset, "lstTable");

                            rpt2.SetDataSource(dataset.Tables["lstTable"]);
                            crvReport.ReportSource = rpt2;
                            crvReport.Refresh();
                            break;
                        case 2:
                            rpt3 = new Rapports.LstSuiviPepiQteSemeePlancheRepiq();
                            //Liste des pépinières par Qte semée et par planche repiquage

                            cmd.CommandText = @"select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',
                            tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',
                            tbl_fiche_ident_pepi.localisation as 'Géolocalisation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité totale planche',tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',
                            tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Autre essence germoir',tbl_plant_repiq_fiche_suivi_pepi.date_repiquage as 'Date repiqauge',tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_plant_repiq_fiche_suivi_pepi.plantules_encore_repiques 'Planture encore repiqué',
                            tbl_plant_repiq_fiche_suivi_pepi.plantules_deja_evacues as 'Planture déjà evacué',tbl_fiche_ident_pepi.observations as 'Observations',tbl_germoir_fiche_suivi_pepi.qte_semee as 'Qte Semée'
                            from tbl_fiche_ident_pepi
                            inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
                            inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
                            inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
                            inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
                            where (tbl_plant_repiq_fiche_suivi_pepi.planches_repiquage_essence=@planches_repiquage_essence or tbl_plant_repiq_fiche_suivi_pepi.planches_repiquage_essence_autre=@planches_repiquage_essence_autre)  and tbl_germoir_fiche_suivi_pepi.qte_semee=@qte_semee";

                            adapter = new SqlDataAdapter((SqlCommand)cmd);

                            cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@planches_repiquage_essence", DbType.String, 255, cboPlancheRepiquage.SelectedValue)); 
                             cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@planches_repiquage_essence_autre", DbType.String, 255, cboPlancheRepiquage.SelectedValue));
                            cmd.Parameters.Add(clsMetier.GetInstance().getParameter(cmd, "@qte_semee", DbType.Int32, 4, Convert.ToInt32(cboQteSemee.SelectedValue, System.Globalization.CultureInfo.CurrentCulture)));

                            dataset = new DataSet();
                            dataset.Locale = CultureInfo.InvariantCulture;
                            adapter.Fill(dataset, "lstTable");
                            
                            rpt3.SetDataSource(dataset.Tables["lstTable"]);
                            crvReport.ReportSource = rpt3;
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
                    if(rpt1 != null)
                        rpt1.Dispose();
                    if(rpt2 != null)
                        rpt2.Dispose();
                    if(rpt3 != null)
                        rpt3.Dispose();

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
                if (!string.IsNullOrEmpty(cboItems.Text) && !string.IsNullOrEmpty(cboSaison.Text) && !string.IsNullOrEmpty(cboLieuProvenance.Text) &&
                    !string.IsNullOrEmpty(cboPlancheRepiquage.Text) && !string.IsNullOrEmpty(cboQteSemee.Text) && !string.IsNullOrEmpty(cboAgent.Text))
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
            cboItems.Items.Add("Liste des pépinières par agent et par saison");
            cboItems.Items.Add("Liste des pépinières par Qte semée et par lieu de provenance");
            cboItems.Items.Add("Liste des pépinières par Qte semée et par planche repiquage");
            cboItems.Sorted = false;
            cboItems.SelectedIndex = 0;

            try
            {
                cboSaison.DataSource = clsMetier.GetInstance().getAllClstbl_saison();
                SetMembersallcbo(cboSaison, "Saison", "Saison");
                cboAgent.DataSource = clsMetier.GetInstance().getAllClstbl_agent();
                SetMembersallcbo(cboAgent, "Agent", "Agent");
                cboQteSemee.DataSource = clsMetier.GetInstance().getAllClstbl_Qte_Semee_germoir_fiche_suivi_pepi();
                SetMembersallcbo(cboQteSemee, "qte_semee", "qte_semee");
                cboLieuProvenance.DataSource = clsMetier.GetInstance().getAllClstbl_Lieu_Provenance_germoir_fiche_suivi_pepi();
                SetMembersallcbo(cboLieuProvenance, "provenance", "provenance");
                cboPlancheRepiquage.DataSource = clsMetier.GetInstance().getAllClstbl_planche_repiquage_fiche_suivi_pepi();
                SetMembersallcbo(cboPlancheRepiquage, "planches_repiquage", "planches_repiquage");

                if (cboSaison.Items.Count > 0)
                    cboSaison.SelectedIndex = 0;
                if (cboAgent.Items.Count > 0)
                    cboAgent.SelectedIndex = 0;
                if (cboQteSemee.Items.Count > 0)
                    cboQteSemee.SelectedIndex = 0;
                if (cboLieuProvenance.Items.Count > 0)
                    cboLieuProvenance.SelectedIndex = 0;
                if (cboPlancheRepiquage.Items.Count > 0)
                    cboPlancheRepiquage.SelectedIndex = 0;
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

        private void cboItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cboItems.SelectedIndex)
            {
                case 0:
                    cboAgent.Enabled = true;
                    cboSaison.Enabled = true;
                    cboLieuProvenance.Enabled = false;
                    cboPlancheRepiquage.Enabled = false;
                    cboQteSemee.Enabled = false;
                    break;
                case 1:
                    cboAgent.Enabled = false;
                    cboSaison.Enabled = false;
                    cboLieuProvenance.Enabled = true;
                    cboPlancheRepiquage.Enabled = false;
                    cboQteSemee.Enabled = true;
                    break;
                case 2:
                    cboAgent.Enabled = false;
                    cboSaison.Enabled = false;
                    cboLieuProvenance.Enabled = false;
                    cboPlancheRepiquage.Enabled = true;
                    cboQteSemee.Enabled = true;
                    break;
            }
        }
    }
}
