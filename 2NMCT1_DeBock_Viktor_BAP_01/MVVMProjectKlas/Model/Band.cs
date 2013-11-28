using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


                String sSQLJOIN = "SELECT * FROM Band INNER JOIN Band_Genre ON Band.ID = Band_Genre.BandID WHERE BandID={0}";
                string data = Convert.ToString(NieuwId);
                string sSQLGenre = string.Format(sSQLJOIN, data);
                DbDataReader readerGenre = Database.GetData(sSQLGenre);
                ObservableCollection<Genre> Genres = new ObservableCollection<Genre>();

                //while (readerGenre.Read())
                //{
                //    Genre aNieuwType = new Genre();
                //    aNieuwType.ID = readerGenre["ID"].ToString();
                //    aNieuwType.Name = readerGenre["Name"].ToString();


                //    aNieuw.Genres = aNieuwType;
                //}
                lijst.Add(aNieuw);
            }

            return lijst;
        }
    }
}
