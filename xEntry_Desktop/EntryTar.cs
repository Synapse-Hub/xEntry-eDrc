using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xEntry_Data;



namespace xEntry_Desktop
{
    public partial class EntryTar : Form
    {
        SqlCommand xCmd;
        SqlConnection xConn;
        SqlCommand yCmd;
        
        public string myPrValue;

        BindingSource _binsrc = new BindingSource();
        BindingSource binSaison = new BindingSource();
        private clstbl_fiche_tar ficheid = new clstbl_fiche_tar();
        private clstbl_saison saisonid = new clstbl_saison();
        private bool blnModifie = false;

        string Texte;

        mdiMainForm xMainForm = new mdiMainForm();

        
        public mdiMainForm getMdiMainForm()
        {
            return xMainForm;
        }
        public void setMdiMainForm(mdiMainForm xmainF)
        {
            xMainForm = xmainF;
        }

        public EntryTar()
        {
            InitializeComponent();
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            this.Width = 1438;
            this.Height = 780;
            this.EntryTabCtrl.Width = 1432;
            this.EntryTabCtrl.Height = 742;
            this.CenterToParent();
            txtid.Visible = false;
            _EmptyControls();
           // FillComboAgent();
            FillComboSaison();
            txtIdAgent.Clear();
        //    txtIdAsso.Clear();
            txtLSP.Clear();

            txtValArbreExistant.Clear();
            txtValBois.Clear();
            txtValChefLoc.Clear();
            txtValDocPro.Clear();
            txtValMakala.Clear();
            txtValParticiper.Clear();
            txtvalPerchette.Clear();
            txtvalPlanche.Clear();
            txtValRiviere.Clear();
            txtValEucal.Clear();
            txtValStick.Clear();
            txtValEucal.Clear();
            

            _DefaultValue();
            txtIdAgent.Clear();
            //<test 
            txtuuidTar.Clear();
            txtNumContr.Clear();
            ///test>
            txtIdAgent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            bdNav.Visible = true;
          

            try
            {
                RefreshData();
                dtgvTAR.DataSource = _binsrc;

                // Ramener le combobox a 1
                if (cboSaison.Items.Count > 0)
                    cboSaison.SelectedIndex = 0;

                if (cboAsso.Items.Count > 0)
                    cboAsso.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors du chargement", "Erreur de chargement des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

 
        }

        private void SetBindingControls(Control ctr, string ctr_prop, object objsrce, string obj_prop)
        {
            ctr.DataBindings.Clear();
            ctr.DataBindings.Add(ctr_prop, objsrce, obj_prop, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        //Permet de lier le BindingSource aux champs du formulaire
        private void BindingCls()
        {
 
            SetBindingControls(txtuuidTar, "Text", ficheid, "Uuid");
            SetBindingControls(txtDeviceId, "Text", ficheid, "Deviceid");
            // SetBindingControls(txtDate, "Text", ficheid, "Date");
            SetBindingControls(cboAgent, "Text", ficheid, "Agent");
            SetBindingControls(cboSaison, "Text", ficheid, "saison");
            SetBindingControls(txtLSP, "Text", ficheid, "association");
            SetBindingControls(txtLSP, "Text", ficheid, "association_autre");
            // SetBindingControls(txtDateIdentification, "Text", ficheid, "bailleur");
            //  SetBindingControls(txtAgent, "Text", ficheid, "bailleur_autre");
            //  SetBindingControls(txtDateSynchronise, "Text", ficheid, "n_plantation");
            SetBindingControls(txtValParticiper, "Text", ficheid, "deja_participe");
            //  SetBindingControls(txtIDPeripherique, "Text", ficheid, "n_plantations");
            SetBindingControls(txtName, "Text", ficheid, "nom");
            SetBindingControls(txtPostnom, "Text", ficheid, "postnom");
            // SetBindingControls(txtP, "Text", ficheid, "prenom");
            SetBindingControls(cboSexe, "Text", ficheid, "sexes");
            SetBindingControls(txtPlace, "Text", ficheid, "nom_lieu_plantation");
            SetBindingControls(txtVillage, "Text", ficheid, "village");
            SetBindingControls(txtLocalite, "Text", ficheid, "localite");
            SetBindingControls(txtTerritoire, "Text", ficheid, "territoire");
            SetBindingControls(txtChefferie, "Text", ficheid, "chefferie");
            SetBindingControls(txtGroupe, "Text", ficheid, "groupement");
            SetBindingControls(cboTypeId, "Text", ficheid, "type_id");
            SetBindingControls(cboTypeId, "Text", ficheid, "type_id_autre");
            //  SetBindingControls(txtDateSynchronise, "Text", ficheid, "nombre_id");
            //   SetBindingControls(txtIdentifiant, "Text", ficheid, "emplacement");
            SetBindingControls(txtEssPrinc, "Text", ficheid, "essence_principale");
            SetBindingControls(txtEssSec, "Text", ficheid, "essence_principale_autre");
            SetBindingControls(txtArea, "Text", ficheid, "superficie_totale");
            SetBindingControls(txtObjective, "Text", ficheid, "objectifs_planteur");
            SetBindingControls(txtAutresObjectifs, "Text", ficheid, "objectifs_planteur_autre");
            SetBindingControls(txtPrevUse, "Text", ficheid, "utilisation_precedente");
            //    SetBindingControls(txtDateIdentification, "Text", ficheid, "autre_precedente_preciser");
            SetBindingControls(txtDepuis, "Text", ficheid, "utilisation_precedente_depuis");
            SetBindingControls(txtValArbreExistant, "Text", ficheid, "arbres_existants");
            SetBindingControls(txtNumberArbr, "Text", ficheid, "ombre_arbres");
            SetBindingControls(txtSituation, "Text", ficheid, "situation");
            SetBindingControls(txtPente, "Text", ficheid, "pente");
            SetBindingControls(txtSol, "Text", ficheid, "sol");
            SetBindingControls(txtValEucal, "Text", ficheid, "eucalyptus");
            SetBindingControls(txtValRiviere, "Text", ficheid, "point_deau_a_proximite");
            SetBindingControls(txtDistanceeucalyptus, "Text", ficheid, "env_point_deau_a_proximite");
            SetBindingControls(txtValChefLoc, "Text", ficheid, "chef_de_localite");
            SetBindingControls(txtNameChief, "Text", ficheid, "chef_nom");
            // SetBindingControls(txtIDPeripherique, "Text", ficheid, "chef_postnom");
            // SetBindingControls(txtDateIdentification, "Text", ficheid, "chef_prenom");
            //   SetBindingControls(txtAgent, "Text", ficheid, "autre");
            SetBindingControls(txtFonctionAutreChef, "Text", ficheid, "autre_fonction");
            SetBindingControls(txtNameAutreChef, "Text", ficheid, "autre_nom");
            //   SetBindingControls(txtIDPeripherique, "Text", ficheid, "autre_postnom");
            //     SetBindingControls(txtDateIdentification, "Text", ficheid, "autre_prenom");
            SetBindingControls(txtValDocPro, "Text", ficheid, "document_de_propriete");
            SetBindingControls(txtTypeDoc, "Text", ficheid, "preciser_document");
            //    SetBindingControls(txtDateIdentification, "Text", ficheid, "autre_document");
            SetBindingControls(txtObservations, "Text", ficheid, "observations");
            SetBindingControls(dtpEntryDate, "Text", ficheid, "synchronized_on");

        }

        //Permet de lier le BindingSource a la source de donnee comme DataGridView
        private void BindingList()
        {
            SetBindingControls(txtuuidTar, "Text", _binsrc, "Uuid");
            SetBindingControls(txtDeviceId, "Text", _binsrc, "Deviceid");
            // SetBindingControls(txtDate, "Text", _binsrc, "Date");
            SetBindingControls(cboAgent, "Text", _binsrc, "Agent");
            SetBindingControls(cboSaison, "Text", _binsrc, "saison");
            SetBindingControls(txtLSP, "Text", _binsrc, "association");
            SetBindingControls(txtLSP, "Text", _binsrc, "association_autre");
            // SetBindingControls(txtDateIdentification, "Text", _binsrc, "bailleur");
            //  SetBindingControls(txtAgent, "Text", _binsrc, "bailleur_autre");
            //  SetBindingControls(txtDateSynchronise, "Text", _binsrc, "n_plantation");
            SetBindingControls(txtValParticiper, "Text", _binsrc, "deja_participe");
            //  SetBindingControls(txtIDPeripherique, "Text", _binsrc, "n_plantations");
            SetBindingControls(txtName, "Text", _binsrc, "nom");
            SetBindingControls(txtPostnom, "Text", _binsrc, "postnom");
            // SetBindingControls(txtP, "Text", _binsrc, "prenom");
            SetBindingControls(cboSexe, "Text", _binsrc, "sexes");
            SetBindingControls(txtPlace, "Text", _binsrc, "nom_lieu_plantation");
            SetBindingControls(txtVillage, "Text", _binsrc, "village");
            SetBindingControls(txtLocalite, "Text", _binsrc, "localite");
            SetBindingControls(txtTerritoire, "Text", _binsrc, "territoire");
            SetBindingControls(txtChefferie, "Text", _binsrc, "chefferie");
            SetBindingControls(txtGroupe, "Text", _binsrc, "groupement");
            SetBindingControls(cboTypeId, "Text", _binsrc, "type_id");
            SetBindingControls(cboTypeId, "Text", _binsrc, "type_id_autre");
            //  SetBindingControls(txtDateSynchronise, "Text", _binsrc, "nombre_id");
            //   SetBindingControls(txtIdentifiant, "Text", _binsrc, "emplacement");
             SetBindingControls(txtEssPrinc, "Text", _binsrc, "essence_principale");
             SetBindingControls(txtEssSec, "Text", _binsrc, "essence_principale_autre");
            SetBindingControls(txtArea, "Text", _binsrc, "superficie_totale");
            SetBindingControls(txtObjective, "Text", _binsrc, "objectifs_planteur");
            SetBindingControls(txtAutresObjectifs, "Text", _binsrc, "objectifs_planteur_autre");
            SetBindingControls(txtPrevUse, "Text", _binsrc, "utilisation_precedente");
            //    SetBindingControls(txtDateIdentification, "Text", _binsrc, "autre_precedente_preciser");
             SetBindingControls(txtDepuis, "Text", _binsrc, "utilisation_precedente_depuis");
            SetBindingControls(txtValArbreExistant, "Text", _binsrc, "arbres_existants");
            SetBindingControls(txtNumberArbr, "Text", _binsrc, "ombre_arbres");
            SetBindingControls(txtSituation, "Text", _binsrc, "situation");
            SetBindingControls(txtPente, "Text", _binsrc, "pente");
            SetBindingControls(txtSol, "Text", _binsrc, "sol");
            SetBindingControls(txtValEucal, "Text", _binsrc, "eucalyptus");
            SetBindingControls(txtValRiviere, "Text", _binsrc, "point_deau_a_proximite");
            SetBindingControls(txtDistanceeucalyptus, "Text", _binsrc, "env_point_deau_a_proximite");
            SetBindingControls(txtValChefLoc, "Text", _binsrc, "chef_de_localite");
            SetBindingControls(txtNameChief, "Text", _binsrc, "chef_nom");
            // SetBindingControls(txtIDPeripherique, "Text", _binsrc, "chef_postnom");
            // SetBindingControls(txtDateIdentification, "Text", _binsrc, "chef_prenom");
            //   SetBindingControls(txtAgent, "Text", _binsrc, "autre");
            SetBindingControls(txtFonctionAutreChef, "Text", _binsrc, "autre_fonction");
            SetBindingControls(txtNameAutreChef, "Text", _binsrc, "autre_nom");
            //   SetBindingControls(txtIDPeripherique, "Text", _binsrc, "autre_postnom");
            //     SetBindingControls(txtDateIdentification, "Text", _binsrc, "autre_prenom");
            SetBindingControls(txtValDocPro, "Text", _binsrc, "document_de_propriete");
           SetBindingControls(txtTypeDoc, "Text", _binsrc, "preciser_document");
            //    SetBindingControls(txtDateIdentification, "Text", _binsrc, "autre_document");
           SetBindingControls(txtObservations, "Text", _binsrc, "observations");
            SetBindingControls(dtpEntryDate, "Text", _binsrc, "synchronized_on");
        }

        private void setMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }


        public void RefreshData()
        {
            //Chargement de la source des donnes BindingSource) en utilisant un DataTable
            _binsrc.DataSource = clsMetier.GetInstance().getAllClstbl_fiche_tar();
            bdNav.BindingSource = _binsrc;

            cboSaison.DataSource = clsMetier.GetInstance().getAllClstbl_saison();
            this.setMembersallcbo(cboSaison, "id_saison", "id_saison");

            cboAsso.DataSource = clsMetier.GetInstance().getAllClstbl_association();
            this.setMembersallcbo(cboAsso, "association", "association");
          

            if (_binsrc.Count == 0)
            {
                bdSave.Enabled = false;
                bdDelete.Enabled = false;
            }

        }

        public void New()
        {
            ficheid = new clstbl_fiche_tar();

            bdSave.Enabled = true;
            blnModifie = false;

            BindingCls();

            //Permet de recuperer la first valeur du Combobox
            /*if (cboAgent.Items.Count > 0) 
                cboAgent.SelectedIndex = 0;*/
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                BindingList();
                blnModifie = true;
                bdDelete.Enabled = true;
            }
            catch (Exception)
            {
                blnModifie = false;
                bdDelete.Enabled = false;
            }
        }

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
   

    //***********************************************************************************************************************************************************************************************************************


        private void FillComboSaison()
        {
            // mettre ici le code pour boucler et lire les infos des associations
            cboSaison.SelectedIndex = -1;

        }


        private void _LockedTextBox()
        {
            txtWpt.Enabled = txtLat.Enabled = txtLongi.Enabled = txtAlt.Enabled = txtEpe.Enabled = false;
            txtnumPh.Enabled = txtLatPh.Enabled = txtLongPh.Enabled = txtAzimut.Enabled = false;
            txtNameChief.Enabled = txtFunction.Enabled = false;
            txtTypeDoc.Enabled = false;
            txtNumberArbr.Enabled = false;
        }

        private void chkGis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGis.Checked)
            {
                txtWpt.Enabled = txtLat.Enabled = txtLongi.Enabled = txtAlt.Enabled = txtEpe.Enabled = true;
                txtWpt.Clear();
                txtWpt.Focus();
            }
            if (!chkGis.Checked)
            {
                txtWpt.Enabled = txtLat.Enabled = txtLongi.Enabled = txtAlt.Enabled = txtEpe.Enabled = false;
            }
        }

