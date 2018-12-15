using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_fiche_pr
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private string deviceid;
        private DateTime date;
        private string nom_agent;
        private string saison;
        private string association;
        private string association_autre;
        private string bailleur;
        private string bailleur_autre;
        private string n_visite;
        private string contreverification;
        private int? n_visite_2;
        private int? n_viste_3;
        private int? visite_calculate;
        private string n_plantation;
        private string n_bloc;
        private string noms_planteur;
        private string nom;
        private string post_nom;
        private string prenom;
        private string sexes;
        private string planteur_present;
        private string changement_surface;
        private Byte[] photo_fiche;
        private string titre_trace_gps;
        private double? superficie;
        private double? superficie_non_plantee;
        private Byte[] photo_inventaire;
        private string periode_debut;
        private string preiode_debut_annee;
        private string periode_fin;
        private string period_fin_annee;
        private string essence_principale;
        private string essence_principale_autre;
        private string melanges;
        private string rpt_b;
        private int? pente_1;
        private int? pente_2;
        private int? pente_3;
        private int? pente_4;
        private string encartement_type;
        private string ecartement_dim_1;
        private string ecartement_dim_2;
        private string alignement;
        private string causes;
        private string piquets;
        private string pourcentage_insuffisants;
        private string eucalyptus_deau;
        private int? n_vides;
        private int? n_zero_demi_metre;
        private int? n_demi_un_metre;
        private int? n_un_deux_metre;
        private int? n_deux_plus_metre;
        private int? p_zero_demi_metre_calc;
        private int? p_demi_un_metre_calc;
        private int? p_un_deux_metre_calc;
        private int? p_deux_plus_metre_calc;
        private int? degats_calc;
        private string type_degats;
        private string n_vaches;
        private string n_chevres;
        private string n_rats;
        private string n_termites;
        private string n_elephants;
        private string n_cultures_vivrieres;
        private string n_erosion;
        private string n_eboulement;
        private string n_feu;
        private string n_secheresse;
        private string n_hommes;
        private string n_plante_avec_sachets;
        private string n_plante_trop_tard;
        private string n_guerren;
        private int? degats_total;
        private string regarnissage;
        private string regarnissage_suffisant;
        private string entretien;
        private string etat;
        private string cultures_vivrieres;
        private string type_cultures_vivieres;
        private string type_cultures_vivieres_autr;
        private string n_haricots;
        private string n_manioc;
        private string n_soja;
        private string n_sorgho;
        private string n_arachides;
        private string n_patates_douces;
        private string n_mais;
        private string n_autres;
        private int? type_cultures_total;
        private string canopee_fermee;
        private double? superficie_canopee_fermee;
        private string croissance_arbres;
        private string arbres_existants;
        private string rpt_c;
        private Byte[] photo_1;
        private string emplacement;
        private string photo_2;
        private string emplacement_2;
        private string localisation;
        private string commentaire_wwf;
        private string commentaire_planteur;
        private string commentaire_association;
        private string eucalyptus_deau_non;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_fiche_prTables()
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_pr();
        }
        public DataTable clstbl_fiche_prTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_fiche_pr(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_fiche_pr(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_fiche_pr(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_fiche_pr(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_fiche_pr()
        {
        }

        //***Accesseur de id***
        public int Id
        {
            get { return id; }
            set { id = value; }
        }  //***Accesseur de uuid***
        public string Uuid
        {
            get { return uuid; }
            set { uuid = value; }
        }  //***Accesseur de deviceid***
        public string Deviceid
        {
            get { return deviceid; }
            set { deviceid = value; }
        }  //***Accesseur de date***
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }  //***Accesseur de nom_agent***
        public string Nom_agent
        {
            get { return nom_agent; }
            set { nom_agent = value; }
        }  //***Accesseur de saison***
        public string Saison
        {
            get { return saison; }
            set { saison = value; }
        }  //***Accesseur de association***
        public string Association
        {
            get { return association; }
            set { association = value; }
        }  //***Accesseur de association_autre***
        public string Association_autre
        {
            get { return association_autre; }
            set { association_autre = value; }
        }  //***Accesseur de bailleur***
        public string Bailleur
        {
            get { return bailleur; }
            set { bailleur = value; }
        }  //***Accesseur de bailleur_autre***
        public string Bailleur_autre
        {
            get { return bailleur_autre; }
            set { bailleur_autre = value; }
        }  //***Accesseur de n_visite***
        public string N_visite
        {
            get { return n_visite; }
            set { n_visite = value; }
        }  //***Accesseur de contreverification***
        public string Contreverification
        {
            get { return contreverification; }
            set { contreverification = value; }
        }  //***Accesseur de n_visite_2***
        public int? N_visite_2
        {
            get { return n_visite_2; }
            set { n_visite_2 = value; }
        }  //***Accesseur de n_viste_3***
        public int? N_viste_3
        {
            get { return n_viste_3; }
            set { n_viste_3 = value; }
        }  //***Accesseur de visite_calculate***
        public int? Visite_calculate
        {
            get { return visite_calculate; }
            set { visite_calculate = value; }
        }  //***Accesseur de n_plantation***
        public string N_plantation
        {
            get { return n_plantation; }
            set { n_plantation = value; }
        }  //***Accesseur de n_bloc***
        public string N_bloc
        {
            get { return n_bloc; }
            set { n_bloc = value; }
        }  //***Accesseur de noms_planteur***
        public string Noms_planteur
        {
            get { return noms_planteur; }
            set { noms_planteur = value; }
        }  //***Accesseur de nom***
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }  //***Accesseur de post_nom***
        public string Post_nom
        {
            get { return post_nom; }
            set { post_nom = value; }
        }  //***Accesseur de prenom***
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }  //***Accesseur de sexes***
        public string Sexes
        {
            get { return sexes; }
            set { sexes = value; }
        }  //***Accesseur de planteur_present***
        public string Planteur_present
        {
            get { return planteur_present; }
            set { planteur_present = value; }
        }  //***Accesseur de changement_surface***
        public string Changement_surface
        {
            get { return changement_surface; }
            set { changement_surface = value; }
        }  //***Accesseur de photo_fiche***
        public Byte[] Photo_fiche
        {
            get { return photo_fiche; }
            set { photo_fiche = value; }
        }  //***Accesseur de titre_trace_gps***
        public string Titre_trace_gps
        {
            get { return titre_trace_gps; }
            set { titre_trace_gps = value; }
        }  //***Accesseur de superficie***
        public double? Superficie
        {
            get { return superficie; }
            set { superficie = value; }
        }  //***Accesseur de superficie_non_plantee***
        public double? Superficie_non_plantee
        {
            get { return superficie_non_plantee; }
            set { superficie_non_plantee = value; }
        }  //***Accesseur de photo_inventaire***
        public Byte[] Photo_inventaire
        {
            get { return photo_inventaire; }
            set { photo_inventaire = value; }
        }  //***Accesseur de periode_debut***
        public string Periode_debut
        {
            get { return periode_debut; }
            set { periode_debut = value; }
        }  //***Accesseur de preiode_debut_annee***
        public string Preiode_debut_annee
        {
            get { return preiode_debut_annee; }
            set { preiode_debut_annee = value; }
        }  //***Accesseur de periode_fin***
        public string Periode_fin
        {
            get { return periode_fin; }
            set { periode_fin = value; }
        }  //***Accesseur de period_fin_annee***
        public string Period_fin_annee
        {
            get { return period_fin_annee; }
            set { period_fin_annee = value; }
        }  //***Accesseur de essence_principale***
        public string Essence_principale
        {
            get { return essence_principale; }
            set { essence_principale = value; }
        }  //***Accesseur de essence_principale_autre***
        public string Essence_principale_autre
        {
            get { return essence_principale_autre; }
            set { essence_principale_autre = value; }
        }  //***Accesseur de melanges***
        public string Melanges
        {
            get { return melanges; }
            set { melanges = value; }
        }  //***Accesseur de rpt_b***
        public string Rpt_b
        {
            get { return rpt_b; }
            set { rpt_b = value; }
        }  //***Accesseur de pente_1***
        public int? Pente_1
        {
            get { return pente_1; }
            set { pente_1 = value; }
        }  //***Accesseur de pente_2***
        public int? Pente_2
        {
            get { return pente_2; }
            set { pente_2 = value; }
        }  //***Accesseur de pente_3***
        public int? Pente_3
        {
            get { return pente_3; }
            set { pente_3 = value; }
        }  //***Accesseur de pente_4***
        public int? Pente_4
        {
            get { return pente_4; }
            set { pente_4 = value; }
        }  //***Accesseur de encartement_type***
        public string Encartement_type
        {
            get { return encartement_type; }
            set { encartement_type = value; }
        }  //***Accesseur de ecartement_dim_1***
        public string Ecartement_dim_1
        {
            get { return ecartement_dim_1; }
            set { ecartement_dim_1 = value; }
        }  //***Accesseur de ecartement_dim_2***
        public string Ecartement_dim_2
        {
            get { return ecartement_dim_2; }
            set { ecartement_dim_2 = value; }
        }  //***Accesseur de alignement***
        public string Alignement
        {
            get { return alignement; }
            set { alignement = value; }
        }  //***Accesseur de causes***
        public string Causes
        {
            get { return causes; }
            set { causes = value; }
        }  //***Accesseur de piquets***
        public string Piquets
        {
            get { return piquets; }
            set { piquets = value; }
        }  //***Accesseur de pourcentage_insuffisants***
        public string Pourcentage_insuffisants
        {
            get { return pourcentage_insuffisants; }
            set { pourcentage_insuffisants = value; }
        }  //***Accesseur de eucalyptus_deau***
        public string Eucalyptus_deau
        {
            get { return eucalyptus_deau; }
            set { eucalyptus_deau = value; }
        }  //***Accesseur de n_vides***
        public int? N_vides
        {
            get { return n_vides; }
            set { n_vides = value; }
        }  //***Accesseur de n_zero_demi_metre***
        public int? N_zero_demi_metre
        {
            get { return n_zero_demi_metre; }
            set { n_zero_demi_metre = value; }
        }  //***Accesseur de n_demi_un_metre***
        public int? N_demi_un_metre
        {
            get { return n_demi_un_metre; }
            set { n_demi_un_metre = value; }
        }  //***Accesseur de n_un_deux_metre***
        public int? N_un_deux_metre
        {
            get { return n_un_deux_metre; }
            set { n_un_deux_metre = value; }
        }  //***Accesseur de n_deux_plus_metre***
        public int? N_deux_plus_metre
        {
            get { return n_deux_plus_metre; }
            set { n_deux_plus_metre = value; }
        }  //***Accesseur de p_zero_demi_metre_calc***
        public int? P_zero_demi_metre_calc
        {
            get { return p_zero_demi_metre_calc; }
            set { p_zero_demi_metre_calc = value; }
        }  //***Accesseur de p_demi_un_metre_calc***
        public int? P_demi_un_metre_calc
        {
            get { return p_demi_un_metre_calc; }
            set { p_demi_un_metre_calc = value; }
        }  //***Accesseur de p_un_deux_metre_calc***
        public int? P_un_deux_metre_calc
        {
            get { return p_un_deux_metre_calc; }
            set { p_un_deux_metre_calc = value; }
        }  //***Accesseur de p_deux_plus_metre_calc***
        public int? P_deux_plus_metre_calc
        {
            get { return p_deux_plus_metre_calc; }
            set { p_deux_plus_metre_calc = value; }
        }  //***Accesseur de degats_calc***
        public int? Degats_calc
        {
            get { return degats_calc; }
            set { degats_calc = value; }
        }  //***Accesseur de type_degats***
        public string Type_degats
        {
            get { return type_degats; }
            set { type_degats = value; }
        }  //***Accesseur de n_vaches***
        public string N_vaches
        {
            get { return n_vaches; }
            set { n_vaches = value; }
        }  //***Accesseur de n_chevres***
        public string N_chevres
        {
            get { return n_chevres; }
            set { n_chevres = value; }
        }  //***Accesseur de n_rats***
        public string N_rats
        {
            get { return n_rats; }
            set { n_rats = value; }
        }  //***Accesseur de n_termites***
        public string N_termites
        {
            get { return n_termites; }
            set { n_termites = value; }
        }  //***Accesseur de n_elephants***
        public string N_elephants
        {
            get { return n_elephants; }
            set { n_elephants = value; }
        }  //***Accesseur de n_cultures_vivrieres***
        public string N_cultures_vivrieres
        {
            get { return n_cultures_vivrieres; }
            set { n_cultures_vivrieres = value; }
        }  //***Accesseur de n_erosion***
        public string N_erosion
        {
            get { return n_erosion; }
            set { n_erosion = value; }
        }  //***Accesseur de n_eboulement***
        public string N_eboulement
        {
            get { return n_eboulement; }
            set { n_eboulement = value; }
        }  //***Accesseur de n_feu***
        public string N_feu
        {
            get { return n_feu; }
            set { n_feu = value; }
        }  //***Accesseur de n_secheresse***
        public string N_secheresse
        {
            get { return n_secheresse; }
            set { n_secheresse = value; }
        }  //***Accesseur de n_hommes***
        public string N_hommes
        {
            get { return n_hommes; }
            set { n_hommes = value; }
        }  //***Accesseur de n_plante_avec_sachets***
        public string N_plante_avec_sachets
        {
            get { return n_plante_avec_sachets; }
            set { n_plante_avec_sachets = value; }
        }  //***Accesseur de n_plante_trop_tard***
        public string N_plante_trop_tard
        {
            get { return n_plante_trop_tard; }
            set { n_plante_trop_tard = value; }
        }  //***Accesseur de n_guerren***
        public string N_guerren
        {
            get { return n_guerren; }
            set { n_guerren = value; }
        }  //***Accesseur de degats_total***
        public int? Degats_total
        {
            get { return degats_total; }
            set { degats_total = value; }
        }  //***Accesseur de regarnissage***
        public string Regarnissage
        {
            get { return regarnissage; }
            set { regarnissage = value; }
        }  //***Accesseur de regarnissage_suffisant***
        public string Regarnissage_suffisant
        {
            get { return regarnissage_suffisant; }
            set { regarnissage_suffisant = value; }
        }  //***Accesseur de entretien***
        public string Entretien
        {
            get { return entretien; }
            set { entretien = value; }
        }  //***Accesseur de etat***
        public string Etat
        {
            get { return etat; }
            set { etat = value; }
        }  //***Accesseur de cultures_vivrieres***
        public string Cultures_vivrieres
        {
            get { return cultures_vivrieres; }
            set { cultures_vivrieres = value; }
        }  //***Accesseur de type_cultures_vivieres***
        public string Type_cultures_vivieres
        {
            get { return type_cultures_vivieres; }
            set { type_cultures_vivieres = value; }
        }  //***Accesseur de type_cultures_vivieres_autr***
        public string Type_cultures_vivieres_autr
        {
            get { return type_cultures_vivieres_autr; }
            set { type_cultures_vivieres_autr = value; }
        }  //***Accesseur de n_haricots***
        public string N_haricots
        {
            get { return n_haricots; }
            set { n_haricots = value; }
        }  //***Accesseur de n_manioc***
        public string N_manioc
        {
            get { return n_manioc; }
            set { n_manioc = value; }
        }  //***Accesseur de n_soja***
        public string N_soja
        {
            get { return n_soja; }
            set { n_soja = value; }
        }  //***Accesseur de n_sorgho***
        public string N_sorgho
        {
            get { return n_sorgho; }
            set { n_sorgho = value; }
        }  //***Accesseur de n_arachides***
        public string N_arachides
        {
            get { return n_arachides; }
            set { n_arachides = value; }
        }  //***Accesseur de n_patates_douces***
        public string N_patates_douces
        {
            get { return n_patates_douces; }
            set { n_patates_douces = value; }
        }  //***Accesseur de n_mais***
        public string N_mais
        {
            get { return n_mais; }
            set { n_mais = value; }
        }  //***Accesseur de n_autres***
        public string N_autres
        {
            get { return n_autres; }
            set { n_autres = value; }
        }  //***Accesseur de type_cultures_total***
        public int? Type_cultures_total
        {
            get { return type_cultures_total; }
            set { type_cultures_total = value; }
        }  //***Accesseur de canopee_fermee***
        public string Canopee_fermee
        {
            get { return canopee_fermee; }
            set { canopee_fermee = value; }
        }  //***Accesseur de superficie_canopee_fermee***
        public double? Superficie_canopee_fermee
        {
            get { return superficie_canopee_fermee; }
            set { superficie_canopee_fermee = value; }
        }  //***Accesseur de croissance_arbres***
        public string Croissance_arbres
        {
            get { return croissance_arbres; }
            set { croissance_arbres = value; }
        }  //***Accesseur de arbres_existants***
        public string Arbres_existants
        {
            get { return arbres_existants; }
            set { arbres_existants = value; }
        }  //***Accesseur de rpt_c***
        public string Rpt_c
        {
            get { return rpt_c; }
            set { rpt_c = value; }
        }  //***Accesseur de photo_1***
        public Byte[] Photo_1
        {
            get { return photo_1; }
            set { photo_1 = value; }
        }  //***Accesseur de emplacement***
        public string Emplacement
        {
            get { return emplacement; }
            set { emplacement = value; }
        }  //***Accesseur de photo_2***
        public string Photo_2
        {
            get { return photo_2; }
            set { photo_2 = value; }
        }  //***Accesseur de emplacement_2***
        public string Emplacement_2
        {
            get { return emplacement_2; }
            set { emplacement_2 = value; }
        }  //***Accesseur de localisation***
        public string Localisation
        {
            get { return localisation; }
            set { localisation = value; }
        }  //***Accesseur de commentaire_wwf***
        public string Commentaire_wwf
        {
            get { return commentaire_wwf; }
            set { commentaire_wwf = value; }
        }  //***Accesseur de commentaire_planteur***
        public string Commentaire_planteur
        {
            get { return commentaire_planteur; }
            set { commentaire_planteur = value; }
        }  //***Accesseur de commentaire_association***
        public string Commentaire_association
        {
            get { return commentaire_association; }
            set { commentaire_association = value; }
        }  //***Accesseur de eucalyptus_deau_non***
        public string Eucalyptus_deau_non
        {
            get { return eucalyptus_deau_non; }
            set { eucalyptus_deau_non = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace