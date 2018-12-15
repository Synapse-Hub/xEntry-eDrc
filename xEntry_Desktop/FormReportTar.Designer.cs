namespace Xentry.Desktop
{
    partial class FormReportTar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "conn")]
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)")]
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTerritoire = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSaison = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboItems = new System.Windows.Forms.ComboBox();
            this.cmdView = new System.Windows.Forms.Button();
            this.crvReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Beige;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.cboTerritoire);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboSaison);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboItems);
            this.panel1.Controls.Add(this.cmdView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1289, 37);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(800, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Territoire :";
            // 
            // cboTerritoire
            // 
            this.cboTerritoire.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboTerritoire.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboTerritoire.DropDownWidth = 300;
            this.cboTerritoire.FormattingEnabled = true;
            this.cboTerritoire.Location = new System.Drawing.Point(855, 8);
            this.cboTerritoire.Name = "cboTerritoire";
            this.cboTerritoire.Size = new System.Drawing.Size(199, 21);
            this.cboTerritoire.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(552, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Saison :";
            // 
            // cboSaison
            // 
            this.cboSaison.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSaison.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSaison.DropDownWidth = 300;
            this.cboSaison.FormattingEnabled = true;
            this.cboSaison.Location = new System.Drawing.Point(597, 8);
            this.cboSaison.Name = "cboSaison";
            this.cboSaison.Size = new System.Drawing.Size(199, 21);
            this.cboSaison.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selection élément pour rapport :";
            // 
            // cboItems
            // 
            this.cboItems.DropDownWidth = 400;
            this.cboItems.FormattingEnabled = true;
            this.cboItems.Location = new System.Drawing.Point(163, 8);
            this.cboItems.Name = "cboItems";
            this.cboItems.Size = new System.Drawing.Size(357, 21);
            this.cboItems.TabIndex = 0;
            // 
            // cmdView
            // 
            this.cmdView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdView.BackColor = System.Drawing.Color.SeaShell;
            this.cmdView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdView.ForeColor = System.Drawing.Color.DarkRed;
            this.cmdView.Location = new System.Drawing.Point(1082, 8);
            this.cmdView.Name = "cmdView";
            this.cmdView.Size = new System.Drawing.Size(196, 22);
            this.cmdView.TabIndex = 3;
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
            this.crvReport.Location = new System.Drawing.Point(0, 37);
            this.crvReport.Name = "crvReport";
            this.crvReport.Size = new System.Drawing.Size(1289, 518);
            this.crvReport.TabIndex = 1;
            // 
            // FormReportTar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 555);
            this.Controls.Add(this.crvReport);
            this.Controls.Add(this.panel1);
            this.Name = "FormReportTar";
            this.Text = "Rapport pour TAR";
            this.Load += new System.EventHandler(this.frmReportTAR_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvReport;
        private System.Windows.Forms.Button cmdView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboItems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTerritoire;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSaison;
    }
}