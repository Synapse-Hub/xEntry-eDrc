using ManageUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Xentry.Data;
using Xentry.Utilities;

namespace Xentry.Desktop
{
    public partial class FormConn : Form
    {
        clsConnexion connection = new clsConnexion();

        ResourceManager stringManager = null;

        #region Instance du mdiMainForm// ++++++++++++++++++++++++++++++++++++++
        // ++++++++++++++++++++++++++++++++++++++
        private MdiMainForm principalform = new MdiMainForm();

        public MdiMainForm PrincipalForm
        {
            get
            {
                return principalform;
            }
            set
            {
                principalform = value;
            }
        }

        // ++++++++++++++++++++++++++++++++++++++++
        // ++++++++++++++++++++++++++++++++++++++++

        #endregion

        private void LoadValues()
        {
            clsDoTraitement.valueUser.Clear();

            //Chardement des parametres de connexion 
            //Ici si le fichier est vide ou qu'il n'existe pas,on charge les paramètres par défaut
            List<string> lstValues = ImplementUtilities.Instance.LoadDatabaseParameters(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.DirectoryUtilConn, Properties.Settings.Default.FileSQLServer, '\n');

            if (lstValues.Count > 0)
            {
                connection.Serveur = lstValues[0];//Nom du serveur
                connection.DB = lstValues[1];//Nom de la Base de Données
                //connection.User = lstValues[2];//User de la Base de Données
                connection.User = txtnomuser.Text;
                connection.Pwd = txtpwd.Text;

                //Iniialisation de la chaine de connexion
                clsMetier.GetInstance().Initialize(connection, 1);
            }
            else
            {
                //Si le fichier des parametres de la BD ne contient rien on y mets des  parametres par defaut qu'on pourra modifier after
                //Le nom correct du serveur doit être change s'il arrive que vous devez utiliser une autre machine
               connection.Serveur = @"MICHELOFD23\SQLSERVMICHELO";
              //  connection.Serveur = @"WWF_SERVER12\SQLSERVWWFE18";
                connection.DB = "xEntryGlobalDb";
                connection.User = txtnomuser.Text;
                connection.Pwd = txtpwd.Text;
                //connection.User = "sa";

                //Iniialisation de la chaine de connexion
                clsMetier.GetInstance().Initialize(connection, 1);
            }
        }
        public FormConn()
        {
            InitializeComponent();
            ImplementUtilities.Instance.MasterDirectoryConfiguration = Properties.Settings.Default.MasterDirectory;

            //Initialisation des Resources
            Assembly _assembly = Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);
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

                if (Convert.ToBoolean(lstValues[3], System.Globalization.CultureInfo.InvariantCulture))
                {
                    //Ajout des droits du user dans la variable
                    foreach (string str in lstValues[2].ToString().Split(','))
                        clsDoTraitement.valueUser.Add(str);

                    MessageBox.Show(stringManager.GetString("StringSuccessConnectionBDMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringSuccessConnectionBDCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

                    //Enregistrement des parametres de connexion
                    ImplementUtilities.Instance.SaveParameters(Properties.Settings.Default.MasterDirectory,
                        string.Format(CultureInfo.InvariantCulture, "Serveur={0}\nDataBase={1}\nUserBD={2}\nPassword={3}", connection.Serveur, connection.DB, string.Empty, string.Empty),
                        Properties.Settings.Default.DirectoryUtilConn, Properties.Settings.Default.FileSQLServer);
                }
                else
                {
                    MessageBox.Show(stringManager.GetString("StringFailedAuthenticationUserBDMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedAuthenticationUserBDCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
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
                clsMetier.bdEnCours = clsMetier.GetInstance().getCurrentDataBase();

                PrincipalForm.Usern = connection.User;

                PrincipalForm.LockMenu(true, clsDoTraitement.valueUser[0]);
                this.Cursor = Cursors.Default;
                this.Close();             
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this.Cursor = Cursors.Default;

                MessageBox.Show(stringManager.GetString("StringFailedOpenConnectionBDMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedOpenConnectionBDCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                //On garde chaque fois une trace de l'erreur generee
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de l'ouverture de la connexion à la Base de données : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch(System.IO.IOException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedOpenConnectionBDMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedOpenConnectionBDCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec de l'ouverture de la connexion à la Base de données : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
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
