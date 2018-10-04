using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using xEntry_Data;

namespace xEntry_Desktop
{
    public partial class frmReportSuiviPepiniere : Form
    {
        IDbConnection conn = null;
        public frmReportSuiviPepiniere()
        {
            InitializeComponent();
        }

        private string SetQueryExecute(ComboBox cboItems)
        {
            string query = null;
            
            switch(cboItems.SelectedIndex)
            {
                case 0:
                    //Liste des pépinières par agent et par saison
                    query = string.Format(@"select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.deviceid as 'ID péripherique',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.saison as 'Saison',tbl_grp_c_fiche_ident_pepi.count as 'Comptage',tbl_fiche_ident_pepi.association as 'Association',tbl_fiche_ident_pepi.bailleur as 'Bailleur',
                    tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',
                    tbl_fiche_ident_pepi.nb_pepinieristes_formes as 'Nbr Pépinieristes formés',tbl_fiche_ident_pepi.contrat as 'Contrat',tbl_fiche_ident_pepi.combien_pepinieristes as 'Total pépinieristes',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité totale planche',
                    tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_fiche_suivi_pepi.superficie_potentielle_2 as 'Superficie potentielle 2',tbl_fiche_suivi_pepi.superficie_potentielle_3 as 'Superficie potentielle 3',tbl_fiche_suivi_pepi.superficie_potentielle_2_5 as 'Superficie potentielle 2-5',
                    tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Autre essence germoir',tbl_plant_repiq_fiche_suivi_pepi.date_repiquage as 'Date repiqauge',tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_plant_repiq_fiche_suivi_pepi.plantules_encore_repiques 'Planture encore repiqué',tbl_fiche_ident_pepi.observations as 'Observations'
                    from tbl_fiche_ident_pepi
                    inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
                    inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
                    inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
                    inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
                    where tbl_fiche_ident_pepi.agent='{0}' and tbl_fiche_ident_pepi.saison='{1}'", cboAgent.SelectedValue, cboSaison.SelectedValue);
                    break;
                case 1:
                    //Liste des pépinières par Qte semée et par lieu de provenance
                    query = string.Format(@"select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.agent as 'Nom agent',tbl_fiche_ident_pepi.nom_site as 'Nom site',
                    tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',tbl_fiche_ident_pepi.localisation as 'Géolocalisation',
                    tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Essence germoir autre',
                    tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_fiche_ident_pepi.observations as 'Observations',tbl_germoir_fiche_suivi_pepi.qte_semee as 'Qte Semée',tbl_germoir_fiche_suivi_pepi.provenance as 'Provenance'
                    from tbl_fiche_ident_pepi
                    inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
                    inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
                    inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
                    inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
                    where tbl_germoir_fiche_suivi_pepi.provenance='{0}'", cboLieuProvenance.SelectedValue);
                    break;
                case 2:
                    //Liste des pépinières par Qte semée et par planche repiquage
                    query = string.Format(@"select tbl_fiche_ident_pepi.uuid as 'Identifiant unique',tbl_fiche_ident_pepi.id as 'Numéro pépinière',tbl_fiche_ident_pepi.nom_site as 'Nom site',tbl_fiche_ident_pepi.village as 'Village',tbl_fiche_ident_pepi.localite as 'Localité',
                    tbl_fiche_ident_pepi.territoire as 'Territoire',tbl_fiche_ident_pepi.chefferie as 'Chefferie',tbl_fiche_ident_pepi.groupement as 'Groupement',tbl_fiche_ident_pepi.date_installation_pepiniere as 'Date installation',tbl_fiche_ident_pepi.nb_pepinieristes as 'Nbr Pepinieristes',
                    tbl_fiche_ident_pepi.localisation as 'Géolocalisation',tbl_grp_c_fiche_ident_pepi.capacite_totale_planche as 'Capacité totale planche',tbl_fiche_suivi_pepi.superficie_potentielle_note as 'Superficie notée',tbl_germoir_fiche_suivi_pepi.germoir_essence as 'Essence germoir',
                    tbl_germoir_fiche_suivi_pepi.germoir_essence_autre as 'Autre essence germoir',tbl_plant_repiq_fiche_suivi_pepi.date_repiquage as 'Date repiqauge',tbl_plant_repiq_fiche_suivi_pepi.qte_observee as 'Qte observée',tbl_plant_repiq_fiche_suivi_pepi.plantules_encore_repiques 'Planture encore repiqué',
                    tbl_plant_repiq_fiche_suivi_pepi.plantules_deja_evacues as 'Planture déjà evacué',tbl_fiche_ident_pepi.observations as 'Observations',tbl_germoir_fiche_suivi_pepi.qte_semee as 'Qte Semée'
                    from tbl_fiche_ident_pepi
                    inner join tbl_grp_c_fiche_ident_pepi on tbl_fiche_ident_pepi.uuid=tbl_grp_c_fiche_ident_pepi.uuid 
                    inner join tbl_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_fiche_suivi_pepi.uuid
                    inner join tbl_germoir_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_germoir_fiche_suivi_pepi.uuid
                    inner join tbl_plant_repiq_fiche_suivi_pepi on tbl_fiche_ident_pepi.uuid=tbl_plant_repiq_fiche_suivi_pepi.uuid
                    where tbl_plant_repiq_fiche_suivi_pepi.planches_repiquage_essence='{0}' or tbl_plant_repiq_fiche_suivi_pepi.planches_repiquage_essence_autre='{1}'", cboPlancheRepiquage.SelectedValue, cboPlancheRepiquage.SelectedValue);
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
                IDbDataAdapter adapter = new SqlDataAdapter((SqlCommand)cmd);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);

                switch (cboIndex)
                {
                    case 0:
                        //Liste des pépinières par agent et par saison
                        Rapports.LstSuiviPepi_Agent_Saison rpt = new Rapports.LstSuiviPepi_Agent_Saison();
                        rpt.SetDataSource(dataset.Tables["lstTable"]);
                        crvReport.ReportSource = rpt;
                        crvReport.Refresh();
                        dataset.Dispose();
                        break;
                    case 1:
                        //Liste des pépinières par Qte semée et par lieu de provenance
                        Rapports.LstSuiviPepi_QteSeme_Provenance rpt1 = new Rapports.LstSuiviPepi_QteSeme_Provenance();
                        rpt1.SetDataSource(dataset.Tables["lstTable"]);
                        crvReport.ReportSource = rpt1;
                        crvReport.Refresh();
                        dataset.Dispose();
                        break;
                    case 2:
                        //Liste des pépinières par Qte semée et par planche repiquage
                        Rapports.LstSuiviPepiQteSemee_PlancheRepiq rpt3 = new Rapports.LstSuiviPepiQteSemee_PlancheRepiq();
                        rpt3.SetDataSource(dataset.Tables["lstTable"]);
                        crvReport.ReportSource = rpt3;
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

        private void setMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
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
                this.setMembersallcbo(cboSaison, "Saison", "Saison");
                cboAgent.DataSource = clsMetier.GetInstance().getAllClstbl_agent();
                this.setMembersallcbo(cboAgent, "Agent", "Id_agent");
                cboQteSemee.DataSource = clsMetier.GetInstance().getAllClstbl_Qte_Semee_germoir_fiche_suivi_pepi();
                this.setMembersallcbo(cboQteSemee, "qte_semee", "qte_semee");
                cboLieuProvenance.DataSource = clsMetier.GetInstance().getAllClstbl_Lieu_Provenance_germoir_fiche_suivi_pepi();
                this.setMembersallcbo(cboLieuProvenance, "provenance", "provenance");
                cboPlancheRepiquage.DataSource = clsMetier.GetInstance().getAllClstbl_planche_repiquage_fiche_suivi_pepi();
                this.setMembersallcbo(cboPlancheRepiquage, "planches_repiquage", "planches_repiquage");

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
            catch (Exception ex)
            {
                MessageBox.Show("Echec de chargement des listes déroulantes, " + ex.Message, "Chargement listes déroulantes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
