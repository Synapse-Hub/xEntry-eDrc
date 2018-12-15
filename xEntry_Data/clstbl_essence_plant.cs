using System;
using System.Data;

namespace Xentry.Data
{
    public class clstbl_essence_plant
    {
        //***Les variables globales***
        //****private string chaine_conn*****
        private string id_essence;
        private string essence;
        //***DataTables***
        public DataTable clstbl_essence_plantTables()
        {
            return clsMetier.GetInstance().getAllClstbl_essence_plant();
        }
        public DataTable clstbl_essence_plantTables(string criteria)
        {
            return clsMetier.GetInstance().getAllClstbl_essence_plant(criteria);
        }
        public int inserts()
        {
            return clsMetier.GetInstance().insertClstbl_essence_plant(this);
        }
        public int update(DataRowView varscls)
        {
            return clsMetier.GetInstance().updateClstbl_essence_plant(varscls);
        }
        public int delete(DataRowView varscls)
        {
            return clsMetier.GetInstance().deleteClstbl_essence_plant(varscls);
        }
        //***Le constructeur par defaut***
        public clstbl_essence_plant()
        {
        }

        //***Accesseur de id_essence***
        public string Id_essence
        {
            get { return id_essence; }
            set { id_essence = value; }
        }  //***Accesseur de essence***
        public string Essence
        {
            get { return essence; }
            set { essence = value; }
        }
    } //***fin class
} //***fin namespace