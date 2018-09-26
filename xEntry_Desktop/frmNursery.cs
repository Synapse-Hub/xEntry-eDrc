using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using xEntry_Data;

namespace xEntry_Desktop
{
    public partial class frmNursery : Form
    {

        mdiMainForm xMainFrm = new mdiMainForm();

        BindingSource _binsrc = new BindingSource();
        BindingSource _binGermoirsrc = new BindingSource();
        BindingSource _binRepiqsrc = new BindingSource();
        BindingSource binSaison = new BindingSource();
        private clstbl_fiche_suivi_pepi ficheid = new clstbl_fiche_suivi_pepi();
        private clstbl_saison saisonid = new clstbl_saison();
        private clstbl_germoir_fiche_suivi_pepi germoirid = new clstbl_germoir_fiche_suivi_pepi();
        private clstbl_plant_repiq_fiche_suivi_pepi repiquageid = new clstbl_plant_repiq_fiche_suivi_pepi();
        private bool blnModifie = false;


        public mdiMainForm getMdiMainForm()
        {
            return xMainFrm;
        }
        public void setMdiMainform(mdiMainForm xmainfrm)
        {
            xMainFrm = xmainfrm;
        }


        
        public frmNursery()
        {
            InitializeComponent();
        }

        private void frmNursery_Load(object sender, EventArgs e)
        {


            try
            {
                RefreshData();
                dtgvSuiviPepi.DataSource = _binsrc;
                dtgvGermoir.DataSource = _binGermoirsrc;
                dtgvRepiquage.DataSource = _binRepiqsrc;

                // Ramener le combobox a 1
                if (cboSaison.Items.Count > 0)
                    cboSaison.SelectedIndex = 0;

             
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors du chargement", "Erreur de chargement des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }


        public void RefreshData()
        {
            //Chargement de la source des donnes BindingSource) en utilisant un DataTable
            _binsrc.DataSource = clsMetier.GetInstance().getAllClstbl_fiche_suivi_pepi();
            bdNav.BindingSource = _binsrc;

         //   _binGermoirsrc.DataSource = clsMetier.GetInstance().getAllClstbl_germoir_fiche_suivi_pepi_by_uuid(txtuuidSuiviPepi.Text);
          //  bdNav.BindingSource = _binsrc;

          //  _binRepiqsrc.DataSource = clsMetier.GetInstance().getAllClstbl_plant_repiq_fiche_suivi_pepi();
          //  bdNav.BindingSource = _binsrc;

            cboSaison.DataSource = clsMetier.GetInstance().getAllClstbl_saison();
            this.setMembersallcbo(cboSaison, "id_saison", "id_saison");

          if (_binsrc.Count == 0)
            {
                bdSave.Enabled = false;
                bdDelete.Enabled = false;
            }

        }


        public void New()
        {
            ficheid = new clstbl_fiche_suivi_pepi();

            bdSave.Enabled = true;
            blnModifie = false;

            BindingCls();

        }


        private void SetBindingControls(Control ctr, string ctr_prop, object objsrce, string obj_prop)
        {
            ctr.DataBindings.Clear();
            ctr.DataBindings.Add(ctr_prop, objsrce, obj_prop, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void setMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }

        #region _SUIVI PEPINIERE_

        //Permet de lier le BindingSource aux champs du formulaire
        private void BindingCls()
        {

            SetBindingControls(txtuuidSuiviPepi, "Text", ficheid, "Uuid");
            SetBindingControls(txtDeviceId, "Text", ficheid, "Deviceid");
            SetBindingControls(txtDateRecolte, "Text", ficheid, "Date");
            SetBindingControls(cboAgent, "Text", ficheid, "Agent");
            SetBindingControls(cboSaison, "Text", ficheid, "saison");
            SetBindingControls(txtLSP, "Text", ficheid, "association");
            SetBindingControls(txtLSP, "Text", ficheid, "association_autre");
            SetBindingControls(txtnomsite, "Text", ficheid, "nom_site");
            SetBindingControls(txtnumIdSite, "Text", ficheid, "identifiant_pepiniere");
            SetBindingControls(txtRonde, "Text", ficheid, "ronde_suivi_pepiniere");
            SetBindingControls(txtSuperficie2m, "Text", ficheid, "superficie_potentielle_2");
            SetBindingControls(txtSuperficie2point5m, "Text", ficheid, "superficie_potentielle_2_5");
            SetBindingControls(txtSuperficie3m, "Text", ficheid, "superficie_potentielle_3");
            SetBindingControls(txtTassement_sachet, "Text", ficheid, "tassement_sachet");
            SetBindingControls(txtBinage, "Text", ficheid, "binage");
            SetBindingControls(txtClassement_taille, "Text", ficheid, "classement_taille");
            SetBindingControls(txtclassement_espece, "Text", ficheid, "classement_espece");
            SetBindingControls(txtCernage, "Text", ficheid, "cernage");
            SetBindingControls(txtEtetage, "Text", ficheid, "etetage");
            SetBindingControls(txtDateRecolte, "Text", ficheid, "synchronized_on");
           
        }
        private void BindingList()
        {

            SetBindingControls(txtuuidSuiviPepi, "Text", _binsrc, "Uuid");
            SetBindingControls(txtDeviceId, "Text", _binsrc, "Deviceid");
            SetBindingControls(txtDateRecolte, "Text", _binsrc, "Date");
            SetBindingControls(cboAgent, "Text", _binsrc, "Agent");
            SetBindingControls(cboSaison, "Text", _binsrc, "saison");
            SetBindingControls(txtLSP, "Text", _binsrc, "association");
            SetBindingControls(txtLSP, "Text", _binsrc, "association_autre");
            SetBindingControls(txtnomsite, "Text", _binsrc, "nom_site");
            SetBindingControls(txtnumIdSite, "Text", _binsrc, "identifiant_pepiniere");
            SetBindingControls(txtRonde, "Text", _binsrc, "ronde_suivi_pepiniere");
            SetBindingControls(txtSuperficie2m, "Text", _binsrc, "superficie_potentielle_2");
            SetBindingControls(txtSuperficie2point5m, "Text", _binsrc, "superficie_potentielle_2_5");
            SetBindingControls(txtSuperficie3m, "Text", _binsrc, "superficie_potentielle_3");
            SetBindingControls(txtTassement_sachet, "Text", _binsrc, "tassement_sachet");
            SetBindingControls(txtBinage, "Text", _binsrc, "binage");
            SetBindingControls(txtClassement_taille, "Text", _binsrc, "classement_taille");
            SetBindingControls(txtclassement_espece, "Text", _binsrc, "classement_espece");
            SetBindingControls(txtCernage, "Text", _binsrc, "cernage");
            SetBindingControls(txtEtetage, "Text", _binsrc, "etetage");
            SetBindingControls(txtDateRecolte, "Text", _binsrc, "synchronized_on");

        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                BindingList();
                blnModifie = true;
                bdDelete.Enabled = true;
                _binGermoirsrc.DataSource = clsMetier.GetInstance().getAllClstbl_germoir_fiche_suivi_pepi_by_uuid(txtuuidSuiviPepi.Text);
            }
            catch (Exception)
            {
                blnModifie = false;
                bdDelete.Enabled = false;
            }
        }

        #endregion

        #region _SUIVI PEPINIERE - GERMOIR_

        //Permet de lier le BindingSource aux champs du formulaire
        private void BindingClsGermoir()
        {

            SetBindingControls(txtuuidSuiviPepi, "Text", germoirid, "Uuid");
            SetBindingControls(txtGermoirEssence, "Text", germoirid, "germoir_essence");
            SetBindingControls(txtGermoirEssenceAutre, "Text", germoirid, "germoir_essence_autre");
            SetBindingControls(txtProvenance, "Text", germoirid, "provenance");
            SetBindingControls(txtQlteSemis, "Text", germoirid, "qte_semee");
            SetBindingControls(txtdateSemis, "Text", germoirid, "date_semis");
            SetBindingControls(txtDatePremiereLevee, "Text", germoirid, "date_premiere_levee");
            SetBindingControls(txtTypeDeSemis, "Text", germoirid, "type_de_semis");
            SetBindingControls(txtBienPlat, "Text", germoirid, "bien_plat");
            SetBindingControls(txtArosage, "Text", germoirid, "arrosage");
            SetBindingControls(txtDesherbage, "Text", germoirid, "desherbage");
            SetBindingControls(txtQlteSemis, "Text", germoirid, "qualite_semis");
            SetBindingControls(txtDateRecolte, "Text", germoirid, "synchronized_on");

        }
        private void BindingListGermoir()
        {

            SetBindingControls(txtuuidSuiviPepi, "Text", _binGermoirsrc, "Uuid");
            SetBindingControls(txtGermoirEssence, "Text", _binGermoirsrc, "germoir_essence");
            SetBindingControls(txtGermoirEssenceAutre, "Text", _binGermoirsrc, "germoir_essence_autre");
            SetBindingControls(txtProvenance, "Text", _binGermoirsrc, "provenance");
            SetBindingControls(txtQlteSemis, "Text", _binGermoirsrc, "qte_semee");
            SetBindingControls(txtdateSemis, "Text", _binGermoirsrc, "date_semis");
            SetBindingControls(txtDatePremiereLevee, "Text", _binGermoirsrc, "date_premiere_levee");
            SetBindingControls(txtTypeDeSemis, "Text", _binGermoirsrc, "type_de_semis");
            SetBindingControls(txtBienPlat, "Text", _binGermoirsrc, "bien_plat");
            SetBindingControls(txtArosage, "Text", _binGermoirsrc, "arrosage");
            SetBindingControls(txtDesherbage, "Text", _binGermoirsrc, "desherbage");
            SetBindingControls(txtQlteSemis, "Text", _binGermoirsrc, "qualite_semis");
            SetBindingControls(txtDateRecolte, "Text", _binGermoirsrc, "synchronized_on");

        }

        private void dgvGermoir_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                BindingListGermoir();
               // blnModifie = true;
             //   bdDelete.Enabled = true;
            }
            catch (Exception)
            {
              //  blnModifie = false;
             //   bdDelete.Enabled = false;
            }
        }

