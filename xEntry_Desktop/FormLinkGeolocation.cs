﻿using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using ManageUtilities;
using System;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Xentry.Data;

namespace Xentry.Desktop
{
    public partial class FormLinkGeolocation : Form
    {
        BindingSource _binsrc = new BindingSource();
        private clstbl_geopoint fichegeo = new clstbl_geopoint();

        //////private bool blnModifie = false;

        //////GMarkerGoogle maker;
        //////GMapOverlay makeroverlay;
        //////DataTable dt;

        //////int selection = 0;
        //////double latitude = -6.139508; //37.4232;
        //////double longitude = 21.729240;// -122.0853;

        ResourceManager stringManager = null;

        MdiMainForm mainForm = new MdiMainForm();

        public MdiMainForm MdiMainForm
        {
            get
            {
                return mainForm;
            }
            set
            {
                mainForm = value;
            }
        }

        public FormLinkGeolocation()
        {
            InitializeComponent();
            //Initialisation des Resources
            Assembly _assembly = Assembly.Load("Xentry.Resources");
            stringManager = new ResourceManager("Xentry.Resources.XentryResource", _assembly);
        }

        private void frmLinkGeolocation_Load(object sender, EventArgs e)
        {
            //Resize the form
            //this.Width = 1714;
            //this.Height = 998;

            DataTable dtTable = null;

            try
            {
                dtTable = new DataTable();
                dtTable.Locale = CultureInfo.InvariantCulture;
                RefreshData();
                dgvGps.DataSource = _binsrc;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadDataMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadDataCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de chargement des données : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(stringManager.GetString("StringFailedLoadDataMessage", CultureInfo.CurrentUICulture), stringManager.GetString("StringFailedLoadDataCaption", CultureInfo.CurrentUICulture), MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de chargement des données : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            finally
            {
                dtTable.Dispose();
            }


            //dtTable.Columns.Add("deviceid", typeof(int));
            //dtTable.Columns.Add("latitude", typeof(String));
            //dtTable.Columns.Add("longitude", typeof(String));

            foreach (DataGridViewColumn col in dgvGps.Columns)
            {
                dtTable.Columns.Add(col.Name);
            }

          //  LoadingMap();

        }

        private void LoadingMap()
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
           // gMapControl1.Position = new PointLatLng(latitud, longitud);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;
        }

        private static void SetBindingControls(Control ctr, string ctr_prop, object objsrce, string obj_prop)
        {
            ctr.DataBindings.Clear();
            ctr.DataBindings.Add(ctr_prop, objsrce, obj_prop, true, DataSourceUpdateMode.OnPropertyChanged);
        }

        //Permet de lier le BindingSource aux champs du formulaire

        private static void SetMembersallcbo(ComboBox cbo, string displayMember, string valueMember)
        {
            cbo.DisplayMember = displayMember;
            cbo.ValueMember = valueMember;
        }

        public void RefreshData()
        {
            //Chargement de la source des donnes BindingSource) en utilisant un DataTable
            _binsrc.DataSource = clsMetier.GetInstance().getAllClstbl_geopoint();
            bdNav.BindingSource = _binsrc;

            if (_binsrc.Count == 0)
            {
                bdSave.Enabled = false;
                bdDelete.Enabled = false;
            }
        }

        private void BindingList()
        {
            SetBindingControls(txtDescription, "Text", _binsrc, "uuid");
            SetBindingControls(txtLatitude, "Text", _binsrc, "latitude");
            SetBindingControls(txtLongitude, "Text", _binsrc, "longitude");
        }

        private void BindingCls()
        {
            SetBindingControls(txtDescription, "Text", fichegeo, "uuid");
            SetBindingControls(txtLatitude, "Text", fichegeo, "latitude");
            SetBindingControls(txtLongitude, "Text", fichegeo, "longitude");
        }

        private void Localisation(double latitud, double longitud)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(latitud, longitud);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;
        }

        private void dgvGps_Click(object sender, EventArgs e)
        {
            //if (dgvGps.CurrentRow.Index >= 0 && dgvGps.CurrentRow.Index < dgvGps.Rows.Count - 1)
            //{
            //    txtLatitude.Text = dgvGps.SelectedRows[dgvGps.CurrentRow.Index].Cells["latitude"].Value.ToString();
            //    txtLongitude.Text = dgvGps.SelectedRows[dgvGps.CurrentRow.Index].Cells["longitude"].Value.ToString();
            //    Localisation(double.Parse(txtLatitude.Text), double.Parse(txtLongitude.Text));

            //}
        }

        private void dgvGps_SelectionChanged(object sender, EventArgs e)
        {
            GMapOverlay markers = null;

            try
            {
                BindingList();
                // blnModifie = true;
                // bdDelete.Enabled = true;
                Localisation(double.Parse(txtLatitude.Text, CultureInfo.CurrentCulture), double.Parse(txtLongitude.Text, CultureInfo.CurrentCulture));

                PointLatLng point = new PointLatLng(double.Parse(txtLatitude.Text, CultureInfo.CurrentCulture), double.Parse(txtLongitude.Text, CultureInfo.CurrentCulture));

                using (GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_dot))
                {
                    // 1. Create a Overlay
                    markers = new GMapOverlay("Marker");
                    // 2. Add all available markers to that Overlay
                    markers.Markers.Add(marker);
                    // 3. Covers map with Overlay
                    gMapControl1.Overlays.Add(markers);
                }
            }
            catch (ArgumentException ex)
            {
                //blnModifie = false;
                //bdDelete.Enabled = false;
                Properties.Settings.Default.StringLogFile = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Erreur de sélection dans le DataGrid : " + this.Name + " : " + ex.GetType().ToString() + " : " + ex.Message;
                ImplementLog.Instance.PutLogMessage(Properties.Settings.Default.MasterDirectory, Properties.Settings.Default.StringLogFile, Properties.Settings.Default.DirectoryUtilLog, Properties.Settings.Default.MasterDirectory + "LogFile.txt");
            }
            finally
            {
                markers.Dispose();
            }
        }

        private void bdSave_Click(object sender, EventArgs e)
        {

        }


        // *******************************************************************************************************************************

    }
}
