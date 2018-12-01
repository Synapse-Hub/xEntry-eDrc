using System;
using System.Windows.Forms;
using xEntry_Data;

namespace xEntry_Desktop
{
    public partial class mdiMainForm : Form
    {
        //private int childFormNumber = 0;
        public string strconn = "", lblstatusconn = "";
        public string usern = "", lblformstatus = "";
        public  frmxConn xconnf = null;
        public EntryTar entry = null;
  
        
        public mdiMainForm()
        {
            InitializeComponent();
        }

        private void mdiMainForm_Load(object sender, EventArgs e)
        {
            LockMenu(false, "");
            lblInfoForm.Text = "";
            lblInfoForm.ToolTipText = "Forms informations status";
            lblformstatus = lblInfoForm.Text;
            //mnuCatego.Enabled = false;
        }

        //
        public void LockMenu(bool val, string levelUser)
        {
            switch(levelUser)
            {
                //Utilisateur Administrateur avec tous les menus actives
                case "Administrateur":
                    //Sous Menus
                    smnConnect.Enabled = !val;
                    smnDisconnect.Enabled = val;

                    smnDEntry.Enabled = val;
                    smnExplData.Enabled = val;
                    smnEntryNursery.Enabled = val;
                    smnEssence.Enabled = val;
                    smnReports.Enabled = val;
                    smnViewManage.Enabled = val;
                    smnUserManage.Enabled = val;

                    ssmnExecute.Enabled = val;
                    ssmnSynchroniseGUI.Enabled = val;

                    ssmnReportsTAR.Enabled = val;
                    ssmnReportsPR.Enabled = val;
                    ssmnReportsSuiviPepi.Enabled = val;
                    ssmnReportsIdentPepi.Enabled = val;

                    //Barre d'outil
                    tlbConnect.Enabled = !val;
                    tlbPrint.Enabled = val;
                    tlbDEntry.Enabled = val;
                    tlbEntryNursery.Enabled = val;
                    tlbEssence.Enabled = val;
                    tlbExpData.Enabled = val;

                    stlbReportsTAR.Enabled = val;
                    stlbReportsPR.Enabled = val;
                    stlbReportsSuiviPepi.Enabled = val;
                    stlbReportsIdentPepi.Enabled = val;

                    break;

                //Utilisateur avec pouvoir
                case "Admin":
                    //Sous Menus
                    smnConnect.Enabled = !val;
                    smnDisconnect.Enabled = val;

                    smnDEntry.Enabled = val;
                    smnExplData.Enabled = val;
                    smnEntryNursery.Enabled = val;
                    smnEssence.Enabled = val;
                    smnReports.Enabled = val;
                    smnViewManage.Enabled = val;
                    smnUserManage.Enabled = val;

                    ssmnExecute.Enabled = val;
                    ssmnSynchroniseGUI.Enabled = val;

                    ssmnReportsTAR.Enabled = val;
                    ssmnReportsPR.Enabled = val;
                    ssmnReportsSuiviPepi.Enabled = val;
                    ssmnReportsIdentPepi.Enabled = val;

                    //Barre d'outil
                    tlbConnect.Enabled = !val;
                    tlbPrint.Enabled = val;
                    tlbDEntry.Enabled = val;
                    tlbEntryNursery.Enabled = val;
                    tlbEssence.Enabled = val;
                    tlbExpData.Enabled = val;

                    stlbReportsTAR.Enabled = val;
                    stlbReportsPR.Enabled = val;
                    stlbReportsSuiviPepi.Enabled = val;
                    stlbReportsIdentPepi.Enabled = val;

                    break;

                //Utilisateur Simple
                case "User":
                    //Sous Menus
                    smnConnect.Enabled = !val;
                    smnDisconnect.Enabled = val;

                    smnDEntry.Enabled = val;
                    smnExplData.Enabled = !val;
                    smnEntryNursery.Enabled =! val;
                    smnEssence.Enabled = !val;
                    smnReports.Enabled = val;
                    smnViewManage.Enabled = !val;
                    smnUserManage.Enabled = !val;

                    ssmnExecute.Enabled = !val;
                    ssmnSynchroniseGUI.Enabled = !val;

                    ssmnReportsTAR.Enabled = val;
                    ssmnReportsPR.Enabled = val;
                    ssmnReportsSuiviPepi.Enabled = val;
                    ssmnReportsIdentPepi.Enabled = val;

                    //Barre d'outil
                    tlbConnect.Enabled = !val;
                    tlbPrint.Enabled = val;
                    tlbDEntry.Enabled = !val;
                    tlbEntryNursery.Enabled = !val;
                    tlbEssence.Enabled = !val;
                    tlbExpData.Enabled = !val;

                    stlbReportsTAR.Enabled = val;
                    stlbReportsPR.Enabled = val;
                    stlbReportsSuiviPepi.Enabled = val;
                    stlbReportsIdentPepi.Enabled = val;

                    break;

                default:
                    //Sous Menus
                    smnConnect.Enabled = !val;
                    smnDisconnect.Enabled = val;

                    smnDEntry.Enabled = val;
                    smnExplData.Enabled = val;
                    smnEntryNursery.Enabled = val;
                    smnEssence.Enabled = val;
                    smnReports.Enabled = val;
                    smnViewManage.Enabled = val;
                    smnUserManage.Enabled = val;

                    ssmnExecute.Enabled = val;
                    ssmnSynchroniseGUI.Enabled = val;

                    ssmnReportsTAR.Enabled = val;
                    ssmnReportsPR.Enabled = val;
                    ssmnReportsSuiviPepi.Enabled = val;
                    ssmnReportsIdentPepi.Enabled = val;

                    //Barre d'outil
                    tlbConnect.Enabled = !val;
                    tlbPrint.Enabled = val;
                    tlbDEntry.Enabled = val;
                    tlbEntryNursery.Enabled = val;
                    tlbEssence.Enabled = val;
                    tlbExpData.Enabled = val;

                    stlbReportsTAR.Enabled = val;
                    stlbReportsPR.Enabled = val;
                    stlbReportsSuiviPepi.Enabled = val;
                    stlbReportsIdentPepi.Enabled = val;

                    break;
            }

            if (val != false)
            {
                this.statLabel.Text = "'" + usern + "' est maintenant connecté ...";
            }
            else
            {
                this.statLabel.Text = "Deconnecté de la base des données ...";
            }
        }

