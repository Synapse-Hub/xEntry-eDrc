using ManageUtilities;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Xentry.Data;

namespace Xentry.Desktop
{
    public partial class MdiMainForm : Form
    {
        ResourceManager stringManager = null;

        public string Usern
        {
            get; set;
        }

        public string Lblformstatus
        {
            get; set;
        }

        public FormEntryTar Entry
        {
            get; set;
        }

        public MdiMainForm()
        {
            InitializeComponent();
            //Si on utilise le meme Assemby que le projet utiliser Assembly.GetExecutingAssembly()
            //stringManager = new ResourceManager("Xentry.Desktop.XentryResource", Assembly.GetExecutingAssembly());

            //Dans le cas contraire utiliser ceci
            //Initialisation des Resources
            Assembly _assembly = Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);

            //Ou encore
            //stringManager = new ResourceManager("Xentry.Resources.XentryResource", typeof(Xentry.Resources.clsMetier).Assembly);
        }

        private void mdiMainForm_Load(object sender, EventArgs e)
        {
            LockMenu(false, "");
            lblInfoForm.Text = "";
            lblInfoForm.ToolTipText = stringManager.GetString("StringInfoStatusAppMessage", CultureInfo.CurrentUICulture);
            Lblformstatus = lblInfoForm.Text;
            //mnuCatego.Enabled = false;
        }

