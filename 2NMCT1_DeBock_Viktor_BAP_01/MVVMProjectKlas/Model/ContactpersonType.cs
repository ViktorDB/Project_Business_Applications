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
    class ContactpersonType
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

        //methode om contactpersoontypes uit de database te gaan ophalen

        public static ObservableCollection<ContactpersonType> GetContactpersonTypes()
        {
            ObservableCollection<ContactpersonType> lijst = new ObservableCollection<ContactpersonType>();

            String sSQL = "SELECT * FROM ContactpersonType";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                ContactpersonType aNieuw = new ContactpersonType();

                aNieuw.ID = reader["ID"].ToString();
                aNieuw.Name = reader["Name"].ToString();
                lijst.Add(aNieuw);
            }

            return lijst;
        }

        public static int InsertStaff(ContactpersonType s)
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

            command.CommandText = "INSERT INTO ContactpersonType VALUES (@Name)";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int UpdateStaff(ContactpersonType c)
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
            par.Value = c.ID;
            command.Parameters.Add(par);

            DbParameter par1 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par1.ParameterName = "Name";
            par1.Value = c.Name;
            command.Parameters.Add(par1);

            command.CommandText = "UPDATE ContactpersonType SET Name = @Name WHERE ID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }
        
    }
}
