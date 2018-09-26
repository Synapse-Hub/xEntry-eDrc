using System;
using System.Data;

namespace xEntry_Data
{
    public class clstbl_geopoint
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private int id;
        private string uuid;
        private string deviceid;
        private string latitude;
        private string longitude;
        private string altitude;
        private string epe;
        private string geo_type;
        private DateTime synchronized_on;
        //***DataTables***
        public DataTable clstbl_geopointTables()
        {
            return clsMetier.GetInstance().getAllClstbl_geopoint();
        }
        public DataTable clstbl_geopointTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_geopoint(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_geopoint(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_geopoint(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_geopoint(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_geopoint()
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
        }  //***Accesseur de latitude***
        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }  //***Accesseur de longitude***
        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }  //***Accesseur de altitude***
        public string Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }  //***Accesseur de epe***
        public string Epe
        {
            get { return epe; }
            set { epe = value; }
        }  //***Accesseur de geo_type***
        public string Geo_type
        {
            get { return geo_type; }
            set { geo_type = value; }
        }  //***Accesseur de synchronized_on***
        public DateTime Synchronized_on
        {
            get { return synchronized_on; }
            set { synchronized_on = value; }
        }
    } //***fin class
} //***fin namespace