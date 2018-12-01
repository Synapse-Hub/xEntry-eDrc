namespace xEntry_Desktop
{
    partial class frmReportSuiviPepiniere
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboQteSemee = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboPlancheRepiquage = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboLieuProvenance = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboAgent = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboItems = new System.Windows.Forms.ComboBox();
            this.cboSaison = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdView = new System.Windows.Forms.Button();
            this.crvReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.cmdView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(2578, 133);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboQteSemee);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboPlancheRepiquage);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboLieuProvenance);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboAgent);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboItems);
            this.groupBox1.Controls.Add(this.cboSaison);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox1.Size = new System.Drawing.Size(2140, 112);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1248, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Qte semée :";
            // 
            // cboQteSemee
            // 
            this.cboQteSemee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboQteSemee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboQteSemee.DropDownWidth = 150;
            this.cboQteSemee.FormattingEnabled = true;
            this.cboQteSemee.Location = new System.Drawing.Point(1252, 56);
            this.cboQteSemee.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cboQteSemee.Name = "cboQteSemee";
            this.cboQteSemee.Size = new System.Drawing.Size(266, 33);
            this.cboQteSemee.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1832, 23);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(204, 25);
            this.label5.TabIndex = 10;
            this.label5.Text = "Planche repiquage :";
            // 
            // cboPlancheRepiquage
            // 
            this.cboPlancheRepiquage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPlancheRepiquage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPlancheRepiquage.DropDownWidth = 150;
            this.cboPlancheRepiquage.FormattingEnabled = true;
            this.cboPlancheRepiquage.Location = new System.Drawing.Point(1836, 56);
            this.cboPlancheRepiquage.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cboPlancheRepiquage.Name = "cboPlancheRepiquage";
            this.cboPlancheRepiquage.Size = new System.Drawing.Size(284, 33);
            this.cboPlancheRepiquage.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1530, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Lieu de provenance :";
            // 
            // cboLieuProvenance
            // 
            this.cboLieuProvenance.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboLieuProvenance.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLieuProvenance.DropDownWidth = 150;
            this.cboLieuProvenance.FormattingEnabled = true;
            this.cboLieuProvenance.Location = new System.Drawing.Point(1534, 56);
            this.cboLieuProvenance.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cboLieuProvenance.Name = "cboLieuProvenance";
            this.cboLieuProvenance.Size = new System.Drawing.Size(284, 33);
            this.cboLieuProvenance.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(948, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Agent :";
            // 
            // cboAgent
            // 
            this.cboAgent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboAgent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAgent.DropDownWidth = 150;
            this.cboAgent.FormattingEnabled = true;
            this.cboAgent.Location = new System.Drawing.Point(952, 56);
            this.cboAgent.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cboAgent.Name = "cboAgent";
            this.cboAgent.Size = new System.Drawing.Size(284, 33);
            this.cboAgent.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(674, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Saison :";
            // 
            // cboItems
            // 
            this.cboItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboItems.DropDownWidth = 400;
            this.cboItems.FormattingEnabled = true;
            this.cboItems.Location = new System.Drawing.Point(12, 56);
            this.cboItems.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cboItems.Name = "cboItems";
            this.cboItems.Size = new System.Drawing.Size(650, 33);
            this.cboItems.TabIndex = 0;
            this.cboItems.SelectedIndexChanged += new System.EventHandler(this.cboItems_SelectedIndexChanged);
            // 
            // cboSaison
            // 
            this.cboSaison.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSaison.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSaison.DropDownWidth = 150;
            this.cboSaison.FormattingEnabled = true;
            this.cboSaison.Location = new System.Drawing.Point(680, 56);
            this.cboSaison.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cboSaison.Name = "cboSaison";
            this.cboSaison.Size = new System.Drawing.Size(256, 33);
            this.cboSaison.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selection élément pour rapport :";
            // 
            // cmdView
            // 
            this.cmdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdView.BackColor = System.Drawing.Color.SeaShell;
            this.cmdView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdView.ForeColor = System.Drawing.Color.DarkRed;
            this.cmdView.Location = new System.Drawing.Point(2166, 46);
            this.cmdView.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(394, 42);
            this.cmdView.TabIndex = 6;
            this.cmdView.Text = "Afficher";
            this.cmdView.UseVisualStyleBackColor = false;
            this.cmdView.Click += new System.EventHandler(this.cmdView_Click);
            // 
            // crvReport
            // 
            this.crvReport.ActiveViewIndex = -1;
            this.crvReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvReport.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvReport.Location = new System.Drawing.Point(0, 133);
            this.crvReport.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.crvReport.Name = "crvReport";
            this.crvReport.Size = new System.Drawing.Size(2578, 934);
            this.crvReport.TabIndex = 1;
            this.crvReport.ToolPanelWidth = 400;
            this.crvReport.Load += new System.EventHandler(this.crvReport_Load);
            // 
            // frmReportSuiviPepiniere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2578, 1067);
            this.Controls.Add(this.crvReport);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "frmReportSuiviPepiniere";
            this.Text = "Rapport pour Suivie pépinière";
            this.Load += new System.EventHandler(this.frmReportTAR_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReport;
        private System.Windows.Forms.Button cmdView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboQteSemee;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboLieuProvenance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboAgent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboItems;
        private System.Windows.Forms.ComboBox cboSaison;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboPlancheRepiquage;
    }
}