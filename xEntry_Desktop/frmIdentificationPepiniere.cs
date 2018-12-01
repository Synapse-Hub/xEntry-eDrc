using System;
using System.Data;
using System.Windows.Forms;
using xEntry_Data;
using xEntry_Utilities;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace xEntry_Desktop
{
    public partial class frmIdentificationPepiniere : Form
    {
        private BindingSource bdsrc = new BindingSource();
        private BindingSource grpc_bdsrc = new BindingSource();
        private clstbl_fiche_ident_pepi ficheid = new clstbl_fiche_ident_pepi();
        private clstbl_grp_c_fiche_ident_pepi grp_c_id = new clstbl_grp_c_fiche_ident_pepi();
        private bool blnModifie = false;
        private bool blnModifie1 = false;

        public frmIdentificationPepiniere()
        {
            InitializeComponent();
        }

        private void setMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }

        private void SetBindingControls(Control ctr, string ctr_prop, object objsrce, string obj_prop)
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
            catch { }
        }

        void binding_Format(object sender, ConvertEventArgs e)
        {
            try
            {
                pbPhoto.Tag = null;
                if (e.DesiredType != typeof(System.Drawing.Image) || e.Value.ToString() == e.DesiredType.FullName || e.Value.ToString() == e.DesiredType.Name) return;
                if (e.Value.ToString() == "System.Drawing.Bitmap") return;
                if (e.Value == null || e.Value.ToString() == "")
                {
                    pbPhoto.Tag = "1";
                }
                else
                {
                    string imagestr = e.Value.ToString();
                    e.Value = (clsDoTraitement.GetInstance().LoadImage(e.Value.ToString()));
                }
            }
            catch { }
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

        //Permet de lier le BindingSource a la source de donnee comme DataGridView
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
            try
            {
                RefreshData();
                dgv.DataSource = bdsrc;
                dtgGrpc.DataSource = grpc_bdsrc;
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors du chargement", "Erreur de chargement des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            catch (Exception)
            {
                blnModifie = false;
                bdDelete.Enabled = false;
            }
        }

        private void bdNew_Click(object sender, EventArgs e)
        {
            try
            {
                New();
                txtIdentifiant.Focus();
            }
            catch (Exception)
            {
                bdSave.Enabled = false;
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
            //if(clsMetier.tbPhoto.Count > 0)
            //    clsMetier.tbPhoto.Clear();

            //clsMetier.tbPhoto.Add(clsDoTraitement.GetInstance().ImageToString64(pbPhoto.Image));

            int record = ficheid.update((DataRowView)bdsrc.Current);
            MessageBox.Show("Modification éffectuée : " + record + " Modifié", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DoUpdate_Grp_c()
        {
            int record = grp_c_id.update((DataRowView)grpc_bdsrc.Current);
            MessageBox.Show("Modification éffectuée : " + record + " Modifié", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                bdsrc.Filter = "Uuid LIKE '%" + txtSearch.Text + "%' OR Agent LIKE '%" + txtSearch.Text + "%'";
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
                        record = ficheid.delete((DataRowView)bdsrc.Current);
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

        private void bdLoadPicture_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    //Creation de l'objet permettant d'explorer les fichiers
                    OpenFileDialog open = new OpenFileDialog();

                    //Titre de la fenetre qui sera ouverte lors de la selection du fichier
                    open.Title = "Sélection d'une photo";

                    /*On specifie le fichier qui devront uniquement etre affiche ou accepte par defaut
                    comme il s'agit d'une image on vas seulement prendre des formats image
                    */
                    open.Filter = "PNG File|*.png|JPG File|*.jpg|TIFF File|*.tif|BitMap Image|*.bmp";

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
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur de chargement de la photo + " + ex.Message, "Sélection photo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void txtAgent_TextChanged(object sender, EventArgs e)
        {

        }

        private void btmParcourir_Click(object sender, EventArgs e)
        {
            try
            {
                //Creation de l'objet permettant d'explorer les fichiers
                OpenFileDialog open = new OpenFileDialog();

                //Titre de la fenetre qui sera ouverte lors de la selection du fichier
                open.Title = "Sélection d'une photo";

                /*On specifie le fichier qui devront uniquement etre affiche ou accepte par defaut
                comme il s'agit d'une image on vas seulement prendre des formats image
                */
                open.Filter = "PNG File|*.png|JPG File|*.jpg|TIFF File|*.tif|BitMap Image|*.bmp";

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
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement de la photo + " + ex.Message, "Sélection photo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveGrpc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!blnModifie1)
                {
                    int record = grp_c_id.inserts();
                    MessageBox.Show("Enregistrement éffectué : " + record + " Affecté", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DoUpdate_Grp_c();
                }

                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de la mise à jour, " + ex.Message, "Mise à jour", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            catch (Exception)
            {
                blnModifie1 = false;
              //  bdDelete.Enabled = false;
            }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }  






    }
}