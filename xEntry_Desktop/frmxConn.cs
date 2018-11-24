using ManageUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using xEntry_Data;
using xEntry_Utilities;

namespace xEntry_Desktop
{
    public partial class frmxConn : Form
    {
        clsConnexion connection = new clsConnexion();

        //Repertoire pour le Log
        private const string MasterDirectory = "xEntry";
        //Nom du repertoire qui contiendra la chaine de connexion a la BD
        private const string DirectoryUtilConn = "ConnectionString";
        //Nom du fichier qui contiendra la chaine de connexion connexion a la BD SQLServer
        private const string FileSQLServer = "UserSQLSever.txt";
        //Repertoire pour le Log
        const string DirectoryUtilLog = "Log";

        #region Instance du mdiMainForm// ++++++++++++++++++++++++++++++++++++++
        // ++++++++++++++++++++++++++++++++++++++
        private mdiMainForm mainform = new mdiMainForm();

        public mdiMainForm Mainform
        {
            get { return mainform; }
            set { mainform = value; }
        }

        // ++++++++++++++++++++++++++++++++++++++++
        // ++++++++++++++++++++++++++++++++++++++++

        #endregion

        private void LoadValues()
        {
            clsDoTraitement.valueUser.Clear();

            //Chardement des parametres de connexion 
            //Ici si le fichier est vide ou qu'il n'existe pas,on charge les paramètres par défaut
            List<string> lstValues = ImplementUtilities.Instance.LoadDatabaseParameters(MasterDirectory, DirectoryUtilConn, FileSQLServer, '\n');

            if (lstValues.Count > 0)
            {
                connection.Serveur = lstValues[0];//Nom du serveur
                connection.DB = lstValues[1];//Nom de la Base de Donnees
                //connection.User = lstValues[2];//User de la Base de Donnees
                connection.User = txtnomuser.Text;
                connection.Pwd = txtpwd.Text;

                //Iniialisation de la chaine de connexion
                clsMetier.GetInstance().Initialize(connection, 1);
            }
            else
            {
                //Si le fichier des parametres de la BD ne contient rien on y mets des  parametres par defaut qu'on pourra modifier after
                //Le nom correct du serveur doit être change s'il arrive que vous devez utiliser une autre machine
               // connection.Serveur = @"MICHELOFD23\SQLSERVMICHELO";
                connection.Serveur = @"WWF_SERVER12\SQLSERVWWFE18";
                connection.DB = "xEntryGlobalDb";
                connection.User = txtnomuser.Text;
                connection.Pwd = txtpwd.Text;
                //connection.User = "sa";

                //Iniialisation de la chaine de connexion
                clsMetier.GetInstance().Initialize(connection, 1);
            }
        }
        public frmxConn()
        {
            InitializeComponent();
            ImplementUtilities.Instance.MasterDirectoryConfiguration = MasterDirectory;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VerifieUser()
        {
            if (clsMetier.GetInstance().isConnect())
            {
                ArrayList lstValues = clsMetier.GetInstance().verifieLoginUser(connection.User, connection.Pwd);

                if (Convert.ToBoolean(lstValues[3]))
                {
                    //Ajout des droits du user dans la variable
                    foreach (string str in lstValues[2].ToString().Split(','))
                        clsDoTraitement.valueUser.Add(str);
                }

                if (Convert.ToBoolean(lstValues[3]))
                {
                    MessageBox.Show("Connexion réussie", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Enregistrement des parametres de connexion
                    ImplementUtilities.Instance.SaveParameters(MasterDirectory,
                        string.Format("Serveur={0}\nDataBase={1}\nUserBD={2}\nPassword={3}", connection.Serveur, connection.DB, string.Empty, string.Empty),
                        DirectoryUtilConn, FileSQLServer);
                }
                else
                {
                    MessageBox.Show("Echec de l'authentification de l'utilisateur", "Authentification de l'utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtnomuser.Clear();
                    txtpwd.Clear();
                    txtnomuser.Focus();
                }
            }
        }

        private void btnxConn_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                //Appel de l'initialisation de la chaine de connexion avant de l'ouvrir
                LoadValues();

                //Execution des verification de l'utilisateur
                VerifieUser();

                //Recupération bd connectée
                try
                {
                    clsMetier.bdEnCours = clsMetier.GetInstance().getCurrentDataBase();
                }
                catch (Exception) { }

                Mainform.usern = connection.User;

                Mainform.LockMenu(true, clsDoTraitement.valueUser[0]);
                this.Cursor = Cursors.Default;
                this.Close();             
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show("Echec de l'ouverture de la connexion à la Base de données, " + ex.Message, "connexion à la Base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //On garde chaque fois une trace de l'erreur generee
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de l'ouverture de la connexion à la Base de données : " + ex.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
            }
        }

        private void txtnomserveur_Enter(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.Khaki;
        }

        private void txtnomserveur_Leave(object sender, EventArgs e)
        {
            ((Control)sender).BackColor = Color.White;
        }

        private void txtpwd_TextChanged(object sender, EventArgs e)
        {
            btnxConn.Enabled = true;
        }

        private void txtpwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnxConn_Click(sender, e);
            }
        }

        private void frmxConn_Load(object sender, EventArgs e)
        {
            txtnomuser.Focus();
        }
    }
}