        #endregion

        private void bdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!blnModifie)
                {
                    int record = ficheid.inserts();
                    MessageBox.Show("Enregistrement éffectué : " + record + " Affecté", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DoUpdate();
                }

                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de la mise à jour, " + ex.Message, "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void DoUpdate()
        {
            int record = ficheid.update((DataRowView)
                _binsrc.Current);
            MessageBox.Show("Modification éffectuée : " + record + " Modifié", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //      _binsrc.Filter = "Uuid LIKE '%" + txtSearch.Text + "%' OR Agent LIKE '%" + txtSearch.Text + "%'";
            }
            catch (Exception) { }
        }

        private void bdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'actualisation, " + ex.Message, "Actualisation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void bdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (blnModifie)
                {
                    DialogResult dr = MessageBox.Show("Voulez-vous supprimer cet enrgistrement ?", "Suppression enregistrement", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    int record = 0;

                    if (dr == DialogResult.Yes)
                    {
                        record = ficheid.delete((DataRowView)_binsrc.Current);
                        MessageBox.Show("Suppression éffectuée : " + record + " Supprimé", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("Aucune suppression éffectuée : " + record + " Supprimé", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de la suppression, " + ex.Message, "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
   



        private void tbIdentification_Click(object sender, EventArgs e)
        {

        }

        private void txtuuidSuiviPepi_TextChanged(object sender, EventArgs e)
        {
         
        }
    }
}
