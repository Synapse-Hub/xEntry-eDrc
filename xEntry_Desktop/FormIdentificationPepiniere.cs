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
using Xentry.Utilities;

namespace Xentry.Desktop
{
    public partial class FormIdentificationPepiniere : Form
    {
        private BindingSource bdsrc = new BindingSource();
        private BindingSource grpc_bdsrc = new BindingSource();
        private clstbl_fiche_ident_pepi ficheid = new clstbl_fiche_ident_pepi();
        private clstbl_grp_c_fiche_ident_pepi grp_c_id = new clstbl_grp_c_fiche_ident_pepi();
        private bool blnModifie = false;
        private bool blnModifie1 = false;

        //Item RessourceManager 
        ResourceManager stringManager = null;

        public FormIdentificationPepiniere()
        {
            InitializeComponent();
            //Initialisation des Resources
            Assembly _assembly = Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);
        }

        private static void SetMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }

        private static void SetBindingControls(Control ctr, string ctr_prop, object objsrce, string obj_prop)
        {
            ctr.DataBindings.Clear();
            ctr.DataBindings.Add(ctr_prop, objsrce, obj_prop, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void Bs_Parse(object sender, ConvertEventArgs e)
        {
            try
            {
                if (e.Value == null) e = null;
                else e.Value = (clsDoTraitement.GetInstance().ImageToString64(pbPhoto.Image));
            }
            catch(NullReferenceException ex)
            {
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de conversion de la photo : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        void binding_Format(object sender, ConvertEventArgs e)
        {
            try
            {
                pbPhoto.Tag = null;
                if (e.DesiredType != typeof(System.Drawing.Image) || e.Value.ToString() == e.DesiredType.FullName || e.Value.ToString() == e.DesiredType.Name) return;
                if (e.Value.ToString() == "System.Drawing.Bitmap") return;
                if (string.IsNullOrEmpty(e.Value.ToString()))
                {
                    pbPhoto.Tag = "1";
                }
                else
                {
                    e.Value = (clsDoTraitement.GetInstance().LoadImage(e.Value.ToString()));
                }
            }
            catch(NullReferenceException ex)
            {
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors du binding de la photo : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (InvalidOperationException ex)
            {
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors du binding de la photo : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private void bingImg(PictureBox pb, Object src, string ctrProp, string obj)
        {
            pb.DataBindings.Clear();
            Binding binding = new Binding(ctrProp, src, obj, true, DataSourceUpdateMode.OnPropertyChanged);
            binding.Parse += new ConvertEventHandler(Bs_Parse);
            binding.Format += new ConvertEventHandler(binding_Format);
            pb.DataBindings.Add(binding);
        }

        //Permet de lier le BindingSource aux champs du formulaire
        private void BindingCls()
        {
            SetBindingControls(txtIdentifiant, "Text", ficheid, "uuid");
            SetBindingControls(txtIDPeripherique, "Text", ficheid, "deviceid");
            SetBindingControls(txtDateIdentification, "Text", ficheid, "date");
            SetBindingControls(txtAgent, "Text", ficheid, "agent");
            SetBindingControls(txtSaison, "Text", ficheid, "saison");
            SetBindingControls(txtAssociation, "Text", ficheid, "association");
            SetBindingControls(txtAssociationAutre, "Text", ficheid, "association_autre");
            SetBindingControls(txtBailleur, "Text", ficheid, "bailleur");
            SetBindingControls(txtBailleurAutre, "Text", ficheid, "bailleur_autre");
            SetBindingControls(txtId, "Text", ficheid, "id");
            SetBindingControls(txtNomSite, "Text", ficheid, "nom_site");
            SetBindingControls(txtVillage, "Text", ficheid, "village");
            SetBindingControls(txtLocalite, "Text", ficheid, "localite");
            SetBindingControls(txtTerritoire, "Text", ficheid, "territoire");
            SetBindingControls(txtChefferie, "Text", ficheid, "chefferie");
            SetBindingControls(txtGroupement, "Text", ficheid, "groupement");
            SetBindingControls(txtDateInstal, "Text", ficheid, "date_installation_pepiniere");
            SetBindingControls(txtGrpC, "Text", ficheid, "grp_c");
            SetBindingControls(txtnbPepinieriste, "Text", ficheid, "nb_pepinieristes");
            SetBindingControls(txtnbPepinieristeForme, "Text", ficheid, "nb_pepinieristes_formes");
            SetBindingControls(txtContrat, "Text", ficheid, "contrat");
            SetBindingControls(txtcmbPepinieristes, "Text", ficheid, "combien_pepinieristes");
            SetBindingControls(txtLocalisation, "Text", ficheid, "localisation");
            SetBindingControls(pbPhoto, "Image", ficheid, "photo");
            SetBindingControls(txtObservations, "Text", ficheid, "observations");
            SetBindingControls(txtDateSynchronise, "Text", ficheid, "Synchronized_on");
        }

        private void Binding_grp_c_Cls()
        {
            SetBindingControls(txtCount, "Text", grp_c_id, "count");
            SetBindingControls(txtDimensionA, "Text", grp_c_id, "dimension_planche_a");
            SetBindingControls(txtDimensionB, "Text", grp_c_id, "dimension_planche_b");
            SetBindingControls(txtCapacitePlanche, "Text", grp_c_id, "capacite_planche");
            SetBindingControls(txtCapaciteTotalePlanche, "Text", grp_c_id, "capacite_totale_planche");
        }

        private void Binding_grp_c_List()
        {
            SetBindingControls(txtCount, "Text", grpc_bdsrc, "count");
            SetBindingControls(txtDimensionA, "Text", grpc_bdsrc, "dimension_planche_a");
            SetBindingControls(txtDimensionB, "Text", grpc_bdsrc, "dimension_planche_b");
            SetBindingControls(txtCapacitePlanche, "Text", grpc_bdsrc, "capacite_planche");
            SetBindingControls(txtCapaciteTotalePlanche, "Text", grpc_bdsrc, "capacite_totale_planche");
        }

        //Permet de lier le BindingSource a la source de donnée comme DataGridView
        private void BindingList()
        {
            SetBindingControls(txtIdentifiant, "Text", bdsrc, "uuid");
            SetBindingControls(txtIDPeripherique, "Text", bdsrc, "deviceid");
            SetBindingControls(txtDateIdentification, "Text", bdsrc, "date");
            SetBindingControls(txtAgent, "Text", bdsrc, "agent");
            SetBindingControls(txtSaison, "Text", bdsrc, "saison");
            SetBindingControls(txtAssociation, "Text", bdsrc, "association");
            SetBindingControls(txtAssociationAutre, "Text", bdsrc, "association_autre");
            SetBindingControls(txtBailleur, "Text", bdsrc, "bailleur");
            SetBindingControls(txtBailleurAutre, "Text", bdsrc, "bailleur_autre");
            SetBindingControls(txtId, "Text", bdsrc, "id");
            SetBindingControls(txtNomSite, "Text", bdsrc, "nom_site");
            SetBindingControls(txtVillage, "Text", bdsrc, "village");
            SetBindingControls(txtLocalite, "Text", bdsrc, "localite");
            SetBindingControls(txtTerritoire, "Text", bdsrc, "territoire");
            SetBindingControls(txtChefferie, "Text", bdsrc, "chefferie");
            SetBindingControls(txtGroupement, "Text", bdsrc, "groupement");
            SetBindingControls(txtDateInstal, "Text", bdsrc, "date_installation_pepiniere");
            SetBindingControls(txtGrpC, "Text", bdsrc, "grp_c");
            SetBindingControls(txtnbPepinieriste, "Text", bdsrc, "nb_pepinieristes");
            SetBindingControls(txtnbPepinieristeForme, "Text", bdsrc, "nb_pepinieristes_formes");
            SetBindingControls(txtContrat, "Text", bdsrc, "contrat");
            SetBindingControls(txtcmbPepinieristes, "Text", bdsrc, "combien_pepinieristes");
            SetBindingControls(txtLocalisation, "Text", bdsrc, "localisation");
            SetBindingControls(pbPhoto, "Image", bdsrc, "photo");
            SetBindingControls(txtObservations, "Text", bdsrc, "observations");
            SetBindingControls(txtDateSynchronise, "Text", bdsrc, "synchronized_on");
        }

        private void frmIdentificationPepiniere_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            try
            {
                RefreshData();
                dgv.DataSource = bdsrc;
                dtgGrpc.DataSource = grpc_bdsrc;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadDataMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadDataCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors du chargement : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadDataMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadDataCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors du chargement : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        public void RefreshData()
        {
            //Chargement de la source des donnes BindingSource) en utilisant un DataTable
            bdsrc.DataSource = clsMetier.GetInstance().getAllClstbl_fiche_ident_pepi();
            grpc_bdsrc.DataSource = clsMetier.GetInstance().getAllClstbl_grp_c_fiche_ident_pepi();

            bdNav.BindingSource = bdsrc;
            bdGrpc.BindingSource = grpc_bdsrc;

            if (bdsrc.Count == 0)
            {
                bdSave.Enabled = false;
                bdDelete.Enabled = false;
            }    
        }

        public void New()
        {
            ficheid = new clstbl_fiche_ident_pepi();

            bdSave.Enabled = true;
            blnModifie = false;

            BindingCls();
        }

        private void dgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                BindingList();
                SetBindingControls(pbPhoto, "Image", bdsrc, "photo");
                blnModifie = true;
                bdDelete.Enabled = true;
            }
            catch(ArgumentException ex) 
            {
                blnModifie = false;
                bdDelete.Enabled = false;
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de sélection dans le DataGrid : " + this.Name + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private void bdNew_Click(object sender, EventArgs e)
        {
            try
            {
                New();
                txtIdentifiant.Focus();
            }
            catch(ArgumentException ex) 
            {
                bdSave.Enabled = false;
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors de la création d'un nouvel enregistrement : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private void bdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!blnModifie)
                {
                  //  ficheid.Photo = (clsDoTraitement.GetInstance().ImageToString64(pbPhoto.Image));
                    int record = ficheid.inserts();
                    if (record == 0)
                        throw new XentryDesktopCustomException(stringManager.GetString("StringZeroRecordAffectedMessage", CultureInfo.CurrentUICulture));
                    else
                        MessageBox.Show(stringManager.GetString("StringSuccessSaveMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSuccessSaveCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    DoUpdate();
                }

                RefreshData();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedSaveUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (XentryDesktopCustomException ex)
            {
                Properties.Settings.Default.StringLogFile = ex.Message;
                MessageBox.Show(Properties.Settings.Default.StringLogFile, stringManager.GetString("StringFailedSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour  : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch(System.Data.SqlTypes.SqlTypeException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedSaveUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedSaveUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        public void DoUpdate()
        {
            int record = ficheid.update((DataRowView)bdsrc.Current);
            if (record == 0)
                throw new XentryDesktopCustomException(stringManager.GetString("StringZeroRecordAffectedMessage", CultureInfo.CurrentUICulture));
            else
                MessageBox.Show(stringManager.GetString("StringSuccessUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSuccessUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        public void DoUpdateGrpC()
        {
            int record = grp_c_id.update((DataRowView)grpc_bdsrc.Current);
            if (record == 0)
                throw new XentryDesktopCustomException(stringManager.GetString("StringZeroRecordAffectedMessage", CultureInfo.CurrentUICulture));
            else
                MessageBox.Show(stringManager.GetString("StringSuccessUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSuccessUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bdsrc.Filter = "Uuid LIKE '%" + txtSearch.Text + "%' OR Agent LIKE '%" + txtSearch.Text + "%'";
            }
            catch(ArgumentException ex)
            {
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de recherche dans un TextBox avec DataTable : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private void bdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedRefreshMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedRefreshCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur lors de l'actualisation : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedRefreshMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedRefreshCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
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
                        record = ficheid.delete((DataRowView)bdsrc.Current);
                        if (record == 0)
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
                MessageBox.Show(stringManager.GetString("StringFailedDeleteMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedDeleteCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la suppression : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
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
                MessageBox.Show(stringManager.GetString("StringFailedDeleteMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedDeleteCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la suppression : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private void bdLoadPicture_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    //Creation de l'objet permettant d'explorer les fichiers
                    using (OpenFileDialog open = new OpenFileDialog())
                    {
                        //Titre de la fenetre qui sera ouverte lors de la selection du fichier
                        open.Title = stringManager.GetString("StringLoadPictureCaption1", CultureInfo.InvariantCulture);

                        /*On specifie le fichier qui devront uniquement etre affiche ou accepte par defaut
                        comme il s'agit d'une image on vas seulement prendre des formats image
                        */
                        open.Filter = stringManager.GetString("StringPictureFilter1", CultureInfo.InvariantCulture);

                        //Ouverture de l'explorateur des fichiers
                        open.ShowDialog();

                        //Recuperation du chemin d'acces du fichier s'il est necessaire
                        string path = open.FileName;

                        //On verifie que le fichier qui a ete selectionne existe reelement
                        if (System.IO.File.Exists(path))
                        {
                            /*Si le fichier existe, on do une action

                             Ici on affecte l'image selectionne dans le xontrol PictureBox
                            */
                            pbPhoto.Load(path);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(stringManager.GetString("StringFailedLoadPictureMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadPictureCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de chargement de la photo : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                    ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
                }
                catch (IOException ex)
                {
                    MessageBox.Show(stringManager.GetString("StringFailedLoadPictureMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadPictureCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de chargement de la photo : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                    ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
                }
            }
        }

        private void txtAgent_TextChanged(object sender, EventArgs e)
        {

        }

        private void btmParcourir_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = null;

            try
            {
                //Creation de l'objet permettant d'explorer les fichiers
                open = new OpenFileDialog();

                //Titre de la fenetre qui sera ouverte lors de la selection du fichier
                open.Title =  stringManager.GetString("StringLoadPictureCaption1",CultureInfo.InvariantCulture);

                /*On specifie le fichier qui devront uniquement etre affiche ou accepte par defaut
                comme il s'agit d'une image on vas seulement prendre des formats image
                */
                open.Filter = stringManager.GetString("StringPictureFilter1", CultureInfo.InvariantCulture);

                //Ouverture de l'explorateur des fichiers
                open.ShowDialog();

                //Recuperation du chemin d'acces du fichier s'il est necessaire
                string path = open.FileName;

                //On verifie que le fichier qui a ete selectionne existe reelement
                if (System.IO.File.Exists(path))
                {
                    /*Si le fichier existe, on do une action

                     Ici on affecte l'image selectionne dans le xontrol PictureBox
                    */
                    pbPhoto.Load(path);
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadPictureMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadPictureCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de chargement de la photo : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (IOException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadPictureMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadPictureCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de chargement de la photo : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            finally
            {
                open.Dispose();
            }
        }

        private void btnSaveGrpc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!blnModifie1)
                {
                    int record = grp_c_id.inserts();
                    if (record == 0)
                        throw new XentryDesktopCustomException(stringManager.GetString("StringZeroRecordAffectedMessage", CultureInfo.CurrentUICulture));
                    else
                        MessageBox.Show(stringManager.GetString("StringSuccessSaveMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSuccessSaveCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    DoUpdateGrpC();
                }

                RefreshData();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedSaveUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (XentryDesktopCustomException ex)
            {
                Properties.Settings.Default.StringLogFile = ex.Message;
                MessageBox.Show(Properties.Settings.Default.StringLogFile, stringManager.GetString("StringFailedSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour  : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedSaveUpdateMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedSaveUpdateCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de la mise à jour : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }  
        }

        private void btnUpdGrpc_Click(object sender, EventArgs e)
        {

        }

        private void dtgvGermoir_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                Binding_grp_c_List();       
                blnModifie1 = true;
               // bdDelete.Enabled = true;

            }
            catch (ArgumentException ex)
            {
                blnModifie1 = false;
                //  bdDelete.Enabled = false;
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de sélection dans le DataGrid : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
        }

        private static void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText, System.Globalization.CultureInfo.InvariantCulture) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value, System.Globalization.CultureInfo.InvariantCulture) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);

            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
            }
        }  
    }
}