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
    class LineUp
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private string from;

        public string From
        {
            get { return from; }
            set { from = value; }
        }

        private string until;

        public string Until
        {
            get { return until; }
            set { until = value; }
        }

        private String stage;

        public String Stage
        {
            get { return stage; }
            set { stage = value; }
        }

        private String band;

        public String Band
        {
            get { return band; }
            set { band = value; }
        }

        private Stage cstage;

        public Stage CStage
        {
            get { return cstage; }
            set { cstage = value; }
        }

        private Band cband;

        public Band CBand
        {
            get { return cband; }
            set { cband = value; }
        }

        public String BandNummer { get; set; }

        public String StageNummer { get; set; }


        //methode om de lineup uit de database te gaan ophalen

        public static ObservableCollection<LineUp> GetLineUp()
        {
            ObservableCollection<LineUp> lijst = new ObservableCollection<LineUp>();

            String sSQL = "SELECT LineUp.*, Stage.*, Band.* FROM Band INNER JOIN LineUp ON Band.IDBand = LineUp.Band INNER JOIN Stage ON LineUp.Stage = Stage.StageID";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                LineUp aNieuw = new LineUp();

                aNieuw.ID = reader["ID"].ToString();
                aNieuw.Date = Convert.ToDateTime(reader["Date"]);
                aNieuw.From = reader["From"].ToString();
                aNieuw.Until = reader["Until"].ToString();
                aNieuw.Stage = reader["Stage"].ToString();
                aNieuw.Band = reader["Band"].ToString();

                //aNieuw.Stage.ID = reader["StageID"].ToString();
                //aNieuw.Stage.Name = reader["StageName"].ToString();

                //aNieuw.Band.ID = reader["BandID"].ToString();
                //aNieuw.Band.Name = reader["BandName"].ToString();
                //aNieuw.Band.Picture = reader["Picture"].ToString();
                //aNieuw.Band.Description = reader["Description"].ToString();

                String sqlgenre = "SELECT * FROM Stage WHERE StageID = {0}";
                string data = aNieuw.stage;
                string sSQLGenre = string.Format(sqlgenre, data);
                DbDataReader readerStage = Database.GetData(sSQLGenre);

                aNieuw.CStage = new Stage();

                while (readerStage.Read())
                {
                    Stage aNieuwStage = new Stage();
                    aNieuwStage.ID = readerStage["StageID"].ToString();
                    aNieuwStage.Name = readerStage["StageName"].ToString();


                    aNieuw.CStage = aNieuwStage;
                }


                String sqlSband = "SELECT * FROM Band WHERE IDBand = {0}";
                string data1 = aNieuw.band;
                string sqlBand = string.Format(sqlSband, data1);
                DbDataReader readerBand = Database.GetData(sqlBand);

                aNieuw.CBand = new Band();

                while (readerBand.Read())
                {
                    Band aNieuwBand = new Band();
                    aNieuwBand.ID = readerBand["IDBand"].ToString();
                    aNieuwBand.Name = readerBand["BandName"].ToString();
                    aNieuwBand.Picture = readerBand["Picture"].ToString();
                    aNieuwBand.Description = readerBand["Description"].ToString();


                    aNieuw.CBand = aNieuwBand;
                }

                lijst.Add(aNieuw);
            }

            return lijst;
        }

        //methode om de verschillende dagen van de lineup uit de database te gaan ophalen

        public static ObservableCollection<DateTime> GetLineUpDagen()
        {
            ObservableCollection<DateTime> lijst = new ObservableCollection<DateTime>();

            String sSQL = "SELECT DISTINCT Date FROM LineUp";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                DateTime aNieuw;

                aNieuw = Convert.ToDateTime(reader["Date"]);

                lijst.Add(aNieuw);
            }

            return lijst;
        }

        // lineup updaten
        public static int UpdateLineUp(LineUp b)
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
            par.ParameterName = "ID";
            par.Value = b.ID;
            command.Parameters.Add(par);

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "Date";
            par2.Value = b.Date;
            command.Parameters.Add(par2);

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "From";
            par3.Value = b.From;
            command.Parameters.Add(par3);

            DbParameter par4 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par4.ParameterName = "Until";
            par4.Value = b.Until;
            command.Parameters.Add(par4);

            DbParameter par5 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par5.ParameterName = "Stage";
            par5.Value = b.StageNummer;
            command.Parameters.Add(par5);

            DbParameter par6 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par6.ParameterName = "Band";
            par6.Value = b.BandNummer;
            command.Parameters.Add(par6);

            command.CommandText = "UPDATE LineUp SET Date=@Date, [From]=@From, Until=@Until, Stage=@Stage, Band=@band WHERE ID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int InsertLineUp(LineUp b)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "Date";
            par2.Value = b.Date;
            command.Parameters.Add(par2);

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "From";
            par3.Value = b.From;
            command.Parameters.Add(par3);

            DbParameter par4 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par4.ParameterName = "Until";
            par4.Value = b.Until;
            command.Parameters.Add(par4);

            DbParameter par5 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par5.ParameterName = "Stage";
            par5.Value = b.StageNummer;
            command.Parameters.Add(par5);

            DbParameter par6 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par6.ParameterName = "Band";
            par6.Value = b.BandNummer;
            command.Parameters.Add(par6);


            command.CommandText = "INSERT INTO LineUp VALUES (@Date, @From, @Until, @Stage, @Band)";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int DeleteDayFromLineUp(DateTime b)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "Date";
            par2.Value = b;
            command.Parameters.Add(par2);

            command.CommandText = "DELETE FROM LineUp WHERE Date=@Date;";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int DeleteRowLineUp(LineUp b)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "ID";
            par2.Value = b.ID;
            command.Parameters.Add(par2);

            command.CommandText = "DELETE FROM LineUp WHERE ID=@ID;";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static ObservableCollection<LineUp> FilterLineUp(DateTime b)
        {
            ObservableCollection<LineUp> lijst = new ObservableCollection<LineUp>();

            String sSQL = "SELECT LineUp.*, Stage.*, Band.* FROM Band INNER JOIN LineUp ON Band.IDBand = LineUp.Band INNER JOIN Stage ON LineUp.Stage = Stage.StageID WHERE StageID = {0}";
            string datake = b.ToString();
            string sSQLl = string.Format(sSQL, datake);

            DbDataReader reader = Database.GetData(sSQLl);
            while (reader.Read())
            {
                LineUp aNieuw = new LineUp();

                aNieuw.ID = reader["ID"].ToString();
                aNieuw.Date = Convert.ToDateTime(reader["Date"]);
                aNieuw.From = reader["From"].ToString();
                aNieuw.Until = reader["Until"].ToString();
                aNieuw.Stage = reader["Stage"].ToString();
                aNieuw.Band = reader["Band"].ToString();


                String sqlgenre = "SELECT * FROM Stage WHERE StageID = {0}";
                string data = aNieuw.stage;
                string sSQLGenre = string.Format(sqlgenre, data);
                DbDataReader readerStage = Database.GetData(sSQLGenre);

                aNieuw.CStage = new Stage();

                while (readerStage.Read())
                {
                    Stage aNieuwStage = new Stage();
                    aNieuwStage.ID = readerStage["StageID"].ToString();
                    aNieuwStage.Name = readerStage["StageName"].ToString();


                    aNieuw.CStage = aNieuwStage;
                }


                String sqlSband = "SELECT * FROM Band WHERE IDBand = {0}";
                string data1 = aNieuw.band;
                string sqlBand = string.Format(sqlSband, data1);
                DbDataReader readerBand = Database.GetData(sqlBand);

                aNieuw.CBand = new Band();

                while (readerBand.Read())
                {
                    Band aNieuwBand = new Band();
                    aNieuwBand.ID = readerBand["IDBand"].ToString();
                    aNieuwBand.Name = readerBand["BandName"].ToString();
                    aNieuwBand.Picture = readerBand["Picture"].ToString();
                    aNieuwBand.Description = readerBand["Description"].ToString();


                    aNieuw.CBand = aNieuwBand;
                }

                lijst.Add(aNieuw);
            }

            return lijst;
        }


    }
}
