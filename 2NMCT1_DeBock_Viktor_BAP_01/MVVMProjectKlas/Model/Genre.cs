using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMProjectKlas.Model
{
    class Genre
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //methode om bands uit de database te gaan ophalen

        public static ObservableCollection<Genre> GetGenres()
        {
            ObservableCollection<Genre> lijst = new ObservableCollection<Genre>();

            String sSQL = "SELECT * FROM Genre";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Genre aNieuw = new Genre();

                aNieuw.ID = reader["ID"].ToString();
                aNieuw.Name = reader["Name"].ToString();
                lijst.Add(aNieuw);
            }

            return lijst;
        }
    }
}
