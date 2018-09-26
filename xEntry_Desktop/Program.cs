using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace xEntry_Desktop
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frflash());
            Application.Run(new mdiMainForm());
        }

        public static void ErrorMsg(string strMsg, string strTitle)
        {
            MessageBox.Show(strMsg, strTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