        private void chkPhotoGis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPhotoGis.Checked)
            {
                txtnumPh.Enabled = txtLatPh.Enabled = txtLongPh.Enabled = txtAzimut.Enabled = true;
                txtnumPh.Clear();
                txtnumPh.Focus();
            }
            if (!chkPhotoGis.Checked)
            {
                txtnumPh.Enabled = txtLatPh.Enabled = txtLongPh.Enabled = txtAzimut.Enabled = false;
            }
        }

        private void chkChief_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChief.Checked)
            {
                txtNameChief.Enabled = txtFunction.Enabled = true;
                txtValChefLoc.Text = "OUI";

            }
            if (!chkChief.Checked)
            {
                txtNameChief.Enabled = txtFunction.Enabled = false;
                txtValChefLoc.Text = "NON";
            }
        }

        private void chkDocProperty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDocProperty.Checked)
            {
                txtTypeDoc.Enabled = true;
                txtValDocPro.Text = "OUI";
                //Tar tempon2 = new Tar();
                //strdocproperty = tempon2._CheckboxStatus(chkDocProperty);
                //txtTypeDoc.Focus();
            }
            if (!chkDocProperty.Checked)
            {
                txtTypeDoc.Enabled = false;
                txtValDocPro.Text = "NON";
            }
        }

        private void chkExistTrees_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExistTrees.Checked)
            {
                txtNumberArbr.Enabled = true;
                txtValArbreExistant.Text = "OUI";
                //Tar tempon3 = new Tar();
                //strExistTrees = tempon3._CheckboxStatus(chkExistTrees);
                //txtNumberArbr.Focus();
            }
            if (!chkExistTrees.Checked)
            {
                txtNumberArbr.Enabled = false;
                txtValArbreExistant.Text = "NON";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _DefaultValue()
        {
            foreach (Control r in this.Controls)
            {
                if (r is TextBox)
                {
                    TextBox rr = (TextBox)r;
                    rr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                }
            }
            txtWpt.Text = "0";
            txtAlt.Text = "0";
            txtEpe.Text = "0";
            txtnumPh.Text = "0";
            txtAzimut.Text = "0";
            txtNumberArbr.Text = "0";
            txtArea.Text = "0";
            cboAgent.SelectedIndex = -1;
            txtName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
          

        
        }

        private void cboAgent_SelectedValueChanged(object sender, EventArgs e)
        {
          
        }

        #region _SELECT_NAME_LSP_
        private void _SelectLspName(string IdLsp)
        {
            //try
            //{
            //    if (xConn.State.ToString().Equals("Closed")) xConn.Open();
            //    xCmd = xConn.CreateCommand();
            //    xCmd.CommandText = "Select * from ASSO where id_asso='" + IdLsp + "'";
            //    SqlDataReader rd = null;
            //    rd = xCmd.ExecuteReader();
            //    while (rd.Read())
            //    {
            //        txtLSP.Text = rd["name_asso"].ToString();
            //    }
            //}
            //catch (SqlException ex)
            //{
            //    MessageBox.Show(this, ex.Message, "error");
            //}
            //xConn.Close();

         //   _NumeroAutoTar();
        }
    
        private void _EmptyControls()
        {
            foreach (Control y in this.panel1.Controls)
            {
                if (y is TextBox)
                {
                    TextBox z = (TextBox)y;
                    z.Clear();
                }
                if (y is CheckBox)
                {
                    CheckBox t = (CheckBox)y;
                    t.Checked = false;
                }
            }
            _LockedTextBox();
            _DefaultValue();
        }



        //#region Numero_Automatique_TAR_et_PR
        //private void _NumeroAutoTar()
        //{
        //    int numero;

        //    try
        //    {
        //        if (xConn.State.ToString().Equals("Closed")) xConn.Open();
        //        xCmd = xConn.CreateCommand();
        //        xCmd.CommandText = "Select max(idt) as maxid from TAR where id_asso='" + txtIdAsso.Text.Trim() + "'";

        //        SqlDataReader xdr = null;
        //        xdr = xCmd.ExecuteReader();
        //        while (xdr.Read())
        //        {
        //            if (xdr["maxid"].ToString() == null)
        //            {
        //                numero = 0;
        //                int x = numero++;
        //                txtid.Text = x.ToString();
        //            }
        //            if (xdr != null)
        //            {
        //                string t = xdr["maxid"].ToString();
        //                if (t.Equals(""))
        //                {
        //                    numero = 0;
        //                    int x = numero + 1;
        //                    //MessageBox.Show(this, x.ToString());
        //                    txtid.Text = x.ToString();
        //                    setGenerateidTAR();
        //                    setContratTAR();
        //                }
        //                if (!t.Equals(""))
        //                {
        //                    int x = int.Parse(t);
        //                    txtid.Text = (x + 1).ToString();
        //                    setGenerateidTAR();
        //                    setContratTAR();
        //                }
        //                txtName.Focus();
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show(this, ex.Message, "Error");
        //    }
        //    xConn.Close();
        //}
        //#endregion


        private void setContratTAR()
        {
            //Tar n = new Tar();
            //txtNumContr.Text = n.NumeroContratTAR(txtLSP.Text, cboSaison.Text, int.Parse(txtid.Text));
        }

        private void setGenerateidTAR()
        {
            //Tar m = new Tar();
            //txtIdTar.Text = m.NumeroIdTAR(txtLSP.Text, cboSaison.Text, int.Parse(txtid.Text));
        }

        private void EntryTabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (EntryTabCtrl.SelectedTab.Name.Equals("tbEntry1"))
            {
                this.Width = 1026;
                this.Height = 652;
                this.EntryTabCtrl.Width = 1017;
                this.EntryTabCtrl.Height = 625;
                this.CenterToParent();
            }
            if (EntryTabCtrl.SelectedTab.Name.Equals("tbEntry2"))
            {
                this.Width = 1257;
                this.Height = 652;
                this.EntryTabCtrl.Width = 1255;
                this.EntryTabCtrl.Height = 625;
                this.CenterToParent();
                _EmptyControls();
            }
            if (EntryTabCtrl.SelectedTab.Name.Equals("tbEntry3"))
            {
                this.Width = 1026;
                this.Height = 652;
                this.EntryTabCtrl.Width = 1017;
                this.EntryTabCtrl.Height = 625;
                this.CenterToParent();
            }
        }
        

      
        #endregion

        private void EntryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            xMainForm.entry = null;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chkEucal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEucal.Checked)
            {
                chkbRiv.Enabled = false;
                chkbSource.Enabled = false;
                txtDistanceeucalyptus.Text = "0.0";
                txtDistanceeucalyptus.Enabled = false;
            }
            else
            {
                chkbRiv.Enabled = true;
                chkbSource.Enabled = true;
                txtDistanceeucalyptus.Text = "0.0";
                txtDistanceeucalyptus.Enabled = true;
            }
        }

        private void chkParticiper_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParticiper.Checked)
            {
                txtValParticiper.Text = "OUI";
            }
            else
            {
                txtValParticiper.Text = "NON";
            }
        }

       

       


      

       


        //****************************************************************************
    }
}

