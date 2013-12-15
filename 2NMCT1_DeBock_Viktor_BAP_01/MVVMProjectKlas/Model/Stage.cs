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
    class Stage
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

        //methode om stages uit de database te gaan ophalen

        public static ObservableCollection<Stage> GetStages()
        {
            ObservableCollection<Stage> lijst = new ObservableCollection<Stage>();

            String sSQL = "SELECT * FROM Stage";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Stage aNieuw = new Stage();

                aNieuw.ID = reader["StageID"].ToString();
                aNieuw.Name = reader["StageName"].ToString();
                lijst.Add(aNieuw);
            }

            return lijst;
        }

        public static int InsertStage(Stage s)
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
            par.Value = s.Name;
            command.Parameters.Add(par);

            command.CommandText = "INSERT INTO Stage VALUES (@Name)";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int UpdateStage(Stage s)
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
            par.Value = s.ID;
            command.Parameters.Add(par);

            DbParameter par1 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par1.ParameterName = "Name";
            par1.Value = s.Name;
            command.Parameters.Add(par1);

            command.CommandText = "UPDATE Stage SET StageName = @Name WHERE StageID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }
        
    }
}
