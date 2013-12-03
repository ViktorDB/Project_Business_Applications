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
    class Band
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

        private string picture;

        public string Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string twitter;

        public string Twitter
        {
            get { return twitter; }
            set { twitter = value; }
        }

        private string facebook;

        public string Facebook
        {
            get { return facebook; }
            set { facebook = value; }
        }

        private ObservableCollection<Genre> genres;

        public ObservableCollection<Genre> Genres
        {
            get { return genres; }
            set { genres = value; }
        }

        public ObservableCollection<String> genreGetallen;

        public ObservableCollection<String> GenreGetallen
        {
            get { return genreGetallen; }
            set { genreGetallen = value; }
        }

        private int standardGenreGetal;

        public int StandardGenreGetal
        {
            get { return standardGenreGetal; }
            set { standardGenreGetal = value; }
        }
        



        //methode om bands uit de database te gaan ophalen

        public static ObservableCollection<Band> GetBands()
        {
            ObservableCollection<Band> lijst = new ObservableCollection<Band>();

            String sSQL = "SELECT * FROM Band";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Band aNieuw = new Band();

                aNieuw.ID = reader["ID"].ToString();
                int NieuwId = Convert.ToInt32(reader["ID"]);
                aNieuw.Description = reader["Description"].ToString();
                aNieuw.Name = reader["Name"].ToString();
                aNieuw.Picture = reader["Picture"].ToString();
                aNieuw.Facebook = reader["Facebook"].ToString();
                aNieuw.Twitter = reader["Twitter"].ToString();
                //aNieuw.Genres = reader["Genres"];


                //SELECT * FROM Band INNER JOIN Band_Genre ON Band.ID = Band_Genre.BandID INNER JOIN Genre ON Genre.ID = Band_Genre.GenreID WHERE BandID = {0}


                String sSQLJOIN = "SELECT * FROM Band INNER JOIN Band_Genre ON Band.ID = Band_Genre.BandID INNER JOIN Genre ON Genre.ID = Band_Genre.GenreID WHERE BandID = {0}";
                string data = Convert.ToString(NieuwId);
                string sSQLGenre = string.Format(sSQLJOIN, data);
                DbDataReader readerGenre = Database.GetData(sSQLGenre);

                aNieuw.Genres = new ObservableCollection<Genre>();

                while (readerGenre.Read())
                {
                    Genre aNieuwType = new Genre();
                    aNieuwType.ID = readerGenre["GenreID"].ToString();
                    aNieuwType.Name = readerGenre["GenreName"].ToString();


                    aNieuw.Genres.Add(aNieuwType);
                }
                lijst.Add(aNieuw);
            }

            return lijst;
        }

        public static int UpdateBand(Band b)
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
            par.ParameterName = "Name";
            par.Value = b.Name;
            command.Parameters.Add(par);

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "Description";
            par2.Value = b.Description;
            command.Parameters.Add(par2);

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "Twitter";
            par3.Value = b.Twitter;
            command.Parameters.Add(par3);

            DbParameter par4 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par4.ParameterName = "Facebook";
            par4.Value = b.Facebook;
            command.Parameters.Add(par4);

            DbParameter par5 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par5.ParameterName = "Picture";
            par5.Value = b.Picture;
            command.Parameters.Add(par5);

            DbParameter par6 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par6.ParameterName = "ID";
            par6.Value = b.ID;
            command.Parameters.Add(par6);

            DbParameter par7 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par7.ParameterName = "Genres";
            par7.Value = b.StandardGenreGetal;
            command.Parameters.Add(par7);


            command.CommandText = "UPDATE Band SET Name=@Name, Description=@Description, Twitter=@Twitter, Facebook=@Facebook, Picture=@Picture, Genres=@Genres WHERE ID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int UpdateBandGenre(Band b, String genreGetal)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par1 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par1.ParameterName = "BandID";
            par1.Value = b.ID;
            command.Parameters.Add(par1);

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "GenreID";
            par2.Value = genreGetal;
            command.Parameters.Add(par2);


            command.CommandText = "INSERT INTO Band_Genre VALUES (@BandID, @GenreID)";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;
        }

        public static int DeleteAllBandGenres(Band b)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par1 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par1.ParameterName = "BandID";
            par1.Value = b.ID;
            command.Parameters.Add(par1);

            command.CommandText = "DELETE FROM Band_Genre WHERE BandID=@BandID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;
        }


    }
}
