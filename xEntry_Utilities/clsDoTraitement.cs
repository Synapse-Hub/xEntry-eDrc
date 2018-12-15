using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Xentry.Utilities
{
    public class clsDoTraitement
    {
        private static clsDoTraitement Fact;
        public static int etat_modification_user = -1;//Variable permettant de prendre le statut pour modification du user (User seul, Mot passe seul ou les deux)
        public static string oldUser = "";
        public static string newUser = "";
        public static string oldPassword = "";
        public static string newPassword = "";
        public static bool activationUser = false;
        public static int nombre_droit = 0;//Variable permettant de récupérer le nombre total des droits de l'utilisateur

        #region Unload ressource
        public void unloadData(BindingSource bsrc, DataGridView dgv)
        {
            bsrc.Dispose();
            dgv.Dispose();
        }

        public void unloadData(BindingSource bsrc, DataGridView dgv, ComboBox cbo)
        {
            bsrc.Dispose();
            dgv.Dispose();
            cbo.Dispose();
        }

        public void unloadData(BindingSource[] bsrc, DataGridView[] dgv, ComboBox[] cbo)
        {
            foreach (BindingSource b in bsrc) b.Dispose();
            foreach (DataGridView b in dgv) b.Dispose();
            foreach (ComboBox b in cbo) b.Dispose();
        }

        public void unloadData(BindingSource[] bsrc, ComboBox[] cbo)
        {
            foreach (BindingSource b in bsrc) b.Dispose();
            foreach (ComboBox b in cbo) b.Dispose();
        }

        public void unloadData(ComboBox[] cbo)
        {
            foreach (ComboBox b in cbo) b.Dispose();
        }

        public void unloadData(BindingSource[] bsrc)
        {
            foreach (BindingSource b in bsrc) b.Dispose();
        }

        public void unloadData(BindingSource[] bsrc, DataGridView[] dgv)
        {
            foreach (BindingSource b in bsrc) b.Dispose();
            foreach (DataGridView b in dgv) b.Dispose();
        }

        public void unloadData(DataGridView[] dgv)
        {
            foreach (DataGridView b in dgv) b.Dispose();
        }
        #endregion

        //Variable pour utilisateur Administrateur ou autres valeurs

        public static bool isAdmin = false;
        public static List<string> valueUser = new List<string>();//Liste qui contient les droits de l'utilisateur connecté
        public static int id_Agent_Connecte = -1;

        public clsDoTraitement()
        {
        }
        public static clsDoTraitement GetInstance()
        {
            if (Fact == null)
                Fact = new clsDoTraitement();
            return Fact;
        }

        #region ACTION SUR LA PHOTO ET FICHIER
        public Bitmap getImageFromByte(byte[] byteArray)
        {
            Bitmap image;
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                image = new Bitmap(stream);
            } return image;
        }

        public string getFileFromByte(byte[] byteArray)
        {
            string fpath = System.IO.Path.GetTempPath() + DateTime.Today.ToString("yyyyMMdd") + ".jpg";

            using (System.IO.FileStream f = new System.IO.FileStream(fpath, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                f.Write(byteArray, 0, byteArray.Length);
            }
            return fpath;
        }

        public byte[] getFileToByte(string file)
        {
            byte[] b;
            using (System.IO.FileStream f = System.IO.File.OpenRead(file))
            {
                int size = Convert.ToInt32(f.Length);
                b = new byte[size];
                f.Read(b, 0, size);
            }
            return b;
        }

        public Image LoadImage(string strImage)
        {
            if (strImage.Trim().Equals("")) return null;
            byte[] bytes = Convert.FromBase64String(strImage);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            return image;
        }

        public string ImageToString64(Image image)
        {
            string strValu = "";
            try
            {
                //using (Image image = Image.FromFile(path))
                //{
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    strValu = Convert.ToBase64String(imageBytes);
                }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return strValu;
        }
        #endregion
    }
}
