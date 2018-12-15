using ManageUtilities;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using Xentry.Data;

namespace Xentry.Desktop
{
    public partial class FormEntryTar : Form
    {
        BindingSource _binsrc = new BindingSource();
        private clstbl_fiche_tar ficheid = new clstbl_fiche_tar();
        private bool blnModifie = false;

        ResourceManager stringManager = null;

        //string Texte;

        MdiMainForm xMainForm = new MdiMainForm();
        
        public MdiMainForm MdiMainForm
        {
            get
            {
                return xMainForm;
            }
            set
            {
                xMainForm = value;
            }
            
        }

        public FormEntryTar()
        {
            InitializeComponent();
            //Initialisation des Resources
            Assembly _assembly = Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);
        }

        private void EntryForm_Load(object sender, EventArgs e)
        {
            //this.Width = 1438;
            //this.Height = 780;
            //this.EntryTabCtrl.Width = 1432;
            //this.EntryTabCtrl.Height = 742;
            //this.CenterToParent();

            this.WindowState = FormWindowState.Maximized;
            txtid.Visible = false;
            EmptyControls();
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
            txtIdAgent.TextAlign = HorizontalAlignment.Center;
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
            catch (ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadDataMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadDataCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors du chargement des données : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadDataMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadDataCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors du chargement des données : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private static void SetBindingControls(Control ctr, string ctr_prop, object objsrce, string obj_prop)
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

        //Permet de lier le BindingSource a la source de donnée comme DataGridView
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

        private static void SetMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
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
            FormEntryTar.SetMembersallcbo(cboSaison, "id_saison", "id_saison");

            cboAsso.DataSource = clsMetier.GetInstance().getAllClstbl_association();
            FormEntryTar.SetMembersallcbo(cboAsso, "association", "association");
          

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
            catch(ArgumentException)
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
                    if (record == 0)
                        throw new XentryDesktopCustomException(stringManager.GetString("StringZeroRecordAffectedMessage", CultureInfo.CurrentUICulture));
                   else
                        MessageBox.Show(stringManager.GetString("StringSuccessSaveMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSuccessSaveCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    DoUpdate();
                }

                RefreshData();
            }
            catch (XentryDesktopCustomException ex)
            {
                Properties.Settings.Default.StringLogFile = ex.Message;
                MessageBox.Show(Properties.Settings.Default.StringLogFile, stringManager.GetString("StringFailSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour  : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailSaveUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        public void DoUpdate()
        {
            int record = ficheid.update((DataRowView)_binsrc.Current);
            if(record == 0)
                throw new XentryDesktopCustomException(stringManager.GetString("StringZeroRecordAffectedMessage", CultureInfo.CurrentUICulture));
            else
                MessageBox.Show(stringManager.GetString("StringSuccessUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSuccessUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void bdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch(ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedRefreshMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailRefreshCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors de l'actualisation : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailRefreshMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailRefreshCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors de l'actualisation : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private void bdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (blnModifie)
                {
                    DialogResult dr = MessageBox.Show(stringManager.GetString("StringPromptDeleteMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringPromptDeleteCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    int record = 0;

                    if (dr == DialogResult.Yes)
                    {
                        record = ficheid.delete((DataRowView)_binsrc.Current);
                        if(record == 0)
                            throw new XentryDesktopCustomException(stringManager.GetString("StringZeroRecordAffectedMessage", CultureInfo.CurrentUICulture));
                        else
                            MessageBox.Show(stringManager.GetString("StringSuccessDeleteMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSuccessDeleteCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                    else
                        MessageBox.Show(stringManager.GetString("StringCancelDeleteMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringCancelDeleteCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }

                RefreshData();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailDeleteMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedDeleteCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la suppression : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (XentryDesktopCustomException ex)
            {
                Properties.Settings.Default.StringLogFile = ex.Message;
                MessageBox.Show(Properties.Settings.Default.StringLogFile, stringManager.GetString("StringFailedDeleteCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la suppression  : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailDeleteMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedDeleteCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la suppressions : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
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
                txtValChefLoc.Text = stringManager.GetString("StringYesValue");
            }
            else
            {
                txtNameChief.Enabled = txtFunction.Enabled = false;
                txtValChefLoc.Text = stringManager.GetString("StringNoValue", CultureInfo.CurrentUICulture);
            }
        }

        private void chkDocProperty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDocProperty.Checked)
            {
                txtTypeDoc.Enabled = true;
                txtValDocPro.Text = stringManager.GetString("StringYesValue", CultureInfo.CurrentUICulture);
                //Tar tempon2 = new Tar();
                //strdocproperty = tempon2._CheckboxStatus(chkDocProperty);
                //txtTypeDoc.Focus();
            }
            if (!chkDocProperty.Checked)
            {
                txtTypeDoc.Enabled = false;
                txtValDocPro.Text = stringManager.GetString("StringNoValue", CultureInfo.CurrentUICulture);
            }
        }

        private void chkExistTrees_CheckedChanged(object sender, EventArgs e)
        {
            if (chkExistTrees.Checked)
            {
                txtNumberArbr.Enabled = true;
                txtValArbreExistant.Text = stringManager.GetString("StringYesValue", CultureInfo.CurrentUICulture);
                //Tar tempon3 = new Tar();
                //strExistTrees = tempon3._CheckboxStatus(chkExistTrees);
                //txtNumberArbr.Focus();
            }
            if (!chkExistTrees.Checked)
            {
                txtNumberArbr.Enabled = false;
                txtValArbreExistant.Text = stringManager.GetString("StringNoValue", CultureInfo.CurrentUICulture);
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
                TextBox rr = r as TextBox;

                if(rr != null)
                {
                    rr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                }
            }
            txtWpt.Text = stringManager.GetString("StringZeroValue", CultureInfo.CurrentUICulture);
            txtAlt.Text = stringManager.GetString("StringZeroValue", CultureInfo.CurrentUICulture);
            txtEpe.Text = stringManager.GetString("StringZeroValue", CultureInfo.CurrentUICulture);
            txtnumPh.Text = stringManager.GetString("StringZeroValue", CultureInfo.CurrentUICulture);
            txtAzimut.Text = stringManager.GetString("StringZeroValue", CultureInfo.CurrentUICulture);
            txtNumberArbr.Text = stringManager.GetString("StringZeroValue", CultureInfo.CurrentUICulture);
            txtArea.Text = stringManager.GetString("StringZeroValue", CultureInfo.CurrentUICulture);
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
        //private void SelectLspName(string IdLsp)
        //{
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
        //}
    
        private void EmptyControls()
        {
            foreach (Control y in this.panel1.Controls)
            {
                TextBox z = y as TextBox;
                CheckBox t = y as CheckBox;

                if(z != null)
                {
                    z.Clear();
                }
                else
                {
                    if(t != null)
                    {
                        t.Checked = false;
                    }
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


        private static void SetContratTAR()
        {
            //Tar n = new Tar();
            //txtNumContr.Text = n.NumeroContratTAR(txtLSP.Text, cboSaison.Text, int.Parse(txtid.Text));
        }

        private static void SetGenerateidTAR()
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
                EmptyControls();
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
            xMainForm.Entry = null;
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
                txtDistanceeucalyptus.Text = stringManager.GetString("StringZeroDotZeroValue", CultureInfo.CurrentUICulture);
                txtDistanceeucalyptus.Enabled = false;
            }
            else
            {
                chkbRiv.Enabled = true;
                chkbSource.Enabled = true;
                txtDistanceeucalyptus.Text = stringManager.GetString("StringZeroDotZeroValue", CultureInfo.CurrentUICulture);
                txtDistanceeucalyptus.Enabled = true;
            }
        }

        private void chkParticiper_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParticiper.Checked)
            {
                txtValParticiper.Text = stringManager.GetString("StringYesValue", CultureInfo.CurrentUICulture);
            }
            else
            {
                txtValParticiper.Text = stringManager.GetString("StringNoValue", CultureInfo.CurrentUICulture);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = stringManager.GetString("StringExcelDocumentFilterValue1", CultureInfo.InvariantCulture);
                sfd.FileName = "export_Tar.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    // ToCsV(dataGridView1, @"c:\export.xls");
                    FormEntryTar.ToCsV(dtgvTAR, sfd.FileName); // Here dataGridview1 is your grid view name
                }

                //  ExportToExcel();
            }
        }


        #region Exportation vers Excel

        private void ExportToExcel()
        {
            // Creating a Excel object. 
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;

            try
            {
                worksheet = workbook.ActiveSheet; // pour que cela marche il faut ajouter la reference Microsoft.CSharp

                worksheet.Name = "ExportedFromDatGrid";

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                //Loop through each row and read value from each column. 
                for (int i = 0; i < dtgvTAR.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dtgvTAR.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dtgvTAR.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dtgvTAR.Rows[i].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }

                //Getting the location and file name of the excel to save from user.

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = stringManager.GetString("StringExcelDocumentFilterValue2", CultureInfo.InvariantCulture);
                    saveDialog.FilterIndex = 2;

                    if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        workbook.SaveAs(saveDialog.FileName);
                        MessageBox.Show(stringManager.GetString("StringSusccessExportExcelMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSusccessExportExcelCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                }
            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedExportExcelMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedExportExcelCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur d'exportation vers Excel : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }

        }

        private static void ToCsV(DataGridView dGV, string filename)
        {
            StringBuilder stOutput = new StringBuilder();
            // Export titles:
            StringBuilder sHeaders = new StringBuilder();

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders.Append(Convert.ToString(dGV.Columns[j].HeaderText, System.Globalization.CultureInfo.InvariantCulture)).Append("\t");
            stOutput.Append(sHeaders).Append("\r\n");
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                StringBuilder stLine = new StringBuilder();
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine.Append(Convert.ToString(dGV.Rows[i].Cells[j].Value, System.Globalization.CultureInfo.InvariantCulture)).Append("\t");
                stOutput.Append(stLine).Append("\r\n");
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput.ToString());
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
            }
        }  

        #endregion

        private void dtgvTAR_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                New();
                txtuuidTar.Focus();
            }
            catch (ArgumentException ex)
            {
                bdSave.Enabled = false;
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors de la création d'un nouvel enregistrement : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }












        //****************************************************************************
    }
}

