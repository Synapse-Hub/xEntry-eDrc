namespace xEntry_Desktop
{
    partial class frmReportPR
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
            this.cboAssociation = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboAgent = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboNbrVisite = new System.Windows.Forms.ComboBox();
            this.rdLstPlantation = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rdLstEssence = new System.Windows.Forms.RadioButton();
            this.cboBailleur = new System.Windows.Forms.ComboBox();
            this.rdLstPlanteur = new System.Windows.Forms.RadioButton();
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1286, 73);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboAssociation);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboAgent);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboNbrVisite);
            this.groupBox1.Controls.Add(this.rdLstPlantation);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rdLstEssence);
            this.groupBox1.Controls.Add(this.cboBailleur);
            this.groupBox1.Controls.Add(this.rdLstPlanteur);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboItems);
            this.groupBox1.Controls.Add(this.cboSaison);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1070, 63);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(643, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Association :";
            // 
            // cboAssociation
            // 
            this.cboAssociation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboAssociation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAssociation.DropDownWidth = 150;
            this.cboAssociation.FormattingEnabled = true;
            this.cboAssociation.Location = new System.Drawing.Point(645, 34);
            this.cboAssociation.Name = "cboAssociation";
            this.cboAssociation.Size = new System.Drawing.Size(135, 21);
            this.cboAssociation.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(925, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Agent :";
            // 
            // cboAgent
            // 
            this.cboAgent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboAgent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAgent.DropDownWidth = 150;
            this.cboAgent.FormattingEnabled = true;
            this.cboAgent.Location = new System.Drawing.Point(927, 34);
            this.cboAgent.Name = "cboAgent";
            this.cboAgent.Size = new System.Drawing.Size(135, 21);
            this.cboAgent.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(784, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Nombre des visites :";
            // 
            // cboNbrVisite
            // 
            this.cboNbrVisite.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboNbrVisite.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboNbrVisite.DropDownWidth = 150;
            this.cboNbrVisite.FormattingEnabled = true;
            this.cboNbrVisite.Location = new System.Drawing.Point(786, 34);
            this.cboNbrVisite.Name = "cboNbrVisite";
            this.cboNbrVisite.Size = new System.Drawing.Size(135, 21);
            this.cboNbrVisite.TabIndex = 7;
            // 
            // rdLstPlantation
            // 
            this.rdLstPlantation.AutoSize = true;
            this.rdLstPlantation.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.rdLstPlantation.Location = new System.Drawing.Point(244, 11);
            this.rdLstPlantation.Name = "rdLstPlantation";
            this.rdLstPlantation.Size = new System.Drawing.Size(121, 17);
            this.rdLstPlantation.TabIndex = 2;
            this.rdLstPlantation.TabStop = true;
            this.rdLstPlantation.Text = "Liste des plantations";
            this.rdLstPlantation.UseVisualStyleBackColor = true;
            this.rdLstPlantation.CheckedChanged += new System.EventHandler(this.rdLstPlantation_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(502, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Bailleur de fonds :";
            // 
            // rdLstEssence
            // 
            this.rdLstEssence.AutoSize = true;
            this.rdLstEssence.ForeColor = System.Drawing.Color.SeaGreen;
            this.rdLstEssence.Location = new System.Drawing.Point(124, 11);
            this.rdLstEssence.Name = "rdLstEssence";
            this.rdLstEssence.Size = new System.Drawing.Size(115, 17);
            this.rdLstEssence.TabIndex = 1;
            this.rdLstEssence.TabStop = true;
            this.rdLstEssence.Text = "Liste des essences";
            this.rdLstEssence.UseVisualStyleBackColor = true;
            this.rdLstEssence.CheckedChanged += new System.EventHandler(this.rdLstEssence_CheckedChanged);
            // 
            // cboBailleur
            // 
            this.cboBailleur.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboBailleur.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboBailleur.DropDownWidth = 150;
            this.cboBailleur.FormattingEnabled = true;
            this.cboBailleur.Location = new System.Drawing.Point(504, 34);
            this.cboBailleur.Name = "cboBailleur";
            this.cboBailleur.Size = new System.Drawing.Size(135, 21);
            this.cboBailleur.TabIndex = 5;
            // 
            // rdLstPlanteur
            // 
            this.rdLstPlanteur.AutoSize = true;
            this.rdLstPlanteur.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.rdLstPlanteur.Location = new System.Drawing.Point(6, 11);
            this.rdLstPlanteur.Name = "rdLstPlanteur";
            this.rdLstPlanteur.Size = new System.Drawing.Size(113, 17);
            this.rdLstPlanteur.TabIndex = 0;
            this.rdLstPlanteur.TabStop = true;
            this.rdLstPlanteur.Text = "Liste des planteurs";
            this.rdLstPlanteur.UseVisualStyleBackColor = true;
            this.rdLstPlanteur.CheckedChanged += new System.EventHandler(this.rdLstPlanteur_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Saison :";
            // 
            // cboItems
            // 
            this.cboItems.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboItems.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboItems.DropDownWidth = 250;
            this.cboItems.FormattingEnabled = true;
            this.cboItems.Location = new System.Drawing.Point(162, 34);
            this.cboItems.Name = "cboItems";
            this.cboItems.Size = new System.Drawing.Size(196, 21);
            this.cboItems.TabIndex = 3;
            // 
            // cboSaison
            // 
            this.cboSaison.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSaison.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSaison.DropDownWidth = 150;
            this.cboSaison.FormattingEnabled = true;
            this.cboSaison.Location = new System.Drawing.Point(368, 34);
            this.cboSaison.Name = "cboSaison";
            this.cboSaison.Size = new System.Drawing.Size(130, 21);
            this.cboSaison.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
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
            this.cmdView.Location = new System.Drawing.Point(1084, 26);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(193, 22);
            this.cmdView.TabIndex = 9;
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
            this.crvReport.Location = new System.Drawing.Point(0, 73);
            this.crvReport.Name = "crvReport";
            this.crvReport.Size = new System.Drawing.Size(1286, 482);
            this.crvReport.TabIndex = 1;
            // 
            // frmReportPR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 555);
            this.Controls.Add(this.crvReport);
            this.Controls.Add(this.panel1);
            this.Name = "frmReportPR";
            this.Text = "Rapports pour PR";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBailleur;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSaison;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdLstPlantation;
        private System.Windows.Forms.RadioButton rdLstEssence;
        private System.Windows.Forms.RadioButton rdLstPlanteur;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboAgent;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboNbrVisite;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboAssociation;
    }
}