﻿using System;
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

        private int lastID;

        public int LastID
        {
            get { return lastID; }
            set { lastID = value; }
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

                aNieuw.ID = reader["IDBand"].ToString();
                int NieuwId = Convert.ToInt32(reader["IDBand"]);
                aNieuw.Description = reader["Description"].ToString();
                aNieuw.Name = reader["BandName"].ToString();
                aNieuw.Picture = reader["Picture"].ToString();
                aNieuw.Facebook = reader["Facebook"].ToString();
                aNieuw.Twitter = reader["Twitter"].ToString();
                //aNieuw.Genres = reader["Genres"];


                //SELECT * FROM Band INNER JOIN Band_Genre ON Band.ID = Band_Genre.BandID INNER JOIN Genre ON Genre.ID = Band_Genre.GenreID WHERE BandID = {0}


                String sSQLJOIN = "SELECT * FROM Band INNER JOIN Band_Genre ON Band.IDBand = Band_Genre.BandID INNER JOIN Genre ON Genre.ID = Band_Genre.GenreID WHERE BandID = {0}";
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


        //methode om de bands te updaten.
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

            //DbParameter par = DbProviderFactories.GetFactory(provider).CreateParameter();
            //par.ParameterName = "Name";
            //par.Value = b.Name;
            //command.Parameters.Add(par);

            //DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            //par2.ParameterName = "Description";
            //par2.Value = b.Description;
            //command.Parameters.Add(par2);

            //DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            //par3.ParameterName = "Twitter";
            //par3.Value = b.Twitter;
            //command.Parameters.Add(par3);

            //DbParameter par4 = DbProviderFactories.GetFactory(provider).CreateParameter();
            //par4.ParameterName = "Facebook";
            //par4.Value = b.Facebook;
            //command.Parameters.Add(par4);

            //DbParameter par5 = DbProviderFactories.GetFactory(provider).CreateParameter();
            //par5.ParameterName = "Picture";
            //par5.Value = b.Picture;
            //command.Parameters.Add(par5);

            //DbParameter par6 = DbProviderFactories.GetFactory(provider).CreateParameter();
            //par6.ParameterName = "ID";
            //par6.Value = b.ID;
            //command.Parameters.Add(par6);

            //DbParameter par7 = DbProviderFactories.GetFactory(provider).CreateParameter();
            //par7.ParameterName = "Genres";
            //par7.Value = b.StandardGenreGetal;
            //command.Parameters.Add(par7);

            if (b.Picture == null)
            {
                DbParameter para = DbProviderFactories.GetFactory(provider).CreateParameter();
                para.ParameterName = "Name";
                para.Value = b.Name;
                command.Parameters.Add(para);

                DbParameter para2 = DbProviderFactories.GetFactory(provider).CreateParameter();
                para2.ParameterName = "Description";
                para2.Value = b.Description;
                command.Parameters.Add(para2);

                DbParameter para3 = DbProviderFactories.GetFactory(provider).CreateParameter();
                para3.ParameterName = "Twitter";
                para3.Value = b.Twitter;
                command.Parameters.Add(para3);

                DbParameter para4 = DbProviderFactories.GetFactory(provider).CreateParameter();
                para4.ParameterName = "Facebook";
                para4.Value = b.Facebook;
                command.Parameters.Add(para4);

                //DbParameter para5 = DbProviderFactories.GetFactory(provider).CreateParameter();
                //para5.ParameterName = "Picture";
                //para5.Value = b.Picture;
                //command.Parameters.Add(para5);

                DbParameter para6 = DbProviderFactories.GetFactory(provider).CreateParameter();
                para6.ParameterName = "ID";
                para6.Value = b.ID;
                command.Parameters.Add(para6);

                DbParameter para7 = DbProviderFactories.GetFactory(provider).CreateParameter();
                para7.ParameterName = "Genres";
                para7.Value = b.StandardGenreGetal;
                command.Parameters.Add(para7);

                command.CommandText = "UPDATE Band SET BandName=@Name, Description=@Description, Twitter=@Twitter, Facebook=@Facebook, Genres=@Genres WHERE IDBand = @ID";
            }
            else
            {
                DbParameter parr = DbProviderFactories.GetFactory(provider).CreateParameter();
                parr.ParameterName = "Name";
                parr.Value = b.Name;
                command.Parameters.Add(parr);

                DbParameter parr2 = DbProviderFactories.GetFactory(provider).CreateParameter();
                parr2.ParameterName = "Description";
                parr2.Value = b.Description;
                command.Parameters.Add(parr2);

                DbParameter parr3 = DbProviderFactories.GetFactory(provider).CreateParameter();
                parr3.ParameterName = "Twitter";
                parr3.Value = b.Twitter;
                command.Parameters.Add(parr3);

                DbParameter parr4 = DbProviderFactories.GetFactory(provider).CreateParameter();
                parr4.ParameterName = "Facebook";
                parr4.Value = b.Facebook;
                command.Parameters.Add(parr4);

                DbParameter parr5 = DbProviderFactories.GetFactory(provider).CreateParameter();
                parr5.ParameterName = "Picture";
                parr5.Value = b.Picture;
                command.Parameters.Add(parr5);

                DbParameter parr6 = DbProviderFactories.GetFactory(provider).CreateParameter();
                parr6.ParameterName = "ID";
                parr6.Value = b.ID;
                command.Parameters.Add(parr6);

                DbParameter parr7 = DbProviderFactories.GetFactory(provider).CreateParameter();
                parr7.ParameterName = "Genres";
                parr7.Value = b.StandardGenreGetal;
                command.Parameters.Add(parr7);

                command.CommandText = "UPDATE Band SET BandName=@Name, Description=@Description, Twitter=@Twitter, Facebook=@Facebook, Picture=@Picture, Genres=@Genres WHERE IDBand = @ID";
            }

            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }


        //methode om de genres van de bands te updaten
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

        //methode om de genres van de bands te verwijderen
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

        //methode om een band toe te voegen
        public static int InsertBand(Band b)
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

            DbParameter par7 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par7.ParameterName = "Genres";
            par7.Value = b.StandardGenreGetal;
            command.Parameters.Add(par7);


            command.CommandText = "INSERT INTO Band VALUES (@Name, @Picture, @Description, @Twitter, @Facebook, @Genres)";
            int affected = command.ExecuteNonQuery();
            
            con.Close();

            return affected;

        }

        public static string laatsteID = "";

        public static String GetLastRowId()
        {
            String sSQL = "SELECT * FROM Band WHERE IDBand = (select MAX(IDBand) FROM Band)";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {

                laatsteID = reader["IDBand"].ToString();

            }

            return laatsteID;
        }

        //methode om een genre toe te voegen aan een nieuwe band
        public static int InsertBandGenre(Band b, String genreGetal)
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
            par1.Value = laatsteID;
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

    }
}
