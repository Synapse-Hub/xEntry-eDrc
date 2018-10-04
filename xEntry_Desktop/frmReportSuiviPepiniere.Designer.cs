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
            this.cmdView = new System.Windows.Forms.Button();
            this.crvReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboQteSemee = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboLieuProvenance = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboAgent = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboItems = new System.Windows.Forms.ComboBox();
            this.cboSaison = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPlancheRepiquage = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1289, 70);
            this.panel1.TabIndex = 0;
            // 
            // cmdView
            // 
            this.cmdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdView.BackColor = System.Drawing.Color.SeaShell;
            this.cmdView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdView.ForeColor = System.Drawing.Color.DarkRed;
            this.cmdView.Location = new System.Drawing.Point(1083, 24);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(196, 22);
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
            this.crvReport.Location = new System.Drawing.Point(0, 70);
            this.crvReport.Name = "crvReport";
            this.crvReport.Size = new System.Drawing.Size(1289, 485);
            this.crvReport.TabIndex = 1;
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
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1070, 58);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(624, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Qte semée :";
            // 
            // cboQteSemee
            // 
            this.cboQteSemee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboQteSemee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboQteSemee.DropDownWidth = 150;
            this.cboQteSemee.FormattingEnabled = true;
            this.cboQteSemee.Location = new System.Drawing.Point(626, 29);
            this.cboQteSemee.Name = "cboQteSemee";
            this.cboQteSemee.Size = new System.Drawing.Size(135, 21);
            this.cboQteSemee.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(765, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Lieu de provenance :";
            // 
            // cboLieuProvenance
            // 
            this.cboLieuProvenance.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboLieuProvenance.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboLieuProvenance.DropDownWidth = 150;
            this.cboLieuProvenance.FormattingEnabled = true;
            this.cboLieuProvenance.Location = new System.Drawing.Point(767, 29);
            this.cboLieuProvenance.Name = "cboLieuProvenance";
            this.cboLieuProvenance.Size = new System.Drawing.Size(144, 21);
            this.cboLieuProvenance.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(474, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Agent :";
            // 
            // cboAgent
            // 
            this.cboAgent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboAgent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAgent.DropDownWidth = 150;
            this.cboAgent.FormattingEnabled = true;
            this.cboAgent.Location = new System.Drawing.Point(476, 29);
            this.cboAgent.Name = "cboAgent";
            this.cboAgent.Size = new System.Drawing.Size(144, 21);
            this.cboAgent.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Saison :";
            // 
            // cboItems
            // 
            this.cboItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboItems.DropDownWidth = 400;
            this.cboItems.FormattingEnabled = true;
            this.cboItems.Location = new System.Drawing.Point(6, 29);
            this.cboItems.Name = "cboItems";
            this.cboItems.Size = new System.Drawing.Size(327, 21);
            this.cboItems.TabIndex = 0;
            this.cboItems.SelectedIndexChanged += new System.EventHandler(this.cboItems_SelectedIndexChanged);
            // 
            // cboSaison
            // 
            this.cboSaison.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSaison.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSaison.DropDownWidth = 150;
            this.cboSaison.FormattingEnabled = true;
            this.cboSaison.Location = new System.Drawing.Point(340, 29);
            this.cboSaison.Name = "cboSaison";
            this.cboSaison.Size = new System.Drawing.Size(130, 21);
            this.cboSaison.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selection élément pour rapport :";
            // 
            // cboPlancheRepiquage
            // 
            this.cboPlancheRepiquage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPlancheRepiquage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPlancheRepiquage.DropDownWidth = 150;
            this.cboPlancheRepiquage.FormattingEnabled = true;
            this.cboPlancheRepiquage.Location = new System.Drawing.Point(918, 29);
            this.cboPlancheRepiquage.Name = "cboPlancheRepiquage";
            this.cboPlancheRepiquage.Size = new System.Drawing.Size(144, 21);
            this.cboPlancheRepiquage.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(916, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Planche repiquage :";
            // 
            // frmReportSuiviPepiniere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 555);
            this.Controls.Add(this.crvReport);
            this.Controls.Add(this.panel1);
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