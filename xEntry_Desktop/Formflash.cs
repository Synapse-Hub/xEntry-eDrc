using System;
using System.Windows.Forms;

namespace Xentry.Desktop
{
    public partial class Formflash : Form
    {
        public Formflash()
        {
            InitializeComponent();
        }

        private void tmrFlash_Tick(object sender, EventArgs e)
        {
            tmrFlash.Interval = 3000;
            tmrFlash.Tick += new System.EventHandler(OnTimerEvent); 
        }
        
        public void OnTimerEvent(object sender, EventArgs e)
        {
            this.Hide();
            tmrFlash.Enabled = false;

            MdiMainForm mdiForm = new MdiMainForm();
            mdiForm.Show();
        }

        private void frflash_Load(object sender, EventArgs e)
        {
            tmrFlash.Enabled = true;
        }
    }
}
