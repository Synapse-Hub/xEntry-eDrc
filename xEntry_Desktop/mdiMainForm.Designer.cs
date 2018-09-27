namespace xEntry_Desktop
{
    partial class mdiMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mdiMainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.smnConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.smnDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.smnExit = new System.Windows.Forms.ToolStripMenuItem();
            this.formStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smnDEntry = new System.Windows.Forms.ToolStripMenuItem();
            this.smn_tar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPR = new System.Windows.Forms.ToolStripMenuItem();
            this.smnEntryNursery = new System.Windows.Forms.ToolStripMenuItem();
            this.identificationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suiviToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.smnExplData = new System.Windows.Forms.ToolStripMenuItem();
            this.smnEssence = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.smnToolBar = new System.Windows.Forms.ToolStripMenuItem();
            this.smnStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.smnReports = new System.Windows.Forms.ToolStripMenuItem();
            this.photoGpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smnViewManage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.smnUserManage = new System.Windows.Forms.ToolStripMenuItem();
            this.smnSynchronise = new System.Windows.Forms.ToolStripMenuItem();
            this.ssmnExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.ssmnSynchroniseGUI = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.smnContent = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.smnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tlbExit = new System.Windows.Forms.ToolStripButton();
            this.tlbConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbDEntry = new System.Windows.Forms.ToolStripButton();
            this.tlbEntryNursery = new System.Windows.Forms.ToolStripButton();
            this.tlbExpData = new System.Windows.Forms.ToolStripButton();
            this.tlbEssence = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbReports = new System.Windows.Forms.ToolStripSplitButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblInfoForm = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ssmnReportsTAR = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.formStripMenuItem,
            this.viewMenu,
            this.photoGpsToolStripMenuItem,
            this.toolsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(778, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smnConnect,
            this.toolStripSeparator3,
            this.smnDisconnect,
            this.smnExit});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.fileMenu.Size = new System.Drawing.Size(54, 20);
            this.fileMenu.Text = "&Fichier";
            // 
            // smnConnect
            // 
            this.smnConnect.Image = ((System.Drawing.Image)(resources.GetObject("smnConnect.Image")));
            this.smnConnect.ImageTransparentColor = System.Drawing.Color.Black;
            this.smnConnect.Name = "smnConnect";
            this.smnConnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.smnConnect.Size = new System.Drawing.Size(187, 26);
            this.smnConnect.Text = "Connexion";
            this.smnConnect.Click += new System.EventHandler(this.smnConnect_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(184, 6);
            // 
            // smnDisconnect
            // 
            this.smnDisconnect.Name = "smnDisconnect";
            this.smnDisconnect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.smnDisconnect.Size = new System.Drawing.Size(187, 26);
            this.smnDisconnect.Text = "Disconnexion";
            this.smnDisconnect.Click += new System.EventHandler(this.smnDisconnect_Click);
            // 
            // smnExit
            // 
            this.smnExit.Name = "smnExit";
            this.smnExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.smnExit.Size = new System.Drawing.Size(187, 26);
            this.smnExit.Text = "Q&uitter";
            this.smnExit.ToolTipText = "Fermer";
            this.smnExit.Click += new System.EventHandler(this.smnExit_Click);
            // 
            // formStripMenuItem
            // 
            this.formStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smnDEntry,
            this.smnEntryNursery,
            this.toolStripSeparator5,
            this.smnExplData,
            this.smnEssence});
            this.formStripMenuItem.Name = "formStripMenuItem";
            this.formStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.formStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.formStripMenuItem.Text = "Inter&faces";
            // 
            // smnDEntry
            // 
            this.smnDEntry.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smn_tar,
            this.mnuPR});
            this.smnDEntry.Image = ((System.Drawing.Image)(resources.GetObject("smnDEntry.Image")));
            this.smnDEntry.Name = "smnDEntry";
            this.smnDEntry.Size = new System.Drawing.Size(252, 26);
            this.smnDEntry.Text = "Entry TAR/PR";
            this.smnDEntry.Click += new System.EventHandler(this.smnDEntry_Click);
            // 
            // smn_tar
            // 
            this.smn_tar.Name = "smn_tar";
            this.smn_tar.Size = new System.Drawing.Size(152, 22);
            this.smn_tar.Text = "TAR";
            this.smn_tar.Click += new System.EventHandler(this.smn_tar_Click);
            // 
            // mnuPR
            // 
            this.mnuPR.Name = "mnuPR";
            this.mnuPR.Size = new System.Drawing.Size(152, 22);
            this.mnuPR.Text = "PR";
            this.mnuPR.Click += new System.EventHandler(this.mnuPR_Click);
            // 
            // smnEntryNursery
            // 
            this.smnEntryNursery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.identificationToolStripMenuItem,
            this.suiviToolStripMenuItem});
            this.smnEntryNursery.Image = ((System.Drawing.Image)(resources.GetObject("smnEntryNursery.Image")));
            this.smnEntryNursery.Name = "smnEntryNursery";
            this.smnEntryNursery.Size = new System.Drawing.Size(252, 26);
            this.smnEntryNursery.Text = "Entry PEPINIERE";
            this.smnEntryNursery.Click += new System.EventHandler(this.smnEntryNursery_Click);
            // 
            // identificationToolStripMenuItem
            // 
            this.identificationToolStripMenuItem.Name = "identificationToolStripMenuItem";
            this.identificationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.identificationToolStripMenuItem.Text = "Identification";
            this.identificationToolStripMenuItem.Click += new System.EventHandler(this.identificationToolStripMenuItem_Click);
            // 
            // suiviToolStripMenuItem
            // 
            this.suiviToolStripMenuItem.Name = "suiviToolStripMenuItem";
            this.suiviToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.suiviToolStripMenuItem.Text = "Suivi";
            this.suiviToolStripMenuItem.Click += new System.EventHandler(this.suiviToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(249, 6);
            // 
            // smnExplData
            // 
            this.smnExplData.Image = ((System.Drawing.Image)(resources.GetObject("smnExplData.Image")));
            this.smnExplData.Name = "smnExplData";
            this.smnExplData.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.smnExplData.Size = new System.Drawing.Size(252, 26);
            this.smnExplData.Text = "Exploitation des données";
            // 
            // smnEssence
            // 
            this.smnEssence.Image = ((System.Drawing.Image)(resources.GetObject("smnEssence.Image")));
            this.smnEssence.Name = "smnEssence";
            this.smnEssence.Size = new System.Drawing.Size(252, 26);
            this.smnEssence.Text = "Essence";
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smnToolBar,
            this.smnStatusBar,
            this.toolStripSeparator6,
            this.smnReports});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.viewMenu.Size = new System.Drawing.Size(70, 20);
            this.viewMenu.Text = "&Affichage";
            // 
            // smnToolBar
            // 
            this.smnToolBar.Checked = true;
            this.smnToolBar.CheckOnClick = true;
            this.smnToolBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smnToolBar.Name = "smnToolBar";
            this.smnToolBar.Size = new System.Drawing.Size(156, 26);
            this.smnToolBar.Text = "&Barre d\'outils";
            this.smnToolBar.Click += new System.EventHandler(this.smnToolBar_Click);
            // 
            // smnStatusBar
            // 
            this.smnStatusBar.Checked = true;
            this.smnStatusBar.CheckOnClick = true;
            this.smnStatusBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.smnStatusBar.Name = "smnStatusBar";
            this.smnStatusBar.Size = new System.Drawing.Size(156, 26);
            this.smnStatusBar.Text = "Barre d\'é&tat";
            this.smnStatusBar.ToolTipText = "View Status Bar";
            this.smnStatusBar.Click += new System.EventHandler(this.smnStatusBar_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(153, 6);
            // 
            // smnReports
            // 
            this.smnReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssmnReportsTAR});
            this.smnReports.Image = ((System.Drawing.Image)(resources.GetObject("smnReports.Image")));
            this.smnReports.Name = "smnReports";
            this.smnReports.Size = new System.Drawing.Size(156, 26);
            this.smnReports.Text = "&Rapports";
            // 
            // photoGpsToolStripMenuItem
            // 
            this.photoGpsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smnViewManage});
            this.photoGpsToolStripMenuItem.Name = "photoGpsToolStripMenuItem";
            this.photoGpsToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.photoGpsToolStripMenuItem.Text = "Photo&Groupe";
            // 
            // smnViewManage
            // 
            this.smnViewManage.Image = ((System.Drawing.Image)(resources.GetObject("smnViewManage.Image")));
            this.smnViewManage.Name = "smnViewManage";
            this.smnViewManage.Size = new System.Drawing.Size(167, 26);
            this.smnViewManage.Text = "Gestion des V&ues";
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smnUserManage,
            this.smnSynchronise});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.toolsMenu.Size = new System.Drawing.Size(50, 20);
            this.toolsMenu.Text = "Outi&ls";
            // 
            // smnUserManage
            // 
            this.smnUserManage.Image = ((System.Drawing.Image)(resources.GetObject("smnUserManage.Image")));
            this.smnUserManage.Name = "smnUserManage";
            this.smnUserManage.Size = new System.Drawing.Size(200, 26);
            this.smnUserManage.Text = "Gestion des &Utilisateurs";
            this.smnUserManage.Click += new System.EventHandler(this.smnUserManage_Click);
            // 
            // smnSynchronise
            // 
            this.smnSynchronise.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssmnExecute,
            this.ssmnSynchroniseGUI});
            this.smnSynchronise.Image = ((System.Drawing.Image)(resources.GetObject("smnSynchronise.Image")));
            this.smnSynchronise.Name = "smnSynchronise";
            this.smnSynchronise.Size = new System.Drawing.Size(200, 26);
            this.smnSynchronise.Text = "Synchronisation";
            // 
            // ssmnExecute
            // 
            this.ssmnExecute.Image = ((System.Drawing.Image)(resources.GetObject("ssmnExecute.Image")));
            this.ssmnExecute.Name = "ssmnExecute";
            this.ssmnExecute.Size = new System.Drawing.Size(268, 26);
            this.ssmnExecute.Text = "Exécuter";
            this.ssmnExecute.Click += new System.EventHandler(this.ssmnExecute_Click);
            // 
            // ssmnSynchroniseGUI
            // 
            this.ssmnSynchroniseGUI.Image = ((System.Drawing.Image)(resources.GetObject("ssmnSynchroniseGUI.Image")));
            this.ssmnSynchroniseGUI.Name = "ssmnSynchroniseGUI";
            this.ssmnSynchroniseGUI.Size = new System.Drawing.Size(268, 26);
            this.ssmnSynchroniseGUI.Text = "Synchronisation Interface Utilisateur";
            this.ssmnSynchroniseGUI.Click += new System.EventHandler(this.ssmnSynchroniseGUI_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smnContent,
            this.toolStripSeparator8,
            this.smnAbout});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.helpMenu.Size = new System.Drawing.Size(43, 20);
            this.helpMenu.Text = "A&ide";
            // 
            // smnContent
            // 
            this.smnContent.Image = ((System.Drawing.Image)(resources.GetObject("smnContent.Image")));
            this.smnContent.Name = "smnContent";
            this.smnContent.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.smnContent.Size = new System.Drawing.Size(166, 22);
            this.smnContent.Text = "Co&ntenu";
            this.smnContent.Click += new System.EventHandler(this.smnContent_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(163, 6);
            // 
            // smnAbout
            // 
            this.smnAbout.Image = ((System.Drawing.Image)(resources.GetObject("smnAbout.Image")));
            this.smnAbout.Name = "smnAbout";
            this.smnAbout.Size = new System.Drawing.Size(166, 22);
            this.smnAbout.Text = "&A Propos ... ...";
            this.smnAbout.Click += new System.EventHandler(this.smnAbout_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbExit,
            this.tlbConnect,
            this.toolStripSeparator1,
            this.tlbPrint,
            this.toolStripSeparator2,
            this.tlbDEntry,
            this.tlbEntryNursery,
            this.tlbExpData,
            this.tlbEssence,
            this.toolStripSeparator4,
            this.tlbReports});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(778, 27);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // tlbExit
            // 
            this.tlbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbExit.Image = ((System.Drawing.Image)(resources.GetObject("tlbExit.Image")));
            this.tlbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbExit.Name = "tlbExit";
            this.tlbExit.Size = new System.Drawing.Size(24, 24);
            this.tlbExit.ToolTipText = "Fermer";
            this.tlbExit.Click += new System.EventHandler(this.tlbExit_Click);
            // 
            // tlbConnect
            // 
            this.tlbConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbConnect.Image = ((System.Drawing.Image)(resources.GetObject("tlbConnect.Image")));
            this.tlbConnect.ImageTransparentColor = System.Drawing.Color.Black;
            this.tlbConnect.Name = "tlbConnect";
            this.tlbConnect.Size = new System.Drawing.Size(24, 24);
            this.tlbConnect.ToolTipText = "Connexion à la Base de données";
            this.tlbConnect.Click += new System.EventHandler(this.tlbConnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tlbPrint
            // 
            this.tlbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbPrint.Image = ((System.Drawing.Image)(resources.GetObject("tlbPrint.Image")));
            this.tlbPrint.ImageTransparentColor = System.Drawing.Color.Black;
            this.tlbPrint.Name = "tlbPrint";
            this.tlbPrint.Size = new System.Drawing.Size(24, 24);
            this.tlbPrint.Text = "Print";
            this.tlbPrint.ToolTipText = "Imprimer";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // tlbDEntry
            // 
            this.tlbDEntry.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbDEntry.Image = ((System.Drawing.Image)(resources.GetObject("tlbDEntry.Image")));
            this.tlbDEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbDEntry.Name = "tlbDEntry";
            this.tlbDEntry.Size = new System.Drawing.Size(24, 24);
            this.tlbDEntry.Text = "Saisie TAR/PR";
            this.tlbDEntry.ToolTipText = "Entrée des données";
            this.tlbDEntry.Click += new System.EventHandler(this.tlbDEntry_Click);
            // 
            // tlbEntryNursery
            // 
            this.tlbEntryNursery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbEntryNursery.Image = ((System.Drawing.Image)(resources.GetObject("tlbEntryNursery.Image")));
            this.tlbEntryNursery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbEntryNursery.Name = "tlbEntryNursery";
            this.tlbEntryNursery.Size = new System.Drawing.Size(24, 24);
            this.tlbEntryNursery.Text = "Saisie des pepinieres";
            this.tlbEntryNursery.ToolTipText = "Saisie des pepinières";
            // 
            // tlbExpData
            // 
            this.tlbExpData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbExpData.Image = ((System.Drawing.Image)(resources.GetObject("tlbExpData.Image")));
            this.tlbExpData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbExpData.Name = "tlbExpData";
            this.tlbExpData.Size = new System.Drawing.Size(24, 24);
            this.tlbExpData.Text = "Nouvelle donnee d\'exploitation";
            this.tlbExpData.ToolTipText = "Nouvelle donnée d\'exploitation";
            // 
            // tlbEssence
            // 
            this.tlbEssence.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbEssence.Image = ((System.Drawing.Image)(resources.GetObject("tlbEssence.Image")));
            this.tlbEssence.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbEssence.Name = "tlbEssence";
            this.tlbEssence.Size = new System.Drawing.Size(24, 24);
            this.tlbEssence.Text = "Nouvelle essence";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // tlbReports
            // 
            this.tlbReports.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlbReports.Image = ((System.Drawing.Image)(resources.GetObject("tlbReports.Image")));
            this.tlbReports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlbReports.Name = "tlbReports";
            this.tlbReports.Size = new System.Drawing.Size(36, 24);
            this.tlbReports.ToolTipText = "Rapports";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.statLabel,
            this.lblInfoForm});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(778, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 17);
            this.lblStatus.Text = "Status";
            // 
            // statLabel
            // 
            this.statLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.statLabel.Name = "statLabel";
            this.statLabel.Size = new System.Drawing.Size(17, 17);
            this.statLabel.Text = "--";
            // 
            // lblInfoForm
            // 
            this.lblInfoForm.ForeColor = System.Drawing.Color.Red;
            this.lblInfoForm.Name = "lblInfoForm";
            this.lblInfoForm.Size = new System.Drawing.Size(22, 17);
            this.lblInfoForm.Text = "el1";
            this.lblInfoForm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ssmnReportsTAR
            // 
            this.ssmnReportsTAR.Name = "ssmnReportsTAR";
            this.ssmnReportsTAR.Size = new System.Drawing.Size(152, 22);
            this.ssmnReportsTAR.Text = "Rapports TAR";
            this.ssmnReportsTAR.Click += new System.EventHandler(this.ssmnReportsTAR_Click);
            // 
            // mdiMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "mdiMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "xEntry_Desktop : Gestion des données EcoMakala";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.mdiMainForm_FormClosed);
            this.Load += new System.EventHandler(this.mdiMainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem smnAbout;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem smnConnect;
        private System.Windows.Forms.ToolStripMenuItem smnExit;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem smnToolBar;
        private System.Windows.Forms.ToolStripMenuItem smnStatusBar;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem smnUserManage;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem smnContent;
        private System.Windows.Forms.ToolStripButton tlbConnect;
        private System.Windows.Forms.ToolStripButton tlbPrint;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem smnReports;
        private System.Windows.Forms.ToolStripMenuItem formStripMenuItem;
        private System.Windows.Forms.ToolStripButton tlbExit;
        private System.Windows.Forms.ToolStripStatusLabel statLabel;
        private System.Windows.Forms.ToolStripSplitButton tlbReports;
        private System.Windows.Forms.ToolStripStatusLabel lblInfoForm;
        private System.Windows.Forms.ToolStripMenuItem smnDEntry;
        private System.Windows.Forms.ToolStripButton tlbDEntry;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tlbEntryNursery;
        private System.Windows.Forms.ToolStripMenuItem smnEntryNursery;
        private System.Windows.Forms.ToolStripMenuItem smnExplData;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tlbExpData;
        private System.Windows.Forms.ToolStripMenuItem smnEssence;
        private System.Windows.Forms.ToolStripButton tlbEssence;
        private System.Windows.Forms.ToolStripMenuItem photoGpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smnViewManage;
        private System.Windows.Forms.ToolStripMenuItem smnDisconnect;
        private System.Windows.Forms.ToolStripMenuItem smnSynchronise;
        private System.Windows.Forms.ToolStripMenuItem ssmnExecute;
        private System.Windows.Forms.ToolStripMenuItem ssmnSynchroniseGUI;
        private System.Windows.Forms.ToolStripMenuItem smn_tar;
        private System.Windows.Forms.ToolStripMenuItem mnuPR;
        private System.Windows.Forms.ToolStripMenuItem identificationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suiviToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ssmnReportsTAR;
    }
}



