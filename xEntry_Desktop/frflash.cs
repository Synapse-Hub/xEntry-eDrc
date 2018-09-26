using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace xEntry_Desktop
{
    public partial class frflash : Form
    {
        public frflash()
        {
            InitializeComponent();
        }

        private void tmrFlash_Tick(object sender, EventArgs e)
        {
            tmrFlash.Interval = 2000;
            tmrFlash.Tick += new System.EventHandler(OnTimerEvent); 
        }
        
        public void OnTimerEvent(object sender, EventArgs e)
        {
            this.Hide();
            tmrFlash.Enabled = false;

            mdiMainForm mainform = new mdiMainForm();
            mainform.Show();
        }

        private void frflash_Load(object sender, EventArgs e)
        {
            tmrFlash.Enabled = true;
        }
    }
}
