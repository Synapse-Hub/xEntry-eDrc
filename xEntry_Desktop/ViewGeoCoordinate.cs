using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using xEntry_Data;
using System.Windows.Forms;
using System.IO;

namespace xEntry_Desktop
{
    public partial class ViewGeoCoordinate : Form
    {

        public string myPrValue;

        BindingSource _binsrc = new BindingSource();
        private clstbl_geopoint ficheid = new clstbl_geopoint();
        private bool blnModifie = false;
         DataTable dtTable;

        //string Texte;

        mdiMainForm xMainForm = new mdiMainForm();


        public mdiMainForm getMdiMainForm()
        {
            return xMainForm;
        }
        public void setMdiMainForm(mdiMainForm xmainF)
        {
            xMainForm = xmainF;
        }

        public ViewGeoCoordinate()
        {
            InitializeComponent();
        }

        private void ViewGeoCoordinate_Load(object sender, EventArgs e)
        {
           //Resize the form
            //this.Width = 1714;
            //this.Height = 998;

            dtTable = new DataTable();

            try
            {
                RefreshData();
                dgvGps.DataSource = _binsrc;

            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors du chargement", "Erreur de chargement des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            //dtTable.Columns.Add("deviceid", typeof(int));
            //dtTable.Columns.Add("latitude", typeof(String));
            //dtTable.Columns.Add("longitude", typeof(String));

            foreach (DataGridViewColumn col in dgvGps.Columns)
            {
                dtTable.Columns.Add(col.Name);
            }

            //foreach (DataGridViewRow dgvR in dgvGps.Rows)
            //{
            //    dgvGps.Rows.Add(dgvR.Cells[2].Value, dgvR.Cells[3].Value, dgvR.Cells[4].Value);
            //}

        }

        private void SetBindingControls(Control ctr, string ctr_prop, object objsrce, string obj_prop)
        {
            ctr.DataBindings.Clear();
            ctr.DataBindings.Add(ctr_prop, objsrce, obj_prop, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        //Permet de lier le BindingSource aux champs du formulaire

        private void setMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }

        public void RefreshData()
        {
            //Chargement de la source des donnes BindingSource) en utilisant un DataTable
            _binsrc.DataSource = clsMetier.GetInstance().getAllClstbl_geopoint();
            bdNav.BindingSource = _binsrc;

            //cboAsso.DataSource = clsMetier.GetInstance().getAllClstbl_association();
            //this.setMembersallcbo(cboAsso, "association", "association");


            if (_binsrc.Count == 0)
            {
                bdSave.Enabled = false;
                bdDelete.Enabled = false;
            }


        }


        private void btnshowmap_Click(object sender, EventArgs e)
        {
            string locations = "";
            int i = 0;

         //   foreach (DataRow row in this.parcellesDataSet.Tables[0].Rows)
        // foreach (DataRow row in dtTable.Rows) 
            foreach (DataRow row in clsMetier.GetInstance().getAllClstbl_geopoint().Rows) 
            {
                i++;
                locations += string.Format("[{0}, {1}, '{2}']", 
                     row["latitude"].ToString().Replace(",", "."),
                     row["longitude"].ToString().Replace(",", "."),
                     row["deviceid"]
                    );
              //  if (i < this.parcellesDataSet.Tables[0].Rows.Count) locations += ",\n";
                //  if (i < dtTable.Rows.Count) locations += ",\n";
                if (i < clsMetier.GetInstance().getAllClstbl_geopoint().Rows.Count) locations += ",\n";
            }

            // google.load(""visualization"", ""1"", {packages:[""map""]});
            // google.setOnLoadCallback(drawChart);
            string html =
            @"<html>
  <head>

 <script type=""text/javascript"" src=""https://www.gstatic.com/charts/loader.js""></script>
    <script type=""text/javascript"">
       google.charts.load('current', { 'packages': ['map'] });
        google.charts.setOnLoadCallback(drawChart);
        
      function drawChart() {
        var data = google.visualization.arrayToDataTable([
          ['Lat', 'Long', 'Name'],DBLOCATIONS 
        ]);

        var map = new google.visualization.Map(document.getElementById('map_div'));
        map.draw(data, {showTip: true});

        var options = {
              showTooltip: true,
              showInfoWindow: true
            };

            var map = new google.visualization.Map(document.getElementById('chart_div'));

            map.draw(data, options);

      }

    </script>
  </head>

  <body>
    <div id=""map_div"" style=""width: 600px; height: 450px; margin-left: auto; margin-right: auto;""></div>
  </body>
</html>";

            html = html.Replace("DBLOCATIONS", locations);
            string fileName = "index.html";
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.Write(html);
                sw.Flush();
                sw.Close();

                System.Diagnostics.Process.Start(fileName);
            }

}















    }
}
