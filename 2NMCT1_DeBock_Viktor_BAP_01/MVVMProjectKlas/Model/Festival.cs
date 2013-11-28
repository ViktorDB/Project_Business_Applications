using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMProjectKlas.Model
{
    class Festival
    {
        private DateTime startData;

        public DateTime StartData
        {
            get { return startData; }
            set { startData = value; }
        }

        private DateTime endData;

        public DateTime EndData
        {
            get { return endData; }
            set { endData = value; }
        }

        private string festivalNaam;

        public string FestivalNaam
        {
            get { return festivalNaam; }
            set { festivalNaam = value; }
        }

        private string festivalPlaats;

        public string FestivalPlaats
        {
            get { return festivalPlaats; }
            set { festivalPlaats = value; }
        }

        //methode om festival uit de database te gaan ophalen

        public static ObservableCollection<Festival> GetFestival()
        {
            ObservableCollection<Festival> lijst = new ObservableCollection<Festival>();

            String sSQL = "SELECT * FROM Festival";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Festival aNieuw = new Festival();

                aNieuw.FestivalNaam = reader["Naam"].ToString();
                aNieuw.FestivalPlaats = reader["Plaats"].ToString();
                aNieuw.StartData = (DateTime)reader["Startdate"];
                aNieuw.EndData = (DateTime)reader["EndDate"];

                lijst.Add(aNieuw);
            }

            return lijst;
        }

        

    }
}
