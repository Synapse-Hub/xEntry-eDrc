using ManageConnexion;
using ManageUtilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace xEntry_Data
{
    public class Factory
    {
        private SqlConnection dbConnexion;
        //Creation du constructeur pour la factory
        private Factory()
        {
            //Rien
        }
        //Creation de l'instance de la Factory
        private static Factory _fact;
        public static Factory Instance
        {
            get
            {
                if (_fact == null) _fact = new Factory();
                return _fact;
            }
        }

        //Initialisation de la chaine de connexion
        public void Initialise(string chaineDeConnexion)
        {
            dbConnexion = new SqlConnection(chaineDeConnexion);
        }
        /*
         cette methode setParameter renvoie a faire ceci :
         sqlcmd.parameter.add("champ de reference",Type des donnes sql.Taille,"valeur du parametre" etc... _
         
          */
        private void setParameter(string nomParametre, DbType typeParametre,
            object valeurParametre, int tailleParametre, SqlCommand sqlcmd)
        {

            SqlParameter p = new SqlParameter();
            if (valeurParametre == null)
                p.Value = DBNull.Value;
            else
                p.Value = valeurParametre;

                p.ParameterName = nomParametre;
                p.Size = tailleParametre;
                p.DbType = typeParametre;

                sqlcmd.Parameters.Add(p);
        }

        #region  UTILISATEUR
        /// <summary>
        /// Permet de verifier les paramètres de connexion de l'utilisateur, donc username et password
        /// et retourne un tableau contenant successivement l'Id de l'Agent, son nom et le niveau de l'utilisateur
        /// </summary>
        /// <param name="username">String nom d'utilisateur</param>
        /// <param name="password">String mot de passe</param>
        /// <returns>Tableau des string</returns>
        public string[] verifieLoginUser(string username, string password)
        {
            string[] tbValue = new string[3];
            bool okActivateUser = false;

            //Echec de la connexion en superAdministrateur alors on peut se connecte en Administrateur 
            //ou en invite
            if (username.ToLower().Equals("sa") || username.ToLower().Equals("wwfadmin"))
            {
                //Super utilisateur de la BD
                tbValue[0] = "0";
                tbValue[1] = "Superutilisateur de la BD";
                tbValue[2] = "Administrateur";
            }
            else
            {
                try
                {
                    if (ImplementConnection.Instance.Conn.State == ConnectionState.Closed)
                        ImplementConnection.Instance.Conn.Open();

                    using (IDbCommand cmd = ImplementConnection.Instance.Conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format(@"SELECT AGENT.id_agent AS id,AGENT.id_categorie ,agent.nom_agent AS nom,UTILISATEUR.activation AS activation,UTILISATEUR.nomuser,UTILISATEUR.droits AS droits,UTILISATEUR.motpass FROM AGENT 
                        LEFT OUTER JOIN UTILISATEUR ON agent.id_agent=UTILISATEUR.id_agentuser 
                        INNER JOIN CATEGORIEAGENT ON CATEGORIEAGENT.id_categorieagent=AGENT.id_categorie WHERE UTILISATEUR.nomuser='{0}' AND UTILISATEUR.motpass='{1}'", username, ImplementChiffer.Instance.Cipher(password, "rootWWF"));

                        using (IDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                tbValue[0] = dr["id"].ToString();
                                tbValue[1] = Convert.ToString(dr["nom"]);
                                tbValue[2] = Convert.ToString(dr["droits"]);//Tous les droit de l'utilisateur
                                //Ces droits sont:0->Administrateur,1->Médecin,2->Infirmier,3->Laborantin,4->Pharmacien,
                                //                5->Caissier,6->Médecin gynéco.,7->Service

                                tbValue[3] = Convert.ToString(dr["matricule"]);
                                okActivateUser = Convert.ToBoolean(dr["activation"]);

                                int nbr = 0;
                                try
                                {
                                    string[] nbdroit = tbValue[2].Split(',');
                                    foreach (string str in nbdroit) nbr++;
                                }
                                catch (Exception) { }

                                clsDoTraitement.nombre_droit = nbr;

                                //Si desvaleurs sont trouvee et que la personne se connecte tout en etant active ,on les inscrits 
                                //dans un fichier text dont le contenu sera supprime apres deconnexion de l'utilisateur
                                if (okActivateUser) { }
                                else
                                {
                                    tbValue[0] = "";
                                    tbValue[1] = "";
                                    tbValue[2] = "20";
                                    tbValue[3] = "";
                                }
                            }
                            else
                            {
                                tbValue[0] = "";
                                tbValue[1] = "";
                                tbValue[2] = "10";
                                tbValue[3] = "";
                            }
                        }
                    }
                    conn.Close();
                }
                catch (Exception exc)
                {
                    conn.Close();
                    throw new Exception(exc.Message);
                }
            }
            return tbValue;
        }

        public clsutilisateur getClsutilisateur(object intid)
        {
            clsutilisateur varclsutilisateur = new clsutilisateur();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT personne.id,utilisateur.id AS idUser,utilisateur.id_agent,isnull(personne.nom,'') + ' ' + isnull(personne.postnom,'') + ' ' + isnull(personne.prenom,'') AS nom,utilisateur.nomuser,utilisateur.motpass,utilisateur.schema_user,utilisateur.droits,utilisateur.activation FROM personne
                    INNER JOIN agent ON personne.id=agent.id_personne
                    INNER JOIN utilisateur ON agent.id=utilisateur.id_agent WHERE utilisateur.id={0}", intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            //Utilisateur
                            if (!dr["idUser"].ToString().Trim().Equals("")) varclsutilisateur.Id = int.Parse(dr["idUser"].ToString());
                            if (!dr["id_agent"].ToString().Trim().Equals("")) varclsutilisateur.Id_agent = int.Parse(dr["id_agent"].ToString());
                            varclsutilisateur.Nomuser = dr["nomuser"].ToString();
                            if (!dr["droits"].ToString().Trim().Equals("")) varclsutilisateur.Droits = dr["droits"].ToString();
                            if (!dr["schema_user"].ToString().Trim().Equals("")) varclsutilisateur.Schema_user = dr["schema_user"].ToString();
                            varclsutilisateur.Motpass = dr["motpass"].ToString();
                            if (!dr["activation"].ToString().Trim().Equals("")) varclsutilisateur.Activation = bool.Parse(dr["activation"].ToString());

                            //Personne
                            if (!dr["id"].ToString().Trim().Equals("")) varclsutilisateur.IdPers = int.Parse(dr["id"].ToString());
                            varclsutilisateur.Nom = dr["nom"].ToString();
                            //varclsutilisateur.Postnom = dr["postnom"].ToString();
                            //varclsutilisateur.Prenom = dr["prenom"].ToString();
                            //varclsutilisateur.Sexe = dr["sexe"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return varclsutilisateur;
        }

        public clsutilisateur getClsutilisateurUser(string nom_user)
        {
            clsutilisateur varclsutilisateur = new clsutilisateur();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT personne.id,utilisateur.id AS idUser,utilisateur.id_agent,isnull(personne.nom,'') + ' ' + isnull(personne.postnom,'') + ' ' + isnull(personne.prenom,'') AS nom,utilisateur.nomuser,utilisateur.motpass,utilisateur.schema_user,utilisateur.droits,utilisateur.activation FROM personne
                    INNER JOIN agent ON personne.id=agent.id_personne
                    INNER JOIN utilisateur ON agent.id=utilisateur.id_agent WHERE utilisateur.nomuser='{0}'", nom_user);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            //Utilisateur
                            if (!dr["idUser"].ToString().Trim().Equals("")) varclsutilisateur.Id = int.Parse(dr["idUser"].ToString());
                            if (!dr["id_agent"].ToString().Trim().Equals("")) varclsutilisateur.Id_agent = int.Parse(dr["id_agent"].ToString());
                            varclsutilisateur.Nomuser = dr["nomuser"].ToString();
                            if (!dr["droits"].ToString().Trim().Equals("")) varclsutilisateur.Droits = dr["droits"].ToString();
                            if (!dr["schema_user"].ToString().Trim().Equals("")) varclsutilisateur.Schema_user = dr["schema_user"].ToString();
                            varclsutilisateur.Motpass = dr["motpass"].ToString();
                            if (!dr["activation"].ToString().Trim().Equals("")) varclsutilisateur.Activation = bool.Parse(dr["activation"].ToString());

                            //Personne
                            if (!dr["id"].ToString().Trim().Equals("")) varclsutilisateur.IdPers = int.Parse(dr["id"].ToString());
                            varclsutilisateur.Nom = dr["nom"].ToString();
                            //varclsutilisateur.Postnom = dr["postnom"].ToString();
                            //varclsutilisateur.Prenom = dr["prenom"].ToString();
                            //varclsutilisateur.Sexe = dr["sexe"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return varclsutilisateur;
        }

        public clsutilisateur getClsutilisateur1(object intid_agent)
        {
            clsutilisateur varclsutilisateur = new clsutilisateur();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT personne.id,utilisateur.id AS idUser,utilisateur.id_agent,isnull(personne.nom,'') + ' ' + isnull(personne.postnom,'') + ' ' + isnull(personne.prenom,'') AS nom,utilisateur.nomuser,utilisateur.motpass,utilisateur.schema_user,utilisateur.droits,utilisateur.activation FROM personne
                    INNER JOIN agent ON personne.id=agent.id_personne
                    INNER JOIN utilisateur ON agent.id=utilisateur.id_agent WHERE agent.id={0}", intid_agent);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            //Utilisateur
                            if (!dr["idUser"].ToString().Trim().Equals("")) varclsutilisateur.Id = int.Parse(dr["idUser"].ToString());
                            if (!dr["id_agent"].ToString().Trim().Equals("")) varclsutilisateur.Id_agent = int.Parse(dr["id_agent"].ToString());
                            varclsutilisateur.Nomuser = dr["nomuser"].ToString();
                            if (!dr["schema_user"].ToString().Trim().Equals("")) varclsutilisateur.Schema_user = dr["schema_user"].ToString();
                            if (!dr["droits"].ToString().Trim().Equals("")) varclsutilisateur.Droits = dr["droits"].ToString();
                            varclsutilisateur.Motpass = dr["motpass"].ToString();
                            if (!dr["activation"].ToString().Trim().Equals("")) varclsutilisateur.Activation = bool.Parse(dr["activation"].ToString());

                            //Personne
                            if (!dr["id"].ToString().Trim().Equals("")) varclsutilisateur.IdPers = int.Parse(dr["id"].ToString());
                            varclsutilisateur.Nom = dr["nom"].ToString();
                            //varclsutilisateur.Postnom = dr["postnom"].ToString();
                            //varclsutilisateur.Prenom = dr["prenom"].ToString();
                            //varclsutilisateur.Sexe = dr["sexe"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return varclsutilisateur;
        }

        public DataTable getAllClsutilisateur1()
        {
            DataTable lstclsutilisateur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT personne.id,utilisateur.id AS idUser,agent.matricule,utilisateur.id_agent,isnull(personne.nom,'') + ' ' + isnull(personne.postnom,'') + ' ' + isnull(personne.prenom,'') AS nom,utilisateur.nomuser,utilisateur.motpass,utilisateur.activation,utilisateur.schema_user,utilisateur.droits FROM personne
                    INNER JOIN agent ON personne.id=agent.id_personne
                    INNER JOIN utilisateur ON agent.id=utilisateur.id_agent ORDER BY utilisateur.nomuser ASC");

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        lstclsutilisateur.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return lstclsutilisateur;
        }

        public DataTable getAllClsutilisateur1(int id_utilisateur)
        {
            DataTable lstclsutilisateur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT personne.id,utilisateur.id AS idUser,agent.matricule,utilisateur.id_agent,isnull(personne.nom,'') + ' ' + isnull(personne.postnom,'') + ' ' + isnull(personne.prenom,'') AS nom,utilisateur.nomuser,utilisateur.motpass,utilisateur.activation,utilisateur.schema_user,utilisateur.droits FROM personne
                    INNER JOIN agent ON personne.id=agent.id_personne
                    INNER JOIN utilisateur ON agent.id=utilisateur.id_agent WHERE utilisateur.id=" + id_utilisateur);

                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        lstclsutilisateur.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return lstclsutilisateur;
        }

        public DataTable getAllClsutilisateur2(int intid)
        {
            DataTable lstclsutilisateur = new DataTable();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT personne.id,utilisateur.id AS idUser,utilisateur.id_agent,isnull(personne.nom,'') + ' ' + isnull(personne.postnom,'') + ' ' + isnull(personne.prenom,'') AS nom,utilisateur.nomuser,utilisateur.motpass,utilisateur.schema_user,utilisateur.droits,utilisateur.activation FROM personne
                    INNER JOIN agent ON personne.id=agent.id_personne
                    INNER JOIN utilisateur ON agent.id=utilisateur.id_agent WHERE utilisateur.id=" + intid);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        lstclsutilisateur.Load(dr);
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return lstclsutilisateur;
        }

        public List<clsutilisateur> getAllClsutilisateur(string criteria)
        {
            List<clsutilisateur> lstclsutilisateur = new List<clsutilisateur>();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = @"SELECT personne.id,utilisateur.id AS idUser,utilisateur.id_agent,isnull(personne.nom,'') + ' ' + isnull(personne.postnom,'') + ' ' + isnull(personne.prenom,'') AS nom,utilisateur.nomuser,utilisateur.schema_user,utilisateur.droits,utilisateur.motpass,utilisateur.activation FROM personne
                    INNER JOIN agent ON personne.id=agent.id_personne
                    INNER JOIN utilisateur ON agent.id=utilisateur.id_agent  WHERE 1=1";
                    sql += "  OR   utilisateur.nomuser LIKE '%" + criteria + "%'";
                    sql += "  OR   utilisateur.motpass LIKE '%" + criteria + "%'";
                    sql += "  OR   utilisateur.droits LIKE '%" + criteria + "%'";
                    sql += "  OR   personne.nom LIKE '%" + criteria + "%'";
                    sql += "  OR   personne.postnom LIKE '%" + criteria + "%'";
                    sql += "  OR   personne.prenom LIKE '%" + criteria + "%'";
                    cmd.CommandText = string.Format(sql);
                    using (IDataReader dr = cmd.ExecuteReader())
                    {

                        clsutilisateur varclsutilisateur = null;
                        while (dr.Read())
                        {
                            varclsutilisateur = new clsutilisateur();
                            //Utilisateur
                            if (!dr["idUser"].ToString().Trim().Equals("")) varclsutilisateur.Id = int.Parse(dr["idUser"].ToString());
                            if (!dr["id_agent"].ToString().Trim().Equals("")) varclsutilisateur.Id_agent = int.Parse(dr["id_agent"].ToString());
                            varclsutilisateur.Nomuser = dr["nomuser"].ToString();
                            if (!dr["schema_user"].ToString().Trim().Equals("")) varclsutilisateur.Schema_user = dr["schema_user"].ToString();
                            if (!dr["droits"].ToString().Trim().Equals("")) varclsutilisateur.Droits = dr["droits"].ToString();
                            varclsutilisateur.Motpass = CryptageJosam_LIB.clsMetier.GetInstance().doDeCrypte(dr["motpass"].ToString());
                            if (!dr["activation"].ToString().Trim().Equals("")) varclsutilisateur.Activation = bool.Parse(dr["activation"].ToString());

                            //Personne
                            if (!dr["id"].ToString().Trim().Equals("")) varclsutilisateur.IdPers = int.Parse(dr["id"].ToString());
                            varclsutilisateur.Nom = dr["nom"].ToString();
                            varclsutilisateur.Postnom = dr["postnom"].ToString();
                            varclsutilisateur.Prenom = dr["prenom"].ToString();
                            varclsutilisateur.Sexe = dr["sexe"].ToString();
                            lstclsutilisateur.Add(varclsutilisateur);
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return lstclsutilisateur;
        }

        //Methode Aj

        public clsutilisateur getAllClsutilisateur1(string criteria)
        {
            clsutilisateur varclsutilisateur = new clsutilisateur();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    string sql = "SELECT * FROM utilisateur WHERE utilisateur.nomuser=@critaire";
                    cmd.CommandText = string.Format(sql);
                    cmd.Parameters.Add(getParameter(cmd, "@critaire", DbType.String, 100, criteria));

                    //cmd.CommandText = "SELECT * FROM utilisateur WHERE nomuser="+criteria;
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            //Utilisateur
                            if (!dr["id"].ToString().Trim().Equals("")) varclsutilisateur.Id = int.Parse(dr["id"].ToString());
                            if (!dr["id_agent"].ToString().Trim().Equals("")) varclsutilisateur.Id_agent = int.Parse(dr["id_agent"].ToString());
                            varclsutilisateur.Nomuser = dr["nomuser"].ToString();
                            if (!dr["schema_user"].ToString().Trim().Equals("")) varclsutilisateur.Schema_user = dr["schema_user"].ToString();
                            varclsutilisateur.Motpass = CryptageJosam_LIB.clsMetier.GetInstance().doDeCrypte(dr["motpass"].ToString());
                            if (!dr["activation"].ToString().Trim().Equals("")) varclsutilisateur.Activation = bool.Parse(dr["activation"].ToString());

                            //Personne
                            //if (!dr["id"].ToString().Trim().Equals("")) varclsutilisateur.IdPers = int.Parse(dr["id"].ToString());
                            //varclsutilisateur.Nom = dr["nom"].ToString();
                            ////varclsutilisateur.Postnom = dr["postnom"].ToString();
                            //varclsutilisateur.Prenom = dr["prenom"].ToString();
                            //varclsutilisateur.Sexe = dr["sexe"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return varclsutilisateur;
        }

        public List<clsutilisateur> getAllClsutilisateur()
        {
            List<clsutilisateur> lstclsutilisateur = new List<clsutilisateur>();
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"SELECT personne.id,utilisateur.id AS idUser,utilisateur.id_agent,isnull(personne.nom,'') + ' ' + isnull(personne.postnom,'') + ' ' + isnull(personne.prenom,'') AS nom,utilisateur.nomuser,utilisateur.motpass,utilisateur.schema_user,utilisateur.droits,utilisateur.activation FROM personne
                    INNER JOIN agent ON personne.id=agent.id_personne
                    INNER JOIN utilisateur ON agent.id=utilisateur.id_agent ORDER BY utilisateur.nomuser ASC");
                    using (IDataReader dr = cmd.ExecuteReader())
                    {
                        clsutilisateur varclsutilisateur = null;
                        while (dr.Read())
                        {
                            varclsutilisateur = new clsutilisateur();
                            //Utilisateur
                            if (!dr["idUser"].ToString().Trim().Equals("")) varclsutilisateur.Id = int.Parse(dr["idUser"].ToString());
                            if (!dr["id_agent"].ToString().Trim().Equals("")) varclsutilisateur.Id_agent = int.Parse(dr["id_agent"].ToString());
                            if (!dr["schema_user"].ToString().Trim().Equals("")) varclsutilisateur.Schema_user = dr["schema_user"].ToString();
                            if (!dr["droits"].ToString().Trim().Equals("")) varclsutilisateur.Droits = dr["droits"].ToString();
                            varclsutilisateur.Nomuser = dr["nomuser"].ToString();
                            varclsutilisateur.Motpass = CryptageJosam_LIB.clsMetier.GetInstance().doDeCrypte(dr["motpass"].ToString());
                            if (!dr["activation"].ToString().Trim().Equals("")) varclsutilisateur.Activation = bool.Parse(dr["activation"].ToString());

                            //Personne
                            if (!dr["id"].ToString().Trim().Equals("")) varclsutilisateur.IdPers = int.Parse(dr["id"].ToString());
                            varclsutilisateur.Nom = dr["nom"].ToString();
                            //varclsutilisateur.Postnom = dr["postnom"].ToString();
                            //varclsutilisateur.Prenom = dr["prenom"].ToString();
                            //varclsutilisateur.Sexe = dr["sexe"].ToString();
                            lstclsutilisateur.Add(varclsutilisateur);
                        }
                    }
                }
                conn.Close();
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return lstclsutilisateur;
        }

        public int insertClsutilisateur(clsutilisateur varclsutilisateur)
        {
            //On crée d'abord le user en déhors de la transaction
            bool echec_create = false;
            string message_erreur_user = "";
            try
            {
                //Avant de faire l'insertion dans la table utilisateur, on commence par créer le login et le user de la BD
                if (conn.State != ConnectionState.Open) conn.Open();
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"exec sp_addlogin '" + varclsutilisateur.Nomuser + "','" + varclsutilisateur.Motpass + "','" + clsMetier.bdEnCours + @"'
                                                 
                                                 exec sp_grantdbaccess '" + varclsutilisateur.Nomuser + @"'
                                                 ");
                    int j = cmd.ExecuteNonQuery();
                    echec_create = true;
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

                if (echec_create) { }
                else throw new Exception(message_erreur_user);//Si la création du user a échoué, on fait échoué le reste

                //Si l'on à cocher la case à cocher d'activation de l'utilisateur on doit le donner accès à se connecter ou non
                if ((bool)varclsutilisateur.Activation) { }
                else
                {
                    using (IDbCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = string.Format(@"revoke connect to " + varclsutilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                        cmd2.Transaction = transaction;
                        i = cmd2.ExecuteNonQuery();
                    }
                }

                //Insertion de l'utilisateur créé dans la table des user
                using (IDbCommand cmd3 = conn.CreateCommand())
                {
                    cmd3.CommandText = string.Format("INSERT INTO utilisateur ( id_agent,nomuser,motpass,activation,schema_user ) VALUES (@id_agent,@nomuser,@motpass,@activation,@schema_user  )");
                    cmd3.Parameters.Add(getParameter(cmd3, "@id_agent", DbType.Int32, 4, varclsutilisateur.Id_agent));
                    if (varclsutilisateur.Nomuser != null) cmd3.Parameters.Add(getParameter(cmd3, "@nomuser", DbType.String, 30, varclsutilisateur.Nomuser));
                    else cmd3.Parameters.Add(getParameter(cmd3, "@nomuser", DbType.String, 30, DBNull.Value));
                    if (varclsutilisateur.Motpass != null) cmd3.Parameters.Add(getParameter(cmd3, "@motpass", DbType.String, 30, CryptageJosam_LIB.clsMetier.GetInstance().doCrypte(varclsutilisateur.Motpass)));
                    else cmd3.Parameters.Add(getParameter(cmd3, "@motpass", DbType.String, 30, DBNull.Value));
                    cmd3.Parameters.Add(getParameter(cmd3, "@schema_user", DbType.String, 100, varclsutilisateur.Nomuser));
                    if (varclsutilisateur.Activation.HasValue) cmd3.Parameters.Add(getParameter(cmd3, "@activation", DbType.Boolean, 2, varclsutilisateur.Activation));
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
                    throw new Exception("Echec lors de la création de l'utilisateur, " + exc.Message);
                }
                conn.Close();
            }
            return i;
        }

        public int updateClsutilisateur(clsutilisateur varclsutilisateur)
        {
            IDbTransaction transaction = null;
            int i = 0;
            bool ok = false;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                transaction = conn.BeginTransaction(IsolationLevel.Serializable);

                if (clsDoTraitement.etat_modification_user == 1)
                {
                    //Modification du nom user seulement

                    //Avant de modifier l'utilisateur dans la table, on modifie le user de la bd
                    using (IDbCommand cmd1 = conn.CreateCommand())
                    {
                        varclsutilisateur.Nomuser = clsDoTraitement.newUser;
                        varclsutilisateur.Motpass = clsDoTraitement.oldPassword;
                        cmd1.CommandText = string.Format("alter login " + clsDoTraitement.oldUser + " with name=" + varclsutilisateur.Nomuser); //On modifie le login de l'utilisateur pour changer son mode de connexion
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
                        varclsutilisateur.Motpass = clsDoTraitement.newPassword;
                        cmd1.CommandText = string.Format("alter LOGIN " + varclsutilisateur.Nomuser + " WITH PASSWORD='" + clsDoTraitement.newPassword + "'"); //On modifie le login de l'utilisateur pour changer son mot de passe de connexion
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
                        varclsutilisateur.Nomuser = clsDoTraitement.newUser;
                        varclsutilisateur.Motpass = clsDoTraitement.newPassword;
                        cmd1.CommandText = string.Format("ALTER LOGIN " + clsDoTraitement.oldUser + " WITH PASSWORD='" + clsDoTraitement.newPassword + "'" + @"
                                                          ALTER LOGIN " + clsDoTraitement.oldUser + " WITH NAME=" + varclsutilisateur.Nomuser); //On modifie le login de l'utilisateur pour changer son mot de passe de connexion, puis on modifie son nom de login
                        cmd1.Transaction = transaction;
                        i = cmd1.ExecuteNonQuery();
                    }
                }

                if (clsDoTraitement.etat_modification_user == 1 || clsDoTraitement.etat_modification_user == 2 || clsDoTraitement.etat_modification_user == 3)
                {
                    //Modification de l'utilisateur
                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE utilisateur  SET id_agent=@id_agent,nomuser=@nomuser,motpass=@motpass,activation=@activation  WHERE 1=1  AND id=@id ");
                        cmd.Parameters.Add(getParameter(cmd, "@id_agent", DbType.Int32, 4, varclsutilisateur.Id_agent));
                        if (varclsutilisateur.Nomuser != null) cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, varclsutilisateur.Nomuser));
                        else cmd.Parameters.Add(getParameter(cmd, "@nomuser", DbType.String, 30, DBNull.Value));
                        if (varclsutilisateur.Motpass != null) cmd.Parameters.Add(getParameter(cmd, "@motpass", DbType.String, 30, CryptageJosam_LIB.clsMetier.GetInstance().doCrypte(varclsutilisateur.Motpass)));
                        else cmd.Parameters.Add(getParameter(cmd, "@motpass", DbType.String, 30, DBNull.Value));
                        if (varclsutilisateur.Activation.HasValue) cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, varclsutilisateur.Activation));
                        else cmd.Parameters.Add(getParameter(cmd, "@activation", DbType.Boolean, 2, DBNull.Value));
                        cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, varclsutilisateur.Id));
                        cmd.Transaction = transaction;
                        i = cmd.ExecuteNonQuery();
                        transaction.Commit();
                        ok = true;
                        conn.Close();
                    }
                }
                if (!ok) conn.Close();

                if (clsDoTraitement.etat_modification_user == 4)
                {
                    varclsutilisateur.Activation = clsDoTraitement.activationUser;
                    try
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();

                        if ((bool)varclsutilisateur.Activation)
                        {
                            using (IDbCommand cmd3 = conn.CreateCommand())
                            {
                                cmd3.CommandText = string.Format(@"grant connect to " + varclsutilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                                i = cmd3.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (IDbCommand cmd3 = conn.CreateCommand())
                            {
                                cmd3.CommandText = string.Format(@"revoke connect to " + varclsutilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                                i = cmd3.ExecuteNonQuery();
                            }
                        }

                        using (IDbCommand cmd4 = conn.CreateCommand())
                        {
                            cmd4.CommandText = string.Format("UPDATE utilisateur SET activation=@activation  WHERE 1=1  AND id=@id ");
                            cmd4.Parameters.Add(getParameter(cmd4, "@activation", DbType.Boolean, 2, varclsutilisateur.Activation));
                            cmd4.Parameters.Add(getParameter(cmd4, "@id", DbType.Int32, 4, varclsutilisateur.Id));
                            cmd4.Transaction = transaction;
                            i = cmd4.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    catch (Exception exc)
                    {
                        conn.Close();
                        throw new Exception("Echec lors de l'activation/ou désactivation de l'utilisateur, " + exc.Message);
                    }
                }
                else if (clsDoTraitement.etat_modification_user == 1 || clsDoTraitement.etat_modification_user == 2 || clsDoTraitement.etat_modification_user == 3)
                {
                    try
                    {
                        if (conn.State != ConnectionState.Open) conn.Open();

                        //On récupère le nom de l'utilisateur qui correspond au premier qui a été créé à la première fois
                        //et qui est équivalente au nom du schema de ce dernier

                        using (IDbCommand cmd2 = conn.CreateCommand())
                        {
                            cmd2.CommandText = string.Format(@"SELECT utilisateur.schema_user FROM utilisateur WHERE id=" + varclsutilisateur.Id);
                            using (IDataReader dr = cmd2.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    varclsutilisateur.Nomuser = dr["schema_user"].ToString();
                                }
                            }
                        }

                        //Si l'on à cocher la case à cocher d'activation de l'utilisateur on doit le donner accès à se connecter ou non
                        if ((bool)varclsutilisateur.Activation)
                        {
                            using (IDbCommand cmd3 = conn.CreateCommand())
                            {
                                cmd3.CommandText = string.Format(@"grant connect to " + varclsutilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                                i = cmd3.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                        else
                        {
                            using (IDbCommand cmd3 = conn.CreateCommand())
                            {
                                cmd3.CommandText = string.Format(@"revoke connect to " + varclsutilisateur.Nomuser); //On interdit à l'utilisateur de se connecter au serveur
                                i = cmd3.ExecuteNonQuery();
                                conn.Close();
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception exc)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    throw new Exception("Echec lors de la modification de l'utilisateur, " + exc.Message);
                }
                conn.Close();
            }
            clsDoTraitement.etat_modification_user = -1;
            return i;
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
                    cmd.CommandText = string.Format("UPDATE utilisateur  SET droits=@droits  WHERE 1=1  AND id=@id ");
                    if (droits != null) cmd.Parameters.Add(getParameter(cmd, "@droits", DbType.String, 100, droits));
                    else cmd.Parameters.Add(getParameter(cmd, "@droits", DbType.String, 100, DBNull.Value));
                    cmd.Parameters.Add(getParameter(cmd, "@id", DbType.Int32, 4, id_user));
                    i = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception exc)
            {
                conn.Close();
                throw new Exception(exc.Message);
            }
            return i;
        }

        public int deleteClsutilisateur(clsutilisateur varclsutilisateur)
        {
            int i = 0;
            IDbTransaction transaction = null;
            try
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                transaction = conn.BeginTransaction(IsolationLevel.Serializable);

                using (IDbCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = string.Format(@"SELECT utilisateur.schema_user FROM utilisateur WHERE utilisateur.id=" + varclsutilisateur.Id);
                    cmd1.Transaction = transaction;
                    using (IDataReader dr = cmd1.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            if (!dr["schema_user"].ToString().Trim().Equals("")) varclsutilisateur.Schema_user = dr["schema_user"].ToString();
                        }
                    }
                }

                //Avant de supprimer l'utilisateur dans la table, on supprime son schema qui correspond au premier nom d'utilisateur crée
                //puis on supprime son nom d'utilisateur et enfin on supprime son login
                using (IDbCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = string.Format("DROP SCHEMA " + varclsutilisateur.Schema_user + @" 
                                                      DROP USER " + varclsutilisateur.Schema_user + @"
                                                      DROP LOGIN " + varclsutilisateur.Nomuser);
                    cmd2.Transaction = transaction;
                    i = cmd2.ExecuteNonQuery();
                }

                //Enfin on supprime l'i=utilisateur dans la table des utilisateurs
                using (IDbCommand cmd3 = conn.CreateCommand())
                {
                    cmd3.CommandText = string.Format("DELETE FROM utilisateur WHERE  1=1  AND id=@id ");
                    cmd3.Parameters.Add(getParameter(cmd3, "@id", DbType.Int32, 4, varclsutilisateur.Id));
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
                    throw new Exception("Echec lors de la suppression de l'utilisateur, " + exc.Message);
                }
                conn.Close();
            }
            return i;
        }

        #endregion CLSUTILISATEUR
    }
}
