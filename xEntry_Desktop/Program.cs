using System;
using System.Windows.Forms;
using System.Threading;
using System.Resources;

namespace Xentry.Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Subscribe to thread (unhandled) exception events
            ThreadExceptionHandler handler = new ThreadExceptionHandler();
            Application.ThreadException += new ThreadExceptionEventHandler(handler.Application_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Formflash());
            Application.Run(new MdiMainForm());
        }
    }

    /// 
    /// Handles a thread (unhandled) exception.
    /// 
    internal class ThreadExceptionHandler
    {
        static ResourceManager stringManager = null;

        internal ThreadExceptionHandler()
        {
            System.Reflection.Assembly _assembly = System.Reflection.Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);
        }
        /// 
        /// Handles the thread exception.
        /// 
        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            try
            {
                // Exit the program if the user clicks Abort.
                DialogResult result = ShowThreadExceptionDialog(e.Exception);

                if (result == DialogResult.Abort)
                    Application.Exit();
            }
            catch
            {
                // Fatal error, terminate program
                try
                {
                    MessageBox.Show(stringManager.GetString("StringSystemErrorMessage", System.Globalization.CultureInfo.CurrentUICulture), stringManager.GetString("StringSystemErrorCaption", System.Globalization.CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                finally
                {
                    Application.Exit();
                }
                throw;
            }
        }

        /// 
        /// Creates and displays the error message.
        /// 
        private static DialogResult ShowThreadExceptionDialog(Exception ex)
        {
            string errorMessage = stringManager.GetString("StringSystemErrorMessage", System.Globalization.CultureInfo.CurrentUICulture);

            Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur inattendue : Xentry.Desktop.Program : " + ex.GetType().ToString() + " : " + ex.Message;
            ManageUtilities.ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");

            return MessageBox.Show(errorMessage, stringManager.GetString("StringSystemErrorCaption", System.Globalization.CultureInfo.CurrentUICulture), MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
    } // End ThreadExceptionHandler
}
