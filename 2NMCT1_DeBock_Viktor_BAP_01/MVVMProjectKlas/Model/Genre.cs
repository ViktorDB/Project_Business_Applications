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
                aNieuw.Name = reader["GenreName"].ToString();
                lijst.Add(aNieuw);
            }

            return lijst;
        }

        public static int InsertGenre(Genre g)
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
            par.Value = g.Name;
            command.Parameters.Add(par);

            command.CommandText = "INSERT INTO Genre VALUES (@Name)";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int UpdateGenre(Genre g)
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
            par.Value = g.ID;
            command.Parameters.Add(par);

            DbParameter par1 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par1.ParameterName = "Name";
            par1.Value = g.Name;
            command.Parameters.Add(par1);

            command.CommandText = "UPDATE Genre SET GenreName = @Name WHERE ID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }
    }
}