        //
        public void LockMenu(bool value, string levelUser)
        {
            switch(levelUser)
            {
                //Utilisateur Administrateur avec tous les menus actives
                case "Administrateur":
                    //Sous Menus
                    smnConnect.Enabled = !value;
                    smnDisconnect.Enabled = value;

                    smnDEntry.Enabled = value;
                    smnExplData.Enabled = value;
                    smnEntryNursery.Enabled = value;
                    smnEssence.Enabled = value;
                    smnReports.Enabled = value;
                    smnViewManage.Enabled = value;
                    smnUserManage.Enabled = value;

                    ssmnExecute.Enabled = value;
                    ssmnSynchroniseGUI.Enabled = value;

                    ssmnIdentificationPepiniere.Enabled = value;
                    ssmnSuiviPepiniere.Enabled = value;

                    ssmnReportsTAR.Enabled = value;
                    ssmnReportsPR.Enabled = value;
                    ssmnReportsSuiviPepi.Enabled = value;
                    ssmnReportsIdentPepi.Enabled = value;

                    //Barre d'outil
                    tlbConnect.Enabled = !value;
                    tlbPrint.Enabled = value;
                    tlbDEntry.Enabled = value;
                    tlbEntryNursery.Enabled = value;
                    tlbEssence.Enabled = value;
                    tlbExpData.Enabled = value;

                    stlbReportsTAR.Enabled = value;
                    stlbReportsPR.Enabled = value;
                    stlbReportsSuiviPepi.Enabled = value;
                    stlbReportsIdentPepi.Enabled = value;

                    break;

                //Utilisateur avec pouvoir
                case "Admin":
                    //Sous Menus
                    smnConnect.Enabled = !value;
                    smnDisconnect.Enabled = value;

                    smnDEntry.Enabled = value;
                    smnExplData.Enabled = value;
                    smnEntryNursery.Enabled = value;
                    smnEssence.Enabled = value;
                    smnReports.Enabled = value;
                    smnViewManage.Enabled = value;
                    smnUserManage.Enabled = value;

                    ssmnExecute.Enabled = value;
                    ssmnSynchroniseGUI.Enabled = value;

                    ssmnIdentificationPepiniere.Enabled = value;
                    ssmnSuiviPepiniere.Enabled = value;

                    ssmnReportsTAR.Enabled = value;
                    ssmnReportsPR.Enabled = value;
                    ssmnReportsSuiviPepi.Enabled = value;
                    ssmnReportsIdentPepi.Enabled = value;

                    //Barre d'outil
                    tlbConnect.Enabled = !value;
                    tlbPrint.Enabled = value;
                    tlbDEntry.Enabled = value;
                    tlbEntryNursery.Enabled = value;
                    tlbEssence.Enabled = value;
                    tlbExpData.Enabled = value;

                    stlbReportsTAR.Enabled = value;
                    stlbReportsPR.Enabled = value;
                    stlbReportsSuiviPepi.Enabled = value;
                    stlbReportsIdentPepi.Enabled = value;

                    break;

                //Utilisateur Simple
                case "User":
                    //Sous Menus
                    smnConnect.Enabled = !value;
                    smnDisconnect.Enabled = value;

                    smnDEntry.Enabled = value;
                    smnExplData.Enabled = !value;
                    smnEntryNursery.Enabled =! value;
                    smnEssence.Enabled = !value;
                    smnReports.Enabled = value;
                    smnViewManage.Enabled = !value;
                    smnUserManage.Enabled = !value;

                    ssmnExecute.Enabled = !value;
                    ssmnSynchroniseGUI.Enabled = !value;

                    ssmnIdentificationPepiniere.Enabled = !value;
                    ssmnSuiviPepiniere.Enabled = !value;

                    ssmnReportsTAR.Enabled = value;
                    ssmnReportsPR.Enabled = value;
                    ssmnReportsSuiviPepi.Enabled = value;
                    ssmnReportsIdentPepi.Enabled = value;

                    //Barre d'outil
                    tlbConnect.Enabled = !value;
                    tlbPrint.Enabled = value;
                    tlbDEntry.Enabled = !value;
                    tlbEntryNursery.Enabled = !value;
                    tlbEssence.Enabled = !value;
                    tlbExpData.Enabled = !value;

                    stlbReportsTAR.Enabled = value;
                    stlbReportsPR.Enabled = value;
                    stlbReportsSuiviPepi.Enabled = value;
                    stlbReportsIdentPepi.Enabled = value;

                    break;

                default:
                    //Sous Menus
                    smnConnect.Enabled = !value;
                    smnDisconnect.Enabled = value;

                    smnDEntry.Enabled = value;
                    smnExplData.Enabled = value;
                    smnEntryNursery.Enabled = value;
                    smnEssence.Enabled = value;
                    smnReports.Enabled = value;
                    smnViewManage.Enabled = value;
                    smnUserManage.Enabled = value;

                    ssmnExecute.Enabled = value;
                    ssmnSynchroniseGUI.Enabled = value;

                    ssmnIdentificationPepiniere.Enabled = value;
                    ssmnSuiviPepiniere.Enabled = value;

                    ssmnReportsTAR.Enabled = value;
                    ssmnReportsPR.Enabled = value;
                    ssmnReportsSuiviPepi.Enabled = value;
                    ssmnReportsIdentPepi.Enabled = value;

                    //Barre d'outil
                    tlbConnect.Enabled = !value;
                    tlbPrint.Enabled = value;
                    tlbDEntry.Enabled = value;
                    tlbEntryNursery.Enabled = value;
                    tlbEssence.Enabled = value;
                    tlbExpData.Enabled = value;

                    stlbReportsTAR.Enabled = value;
                    stlbReportsPR.Enabled = value;
                    stlbReportsSuiviPepi.Enabled = value;
                    stlbReportsIdentPepi.Enabled = value;

                    break;
            }

            if (value != false)
            {
                Properties.Settings.Default.StringUserConnectedNameMDI = "'" + Usern + "' ";
                statLabel.Text = Properties.Settings.Default.StringUserConnectedNameMDI + stringManager.GetString("StringStatusConnectedUserMessage", CultureInfo.CurrentUICulture);
            }
            else
            {
                statLabel.Text = stringManager.GetString("StringStatusDisconnectedUserMessage", CultureInfo.CurrentUICulture);
            }
        }

        private void tlbConnect_Click(object sender, EventArgs e)
        {
            FormConn frm = new FormConn();
            frm.PrincipalForm = this;
            frm.ShowDialog();
        }