        private void tlbConnect_Click(object sender, EventArgs e)
        {
            frmxConn frm = new frmxConn();
            frm.Mainform = this;
            frm.ShowDialog();
        }

        private void tlbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Voulez-vous vraiment quitter ?", "Quitter l'application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                //On se deconnecte de la BD si on y etait connecte
                try
                {
                    clsMetier.GetInstance().closeConnexion();
                }
                catch (Exception) { }

                this.Close();
                Application.Exit();
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
            frmUtilisateur frm = new frmUtilisateur();
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
            catch (Exception) { }
        }

        private void ssmnExecute_Click(object sender, EventArgs e)
        {

        }

        private void ssmnSynchroniseGUI_Click(object sender, EventArgs e)
        {

        }

        private void mdiMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                clsMetier.GetInstance().closeConnexion();
            }
            catch (Exception) { }

            Application.Exit();
        }

        private void smnEntryNursery_Click(object sender, EventArgs e)
        {
            frmIdentificationPepiniere frm = new frmIdentificationPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void smn_tar_Click(object sender, EventArgs e)
        {
            EntryTar frm = new EntryTar();
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

        private void suiviToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNursery frm = new frmNursery();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void ssmnReportsTAR_Click(object sender, EventArgs e)
        {
            frmReportTAR frm = new frmReportTAR();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void smnConnect_Click(object sender, EventArgs e)
        {
            frmxConn frm = new frmxConn();
            frm.Mainform = this;
            frm.ShowDialog();
        }

        private void ssmnReportsPR_Click(object sender, EventArgs e)
        {
            frmReportPR frm = new frmReportPR();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void ssmnReportsSuiviPepi_Click(object sender, EventArgs e)
        {
            frmReportSuiviPepiniere frm = new frmReportSuiviPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void ssmnReportsIdentPepi_Click(object sender, EventArgs e)
        {
            frmReportIdentPepiniere frm = new frmReportIdentPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void stlbReportsTAR_Click(object sender, EventArgs e)
        {
            frmReportTAR frm = new frmReportTAR();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void stlbReportsPR_Click(object sender, EventArgs e)
        {
            frmReportPR frm = new frmReportPR();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void stlbReportsSuiviPepi_Click(object sender, EventArgs e)
        {
            frmReportSuiviPepiniere frm = new frmReportSuiviPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void stlbReportsIdentPepi_Click(object sender, EventArgs e)
        {
            frmReportIdentPepiniere frm = new frmReportIdentPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void identificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIdentificationPepiniere frm = new frmIdentificationPepiniere();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }

        private void mnuPR_Click(object sender, EventArgs e)
        {
            PlantationReliaser frm = new PlantationReliaser();
            frm.MdiParent = this;
            frm.Icon = this.Icon;
            frm.Show();
        }
    }
}
