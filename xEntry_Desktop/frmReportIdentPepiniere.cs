using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace xEntry_Desktop
{
    public partial class frmReportIdentPepiniere : Form
    {
        IDbConnection conn = null;
        public frmReportIdentPepiniere()
        {
            InitializeComponent();
        }

        private string SetQueryExecute(ComboBox cboItems)
        {
            string query = null;
            
            switch(cboItems.SelectedIndex)
            {
                case 0:
                    //Liste des pépinières identifiées
                    query = string.Format(@"select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.saison as 'Saison',tbl_fiche_ident_pepi.association as 'Association',tbl_fiche_ident_pepi.bailleur as 'Bailleur',
                    tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',
                    tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité planche',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',observations as 'Observations'
                    from tbl_fiche_ident_pepi
                    inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid");
                    break;
            }
            return query;
        }

        private void LoadReport(string query, int cboIndex)
        {
            //Initialisation de la chaine de connexion
            conn = new SqlConnection(xEntry_Data.Properties.Settings.Default.strChaineConnexion);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            using (IDbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = query;
                SqlDataAdapter adapter = new SqlDataAdapter((SqlCommand)cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset, "lstTable");

                switch (cboIndex)
                {
                    case 0:
                        //Liste des pépinières identifiées
                        Rapports.LstIdentPepiniere1 rpt = new Rapports.LstIdentPepiniere1();
                        rpt.SetDataSource(dataset.Tables["lstTable"]);
                        crvReport.ReportSource = rpt;
                        crvReport.Refresh();
                        dataset.Dispose();
                        break;
                }
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            try
            {
                LoadReport(SetQueryExecute(cboItems), cboItems.SelectedIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de chargement du rapport, " + ex.Message, "Chargement rapport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                conn.Close();
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