        private void tlbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(stringManager.GetString("StringPromptQuitAppMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringPromptQuitAppCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if(result == DialogResult.Yes)
            {
                //On se deconnecte de la BD si on y etait connecte
                try
                {
                    clsMetier.GetInstance().closeConnexion();
                }
                catch (ObjectDisposedException ex)
                {
                    ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, stringManager.GetString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de fermeture du formulaire principal : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message, CultureInfo.CurrentUICulture), Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
                }
                catch (InvalidOperationException ex)
                {
                    ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, stringManager.GetString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de fermeture du formulaire principal : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message, CultureInfo.CurrentUICulture), Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
                }
                catch (MissingManifestResourceException ex)
                {
                    ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, stringManager.GetString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de fermeture du formulaire principal : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message, CultureInfo.CurrentUICulture), Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
                }
                finally
                {
                    this.Close();
                    Application.Exit();
                }
            }           
        }

        private void smnDEntry_Click(object sender, EventArgs e)
        {
            //if (entry == null)
            //{
            //    entry = new EntryForm();
            //    entry.MdiParent = this;
            //    entry.setMdiMainForm(this);
            //    entry.Icon = this.Icon;
            //    entry.Show();
            //}
        }

        private void tlbDEntry_Click(object sender, EventArgs e)
        {
            smnDEntry_Click(sender, e);
        }

        private void smnUserManage_Click(object sender, EventArgs e)
        {
            FormUtilisateur frm = new FormUtilisateur();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void smnToolBar_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = smnToolBar.Checked;
        }

        private void smnStatusBar_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = smnStatusBar.Checked;
        }

        private void smnExit_Click(object sender, EventArgs e)
        {
            tlbExit_Click(sender, e);
        }

        private void smnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                clsMetier.GetInstance().closeConnexion();
                LockMenu(false, "");
            }
            catch(ObjectDisposedException ex)
            {
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, stringManager.GetString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors de la déconnexion à la BD : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message, CultureInfo.CurrentUICulture), Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (InvalidOperationException ex)
            {
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, stringManager.GetString(DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors de la déconnexion à la BD : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message, CultureInfo.CurrentUICulture), Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private void ssmnExecute_Click(object sender, EventArgs e)
        {

        }

        private void ssmnSynchroniseGUI_Click(object sender, EventArgs e)
        {

        }

        private void mdiMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void smnEntryNursery_Click(object sender, EventArgs e)
        {
            
        }

        private void smn_tar_Click(object sender, EventArgs e)
        {
            FormEntryTar frm = new FormEntryTar();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void smnAbout_Click(object sender, EventArgs e)
        {

        }

        private void smnContent_Click(object sender, EventArgs e)
        {
            
        }

        private void ssmnReportsTAR_Click(object sender, EventArgs e)
        {
            FormReportTar frm = new FormReportTar();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void smnConnect_Click(object sender, EventArgs e)
        {
            FormConn frm = new FormConn();
            frm.PrincipalForm = this;
            frm.ShowDialog();
        }

        private void ssmnReportsPR_Click(object sender, EventArgs e)
        {
            FormReportPr frm = new FormReportPr();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void ssmnReportsSuiviPepi_Click(object sender, EventArgs e)
        {
            FormReportSuiviPepiniere frm = new FormReportSuiviPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void ssmnReportsIdentPepi_Click(object sender, EventArgs e)
        {
            FormReportIdentPepiniere frm = new FormReportIdentPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void stlbReportsTAR_Click(object sender, EventArgs e)
        {
            FormReportTar frm = new FormReportTar();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void stlbReportsPR_Click(object sender, EventArgs e)
        {
            FormReportPr frm = new FormReportPr();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void stlbReportsSuiviPepi_Click(object sender, EventArgs e)
        {
            FormReportSuiviPepiniere frm = new FormReportSuiviPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void stlbReportsIdentPepi_Click(object sender, EventArgs e)
        {
            FormReportIdentPepiniere frm = new FormReportIdentPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void mnuPR_Click(object sender, EventArgs e)
        {
            FormPlantationrealise frm = new FormPlantationrealise();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void btnShowMap_Click(object sender, EventArgs e)
        {
            FormLinkGeolocation frm = new FormLinkGeolocation();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();

            //FormLinkGeolocation frm = null;

            //try
            //{
            //    frm = new FormLinkGeolocation();
            //    frm.MdiParent = this;
            //    frm.Icon = this.Icon;
            //    frm.Show();
            //}
            //finally
            //{
            //    frm.Dispose();
            //}
        }

        private void ssmnIdentificationPepiniere_Click(object sender, EventArgs e)
        {
            FormIdentificationPepiniere frm = new FormIdentificationPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void ssmnSuiviPepiniere_Click(object sender, EventArgs e)
        {
            FormNursery frm = new FormNursery();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }
    }
}
