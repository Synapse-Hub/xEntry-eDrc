using ManageUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using xEntry_Utilities;


namespace xEntry_Data
{
    public class clsMetier
    {
        const string DirectoryUtilLog = "Log"; //***Les variables globales***
        private static string _ConnectionString, _host, _db, _user, _pwd;
        private static clsMetier Fact;
        private SqlConnection conn;
        public static string bdEnCours = "";

        #region prerecquis
        public static clsMetier GetInstance()
        {
            if (Fact == null)
                Fact = new clsMetier();
            return Fact;
        }
        private object getParameter(IDbCommand cmd, string name, DbType type, int size, object value)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.Size = size;
            param.DbType = type;
            param.ParameterName = name;
            param.Value = value;
            return param;
        }
        public void Initialize(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
            conn = new SqlConnection(ConnectionString);
        }
        public void Initialize(clsConnexion con)
        {
            _host = con.Serveur;// host;
            _db = con.DB; ;
            _user = con.User;
            _pwd = con.Pwd;
            string sch = string.Format("server={0}; database={1}; user={2}; pwd={3}", _host, _db, _user, _pwd);
            conn = new SqlConnection(sch);
        }
        public void Initialize(clsConnexion con, int type)
        {
            _host = con.Serveur;// host;
            _db = con.DB; ;
            _user = con.User;
            _pwd = con.Pwd;
            string sch = string.Format("server={0}; database={1}; user={2}; pwd={3}", _host, _db, _user, _pwd);
            switch (type)
            {
                //sql server 2005
                case 1: sch = string.Format("Data Source={0};Persist Security Info=True; Initial Catalog={1};User ID={2}; Password={3}", _host, _db, _user, _pwd); break;
                //sql server 2008 Data Source=WIN7-PC\SQLEXPRESS;Initial Catalog=bihito;Persist Security Info=True;User ID=sa;Password=sa
                case 2: sch = string.Format("Data Source={0};Persist Security Info=True; Initial Catalog={1};User ID={2}; Password={3}", _host, _db, _user, _pwd); break;
                case 3: break;
            }

            //On garde la chaine de connexion pour utilisation avec les reports
            xEntry_Data.Properties.Settings.Default.strChaineConnexion = sch;
            conn = new SqlConnection(sch);
        }
        public void Initialize(string host, string db, string user, string pwd)
        {
            _host = host;
            _db = db;
            _user = user;
            _pwd = pwd;
            string sch = string.Format("server={0}; database={1}; user={2}; pwd={3}", _host, _db, _user, _pwd);
            conn = new SqlConnection(sch);
        }
        public void setDB(string db)
        {
            _db = db;
        }
        public bool isConnect()
        {
            bool bl = true;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                conn.Close();
            }
            catch (Exception exc)
            {
                bl = false;
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Etat de la connexion à la BD sans paramètre : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return bl;
        }
        public bool isConnect(clsConnexion con)
        {
            bool bl = true;
            _host = con.Serveur;// host;
            _db = con.DB;
            _user = con.User;
            _pwd = con.Pwd;
            string sch = string.Format("server={0}; database=Master; user={1}; pwd={2}", con.Serveur, con.User, con.Pwd);
            conn = new SqlConnection(sch);
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                conn.Close();
            }
            catch (Exception exc)
            {
                sch = string.Format("server={0}; database={1};id user={2}; pwd={3}", _host, _db, _user, _pwd);
                bl = false;
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Etat connexion à la BD avec paramètre : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return bl;
        }
        public void closeConnexion()
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            catch (Exception) { }
        }
        public List<string> getAllDB()
        {
            List<string> lst = new List<string>();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT name FROM sysdatabases where name!='master' order by name");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                            lst.Add(dr["name"].ToString());
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Récupération de toutes les bases de Données SQLServer : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return lst;
        }
        public string getCurrentDataBase()
        {
            string bd = "";
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    //Sélection de la base des données en cours
                    cmd.CommandText = string.Format("SELECT DB_NAME() AS bd_encours");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bd = (dr["bd_encours"].ToString());
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Récupération de toutes les bases de Données SQLServer : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return bd;
        }
        #endregion prerecquis
        #region  CLSTBL_FICHE_MENAGE
        public clstbl_fiche_menage getClstbl_fiche_menage(object intid)
        {
            clstbl_fiche_menage varclstbl_fiche_menage = new clstbl_fiche_menage();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_menage WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_fiche_menage.Id = int.Parse(dr["id"].ToString());
                            varclstbl_fiche_menage.Uuid = dr["uuid"].ToString();
                            varclstbl_fiche_menage.Deviceid = dr["deviceid"].ToString();
                            if (!dr["date"].ToString().Trim().Equals("")) varclstbl_fiche_menage.Date = DateTime.Parse(dr["date"].ToString());
                            varclstbl_fiche_menage.Questionnaire_id = dr["questionnaire_id"].ToString();
                            varclstbl_fiche_menage.Name = dr["name"].ToString();
                            varclstbl_fiche_menage.Id_menage = dr["id_menage"].ToString();
                            varclstbl_fiche_menage.Nom_menage = dr["nom_menage"].ToString();
                            varclstbl_fiche_menage.Deuxio_representant = dr["deuxio_representant"].ToString();
                            if (!dr["taille_menage"].ToString().Trim().Equals("")) varclstbl_fiche_menage.Taille_menage = int.Parse(dr["taille_menage"].ToString());
                            varclstbl_fiche_menage.Village_menage = dr["village_menage"].ToString();
                            varclstbl_fiche_menage.Province = dr["province"].ToString();
                            varclstbl_fiche_menage.Groupement = dr["groupement"].ToString();
                            varclstbl_fiche_menage.Territoire = dr["territoire"].ToString();
                            varclstbl_fiche_menage.Zs = dr["zs"].ToString();
                            varclstbl_fiche_menage.Camps = dr["camps"].ToString();
                            varclstbl_fiche_menage.Localisation = dr["localisation"].ToString();
                            varclstbl_fiche_menage.Rpt_gps = dr["rpt_gps"].ToString();
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_fiche_menage.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_fiche_menage' avec la classe 'clstbl_fiche_menage' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_fiche_menage;
        }

        public DataTable getAllClstbl_fiche_menage(string criteria)
        {
            DataTable dtclstbl_fiche_menage = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_fiche_menage  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   deviceid LIKE '%" + criteria + "%'";
                    sql += "  OR   questionnaire_id LIKE '%" + criteria + "%'";
                    sql += "  OR   name LIKE '%" + criteria + "%'";
                    sql += "  OR   id_menage LIKE '%" + criteria + "%'";
                    sql += "  OR   nom_menage LIKE '%" + criteria + "%'";
                    sql += "  OR   deuxio_representant LIKE '%" + criteria + "%'";
                    sql += "  OR   village_menage LIKE '%" + criteria + "%'";
                    sql += "  OR   province LIKE '%" + criteria + "%'";
                    sql += "  OR   groupement LIKE '%" + criteria + "%'";
                    sql += "  OR   territoire LIKE '%" + criteria + "%'";
                    sql += "  OR   zs LIKE '%" + criteria + "%'";
                    sql += "  OR   camps LIKE '%" + criteria + "%'";
                    sql += "  OR   localisation LIKE '%" + criteria + "%'";
                    sql += "  OR   rpt_gps LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_menage.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_fiche_menage' avec la classe 'clstbl_fiche_menage' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_menage;
        }

        public DataTable getAllClstbl_fiche_menage()
        {
            DataTable dtclstbl_fiche_menage = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_menage ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_menage.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_fiche_menage' avec la classe 'clstbl_fiche_menage' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_menage;
        }

        public int insertClstbl_fiche_menage(clstbl_fiche_menage varclstbl_fiche_menage)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_fiche_menage ( uuid,deviceid,date,questionnaire_id,name,id_menage,nom_menage,deuxio_representant,taille_menage,village_menage,province,groupement,territoire,zs,camps,localisation,rpt_gps,synchronized_on ) VALUES (@uuid,@deviceid,@date,@questionnaire_id,@name,@id_menage,@nom_menage,@deuxio_representant,@taille_menage,@village_menage,@province,@groupement,@territoire,@zs,@camps,@localisation,@rpt_gps,@synchronized_on  )");
                    if (varclstbl_fiche_menage.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_menage.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_fiche_menage.Deviceid != null) cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_menage.Deviceid));
                    else cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_menage.Date));
                    if (varclstbl_fiche_menage.Questionnaire_id != null) cmd.Parameters.Add(getParameter(cmd, "@questionnaire_id", DbType.String, 50, varclstbl_fiche_menage.Questionnaire_id));
                    else cmd.Parameters.Add(getParameter(cmd, "@questionnaire_id", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Name != null) cmd.Parameters.Add(getParameter(cmd, "@name", DbType.String, 50, varclstbl_fiche_menage.Name));
                    else cmd.Parameters.Add(getParameter(cmd, "@name", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Id_menage != null) cmd.Parameters.Add(getParameter(cmd, "@id_menage", DbType.String, 50, varclstbl_fiche_menage.Id_menage));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_menage", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Nom_menage != null) cmd.Parameters.Add(getParameter(cmd, "@nom_menage", DbType.String, 50, varclstbl_fiche_menage.Nom_menage));
                    else cmd.Parameters.Add(getParameter(cmd, "@nom_menage", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Deuxio_representant != null) cmd.Parameters.Add(getParameter(cmd, "@deuxio_representant", DbType.String, 50, varclstbl_fiche_menage.Deuxio_representant));
                    else cmd.Parameters.Add(getParameter(cmd, "@deuxio_representant", DbType.String, 50, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@taille_menage", DbType.Int32, 4, varclstbl_fiche_menage.Taille_menage));
                    if (varclstbl_fiche_menage.Village_menage != null) cmd.Parameters.Add(getParameter(cmd, "@village_menage", DbType.String, 50, varclstbl_fiche_menage.Village_menage));
                    else cmd.Parameters.Add(getParameter(cmd, "@village_menage", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Province != null) cmd.Parameters.Add(getParameter(cmd, "@province", DbType.String, 50, varclstbl_fiche_menage.Province));
                    else cmd.Parameters.Add(getParameter(cmd, "@province", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Groupement != null) cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 50, varclstbl_fiche_menage.Groupement));
                    else cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Territoire != null) cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 50, varclstbl_fiche_menage.Territoire));
                    else cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Zs != null) cmd.Parameters.Add(getParameter(cmd, "@zs", DbType.String, 50, varclstbl_fiche_menage.Zs));
                    else cmd.Parameters.Add(getParameter(cmd, "@zs", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Camps != null) cmd.Parameters.Add(getParameter(cmd, "@camps", DbType.String, 50, varclstbl_fiche_menage.Camps));
                    else cmd.Parameters.Add(getParameter(cmd, "@camps", DbType.String, 50, DBNull.Value));
                    if (varclstbl_fiche_menage.Localisation != null) cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, varclstbl_fiche_menage.Localisation));
                    else cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_menage.Rpt_gps != null) cmd.Parameters.Add(getParameter(cmd, "@rpt_gps", DbType.String, 10, varclstbl_fiche_menage.Rpt_gps));
                    else cmd.Parameters.Add(getParameter(cmd, "@rpt_gps", DbType.String, 10, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_menage.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_fiche_menage' avec la classe 'clstbl_fiche_menage' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_fiche_menage(DataRowView varclstbl_fiche_menage)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_fiche_menage  SET uuid=@uuid,deviceid=@deviceid,date=@date,questionnaire_id=@questionnaire_id,name=@name,id_menage=@id_menage,nom_menage=@nom_menage,deuxio_representant=@deuxio_representant,taille_menage=@taille_menage,village_menage=@village_menage,province=@province,groupement=@groupement,territoire=@territoire,zs=@zs,camps=@camps,localisation=@localisation,rpt_gps=@rpt_gps,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_menage["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_menage["deviceid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_menage["date"]));
                    cmd.Parameters.Add(getParameter(cmd, "@questionnaire_id", DbType.String, 50, varclstbl_fiche_menage["questionnaire_id"]));
                    cmd.Parameters.Add(getParameter(cmd, "@name", DbType.String, 50, varclstbl_fiche_menage["name"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_menage", DbType.String, 50, varclstbl_fiche_menage["id_menage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nom_menage", DbType.String, 50, varclstbl_fiche_menage["nom_menage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@deuxio_representant", DbType.String, 50, varclstbl_fiche_menage["deuxio_representant"]));
                    cmd.Parameters.Add(getParameter(cmd, "@taille_menage", DbType.Int32, 4, varclstbl_fiche_menage["taille_menage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@village_menage", DbType.String, 50, varclstbl_fiche_menage["village_menage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@province", DbType.String, 50, varclstbl_fiche_menage["province"]));
                    cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 50, varclstbl_fiche_menage["groupement"]));
                    cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 50, varclstbl_fiche_menage["territoire"]));
                    cmd.Parameters.Add(getParameter(cmd, "@zs", DbType.String, 50, varclstbl_fiche_menage["zs"]));
                    cmd.Parameters.Add(getParameter(cmd, "@camps", DbType.String, 50, varclstbl_fiche_menage["camps"]));
                    cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, varclstbl_fiche_menage["localisation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@rpt_gps", DbType.String, 10, varclstbl_fiche_menage["rpt_gps"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_menage["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_fiche_menage["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_fiche_menage' avec la classe 'clstbl_fiche_menage' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_fiche_menage(DataRowView varclstbl_fiche_menage)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_fiche_menage  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_fiche_menage["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_fiche_menage' avec la classe 'clstbl_fiche_menage' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_FICHE_MENAGE 
        #region  CLSTBL_LOCALISATION_POLY
        public clstbl_localisation_poly getClstbl_localisation_poly(object intid)
        {
            clstbl_localisation_poly varclstbl_localisation_poly = new clstbl_localisation_poly();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_localisation_poly WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_localisation_poly.Id = int.Parse(dr["id"].ToString());
                            varclstbl_localisation_poly.Uuid = dr["uuid"].ToString();
                            varclstbl_localisation_poly.Name_point = dr["name_point"].ToString();
                            varclstbl_localisation_poly.Localisation_poly = dr["localisation_poly"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_localisation_poly' avec la classe 'clstbl_localisation_poly' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_localisation_poly;
        }

        public DataTable getAllClstbl_localisation_poly(string criteria)
        {
            DataTable dtclstbl_localisation_poly = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_localisation_poly  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   name_point LIKE '%" + criteria + "%'";
                    sql += "  OR   localisation_poly LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_localisation_poly.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_localisation_poly' avec la classe 'clstbl_localisation_poly' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_localisation_poly;
        }

        public DataTable getAllClstbl_localisation_poly()
        {
            DataTable dtclstbl_localisation_poly = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_localisation_poly ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_localisation_poly.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_localisation_poly' avec la classe 'clstbl_localisation_poly' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_localisation_poly;
        }

        public int insertClstbl_localisation_poly(clstbl_localisation_poly varclstbl_localisation_poly)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_localisation_poly ( uuid,name_point,localisation_poly ) VALUES (@uuid,@name_point,@localisation_poly  )");
                    if (varclstbl_localisation_poly.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_localisation_poly.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_localisation_poly.Name_point != null) cmd.Parameters.Add(getParameter(cmd, "@name_point", DbType.String, 50, varclstbl_localisation_poly.Name_point));
                    else cmd.Parameters.Add(getParameter(cmd, "@name_point", DbType.String, 50, DBNull.Value));
                    if (varclstbl_localisation_poly.Localisation_poly != null) cmd.Parameters.Add(getParameter(cmd, "@localisation_poly", DbType.String, 255, varclstbl_localisation_poly.Localisation_poly));
                    else cmd.Parameters.Add(getParameter(cmd, "@localisation_poly", DbType.String, 255, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_localisation_poly' avec la classe 'clstbl_localisation_poly' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_localisation_poly(DataRowView varclstbl_localisation_poly)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_localisation_poly  SET uuid=@uuid,name_point=@name_point,localisation_poly=@localisation_poly  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_localisation_poly["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@name_point", DbType.String, 50, varclstbl_localisation_poly["name_point"]));
                    cmd.Parameters.Add(getParameter(cmd, "@localisation_poly", DbType.String, 255, varclstbl_localisation_poly["localisation_poly"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_localisation_poly["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_localisation_poly' avec la classe 'clstbl_localisation_poly' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_localisation_poly(DataRowView varclstbl_localisation_poly)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_localisation_poly  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_localisation_poly["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_localisation_poly' avec la classe 'clstbl_localisation_poly' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_LOCALISATION_POLY 
        #region  CLSTBL_FICHE_PR
        public clstbl_fiche_pr getClstbl_fiche_pr(object intid)
        {
            clstbl_fiche_pr varclstbl_fiche_pr = new clstbl_fiche_pr();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_pr WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Id = int.Parse(dr["id"].ToString());
                            varclstbl_fiche_pr.Uuid = dr["uuid"].ToString();
                            varclstbl_fiche_pr.Deviceid = dr["deviceid"].ToString();
                            if (!dr["date"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Date = DateTime.Parse(dr["date"].ToString());
                            varclstbl_fiche_pr.Nom_agent = dr["nom_agent"].ToString();
                            varclstbl_fiche_pr.Saison = dr["saison"].ToString();
                            varclstbl_fiche_pr.Association = dr["association"].ToString();
                            varclstbl_fiche_pr.Association_autre = dr["association_autre"].ToString();
                            varclstbl_fiche_pr.Bailleur = dr["bailleur"].ToString();
                            varclstbl_fiche_pr.Bailleur_autre = dr["bailleur_autre"].ToString();
                            varclstbl_fiche_pr.N_visite = dr["n_visite"].ToString();
                            varclstbl_fiche_pr.Contreverification = dr["contreverification"].ToString();
                            if (!dr["n_visite_2"].ToString().Trim().Equals("")) varclstbl_fiche_pr.N_visite_2 = int.Parse(dr["n_visite_2"].ToString());
                            if (!dr["n_viste_3"].ToString().Trim().Equals("")) varclstbl_fiche_pr.N_viste_3 = int.Parse(dr["n_viste_3"].ToString());
                            if (!dr["visite_calculate"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Visite_calculate = int.Parse(dr["visite_calculate"].ToString());
                            varclstbl_fiche_pr.N_plantation = dr["n_plantation"].ToString();
                            varclstbl_fiche_pr.N_bloc = dr["n_bloc"].ToString();
                            varclstbl_fiche_pr.Noms_planteur = dr["noms_planteur"].ToString();
                            varclstbl_fiche_pr.Nom = dr["nom"].ToString();
                            varclstbl_fiche_pr.Post_nom = dr["post_nom"].ToString();
                            varclstbl_fiche_pr.Prenom = dr["prenom"].ToString();
                            varclstbl_fiche_pr.Sexes = dr["sexes"].ToString();
                            varclstbl_fiche_pr.Planteur_present = dr["planteur_present"].ToString();
                            varclstbl_fiche_pr.Changement_surface = dr["changement_surface"].ToString();
                            if (!dr["photo_fiche"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Photo_fiche = (Byte[])dr["photo_fiche"];
                            varclstbl_fiche_pr.Titre_trace_gps = dr["titre_trace_gps"].ToString();
                            if (!dr["superficie"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Superficie = double.Parse(dr["superficie"].ToString());
                            if (!dr["superficie_non_plantee"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Superficie_non_plantee = double.Parse(dr["superficie_non_plantee"].ToString());
                            if (!dr["photo_inventaire"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Photo_inventaire = (Byte[])dr["photo_inventaire"];
                            varclstbl_fiche_pr.Periode_debut = dr["periode_debut"].ToString();
                            varclstbl_fiche_pr.Preiode_debut_annee = dr["preiode_debut_annee"].ToString();
                            varclstbl_fiche_pr.Periode_fin = dr["periode_fin"].ToString();
                            varclstbl_fiche_pr.Period_fin_annee = dr["period_fin_annee"].ToString();
                            varclstbl_fiche_pr.Essence_principale = dr["essence_principale"].ToString();
                            varclstbl_fiche_pr.Essence_principale_autre = dr["essence_principale_autre"].ToString();
                            varclstbl_fiche_pr.Melanges = dr["melanges"].ToString();
                            varclstbl_fiche_pr.Rpt_b = dr["rpt_b"].ToString();
                            if (!dr["pente_1"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Pente_1 = int.Parse(dr["pente_1"].ToString());
                            if (!dr["pente_2"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Pente_2 = int.Parse(dr["pente_2"].ToString());
                            if (!dr["pente_3"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Pente_3 = int.Parse(dr["pente_3"].ToString());
                            if (!dr["pente_4"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Pente_4 = int.Parse(dr["pente_4"].ToString());
                            varclstbl_fiche_pr.Encartement_type = dr["encartement_type"].ToString();
                            varclstbl_fiche_pr.Ecartement_dim_1 = dr["ecartement_dim_1"].ToString();
                            varclstbl_fiche_pr.Ecartement_dim_2 = dr["ecartement_dim_2"].ToString();
                            varclstbl_fiche_pr.Alignement = dr["alignement"].ToString();
                            varclstbl_fiche_pr.Causes = dr["causes"].ToString();
                            varclstbl_fiche_pr.Piquets = dr["piquets"].ToString();
                            varclstbl_fiche_pr.Pourcentage_insuffisants = dr["pourcentage_insuffisants"].ToString();
                            varclstbl_fiche_pr.Eucalyptus_deau = dr["eucalyptus_deau"].ToString();
                            if (!dr["n_vides"].ToString().Trim().Equals("")) varclstbl_fiche_pr.N_vides = int.Parse(dr["n_vides"].ToString());
                            if (!dr["n_zero_demi_metre"].ToString().Trim().Equals("")) varclstbl_fiche_pr.N_zero_demi_metre = int.Parse(dr["n_zero_demi_metre"].ToString());
                            if (!dr["n_demi_un_metre"].ToString().Trim().Equals("")) varclstbl_fiche_pr.N_demi_un_metre = int.Parse(dr["n_demi_un_metre"].ToString());
                            if (!dr["n_un_deux_metre"].ToString().Trim().Equals("")) varclstbl_fiche_pr.N_un_deux_metre = int.Parse(dr["n_un_deux_metre"].ToString());
                            if (!dr["n_deux_plus_metre"].ToString().Trim().Equals("")) varclstbl_fiche_pr.N_deux_plus_metre = int.Parse(dr["n_deux_plus_metre"].ToString());
                            if (!dr["p_zero_demi_metre_calc"].ToString().Trim().Equals("")) varclstbl_fiche_pr.P_zero_demi_metre_calc = int.Parse(dr["p_zero_demi_metre_calc"].ToString());
                            if (!dr["p_demi_un_metre_calc"].ToString().Trim().Equals("")) varclstbl_fiche_pr.P_demi_un_metre_calc = int.Parse(dr["p_demi_un_metre_calc"].ToString());
                            if (!dr["p_un_deux_metre_calc"].ToString().Trim().Equals("")) varclstbl_fiche_pr.P_un_deux_metre_calc = int.Parse(dr["p_un_deux_metre_calc"].ToString());
                            if (!dr["p_deux_plus_metre_calc"].ToString().Trim().Equals("")) varclstbl_fiche_pr.P_deux_plus_metre_calc = int.Parse(dr["p_deux_plus_metre_calc"].ToString());
                            if (!dr["degats_calc"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Degats_calc = int.Parse(dr["degats_calc"].ToString());
                            varclstbl_fiche_pr.Type_degats = dr["type_degats"].ToString();
                            varclstbl_fiche_pr.N_vaches = dr["n_vaches"].ToString();
                            varclstbl_fiche_pr.N_chevres = dr["n_chevres"].ToString();
                            varclstbl_fiche_pr.N_rats = dr["n_rats"].ToString();
                            varclstbl_fiche_pr.N_termites = dr["n_termites"].ToString();
                            varclstbl_fiche_pr.N_elephants = dr["n_elephants"].ToString();
                            varclstbl_fiche_pr.N_cultures_vivrieres = dr["n_cultures_vivrieres"].ToString();
                            varclstbl_fiche_pr.N_erosion = dr["n_erosion"].ToString();
                            varclstbl_fiche_pr.N_eboulement = dr["n_eboulement"].ToString();
                            varclstbl_fiche_pr.N_feu = dr["n_feu"].ToString();
                            varclstbl_fiche_pr.N_secheresse = dr["n_secheresse"].ToString();
                            varclstbl_fiche_pr.N_hommes = dr["n_hommes"].ToString();
                            varclstbl_fiche_pr.N_plante_avec_sachets = dr["n_plante_avec_sachets"].ToString();
                            varclstbl_fiche_pr.N_plante_trop_tard = dr["n_plante_trop_tard"].ToString();
                            varclstbl_fiche_pr.N_guerren = dr["n_guerren"].ToString();
                            if (!dr["degats_total"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Degats_total = int.Parse(dr["degats_total"].ToString());
                            varclstbl_fiche_pr.Regarnissage = dr["regarnissage"].ToString();
                            varclstbl_fiche_pr.Regarnissage_suffisant = dr["regarnissage_suffisant"].ToString();
                            varclstbl_fiche_pr.Entretien = dr["entretien"].ToString();
                            varclstbl_fiche_pr.Etat = dr["etat"].ToString();
                            varclstbl_fiche_pr.Cultures_vivrieres = dr["cultures_vivrieres"].ToString();
                            varclstbl_fiche_pr.Type_cultures_vivieres = dr["type_cultures_vivieres"].ToString();
                            varclstbl_fiche_pr.Type_cultures_vivieres_autr = dr["type_cultures_vivieres_autr"].ToString();
                            varclstbl_fiche_pr.N_haricots = dr["n_haricots"].ToString();
                            varclstbl_fiche_pr.N_manioc = dr["n_manioc"].ToString();
                            varclstbl_fiche_pr.N_soja = dr["n_soja"].ToString();
                            varclstbl_fiche_pr.N_sorgho = dr["n_sorgho"].ToString();
                            varclstbl_fiche_pr.N_arachides = dr["n_arachides"].ToString();
                            varclstbl_fiche_pr.N_patates_douces = dr["n_patates_douces"].ToString();
                            varclstbl_fiche_pr.N_mais = dr["n_mais"].ToString();
                            varclstbl_fiche_pr.N_autres = dr["n_autres"].ToString();
                            if (!dr["type_cultures_total"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Type_cultures_total = int.Parse(dr["type_cultures_total"].ToString());
                            varclstbl_fiche_pr.Canopee_fermee = dr["canopee_fermee"].ToString();
                            if (!dr["superficie_canopee_fermee"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Superficie_canopee_fermee = double.Parse(dr["superficie_canopee_fermee"].ToString());
                            varclstbl_fiche_pr.Croissance_arbres = dr["croissance_arbres"].ToString();
                            varclstbl_fiche_pr.Arbres_existants = dr["arbres_existants"].ToString();
                            varclstbl_fiche_pr.Rpt_c = dr["rpt_c"].ToString();
                            if (!dr["photo_1"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Photo_1 = (Byte[])dr["photo_1"];
                            varclstbl_fiche_pr.Emplacement = dr["emplacement"].ToString();
                            varclstbl_fiche_pr.Photo_2 = dr["photo_2"].ToString();
                            varclstbl_fiche_pr.Emplacement_2 = dr["emplacement_2"].ToString();
                            varclstbl_fiche_pr.Localisation = dr["localisation"].ToString();
                            varclstbl_fiche_pr.Commentaire_wwf = dr["commentaire_wwf"].ToString();
                            varclstbl_fiche_pr.Commentaire_planteur = dr["commentaire_planteur"].ToString();
                            varclstbl_fiche_pr.Commentaire_association = dr["commentaire_association"].ToString();
                            varclstbl_fiche_pr.Eucalyptus_deau_non = dr["eucalyptus_deau_non"].ToString();
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_fiche_pr.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_fiche_pr' avec la classe 'clstbl_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_fiche_pr;
        }

        public DataTable getAllClstbl_fiche_pr(string criteria)
        {
            DataTable dtclstbl_fiche_pr = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_fiche_pr  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   deviceid LIKE '%" + criteria + "%'";
                    sql += "  OR   nom_agent LIKE '%" + criteria + "%'";
                    sql += "  OR   saison LIKE '%" + criteria + "%'";
                    sql += "  OR   association LIKE '%" + criteria + "%'";
                    sql += "  OR   association_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   n_visite LIKE '%" + criteria + "%'";
                    sql += "  OR   contreverification LIKE '%" + criteria + "%'";
                    sql += "  OR   n_plantation LIKE '%" + criteria + "%'";
                    sql += "  OR   n_bloc LIKE '%" + criteria + "%'";
                    sql += "  OR   noms_planteur LIKE '%" + criteria + "%'";
                    sql += "  OR   nom LIKE '%" + criteria + "%'";
                    sql += "  OR   post_nom LIKE '%" + criteria + "%'";
                    sql += "  OR   prenom LIKE '%" + criteria + "%'";
                    sql += "  OR   sexes LIKE '%" + criteria + "%'";
                    sql += "  OR   planteur_present LIKE '%" + criteria + "%'";
                    sql += "  OR   changement_surface LIKE '%" + criteria + "%'";
                    sql += "  OR   titre_trace_gps LIKE '%" + criteria + "%'";
                    sql += "  OR   periode_debut LIKE '%" + criteria + "%'";
                    sql += "  OR   preiode_debut_annee LIKE '%" + criteria + "%'";
                    sql += "  OR   periode_fin LIKE '%" + criteria + "%'";
                    sql += "  OR   period_fin_annee LIKE '%" + criteria + "%'";
                    sql += "  OR   essence_principale LIKE '%" + criteria + "%'";
                    sql += "  OR   essence_principale_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   melanges LIKE '%" + criteria + "%'";
                    sql += "  OR   rpt_b LIKE '%" + criteria + "%'";
                    sql += "  OR   encartement_type LIKE '%" + criteria + "%'";
                    sql += "  OR   ecartement_dim_1 LIKE '%" + criteria + "%'";
                    sql += "  OR   ecartement_dim_2 LIKE '%" + criteria + "%'";
                    sql += "  OR   alignement LIKE '%" + criteria + "%'";
                    sql += "  OR   causes LIKE '%" + criteria + "%'";
                    sql += "  OR   piquets LIKE '%" + criteria + "%'";
                    sql += "  OR   pourcentage_insuffisants LIKE '%" + criteria + "%'";
                    sql += "  OR   eucalyptus_deau LIKE '%" + criteria + "%'";
                    sql += "  OR   type_degats LIKE '%" + criteria + "%'";
                    sql += "  OR   n_vaches LIKE '%" + criteria + "%'";
                    sql += "  OR   n_chevres LIKE '%" + criteria + "%'";
                    sql += "  OR   n_rats LIKE '%" + criteria + "%'";
                    sql += "  OR   n_termites LIKE '%" + criteria + "%'";
                    sql += "  OR   n_elephants LIKE '%" + criteria + "%'";
                    sql += "  OR   n_cultures_vivrieres LIKE '%" + criteria + "%'";
                    sql += "  OR   n_erosion LIKE '%" + criteria + "%'";
                    sql += "  OR   n_eboulement LIKE '%" + criteria + "%'";
                    sql += "  OR   n_feu LIKE '%" + criteria + "%'";
                    sql += "  OR   n_secheresse LIKE '%" + criteria + "%'";
                    sql += "  OR   n_hommes LIKE '%" + criteria + "%'";
                    sql += "  OR   n_plante_avec_sachets LIKE '%" + criteria + "%'";
                    sql += "  OR   n_plante_trop_tard LIKE '%" + criteria + "%'";
                    sql += "  OR   n_guerren LIKE '%" + criteria + "%'";
                    sql += "  OR   regarnissage LIKE '%" + criteria + "%'";
                    sql += "  OR   regarnissage_suffisant LIKE '%" + criteria + "%'";
                    sql += "  OR   entretien LIKE '%" + criteria + "%'";
                    sql += "  OR   etat LIKE '%" + criteria + "%'";
                    sql += "  OR   cultures_vivrieres LIKE '%" + criteria + "%'";
                    sql += "  OR   type_cultures_vivieres LIKE '%" + criteria + "%'";
                    sql += "  OR   type_cultures_vivieres_autr LIKE '%" + criteria + "%'";
                    sql += "  OR   n_haricots LIKE '%" + criteria + "%'";
                    sql += "  OR   n_manioc LIKE '%" + criteria + "%'";
                    sql += "  OR   n_soja LIKE '%" + criteria + "%'";
                    sql += "  OR   n_sorgho LIKE '%" + criteria + "%'";
                    sql += "  OR   n_arachides LIKE '%" + criteria + "%'";
                    sql += "  OR   n_patates_douces LIKE '%" + criteria + "%'";
                    sql += "  OR   n_mais LIKE '%" + criteria + "%'";
                    sql += "  OR   n_autres LIKE '%" + criteria + "%'";
                    sql += "  OR   canopee_fermee LIKE '%" + criteria + "%'";
                    sql += "  OR   croissance_arbres LIKE '%" + criteria + "%'";
                    sql += "  OR   arbres_existants LIKE '%" + criteria + "%'";
                    sql += "  OR   rpt_c LIKE '%" + criteria + "%'";
                    sql += "  OR   emplacement LIKE '%" + criteria + "%'";
                    sql += "  OR   photo_2 LIKE '%" + criteria + "%'";
                    sql += "  OR   emplacement_2 LIKE '%" + criteria + "%'";
                    sql += "  OR   localisation LIKE '%" + criteria + "%'";
                    sql += "  OR   commentaire_wwf LIKE '%" + criteria + "%'";
                    sql += "  OR   commentaire_planteur LIKE '%" + criteria + "%'";
                    sql += "  OR   commentaire_association LIKE '%" + criteria + "%'";
                    sql += "  OR   eucalyptus_deau_non LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_pr.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_fiche_pr' avec la classe 'clstbl_fiche_pr' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_pr;
        }

        public DataTable getAllClstbl_fiche_pr()
        {
            DataTable dtclstbl_fiche_pr = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_pr ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_pr.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_fiche_pr' avec la classe 'clstbl_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_pr;
        }

        public int insertClstbl_fiche_pr(clstbl_fiche_pr varclstbl_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_fiche_pr ( uuid,deviceid,date,nom_agent,saison,association,association_autre,bailleur,bailleur_autre,n_visite,contreverification,n_visite_2,n_viste_3,visite_calculate,n_plantation,n_bloc,noms_planteur,nom,post_nom,prenom,sexes,planteur_present,changement_surface,photo_fiche,titre_trace_gps,superficie,superficie_non_plantee,photo_inventaire,periode_debut,preiode_debut_annee,periode_fin,period_fin_annee,essence_principale,essence_principale_autre,melanges,rpt_b,pente_1,pente_2,pente_3,pente_4,encartement_type,ecartement_dim_1,ecartement_dim_2,alignement,causes,piquets,pourcentage_insuffisants,eucalyptus_deau,n_vides,n_zero_demi_metre,n_demi_un_metre,n_un_deux_metre,n_deux_plus_metre,p_zero_demi_metre_calc,p_demi_un_metre_calc,p_un_deux_metre_calc,p_deux_plus_metre_calc,degats_calc,type_degats,n_vaches,n_chevres,n_rats,n_termites,n_elephants,n_cultures_vivrieres,n_erosion,n_eboulement,n_feu,n_secheresse,n_hommes,n_plante_avec_sachets,n_plante_trop_tard,n_guerren,degats_total,regarnissage,regarnissage_suffisant,entretien,etat,cultures_vivrieres,type_cultures_vivieres,type_cultures_vivieres_autr,n_haricots,n_manioc,n_soja,n_sorgho,n_arachides,n_patates_douces,n_mais,n_autres,type_cultures_total,canopee_fermee,superficie_canopee_fermee,croissance_arbres,arbres_existants,rpt_c,photo_1,emplacement,photo_2,emplacement_2,localisation,commentaire_wwf,commentaire_planteur,commentaire_association,eucalyptus_deau_non,synchronized_on ) VALUES (@uuid,@deviceid,@date,@nom_agent,@saison,@association,@association_autre,@bailleur,@bailleur_autre,@n_visite,@contreverification,@n_visite_2,@n_viste_3,@visite_calculate,@n_plantation,@n_bloc,@noms_planteur,@nom,@post_nom,@prenom,@sexes,@planteur_present,@changement_surface,@photo_fiche,@titre_trace_gps,@superficie,@superficie_non_plantee,@photo_inventaire,@periode_debut,@preiode_debut_annee,@periode_fin,@period_fin_annee,@essence_principale,@essence_principale_autre,@melanges,@rpt_b,@pente_1,@pente_2,@pente_3,@pente_4,@encartement_type,@ecartement_dim_1,@ecartement_dim_2,@alignement,@causes,@piquets,@pourcentage_insuffisants,@eucalyptus_deau,@n_vides,@n_zero_demi_metre,@n_demi_un_metre,@n_un_deux_metre,@n_deux_plus_metre,@p_zero_demi_metre_calc,@p_demi_un_metre_calc,@p_un_deux_metre_calc,@p_deux_plus_metre_calc,@degats_calc,@type_degats,@n_vaches,@n_chevres,@n_rats,@n_termites,@n_elephants,@n_cultures_vivrieres,@n_erosion,@n_eboulement,@n_feu,@n_secheresse,@n_hommes,@n_plante_avec_sachets,@n_plante_trop_tard,@n_guerren,@degats_total,@regarnissage,@regarnissage_suffisant,@entretien,@etat,@cultures_vivrieres,@type_cultures_vivieres,@type_cultures_vivieres_autr,@n_haricots,@n_manioc,@n_soja,@n_sorgho,@n_arachides,@n_patates_douces,@n_mais,@n_autres,@type_cultures_total,@canopee_fermee,@superficie_canopee_fermee,@croissance_arbres,@arbres_existants,@rpt_c,@photo_1,@emplacement,@photo_2,@emplacement_2,@localisation,@commentaire_wwf,@commentaire_planteur,@commentaire_association,@eucalyptus_deau_non,@synchronized_on  )");
                    if (varclstbl_fiche_pr.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_pr.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_fiche_pr.Deviceid != null) cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_pr.Deviceid));
                    else cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_pr.Date));
                    if (varclstbl_fiche_pr.Nom_agent != null) cmd.Parameters.Add(getParameter(cmd, "@nom_agent", DbType.String, 255, varclstbl_fiche_pr.Nom_agent));
                    else cmd.Parameters.Add(getParameter(cmd, "@nom_agent", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Saison != null) cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, varclstbl_fiche_pr.Saison));
                    else cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Association != null) cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, varclstbl_fiche_pr.Association));
                    else cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Association_autre != null) cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, varclstbl_fiche_pr.Association_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Bailleur != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, varclstbl_fiche_pr.Bailleur));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Bailleur_autre != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, varclstbl_fiche_pr.Bailleur_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_visite != null) cmd.Parameters.Add(getParameter(cmd, "@n_visite", DbType.String, 255, varclstbl_fiche_pr.N_visite));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_visite", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Contreverification != null) cmd.Parameters.Add(getParameter(cmd, "@contreverification", DbType.String, 255, varclstbl_fiche_pr.Contreverification));
                    else cmd.Parameters.Add(getParameter(cmd, "@contreverification", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_visite_2.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_visite_2", DbType.Int32, 4, varclstbl_fiche_pr.N_visite_2));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_visite_2", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.N_viste_3.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_viste_3", DbType.Int32, 4, varclstbl_fiche_pr.N_viste_3));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_viste_3", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Visite_calculate.HasValue) cmd.Parameters.Add(getParameter(cmd, "@visite_calculate", DbType.Int32, 4, varclstbl_fiche_pr.Visite_calculate));
                    else cmd.Parameters.Add(getParameter(cmd, "@visite_calculate", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.N_plantation != null) cmd.Parameters.Add(getParameter(cmd, "@n_plantation", DbType.String, 255, varclstbl_fiche_pr.N_plantation));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_plantation", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_bloc != null) cmd.Parameters.Add(getParameter(cmd, "@n_bloc", DbType.String, 255, varclstbl_fiche_pr.N_bloc));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_bloc", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Noms_planteur != null) cmd.Parameters.Add(getParameter(cmd, "@noms_planteur", DbType.String, 255, varclstbl_fiche_pr.Noms_planteur));
                    else cmd.Parameters.Add(getParameter(cmd, "@noms_planteur", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Nom != null) cmd.Parameters.Add(getParameter(cmd, "@nom", DbType.String, 255, varclstbl_fiche_pr.Nom));
                    else cmd.Parameters.Add(getParameter(cmd, "@nom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Post_nom != null) cmd.Parameters.Add(getParameter(cmd, "@post_nom", DbType.String, 255, varclstbl_fiche_pr.Post_nom));
                    else cmd.Parameters.Add(getParameter(cmd, "@post_nom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Prenom != null) cmd.Parameters.Add(getParameter(cmd, "@prenom", DbType.String, 255, varclstbl_fiche_pr.Prenom));
                    else cmd.Parameters.Add(getParameter(cmd, "@prenom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Sexes != null) cmd.Parameters.Add(getParameter(cmd, "@sexes", DbType.String, 15, varclstbl_fiche_pr.Sexes));
                    else cmd.Parameters.Add(getParameter(cmd, "@sexes", DbType.String, 15, DBNull.Value));
                    if (varclstbl_fiche_pr.Planteur_present != null) cmd.Parameters.Add(getParameter(cmd, "@planteur_present", DbType.String, 255, varclstbl_fiche_pr.Planteur_present));
                    else cmd.Parameters.Add(getParameter(cmd, "@planteur_present", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Changement_surface != null) cmd.Parameters.Add(getParameter(cmd, "@changement_surface", DbType.String, 255, varclstbl_fiche_pr.Changement_surface));
                    else cmd.Parameters.Add(getParameter(cmd, "@changement_surface", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Photo_fiche != null) cmd.Parameters.Add(getParameter(cmd, "@photo_fiche", DbType.Binary, Int32.MaxValue, varclstbl_fiche_pr.Photo_fiche));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo_fiche", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    if (varclstbl_fiche_pr.Titre_trace_gps != null) cmd.Parameters.Add(getParameter(cmd, "@titre_trace_gps", DbType.String, 255, varclstbl_fiche_pr.Titre_trace_gps));
                    else cmd.Parameters.Add(getParameter(cmd, "@titre_trace_gps", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Superficie.HasValue) cmd.Parameters.Add(getParameter(cmd, "@superficie", DbType.Single, 4, varclstbl_fiche_pr.Superficie));
                    else cmd.Parameters.Add(getParameter(cmd, "@superficie", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Superficie_non_plantee.HasValue) cmd.Parameters.Add(getParameter(cmd, "@superficie_non_plantee", DbType.Single, 4, varclstbl_fiche_pr.Superficie_non_plantee));
                    else cmd.Parameters.Add(getParameter(cmd, "@superficie_non_plantee", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Photo_inventaire != null) cmd.Parameters.Add(getParameter(cmd, "@photo_inventaire", DbType.Binary, Int32.MaxValue, varclstbl_fiche_pr.Photo_inventaire));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo_inventaire", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    if (varclstbl_fiche_pr.Periode_debut != null) cmd.Parameters.Add(getParameter(cmd, "@periode_debut", DbType.String, 255, varclstbl_fiche_pr.Periode_debut));
                    else cmd.Parameters.Add(getParameter(cmd, "@periode_debut", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Preiode_debut_annee != null) cmd.Parameters.Add(getParameter(cmd, "@preiode_debut_annee", DbType.String, 255, varclstbl_fiche_pr.Preiode_debut_annee));
                    else cmd.Parameters.Add(getParameter(cmd, "@preiode_debut_annee", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Periode_fin != null) cmd.Parameters.Add(getParameter(cmd, "@periode_fin", DbType.String, 255, varclstbl_fiche_pr.Periode_fin));
                    else cmd.Parameters.Add(getParameter(cmd, "@periode_fin", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Period_fin_annee != null) cmd.Parameters.Add(getParameter(cmd, "@period_fin_annee", DbType.String, 255, varclstbl_fiche_pr.Period_fin_annee));
                    else cmd.Parameters.Add(getParameter(cmd, "@period_fin_annee", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Essence_principale != null) cmd.Parameters.Add(getParameter(cmd, "@essence_principale", DbType.String, 255, varclstbl_fiche_pr.Essence_principale));
                    else cmd.Parameters.Add(getParameter(cmd, "@essence_principale", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Essence_principale_autre != null) cmd.Parameters.Add(getParameter(cmd, "@essence_principale_autre", DbType.String, 255, varclstbl_fiche_pr.Essence_principale_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@essence_principale_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Melanges != null) cmd.Parameters.Add(getParameter(cmd, "@melanges", DbType.String, 255, varclstbl_fiche_pr.Melanges));
                    else cmd.Parameters.Add(getParameter(cmd, "@melanges", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Rpt_b != null) cmd.Parameters.Add(getParameter(cmd, "@rpt_b", DbType.String, 10, varclstbl_fiche_pr.Rpt_b));
                    else cmd.Parameters.Add(getParameter(cmd, "@rpt_b", DbType.String, 10, DBNull.Value));
                    if (varclstbl_fiche_pr.Pente_1.HasValue) cmd.Parameters.Add(getParameter(cmd, "@pente_1", DbType.Int32, 4, varclstbl_fiche_pr.Pente_1));
                    else cmd.Parameters.Add(getParameter(cmd, "@pente_1", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Pente_2.HasValue) cmd.Parameters.Add(getParameter(cmd, "@pente_2", DbType.Int32, 4, varclstbl_fiche_pr.Pente_2));
                    else cmd.Parameters.Add(getParameter(cmd, "@pente_2", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Pente_3.HasValue) cmd.Parameters.Add(getParameter(cmd, "@pente_3", DbType.Int32, 4, varclstbl_fiche_pr.Pente_3));
                    else cmd.Parameters.Add(getParameter(cmd, "@pente_3", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Pente_4.HasValue) cmd.Parameters.Add(getParameter(cmd, "@pente_4", DbType.Int32, 4, varclstbl_fiche_pr.Pente_4));
                    else cmd.Parameters.Add(getParameter(cmd, "@pente_4", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Encartement_type != null) cmd.Parameters.Add(getParameter(cmd, "@encartement_type", DbType.String, 255, varclstbl_fiche_pr.Encartement_type));
                    else cmd.Parameters.Add(getParameter(cmd, "@encartement_type", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Ecartement_dim_1 != null) cmd.Parameters.Add(getParameter(cmd, "@ecartement_dim_1", DbType.String, 255, varclstbl_fiche_pr.Ecartement_dim_1));
                    else cmd.Parameters.Add(getParameter(cmd, "@ecartement_dim_1", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Ecartement_dim_2 != null) cmd.Parameters.Add(getParameter(cmd, "@ecartement_dim_2", DbType.String, 255, varclstbl_fiche_pr.Ecartement_dim_2));
                    else cmd.Parameters.Add(getParameter(cmd, "@ecartement_dim_2", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Alignement != null) cmd.Parameters.Add(getParameter(cmd, "@alignement", DbType.String, 255, varclstbl_fiche_pr.Alignement));
                    else cmd.Parameters.Add(getParameter(cmd, "@alignement", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Causes != null) cmd.Parameters.Add(getParameter(cmd, "@causes", DbType.String, 255, varclstbl_fiche_pr.Causes));
                    else cmd.Parameters.Add(getParameter(cmd, "@causes", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Piquets != null) cmd.Parameters.Add(getParameter(cmd, "@piquets", DbType.String, 255, varclstbl_fiche_pr.Piquets));
                    else cmd.Parameters.Add(getParameter(cmd, "@piquets", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Pourcentage_insuffisants != null) cmd.Parameters.Add(getParameter(cmd, "@pourcentage_insuffisants", DbType.String, 255, varclstbl_fiche_pr.Pourcentage_insuffisants));
                    else cmd.Parameters.Add(getParameter(cmd, "@pourcentage_insuffisants", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Eucalyptus_deau != null) cmd.Parameters.Add(getParameter(cmd, "@eucalyptus_deau", DbType.String, 255, varclstbl_fiche_pr.Eucalyptus_deau));
                    else cmd.Parameters.Add(getParameter(cmd, "@eucalyptus_deau", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_vides.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_vides", DbType.Int32, 4, varclstbl_fiche_pr.N_vides));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_vides", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.N_zero_demi_metre.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_zero_demi_metre", DbType.Int32, 4, varclstbl_fiche_pr.N_zero_demi_metre));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_zero_demi_metre", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.N_demi_un_metre.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_demi_un_metre", DbType.Int32, 4, varclstbl_fiche_pr.N_demi_un_metre));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_demi_un_metre", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.N_un_deux_metre.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_un_deux_metre", DbType.Int32, 4, varclstbl_fiche_pr.N_un_deux_metre));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_un_deux_metre", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.N_deux_plus_metre.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_deux_plus_metre", DbType.Int32, 4, varclstbl_fiche_pr.N_deux_plus_metre));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_deux_plus_metre", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.P_zero_demi_metre_calc.HasValue) cmd.Parameters.Add(getParameter(cmd, "@p_zero_demi_metre_calc", DbType.Int32, 4, varclstbl_fiche_pr.P_zero_demi_metre_calc));
                    else cmd.Parameters.Add(getParameter(cmd, "@p_zero_demi_metre_calc", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.P_demi_un_metre_calc.HasValue) cmd.Parameters.Add(getParameter(cmd, "@p_demi_un_metre_calc", DbType.Int32, 4, varclstbl_fiche_pr.P_demi_un_metre_calc));
                    else cmd.Parameters.Add(getParameter(cmd, "@p_demi_un_metre_calc", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.P_un_deux_metre_calc.HasValue) cmd.Parameters.Add(getParameter(cmd, "@p_un_deux_metre_calc", DbType.Int32, 4, varclstbl_fiche_pr.P_un_deux_metre_calc));
                    else cmd.Parameters.Add(getParameter(cmd, "@p_un_deux_metre_calc", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.P_deux_plus_metre_calc.HasValue) cmd.Parameters.Add(getParameter(cmd, "@p_deux_plus_metre_calc", DbType.Int32, 4, varclstbl_fiche_pr.P_deux_plus_metre_calc));
                    else cmd.Parameters.Add(getParameter(cmd, "@p_deux_plus_metre_calc", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Degats_calc.HasValue) cmd.Parameters.Add(getParameter(cmd, "@degats_calc", DbType.Int32, 4, varclstbl_fiche_pr.Degats_calc));
                    else cmd.Parameters.Add(getParameter(cmd, "@degats_calc", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Type_degats != null) cmd.Parameters.Add(getParameter(cmd, "@type_degats", DbType.String, 255, varclstbl_fiche_pr.Type_degats));
                    else cmd.Parameters.Add(getParameter(cmd, "@type_degats", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_vaches != null) cmd.Parameters.Add(getParameter(cmd, "@n_vaches", DbType.String, 255, varclstbl_fiche_pr.N_vaches));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_vaches", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_chevres != null) cmd.Parameters.Add(getParameter(cmd, "@n_chevres", DbType.String, 255, varclstbl_fiche_pr.N_chevres));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_chevres", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_rats != null) cmd.Parameters.Add(getParameter(cmd, "@n_rats", DbType.String, 255, varclstbl_fiche_pr.N_rats));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_rats", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_termites != null) cmd.Parameters.Add(getParameter(cmd, "@n_termites", DbType.String, 255, varclstbl_fiche_pr.N_termites));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_termites", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_elephants != null) cmd.Parameters.Add(getParameter(cmd, "@n_elephants", DbType.String, 255, varclstbl_fiche_pr.N_elephants));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_elephants", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_cultures_vivrieres != null) cmd.Parameters.Add(getParameter(cmd, "@n_cultures_vivrieres", DbType.String, 255, varclstbl_fiche_pr.N_cultures_vivrieres));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_cultures_vivrieres", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_erosion != null) cmd.Parameters.Add(getParameter(cmd, "@n_erosion", DbType.String, 255, varclstbl_fiche_pr.N_erosion));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_erosion", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_eboulement != null) cmd.Parameters.Add(getParameter(cmd, "@n_eboulement", DbType.String, 255, varclstbl_fiche_pr.N_eboulement));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_eboulement", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_feu != null) cmd.Parameters.Add(getParameter(cmd, "@n_feu", DbType.String, 255, varclstbl_fiche_pr.N_feu));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_feu", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_secheresse != null) cmd.Parameters.Add(getParameter(cmd, "@n_secheresse", DbType.String, 255, varclstbl_fiche_pr.N_secheresse));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_secheresse", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_hommes != null) cmd.Parameters.Add(getParameter(cmd, "@n_hommes", DbType.String, 255, varclstbl_fiche_pr.N_hommes));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_hommes", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_plante_avec_sachets != null) cmd.Parameters.Add(getParameter(cmd, "@n_plante_avec_sachets", DbType.String, 255, varclstbl_fiche_pr.N_plante_avec_sachets));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_plante_avec_sachets", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_plante_trop_tard != null) cmd.Parameters.Add(getParameter(cmd, "@n_plante_trop_tard", DbType.String, 255, varclstbl_fiche_pr.N_plante_trop_tard));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_plante_trop_tard", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_guerren != null) cmd.Parameters.Add(getParameter(cmd, "@n_guerren", DbType.String, 255, varclstbl_fiche_pr.N_guerren));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_guerren", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Degats_total.HasValue) cmd.Parameters.Add(getParameter(cmd, "@degats_total", DbType.Int32, 4, varclstbl_fiche_pr.Degats_total));
                    else cmd.Parameters.Add(getParameter(cmd, "@degats_total", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Regarnissage != null) cmd.Parameters.Add(getParameter(cmd, "@regarnissage", DbType.String, 255, varclstbl_fiche_pr.Regarnissage));
                    else cmd.Parameters.Add(getParameter(cmd, "@regarnissage", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Regarnissage_suffisant != null) cmd.Parameters.Add(getParameter(cmd, "@regarnissage_suffisant", DbType.String, 255, varclstbl_fiche_pr.Regarnissage_suffisant));
                    else cmd.Parameters.Add(getParameter(cmd, "@regarnissage_suffisant", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Entretien != null) cmd.Parameters.Add(getParameter(cmd, "@entretien", DbType.String, 255, varclstbl_fiche_pr.Entretien));
                    else cmd.Parameters.Add(getParameter(cmd, "@entretien", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Etat != null) cmd.Parameters.Add(getParameter(cmd, "@etat", DbType.String, 255, varclstbl_fiche_pr.Etat));
                    else cmd.Parameters.Add(getParameter(cmd, "@etat", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Cultures_vivrieres != null) cmd.Parameters.Add(getParameter(cmd, "@cultures_vivrieres", DbType.String, 255, varclstbl_fiche_pr.Cultures_vivrieres));
                    else cmd.Parameters.Add(getParameter(cmd, "@cultures_vivrieres", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Type_cultures_vivieres != null) cmd.Parameters.Add(getParameter(cmd, "@type_cultures_vivieres", DbType.String, 255, varclstbl_fiche_pr.Type_cultures_vivieres));
                    else cmd.Parameters.Add(getParameter(cmd, "@type_cultures_vivieres", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Type_cultures_vivieres_autr != null) cmd.Parameters.Add(getParameter(cmd, "@type_cultures_vivieres_autr", DbType.String, 255, varclstbl_fiche_pr.Type_cultures_vivieres_autr));
                    else cmd.Parameters.Add(getParameter(cmd, "@type_cultures_vivieres_autr", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_haricots != null) cmd.Parameters.Add(getParameter(cmd, "@n_haricots", DbType.String, 255, varclstbl_fiche_pr.N_haricots));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_haricots", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_manioc != null) cmd.Parameters.Add(getParameter(cmd, "@n_manioc", DbType.String, 255, varclstbl_fiche_pr.N_manioc));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_manioc", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_soja != null) cmd.Parameters.Add(getParameter(cmd, "@n_soja", DbType.String, 255, varclstbl_fiche_pr.N_soja));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_soja", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_sorgho != null) cmd.Parameters.Add(getParameter(cmd, "@n_sorgho", DbType.String, 255, varclstbl_fiche_pr.N_sorgho));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_sorgho", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_arachides != null) cmd.Parameters.Add(getParameter(cmd, "@n_arachides", DbType.String, 255, varclstbl_fiche_pr.N_arachides));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_arachides", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_patates_douces != null) cmd.Parameters.Add(getParameter(cmd, "@n_patates_douces", DbType.String, 255, varclstbl_fiche_pr.N_patates_douces));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_patates_douces", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_mais != null) cmd.Parameters.Add(getParameter(cmd, "@n_mais", DbType.String, 255, varclstbl_fiche_pr.N_mais));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_mais", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.N_autres != null) cmd.Parameters.Add(getParameter(cmd, "@n_autres", DbType.String, 255, varclstbl_fiche_pr.N_autres));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_autres", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Type_cultures_total.HasValue) cmd.Parameters.Add(getParameter(cmd, "@type_cultures_total", DbType.Int32, 4, varclstbl_fiche_pr.Type_cultures_total));
                    else cmd.Parameters.Add(getParameter(cmd, "@type_cultures_total", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Canopee_fermee != null) cmd.Parameters.Add(getParameter(cmd, "@canopee_fermee", DbType.String, 255, varclstbl_fiche_pr.Canopee_fermee));
                    else cmd.Parameters.Add(getParameter(cmd, "@canopee_fermee", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Superficie_canopee_fermee.HasValue) cmd.Parameters.Add(getParameter(cmd, "@superficie_canopee_fermee", DbType.Single, 4, varclstbl_fiche_pr.Superficie_canopee_fermee));
                    else cmd.Parameters.Add(getParameter(cmd, "@superficie_canopee_fermee", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_fiche_pr.Croissance_arbres != null) cmd.Parameters.Add(getParameter(cmd, "@croissance_arbres", DbType.String, 255, varclstbl_fiche_pr.Croissance_arbres));
                    else cmd.Parameters.Add(getParameter(cmd, "@croissance_arbres", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Arbres_existants != null) cmd.Parameters.Add(getParameter(cmd, "@arbres_existants", DbType.String, 255, varclstbl_fiche_pr.Arbres_existants));
                    else cmd.Parameters.Add(getParameter(cmd, "@arbres_existants", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Rpt_c != null) cmd.Parameters.Add(getParameter(cmd, "@rpt_c", DbType.String, 10, varclstbl_fiche_pr.Rpt_c));
                    else cmd.Parameters.Add(getParameter(cmd, "@rpt_c", DbType.String, 10, DBNull.Value));
                    if (varclstbl_fiche_pr.Photo_1 != null) cmd.Parameters.Add(getParameter(cmd, "@photo_1", DbType.Binary, Int32.MaxValue, varclstbl_fiche_pr.Photo_1));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo_1", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    if (varclstbl_fiche_pr.Emplacement != null) cmd.Parameters.Add(getParameter(cmd, "@emplacement", DbType.String, 255, varclstbl_fiche_pr.Emplacement));
                    else cmd.Parameters.Add(getParameter(cmd, "@emplacement", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Photo_2 != null) cmd.Parameters.Add(getParameter(cmd, "@photo_2", DbType.String, 255, varclstbl_fiche_pr.Photo_2));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo_2", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Emplacement_2 != null) cmd.Parameters.Add(getParameter(cmd, "@emplacement_2", DbType.String, 255, varclstbl_fiche_pr.Emplacement_2));
                    else cmd.Parameters.Add(getParameter(cmd, "@emplacement_2", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Localisation != null) cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, varclstbl_fiche_pr.Localisation));
                    else cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Commentaire_wwf != null) cmd.Parameters.Add(getParameter(cmd, "@commentaire_wwf", DbType.String, 255, varclstbl_fiche_pr.Commentaire_wwf));
                    else cmd.Parameters.Add(getParameter(cmd, "@commentaire_wwf", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Commentaire_planteur != null) cmd.Parameters.Add(getParameter(cmd, "@commentaire_planteur", DbType.String, 255, varclstbl_fiche_pr.Commentaire_planteur));
                    else cmd.Parameters.Add(getParameter(cmd, "@commentaire_planteur", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Commentaire_association != null) cmd.Parameters.Add(getParameter(cmd, "@commentaire_association", DbType.String, 255, varclstbl_fiche_pr.Commentaire_association));
                    else cmd.Parameters.Add(getParameter(cmd, "@commentaire_association", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_pr.Eucalyptus_deau_non != null) cmd.Parameters.Add(getParameter(cmd, "@eucalyptus_deau_non", DbType.String, 255, varclstbl_fiche_pr.Eucalyptus_deau_non));
                    else cmd.Parameters.Add(getParameter(cmd, "@eucalyptus_deau_non", DbType.String, 255, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_pr.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_fiche_pr' avec la classe 'clstbl_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_fiche_pr(DataRowView varclstbl_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_fiche_pr  SET uuid=@uuid,deviceid=@deviceid,date=@date,nom_agent=@nom_agent,saison=@saison,association=@association,association_autre=@association_autre,bailleur=@bailleur,bailleur_autre=@bailleur_autre,n_visite=@n_visite,contreverification=@contreverification,n_visite_2=@n_visite_2,n_viste_3=@n_viste_3,visite_calculate=@visite_calculate,n_plantation=@n_plantation,n_bloc=@n_bloc,noms_planteur=@noms_planteur,nom=@nom,post_nom=@post_nom,prenom=@prenom,sexes=@sexes,planteur_present=@planteur_present,changement_surface=@changement_surface,photo_fiche=@photo_fiche,titre_trace_gps=@titre_trace_gps,superficie=@superficie,superficie_non_plantee=@superficie_non_plantee,photo_inventaire=@photo_inventaire,periode_debut=@periode_debut,preiode_debut_annee=@preiode_debut_annee,periode_fin=@periode_fin,period_fin_annee=@period_fin_annee,essence_principale=@essence_principale,essence_principale_autre=@essence_principale_autre,melanges=@melanges,rpt_b=@rpt_b,pente_1=@pente_1,pente_2=@pente_2,pente_3=@pente_3,pente_4=@pente_4,encartement_type=@encartement_type,ecartement_dim_1=@ecartement_dim_1,ecartement_dim_2=@ecartement_dim_2,alignement=@alignement,causes=@causes,piquets=@piquets,pourcentage_insuffisants=@pourcentage_insuffisants,eucalyptus_deau=@eucalyptus_deau,n_vides=@n_vides,n_zero_demi_metre=@n_zero_demi_metre,n_demi_un_metre=@n_demi_un_metre,n_un_deux_metre=@n_un_deux_metre,n_deux_plus_metre=@n_deux_plus_metre,p_zero_demi_metre_calc=@p_zero_demi_metre_calc,p_demi_un_metre_calc=@p_demi_un_metre_calc,p_un_deux_metre_calc=@p_un_deux_metre_calc,p_deux_plus_metre_calc=@p_deux_plus_metre_calc,degats_calc=@degats_calc,type_degats=@type_degats,n_vaches=@n_vaches,n_chevres=@n_chevres,n_rats=@n_rats,n_termites=@n_termites,n_elephants=@n_elephants,n_cultures_vivrieres=@n_cultures_vivrieres,n_erosion=@n_erosion,n_eboulement=@n_eboulement,n_feu=@n_feu,n_secheresse=@n_secheresse,n_hommes=@n_hommes,n_plante_avec_sachets=@n_plante_avec_sachets,n_plante_trop_tard=@n_plante_trop_tard,n_guerren=@n_guerren,degats_total=@degats_total,regarnissage=@regarnissage,regarnissage_suffisant=@regarnissage_suffisant,entretien=@entretien,etat=@etat,cultures_vivrieres=@cultures_vivrieres,type_cultures_vivieres=@type_cultures_vivieres,type_cultures_vivieres_autr=@type_cultures_vivieres_autr,n_haricots=@n_haricots,n_manioc=@n_manioc,n_soja=@n_soja,n_sorgho=@n_sorgho,n_arachides=@n_arachides,n_patates_douces=@n_patates_douces,n_mais=@n_mais,n_autres=@n_autres,type_cultures_total=@type_cultures_total,canopee_fermee=@canopee_fermee,superficie_canopee_fermee=@superficie_canopee_fermee,croissance_arbres=@croissance_arbres,arbres_existants=@arbres_existants,rpt_c=@rpt_c,photo_1=@photo_1,emplacement=@emplacement,photo_2=@photo_2,emplacement_2=@emplacement_2,localisation=@localisation,commentaire_wwf=@commentaire_wwf,commentaire_planteur=@commentaire_planteur,commentaire_association=@commentaire_association,eucalyptus_deau_non=@eucalyptus_deau_non,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_pr["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_pr["deviceid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_pr["date"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nom_agent", DbType.String, 255, varclstbl_fiche_pr["nom_agent"]));
                    cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, varclstbl_fiche_pr["saison"]));
                    cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, varclstbl_fiche_pr["association"]));
                    cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, varclstbl_fiche_pr["association_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, varclstbl_fiche_pr["bailleur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, varclstbl_fiche_pr["bailleur_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_visite", DbType.String, 255, varclstbl_fiche_pr["n_visite"]));
                    cmd.Parameters.Add(getParameter(cmd, "@contreverification", DbType.String, 255, varclstbl_fiche_pr["contreverification"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_visite_2", DbType.Int32, 4, varclstbl_fiche_pr["n_visite_2"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_viste_3", DbType.Int32, 4, varclstbl_fiche_pr["n_viste_3"]));
                    cmd.Parameters.Add(getParameter(cmd, "@visite_calculate", DbType.Int32, 4, varclstbl_fiche_pr["visite_calculate"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_plantation", DbType.String, 255, varclstbl_fiche_pr["n_plantation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_bloc", DbType.String, 255, varclstbl_fiche_pr["n_bloc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@noms_planteur", DbType.String, 255, varclstbl_fiche_pr["noms_planteur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nom", DbType.String, 255, varclstbl_fiche_pr["nom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@post_nom", DbType.String, 255, varclstbl_fiche_pr["post_nom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@prenom", DbType.String, 255, varclstbl_fiche_pr["prenom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@sexes", DbType.String, 15, varclstbl_fiche_pr["sexes"]));
                    cmd.Parameters.Add(getParameter(cmd, "@planteur_present", DbType.String, 255, varclstbl_fiche_pr["planteur_present"]));
                    cmd.Parameters.Add(getParameter(cmd, "@changement_surface", DbType.String, 255, varclstbl_fiche_pr["changement_surface"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo_fiche", DbType.Binary, Int32.MaxValue, varclstbl_fiche_pr["photo_fiche"]));
                    cmd.Parameters.Add(getParameter(cmd, "@titre_trace_gps", DbType.String, 255, varclstbl_fiche_pr["titre_trace_gps"]));
                    cmd.Parameters.Add(getParameter(cmd, "@superficie", DbType.Single, 4, varclstbl_fiche_pr["superficie"]));
                    cmd.Parameters.Add(getParameter(cmd, "@superficie_non_plantee", DbType.Single, 4, varclstbl_fiche_pr["superficie_non_plantee"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo_inventaire", DbType.Binary, Int32.MaxValue, varclstbl_fiche_pr["photo_inventaire"]));
                    cmd.Parameters.Add(getParameter(cmd, "@periode_debut", DbType.String, 255, varclstbl_fiche_pr["periode_debut"]));
                    cmd.Parameters.Add(getParameter(cmd, "@preiode_debut_annee", DbType.String, 255, varclstbl_fiche_pr["preiode_debut_annee"]));
                    cmd.Parameters.Add(getParameter(cmd, "@periode_fin", DbType.String, 255, varclstbl_fiche_pr["periode_fin"]));
                    cmd.Parameters.Add(getParameter(cmd, "@period_fin_annee", DbType.String, 255, varclstbl_fiche_pr["period_fin_annee"]));
                    cmd.Parameters.Add(getParameter(cmd, "@essence_principale", DbType.String, 255, varclstbl_fiche_pr["essence_principale"]));
                    cmd.Parameters.Add(getParameter(cmd, "@essence_principale_autre", DbType.String, 255, varclstbl_fiche_pr["essence_principale_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@melanges", DbType.String, 255, varclstbl_fiche_pr["melanges"]));
                    cmd.Parameters.Add(getParameter(cmd, "@rpt_b", DbType.String, 10, varclstbl_fiche_pr["rpt_b"]));
                    cmd.Parameters.Add(getParameter(cmd, "@pente_1", DbType.Int32, 4, varclstbl_fiche_pr["pente_1"]));
                    cmd.Parameters.Add(getParameter(cmd, "@pente_2", DbType.Int32, 4, varclstbl_fiche_pr["pente_2"]));
                    cmd.Parameters.Add(getParameter(cmd, "@pente_3", DbType.Int32, 4, varclstbl_fiche_pr["pente_3"]));
                    cmd.Parameters.Add(getParameter(cmd, "@pente_4", DbType.Int32, 4, varclstbl_fiche_pr["pente_4"]));
                    cmd.Parameters.Add(getParameter(cmd, "@encartement_type", DbType.String, 255, varclstbl_fiche_pr["encartement_type"]));
                    cmd.Parameters.Add(getParameter(cmd, "@ecartement_dim_1", DbType.String, 255, varclstbl_fiche_pr["ecartement_dim_1"]));
                    cmd.Parameters.Add(getParameter(cmd, "@ecartement_dim_2", DbType.String, 255, varclstbl_fiche_pr["ecartement_dim_2"]));
                    cmd.Parameters.Add(getParameter(cmd, "@alignement", DbType.String, 255, varclstbl_fiche_pr["alignement"]));
                    cmd.Parameters.Add(getParameter(cmd, "@causes", DbType.String, 255, varclstbl_fiche_pr["causes"]));
                    cmd.Parameters.Add(getParameter(cmd, "@piquets", DbType.String, 255, varclstbl_fiche_pr["piquets"]));
                    cmd.Parameters.Add(getParameter(cmd, "@pourcentage_insuffisants", DbType.String, 255, varclstbl_fiche_pr["pourcentage_insuffisants"]));
                    cmd.Parameters.Add(getParameter(cmd, "@eucalyptus_deau", DbType.String, 255, varclstbl_fiche_pr["eucalyptus_deau"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_vides", DbType.Int32, 4, varclstbl_fiche_pr["n_vides"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_zero_demi_metre", DbType.Int32, 4, varclstbl_fiche_pr["n_zero_demi_metre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_demi_un_metre", DbType.Int32, 4, varclstbl_fiche_pr["n_demi_un_metre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_un_deux_metre", DbType.Int32, 4, varclstbl_fiche_pr["n_un_deux_metre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_deux_plus_metre", DbType.Int32, 4, varclstbl_fiche_pr["n_deux_plus_metre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@p_zero_demi_metre_calc", DbType.Int32, 4, varclstbl_fiche_pr["p_zero_demi_metre_calc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@p_demi_un_metre_calc", DbType.Int32, 4, varclstbl_fiche_pr["p_demi_un_metre_calc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@p_un_deux_metre_calc", DbType.Int32, 4, varclstbl_fiche_pr["p_un_deux_metre_calc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@p_deux_plus_metre_calc", DbType.Int32, 4, varclstbl_fiche_pr["p_deux_plus_metre_calc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@degats_calc", DbType.Int32, 4, varclstbl_fiche_pr["degats_calc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@type_degats", DbType.String, 255, varclstbl_fiche_pr["type_degats"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_vaches", DbType.String, 255, varclstbl_fiche_pr["n_vaches"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_chevres", DbType.String, 255, varclstbl_fiche_pr["n_chevres"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_rats", DbType.String, 255, varclstbl_fiche_pr["n_rats"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_termites", DbType.String, 255, varclstbl_fiche_pr["n_termites"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_elephants", DbType.String, 255, varclstbl_fiche_pr["n_elephants"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_cultures_vivrieres", DbType.String, 255, varclstbl_fiche_pr["n_cultures_vivrieres"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_erosion", DbType.String, 255, varclstbl_fiche_pr["n_erosion"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_eboulement", DbType.String, 255, varclstbl_fiche_pr["n_eboulement"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_feu", DbType.String, 255, varclstbl_fiche_pr["n_feu"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_secheresse", DbType.String, 255, varclstbl_fiche_pr["n_secheresse"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_hommes", DbType.String, 255, varclstbl_fiche_pr["n_hommes"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_plante_avec_sachets", DbType.String, 255, varclstbl_fiche_pr["n_plante_avec_sachets"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_plante_trop_tard", DbType.String, 255, varclstbl_fiche_pr["n_plante_trop_tard"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_guerren", DbType.String, 255, varclstbl_fiche_pr["n_guerren"]));
                    cmd.Parameters.Add(getParameter(cmd, "@degats_total", DbType.Int32, 4, varclstbl_fiche_pr["degats_total"]));
                    cmd.Parameters.Add(getParameter(cmd, "@regarnissage", DbType.String, 255, varclstbl_fiche_pr["regarnissage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@regarnissage_suffisant", DbType.String, 255, varclstbl_fiche_pr["regarnissage_suffisant"]));
                    cmd.Parameters.Add(getParameter(cmd, "@entretien", DbType.String, 255, varclstbl_fiche_pr["entretien"]));
                    cmd.Parameters.Add(getParameter(cmd, "@etat", DbType.String, 255, varclstbl_fiche_pr["etat"]));
                    cmd.Parameters.Add(getParameter(cmd, "@cultures_vivrieres", DbType.String, 255, varclstbl_fiche_pr["cultures_vivrieres"]));
                    cmd.Parameters.Add(getParameter(cmd, "@type_cultures_vivieres", DbType.String, 255, varclstbl_fiche_pr["type_cultures_vivieres"]));
                    cmd.Parameters.Add(getParameter(cmd, "@type_cultures_vivieres_autr", DbType.String, 255, varclstbl_fiche_pr["type_cultures_vivieres_autr"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_haricots", DbType.String, 255, varclstbl_fiche_pr["n_haricots"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_manioc", DbType.String, 255, varclstbl_fiche_pr["n_manioc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_soja", DbType.String, 255, varclstbl_fiche_pr["n_soja"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_sorgho", DbType.String, 255, varclstbl_fiche_pr["n_sorgho"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_arachides", DbType.String, 255, varclstbl_fiche_pr["n_arachides"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_patates_douces", DbType.String, 255, varclstbl_fiche_pr["n_patates_douces"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_mais", DbType.String, 255, varclstbl_fiche_pr["n_mais"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_autres", DbType.String, 255, varclstbl_fiche_pr["n_autres"]));
                    cmd.Parameters.Add(getParameter(cmd, "@type_cultures_total", DbType.Int32, 4, varclstbl_fiche_pr["type_cultures_total"]));
                    cmd.Parameters.Add(getParameter(cmd, "@canopee_fermee", DbType.String, 255, varclstbl_fiche_pr["canopee_fermee"]));
                    cmd.Parameters.Add(getParameter(cmd, "@superficie_canopee_fermee", DbType.Single, 4, varclstbl_fiche_pr["superficie_canopee_fermee"]));
                    cmd.Parameters.Add(getParameter(cmd, "@croissance_arbres", DbType.String, 255, varclstbl_fiche_pr["croissance_arbres"]));
                    cmd.Parameters.Add(getParameter(cmd, "@arbres_existants", DbType.String, 255, varclstbl_fiche_pr["arbres_existants"]));
                    cmd.Parameters.Add(getParameter(cmd, "@rpt_c", DbType.String, 10, varclstbl_fiche_pr["rpt_c"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo_1", DbType.Binary, Int32.MaxValue, varclstbl_fiche_pr["photo_1"]));
                    cmd.Parameters.Add(getParameter(cmd, "@emplacement", DbType.String, 255, varclstbl_fiche_pr["emplacement"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo_2", DbType.String, 255, varclstbl_fiche_pr["photo_2"]));
                    cmd.Parameters.Add(getParameter(cmd, "@emplacement_2", DbType.String, 255, varclstbl_fiche_pr["emplacement_2"]));
                    cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, varclstbl_fiche_pr["localisation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@commentaire_wwf", DbType.String, 255, varclstbl_fiche_pr["commentaire_wwf"]));
                    cmd.Parameters.Add(getParameter(cmd, "@commentaire_planteur", DbType.String, 255, varclstbl_fiche_pr["commentaire_planteur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@commentaire_association", DbType.String, 255, varclstbl_fiche_pr["commentaire_association"]));
                    cmd.Parameters.Add(getParameter(cmd, "@eucalyptus_deau_non", DbType.String, 255, varclstbl_fiche_pr["eucalyptus_deau_non"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_pr["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_fiche_pr["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_fiche_pr' avec la classe 'clstbl_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_fiche_pr(DataRowView varclstbl_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_fiche_pr  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_fiche_pr["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_fiche_pr' avec la classe 'clstbl_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_FICHE_PR 
        #region  CLSTBL_AUTRE_ESSENCE_MEL_FICHE_PR
        public clstbl_autre_essence_mel_fiche_pr getClstbl_autre_essence_mel_fiche_pr(object intid)
        {
            clstbl_autre_essence_mel_fiche_pr varclstbl_autre_essence_mel_fiche_pr = new clstbl_autre_essence_mel_fiche_pr();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_autre_essence_mel_fiche_pr WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_autre_essence_mel_fiche_pr.Id = int.Parse(dr["id"].ToString());
                            varclstbl_autre_essence_mel_fiche_pr.Uuid = dr["uuid"].ToString();
                            varclstbl_autre_essence_mel_fiche_pr.Autre_essence = dr["autre_essence"].ToString();
                            varclstbl_autre_essence_mel_fiche_pr.Autre_essence_autre = dr["autre_essence_autre"].ToString();
                            if (!dr["autre_essence_pourcentage"].ToString().Trim().Equals("")) varclstbl_autre_essence_mel_fiche_pr.Autre_essence_pourcentage = int.Parse(dr["autre_essence_pourcentage"].ToString());
                            if (!dr["autre_essence_count"].ToString().Trim().Equals("")) varclstbl_autre_essence_mel_fiche_pr.Autre_essence_count = int.Parse(dr["autre_essence_count"].ToString());
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_autre_essence_mel_fiche_pr.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_autre_essence_mel_fiche_pr' avec la classe 'clstbl_autre_essence_mel_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_autre_essence_mel_fiche_pr;
        }

        public DataTable getAllClstbl_autre_essence_mel_fiche_pr(string criteria)
        {
            DataTable dtclstbl_autre_essence_mel_fiche_pr = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_autre_essence_mel_fiche_pr  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   autre_essence LIKE '%" + criteria + "%'";
                    sql += "  OR   autre_essence_autre LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_autre_essence_mel_fiche_pr.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_autre_essence_mel_fiche_pr' avec la classe 'clstbl_autre_essence_mel_fiche_pr' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_autre_essence_mel_fiche_pr;
        }

        public DataTable getAllClstbl_autre_essence_mel_fiche_pr()
        {
            DataTable dtclstbl_autre_essence_mel_fiche_pr = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_autre_essence_mel_fiche_pr ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_autre_essence_mel_fiche_pr.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_autre_essence_mel_fiche_pr' avec la classe 'clstbl_autre_essence_mel_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_autre_essence_mel_fiche_pr;
        }

        public int insertClstbl_autre_essence_mel_fiche_pr(clstbl_autre_essence_mel_fiche_pr varclstbl_autre_essence_mel_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_autre_essence_mel_fiche_pr ( uuid,autre_essence,autre_essence_autre,autre_essence_pourcentage,autre_essence_count,synchronized_on ) VALUES (@uuid,@autre_essence,@autre_essence_autre,@autre_essence_pourcentage,@autre_essence_count,@synchronized_on  )");
                    if (varclstbl_autre_essence_mel_fiche_pr.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_autre_essence_mel_fiche_pr.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_autre_essence_mel_fiche_pr.Autre_essence != null) cmd.Parameters.Add(getParameter(cmd, "@autre_essence", DbType.String, 255, varclstbl_autre_essence_mel_fiche_pr.Autre_essence));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_essence", DbType.String, 255, DBNull.Value));
                    if (varclstbl_autre_essence_mel_fiche_pr.Autre_essence_autre != null) cmd.Parameters.Add(getParameter(cmd, "@autre_essence_autre", DbType.String, 255, varclstbl_autre_essence_mel_fiche_pr.Autre_essence_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_essence_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_autre_essence_mel_fiche_pr.Autre_essence_pourcentage.HasValue) cmd.Parameters.Add(getParameter(cmd, "@autre_essence_pourcentage", DbType.Int32, 4, varclstbl_autre_essence_mel_fiche_pr.Autre_essence_pourcentage));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_essence_pourcentage", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_autre_essence_mel_fiche_pr.Autre_essence_count.HasValue) cmd.Parameters.Add(getParameter(cmd, "@autre_essence_count", DbType.Int32, 4, varclstbl_autre_essence_mel_fiche_pr.Autre_essence_count));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_essence_count", DbType.Int32, 4, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_autre_essence_mel_fiche_pr.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_autre_essence_mel_fiche_pr' avec la classe 'clstbl_autre_essence_mel_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_autre_essence_mel_fiche_pr(DataRowView varclstbl_autre_essence_mel_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_autre_essence_mel_fiche_pr  SET uuid=@uuid,autre_essence=@autre_essence,autre_essence_autre=@autre_essence_autre,autre_essence_pourcentage=@autre_essence_pourcentage,autre_essence_count=@autre_essence_count,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_autre_essence_mel_fiche_pr["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_essence", DbType.String, 255, varclstbl_autre_essence_mel_fiche_pr["autre_essence"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_essence_autre", DbType.String, 255, varclstbl_autre_essence_mel_fiche_pr["autre_essence_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_essence_pourcentage", DbType.Int32, 4, varclstbl_autre_essence_mel_fiche_pr["autre_essence_pourcentage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_essence_count", DbType.Int32, 4, varclstbl_autre_essence_mel_fiche_pr["autre_essence_count"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_autre_essence_mel_fiche_pr["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_autre_essence_mel_fiche_pr["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_autre_essence_mel_fiche_pr' avec la classe 'clstbl_autre_essence_mel_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_autre_essence_mel_fiche_pr(DataRowView varclstbl_autre_essence_mel_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_autre_essence_mel_fiche_pr  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_autre_essence_mel_fiche_pr["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_autre_essence_mel_fiche_pr' avec la classe 'clstbl_autre_essence_mel_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_AUTRE_ESSENCE_MEL_FICHE_PR 
        #region  CLSTBL_ARBRES_FICHE_PR
        public clstbl_arbres_fiche_pr getClstbl_arbres_fiche_pr(object intid)
        {
            clstbl_arbres_fiche_pr varclstbl_arbres_fiche_pr = new clstbl_arbres_fiche_pr();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_arbres_fiche_pr WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_arbres_fiche_pr.Id = int.Parse(dr["id"].ToString());
                            varclstbl_arbres_fiche_pr.Uuid = dr["uuid"].ToString();
                            if (!dr["hauteur_total"].ToString().Trim().Equals("")) varclstbl_arbres_fiche_pr.Hauteur_total = double.Parse(dr["hauteur_total"].ToString());
                            if (!dr["hauteur_tronc"].ToString().Trim().Equals("")) varclstbl_arbres_fiche_pr.Hauteur_tronc = double.Parse(dr["hauteur_tronc"].ToString());
                            if (!dr["houppier_1"].ToString().Trim().Equals("")) varclstbl_arbres_fiche_pr.Houppier_1 = int.Parse(dr["houppier_1"].ToString());
                            if (!dr["houppier_2"].ToString().Trim().Equals("")) varclstbl_arbres_fiche_pr.Houppier_2 = int.Parse(dr["houppier_2"].ToString());
                            if (!dr["diametre"].ToString().Trim().Equals("")) varclstbl_arbres_fiche_pr.Diametre = double.Parse(dr["diametre"].ToString());
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_arbres_fiche_pr.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_arbres_fiche_pr' avec la classe 'clstbl_arbres_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_arbres_fiche_pr;
        }

        public DataTable getAllClstbl_arbres_fiche_pr(string criteria)
        {
            DataTable dtclstbl_arbres_fiche_pr = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_arbres_fiche_pr  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_arbres_fiche_pr.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_arbres_fiche_pr' avec la classe 'clstbl_arbres_fiche_pr' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_arbres_fiche_pr;
        }

        public DataTable getAllClstbl_arbres_fiche_pr()
        {
            DataTable dtclstbl_arbres_fiche_pr = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_arbres_fiche_pr ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_arbres_fiche_pr.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_arbres_fiche_pr' avec la classe 'clstbl_arbres_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_arbres_fiche_pr;
        }

        public int insertClstbl_arbres_fiche_pr(clstbl_arbres_fiche_pr varclstbl_arbres_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_arbres_fiche_pr ( uuid,hauteur_total,hauteur_tronc,houppier_1,houppier_2,diametre,synchronized_on ) VALUES (@uuid,@hauteur_total,@hauteur_tronc,@houppier_1,@houppier_2,@diametre,@synchronized_on  )");
                    if (varclstbl_arbres_fiche_pr.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_arbres_fiche_pr.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_arbres_fiche_pr.Hauteur_total.HasValue) cmd.Parameters.Add(getParameter(cmd, "@hauteur_total", DbType.Single, 4, varclstbl_arbres_fiche_pr.Hauteur_total));
                    else cmd.Parameters.Add(getParameter(cmd, "@hauteur_total", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_arbres_fiche_pr.Hauteur_tronc.HasValue) cmd.Parameters.Add(getParameter(cmd, "@hauteur_tronc", DbType.Single, 4, varclstbl_arbres_fiche_pr.Hauteur_tronc));
                    else cmd.Parameters.Add(getParameter(cmd, "@hauteur_tronc", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_arbres_fiche_pr.Houppier_1.HasValue) cmd.Parameters.Add(getParameter(cmd, "@houppier_1", DbType.Int32, 4, varclstbl_arbres_fiche_pr.Houppier_1));
                    else cmd.Parameters.Add(getParameter(cmd, "@houppier_1", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_arbres_fiche_pr.Houppier_2.HasValue) cmd.Parameters.Add(getParameter(cmd, "@houppier_2", DbType.Int32, 4, varclstbl_arbres_fiche_pr.Houppier_2));
                    else cmd.Parameters.Add(getParameter(cmd, "@houppier_2", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_arbres_fiche_pr.Diametre.HasValue) cmd.Parameters.Add(getParameter(cmd, "@diametre", DbType.Single, 4, varclstbl_arbres_fiche_pr.Diametre));
                    else cmd.Parameters.Add(getParameter(cmd, "@diametre", DbType.Single, 4, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_arbres_fiche_pr.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_arbres_fiche_pr' avec la classe 'clstbl_arbres_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_arbres_fiche_pr(DataRowView varclstbl_arbres_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_arbres_fiche_pr  SET uuid=@uuid,hauteur_total=@hauteur_total,hauteur_tronc=@hauteur_tronc,houppier_1=@houppier_1,houppier_2=@houppier_2,diametre=@diametre,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_arbres_fiche_pr["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@hauteur_total", DbType.Single, 4, varclstbl_arbres_fiche_pr["hauteur_total"]));
                    cmd.Parameters.Add(getParameter(cmd, "@hauteur_tronc", DbType.Single, 4, varclstbl_arbres_fiche_pr["hauteur_tronc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@houppier_1", DbType.Int32, 4, varclstbl_arbres_fiche_pr["houppier_1"]));
                    cmd.Parameters.Add(getParameter(cmd, "@houppier_2", DbType.Int32, 4, varclstbl_arbres_fiche_pr["houppier_2"]));
                    cmd.Parameters.Add(getParameter(cmd, "@diametre", DbType.Single, 4, varclstbl_arbres_fiche_pr["diametre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_arbres_fiche_pr["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_arbres_fiche_pr["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_arbres_fiche_pr' avec la classe 'clstbl_arbres_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_arbres_fiche_pr(DataRowView varclstbl_arbres_fiche_pr)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_arbres_fiche_pr  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_arbres_fiche_pr["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_arbres_fiche_pr' avec la classe 'clstbl_arbres_fiche_pr' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_ARBRES_FICHE_PR 
        #region  CLSTBL_FICHE_IDENT_PEPI
        public clstbl_fiche_ident_pepi getClstbl_fiche_ident_pepi(object intid)
        {
            clstbl_fiche_ident_pepi varclstbl_fiche_ident_pepi = new clstbl_fiche_ident_pepi();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_ident_pepi WHERE pid={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["pid"].ToString().Trim().Equals("")) varclstbl_fiche_ident_pepi.Pid = int.Parse(dr["pid"].ToString());
                            varclstbl_fiche_ident_pepi.Uuid = dr["uuid"].ToString();
                            varclstbl_fiche_ident_pepi.Deviceid = dr["deviceid"].ToString();
                            if (!dr["date"].ToString().Trim().Equals("")) varclstbl_fiche_ident_pepi.Date = DateTime.Parse(dr["date"].ToString());
                            varclstbl_fiche_ident_pepi.Agent = dr["agent"].ToString();
                            varclstbl_fiche_ident_pepi.Saison = dr["saison"].ToString();
                            varclstbl_fiche_ident_pepi.Association = dr["association"].ToString();
                            varclstbl_fiche_ident_pepi.Association_autre = dr["association_autre"].ToString();
                            varclstbl_fiche_ident_pepi.Bailleur = dr["bailleur"].ToString();
                            varclstbl_fiche_ident_pepi.Bailleur_autre = dr["bailleur_autre"].ToString();
                            varclstbl_fiche_ident_pepi.Id = dr["id"].ToString();
                            varclstbl_fiche_ident_pepi.Nom_site = dr["nom_site"].ToString();
                            varclstbl_fiche_ident_pepi.Village = dr["village"].ToString();
                            varclstbl_fiche_ident_pepi.Localite = dr["localite"].ToString();
                            varclstbl_fiche_ident_pepi.Territoire = dr["territoire"].ToString();
                            varclstbl_fiche_ident_pepi.Chefferie = dr["chefferie"].ToString();
                            varclstbl_fiche_ident_pepi.Groupement = dr["groupement"].ToString();
                            if (!dr["date_installation_pepiniere"].ToString().Trim().Equals("")) varclstbl_fiche_ident_pepi.Date_installation_pepiniere = DateTime.Parse(dr["date_installation_pepiniere"].ToString());
                            varclstbl_fiche_ident_pepi.Grp_c = dr["grp_c"].ToString();
                            if (!dr["nb_pepinieristes"].ToString().Trim().Equals("")) varclstbl_fiche_ident_pepi.Nb_pepinieristes = int.Parse(dr["nb_pepinieristes"].ToString());
                            if (!dr["nb_pepinieristes_formes"].ToString().Trim().Equals("")) varclstbl_fiche_ident_pepi.Nb_pepinieristes_formes = int.Parse(dr["nb_pepinieristes_formes"].ToString());
                            varclstbl_fiche_ident_pepi.Contrat = dr["contrat"].ToString();
                            if (!dr["combien_pepinieristes"].ToString().Trim().Equals("")) varclstbl_fiche_ident_pepi.Combien_pepinieristes = int.Parse(dr["combien_pepinieristes"].ToString());
                            varclstbl_fiche_ident_pepi.Localisation = dr["localisation"].ToString();
                            if (!dr["photo"].ToString().Trim().Equals("")) varclstbl_fiche_ident_pepi.Photo = (Byte[])dr["photo"];
                            varclstbl_fiche_ident_pepi.Observations = dr["observations"].ToString();
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_fiche_ident_pepi.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_fiche_ident_pepi' avec la classe 'clstbl_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_fiche_ident_pepi;
        }

        public DataTable getAllClstbl_fiche_ident_pepi(string criteria)
        {
            DataTable dtclstbl_fiche_ident_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_fiche_ident_pepi  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   deviceid LIKE '%" + criteria + "%'";
                    sql += "  OR   agent LIKE '%" + criteria + "%'";
                    sql += "  OR   saison LIKE '%" + criteria + "%'";
                    sql += "  OR   association LIKE '%" + criteria + "%'";
                    sql += "  OR   association_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   id LIKE '%" + criteria + "%'";
                    sql += "  OR   nom_site LIKE '%" + criteria + "%'";
                    sql += "  OR   village LIKE '%" + criteria + "%'";
                    sql += "  OR   localite LIKE '%" + criteria + "%'";
                    sql += "  OR   territoire LIKE '%" + criteria + "%'";
                    sql += "  OR   chefferie LIKE '%" + criteria + "%'";
                    sql += "  OR   groupement LIKE '%" + criteria + "%'";
                    sql += "  OR   grp_c LIKE '%" + criteria + "%'";
                    sql += "  OR   contrat LIKE '%" + criteria + "%'";
                    sql += "  OR   localisation LIKE '%" + criteria + "%'";
                    sql += "  OR   observations LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_ident_pepi.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_fiche_ident_pepi' avec la classe 'clstbl_fiche_ident_pepi' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_ident_pepi;
        }

        public DataTable getAllClstbl_fiche_ident_pepi()
        {
            DataTable dtclstbl_fiche_ident_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_ident_pepi ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_ident_pepi.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_fiche_ident_pepi' avec la classe 'clstbl_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_ident_pepi;
        }

        public int insertClstbl_fiche_ident_pepi(clstbl_fiche_ident_pepi varclstbl_fiche_ident_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_fiche_ident_pepi ( uuid,deviceid,date,agent,saison,association,association_autre,bailleur,bailleur_autre,id,nom_site,village,localite,territoire,chefferie,groupement,date_installation_pepiniere,grp_c,nb_pepinieristes,nb_pepinieristes_formes,contrat,combien_pepinieristes,localisation,photo,observations,synchronized_on ) VALUES (@uuid,@deviceid,@date,@agent,@saison,@association,@association_autre,@bailleur,@bailleur_autre,@id,@nom_site,@village,@localite,@territoire,@chefferie,@groupement,@date_installation_pepiniere,@grp_c,@nb_pepinieristes,@nb_pepinieristes_formes,@contrat,@combien_pepinieristes,@localisation,@photo,@observations,@synchronized_on  )");
                    if (varclstbl_fiche_ident_pepi.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_ident_pepi.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Deviceid != null) cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_ident_pepi.Deviceid));
                    else cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_ident_pepi.Date));
                    if (varclstbl_fiche_ident_pepi.Agent != null) cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, varclstbl_fiche_ident_pepi.Agent));
                    else cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Saison != null) cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, varclstbl_fiche_ident_pepi.Saison));
                    else cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Association != null) cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, varclstbl_fiche_ident_pepi.Association));
                    else cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Association_autre != null) cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, varclstbl_fiche_ident_pepi.Association_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Bailleur != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, varclstbl_fiche_ident_pepi.Bailleur));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Bailleur_autre != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, varclstbl_fiche_ident_pepi.Bailleur_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Id != null) cmd.Parameters.Add(getParameter(cmd, "@id", DbType.String, 255, varclstbl_fiche_ident_pepi.Id));
                    else cmd.Parameters.Add(getParameter(cmd, "@id", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Nom_site != null) cmd.Parameters.Add(getParameter(cmd, "@nom_site", DbType.String, 255, varclstbl_fiche_ident_pepi.Nom_site));
                    else cmd.Parameters.Add(getParameter(cmd, "@nom_site", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Village != null) cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 255, varclstbl_fiche_ident_pepi.Village));
                    else cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Localite != null) cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 255, varclstbl_fiche_ident_pepi.Localite));
                    else cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Territoire != null) cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 255, varclstbl_fiche_ident_pepi.Territoire));
                    else cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Chefferie != null) cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 255, varclstbl_fiche_ident_pepi.Chefferie));
                    else cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Groupement != null) cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 255, varclstbl_fiche_ident_pepi.Groupement));
                    else cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Date_installation_pepiniere.HasValue) cmd.Parameters.Add(getParameter(cmd, "@date_installation_pepiniere", DbType.DateTime, 8, varclstbl_fiche_ident_pepi.Date_installation_pepiniere));
                    else cmd.Parameters.Add(getParameter(cmd, "@date_installation_pepiniere", DbType.DateTime, 8, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Grp_c != null) cmd.Parameters.Add(getParameter(cmd, "@grp_c", DbType.String, 255, varclstbl_fiche_ident_pepi.Grp_c));
                    else cmd.Parameters.Add(getParameter(cmd, "@grp_c", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Nb_pepinieristes.HasValue) cmd.Parameters.Add(getParameter(cmd, "@nb_pepinieristes", DbType.Int32, 4, varclstbl_fiche_ident_pepi.Nb_pepinieristes));
                    else cmd.Parameters.Add(getParameter(cmd, "@nb_pepinieristes", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Nb_pepinieristes_formes.HasValue) cmd.Parameters.Add(getParameter(cmd, "@nb_pepinieristes_formes", DbType.Int32, 4, varclstbl_fiche_ident_pepi.Nb_pepinieristes_formes));
                    else cmd.Parameters.Add(getParameter(cmd, "@nb_pepinieristes_formes", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Contrat != null) cmd.Parameters.Add(getParameter(cmd, "@contrat", DbType.String, 255, varclstbl_fiche_ident_pepi.Contrat));
                    else cmd.Parameters.Add(getParameter(cmd, "@contrat", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Combien_pepinieristes.HasValue) cmd.Parameters.Add(getParameter(cmd, "@combien_pepinieristes", DbType.Int32, 4, varclstbl_fiche_ident_pepi.Combien_pepinieristes));
                    else cmd.Parameters.Add(getParameter(cmd, "@combien_pepinieristes", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Localisation != null) cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, varclstbl_fiche_ident_pepi.Localisation));
                    else cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Photo != null) cmd.Parameters.Add(getParameter(cmd, "@photo", DbType.Binary, Int32.MaxValue, varclstbl_fiche_ident_pepi.Photo));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    if (varclstbl_fiche_ident_pepi.Observations != null) cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, varclstbl_fiche_ident_pepi.Observations));
                    else cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_ident_pepi.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_fiche_ident_pepi' avec la classe 'clstbl_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_fiche_ident_pepi(DataRowView varclstbl_fiche_ident_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_fiche_ident_pepi  SET uuid=@uuid,deviceid=@deviceid,date=@date,agent=@agent,saison=@saison,association=@association,association_autre=@association_autre,bailleur=@bailleur,bailleur_autre=@bailleur_autre,id=@id,nom_site=@nom_site,village=@village,localite=@localite,territoire=@territoire,chefferie=@chefferie,groupement=@groupement,date_installation_pepiniere=@date_installation_pepiniere,grp_c=@grp_c,nb_pepinieristes=@nb_pepinieristes,nb_pepinieristes_formes=@nb_pepinieristes_formes,contrat=@contrat,combien_pepinieristes=@combien_pepinieristes,localisation=@localisation,photo=@photo,observations=@observations,synchronized_on=@synchronized_on  WHERE 1=1  AND pid=@pid ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_ident_pepi["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_ident_pepi["deviceid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_ident_pepi["date"]));
                    cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, varclstbl_fiche_ident_pepi["agent"]));
                    cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, varclstbl_fiche_ident_pepi["saison"]));
                    cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, varclstbl_fiche_ident_pepi["association"]));
                    cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, varclstbl_fiche_ident_pepi["association_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, varclstbl_fiche_ident_pepi["bailleur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, varclstbl_fiche_ident_pepi["bailleur_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.String, 255, varclstbl_fiche_ident_pepi["id"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nom_site", DbType.String, 255, varclstbl_fiche_ident_pepi["nom_site"]));
                    cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 255, varclstbl_fiche_ident_pepi["village"]));
                    cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 255, varclstbl_fiche_ident_pepi["localite"]));
                    cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 255, varclstbl_fiche_ident_pepi["territoire"]));
                    cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 255, varclstbl_fiche_ident_pepi["chefferie"]));
                    cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 255, varclstbl_fiche_ident_pepi["groupement"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date_installation_pepiniere", DbType.DateTime, 8, varclstbl_fiche_ident_pepi["date_installation_pepiniere"]));
                    cmd.Parameters.Add(getParameter(cmd, "@grp_c", DbType.String, 255, varclstbl_fiche_ident_pepi["grp_c"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nb_pepinieristes", DbType.Int32, 4, varclstbl_fiche_ident_pepi["nb_pepinieristes"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nb_pepinieristes_formes", DbType.Int32, 4, varclstbl_fiche_ident_pepi["nb_pepinieristes_formes"]));
                    cmd.Parameters.Add(getParameter(cmd, "@contrat", DbType.String, 255, varclstbl_fiche_ident_pepi["contrat"]));
                    cmd.Parameters.Add(getParameter(cmd, "@combien_pepinieristes", DbType.Int32, 4, varclstbl_fiche_ident_pepi["combien_pepinieristes"]));
                    cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, varclstbl_fiche_ident_pepi["localisation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo", DbType.Binary, Int32.MaxValue, varclstbl_fiche_ident_pepi["photo"]));
                    cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, varclstbl_fiche_ident_pepi["observations"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_ident_pepi["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@pid", DbType.Int32, 4, varclstbl_fiche_ident_pepi["pid"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_fiche_ident_pepi' avec la classe 'clstbl_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_fiche_ident_pepi(DataRowView varclstbl_fiche_ident_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_fiche_ident_pepi  WHERE  1=1  AND pid=@pid ");
                    cmd.Parameters.Add(getParameter(cmd, "@pid", DbType.Int32, 4, varclstbl_fiche_ident_pepi["pid"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_fiche_ident_pepi' avec la classe 'clstbl_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_FICHE_IDENT_PEPI 
        #region  CLSTBL_GRP_C_FICHE_IDENT_PEPI
        public clstbl_grp_c_fiche_ident_pepi getClstbl_grp_c_fiche_ident_pepi(object intid)
        {
            clstbl_grp_c_fiche_ident_pepi varclstbl_grp_c_fiche_ident_pepi = new clstbl_grp_c_fiche_ident_pepi();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_grp_c_fiche_ident_pepi WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_grp_c_fiche_ident_pepi.Id = int.Parse(dr["id"].ToString());
                            varclstbl_grp_c_fiche_ident_pepi.Uuid = dr["uuid"].ToString();
                            if (!dr["count"].ToString().Trim().Equals("")) varclstbl_grp_c_fiche_ident_pepi.Count = double.Parse(dr["count"].ToString());
                            if (!dr["dimension_planche_a"].ToString().Trim().Equals("")) varclstbl_grp_c_fiche_ident_pepi.Dimension_planche_a = double.Parse(dr["dimension_planche_a"].ToString());
                            if (!dr["dimension_planche_b"].ToString().Trim().Equals("")) varclstbl_grp_c_fiche_ident_pepi.Dimension_planche_b = double.Parse(dr["dimension_planche_b"].ToString());
                            if (!dr["capacite_planche"].ToString().Trim().Equals("")) varclstbl_grp_c_fiche_ident_pepi.Capacite_planche = int.Parse(dr["capacite_planche"].ToString());
                            if (!dr["capacite_totale_planche"].ToString().Trim().Equals("")) varclstbl_grp_c_fiche_ident_pepi.Capacite_totale_planche = int.Parse(dr["capacite_totale_planche"].ToString());
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_grp_c_fiche_ident_pepi.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_grp_c_fiche_ident_pepi' avec la classe 'clstbl_grp_c_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_grp_c_fiche_ident_pepi;
        }

        public DataTable getAllClstbl_grp_c_fiche_ident_pepi(string criteria)
        {
            DataTable dtclstbl_grp_c_fiche_ident_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_grp_c_fiche_ident_pepi  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_grp_c_fiche_ident_pepi.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_grp_c_fiche_ident_pepi' avec la classe 'clstbl_grp_c_fiche_ident_pepi' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_grp_c_fiche_ident_pepi;
        }

        public DataTable getAllClstbl_grp_c_fiche_ident_pepi()
        {
            DataTable dtclstbl_grp_c_fiche_ident_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_grp_c_fiche_ident_pepi ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_grp_c_fiche_ident_pepi.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_grp_c_fiche_ident_pepi' avec la classe 'clstbl_grp_c_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_grp_c_fiche_ident_pepi;
        }

        public int insertClstbl_grp_c_fiche_ident_pepi(clstbl_grp_c_fiche_ident_pepi varclstbl_grp_c_fiche_ident_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_grp_c_fiche_ident_pepi ( uuid,count,dimension_planche_a,dimension_planche_b,capacite_planche,capacite_totale_planche,synchronized_on ) VALUES (@uuid,@count,@dimension_planche_a,@dimension_planche_b,@capacite_planche,@capacite_totale_planche,@synchronized_on  )");
                    if (varclstbl_grp_c_fiche_ident_pepi.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_grp_c_fiche_ident_pepi.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_grp_c_fiche_ident_pepi.Count.HasValue) cmd.Parameters.Add(getParameter(cmd, "@count", DbType.Single, 4, varclstbl_grp_c_fiche_ident_pepi.Count));
                    else cmd.Parameters.Add(getParameter(cmd, "@count", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_grp_c_fiche_ident_pepi.Dimension_planche_a.HasValue) cmd.Parameters.Add(getParameter(cmd, "@dimension_planche_a", DbType.Single, 4, varclstbl_grp_c_fiche_ident_pepi.Dimension_planche_a));
                    else cmd.Parameters.Add(getParameter(cmd, "@dimension_planche_a", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_grp_c_fiche_ident_pepi.Dimension_planche_b.HasValue) cmd.Parameters.Add(getParameter(cmd, "@dimension_planche_b", DbType.Single, 4, varclstbl_grp_c_fiche_ident_pepi.Dimension_planche_b));
                    else cmd.Parameters.Add(getParameter(cmd, "@dimension_planche_b", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_grp_c_fiche_ident_pepi.Capacite_planche.HasValue) cmd.Parameters.Add(getParameter(cmd, "@capacite_planche", DbType.Int32, 4, varclstbl_grp_c_fiche_ident_pepi.Capacite_planche));
                    else cmd.Parameters.Add(getParameter(cmd, "@capacite_planche", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_grp_c_fiche_ident_pepi.Capacite_totale_planche.HasValue) cmd.Parameters.Add(getParameter(cmd, "@capacite_totale_planche", DbType.Int32, 4, varclstbl_grp_c_fiche_ident_pepi.Capacite_totale_planche));
                    else cmd.Parameters.Add(getParameter(cmd, "@capacite_totale_planche", DbType.Int32, 4, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_grp_c_fiche_ident_pepi.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_grp_c_fiche_ident_pepi' avec la classe 'clstbl_grp_c_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_grp_c_fiche_ident_pepi(DataRowView varclstbl_grp_c_fiche_ident_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_grp_c_fiche_ident_pepi  SET uuid=@uuid,count=@count,dimension_planche_a=@dimension_planche_a,dimension_planche_b=@dimension_planche_b,capacite_planche=@capacite_planche,capacite_totale_planche=@capacite_totale_planche,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_grp_c_fiche_ident_pepi["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@count", DbType.Single, 4, varclstbl_grp_c_fiche_ident_pepi["count"]));
                    cmd.Parameters.Add(getParameter(cmd, "@dimension_planche_a", DbType.Single, 4, varclstbl_grp_c_fiche_ident_pepi["dimension_planche_a"]));
                    cmd.Parameters.Add(getParameter(cmd, "@dimension_planche_b", DbType.Single, 4, varclstbl_grp_c_fiche_ident_pepi["dimension_planche_b"]));
                    cmd.Parameters.Add(getParameter(cmd, "@capacite_planche", DbType.Int32, 4, varclstbl_grp_c_fiche_ident_pepi["capacite_planche"]));
                    cmd.Parameters.Add(getParameter(cmd, "@capacite_totale_planche", DbType.Int32, 4, varclstbl_grp_c_fiche_ident_pepi["capacite_totale_planche"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_grp_c_fiche_ident_pepi["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_grp_c_fiche_ident_pepi["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_grp_c_fiche_ident_pepi' avec la classe 'clstbl_grp_c_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_grp_c_fiche_ident_pepi(DataRowView varclstbl_grp_c_fiche_ident_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_grp_c_fiche_ident_pepi  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_grp_c_fiche_ident_pepi["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_grp_c_fiche_ident_pepi' avec la classe 'clstbl_grp_c_fiche_ident_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_GRP_C_FICHE_IDENT_PEPI 
        #region  CLSTBL_FICHE_SUIVI_PEPI
        public clstbl_fiche_suivi_pepi getClstbl_fiche_suivi_pepi(object intid)
        {
            clstbl_fiche_suivi_pepi varclstbl_fiche_suivi_pepi = new clstbl_fiche_suivi_pepi();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_suivi_pepi WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_fiche_suivi_pepi.Id = int.Parse(dr["id"].ToString());
                            varclstbl_fiche_suivi_pepi.Uuid = dr["uuid"].ToString();
                            varclstbl_fiche_suivi_pepi.Deviceid = dr["deviceid"].ToString();
                            if (!dr["date"].ToString().Trim().Equals("")) varclstbl_fiche_suivi_pepi.Date = DateTime.Parse(dr["date"].ToString());
                            varclstbl_fiche_suivi_pepi.Agent = dr["agent"].ToString();
                            varclstbl_fiche_suivi_pepi.Saison = dr["saison"].ToString();
                            varclstbl_fiche_suivi_pepi.Association = dr["association"].ToString();
                            varclstbl_fiche_suivi_pepi.Association_autre = dr["association_autre"].ToString();
                            varclstbl_fiche_suivi_pepi.Bailleur = dr["bailleur"].ToString();
                            varclstbl_fiche_suivi_pepi.Bailleur_autre = dr["bailleur_autre"].ToString();
                            varclstbl_fiche_suivi_pepi.Nom_site = dr["nom_site"].ToString();
                            varclstbl_fiche_suivi_pepi.Identifiant_pepiniere = dr["identifiant_pepiniere"].ToString();
                            varclstbl_fiche_suivi_pepi.Ronde_suivi_pepiniere = dr["ronde_suivi_pepiniere"].ToString();
                            varclstbl_fiche_suivi_pepi.Grp_c = dr["grp_c"].ToString();
                            varclstbl_fiche_suivi_pepi.Grp_f = dr["grp_f"].ToString();
                            if (!dr["superficie_potentielle_note"].ToString().Trim().Equals("")) varclstbl_fiche_suivi_pepi.Superficie_potentielle_note = double.Parse(dr["superficie_potentielle_note"].ToString());
                            if (!dr["superficie_potentielle_2"].ToString().Trim().Equals("")) varclstbl_fiche_suivi_pepi.Superficie_potentielle_2 = double.Parse(dr["superficie_potentielle_2"].ToString());
                            if (!dr["superficie_potentielle_2_5"].ToString().Trim().Equals("")) varclstbl_fiche_suivi_pepi.Superficie_potentielle_2_5 = double.Parse(dr["superficie_potentielle_2_5"].ToString());
                            if (!dr["superficie_potentielle_3"].ToString().Trim().Equals("")) varclstbl_fiche_suivi_pepi.Superficie_potentielle_3 = double.Parse(dr["superficie_potentielle_3"].ToString());
                            varclstbl_fiche_suivi_pepi.Tassement_sachet = dr["tassement_sachet"].ToString();
                            varclstbl_fiche_suivi_pepi.Binage = dr["binage"].ToString();
                            varclstbl_fiche_suivi_pepi.Classement_taille = dr["classement_taille"].ToString();
                            varclstbl_fiche_suivi_pepi.Classement_espece = dr["classement_espece"].ToString();
                            varclstbl_fiche_suivi_pepi.Cernage = dr["cernage"].ToString();
                            varclstbl_fiche_suivi_pepi.Etetage = dr["etetage"].ToString();
                            varclstbl_fiche_suivi_pepi.Localisation = dr["localisation"].ToString();
                            if (!dr["photo"].ToString().Trim().Equals("")) varclstbl_fiche_suivi_pepi.Photo = (Byte[])dr["photo"];
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_fiche_suivi_pepi.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_fiche_suivi_pepi' avec la classe 'clstbl_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_fiche_suivi_pepi;
        }

        public DataTable getAllClstbl_fiche_suivi_pepi(string criteria)
        {
            DataTable dtclstbl_fiche_suivi_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_fiche_suivi_pepi  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   deviceid LIKE '%" + criteria + "%'";
                    sql += "  OR   agent LIKE '%" + criteria + "%'";
                    sql += "  OR   saison LIKE '%" + criteria + "%'";
                    sql += "  OR   association LIKE '%" + criteria + "%'";
                    sql += "  OR   association_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   nom_site LIKE '%" + criteria + "%'";
                    sql += "  OR   identifiant_pepiniere LIKE '%" + criteria + "%'";
                    sql += "  OR   ronde_suivi_pepiniere LIKE '%" + criteria + "%'";
                    sql += "  OR   grp_c LIKE '%" + criteria + "%'";
                    sql += "  OR   grp_f LIKE '%" + criteria + "%'";
                    sql += "  OR   tassement_sachet LIKE '%" + criteria + "%'";
                    sql += "  OR   binage LIKE '%" + criteria + "%'";
                    sql += "  OR   classement_taille LIKE '%" + criteria + "%'";
                    sql += "  OR   classement_espece LIKE '%" + criteria + "%'";
                    sql += "  OR   cernage LIKE '%" + criteria + "%'";
                    sql += "  OR   etetage LIKE '%" + criteria + "%'";
                    sql += "  OR   localisation LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_suivi_pepi.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_fiche_suivi_pepi' avec la classe 'clstbl_fiche_suivi_pepi' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_suivi_pepi;
        }

        public DataTable getAllClstbl_fiche_suivi_pepi()
        {
            DataTable dtclstbl_fiche_suivi_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_suivi_pepi ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_suivi_pepi.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_fiche_suivi_pepi' avec la classe 'clstbl_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_suivi_pepi;
        }

        public int insertClstbl_fiche_suivi_pepi(clstbl_fiche_suivi_pepi varclstbl_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_fiche_suivi_pepi ( uuid,deviceid,date,agent,saison,association,association_autre,bailleur,bailleur_autre,nom_site,identifiant_pepiniere,ronde_suivi_pepiniere,grp_c,grp_f,superficie_potentielle_note,superficie_potentielle_2,superficie_potentielle_2_5,superficie_potentielle_3,tassement_sachet,binage,classement_taille,classement_espece,cernage,etetage,localisation,photo,synchronized_on ) VALUES (@uuid,@deviceid,@date,@agent,@saison,@association,@association_autre,@bailleur,@bailleur_autre,@nom_site,@identifiant_pepiniere,@ronde_suivi_pepiniere,@grp_c,@grp_f,@superficie_potentielle_note,@superficie_potentielle_2,@superficie_potentielle_2_5,@superficie_potentielle_3,@tassement_sachet,@binage,@classement_taille,@classement_espece,@cernage,@etetage,@localisation,@photo,@synchronized_on  )");
                    if (varclstbl_fiche_suivi_pepi.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_suivi_pepi.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Deviceid != null) cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_suivi_pepi.Deviceid));
                    else cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_suivi_pepi.Date));
                    if (varclstbl_fiche_suivi_pepi.Agent != null) cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, varclstbl_fiche_suivi_pepi.Agent));
                    else cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Saison != null) cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, varclstbl_fiche_suivi_pepi.Saison));
                    else cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Association != null) cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, varclstbl_fiche_suivi_pepi.Association));
                    else cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Association_autre != null) cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, varclstbl_fiche_suivi_pepi.Association_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Bailleur != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, varclstbl_fiche_suivi_pepi.Bailleur));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Bailleur_autre != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, varclstbl_fiche_suivi_pepi.Bailleur_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Nom_site != null) cmd.Parameters.Add(getParameter(cmd, "@nom_site", DbType.String, 255, varclstbl_fiche_suivi_pepi.Nom_site));
                    else cmd.Parameters.Add(getParameter(cmd, "@nom_site", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Identifiant_pepiniere != null) cmd.Parameters.Add(getParameter(cmd, "@identifiant_pepiniere", DbType.String, 255, varclstbl_fiche_suivi_pepi.Identifiant_pepiniere));
                    else cmd.Parameters.Add(getParameter(cmd, "@identifiant_pepiniere", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Ronde_suivi_pepiniere != null) cmd.Parameters.Add(getParameter(cmd, "@ronde_suivi_pepiniere", DbType.String, 255, varclstbl_fiche_suivi_pepi.Ronde_suivi_pepiniere));
                    else cmd.Parameters.Add(getParameter(cmd, "@ronde_suivi_pepiniere", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Grp_c != null) cmd.Parameters.Add(getParameter(cmd, "@grp_c", DbType.String, 10, varclstbl_fiche_suivi_pepi.Grp_c));
                    else cmd.Parameters.Add(getParameter(cmd, "@grp_c", DbType.String, 10, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Grp_f != null) cmd.Parameters.Add(getParameter(cmd, "@grp_f", DbType.String, 10, varclstbl_fiche_suivi_pepi.Grp_f));
                    else cmd.Parameters.Add(getParameter(cmd, "@grp_f", DbType.String, 10, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Superficie_potentielle_note.HasValue) cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_note", DbType.Single, 4, varclstbl_fiche_suivi_pepi.Superficie_potentielle_note));
                    else cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_note", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Superficie_potentielle_2.HasValue) cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_2", DbType.Single, 4, varclstbl_fiche_suivi_pepi.Superficie_potentielle_2));
                    else cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_2", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Superficie_potentielle_2_5.HasValue) cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_2_5", DbType.Single, 4, varclstbl_fiche_suivi_pepi.Superficie_potentielle_2_5));
                    else cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_2_5", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Superficie_potentielle_3.HasValue) cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_3", DbType.Single, 4, varclstbl_fiche_suivi_pepi.Superficie_potentielle_3));
                    else cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_3", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Tassement_sachet != null) cmd.Parameters.Add(getParameter(cmd, "@tassement_sachet", DbType.String, 255, varclstbl_fiche_suivi_pepi.Tassement_sachet));
                    else cmd.Parameters.Add(getParameter(cmd, "@tassement_sachet", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Binage != null) cmd.Parameters.Add(getParameter(cmd, "@binage", DbType.String, 255, varclstbl_fiche_suivi_pepi.Binage));
                    else cmd.Parameters.Add(getParameter(cmd, "@binage", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Classement_taille != null) cmd.Parameters.Add(getParameter(cmd, "@classement_taille", DbType.String, 255, varclstbl_fiche_suivi_pepi.Classement_taille));
                    else cmd.Parameters.Add(getParameter(cmd, "@classement_taille", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Classement_espece != null) cmd.Parameters.Add(getParameter(cmd, "@classement_espece", DbType.String, 255, varclstbl_fiche_suivi_pepi.Classement_espece));
                    else cmd.Parameters.Add(getParameter(cmd, "@classement_espece", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Cernage != null) cmd.Parameters.Add(getParameter(cmd, "@cernage", DbType.String, 255, varclstbl_fiche_suivi_pepi.Cernage));
                    else cmd.Parameters.Add(getParameter(cmd, "@cernage", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Etetage != null) cmd.Parameters.Add(getParameter(cmd, "@etetage", DbType.String, 255, varclstbl_fiche_suivi_pepi.Etetage));
                    else cmd.Parameters.Add(getParameter(cmd, "@etetage", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Localisation != null) cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, varclstbl_fiche_suivi_pepi.Localisation));
                    else cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_suivi_pepi.Photo != null) cmd.Parameters.Add(getParameter(cmd, "@photo", DbType.Binary, Int32.MaxValue, varclstbl_fiche_suivi_pepi.Photo));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_suivi_pepi.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_fiche_suivi_pepi' avec la classe 'clstbl_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_fiche_suivi_pepi(DataRowView varclstbl_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_fiche_suivi_pepi  SET uuid=@uuid,deviceid=@deviceid,date=@date,agent=@agent,saison=@saison,association=@association,association_autre=@association_autre,bailleur=@bailleur,bailleur_autre=@bailleur_autre,nom_site=@nom_site,identifiant_pepiniere=@identifiant_pepiniere,ronde_suivi_pepiniere=@ronde_suivi_pepiniere,grp_c=@grp_c,grp_f=@grp_f,superficie_potentielle_note=@superficie_potentielle_note,superficie_potentielle_2=@superficie_potentielle_2,superficie_potentielle_2_5=@superficie_potentielle_2_5,superficie_potentielle_3=@superficie_potentielle_3,tassement_sachet=@tassement_sachet,binage=@binage,classement_taille=@classement_taille,classement_espece=@classement_espece,cernage=@cernage,etetage=@etetage,localisation=@localisation,photo=@photo,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_suivi_pepi["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_suivi_pepi["deviceid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_suivi_pepi["date"]));
                    cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, varclstbl_fiche_suivi_pepi["agent"]));
                    cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, varclstbl_fiche_suivi_pepi["saison"]));
                    cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, varclstbl_fiche_suivi_pepi["association"]));
                    cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, varclstbl_fiche_suivi_pepi["association_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, varclstbl_fiche_suivi_pepi["bailleur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, varclstbl_fiche_suivi_pepi["bailleur_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nom_site", DbType.String, 255, varclstbl_fiche_suivi_pepi["nom_site"]));
                    cmd.Parameters.Add(getParameter(cmd, "@identifiant_pepiniere", DbType.String, 255, varclstbl_fiche_suivi_pepi["identifiant_pepiniere"]));
                    cmd.Parameters.Add(getParameter(cmd, "@ronde_suivi_pepiniere", DbType.String, 255, varclstbl_fiche_suivi_pepi["ronde_suivi_pepiniere"]));
                    cmd.Parameters.Add(getParameter(cmd, "@grp_c", DbType.String, 10, varclstbl_fiche_suivi_pepi["grp_c"]));
                    cmd.Parameters.Add(getParameter(cmd, "@grp_f", DbType.String, 10, varclstbl_fiche_suivi_pepi["grp_f"]));
                    cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_note", DbType.Single, 4, varclstbl_fiche_suivi_pepi["superficie_potentielle_note"]));
                    cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_2", DbType.Single, 4, varclstbl_fiche_suivi_pepi["superficie_potentielle_2"]));
                    cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_2_5", DbType.Single, 4, varclstbl_fiche_suivi_pepi["superficie_potentielle_2_5"]));
                    cmd.Parameters.Add(getParameter(cmd, "@superficie_potentielle_3", DbType.Single, 4, varclstbl_fiche_suivi_pepi["superficie_potentielle_3"]));
                    cmd.Parameters.Add(getParameter(cmd, "@tassement_sachet", DbType.String, 255, varclstbl_fiche_suivi_pepi["tassement_sachet"]));
                    cmd.Parameters.Add(getParameter(cmd, "@binage", DbType.String, 255, varclstbl_fiche_suivi_pepi["binage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@classement_taille", DbType.String, 255, varclstbl_fiche_suivi_pepi["classement_taille"]));
                    cmd.Parameters.Add(getParameter(cmd, "@classement_espece", DbType.String, 255, varclstbl_fiche_suivi_pepi["classement_espece"]));
                    cmd.Parameters.Add(getParameter(cmd, "@cernage", DbType.String, 255, varclstbl_fiche_suivi_pepi["cernage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@etetage", DbType.String, 255, varclstbl_fiche_suivi_pepi["etetage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@localisation", DbType.String, 255, varclstbl_fiche_suivi_pepi["localisation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo", DbType.Binary, Int32.MaxValue, varclstbl_fiche_suivi_pepi["photo"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_suivi_pepi["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_fiche_suivi_pepi["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_fiche_suivi_pepi' avec la classe 'clstbl_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_fiche_suivi_pepi(DataRowView varclstbl_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_fiche_suivi_pepi  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_fiche_suivi_pepi["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_fiche_suivi_pepi' avec la classe 'clstbl_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_FICHE_SUIVI_PEPI 
        #region  CLSTBL_GERMOIR_FICHE_SUIVI_PEPI
        public clstbl_germoir_fiche_suivi_pepi getClstbl_germoir_fiche_suivi_pepi(object intid)
        {
            clstbl_germoir_fiche_suivi_pepi varclstbl_germoir_fiche_suivi_pepi = new clstbl_germoir_fiche_suivi_pepi();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_germoir_fiche_suivi_pepi WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_germoir_fiche_suivi_pepi.Id = int.Parse(dr["id"].ToString());
                            varclstbl_germoir_fiche_suivi_pepi.Uuid = dr["uuid"].ToString();
                            varclstbl_germoir_fiche_suivi_pepi.Germoir_essence = dr["germoir_essence"].ToString();
                            varclstbl_germoir_fiche_suivi_pepi.Germoir_essence_autre = dr["germoir_essence_autre"].ToString();
                            varclstbl_germoir_fiche_suivi_pepi.Provenance = dr["provenance"].ToString();
                            if (!dr["qte_semee"].ToString().Trim().Equals("")) varclstbl_germoir_fiche_suivi_pepi.Qte_semee = int.Parse(dr["qte_semee"].ToString());
                            if (!dr["date_semis"].ToString().Trim().Equals("")) varclstbl_germoir_fiche_suivi_pepi.Date_semis = DateTime.Parse(dr["date_semis"].ToString());
                            if (!dr["date_premiere_levee"].ToString().Trim().Equals("")) varclstbl_germoir_fiche_suivi_pepi.Date_premiere_levee = DateTime.Parse(dr["date_premiere_levee"].ToString());
                            varclstbl_germoir_fiche_suivi_pepi.Type_de_semis = dr["type_de_semis"].ToString();
                            varclstbl_germoir_fiche_suivi_pepi.Bien_plat = dr["bien_plat"].ToString();
                            varclstbl_germoir_fiche_suivi_pepi.Arrosage = dr["arrosage"].ToString();
                            varclstbl_germoir_fiche_suivi_pepi.Desherbage = dr["desherbage"].ToString();
                            varclstbl_germoir_fiche_suivi_pepi.Qualite_semis = dr["qualite_semis"].ToString();
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_germoir_fiche_suivi_pepi.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_germoir_fiche_suivi_pepi' avec la classe 'clstbl_germoir_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_germoir_fiche_suivi_pepi;
        }

        public DataTable getAllClstbl_germoir_fiche_suivi_pepi(string criteria)
        {
            DataTable dtclstbl_germoir_fiche_suivi_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_germoir_fiche_suivi_pepi  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   germoir_essence LIKE '%" + criteria + "%'";
                    sql += "  OR   germoir_essence_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   provenance LIKE '%" + criteria + "%'";
                    sql += "  OR   type_de_semis LIKE '%" + criteria + "%'";
                    sql += "  OR   bien_plat LIKE '%" + criteria + "%'";
                    sql += "  OR   arrosage LIKE '%" + criteria + "%'";
                    sql += "  OR   desherbage LIKE '%" + criteria + "%'";
                    sql += "  OR   qualite_semis LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_germoir_fiche_suivi_pepi.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_germoir_fiche_suivi_pepi' avec la classe 'clstbl_germoir_fiche_suivi_pepi' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_germoir_fiche_suivi_pepi;
        }

        public DataTable getAllClstbl_germoir_fiche_suivi_pepi()
        {
            DataTable dtclstbl_germoir_fiche_suivi_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_germoir_fiche_suivi_pepi ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_germoir_fiche_suivi_pepi.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_germoir_fiche_suivi_pepi' avec la classe 'clstbl_germoir_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_germoir_fiche_suivi_pepi;
        }

        public DataTable getAllClstbl_germoir_fiche_suivi_pepi_by_uuid(string uuid)
        {
            DataTable dtclstbl_germoir_fiche_suivi_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_germoir_fiche_suivi_pepi where uuid= " + uuid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_germoir_fiche_suivi_pepi.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_germoir_fiche_suivi_pepi' avec la classe 'clstbl_germoir_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_germoir_fiche_suivi_pepi;
        }

        public int insertClstbl_germoir_fiche_suivi_pepi(clstbl_germoir_fiche_suivi_pepi varclstbl_germoir_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_germoir_fiche_suivi_pepi ( uuid,germoir_essence,germoir_essence_autre,provenance,qte_semee,date_semis,date_premiere_levee,type_de_semis,bien_plat,arrosage,desherbage,qualite_semis,synchronized_on ) VALUES (@uuid,@germoir_essence,@germoir_essence_autre,@provenance,@qte_semee,@date_semis,@date_premiere_levee,@type_de_semis,@bien_plat,@arrosage,@desherbage,@qualite_semis,@synchronized_on  )");
                    if (varclstbl_germoir_fiche_suivi_pepi.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_germoir_fiche_suivi_pepi.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Germoir_essence != null) cmd.Parameters.Add(getParameter(cmd, "@germoir_essence", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi.Germoir_essence));
                    else cmd.Parameters.Add(getParameter(cmd, "@germoir_essence", DbType.String, 255, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Germoir_essence_autre != null) cmd.Parameters.Add(getParameter(cmd, "@germoir_essence_autre", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi.Germoir_essence_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@germoir_essence_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Provenance != null) cmd.Parameters.Add(getParameter(cmd, "@provenance", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi.Provenance));
                    else cmd.Parameters.Add(getParameter(cmd, "@provenance", DbType.String, 255, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Qte_semee.HasValue) cmd.Parameters.Add(getParameter(cmd, "@qte_semee", DbType.Int32, 4, varclstbl_germoir_fiche_suivi_pepi.Qte_semee));
                    else cmd.Parameters.Add(getParameter(cmd, "@qte_semee", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Date_semis.HasValue) cmd.Parameters.Add(getParameter(cmd, "@date_semis", DbType.DateTime, 8, varclstbl_germoir_fiche_suivi_pepi.Date_semis));
                    else cmd.Parameters.Add(getParameter(cmd, "@date_semis", DbType.DateTime, 8, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Date_premiere_levee.HasValue) cmd.Parameters.Add(getParameter(cmd, "@date_premiere_levee", DbType.DateTime, 8, varclstbl_germoir_fiche_suivi_pepi.Date_premiere_levee));
                    else cmd.Parameters.Add(getParameter(cmd, "@date_premiere_levee", DbType.DateTime, 8, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Type_de_semis != null) cmd.Parameters.Add(getParameter(cmd, "@type_de_semis", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi.Type_de_semis));
                    else cmd.Parameters.Add(getParameter(cmd, "@type_de_semis", DbType.String, 255, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Bien_plat != null) cmd.Parameters.Add(getParameter(cmd, "@bien_plat", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi.Bien_plat));
                    else cmd.Parameters.Add(getParameter(cmd, "@bien_plat", DbType.String, 255, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Arrosage != null) cmd.Parameters.Add(getParameter(cmd, "@arrosage", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi.Arrosage));
                    else cmd.Parameters.Add(getParameter(cmd, "@arrosage", DbType.String, 255, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Desherbage != null) cmd.Parameters.Add(getParameter(cmd, "@desherbage", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi.Desherbage));
                    else cmd.Parameters.Add(getParameter(cmd, "@desherbage", DbType.String, 255, DBNull.Value));
                    if (varclstbl_germoir_fiche_suivi_pepi.Qualite_semis != null) cmd.Parameters.Add(getParameter(cmd, "@qualite_semis", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi.Qualite_semis));
                    else cmd.Parameters.Add(getParameter(cmd, "@qualite_semis", DbType.String, 255, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_germoir_fiche_suivi_pepi.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_germoir_fiche_suivi_pepi' avec la classe 'clstbl_germoir_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_germoir_fiche_suivi_pepi(DataRowView varclstbl_germoir_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_germoir_fiche_suivi_pepi  SET uuid=@uuid,germoir_essence=@germoir_essence,germoir_essence_autre=@germoir_essence_autre,provenance=@provenance,qte_semee=@qte_semee,date_semis=@date_semis,date_premiere_levee=@date_premiere_levee,type_de_semis=@type_de_semis,bien_plat=@bien_plat,arrosage=@arrosage,desherbage=@desherbage,qualite_semis=@qualite_semis,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_germoir_fiche_suivi_pepi["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@germoir_essence", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi["germoir_essence"]));
                    cmd.Parameters.Add(getParameter(cmd, "@germoir_essence_autre", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi["germoir_essence_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@provenance", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi["provenance"]));
                    cmd.Parameters.Add(getParameter(cmd, "@qte_semee", DbType.Int32, 4, varclstbl_germoir_fiche_suivi_pepi["qte_semee"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date_semis", DbType.DateTime, 8, varclstbl_germoir_fiche_suivi_pepi["date_semis"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date_premiere_levee", DbType.DateTime, 8, varclstbl_germoir_fiche_suivi_pepi["date_premiere_levee"]));
                    cmd.Parameters.Add(getParameter(cmd, "@type_de_semis", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi["type_de_semis"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bien_plat", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi["bien_plat"]));
                    cmd.Parameters.Add(getParameter(cmd, "@arrosage", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi["arrosage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@desherbage", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi["desherbage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@qualite_semis", DbType.String, 255, varclstbl_germoir_fiche_suivi_pepi["qualite_semis"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_germoir_fiche_suivi_pepi["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_germoir_fiche_suivi_pepi["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_germoir_fiche_suivi_pepi' avec la classe 'clstbl_germoir_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_germoir_fiche_suivi_pepi(DataRowView varclstbl_germoir_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_germoir_fiche_suivi_pepi  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_germoir_fiche_suivi_pepi["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_germoir_fiche_suivi_pepi' avec la classe 'clstbl_germoir_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_GERMOIR_FICHE_SUIVI_PEPI 
        #region  CLSTBL_PLANT_REPIQ_FICHE_SUIVI_PEPI
        public clstbl_plant_repiq_fiche_suivi_pepi getClstbl_plant_repiq_fiche_suivi_pepi(object intid)
        {
            clstbl_plant_repiq_fiche_suivi_pepi varclstbl_plant_repiq_fiche_suivi_pepi = new clstbl_plant_repiq_fiche_suivi_pepi();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_plant_repiq_fiche_suivi_pepi WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Id = int.Parse(dr["id"].ToString());
                            varclstbl_plant_repiq_fiche_suivi_pepi.Uuid = dr["uuid"].ToString();
                            varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_essence = dr["planches_repiquage_essence"].ToString();
                            varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_essence_autre = dr["planches_repiquage_essence_autre"].ToString();
                            if (!dr["plantules_encore_repiques"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Plantules_encore_repiques = int.Parse(dr["plantules_encore_repiques"].ToString());
                            if (!dr["plantules_deja_evacues"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Plantules_deja_evacues = int.Parse(dr["plantules_deja_evacues"].ToString());
                            if (!dr["qte_observee"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Qte_observee = int.Parse(dr["qte_observee"].ToString());
                            if (!dr["date_repiquage"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Date_repiquage = DateTime.Parse(dr["date_repiquage"].ToString());
                            if (!dr["taille_moyenne"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Taille_moyenne = int.Parse(dr["taille_moyenne"].ToString());
                            if (!dr["nbre_feuille_moyenne"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Nbre_feuille_moyenne = int.Parse(dr["nbre_feuille_moyenne"].ToString());
                            if (!dr["planches_repiquage_count"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_count = double.Parse(dr["planches_repiquage_count"].ToString());
                            varclstbl_plant_repiq_fiche_suivi_pepi.Observations = dr["observations"].ToString();
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_plant_repiq_fiche_suivi_pepi.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_plant_repiq_fiche_suivi_pepi' avec la classe 'clstbl_plant_repiq_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_plant_repiq_fiche_suivi_pepi;
        }

        public DataTable getAllClstbl_plant_repiq_fiche_suivi_pepi(string criteria)
        {
            DataTable dtclstbl_plant_repiq_fiche_suivi_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_plant_repiq_fiche_suivi_pepi  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   planches_repiquage_essence LIKE '%" + criteria + "%'";
                    sql += "  OR   planches_repiquage_essence_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   observations LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_plant_repiq_fiche_suivi_pepi.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_plant_repiq_fiche_suivi_pepi' avec la classe 'clstbl_plant_repiq_fiche_suivi_pepi' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_plant_repiq_fiche_suivi_pepi;
        }

        public DataTable getAllClstbl_plant_repiq_fiche_suivi_pepi()
        {
            DataTable dtclstbl_plant_repiq_fiche_suivi_pepi = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_plant_repiq_fiche_suivi_pepi ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_plant_repiq_fiche_suivi_pepi.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_plant_repiq_fiche_suivi_pepi' avec la classe 'clstbl_plant_repiq_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_plant_repiq_fiche_suivi_pepi;
        }

        public int insertClstbl_plant_repiq_fiche_suivi_pepi(clstbl_plant_repiq_fiche_suivi_pepi varclstbl_plant_repiq_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_plant_repiq_fiche_suivi_pepi ( uuid,planches_repiquage_essence,planches_repiquage_essence_autre,plantules_encore_repiques,plantules_deja_evacues,qte_observee,date_repiquage,taille_moyenne,nbre_feuille_moyenne,planches_repiquage_count,observations,synchronized_on ) VALUES (@uuid,@planches_repiquage_essence,@planches_repiquage_essence_autre,@plantules_encore_repiques,@plantules_deja_evacues,@qte_observee,@date_repiquage,@taille_moyenne,@nbre_feuille_moyenne,@planches_repiquage_count,@observations,@synchronized_on  )");
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_plant_repiq_fiche_suivi_pepi.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_essence != null) cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_essence", DbType.String, 255, varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_essence));
                    else cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_essence", DbType.String, 255, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_essence_autre != null) cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_essence_autre", DbType.String, 255, varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_essence_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_essence_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Plantules_encore_repiques.HasValue) cmd.Parameters.Add(getParameter(cmd, "@plantules_encore_repiques", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi.Plantules_encore_repiques));
                    else cmd.Parameters.Add(getParameter(cmd, "@plantules_encore_repiques", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Plantules_deja_evacues.HasValue) cmd.Parameters.Add(getParameter(cmd, "@plantules_deja_evacues", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi.Plantules_deja_evacues));
                    else cmd.Parameters.Add(getParameter(cmd, "@plantules_deja_evacues", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Qte_observee.HasValue) cmd.Parameters.Add(getParameter(cmd, "@qte_observee", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi.Qte_observee));
                    else cmd.Parameters.Add(getParameter(cmd, "@qte_observee", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Date_repiquage.HasValue) cmd.Parameters.Add(getParameter(cmd, "@date_repiquage", DbType.DateTime, 8, varclstbl_plant_repiq_fiche_suivi_pepi.Date_repiquage));
                    else cmd.Parameters.Add(getParameter(cmd, "@date_repiquage", DbType.DateTime, 8, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Taille_moyenne.HasValue) cmd.Parameters.Add(getParameter(cmd, "@taille_moyenne", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi.Taille_moyenne));
                    else cmd.Parameters.Add(getParameter(cmd, "@taille_moyenne", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Nbre_feuille_moyenne.HasValue) cmd.Parameters.Add(getParameter(cmd, "@nbre_feuille_moyenne", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi.Nbre_feuille_moyenne));
                    else cmd.Parameters.Add(getParameter(cmd, "@nbre_feuille_moyenne", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_count.HasValue) cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_count", DbType.Single, 4, varclstbl_plant_repiq_fiche_suivi_pepi.Planches_repiquage_count));
                    else cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_count", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_plant_repiq_fiche_suivi_pepi.Observations != null) cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, varclstbl_plant_repiq_fiche_suivi_pepi.Observations));
                    else cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_plant_repiq_fiche_suivi_pepi.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_plant_repiq_fiche_suivi_pepi' avec la classe 'clstbl_plant_repiq_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_plant_repiq_fiche_suivi_pepi(DataRowView varclstbl_plant_repiq_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_plant_repiq_fiche_suivi_pepi  SET uuid=@uuid,planches_repiquage_essence=@planches_repiquage_essence,planches_repiquage_essence_autre=@planches_repiquage_essence_autre,plantules_encore_repiques=@plantules_encore_repiques,plantules_deja_evacues=@plantules_deja_evacues,qte_observee=@qte_observee,date_repiquage=@date_repiquage,taille_moyenne=@taille_moyenne,nbre_feuille_moyenne=@nbre_feuille_moyenne,planches_repiquage_count=@planches_repiquage_count,observations=@observations,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_plant_repiq_fiche_suivi_pepi["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_essence", DbType.String, 255, varclstbl_plant_repiq_fiche_suivi_pepi["planches_repiquage_essence"]));
                    cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_essence_autre", DbType.String, 255, varclstbl_plant_repiq_fiche_suivi_pepi["planches_repiquage_essence_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@plantules_encore_repiques", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi["plantules_encore_repiques"]));
                    cmd.Parameters.Add(getParameter(cmd, "@plantules_deja_evacues", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi["plantules_deja_evacues"]));
                    cmd.Parameters.Add(getParameter(cmd, "@qte_observee", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi["qte_observee"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date_repiquage", DbType.DateTime, 8, varclstbl_plant_repiq_fiche_suivi_pepi["date_repiquage"]));
                    cmd.Parameters.Add(getParameter(cmd, "@taille_moyenne", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi["taille_moyenne"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nbre_feuille_moyenne", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi["nbre_feuille_moyenne"]));
                    cmd.Parameters.Add(getParameter(cmd, "@planches_repiquage_count", DbType.Single, 4, varclstbl_plant_repiq_fiche_suivi_pepi["planches_repiquage_count"]));
                    cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, varclstbl_plant_repiq_fiche_suivi_pepi["observations"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_plant_repiq_fiche_suivi_pepi["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_plant_repiq_fiche_suivi_pepi' avec la classe 'clstbl_plant_repiq_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_plant_repiq_fiche_suivi_pepi(DataRowView varclstbl_plant_repiq_fiche_suivi_pepi)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_plant_repiq_fiche_suivi_pepi  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_plant_repiq_fiche_suivi_pepi["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_plant_repiq_fiche_suivi_pepi' avec la classe 'clstbl_plant_repiq_fiche_suivi_pepi' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_PLANT_REPIQ_FICHE_SUIVI_PEPI 
        #region  CLSTBL_TERRITOIRE
        public clstbl_territoire getClstbl_territoire(object intid)
        {
            clstbl_territoire varclstbl_territoire = new clstbl_territoire();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_territoire WHERE idt={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["idt"].ToString().Trim().Equals("")) varclstbl_territoire.Idt = int.Parse(dr["idt"].ToString());
                            varclstbl_territoire.Territoire = dr["territoire"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_territoire' avec la classe 'clstbl_territoire' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_territoire;
        }

        public DataTable getAllClstbl_territoire(string criteria)
        {
            DataTable dtclstbl_territoire = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_territoire  WHERE 1=1";
                    sql += "  OR   territoire LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_territoire.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_territoire' avec la classe 'clstbl_territoire' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_territoire;
        }

        public DataTable getAllClstbl_territoire()
        {
            DataTable dtclstbl_territoire = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_territoire ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_territoire.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_territoire' avec la classe 'clstbl_territoire' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_territoire;
        }

        public int insertClstbl_territoire(clstbl_territoire varclstbl_territoire)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_territoire ( territoire ) VALUES (@territoire  )");
                    if (varclstbl_territoire.Territoire != null) cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 100, varclstbl_territoire.Territoire));
                    else cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 100, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_territoire' avec la classe 'clstbl_territoire' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_territoire(DataRowView varclstbl_territoire)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_territoire  SET territoire=@territoire  WHERE 1=1  AND idt=@idt ");
                    cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 100, varclstbl_territoire["territoire"]));
                    cmd.Parameters.Add(getParameter(cmd, "@idt", DbType.Int32, 4, varclstbl_territoire["idt"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_territoire' avec la classe 'clstbl_territoire' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_territoire(DataRowView varclstbl_territoire)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_territoire  WHERE  1=1  AND idt=@idt ");
                    cmd.Parameters.Add(getParameter(cmd, "@idt", DbType.Int32, 4, varclstbl_territoire["idt"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_territoire' avec la classe 'clstbl_territoire' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_TERRITOIRE 
        #region  CLSTBL_GROUPEMENT
        public clstbl_groupement getClstbl_groupement(object intid)
        {
            clstbl_groupement varclstbl_groupement = new clstbl_groupement();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_groupement WHERE idg={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["idg"].ToString().Trim().Equals("")) varclstbl_groupement.Idg = int.Parse(dr["idg"].ToString());
                            if (!dr["idt"].ToString().Trim().Equals("")) varclstbl_groupement.Idt = int.Parse(dr["idt"].ToString());
                            varclstbl_groupement.Groupement = dr["groupement"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_groupement' avec la classe 'clstbl_groupement' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_groupement;
        }

        public DataTable getAllClstbl_groupement(string criteria)
        {
            DataTable dtclstbl_groupement = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_groupement  WHERE 1=1";
                    sql += "  OR   groupement LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_groupement.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_groupement' avec la classe 'clstbl_groupement' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_groupement;
        }

        public DataTable getAllClstbl_groupement()
        {
            DataTable dtclstbl_groupement = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_groupement ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_groupement.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_groupement' avec la classe 'clstbl_groupement' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_groupement;
        }

        public int insertClstbl_groupement(clstbl_groupement varclstbl_groupement)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_groupement ( idt,groupement ) VALUES (@idt,@groupement  )");
                    cmd.Parameters.Add(getParameter(cmd, "@idt", DbType.Int32, 4, varclstbl_groupement.Idt));
                    if (varclstbl_groupement.Groupement != null) cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 100, varclstbl_groupement.Groupement));
                    else cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 100, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_groupement' avec la classe 'clstbl_groupement' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_groupement(DataRowView varclstbl_groupement)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_groupement  SET idt=@idt,groupement=@groupement  WHERE 1=1  AND idg=@idg ");
                    cmd.Parameters.Add(getParameter(cmd, "@idt", DbType.Int32, 4, varclstbl_groupement["idt"]));
                    cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 100, varclstbl_groupement["groupement"]));
                    cmd.Parameters.Add(getParameter(cmd, "@idg", DbType.Int32, 4, varclstbl_groupement["idg"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_groupement' avec la classe 'clstbl_groupement' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_groupement(DataRowView varclstbl_groupement)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_groupement  WHERE  1=1  AND idg=@idg ");
                    cmd.Parameters.Add(getParameter(cmd, "@idg", DbType.Int32, 4, varclstbl_groupement["idg"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_groupement' avec la classe 'clstbl_groupement' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_GROUPEMENT 
        #region  CLSTBL_LOCALITE
        public clstbl_localite getClstbl_localite(object intid)
        {
            clstbl_localite varclstbl_localite = new clstbl_localite();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_localite WHERE idl={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["idl"].ToString().Trim().Equals("")) varclstbl_localite.Idl = int.Parse(dr["idl"].ToString());
                            if (!dr["idg"].ToString().Trim().Equals("")) varclstbl_localite.Idg = int.Parse(dr["idg"].ToString());
                            varclstbl_localite.Localite = dr["localite"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_localite' avec la classe 'clstbl_localite' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_localite;
        }

        public DataTable getAllClstbl_localite(string criteria)
        {
            DataTable dtclstbl_localite = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_localite  WHERE 1=1";
                    sql += "  OR   localite LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_localite.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_localite' avec la classe 'clstbl_localite' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_localite;
        }

        public DataTable getAllClstbl_localite()
        {
            DataTable dtclstbl_localite = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_localite ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_localite.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_localite' avec la classe 'clstbl_localite' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_localite;
        }

        public int insertClstbl_localite(clstbl_localite varclstbl_localite)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_localite ( idg,localite ) VALUES (@idg,@localite  )");
                    cmd.Parameters.Add(getParameter(cmd, "@idg", DbType.Int32, 4, varclstbl_localite.Idg));
                    if (varclstbl_localite.Localite != null) cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 100, varclstbl_localite.Localite));
                    else cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 100, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_localite' avec la classe 'clstbl_localite' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_localite(DataRowView varclstbl_localite)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_localite  SET idg=@idg,localite=@localite  WHERE 1=1  AND idl=@idl ");
                    cmd.Parameters.Add(getParameter(cmd, "@idg", DbType.Int32, 4, varclstbl_localite["idg"]));
                    cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 100, varclstbl_localite["localite"]));
                    cmd.Parameters.Add(getParameter(cmd, "@idl", DbType.Int32, 4, varclstbl_localite["idl"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_localite' avec la classe 'clstbl_localite' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_localite(DataRowView varclstbl_localite)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_localite  WHERE  1=1  AND idl=@idl ");
                    cmd.Parameters.Add(getParameter(cmd, "@idl", DbType.Int32, 4, varclstbl_localite["idl"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_localite' avec la classe 'clstbl_localite' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_LOCALITE 
        #region  CLSTBL_CHEFFERIE
        public clstbl_chefferie getClstbl_chefferie(object intid)
        {
            clstbl_chefferie varclstbl_chefferie = new clstbl_chefferie();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_chefferie WHERE idc={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["idc"].ToString().Trim().Equals("")) varclstbl_chefferie.Idc = int.Parse(dr["idc"].ToString());
                            if (!dr["idl"].ToString().Trim().Equals("")) varclstbl_chefferie.Idl = int.Parse(dr["idl"].ToString());
                            varclstbl_chefferie.Chefferie = dr["chefferie"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_chefferie' avec la classe 'clstbl_chefferie' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_chefferie;
        }

        public DataTable getAllClstbl_chefferie(string criteria)
        {
            DataTable dtclstbl_chefferie = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_chefferie  WHERE 1=1";
                    sql += "  OR   chefferie LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_chefferie.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_chefferie' avec la classe 'clstbl_chefferie' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_chefferie;
        }

        public DataTable getAllClstbl_chefferie()
        {
            DataTable dtclstbl_chefferie = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_chefferie ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_chefferie.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_chefferie' avec la classe 'clstbl_chefferie' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_chefferie;
        }

        public int insertClstbl_chefferie(clstbl_chefferie varclstbl_chefferie)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_chefferie ( idl,chefferie ) VALUES (@idl,@chefferie  )");
                    cmd.Parameters.Add(getParameter(cmd, "@idl", DbType.Int32, 4, varclstbl_chefferie.Idl));
                    if (varclstbl_chefferie.Chefferie != null) cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 100, varclstbl_chefferie.Chefferie));
                    else cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 100, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_chefferie' avec la classe 'clstbl_chefferie' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_chefferie(DataRowView varclstbl_chefferie)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_chefferie  SET idl=@idl,chefferie=@chefferie  WHERE 1=1  AND idc=@idc ");
                    cmd.Parameters.Add(getParameter(cmd, "@idl", DbType.Int32, 4, varclstbl_chefferie["idl"]));
                    cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 100, varclstbl_chefferie["chefferie"]));
                    cmd.Parameters.Add(getParameter(cmd, "@idc", DbType.Int32, 4, varclstbl_chefferie["idc"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_chefferie' avec la classe 'clstbl_chefferie' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_chefferie(DataRowView varclstbl_chefferie)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_chefferie  WHERE  1=1  AND idc=@idc ");
                    cmd.Parameters.Add(getParameter(cmd, "@idc", DbType.Int32, 4, varclstbl_chefferie["idc"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_chefferie' avec la classe 'clstbl_chefferie' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_CHEFFERIE 
        #region  CLSTBL_VILLAGE
        public clstbl_village getClstbl_village(object intid)
        {
            clstbl_village varclstbl_village = new clstbl_village();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_village WHERE idv={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["idv"].ToString().Trim().Equals("")) varclstbl_village.Idv = int.Parse(dr["idv"].ToString());
                            if (!dr["idc"].ToString().Trim().Equals("")) varclstbl_village.Idc = int.Parse(dr["idc"].ToString());
                            varclstbl_village.Village = dr["village"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_village' avec la classe 'clstbl_village' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_village;
        }

        public DataTable getAllClstbl_village(string criteria)
        {
            DataTable dtclstbl_village = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_village  WHERE 1=1";
                    sql += "  OR   village LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_village.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_village' avec la classe 'clstbl_village' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_village;
        }

        public DataTable getAllClstbl_village()
        {
            DataTable dtclstbl_village = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_village ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_village.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_village' avec la classe 'clstbl_village' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_village;
        }

        public int insertClstbl_village(clstbl_village varclstbl_village)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_village ( idc,village ) VALUES (@idc,@village  )");
                    cmd.Parameters.Add(getParameter(cmd, "@idc", DbType.Int32, 4, varclstbl_village.Idc));
                    if (varclstbl_village.Village != null) cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 100, varclstbl_village.Village));
                    else cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 100, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_village' avec la classe 'clstbl_village' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_village(DataRowView varclstbl_village)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_village  SET idc=@idc,village=@village  WHERE 1=1  AND idv=@idv ");
                    cmd.Parameters.Add(getParameter(cmd, "@idc", DbType.Int32, 4, varclstbl_village["idc"]));
                    cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 100, varclstbl_village["village"]));
                    cmd.Parameters.Add(getParameter(cmd, "@idv", DbType.Int32, 4, varclstbl_village["idv"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_village' avec la classe 'clstbl_village' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_village(DataRowView varclstbl_village)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_village  WHERE  1=1  AND idv=@idv ");
                    cmd.Parameters.Add(getParameter(cmd, "@idv", DbType.Int32, 4, varclstbl_village["idv"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_village' avec la classe 'clstbl_village' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_VILLAGE 
        #region  CLSTBL_SAISON
        public clstbl_saison getClstbl_saison(object intid)
        {
            clstbl_saison varclstbl_saison = new clstbl_saison();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_saison WHERE id_saison={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            varclstbl_saison.Id_saison = dr["id_saison"].ToString();
                            varclstbl_saison.Saison = dr["saison"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_saison' avec la classe 'clstbl_saison' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_saison;
        }

        public DataTable getAllClstbl_saison(string criteria)
        {
            DataTable dtclstbl_saison = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_saison  WHERE 1=1";
                    sql += "  OR   id_saison LIKE '%" + criteria + "%'";
                    sql += "  OR   saison LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_saison.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_saison' avec la classe 'clstbl_saison' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_saison;
        }

        public DataTable getAllClstbl_saison()
        {
            DataTable dtclstbl_saison = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_saison ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_saison.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_saison' avec la classe 'clstbl_saison' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_saison;
        }

        public int insertClstbl_saison(clstbl_saison varclstbl_saison)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_saison ( id_saison,saison ) VALUES (@id_saison,@saison  )");
                    if (varclstbl_saison.Id_saison != null) cmd.Parameters.Add(getParameter(cmd, "@id_saison", DbType.String, 6, varclstbl_saison.Id_saison));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_saison", DbType.String, 6, DBNull.Value));
                    if (varclstbl_saison.Saison != null) cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 25, varclstbl_saison.Saison));
                    else cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 25, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_saison' avec la classe 'clstbl_saison' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_saison(DataRowView varclstbl_saison)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_saison  SET saison=@saison  WHERE 1=1  AND id_saison=@id_saison ");
                    cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 25, varclstbl_saison["saison"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_saison", DbType.String, 6, varclstbl_saison["id_saison"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_saison' avec la classe 'clstbl_saison' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_saison(DataRowView varclstbl_saison)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_saison  WHERE  1=1  AND id_saison=@id_saison ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_saison", DbType.String, 6, varclstbl_saison["id_saison"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_saison' avec la classe 'clstbl_saison' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_SAISON 
        #region  CLSTBL_AGENT
        public clstbl_agent getClstbl_agent(object intid)
        {
            clstbl_agent varclstbl_agent = new clstbl_agent();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_agent WHERE id_agent={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            varclstbl_agent.Id_agent = dr["id_agent"].ToString();
                            varclstbl_agent.Agent = dr["agent"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_agent' avec la classe 'clstbl_agent' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_agent;
        }

        public DataTable getAllClstbl_agent(string criteria)
        {
            DataTable dtclstbl_agent = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_agent  WHERE 1=1";
                    sql += "  OR   id_agent LIKE '%" + criteria + "%'";
                    sql += "  OR   agent LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_agent.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_agent' avec la classe 'clstbl_agent' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_agent;
        }

        public DataTable getAllClstbl_agent()
        {
            DataTable dtclstbl_agent = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_agent ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_agent.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_agent' avec la classe 'clstbl_agent' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_agent;
        }

        public int insertClstbl_agent(clstbl_agent varclstbl_agent)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_agent ( id_agent,agent ) VALUES (@id_agent,@agent  )");
                    if (varclstbl_agent.Id_agent != null) cmd.Parameters.Add(getParameter(cmd, "@id_agent", DbType.String, 6, varclstbl_agent.Id_agent));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_agent", DbType.String, 6, DBNull.Value));
                    if (varclstbl_agent.Agent != null) cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 100, varclstbl_agent.Agent));
                    else cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 100, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_agent' avec la classe 'clstbl_agent' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_agent(DataRowView varclstbl_agent)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_agent  SET agent=@agent  WHERE 1=1  AND id_agent=@id_agent ");
                    cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 100, varclstbl_agent["agent"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_agent", DbType.String, 6, varclstbl_agent["id_agent"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_agent' avec la classe 'clstbl_agent' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_agent(DataRowView varclstbl_agent)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_agent  WHERE  1=1  AND id_agent=@id_agent ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_agent", DbType.String, 6, varclstbl_agent["id_agent"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_agent' avec la classe 'clstbl_agent' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_AGENT 
        #region  CLSTBL_ASSOCIATION
        public clstbl_association getClstbl_association(object intid)
        {
            clstbl_association varclstbl_association = new clstbl_association();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_association WHERE id_asso={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            varclstbl_association.Id_asso = dr["id_asso"].ToString();
                            varclstbl_association.Association = dr["association"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_association' avec la classe 'clstbl_association' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_association;
        }

        public DataTable getAllClstbl_association(string criteria)
        {
            DataTable dtclstbl_association = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_association  WHERE 1=1";
                    sql += "  OR   id_asso LIKE '%" + criteria + "%'";
                    sql += "  OR   association LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_association.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_association' avec la classe 'clstbl_association' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_association;
        }

        public DataTable getAllClstbl_association()
        {
            DataTable dtclstbl_association = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_association ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_association.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_association' avec la classe 'clstbl_association' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_association;
        }

        public int insertClstbl_association(clstbl_association varclstbl_association)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_association ( id_asso,association ) VALUES (@id_asso,@association  )");
                    if (varclstbl_association.Id_asso != null) cmd.Parameters.Add(getParameter(cmd, "@id_asso", DbType.String, 25, varclstbl_association.Id_asso));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_asso", DbType.String, 25, DBNull.Value));
                    if (varclstbl_association.Association != null) cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 50, varclstbl_association.Association));
                    else cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 50, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_association' avec la classe 'clstbl_association' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_association(DataRowView varclstbl_association)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_association  SET association=@association  WHERE 1=1  AND id_asso=@id_asso ");
                    cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 50, varclstbl_association["association"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_asso", DbType.String, 25, varclstbl_association["id_asso"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_association' avec la classe 'clstbl_association' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_association(DataRowView varclstbl_association)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_association  WHERE  1=1  AND id_asso=@id_asso ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_asso", DbType.String, 25, varclstbl_association["id_asso"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_association' avec la classe 'clstbl_association' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_ASSOCIATION 
        #region  CLSTBL_BAILLEUR
        public clstbl_bailleur getClstbl_bailleur(object intid)
        {
            clstbl_bailleur varclstbl_bailleur = new clstbl_bailleur();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_bailleur WHERE id_bailleur={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            varclstbl_bailleur.Id_bailleur = dr["id_bailleur"].ToString();
                            varclstbl_bailleur.Bailleur = dr["bailleur"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_bailleur' avec la classe 'clstbl_bailleur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_bailleur;
        }

        public DataTable getAllClstbl_bailleur(string criteria)
        {
            DataTable dtclstbl_bailleur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_bailleur  WHERE 1=1";
                    sql += "  OR   id_bailleur LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_bailleur.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_bailleur' avec la classe 'clstbl_bailleur' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_bailleur;
        }

        public DataTable getAllClstbl_bailleur()
        {
            DataTable dtclstbl_bailleur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_bailleur ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_bailleur.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_bailleur' avec la classe 'clstbl_bailleur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_bailleur;
        }

        public int insertClstbl_bailleur(clstbl_bailleur varclstbl_bailleur)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_bailleur ( id_bailleur,bailleur ) VALUES (@id_bailleur,@bailleur  )");
                    if (varclstbl_bailleur.Id_bailleur != null) cmd.Parameters.Add(getParameter(cmd, "@id_bailleur", DbType.String, 20, varclstbl_bailleur.Id_bailleur));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_bailleur", DbType.String, 20, DBNull.Value));
                    if (varclstbl_bailleur.Bailleur != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 75, varclstbl_bailleur.Bailleur));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 75, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_bailleur' avec la classe 'clstbl_bailleur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_bailleur(DataRowView varclstbl_bailleur)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_bailleur  SET bailleur=@bailleur  WHERE 1=1  AND id_bailleur=@id_bailleur ");
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 75, varclstbl_bailleur["bailleur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_bailleur", DbType.String, 20, varclstbl_bailleur["id_bailleur"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_bailleur' avec la classe 'clstbl_bailleur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_bailleur(DataRowView varclstbl_bailleur)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_bailleur  WHERE  1=1  AND id_bailleur=@id_bailleur ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_bailleur", DbType.String, 20, varclstbl_bailleur["id_bailleur"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_bailleur' avec la classe 'clstbl_bailleur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_BAILLEUR 
        #region  CLSTBL_SAISON_ASSOC
        public clstbl_saison_assoc getClstbl_saison_assoc(object intid)
        {
            clstbl_saison_assoc varclstbl_saison_assoc = new clstbl_saison_assoc();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_saison_assoc WHERE id_saison_assoc={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            varclstbl_saison_assoc.Id_asso = dr["id_asso"].ToString();
                            varclstbl_saison_assoc.Id_saison = dr["id_saison"].ToString();
                            varclstbl_saison_assoc.Id_saison_assoc = dr["id_saison_assoc"].ToString();
                            varclstbl_saison_assoc.Numero_contrat_asso = dr["numero_contrat_asso"].ToString();
                            if (!dr["surf_contr"].ToString().Trim().Equals("")) varclstbl_saison_assoc.Surf_contr = float.Parse(dr["surf_contr"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_saison_assoc' avec la classe 'clstbl_saison_assoc' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_saison_assoc;
        }

        public DataTable getAllClstbl_saison_assoc(string criteria)
        {
            DataTable dtclstbl_saison_assoc = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_saison_assoc  WHERE 1=1";
                    sql += "  OR   id_asso LIKE '%" + criteria + "%'";
                    sql += "  OR   id_saison LIKE '%" + criteria + "%'";
                    sql += "  OR   id_saison_assoc LIKE '%" + criteria + "%'";
                    sql += "  OR   numero_contrat_asso LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_saison_assoc.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_saison_assoc' avec la classe 'clstbl_saison_assoc' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_saison_assoc;
        }

        public DataTable getAllClstbl_saison_assoc()
        {
            DataTable dtclstbl_saison_assoc = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_saison_assoc ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_saison_assoc.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_saison_assoc' avec la classe 'clstbl_saison_assoc' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_saison_assoc;
        }

        public int insertClstbl_saison_assoc(clstbl_saison_assoc varclstbl_saison_assoc)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_saison_assoc ( id_asso,id_saison,id_saison_assoc,numero_contrat_asso,surf_contr ) VALUES (@id_asso,@id_saison,@id_saison_assoc,@numero_contrat_asso,@surf_contr  )");
                    if (varclstbl_saison_assoc.Id_asso != null) cmd.Parameters.Add(getParameter(cmd, "@id_asso", DbType.String, 25, varclstbl_saison_assoc.Id_asso));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_asso", DbType.String, 25, DBNull.Value));
                    if (varclstbl_saison_assoc.Id_saison != null) cmd.Parameters.Add(getParameter(cmd, "@id_saison", DbType.String, 6, varclstbl_saison_assoc.Id_saison));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_saison", DbType.String, 6, DBNull.Value));
                    if (varclstbl_saison_assoc.Id_saison_assoc != null) cmd.Parameters.Add(getParameter(cmd, "@id_saison_assoc", DbType.String, 32, varclstbl_saison_assoc.Id_saison_assoc));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_saison_assoc", DbType.String, 32, DBNull.Value));
                    if (varclstbl_saison_assoc.Numero_contrat_asso != null) cmd.Parameters.Add(getParameter(cmd, "@numero_contrat_asso", DbType.String, 50, varclstbl_saison_assoc.Numero_contrat_asso));
                    else cmd.Parameters.Add(getParameter(cmd, "@numero_contrat_asso", DbType.String, 50, DBNull.Value));
                    if (varclstbl_saison_assoc.Surf_contr.HasValue) cmd.Parameters.Add(getParameter(cmd, "@surf_contr", DbType.Double, 8, varclstbl_saison_assoc.Surf_contr));
                    else cmd.Parameters.Add(getParameter(cmd, "@surf_contr", DbType.Double, 8, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_saison_assoc' avec la classe 'clstbl_saison_assoc' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_saison_assoc(DataRowView varclstbl_saison_assoc)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_saison_assoc  SET id_asso=@id_asso,id_saison=@id_saison,numero_contrat_asso=@numero_contrat_asso,surf_contr=@surf_contr  WHERE 1=1  AND id_saison_assoc=@id_saison_assoc ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_asso", DbType.String, 25, varclstbl_saison_assoc["id_asso"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_saison", DbType.String, 6, varclstbl_saison_assoc["id_saison"]));
                    cmd.Parameters.Add(getParameter(cmd, "@numero_contrat_asso", DbType.String, 50, varclstbl_saison_assoc["numero_contrat_asso"]));
                    cmd.Parameters.Add(getParameter(cmd, "@surf_contr", DbType.Double, 8, varclstbl_saison_assoc["surf_contr"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_saison_assoc", DbType.String, 32, varclstbl_saison_assoc["id_saison_assoc"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_saison_assoc' avec la classe 'clstbl_saison_assoc' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_saison_assoc(DataRowView varclstbl_saison_assoc)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_saison_assoc  WHERE  1=1  AND id_saison_assoc=@id_saison_assoc ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_saison_assoc", DbType.String, 32, varclstbl_saison_assoc["id_saison_assoc"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_saison_assoc' avec la classe 'clstbl_saison_assoc' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_SAISON_ASSOC 
        #region  CLSTBL_ESSENCE_PLANT
        public clstbl_essence_plant getClstbl_essence_plant(object intid)
        {
            clstbl_essence_plant varclstbl_essence_plant = new clstbl_essence_plant();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_essence_plant WHERE id_essence={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            varclstbl_essence_plant.Id_essence = dr["id_essence"].ToString();
                            varclstbl_essence_plant.Essence = dr["essence"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_essence_plant' avec la classe 'clstbl_essence_plant' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_essence_plant;
        }

        public DataTable getAllClstbl_essence_plant(string criteria)
        {
            DataTable dtclstbl_essence_plant = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_essence_plant  WHERE 1=1";
                    sql += "  OR   id_essence LIKE '%" + criteria + "%'";
                    sql += "  OR   essence LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_essence_plant.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_essence_plant' avec la classe 'clstbl_essence_plant' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_essence_plant;
        }

        public DataTable getAllClstbl_essence_plant()
        {
            DataTable dtclstbl_essence_plant = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_essence_plant ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_essence_plant.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_essence_plant' avec la classe 'clstbl_essence_plant' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_essence_plant;
        }

        public int insertClstbl_essence_plant(clstbl_essence_plant varclstbl_essence_plant)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_essence_plant ( id_essence,essence ) VALUES (@id_essence,@essence  )");
                    if (varclstbl_essence_plant.Id_essence != null) cmd.Parameters.Add(getParameter(cmd, "@id_essence", DbType.String, 20, varclstbl_essence_plant.Id_essence));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_essence", DbType.String, 20, DBNull.Value));
                    if (varclstbl_essence_plant.Essence != null) cmd.Parameters.Add(getParameter(cmd, "@essence", DbType.String, 100, varclstbl_essence_plant.Essence));
                    else cmd.Parameters.Add(getParameter(cmd, "@essence", DbType.String, 100, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_essence_plant' avec la classe 'clstbl_essence_plant' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_essence_plant(DataRowView varclstbl_essence_plant)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_essence_plant  SET essence=@essence  WHERE 1=1  AND id_essence=@id_essence ");
                    cmd.Parameters.Add(getParameter(cmd, "@essence", DbType.String, 100, varclstbl_essence_plant["essence"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_essence", DbType.String, 20, varclstbl_essence_plant["id_essence"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_essence_plant' avec la classe 'clstbl_essence_plant' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_essence_plant(DataRowView varclstbl_essence_plant)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_essence_plant  WHERE  1=1  AND id_essence=@id_essence ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_essence", DbType.String, 20, varclstbl_essence_plant["id_essence"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_essence_plant' avec la classe 'clstbl_essence_plant' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_ESSENCE_PLANT 
        #region  CLSTBL_UTILISATEUR
        public clstbl_utilisateur getClstbl_utilisateur(object intid)
        {
            clstbl_utilisateur varclstbl_utilisateur = new clstbl_utilisateur();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_utilisateur WHERE id_utilisateur={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id_utilisateur"].ToString().Trim().Equals("")) varclstbl_utilisateur.Id_utilisateur = int.Parse(dr["id_utilisateur"].ToString());
                            varclstbl_utilisateur.Id_agentuser = dr["id_agentuser"].ToString();
                            varclstbl_utilisateur.Nomuser = dr["nomuser"].ToString();
                            varclstbl_utilisateur.Motpass = dr["motpass"].ToString();
                            varclstbl_utilisateur.Schema_user = dr["schema_user"].ToString();
                            varclstbl_utilisateur.Droits = dr["droits"].ToString();
                            if (!dr["activation"].ToString().Trim().Equals("")) varclstbl_utilisateur.Activation = bool.Parse(dr["activation"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_utilisateur;
        }

        public DataTable getAllClstbl_utilisateur(string criteria)
        {
            DataTable dtclstbl_utilisateur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_utilisateur  WHERE 1=1";
                    sql += "  OR   id_agentuser LIKE '%" + criteria + "%'";
                    sql += "  OR   nomuser LIKE '%" + criteria + "%'";
                    sql += "  OR   motpass LIKE '%" + criteria + "%'";
                    sql += "  OR   schema_user LIKE '%" + criteria + "%'";
                    sql += "  OR   droits LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_utilisateur.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_utilisateur;
        }

        public DataTable getAllClstbl_utilisateur()
        {
            DataTable dtclstbl_utilisateur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_utilisateur ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_utilisateur.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_utilisateur;
        }

        public int insertClstbl_utilisateur1(clstbl_utilisateur varclstbl_utilisateur)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_utilisateur ( id_agentuser,nomuser,motpass,schema_user,droits,activation ) VALUES (@id_agentuser,@nomuser,@motpass,@schema_user,@droits,@activation  )");
                    if (varclstbl_utilisateur.Id_agentuser != null) cmd.Parameters.Add(getParameter(cmd, "@id_agentuser", DbType.String, 6, varclstbl_utilisateur.Id_agentuser));
                    else cmd.Parameters.Add(getParameter(cmd, "@id_agentuser", DbType.String, 6, DBNull.Value));
                    if (varclstbl_utilisateur.Nomuser != null) cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, varclstbl_utilisateur.Nomuser));
                    else cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, DBNull.Value));
                    if (varclstbl_utilisateur.Motpass != null) cmd.Parameters.Add(getParameter(cmd, "@motpass", DbType.String, 1000, varclstbl_utilisateur.Motpass));
                    else cmd.Parameters.Add(getParameter(cmd, "@motpass", DbType.String, 1000, DBNull.Value));
                    if (varclstbl_utilisateur.Schema_user != null) cmd.Parameters.Add(getParameter(cmd, "@schema_user", DbType.String, 20, varclstbl_utilisateur.Schema_user));
                    else cmd.Parameters.Add(getParameter(cmd, "@schema_user", DbType.String, 20, DBNull.Value));
                    if (varclstbl_utilisateur.Droits != null) cmd.Parameters.Add(getParameter(cmd, "@droits", DbType.String, 300, varclstbl_utilisateur.Droits));
                    else cmd.Parameters.Add(getParameter(cmd, "@droits", DbType.String, 300, DBNull.Value));
                    if (varclstbl_utilisateur.Activation.HasValue) cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, varclstbl_utilisateur.Activation));
                    else cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_utilisateur1(clstbl_utilisateur varclstbl_utilisateur)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_utilisateur  SET id_agentuser=@id_agentuser,nomuser=@nomuser,motpass=@motpass,schema_user=@schema_user,droits=@droits,activation=@activation  WHERE 1=1  AND id_utilisateur=@id_utilisateur ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_agentuser", DbType.String, 6, varclstbl_utilisateur.Id_agentuser));
                    cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, varclstbl_utilisateur.Nomuser));
                    cmd.Parameters.Add(getParameter(cmd, "@motpass", DbType.String, 1000, varclstbl_utilisateur.Motpass));
                    cmd.Parameters.Add(getParameter(cmd, "@schema_user", DbType.String, 20, varclstbl_utilisateur.Schema_user));
                    cmd.Parameters.Add(getParameter(cmd, "@droits", DbType.String, 300, varclstbl_utilisateur.Droits));
                    cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, varclstbl_utilisateur.Activation));
                    cmd.Parameters.Add(getParameter(cmd, "@id_utilisateur", DbType.Int32, 4, varclstbl_utilisateur.Id_utilisateur));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_utilisateur1(clstbl_utilisateur varclstbl_utilisateur)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_utilisateur  WHERE  1=1  AND id_utilisateur=@id_utilisateur ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_utilisateur", DbType.Int32, 4, varclstbl_utilisateur.Id_utilisateur));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        //Bonne partie pour Gestion users
        public int insertClstbl_utilisateur(clstbl_utilisateur varclstbl_utilisateur)
        {
            //On crée d'abord le user en déhors de la transaction car les procedures stocke ont un traitement 
            //transactionnel par defaut
            bool echec_create = true;
            string message_erreur_user = "";

            try
            {
                //Avant de faire l'insertion dans la table utilisateur, on commence par créer le login et le user de la BD
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"exec sp_addlogin '" + varclstbl_utilisateur.Nomuser + "','" + varclstbl_utilisateur.Motpass + "','" + clsMetier.bdEnCours + @"'                                               
                                                      exec sp_grantdbaccess '" + varclstbl_utilisateur.Nomuser + @"'
                                                 ");
                    int j = cmd.ExecuteNonQuery();
                    echec_create = false;
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                message_erreur_user = exc.Message;
                conn.Close();
                throw new Exception(exc.Message);
            }

            //Dans la transaction on fait le reste
            IDbTransaction transaction = null;

            int i = 0;

            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                transaction = conn.BeginTransaction(IsolationLevel.Serializable);

                if (echec_create)
                    throw new Exception(message_erreur_user);//Si la création du user a échoué, on fait échoué le reste

                //Si l'on à cocher la case à cocher d'activation de l'utilisateur on doit le donner accès à se connecter ou non
                if (!(bool)varclstbl_utilisateur.Activation)
                {
                    using (IDbCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = string.Format(@"revoke connect to " + varclstbl_utilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                        cmd2.Transaction = transaction;

                        i = cmd2.ExecuteNonQuery();
                    }
                }

                //Insertion de l'utilisateur créé dans la table des user sans droits
                using (IDbCommand cmd3 = conn.CreateCommand())
                {
                    cmd3.CommandText = string.Format("INSERT INTO tbl_utilisateur (id_agentuser,nomuser,motpass,schema_user,activation ) VALUES (@id_agentuser,@nomuser,@motpass,@schema_user,@activation)");
                    if (varclstbl_utilisateur.Id_agentuser != null) cmd3.Parameters.Add(getParameter(cmd3, "@id_agentuser", DbType.String, 6, varclstbl_utilisateur.Id_agentuser));
                    else cmd3.Parameters.Add(getParameter(cmd3, "@id_agentuser", DbType.String, 6, DBNull.Value));
                    if (varclstbl_utilisateur.Nomuser != null) cmd3.Parameters.Add(getParameter(cmd3, "@nomuser", DbType.String, 30, varclstbl_utilisateur.Nomuser));
                    else cmd3.Parameters.Add(getParameter(cmd3, "@nomuser", DbType.String, 30, DBNull.Value));
                    if (varclstbl_utilisateur.Motpass != null) cmd3.Parameters.Add(getParameter(cmd3, "@motpass", DbType.String, 1000, ImplementChiffer.Instance.Cipher(varclstbl_utilisateur.Motpass, "rootWWF")));//On chiffre le password a mettre dans la BD
                    else cmd3.Parameters.Add(getParameter(cmd3, "@motpass", DbType.String, 1000, DBNull.Value));
                    varclstbl_utilisateur.Schema_user = varclstbl_utilisateur.Nomuser;
                    if (varclstbl_utilisateur.Schema_user != null) cmd3.Parameters.Add(getParameter(cmd3, "@schema_user", DbType.String, 20, varclstbl_utilisateur.Schema_user));
                    else cmd3.Parameters.Add(getParameter(cmd3, "@schema_user", DbType.String, 20, DBNull.Value));
                    if (varclstbl_utilisateur.Activation.HasValue) cmd3.Parameters.Add(getParameter(cmd3, "@activation", DbType.Boolean, 2, varclstbl_utilisateur.Activation));
                    else cmd3.Parameters.Add(getParameter(cmd3, "@activation", DbType.Boolean, 2, DBNull.Value));
                    cmd3.Transaction = transaction;
                    i = cmd3.ExecuteNonQuery();

                    transaction.Commit();
                }

                conn.Close();
            }
            catch (Exception exc)
            {
                if (transaction != null)
                {
                    transaction.Rollback();

                    string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                    ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec lors de la création de l'utilisateur  : " + message_erreur_user + " => " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                    throw new Exception(exc.Message);
                }
                conn.Close();
            }
            return i;
        }

        public int updateClstbl_utilisateur(clstbl_utilisateur varclstbl_utilisateur)
        {
            IDbTransaction transaction = null;
            int i = 0;
            bool ok = false;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                transaction = conn.BeginTransaction(IsolationLevel.Serializable);

                if (clsDoTraitement.etat_modification_user == 4)
                {
                    varclstbl_utilisateur.Activation = clsDoTraitement.activationUser;

                    if (conn.State != ConnectionState.Open) conn.Open();

                    if ((bool)varclstbl_utilisateur.Activation)
                    {
                        using (IDbCommand cmd3 = conn.CreateCommand())
                        {
                            cmd3.CommandText = string.Format(@"grant connect to " + varclstbl_utilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                            cmd3.Transaction = transaction;
                            i = cmd3.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (IDbCommand cmd3 = conn.CreateCommand())
                        {
                            cmd3.CommandText = string.Format(@"revoke connect to " + varclstbl_utilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                            cmd3.Transaction = transaction;
                            i = cmd3.ExecuteNonQuery();
                        }
                    }

                    using (IDbCommand cmd4 = conn.CreateCommand())
                    {
                        cmd4.CommandText = string.Format("UPDATE tbl_utilisateur SET activation=@activation  WHERE 1=1  AND id_utilisateur=@id_utilisateur ");
                        cmd4.Parameters.Add(getParameter(cmd4, "@activation", DbType.Boolean, 2, varclstbl_utilisateur.Activation));
                        cmd4.Parameters.Add(getParameter(cmd4, "@id_utilisateur", DbType.Int32, 4, varclstbl_utilisateur.Id_utilisateur));
                        cmd4.Transaction = transaction;

                        i = cmd4.ExecuteNonQuery();
                    }
                }
                else if (clsDoTraitement.etat_modification_user == 1)
                {
                    //Modification du nom user seulement

                    //Avant de modifier l'utilisateur dans la table, on modifie le user de la bd
                    using (IDbCommand cmd1 = conn.CreateCommand())
                    {
                        varclstbl_utilisateur.Nomuser = clsDoTraitement.newUser;
                        //varclstbl_utilisateur.Motpass = clsDoTraitement.oldPassword;
                        cmd1.CommandText = string.Format("alter login " + clsDoTraitement.oldUser + " with name=" + varclstbl_utilisateur.Nomuser); //On modifie le login de l'utilisateur pour changer son mode de connexion
                        cmd1.Transaction = transaction;
                        i = cmd1.ExecuteNonQuery();
                    }
                }
                else if (clsDoTraitement.etat_modification_user == 2)
                {
                    //Modification du mot de passe seulement

                    //Avant de modifier l'utilisateur dans la table, on modifie le user de la bd
                    using (IDbCommand cmd1 = conn.CreateCommand())
                    {
                        varclstbl_utilisateur.Motpass = clsDoTraitement.newPassword;
                        cmd1.CommandText = string.Format("alter LOGIN " + varclstbl_utilisateur.Nomuser + " WITH PASSWORD='" + ImplementChiffer.Instance.Decipher(clsDoTraitement.newPassword, "rootWWF") + "'"); //On modifie le login de l'utilisateur pour changer son mot de passe de connexion
                        cmd1.Transaction = transaction;
                        i = cmd1.ExecuteNonQuery();
                    }
                }
                else if (clsDoTraitement.etat_modification_user == 3)
                {
                    //Modification du nom d'utilisateur et du mot de passe

                    //Avant de modifier l'utilisateur dans la table, on modifie le user de la bd
                    using (IDbCommand cmd1 = conn.CreateCommand())
                    {
                        varclstbl_utilisateur.Nomuser = clsDoTraitement.newUser;
                        varclstbl_utilisateur.Motpass = clsDoTraitement.newPassword;
                        cmd1.CommandText = string.Format("ALTER LOGIN " + clsDoTraitement.oldUser + " WITH PASSWORD='" + ImplementChiffer.Instance.Decipher(clsDoTraitement.newPassword, "rootWWF") + "'" + @"
                                                          ALTER LOGIN " + clsDoTraitement.oldUser + " WITH NAME=" + varclstbl_utilisateur.Nomuser); //On modifie le login de l'utilisateur pour changer son mot de passe de connexion, puis on modifie son nom de login
                        cmd1.Transaction = transaction;
                        i = cmd1.ExecuteNonQuery();
                    }
                }

                if (clsDoTraitement.etat_modification_user == 1)
                {
                    //Modification de l'utilisateur
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE tbl_utilisateur  SET id_agentuser=@id_agentuser,nomuser=@nomuser,activation=@activation  WHERE 1=1  AND id_utilisateur=@id_utilisateur ");
                        if (varclstbl_utilisateur.Id_agentuser != null) cmd.Parameters.Add(getParameter(cmd, "@id_agentuser", DbType.String, 6, varclstbl_utilisateur.Id_agentuser));
                        else cmd.Parameters.Add(getParameter(cmd, "@id_agentuser", DbType.String, 6, DBNull.Value));
                        if (varclstbl_utilisateur.Nomuser != null) cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, varclstbl_utilisateur.Nomuser));
                        else cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, DBNull.Value));
                        if (varclstbl_utilisateur.Activation.HasValue) cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, varclstbl_utilisateur.Activation));
                        else cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, DBNull.Value));
                        cmd.Parameters.Add(getParameter(cmd, "@id_utilisateur", DbType.Int32, 4, varclstbl_utilisateur.Id_utilisateur));
                        cmd.Transaction = transaction;
                        i = cmd.ExecuteNonQuery();
                        ok = true;
                    }
                }
                else if (clsDoTraitement.etat_modification_user == 2 || clsDoTraitement.etat_modification_user == 3)
                {
                    //Modification de l'utilisateur
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE tbl_utilisateur  SET id_agentuser=@id_agentuser,nomuser=@nomuser,motpass=@motpass,activation=@activation  WHERE 1=1  AND id_utilisateur=@id_utilisateur ");
                        if (varclstbl_utilisateur.Id_agentuser != null) cmd.Parameters.Add(getParameter(cmd, "@id_agentuser", DbType.String, 6, varclstbl_utilisateur.Id_agentuser));
                        else cmd.Parameters.Add(getParameter(cmd, "@id_agentuser", DbType.String, 6, DBNull.Value));
                        if (varclstbl_utilisateur.Nomuser != null) cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, varclstbl_utilisateur.Nomuser));
                        else cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, DBNull.Value));
                        if (varclstbl_utilisateur.Motpass != null) cmd.Parameters.Add(getParameter(cmd, "@motpass", DbType.String, 1000, varclstbl_utilisateur.Motpass));
                        else cmd.Parameters.Add(getParameter(cmd, "@motpass", DbType.String, 1000, DBNull.Value));
                        if (varclstbl_utilisateur.Activation.HasValue) cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, varclstbl_utilisateur.Activation));
                        else cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, DBNull.Value));
                        cmd.Parameters.Add(getParameter(cmd, "@id_utilisateur", DbType.Int32, 4, varclstbl_utilisateur.Id_utilisateur));
                        cmd.Transaction = transaction;
                        i = cmd.ExecuteNonQuery();
                        ok = true;
                    }
                }

                if (!ok) conn.Close();

                if (ok)
                {
                    if (clsDoTraitement.etat_modification_user == 1 || clsDoTraitement.etat_modification_user == 2 || clsDoTraitement.etat_modification_user == 3)
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();

                        //On récupère le nom de l'utilisateur qui correspond au premier qui a été créé à la première fois
                        //et qui est équivalente au nom du schema de ce dernier

                        using (IDbCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = string.Format(@"SELECT tbl_utilisateur.schema_user FROM tbl_utilisateur WHERE tbl_utilisateur.id_utilisateur=" + varclstbl_utilisateur.Id_utilisateur);
                            cmd2.Transaction = transaction;

                            using (IDataReader dr = cmd2.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    varclstbl_utilisateur.Nomuser = dr["schema_user"].ToString();
                                }
                            }
                        }

                        //Si l'on à cocher la case à cocher d'activation de l'utilisateur on doit le donner accès à se connecter ou non
                        if ((bool)varclstbl_utilisateur.Activation)
                        {
                            using (IDbCommand cmd3 = conn.CreateCommand())
                            {
                                cmd3.CommandText = string.Format(@"grant connect to " + varclstbl_utilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                                cmd3.Transaction = transaction;
                                i = cmd3.ExecuteNonQuery();
                                transaction.Commit();
                                conn.Close();
                            }
                        }
                        else
                        {
                            using (IDbCommand cmd3 = conn.CreateCommand())
                            {
                                cmd3.CommandText = string.Format(@"revoke connect to " + varclstbl_utilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                                cmd3.Transaction = transaction;
                                i = cmd3.ExecuteNonQuery();
                                transaction.Commit();
                                conn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                if (transaction != null)
                {
                    transaction.Rollback();

                    string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                    ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec lors de la modification de l'utilisateur, " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                    throw new Exception(exc.Message);
                }

                conn.Close();
            }
            clsDoTraitement.etat_modification_user = -1;
            return i;
        }

        public int deleteClstbl_utilisateur(clstbl_utilisateur varclstbl_utilisateur)
        {
            int i = 0;
            IDbTransaction transaction = null;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                transaction = conn.BeginTransaction(IsolationLevel.Serializable);

                using (IDbCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = string.Format(@"SELECT tbl_utilisateur.schema_user FROM tbl_utilisateur WHERE tbl_utilisateur.id_utilisateur=" + varclstbl_utilisateur.Id_utilisateur);
                    cmd1.Transaction = transaction;
                    using (IDataReader dr = cmd1.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (!dr["schema_user"].ToString().Trim().Equals("")) varclstbl_utilisateur.Schema_user = dr["schema_user"].ToString();
                        }
                    }
                }

                //Avant de supprimer l'utilisateur dans la table, on supprime son schema qui correspond au premier nom d'utilisateur crée
                //puis on supprime son nom d'utilisateur et enfin on supprime son login
                using (IDbCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = string.Format("DROP SCHEMA " + varclstbl_utilisateur.Schema_user + @" 
                                                      DROP USER " + varclstbl_utilisateur.Schema_user + @"
                                                      DROP LOGIN " + varclstbl_utilisateur.Nomuser);
                    cmd2.Transaction = transaction;
                    i = cmd2.ExecuteNonQuery();
                }

                //Enfin on supprime l'utilisateur dans la table des utilisateurs
                using (IDbCommand cmd3 = conn.CreateCommand())
                {
                    cmd3.CommandText = string.Format("DELETE FROM tbl_utilisateur WHERE  1=1  AND id_utilisateur=@id_utilisateur ");
                    cmd3.Parameters.Add(getParameter(cmd3, "@id_utilisateur", DbType.Int32, 4, varclstbl_utilisateur.Id_utilisateur));
                    cmd3.Transaction = transaction;
                    i = cmd3.ExecuteNonQuery();
                    transaction.Commit();
                }

                conn.Close();
            }
            catch (Exception exc)
            {
                if (transaction != null)
                {
                    transaction.Rollback();

                    string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                    ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec lors de la suppression de l'utilisateur, " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                    throw new Exception(exc.Message);
                }
            }
            return i;
        }

        #endregion CLSTBL_UTILISATEUR 
        #region  CLSTBL_GROUPE
        public clstbl_groupe getClstbl_groupe(object intid)
        {
            clstbl_groupe varclstbl_groupe = new clstbl_groupe();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_groupe WHERE id_groupe={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id_groupe"].ToString().Trim().Equals("")) varclstbl_groupe.Id_groupe = int.Parse(dr["id_groupe"].ToString());
                            varclstbl_groupe.Designation = dr["designation"].ToString();
                            if (!dr["niveau"].ToString().Trim().Equals("")) varclstbl_groupe.Niveau = int.Parse(dr["niveau"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_groupe' avec la classe 'clstbl_groupe' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_groupe;
        }

        public DataTable getAllClstbl_groupe(string criteria)
        {
            DataTable dtclstbl_groupe = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_groupe  WHERE 1=1";
                    sql += "  OR   designation LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_groupe.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_groupe' avec la classe 'clstbl_groupe' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_groupe;
        }

        public DataTable getAllClstbl_groupe()
        {
            DataTable dtclstbl_groupe = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_groupe ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_groupe.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_groupe' avec la classe 'clstbl_groupe' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_groupe;
        }

        public int insertClstbl_groupe(clstbl_groupe varclstbl_groupe)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_groupe ( designation,niveau ) VALUES (@designation,@niveau  )");
                    if (varclstbl_groupe.Designation != null) cmd.Parameters.Add(getParameter(cmd, "@designation", DbType.String, 30, varclstbl_groupe.Designation));
                    else cmd.Parameters.Add(getParameter(cmd, "@designation", DbType.String, 30, DBNull.Value));
                    if (varclstbl_groupe.Niveau.HasValue) cmd.Parameters.Add(getParameter(cmd, "@niveau", DbType.Int32, 4, varclstbl_groupe.Niveau));
                    else cmd.Parameters.Add(getParameter(cmd, "@niveau", DbType.Int32, 4, DBNull.Value));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_groupe' avec la classe 'clstbl_groupe' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_groupe(DataRowView varclstbl_groupe)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_groupe  SET designation=@designation,niveau=@niveau  WHERE 1=1  AND id_groupe=@id_groupe ");
                    cmd.Parameters.Add(getParameter(cmd, "@designation", DbType.String, 30, varclstbl_groupe["designation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@niveau", DbType.Int32, 4, varclstbl_groupe["niveau"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id_groupe", DbType.Int32, 4, varclstbl_groupe["id_groupe"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_groupe' avec la classe 'clstbl_groupe' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_groupe(DataRowView varclstbl_groupe)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_groupe  WHERE  1=1  AND id_groupe=@id_groupe ");
                    cmd.Parameters.Add(getParameter(cmd, "@id_groupe", DbType.Int32, 4, varclstbl_groupe["id_groupe"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_groupe' avec la classe 'clstbl_groupe' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_GROUPE 
        #region  CLSTBL_FICHE_TAR
        public clstbl_fiche_tar getClstbl_fiche_tar(object intid)
        {
            clstbl_fiche_tar varclstbl_fiche_tar = new clstbl_fiche_tar();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_tar WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Id = int.Parse(dr["id"].ToString());
                            varclstbl_fiche_tar.Uuid = dr["uuid"].ToString();
                            varclstbl_fiche_tar.Deviceid = dr["deviceid"].ToString();
                            if (!dr["date"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Date = DateTime.Parse(dr["date"].ToString());
                            varclstbl_fiche_tar.Agent = dr["agent"].ToString();
                            varclstbl_fiche_tar.Saison = dr["saison"].ToString();
                            varclstbl_fiche_tar.Association = dr["association"].ToString();
                            varclstbl_fiche_tar.Association_autre = dr["association_autre"].ToString();
                            varclstbl_fiche_tar.Bailleur = dr["bailleur"].ToString();
                            varclstbl_fiche_tar.Bailleur_autre = dr["bailleur_autre"].ToString();
                            if (!dr["n_plantation"].ToString().Trim().Equals("")) varclstbl_fiche_tar.N_plantation = int.Parse(dr["n_plantation"].ToString());
                            varclstbl_fiche_tar.Deja_participe = dr["deja_participe"].ToString();
                            if (!dr["n_plantations"].ToString().Trim().Equals("")) varclstbl_fiche_tar.N_plantations = int.Parse(dr["n_plantations"].ToString());
                            varclstbl_fiche_tar.Nom = dr["nom"].ToString();
                            varclstbl_fiche_tar.Postnom = dr["postnom"].ToString();
                            varclstbl_fiche_tar.Prenom = dr["prenom"].ToString();
                            varclstbl_fiche_tar.Sexes = dr["sexes"].ToString();
                            varclstbl_fiche_tar.Nom_lieu_plantation = dr["nom_lieu_plantation"].ToString();
                            varclstbl_fiche_tar.Village = dr["village"].ToString();
                            varclstbl_fiche_tar.Localite = dr["localite"].ToString();
                            varclstbl_fiche_tar.Territoire = dr["territoire"].ToString();
                            varclstbl_fiche_tar.Chefferie = dr["chefferie"].ToString();
                            varclstbl_fiche_tar.Groupement = dr["groupement"].ToString();
                            varclstbl_fiche_tar.Type_id = dr["type_id"].ToString();
                            varclstbl_fiche_tar.Type_id_autre = dr["type_id_autre"].ToString();
                            varclstbl_fiche_tar.Nombre_id = dr["nombre_id"].ToString();
                            if (!dr["photo_id"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Photo_id = (Byte[])dr["photo_id"];
                            if (!dr["photo_planteur"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Photo_planteur = (Byte[])dr["photo_planteur"];
                            if (!dr["photo_terrain"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Photo_terrain = (Byte[])dr["photo_terrain"];
                            varclstbl_fiche_tar.Emplacement = dr["emplacement"].ToString();
                            varclstbl_fiche_tar.Essence_principale = dr["essence_principale"].ToString();
                            varclstbl_fiche_tar.Essence_principale_autre = dr["essence_principale_autre"].ToString();
                            if (!dr["superficie_totale"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Superficie_totale = double.Parse(dr["superficie_totale"].ToString());
                            varclstbl_fiche_tar.Objectifs_planteur = dr["objectifs_planteur"].ToString();
                            varclstbl_fiche_tar.Objectifs_planteur_autre = dr["objectifs_planteur_autre"].ToString();
                            varclstbl_fiche_tar.Utilisation_precedente = dr["utilisation_precedente"].ToString();
                            varclstbl_fiche_tar.Autre_precedente_preciser = dr["autre_precedente_preciser"].ToString();
                            if (!dr["utilisation_precedente_depuis"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Utilisation_precedente_depuis = DateTime.Parse(dr["utilisation_precedente_depuis"].ToString());
                            varclstbl_fiche_tar.Arbres_existants = dr["arbres_existants"].ToString();
                            if (!dr["ombre_arbres"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Ombre_arbres = int.Parse(dr["ombre_arbres"].ToString());
                            varclstbl_fiche_tar.Situation = dr["situation"].ToString();
                            varclstbl_fiche_tar.Pente = dr["pente"].ToString();
                            varclstbl_fiche_tar.Sol = dr["sol"].ToString();
                            varclstbl_fiche_tar.Eucalyptus = dr["eucalyptus"].ToString();
                            varclstbl_fiche_tar.Point_deau_a_proximite = dr["point_deau_a_proximite"].ToString();
                            if (!dr["env_point_deau_a_proximite"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Env_point_deau_a_proximite = int.Parse(dr["env_point_deau_a_proximite"].ToString());
                            varclstbl_fiche_tar.Chef_de_localite = dr["chef_de_localite"].ToString();
                            varclstbl_fiche_tar.Chef_nom = dr["chef_nom"].ToString();
                            varclstbl_fiche_tar.Chef_postnom = dr["chef_postnom"].ToString();
                            varclstbl_fiche_tar.Chef_prenom = dr["chef_prenom"].ToString();
                            varclstbl_fiche_tar.Autre = dr["autre"].ToString();
                            varclstbl_fiche_tar.Autre_fonction = dr["autre_fonction"].ToString();
                            varclstbl_fiche_tar.Autre_nom = dr["autre_nom"].ToString();
                            varclstbl_fiche_tar.Autre_postnom = dr["autre_postnom"].ToString();
                            varclstbl_fiche_tar.Autre_prenom = dr["autre_prenom"].ToString();
                            varclstbl_fiche_tar.Document_de_propriete = dr["document_de_propriete"].ToString();
                            varclstbl_fiche_tar.Preciser_document = dr["preciser_document"].ToString();
                            varclstbl_fiche_tar.Autre_document = dr["autre_document"].ToString();
                            if (!dr["photo_document_de_propriet"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Photo_document_de_propriet = (Byte[])dr["photo_document_de_propriet"];
                            varclstbl_fiche_tar.Observations = dr["observations"].ToString();
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_fiche_tar.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_fiche_tar' avec la classe 'clstbl_fiche_tar' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_fiche_tar;
        }

        public DataTable getAllClstbl_fiche_tar(string criteria)
        {
            DataTable dtclstbl_fiche_tar = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_fiche_tar  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   deviceid LIKE '%" + criteria + "%'";
                    sql += "  OR   agent LIKE '%" + criteria + "%'";
                    sql += "  OR   saison LIKE '%" + criteria + "%'";
                    sql += "  OR   association LIKE '%" + criteria + "%'";
                    sql += "  OR   association_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur LIKE '%" + criteria + "%'";
                    sql += "  OR   bailleur_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   deja_participe LIKE '%" + criteria + "%'";
                    sql += "  OR   nom LIKE '%" + criteria + "%'";
                    sql += "  OR   postnom LIKE '%" + criteria + "%'";
                    sql += "  OR   prenom LIKE '%" + criteria + "%'";
                    sql += "  OR   sexes LIKE '%" + criteria + "%'";
                    sql += "  OR   nom_lieu_plantation LIKE '%" + criteria + "%'";
                    sql += "  OR   village LIKE '%" + criteria + "%'";
                    sql += "  OR   localite LIKE '%" + criteria + "%'";
                    sql += "  OR   territoire LIKE '%" + criteria + "%'";
                    sql += "  OR   chefferie LIKE '%" + criteria + "%'";
                    sql += "  OR   groupement LIKE '%" + criteria + "%'";
                    sql += "  OR   type_id LIKE '%" + criteria + "%'";
                    sql += "  OR   type_id_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   nombre_id LIKE '%" + criteria + "%'";
                    sql += "  OR   emplacement LIKE '%" + criteria + "%'";
                    sql += "  OR   essence_principale LIKE '%" + criteria + "%'";
                    sql += "  OR   essence_principale_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   objectifs_planteur LIKE '%" + criteria + "%'";
                    sql += "  OR   objectifs_planteur_autre LIKE '%" + criteria + "%'";
                    sql += "  OR   utilisation_precedente LIKE '%" + criteria + "%'";
                    sql += "  OR   autre_precedente_preciser LIKE '%" + criteria + "%'";
                    sql += "  OR   arbres_existants LIKE '%" + criteria + "%'";
                    sql += "  OR   situation LIKE '%" + criteria + "%'";
                    sql += "  OR   pente LIKE '%" + criteria + "%'";
                    sql += "  OR   sol LIKE '%" + criteria + "%'";
                    sql += "  OR   eucalyptus LIKE '%" + criteria + "%'";
                    sql += "  OR   point_deau_a_proximite LIKE '%" + criteria + "%'";
                    sql += "  OR   chef_de_localite LIKE '%" + criteria + "%'";
                    sql += "  OR   chef_nom LIKE '%" + criteria + "%'";
                    sql += "  OR   chef_postnom LIKE '%" + criteria + "%'";
                    sql += "  OR   chef_prenom LIKE '%" + criteria + "%'";
                    sql += "  OR   autre LIKE '%" + criteria + "%'";
                    sql += "  OR   autre_fonction LIKE '%" + criteria + "%'";
                    sql += "  OR   autre_nom LIKE '%" + criteria + "%'";
                    sql += "  OR   autre_postnom LIKE '%" + criteria + "%'";
                    sql += "  OR   autre_prenom LIKE '%" + criteria + "%'";
                    sql += "  OR   document_de_propriete LIKE '%" + criteria + "%'";
                    sql += "  OR   preciser_document LIKE '%" + criteria + "%'";
                    sql += "  OR   autre_document LIKE '%" + criteria + "%'";
                    sql += "  OR   observations LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_tar.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_fiche_tar' avec la classe 'clstbl_fiche_tar' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_tar;
        }

        public DataTable getAllClstbl_fiche_tar()
        {
            DataTable dtclstbl_fiche_tar = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_fiche_tar ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_fiche_tar.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_fiche_tar' avec la classe 'clstbl_fiche_tar' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_fiche_tar;
        }

        public int insertClstbl_fiche_tar(clstbl_fiche_tar varclstbl_fiche_tar)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_fiche_tar ( uuid,deviceid,date,agent,saison,association,association_autre,bailleur,bailleur_autre,n_plantation,deja_participe,n_plantations,nom,postnom,prenom,sexes,nom_lieu_plantation,village,localite,territoire,chefferie,groupement,type_id,type_id_autre,nombre_id,photo_id,photo_planteur,photo_terrain,emplacement,essence_principale,essence_principale_autre,superficie_totale,objectifs_planteur,objectifs_planteur_autre,utilisation_precedente,autre_precedente_preciser,utilisation_precedente_depuis,arbres_existants,ombre_arbres,situation,pente,sol,eucalyptus,point_deau_a_proximite,env_point_deau_a_proximite,chef_de_localite,chef_nom,chef_postnom,chef_prenom,autre,autre_fonction,autre_nom,autre_postnom,autre_prenom,document_de_propriete,preciser_document,autre_document,photo_document_de_propriet,observations,synchronized_on ) VALUES (@uuid,@deviceid,@date,@agent,@saison,@association,@association_autre,@bailleur,@bailleur_autre,@n_plantation,@deja_participe,@n_plantations,@nom,@postnom,@prenom,@sexes,@nom_lieu_plantation,@village,@localite,@territoire,@chefferie,@groupement,@type_id,@type_id_autre,@nombre_id,@photo_id,@photo_planteur,@photo_terrain,@emplacement,@essence_principale,@essence_principale_autre,@superficie_totale,@objectifs_planteur,@objectifs_planteur_autre,@utilisation_precedente,@autre_precedente_preciser,@utilisation_precedente_depuis,@arbres_existants,@ombre_arbres,@situation,@pente,@sol,@eucalyptus,@point_deau_a_proximite,@env_point_deau_a_proximite,@chef_de_localite,@chef_nom,@chef_postnom,@chef_prenom,@autre,@autre_fonction,@autre_nom,@autre_postnom,@autre_prenom,@document_de_propriete,@preciser_document,@autre_document,@photo_document_de_propriet,@observations,@synchronized_on  )");
                    if (varclstbl_fiche_tar.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_tar.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_fiche_tar.Deviceid != null) cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_tar.Deviceid));
                    else cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_tar.Date));
                    if (varclstbl_fiche_tar.Agent != null) cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, varclstbl_fiche_tar.Agent));
                    else cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Saison != null) cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, varclstbl_fiche_tar.Saison));
                    else cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Association != null) cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, varclstbl_fiche_tar.Association));
                    else cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Association_autre != null) cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, varclstbl_fiche_tar.Association_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Bailleur != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, varclstbl_fiche_tar.Bailleur));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Bailleur_autre != null) cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, varclstbl_fiche_tar.Bailleur_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.N_plantation.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_plantation", DbType.Int32, 4, varclstbl_fiche_tar.N_plantation));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_plantation", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_tar.Deja_participe != null) cmd.Parameters.Add(getParameter(cmd, "@deja_participe", DbType.String, 20, varclstbl_fiche_tar.Deja_participe));
                    else cmd.Parameters.Add(getParameter(cmd, "@deja_participe", DbType.String, 20, DBNull.Value));
                    if (varclstbl_fiche_tar.N_plantations.HasValue) cmd.Parameters.Add(getParameter(cmd, "@n_plantations", DbType.Int32, 4, varclstbl_fiche_tar.N_plantations));
                    else cmd.Parameters.Add(getParameter(cmd, "@n_plantations", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_tar.Nom != null) cmd.Parameters.Add(getParameter(cmd, "@nom", DbType.String, 255, varclstbl_fiche_tar.Nom));
                    else cmd.Parameters.Add(getParameter(cmd, "@nom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Postnom != null) cmd.Parameters.Add(getParameter(cmd, "@postnom", DbType.String, 255, varclstbl_fiche_tar.Postnom));
                    else cmd.Parameters.Add(getParameter(cmd, "@postnom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Prenom != null) cmd.Parameters.Add(getParameter(cmd, "@prenom", DbType.String, 255, varclstbl_fiche_tar.Prenom));
                    else cmd.Parameters.Add(getParameter(cmd, "@prenom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Sexes != null) cmd.Parameters.Add(getParameter(cmd, "@sexes", DbType.String, 255, varclstbl_fiche_tar.Sexes));
                    else cmd.Parameters.Add(getParameter(cmd, "@sexes", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Nom_lieu_plantation != null) cmd.Parameters.Add(getParameter(cmd, "@nom_lieu_plantation", DbType.String, 255, varclstbl_fiche_tar.Nom_lieu_plantation));
                    else cmd.Parameters.Add(getParameter(cmd, "@nom_lieu_plantation", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Village != null) cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 255, varclstbl_fiche_tar.Village));
                    else cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Localite != null) cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 255, varclstbl_fiche_tar.Localite));
                    else cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Territoire != null) cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 255, varclstbl_fiche_tar.Territoire));
                    else cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Chefferie != null) cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 255, varclstbl_fiche_tar.Chefferie));
                    else cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Groupement != null) cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 255, varclstbl_fiche_tar.Groupement));
                    else cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Type_id != null) cmd.Parameters.Add(getParameter(cmd, "@type_id", DbType.String, 255, varclstbl_fiche_tar.Type_id));
                    else cmd.Parameters.Add(getParameter(cmd, "@type_id", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Type_id_autre != null) cmd.Parameters.Add(getParameter(cmd, "@type_id_autre", DbType.String, 255, varclstbl_fiche_tar.Type_id_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@type_id_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Nombre_id != null) cmd.Parameters.Add(getParameter(cmd, "@nombre_id", DbType.String, 255, varclstbl_fiche_tar.Nombre_id));
                    else cmd.Parameters.Add(getParameter(cmd, "@nombre_id", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Photo_id != null) cmd.Parameters.Add(getParameter(cmd, "@photo_id", DbType.Binary, Int32.MaxValue, varclstbl_fiche_tar.Photo_id));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo_id", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    if (varclstbl_fiche_tar.Photo_planteur != null) cmd.Parameters.Add(getParameter(cmd, "@photo_planteur", DbType.Binary, Int32.MaxValue, varclstbl_fiche_tar.Photo_planteur));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo_planteur", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    if (varclstbl_fiche_tar.Photo_terrain != null) cmd.Parameters.Add(getParameter(cmd, "@photo_terrain", DbType.Binary, Int32.MaxValue, varclstbl_fiche_tar.Photo_terrain));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo_terrain", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    if (varclstbl_fiche_tar.Emplacement != null) cmd.Parameters.Add(getParameter(cmd, "@emplacement", DbType.String, 255, varclstbl_fiche_tar.Emplacement));
                    else cmd.Parameters.Add(getParameter(cmd, "@emplacement", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Essence_principale != null) cmd.Parameters.Add(getParameter(cmd, "@essence_principale", DbType.String, 255, varclstbl_fiche_tar.Essence_principale));
                    else cmd.Parameters.Add(getParameter(cmd, "@essence_principale", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Essence_principale_autre != null) cmd.Parameters.Add(getParameter(cmd, "@essence_principale_autre", DbType.String, 255, varclstbl_fiche_tar.Essence_principale_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@essence_principale_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Superficie_totale.HasValue) cmd.Parameters.Add(getParameter(cmd, "@superficie_totale", DbType.Single, 4, varclstbl_fiche_tar.Superficie_totale));
                    else cmd.Parameters.Add(getParameter(cmd, "@superficie_totale", DbType.Single, 4, DBNull.Value));
                    if (varclstbl_fiche_tar.Objectifs_planteur != null) cmd.Parameters.Add(getParameter(cmd, "@objectifs_planteur", DbType.String, 255, varclstbl_fiche_tar.Objectifs_planteur));
                    else cmd.Parameters.Add(getParameter(cmd, "@objectifs_planteur", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Objectifs_planteur_autre != null) cmd.Parameters.Add(getParameter(cmd, "@objectifs_planteur_autre", DbType.String, 255, varclstbl_fiche_tar.Objectifs_planteur_autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@objectifs_planteur_autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Utilisation_precedente != null) cmd.Parameters.Add(getParameter(cmd, "@utilisation_precedente", DbType.String, 255, varclstbl_fiche_tar.Utilisation_precedente));
                    else cmd.Parameters.Add(getParameter(cmd, "@utilisation_precedente", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Autre_precedente_preciser != null) cmd.Parameters.Add(getParameter(cmd, "@autre_precedente_preciser", DbType.String, 255, varclstbl_fiche_tar.Autre_precedente_preciser));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_precedente_preciser", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Utilisation_precedente_depuis.HasValue) cmd.Parameters.Add(getParameter(cmd, "@utilisation_precedente_depuis", DbType.DateTime, 8, varclstbl_fiche_tar.Utilisation_precedente_depuis));
                    else cmd.Parameters.Add(getParameter(cmd, "@utilisation_precedente_depuis", DbType.DateTime, 8, DBNull.Value));
                    if (varclstbl_fiche_tar.Arbres_existants != null) cmd.Parameters.Add(getParameter(cmd, "@arbres_existants", DbType.String, 255, varclstbl_fiche_tar.Arbres_existants));
                    else cmd.Parameters.Add(getParameter(cmd, "@arbres_existants", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Ombre_arbres.HasValue) cmd.Parameters.Add(getParameter(cmd, "@ombre_arbres", DbType.Int32, 4, varclstbl_fiche_tar.Ombre_arbres));
                    else cmd.Parameters.Add(getParameter(cmd, "@ombre_arbres", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_tar.Situation != null) cmd.Parameters.Add(getParameter(cmd, "@situation", DbType.String, 255, varclstbl_fiche_tar.Situation));
                    else cmd.Parameters.Add(getParameter(cmd, "@situation", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Pente != null) cmd.Parameters.Add(getParameter(cmd, "@pente", DbType.String, 255, varclstbl_fiche_tar.Pente));
                    else cmd.Parameters.Add(getParameter(cmd, "@pente", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Sol != null) cmd.Parameters.Add(getParameter(cmd, "@sol", DbType.String, 255, varclstbl_fiche_tar.Sol));
                    else cmd.Parameters.Add(getParameter(cmd, "@sol", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Eucalyptus != null) cmd.Parameters.Add(getParameter(cmd, "@eucalyptus", DbType.String, 255, varclstbl_fiche_tar.Eucalyptus));
                    else cmd.Parameters.Add(getParameter(cmd, "@eucalyptus", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Point_deau_a_proximite != null) cmd.Parameters.Add(getParameter(cmd, "@point_deau_a_proximite", DbType.String, 255, varclstbl_fiche_tar.Point_deau_a_proximite));
                    else cmd.Parameters.Add(getParameter(cmd, "@point_deau_a_proximite", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Env_point_deau_a_proximite.HasValue) cmd.Parameters.Add(getParameter(cmd, "@env_point_deau_a_proximite", DbType.Int32, 4, varclstbl_fiche_tar.Env_point_deau_a_proximite));
                    else cmd.Parameters.Add(getParameter(cmd, "@env_point_deau_a_proximite", DbType.Int32, 4, DBNull.Value));
                    if (varclstbl_fiche_tar.Chef_de_localite != null) cmd.Parameters.Add(getParameter(cmd, "@chef_de_localite", DbType.String, 255, varclstbl_fiche_tar.Chef_de_localite));
                    else cmd.Parameters.Add(getParameter(cmd, "@chef_de_localite", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Chef_nom != null) cmd.Parameters.Add(getParameter(cmd, "@chef_nom", DbType.String, 255, varclstbl_fiche_tar.Chef_nom));
                    else cmd.Parameters.Add(getParameter(cmd, "@chef_nom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Chef_postnom != null) cmd.Parameters.Add(getParameter(cmd, "@chef_postnom", DbType.String, 255, varclstbl_fiche_tar.Chef_postnom));
                    else cmd.Parameters.Add(getParameter(cmd, "@chef_postnom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Chef_prenom != null) cmd.Parameters.Add(getParameter(cmd, "@chef_prenom", DbType.String, 255, varclstbl_fiche_tar.Chef_prenom));
                    else cmd.Parameters.Add(getParameter(cmd, "@chef_prenom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Autre != null) cmd.Parameters.Add(getParameter(cmd, "@autre", DbType.String, 255, varclstbl_fiche_tar.Autre));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Autre_fonction != null) cmd.Parameters.Add(getParameter(cmd, "@autre_fonction", DbType.String, 255, varclstbl_fiche_tar.Autre_fonction));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_fonction", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Autre_nom != null) cmd.Parameters.Add(getParameter(cmd, "@autre_nom", DbType.String, 255, varclstbl_fiche_tar.Autre_nom));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_nom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Autre_postnom != null) cmd.Parameters.Add(getParameter(cmd, "@autre_postnom", DbType.String, 255, varclstbl_fiche_tar.Autre_postnom));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_postnom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Autre_prenom != null) cmd.Parameters.Add(getParameter(cmd, "@autre_prenom", DbType.String, 255, varclstbl_fiche_tar.Autre_prenom));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_prenom", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Document_de_propriete != null) cmd.Parameters.Add(getParameter(cmd, "@document_de_propriete", DbType.String, 255, varclstbl_fiche_tar.Document_de_propriete));
                    else cmd.Parameters.Add(getParameter(cmd, "@document_de_propriete", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Preciser_document != null) cmd.Parameters.Add(getParameter(cmd, "@preciser_document", DbType.String, 255, varclstbl_fiche_tar.Preciser_document));
                    else cmd.Parameters.Add(getParameter(cmd, "@preciser_document", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Autre_document != null) cmd.Parameters.Add(getParameter(cmd, "@autre_document", DbType.String, 255, varclstbl_fiche_tar.Autre_document));
                    else cmd.Parameters.Add(getParameter(cmd, "@autre_document", DbType.String, 255, DBNull.Value));
                    if (varclstbl_fiche_tar.Photo_document_de_propriet != null) cmd.Parameters.Add(getParameter(cmd, "@photo_document_de_propriet", DbType.Binary, Int32.MaxValue, varclstbl_fiche_tar.Photo_document_de_propriet));
                    else cmd.Parameters.Add(getParameter(cmd, "@photo_document_de_propriet", DbType.Binary, Int32.MaxValue, DBNull.Value));
                    if (varclstbl_fiche_tar.Observations != null) cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, varclstbl_fiche_tar.Observations));
                    else cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_tar.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_fiche_tar' avec la classe 'clstbl_fiche_tar' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_fiche_tar(DataRowView varclstbl_fiche_tar)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_fiche_tar  SET uuid=@uuid,deviceid=@deviceid,date=@date,agent=@agent,saison=@saison,association=@association,association_autre=@association_autre,bailleur=@bailleur,bailleur_autre=@bailleur_autre,n_plantation=@n_plantation,deja_participe=@deja_participe,n_plantations=@n_plantations,nom=@nom,postnom=@postnom,prenom=@prenom,sexes=@sexes,nom_lieu_plantation=@nom_lieu_plantation,village=@village,localite=@localite,territoire=@territoire,chefferie=@chefferie,groupement=@groupement,type_id=@type_id,type_id_autre=@type_id_autre,nombre_id=@nombre_id,photo_id=@photo_id,photo_planteur=@photo_planteur,photo_terrain=@photo_terrain,emplacement=@emplacement,essence_principale=@essence_principale,essence_principale_autre=@essence_principale_autre,superficie_totale=@superficie_totale,objectifs_planteur=@objectifs_planteur,objectifs_planteur_autre=@objectifs_planteur_autre,utilisation_precedente=@utilisation_precedente,autre_precedente_preciser=@autre_precedente_preciser,utilisation_precedente_depuis=@utilisation_precedente_depuis,arbres_existants=@arbres_existants,ombre_arbres=@ombre_arbres,situation=@situation,pente=@pente,sol=@sol,eucalyptus=@eucalyptus,point_deau_a_proximite=@point_deau_a_proximite,env_point_deau_a_proximite=@env_point_deau_a_proximite,chef_de_localite=@chef_de_localite,chef_nom=@chef_nom,chef_postnom=@chef_postnom,chef_prenom=@chef_prenom,autre=@autre,autre_fonction=@autre_fonction,autre_nom=@autre_nom,autre_postnom=@autre_postnom,autre_prenom=@autre_prenom,document_de_propriete=@document_de_propriete,preciser_document=@preciser_document,autre_document=@autre_document,photo_document_de_propriet=@photo_document_de_propriet,observations=@observations,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_fiche_tar["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_fiche_tar["deviceid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@date", DbType.DateTime, 8, varclstbl_fiche_tar["date"]));
                    cmd.Parameters.Add(getParameter(cmd, "@agent", DbType.String, 255, varclstbl_fiche_tar["agent"]));
                    cmd.Parameters.Add(getParameter(cmd, "@saison", DbType.String, 255, varclstbl_fiche_tar["saison"]));
                    cmd.Parameters.Add(getParameter(cmd, "@association", DbType.String, 255, varclstbl_fiche_tar["association"]));
                    cmd.Parameters.Add(getParameter(cmd, "@association_autre", DbType.String, 255, varclstbl_fiche_tar["association_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur", DbType.String, 255, varclstbl_fiche_tar["bailleur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@bailleur_autre", DbType.String, 255, varclstbl_fiche_tar["bailleur_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_plantation", DbType.Int32, 4, varclstbl_fiche_tar["n_plantation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@deja_participe", DbType.String, 20, varclstbl_fiche_tar["deja_participe"]));
                    cmd.Parameters.Add(getParameter(cmd, "@n_plantations", DbType.Int32, 4, varclstbl_fiche_tar["n_plantations"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nom", DbType.String, 255, varclstbl_fiche_tar["nom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@postnom", DbType.String, 255, varclstbl_fiche_tar["postnom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@prenom", DbType.String, 255, varclstbl_fiche_tar["prenom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@sexes", DbType.String, 255, varclstbl_fiche_tar["sexes"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nom_lieu_plantation", DbType.String, 255, varclstbl_fiche_tar["nom_lieu_plantation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@village", DbType.String, 255, varclstbl_fiche_tar["village"]));
                    cmd.Parameters.Add(getParameter(cmd, "@localite", DbType.String, 255, varclstbl_fiche_tar["localite"]));
                    cmd.Parameters.Add(getParameter(cmd, "@territoire", DbType.String, 255, varclstbl_fiche_tar["territoire"]));
                    cmd.Parameters.Add(getParameter(cmd, "@chefferie", DbType.String, 255, varclstbl_fiche_tar["chefferie"]));
                    cmd.Parameters.Add(getParameter(cmd, "@groupement", DbType.String, 255, varclstbl_fiche_tar["groupement"]));
                    cmd.Parameters.Add(getParameter(cmd, "@type_id", DbType.String, 255, varclstbl_fiche_tar["type_id"]));
                    cmd.Parameters.Add(getParameter(cmd, "@type_id_autre", DbType.String, 255, varclstbl_fiche_tar["type_id_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@nombre_id", DbType.String, 255, varclstbl_fiche_tar["nombre_id"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo_id", DbType.Binary, Int32.MaxValue, varclstbl_fiche_tar["photo_id"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo_planteur", DbType.Binary, Int32.MaxValue, varclstbl_fiche_tar["photo_planteur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo_terrain", DbType.Binary, Int32.MaxValue, varclstbl_fiche_tar["photo_terrain"]));
                    cmd.Parameters.Add(getParameter(cmd, "@emplacement", DbType.String, 255, varclstbl_fiche_tar["emplacement"]));
                    cmd.Parameters.Add(getParameter(cmd, "@essence_principale", DbType.String, 255, varclstbl_fiche_tar["essence_principale"]));
                    cmd.Parameters.Add(getParameter(cmd, "@essence_principale_autre", DbType.String, 255, varclstbl_fiche_tar["essence_principale_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@superficie_totale", DbType.Single, 4, varclstbl_fiche_tar["superficie_totale"]));
                    cmd.Parameters.Add(getParameter(cmd, "@objectifs_planteur", DbType.String, 255, varclstbl_fiche_tar["objectifs_planteur"]));
                    cmd.Parameters.Add(getParameter(cmd, "@objectifs_planteur_autre", DbType.String, 255, varclstbl_fiche_tar["objectifs_planteur_autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@utilisation_precedente", DbType.String, 255, varclstbl_fiche_tar["utilisation_precedente"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_precedente_preciser", DbType.String, 255, varclstbl_fiche_tar["autre_precedente_preciser"]));
                    cmd.Parameters.Add(getParameter(cmd, "@utilisation_precedente_depuis", DbType.DateTime, 8, varclstbl_fiche_tar["utilisation_precedente_depuis"]));
                    cmd.Parameters.Add(getParameter(cmd, "@arbres_existants", DbType.String, 255, varclstbl_fiche_tar["arbres_existants"]));
                    cmd.Parameters.Add(getParameter(cmd, "@ombre_arbres", DbType.Int32, 4, varclstbl_fiche_tar["ombre_arbres"]));
                    cmd.Parameters.Add(getParameter(cmd, "@situation", DbType.String, 255, varclstbl_fiche_tar["situation"]));
                    cmd.Parameters.Add(getParameter(cmd, "@pente", DbType.String, 255, varclstbl_fiche_tar["pente"]));
                    cmd.Parameters.Add(getParameter(cmd, "@sol", DbType.String, 255, varclstbl_fiche_tar["sol"]));
                    cmd.Parameters.Add(getParameter(cmd, "@eucalyptus", DbType.String, 255, varclstbl_fiche_tar["eucalyptus"]));
                    cmd.Parameters.Add(getParameter(cmd, "@point_deau_a_proximite", DbType.String, 255, varclstbl_fiche_tar["point_deau_a_proximite"]));
                    cmd.Parameters.Add(getParameter(cmd, "@env_point_deau_a_proximite", DbType.Int32, 4, varclstbl_fiche_tar["env_point_deau_a_proximite"]));
                    cmd.Parameters.Add(getParameter(cmd, "@chef_de_localite", DbType.String, 255, varclstbl_fiche_tar["chef_de_localite"]));
                    cmd.Parameters.Add(getParameter(cmd, "@chef_nom", DbType.String, 255, varclstbl_fiche_tar["chef_nom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@chef_postnom", DbType.String, 255, varclstbl_fiche_tar["chef_postnom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@chef_prenom", DbType.String, 255, varclstbl_fiche_tar["chef_prenom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre", DbType.String, 255, varclstbl_fiche_tar["autre"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_fonction", DbType.String, 255, varclstbl_fiche_tar["autre_fonction"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_nom", DbType.String, 255, varclstbl_fiche_tar["autre_nom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_postnom", DbType.String, 255, varclstbl_fiche_tar["autre_postnom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_prenom", DbType.String, 255, varclstbl_fiche_tar["autre_prenom"]));
                    cmd.Parameters.Add(getParameter(cmd, "@document_de_propriete", DbType.String, 255, varclstbl_fiche_tar["document_de_propriete"]));
                    cmd.Parameters.Add(getParameter(cmd, "@preciser_document", DbType.String, 255, varclstbl_fiche_tar["preciser_document"]));
                    cmd.Parameters.Add(getParameter(cmd, "@autre_document", DbType.String, 255, varclstbl_fiche_tar["autre_document"]));
                    cmd.Parameters.Add(getParameter(cmd, "@photo_document_de_propriet", DbType.Binary, Int32.MaxValue, varclstbl_fiche_tar["photo_document_de_propriet"]));
                    cmd.Parameters.Add(getParameter(cmd, "@observations", DbType.String, 255, varclstbl_fiche_tar["observations"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_fiche_tar["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_fiche_tar["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_fiche_tar' avec la classe 'clstbl_fiche_tar' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_fiche_tar(DataRowView varclstbl_fiche_tar)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_fiche_tar  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_fiche_tar["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_fiche_tar' avec la classe 'clstbl_fiche_tar' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion CLSTBL_FICHE_TAR 
        #region  CLSTBL_GEOPOINT
        public clstbl_geopoint getClstbl_geopoint(object intid)
        {
            clstbl_geopoint varclstbl_geopoint = new clstbl_geopoint();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_geopoint WHERE id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            if (!dr["id"].ToString().Trim().Equals("")) varclstbl_geopoint.Id = int.Parse(dr["id"].ToString());
                            varclstbl_geopoint.Uuid = dr["uuid"].ToString();
                            varclstbl_geopoint.Deviceid = dr["deviceid"].ToString();
                            varclstbl_geopoint.Latitude = dr["latitude"].ToString();
                            varclstbl_geopoint.Longitude = dr["longitude"].ToString();
                            varclstbl_geopoint.Altitude = dr["altitude"].ToString();
                            varclstbl_geopoint.Epe = dr["EPE"].ToString();
                            varclstbl_geopoint.Geo_type = dr["geo_type"].ToString();
                            if (!dr["synchronized_on"].ToString().Trim().Equals("")) varclstbl_geopoint.Synchronized_on = DateTime.Parse(dr["synchronized_on"].ToString());
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_geopoint' avec la classe 'clstbl_geopoint' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_geopoint;
        }

        public DataTable getAllClstbl_geopoint(string criteria)
        {
            DataTable dtclstbl_geopoint = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT *  FROM tbl_geopoint  WHERE 1=1";
                    sql += "  OR   uuid LIKE '%" + criteria + "%'";
                    sql += "  OR   deviceid LIKE '%" + criteria + "%'";
                    sql += "  OR   latitude LIKE '%" + criteria + "%'";
                    sql += "  OR   longitude LIKE '%" + criteria + "%'";
                    sql += "  OR   altitude LIKE '%" + criteria + "%'";
                    sql += "  OR   EPE LIKE '%" + criteria + "%'";
                    sql += "  OR   geo_type LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_geopoint.Load(dr);
                    }
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection des tous les enregistrements de la table : 'tbl_geopoint' avec la classe 'clstbl_geopoint' suivant un critère : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_geopoint;
        }

        public DataTable getAllClstbl_geopoint()
        {
            DataTable dtclstbl_geopoint = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT *  FROM tbl_geopoint ");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        dtclstbl_geopoint.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_geopoint' avec la classe 'clstbl_geopoint' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return dtclstbl_geopoint;
        }

        public int insertClstbl_geopoint(clstbl_geopoint varclstbl_geopoint)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("INSERT INTO tbl_geopoint ( uuid,deviceid,latitude,longitude,altitude,EPE,geo_type,synchronized_on ) VALUES (@uuid,@deviceid,@latitude,@longitude,@altitude,@EPE,@geo_type,@synchronized_on  )");
                    if (varclstbl_geopoint.Uuid != null) cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_geopoint.Uuid));
                    else cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_geopoint.Deviceid != null) cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_geopoint.Deviceid));
                    else cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, DBNull.Value));
                    if (varclstbl_geopoint.Latitude != null) cmd.Parameters.Add(getParameter(cmd, "@latitude", DbType.String, 50, varclstbl_geopoint.Latitude));
                    else cmd.Parameters.Add(getParameter(cmd, "@latitude", DbType.String, 50, DBNull.Value));
                    if (varclstbl_geopoint.Longitude != null) cmd.Parameters.Add(getParameter(cmd, "@longitude", DbType.String, 50, varclstbl_geopoint.Longitude));
                    else cmd.Parameters.Add(getParameter(cmd, "@longitude", DbType.String, 50, DBNull.Value));
                    if (varclstbl_geopoint.Altitude != null) cmd.Parameters.Add(getParameter(cmd, "@altitude", DbType.String, 50, varclstbl_geopoint.Altitude));
                    else cmd.Parameters.Add(getParameter(cmd, "@altitude", DbType.String, 50, DBNull.Value));
                    if (varclstbl_geopoint.Epe != null) cmd.Parameters.Add(getParameter(cmd, "@EPE", DbType.String, 50, varclstbl_geopoint.Epe));
                    else cmd.Parameters.Add(getParameter(cmd, "@EPE", DbType.String, 50, DBNull.Value));
                    if (varclstbl_geopoint.Geo_type != null) cmd.Parameters.Add(getParameter(cmd, "@geo_type", DbType.String, 50, varclstbl_geopoint.Geo_type));
                    else cmd.Parameters.Add(getParameter(cmd, "@geo_type", DbType.String, 50, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_geopoint.Synchronized_on));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_geopoint' avec la classe 'clstbl_geopoint' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int updateClstbl_geopoint(DataRowView varclstbl_geopoint)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_geopoint  SET uuid=@uuid,deviceid=@deviceid,latitude=@latitude,longitude=@longitude,altitude=@altitude,EPE=@EPE,geo_type=@geo_type,synchronized_on=@synchronized_on  WHERE 1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@uuid", DbType.String, 100, varclstbl_geopoint["uuid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@deviceid", DbType.String, 100, varclstbl_geopoint["deviceid"]));
                    cmd.Parameters.Add(getParameter(cmd, "@latitude", DbType.String, 50, varclstbl_geopoint["latitude"]));
                    cmd.Parameters.Add(getParameter(cmd, "@longitude", DbType.String, 50, varclstbl_geopoint["longitude"]));
                    cmd.Parameters.Add(getParameter(cmd, "@altitude", DbType.String, 50, varclstbl_geopoint["altitude"]));
                    cmd.Parameters.Add(getParameter(cmd, "@EPE", DbType.String, 50, varclstbl_geopoint["EPE"]));
                    cmd.Parameters.Add(getParameter(cmd, "@geo_type", DbType.String, 50, varclstbl_geopoint["geo_type"]));
                    cmd.Parameters.Add(getParameter(cmd, "@synchronized_on", DbType.DateTime, 8, varclstbl_geopoint["synchronized_on"]));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_geopoint["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Update enregistrement de la table : 'tbl_geopoint' avec la classe 'clstbl_geopoint' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClstbl_geopoint(DataRowView varclstbl_geopoint)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("DELETE FROM tbl_geopoint  WHERE  1=1  AND id=@id ");
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclstbl_geopoint["id"]));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Suppression enregistrement de la table : 'tbl_geopoint' avec la classe 'clstbl_geopoint' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }
        #endregion CLSTBL_GEOPOINT 


        #region BEGIN ADD UTILISATEUR
        /// <summary>
        /// Permet de verifier les paramètres de connexion de l'utilisateur, donc username et password
        /// et retourne un tableau contenant successivement l'Id de l'Agent, son nom et ses droits qui determinent son niveau
        /// </summary>
        /// <param name="String nom d'utilisateur"></param>
        /// <param name="String mot de passe"></param>
        /// <returns>ArrayList</returns>
        public ArrayList verifieLoginUser(string username, string password)
        {
            ArrayList lstValue = new ArrayList();
            bool okActivateUser = false;

            //Echec de la connexion en superAdministrateur alors on peut se connecte en Administrateur 
            //ou en invite (User)
            if (username.ToLower().Equals("sa"))
            {
                throw new Exception("L'utilisateur 'SA' a été désactivé dans cette application pour raisons de sécurité, veuillez contacter votre Administrateur");
            }
            else if (username.Equals("wwfadmin"))//Super Administrateur par defaut
            {
                //Super utilisateur de la BD
                lstValue.Add("0");
                lstValue.Add("Superutilisateur de la BD");
                lstValue.Add("Administrateur");
                lstValue.Add(true);
            }
            else
            {
                try
                {
                    if (conn.State != ConnectionState.Open) conn.Open();

                    //On commence par recuperer le password chiffre dans la BD pour la comparer avec celui que 
                    //le user a saisi
                    string strBDdecipherPasswor = "", strBDCipher = "";
                    bool ok = false;

                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("SELECT motpass from tbl_utilisateur WHERE nomuser='{0}'", username);
                        IDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            strBDCipher = (dr["motpass"]).ToString();
                            strBDdecipherPasswor = ImplementChiffer.Instance.Decipher((dr["motpass"]).ToString(), "rootWWF");
                            ok = true;
                        }
                        dr.Close();
                        cmd.Dispose();
                    }


                    if (ok && strBDdecipherPasswor.CompareTo(password) == 0)
                    {
                        using (IDbCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = string.Format(@"SELECT tbl_agent.id_agent AS id,tbl_agent.agent AS nom,tbl_utilisateur.activation AS activation,tbl_utilisateur.nomuser,tbl_utilisateur.droits AS droits,tbl_utilisateur.motpass FROM tbl_agent 
                            LEFT OUTER JOIN tbl_utilisateur ON tbl_agent.id_agent=tbl_utilisateur.id_agentuser WHERE tbl_utilisateur.nomuser='{0}' AND tbl_utilisateur.motpass='{1}'", username, strBDCipher);

                            using (IDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    lstValue.Add(dr["id"].ToString());
                                    lstValue.Add(Convert.ToString(dr["nom"]));
                                    lstValue.Add(Convert.ToString(dr["droits"]));//Tous les droit de l'utilisateur
                                                                                 /*Ces droits sont:0->Administrateur : Administrateur de la BD avec tous les droits
                                                                                                   1->Admin          : Adminikstrateur local de l'application avec certaines restrictions
                                                                                                     comme la non possibilite de suppression de users ou des enregistrements
                                                                                                   2->User           : Utilisateur simple avec beaucoup des restrictions
                                                                                 */

                                    okActivateUser = Convert.ToBoolean(dr["activation"]);

                                    //Recuperation du nombre des droits de l'utilisateur
                                    int nbr = 0;

                                    if (!string.IsNullOrEmpty(lstValue[2].ToString()))
                                    {
                                        string[] nbdroit = lstValue[2].ToString().Split(',');
                                        foreach (string str in nbdroit)
                                            nbr++;
                                        clsDoTraitement.nombre_droit = nbr;//Nombre total des droists de l'utilisateur
                                    }

                                    if (okActivateUser)
                                    {
                                        if (clsDoTraitement.nombre_droit == 0)
                                        {
                                            lstValue.Add(false);
                                            throw new Exception("Cet utilisateur est activé mais n'a encore aucun droit");
                                        }
                                        else
                                        {
                                            //Utilisateur valide
                                            lstValue.Add(true);
                                        }
                                    }
                                    else
                                    {
                                        lstValue.Add(false);

                                        if (clsDoTraitement.nombre_droit == 0)
                                        {
                                            throw new Exception("Cet utilisateur est désactivé et n'a aucun droit");
                                        }
                                        else
                                        {
                                            throw new Exception("Cet utilisateur est désactivé mais a des droits d'accès");
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        lstValue.Add(false);
                        throw new Exception("Nom d'utilisateur ou mot de passe invalide, contacter votre administrateur");
                    }
                    conn.Close();
                }
                catch (Exception exc)
                {
                    conn.Close();
                    string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                    ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Vérification des paramètres de connexion de l'utilisateur : username et password : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                    throw new Exception(exc.Message);
                }
            }
            return lstValue;
        }
        public DataTable getAllClstbl_utilisateur_Agent()
        {
            DataTable lstclstbl_utilisateur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT tbl_utilisateur.id_utilisateur,tbl_utilisateur.id_agentuser,tbl_utilisateur.nomuser,tbl_utilisateur.motpass,tbl_utilisateur.schema_user,tbl_utilisateur.droits,tbl_utilisateur.activation,tbl_agent.id_agent, tbl_agent.agent AS nom FROM tbl_utilisateur 
                    INNER JOIN tbl_agent ON tbl_agent.id_agent = tbl_utilisateur.id_agentuser ORDER BY tbl_utilisateur.nomuser ASC");

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        lstclstbl_utilisateur.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return lstclstbl_utilisateur;
        }

        public DataTable getAllClstbl_utilisateur_Agent2(int intid)
        {
            DataTable lstclstbl_utilisateur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT tbl_utilisateur.id_utilisateur,tbl_utilisateur.id_agentuser,tbl_utilisateur.nomuser,tbl_utilisateur.motpass,tbl_utilisateur.schema_user,
                    tbl_utilisateur.droits,tbl_utilisateur.activation,tbl_agent.id_agent, tbl_agent.agent AS nom FROM tbl_utilisateur 
                    INNER JOIN tbl_agent ON tbl_agent.id_agent = tbl_utilisateur.id_agentuser WHERE tbl_utilisateur.id_utilisateur=" + intid);

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        lstclstbl_utilisateur.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection de tous les enregistrements de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return lstclstbl_utilisateur;
        }

        public clstbl_utilisateur getClstbl_utilisateurUser(string nom_user)
        {
            clstbl_utilisateur varclstbl_utilisateur = new clstbl_utilisateur();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT tbl_utilisateur.id_utilisateur AS idUser,tbl_utilisateur.id_agentuser,tbl_utilisateur.nomuser,tbl_utilisateur.motpass,
                    tbl_utilisateur.schema_user,tbl_utilisateur.droits,tbl_utilisateur.activation,tbl_agent.id_agent, tbl_agent.agent AS nom FROM tbl_utilisateur 
                    INNER JOIN tbl_agent ON tbl_agent.id_agent = tbl_utilisateur.id_agentuser WHERE tbl_utilisateur.nomuser='{0}'", nom_user);

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            //Utilisateur
                            if (!dr["idUser"].ToString().Trim().Equals("")) varclstbl_utilisateur.Id_utilisateur = int.Parse(dr["idUser"].ToString());
                            varclstbl_utilisateur.Id_agentuser = dr["id_agentuser"].ToString();
                            varclstbl_utilisateur.Nomuser = dr["nomuser"].ToString();
                            varclstbl_utilisateur.Motpass = ImplementChiffer.Instance.Decipher(dr["motpass"].ToString(), "rootWWF");
                            varclstbl_utilisateur.Schema_user = dr["schema_user"].ToString();
                            varclstbl_utilisateur.Droits = dr["droits"].ToString();
                            if (!dr["activation"].ToString().Trim().Equals("")) varclstbl_utilisateur.Activation = bool.Parse(dr["activation"].ToString());

                            //Agent
                            varclstbl_utilisateur.Id_agent = dr["id_agent"].ToString();
                            varclstbl_utilisateur.Agent = dr["nom"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Vérification des paramètres de connexion de l'utilisateur : username et password : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return varclstbl_utilisateur;
        }

        public int updateClsutilisateur_droit(int id_user, string droits)
        {
            int i = 0;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                //Modification de l'utilisateur
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("UPDATE tbl_utilisateur  SET droits=@droits  WHERE 1=1  AND id_utilisateur=@id_utilisateur ");
                    if (droits != null) cmd.Parameters.Add(getParameter(cmd, "@droits", DbType.String, 100, droits));
                    else cmd.Parameters.Add(getParameter(cmd, "@droits", DbType.String, 100, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@id_utilisateur", DbType.Int32, 4, id_user));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Insertion enregistrement de la table : 'tbl_utilisateur' avec la classe 'clstbl_utilisateur' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return i;
        }

        #endregion END ADD UTILISATEUR
        #region GESTION DES DROITS D'ACCES SUR LES TABLES POUR LES UTILISATEUR
        public string[] getLogin_SchemaUser(int id_user)
        {
            string[] schema = new string[2];

            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT nomuser,schema_user  FROM tbl_utilisateur WHERE id_utilisateur=" + id_user);

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            schema[0] = dr["nomuser"].ToString();
                            schema[1] = dr["schema_user"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_fiche_menage' avec la classe 'clstbl_fiche_menage' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return schema;
        }

        public List<int> getDroitsUser(int id_user)
        {
            List<int> droits = new List<int>();

            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();

                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format("SELECT droits FROM tbl_utilisateur WHERE id_utilisateur=" + id_user);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            string[] temp = dr["droits"].ToString().Split(',');
                            int taille = temp.Length;

                            foreach (string str in temp)
                            {
                                if (str.ToString().Equals("Administrateur")) droits.Add(0);
                                else if (str.Equals("Admin")) droits.Add(1);
                                else if (str.Equals("User")) droits.Add(2);
                            }
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Sélection d'un enregistrement de la table : 'tbl_fiche_menage' avec la classe 'clstbl_fiche_menage' : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
            return droits;
        }

        public void grantPermission(List<int> permission, string nom_login, string nom_utilisateur)
        {
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                //On tourne dans la boucle qui tournera tant qu'il ya encore un groupe de permission à accordé
                foreach (int droit in permission)
                {
                    if (droit == 0)
                    {
                        #region Droit pour administrateur (Ce dernier a tous les droits sur le systeme)
                        string requete = @"exec sp_addsrvrolemember '" + nom_login + @"','sysadmin' 
                        exec sp_addsrvrolemember '" + nom_login + @"','securityadmin' 
                        exec sp_addsrvrolemember '" + nom_login + @"','dbcreator' 
                        exec sp_addrolemember 'db_owner','" + nom_utilisateur + @"'
                        exec sp_addrolemember 'db_ddladmin','" + nom_utilisateur + @"'
                        exec sp_addrolemember 'db_accessadmin','" + nom_utilisateur + @"'";

                        using (IDbCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = string.Format(requete);
                            cmd.ExecuteNonQuery();
                        }
                        #endregion
                    }
                    else if (droit == 1)
                    {
                        #region Droit pour Admin (Ce dernier est aussi administrateur mais avec certaines limites comme suppressionm etc)
                        string requete = @"grant select,insert,update on tbl_fiche_ident_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_grp_c_fiche_ident_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_germoir_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_plant_repiq_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_territoire  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_groupement  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_localite  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_chefferie  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_village  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_saison  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_agent  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_association  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_bailleur  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_saison_assoc  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_essence_plant  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_utilisateur  to " + nom_utilisateur + @" 
                        grant select on tbl_groupe  to " + nom_utilisateur;

                        using (IDbCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = string.Format(requete);
                            cmd1.ExecuteNonQuery();
                        }
                        #endregion
                    }
                    else if (droit == 2)
                    {
                        #region Droit pour User
                        string requete = @"grant select,insert,update on tbl_fiche_ident_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_grp_c_fiche_ident_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_germoir_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_plant_repiq_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_territoire  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_groupement  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_localite  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_chefferie  to " + nom_utilisateur + @" 
                        grant select on tbl_agent  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_village  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_saison  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_association  to " + nom_utilisateur + @" 
                        grant select,insert,update on tbl_bailleur  to " + nom_utilisateur + @"
                        grant select,insert,update on tbl_saison_assoc  to " + nom_utilisateur + @"
                        grant select,insert,update on tbl_essence_plant  to " + nom_utilisateur + @"
                        grant select on tbl_utilisateur to " + nom_utilisateur;

                        using (IDbCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = string.Format(requete);
                            cmd2.ExecuteNonQuery();
                        }
                        #endregion
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec d'attribution des droits à l'utilisateur, veuillez réessayez ultérieurement : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
        }

        public void revokePermission(List<int> permission, string nom_login, string nom_utilisateur)
        {
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                //On tourne dans la boucle qui tournera tant qu'il ya encore un groupe dde permission à accordé
                foreach (int droit in permission)
                {
                    if (droit == 0)
                    {
                        //Droit pour administrateur
                        throw new Exception("Les droits de l'administrateur ne peuvent pas être retirés à ce niveau, reportez vous au moteur de SGBD");
                    }
                    else if (droit == 1)
                    {
                        #region Droit pour Admin
                        string requete = @"revoke select,insert,update on tbl_fiche_ident_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_grp_c_fiche_ident_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_germoir_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_plant_repiq_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_territoire  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_groupement  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_localite  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_chefferie  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_village  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_saison  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_agent  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_association  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_bailleur  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_saison_assoc  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_essence_plant  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_utilisateur  to " + nom_utilisateur + @" 
                        revoke select on tbl_groupe  to " + nom_utilisateur;

                        using (IDbCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = string.Format(requete);
                            cmd1.ExecuteNonQuery();
                        }
                        #endregion
                    }
                    else if (droit == 2)
                    {
                        #region Droit pour User
                        string requete = @"revoke select,insert,update on tbl_fiche_ident_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_grp_c_fiche_ident_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_germoir_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_plant_repiq_fiche_suivi_pepi  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_territoire  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_groupement  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_localite  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_chefferie  to " + nom_utilisateur + @" 
                        revoke select on tbl_agent  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_village  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_saison  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_association  to " + nom_utilisateur + @" 
                        revoke select,insert,update on tbl_bailleur  to " + nom_utilisateur + @"
                        revoke select,insert,update on tbl_saison_assoc  to " + nom_utilisateur + @"
                        revoke select,insert,update on tbl_essence_plant  to " + nom_utilisateur + @"
                        revoke select on tbl_utilisateur to " + nom_utilisateur;

                        using (IDbCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = string.Format(requete);
                            cmd2.ExecuteNonQuery();
                        }
                        #endregion
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                string MasterDirectory = ImplementUtilities.Instance.MasterDirectoryConfiguration;
                ImplementLog.Instance.PutLogMessage(MasterDirectory, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + " : Echec retrait des droits à l'utilisateur, veuillez réessayez ultérieurement : " + exc.Message, DirectoryUtilLog, MasterDirectory + "LogFile.txt");
                throw new Exception(exc.Message);
            }
        }
        #endregion
    } //***fin class 
} //***fin namespace 
