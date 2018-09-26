using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using xEntry_Data;
using xEntry_Utilities;

namespace xEntry_Desktop
{
    public partial class PlantationReliaser : Form
    {
        private BindingSource bdsrc = new BindingSource();

        private clstbl_fiche_pr ficheid = new clstbl_fiche_pr();
        private bool blnModifie = false;
        public PlantationReliaser()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void SetBindingControls(Control ctr, string ctr_prop, object objsrce, string obj_prop)
        {
            ctr.DataBindings.Clear();
            ctr.DataBindings.Add(ctr_prop, objsrce, obj_prop, true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public void RefreshData()
        {
            //Chargement de la source des donnes BindingSource) en utilisant un DataTable
            bdsrc.DataSource = clsMetier.GetInstance().getAllClstbl_fiche_pr();

            bdNav.BindingSource = bdsrc;

            if (bdsrc.Count == 0)
            {
                btnAdd.Enabled = false;
                btnDel.Enabled = false;
            }
        }
      


        //pour la photo1

        private void Bs_Parse(object sender, ConvertEventArgs e)
        {
            try
            {
                if (e.Value == null) e = null;
                else e.Value = (clsDoTraitement.GetInstance().ImageToString64(imageFiche.Image));
            }
            catch { }
        }

       //LA FONCTION POUR LE FORMATAGE DE LA PHOTO
        //void binding_Format(object sender, ConvertEventArgs e)
        //{
        //    try
        //    {
        //        pbPhoto.Tag = null;
        //        if (e.DesiredType != typeof(System.Drawing.Image) || e.Value.ToString() == e.DesiredType.FullName || e.Value.ToString() == e.DesiredType.Name) return;
        //        if (e.Value.ToString() == "System.Drawing.Bitmap") return;
        //        if (e.Value == null || e.Value.ToString() == "")
        //        {
        //            pbPhoto.Tag = "1";
        //        }
        //        else
        //        {
        //            string imagestr = e.Value.ToString();
        //            e.Value = (clsDoTraitement.GetInstance().LoadImage(e.Value.ToString()));
        //        }
        //    }
        //    catch { }
        //}

        //private void bingImg(PictureBox pb, Object src, string ctrProp, string obj)
        //{
        //    pb.DataBindings.Clear();
        //    Binding binding = new Binding(ctrProp, src, obj, true, DataSourceUpdateMode.OnPropertyChanged);
        //    binding.Parse += new ConvertEventHandler(Bs_Parse);
        //    binding.Format += new ConvertEventHandler(binding_Format);
        //    pb.DataBindings.Add(binding);
        //}



        private void BindingCls()
        {
            SetBindingControls(txtid, "Text", ficheid, "id");
            SetBindingControls(txtuuid, "Text", ficheid, "uuid");
            SetBindingControls(txtdececeid, "Text", ficheid, "deviceid");
            SetBindingControls(txtdate, "Text", ficheid, "date");
            SetBindingControls(txtnomAgent, "Text", ficheid, "nom_agent");
            SetBindingControls(txtsaison, "Text", ficheid, "saison");
            SetBindingControls(txtassociation, "Text", ficheid, "association");
            SetBindingControls(txtassociationAutre, "Text", ficheid, "association_autre");
            SetBindingControls(txtbailleur, "Text", ficheid, "bailleur");
            SetBindingControls(txtbailleurAutre, "Text", ficheid, "bailleur_autre");
            SetBindingControls(txtnvisite, "Text", ficheid, "n_visite");
            SetBindingControls(txtCverification, "Text", ficheid, "contreverification");
            SetBindingControls(txtNvisite2, "Text", ficheid, "n_visite_2");
            SetBindingControls(txtnVisite3, "Text", ficheid, "n_visite_3");
            SetBindingControls(txtNvisiteCalculate, "Text", ficheid, "visite_calculate");
            SetBindingControls(txtNplantation, "Text", ficheid, "n_plantation");
            SetBindingControls(txtNbloc, "Text", ficheid, "n_bloc");
            SetBindingControls(txtnomPlanteur, "Text", ficheid, "nom_planteur");
            SetBindingControls(txtnom, "Text", ficheid, "nom");
            SetBindingControls(txtpostnom, "Text", ficheid, "postnom");
            SetBindingControls(txtprenom, "Text", ficheid, "prenom");
            SetBindingControls(txtSexe, "Text", ficheid, "sexes");

            SetBindingControls(txtplanteurPresent, "Text", ficheid, "planteur_present");
            SetBindingControls(txtChangement, "Text", ficheid, "changement_surface");
            SetBindingControls(imageFiche, "Image", ficheid, "photo_fiche");
            SetBindingControls(txtPreiodeDebut, "Text", ficheid, "periode_debut");
            SetBindingControls(txtDebutAnnee, "Text", ficheid, "periode_debut_annee");
            SetBindingControls(txtPreiodeFin, "Text", ficheid, "periode_fin");
            SetBindingControls(periodeFinAnnee, "Text", ficheid, "periode_fin_annee");
            SetBindingControls(txtEssencePrincale, "Text", ficheid, "essence_principale");
            SetBindingControls(txtEssenceAutre, "Text", ficheid, "essence_principale_autre");
            SetBindingControls(txtMelange, "Text", ficheid, "melanges");
            SetBindingControls(txtRpt, "Text", ficheid, "rpt_b");
            SetBindingControls(txtPente1, "Text", ficheid, "pente_1");
            SetBindingControls(txtPente2, "Text", ficheid, "pente_2");
            SetBindingControls(txtPente3, "Text", ficheid, "pente_3");
            SetBindingControls(txtPente4, "Text", ficheid, "pente_4");
            SetBindingControls(txtencartementType, "Text", ficheid, "encartement_type");
            SetBindingControls(txtDim1, "Text", ficheid, "ecartement_dim_1");
            SetBindingControls(txtDim2, "Text", ficheid, "ecartement_dim_2");
            SetBindingControls(txtAlignement, "Text", ficheid, "alignement");
            SetBindingControls(txtcause, "Text", ficheid, "causes");
            SetBindingControls(txtPiquet, "Text", ficheid, "piquets");
            SetBindingControls(txtPourcantage, "Text", ficheid, "pourcentage_insuffisants");
            SetBindingControls(txtEucalyptusEau, "Text", ficheid, "eucalyptus_deau");
            SetBindingControls(txtNvide, "Text", ficheid, "n_vides");
            SetBindingControls(txtZero, "Text", ficheid, "n_zero_demi_metre");
            SetBindingControls(txtDemiMetre, "Text", ficheid, "n_demi_un_metre");
            SetBindingControls(txtUnDeuxMetre, "Text", ficheid, "n_un_deux_metre");
            SetBindingControls(txtDeusPlusMetre, "Text", ficheid, "n_deux_plus_metre");
            SetBindingControls(txtDeuxPlusM, "Text", ficheid, "n_deux_plus_metre");
            SetBindingControls(P_zero_demi_metre, "Text", ficheid, "p_zero_demi_metre_calc");
            SetBindingControls(p_deux_, "Text", ficheid, "p_demi_un_metre_calc");
            
            SetBindingControls(p_deux_plus_metre, "Text", ficheid, "p_un_deux_metre_calc");
            SetBindingControls(txtDeuxPlusM, "Text", ficheid, "p_deux_plus_metre_calc");
            SetBindingControls(txtdegatC, "Text", ficheid, "degats_calc");
            SetBindingControls(txtTypeDegats, "Text", ficheid, "type_degats");
            SetBindingControls(chkVache, "Text", ficheid, "n_vaches");
            SetBindingControls(chkChevre, "Text", ficheid, "n_chevres");
            SetBindingControls(chkRats, "Text", ficheid, "n_rats");
            SetBindingControls(chkTemittes, "Text", ficheid, "n_termites");
            SetBindingControls(chkElephants, "Text", ficheid, "n_elephants");
            SetBindingControls(chkVivrieres, "Text", ficheid, "n_cultures_vivrieres");
            SetBindingControls(chkErosion, "Text", ficheid, "n_erosions");
            SetBindingControls(chkEboulement, "Text", ficheid, "n_eboulement");
            SetBindingControls(chkFeu, "Text", ficheid, "n_feu");
            SetBindingControls(chkSecheresse, "Text", ficheid, "n_secheresse");
            SetBindingControls(chkHommes, "Text", ficheid, "n_hommes");
            SetBindingControls(chkPlantesAvecSaches, "Text", ficheid, "n_plante_avec_sachets");
            SetBindingControls(chkPlanteesTrop, "Text", ficheid, "n_plante_trop_tard");
            SetBindingControls(chkGeure, "Text", ficheid, "n_guerren");
            SetBindingControls(txtDegats, "Text", ficheid, "degats_total");
            SetBindingControls(txtRegarnissage, "Text", ficheid, "regarnissage");
            SetBindingControls(txtRsuffisante, "Text", ficheid, "regarnissage_suffisant");
            SetBindingControls(txtentretien, "Text", ficheid, "entretien");
            SetBindingControls(txtetat, "Text", ficheid, "etat");
            SetBindingControls(chkVivrieres, "Text", ficheid, "cultures_vivrieres");
            SetBindingControls(txtCultureVivi, "Text", ficheid, "types_cultures_vivieres");
            SetBindingControls(txtTypecultureAutre, "Text", ficheid, "types_cultures_vivieres_autr");
            SetBindingControls(chkHaricaot, "Text", ficheid, "n_haricots");
            SetBindingControls(chkManioc, "Text", ficheid, "n_manioc");
            SetBindingControls(chksoja, "Text", ficheid, "n_soja");
            SetBindingControls(chkSorgho, "Text", ficheid, "n_sorgho");
            SetBindingControls(chkArachide, "Text", ficheid, "n_arachides");
            SetBindingControls(chkPatateDouce, "Text", ficheid, "n_patates_douces");
            SetBindingControls(chkmais, "Text", ficheid, "n_mais");
            SetBindingControls(chkAutres, "Text", ficheid, "n_autres");
            SetBindingControls(txtCultures, "Text", ficheid, "type_cultures_total");
            SetBindingControls(txtcanopee, "Text", ficheid, "canopee_fermee");
            SetBindingControls(txtSuperficieCanopee, "Text", ficheid, "superficie_canopee_fermee");
            SetBindingControls(txtarbreCroissance, "Text", ficheid, "croissance_arbres");
            SetBindingControls(txtarbreExistant, "Text", ficheid, "arbres_existants");
            SetBindingControls(txtRpt_c, "Text", ficheid, "rpt_c");
            SetBindingControls(imagePhoto1, "Image", ficheid, "photo_1");
            SetBindingControls(txtEmplacement, "Text", ficheid, "emplacement");
            SetBindingControls(image2, "Image", ficheid, "photo_2");
            SetBindingControls(txtEmplacement2, "Text", ficheid, "emplacement_2");
            SetBindingControls(txtLocalisation, "Text", ficheid, "localisation");
            SetBindingControls(txtComWWF, "Text", ficheid, "commentaire_wwf");
            SetBindingControls(txtComPlanteur, "Text", ficheid, "commentaire_planteur");
            SetBindingControls(txtComAssociation, "Text", ficheid, "commentaire_association");
            SetBindingControls(txtecalyptusEauNon, "Text", ficheid, "eucalyptus_deau_non");
            SetBindingControls(txtDateSynchro, "Text", ficheid, "synchronized_on");
           
        }

        //Permet de lier le BindingSource a la source de donnee comme DataGridView
        private void BindingList()
        {
            SetBindingControls(txtid, "Text", bdsrc, "id");
            SetBindingControls(txtuuid, "Text", bdsrc, "uuid");
            SetBindingControls(txtdececeid, "Text", bdsrc, "deviceid");
            SetBindingControls(txtdate, "Text", bdsrc, "date");
            SetBindingControls(txtnomAgent, "Text", bdsrc, "nom_agent");
            SetBindingControls(txtsaison, "Text", bdsrc, "saison");
            SetBindingControls(txtassociation, "Text", bdsrc, "association");
            SetBindingControls(txtassociationAutre, "Text", bdsrc, "association_autre");
            SetBindingControls(txtbailleur, "Text", bdsrc, "bailleur");
            SetBindingControls(txtbailleurAutre, "Text", bdsrc, "bailleur_autre");
            SetBindingControls(txtnvisite, "Text", bdsrc, "n_visite");
            SetBindingControls(txtCverification, "Text", bdsrc, "contreverification");
            SetBindingControls(txtNvisite2, "Text", bdsrc, "n_visite_2");
            SetBindingControls(txtnVisite3, "Text", bdsrc, "n_visite_3");
            SetBindingControls(txtNvisiteCalculate, "Text", bdsrc, "visite_calculate");
            SetBindingControls(txtNplantation, "Text", bdsrc, "n_plantation");
            SetBindingControls(txtNbloc, "Text", bdsrc, "n_bloc");
            SetBindingControls(txtnomPlanteur, "Text", bdsrc, "nom_planteur");
            SetBindingControls(txtnom, "Text", bdsrc, "nom");
            SetBindingControls(txtpostnom, "Text", bdsrc, "postnom");
            SetBindingControls(txtprenom, "Text", bdsrc, "prenom");
            SetBindingControls(txtSexe, "Text", bdsrc, "sexes");

            SetBindingControls(txtplanteurPresent, "Text", bdsrc, "planteur_present");
            SetBindingControls(txtChangement, "Text", bdsrc, "changement_surface");
            SetBindingControls(imageFiche, "Image", bdsrc, "photo_fiche");
            SetBindingControls(txtPreiodeDebut, "Text", bdsrc, "periode_debut");
            SetBindingControls(txtDebutAnnee, "Text", bdsrc, "periode_debut_annee");
            SetBindingControls(txtPreiodeFin, "Text", bdsrc, "periode_fin");
            SetBindingControls(periodeFinAnnee, "Text", bdsrc, "periode_fin_annee");
            SetBindingControls(txtEssencePrincale, "Text", bdsrc, "essence_principale");
            SetBindingControls(txtEssenceAutre, "Text", bdsrc, "essence_principale_autre");
            SetBindingControls(txtMelange, "Text", bdsrc, "melanges");
            SetBindingControls(txtRpt, "Text", bdsrc, "rpt_b");
            SetBindingControls(txtPente1, "Text", bdsrc, "pente_1");
            SetBindingControls(txtPente2, "Text", bdsrc, "pente_2");
            SetBindingControls(txtPente3, "Text", bdsrc, "pente_3");
            SetBindingControls(txtPente4, "Text", bdsrc, "pente_4");
            SetBindingControls(txtencartementType, "Text", bdsrc, "encartement_type");
            SetBindingControls(txtDim1, "Text", bdsrc, "ecartement_dim_1");
            SetBindingControls(txtDim2, "Text", bdsrc, "ecartement_dim_2");
            SetBindingControls(txtAlignement, "Text", bdsrc, "alignement");
            SetBindingControls(txtcause, "Text", bdsrc, "causes");
            SetBindingControls(txtPiquet, "Text", bdsrc, "piquets");
            SetBindingControls(txtPourcantage, "Text", bdsrc, "pourcentage_insuffisants");
            SetBindingControls(txtEucalyptusEau, "Text", bdsrc, "eucalyptus_deau");
            SetBindingControls(txtNvide, "Text", bdsrc, "n_vides");
            SetBindingControls(txtZero, "Text", bdsrc, "n_zero_demi_metre");
            SetBindingControls(txtDemiMetre, "Text", bdsrc, "n_demi_un_metre");
            SetBindingControls(txtUnDeuxMetre, "Text", bdsrc, "n_un_deux_metre");
            SetBindingControls(txtDeusPlusMetre, "Text", bdsrc, "n_deux_plus_metre");
            SetBindingControls(txtDeuxPlusM, "Text", bdsrc, "n_deux_plus_metre");
            SetBindingControls(P_zero_demi_metre, "Text", bdsrc, "p_zero_demi_metre_calc");
            SetBindingControls(p_deux_, "Text", bdsrc, "p_demi_un_metre_calc");

            SetBindingControls(p_deux_plus_metre, "Text", bdsrc, "p_un_deux_metre_calc");
            SetBindingControls(txtDeuxPlusM, "Text", bdsrc, "p_deux_plus_metre_calc");
            SetBindingControls(txtdegatC, "Text", bdsrc, "degats_calc");
            SetBindingControls(txtTypeDegats, "Text", bdsrc, "type_degats");
            SetBindingControls(chkVache, "Text", bdsrc, "n_vaches");
            SetBindingControls(chkChevre, "Text", bdsrc, "n_chevres");
            SetBindingControls(chkRats, "Text", bdsrc, "n_rats");
            SetBindingControls(chkTemittes, "Text", bdsrc, "n_termites");
            SetBindingControls(chkElephants, "Text", bdsrc, "n_elephants");
            SetBindingControls(chkVivrieres, "Text", bdsrc, "n_cultures_vivrieres");
            SetBindingControls(chkErosion, "Text", bdsrc, "n_erosions");
            SetBindingControls(chkEboulement, "Text", bdsrc, "n_eboulement");
            SetBindingControls(chkFeu, "Text", bdsrc, "n_feu");
            SetBindingControls(chkSecheresse, "Text", bdsrc, "n_secheresse");
            SetBindingControls(chkHommes, "Text", bdsrc, "n_hommes");
            SetBindingControls(chkPlantesAvecSaches, "Text", bdsrc, "n_plante_avec_sachets");
            SetBindingControls(chkPlanteesTrop, "Text", bdsrc, "n_plante_trop_tard");
            SetBindingControls(chkGeure, "Text", bdsrc, "n_guerren");
            SetBindingControls(txtDegats, "Text", bdsrc, "degats_total");
            SetBindingControls(txtRegarnissage, "Text", bdsrc, "regarnissage");
            SetBindingControls(txtRsuffisante, "Text", bdsrc, "regarnissage_suffisant");
            SetBindingControls(txtentretien, "Text", bdsrc, "entretien");
            SetBindingControls(txtetat, "Text", bdsrc, "etat");
            SetBindingControls(chkVivrieres, "Text", bdsrc, "cultures_vivrieres");
            SetBindingControls(txtCultureVivi, "Text", bdsrc, "types_cultures_vivieres");
            SetBindingControls(txtTypecultureAutre, "Text", bdsrc, "types_cultures_vivieres_autr");
            SetBindingControls(chkHaricaot, "Text", bdsrc, "n_haricots");
            SetBindingControls(chkManioc, "Text", bdsrc, "n_manioc");
            SetBindingControls(chksoja, "Text", bdsrc, "n_soja");
            SetBindingControls(chkSorgho, "Text", bdsrc, "n_sorgho");
            SetBindingControls(chkArachide, "Text", bdsrc, "n_arachides");
            SetBindingControls(chkPatateDouce, "Text", bdsrc, "n_patates_douces");
            SetBindingControls(chkmais, "Text", bdsrc, "n_mais");
            SetBindingControls(chkAutres, "Text", bdsrc, "n_autres");
            SetBindingControls(txtCultures, "Text", bdsrc, "type_cultures_total");
            SetBindingControls(txtcanopee, "Text", bdsrc, "canopee_fermee");
            SetBindingControls(txtSuperficieCanopee, "Text", bdsrc, "superficie_canopee_fermee");
            SetBindingControls(txtarbreCroissance, "Text", bdsrc, "croissance_arbres");
            SetBindingControls(txtarbreExistant, "Text", bdsrc, "arbres_existants");
            SetBindingControls(txtRpt_c, "Text", bdsrc, "rpt_c");
            SetBindingControls(imagePhoto1, "Image", bdsrc, "photo_1");
            SetBindingControls(txtEmplacement, "Text", bdsrc, "emplacement");
            SetBindingControls(image2, "Image", bdsrc, "photo_2");
            SetBindingControls(txtEmplacement2, "Text", bdsrc, "emplacement_2");
            SetBindingControls(txtLocalisation, "Text", bdsrc, "localisation");
            SetBindingControls(txtComWWF, "Text", bdsrc, "commentaire_wwf");
            SetBindingControls(txtComPlanteur, "Text", bdsrc, "commentaire_planteur");
            SetBindingControls(txtComAssociation, "Text", bdsrc, "commentaire_association");
            SetBindingControls(txtecalyptusEauNon, "Text", bdsrc, "eucalyptus_deau_non");
            SetBindingControls(txtDateSynchro, "Text", bdsrc, "synchronized_on");
        }
        private void PlantationReliaser_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshData();
                dgvPr.DataSource = bdsrc;
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors du chargement", "Erreur de chargement des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void New()
        {
            ficheid = new clstbl_fiche_pr();

            btnAdd.Enabled = true;
            blnModifie = false;

            BindingCls();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!blnModifie)
                {
                    //ficheid.Photo = (clsDoTraitement.GetInstance().ImageToString64(imageFiche.Image));
                    //ficheid.Photo = (clsDoTraitement.GetInstance().ImageToString64(imagePhoto1.Image));
                    //ficheid.Photo = (clsDoTraitement.GetInstance().ImageToString64(image2.Image));
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
            //if (clsMetier.tbPhoto.Count > 0)
            //    clsMetier.tbPhoto.Clear();

            //clsMetier.tbPhoto.Add(clsDoTraitement.GetInstance().ImageToString64(imageFiche.Image));

            int record = ficheid.update((DataRowView)bdsrc.Current);
            MessageBox.Show("Modification éffectuée : " + record + " Modifié", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

       

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPhoto2_Click(object sender, EventArgs e)
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
                    imageInventaire.Load(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement de la photo + " + ex.Message, "Sélection photo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPhoto1_Click(object sender, EventArgs e)
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
                    imageFiche.Load(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement de la photo + " + ex.Message, "Sélection photo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPhoto3_Click(object sender, EventArgs e)
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
                    imagePhoto1.Load(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement de la photo + " + ex.Message, "Sélection photo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnPhoto4_Click(object sender, EventArgs e)
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
                    image2.Load(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de chargement de la photo + " + ex.Message, "Sélection photo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
        }
        }
