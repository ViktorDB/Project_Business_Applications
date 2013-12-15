using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

        // festival updaten
        public static int UpdateFestival(Festival b)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par = DbProviderFactories.GetFactory(provider).CreateParameter();
            par.ParameterName = "FestivalNaam";
            par.Value = b.FestivalNaam;
            command.Parameters.Add(par);

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "FestivalPlaats";
            par2.Value = b.FestivalPlaats;
            command.Parameters.Add(par2);

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "StartData";
            par3.Value = b.StartData;
            command.Parameters.Add(par3);

            DbParameter par4 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par4.ParameterName = "EndData";
            par4.Value = b.EndData;
            command.Parameters.Add(par4);

            command.CommandText = "UPDATE Festival SET Naam=@FestivalNaam, Plaats=@FestivalPlaats, StartDate=@StartData, EndDate=@EndData";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

    }
}
