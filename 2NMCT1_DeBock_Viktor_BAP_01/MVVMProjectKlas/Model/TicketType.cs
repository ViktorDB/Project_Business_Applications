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
    class TicketType
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

        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        private int availableTikets;

        public int AvailableTickets
        {
            get { return availableTikets; }
            set { availableTikets = value; }
        }

        private int aantalTickets;

        public int AantalTickets
        {
            get { return aantalTickets; }
            set { aantalTickets = value; }
        }
        

        //methode om ticketypes uit de database te gaan ophalen

        public static ObservableCollection<TicketType> GetTicketTypes()
        {
            ObservableCollection<TicketType> lijst = new ObservableCollection<TicketType>();

            String sSQL = "SELECT * FROM TicketType";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                TicketType aNieuw = new TicketType();

                aNieuw.ID = reader["ID"].ToString();
                aNieuw.Name = reader["Name"].ToString();
                aNieuw.Price = Convert.ToDouble(reader["Price"]);
                aNieuw.AvailableTickets = Convert.ToInt32(reader["AvailableTickets"]);
                lijst.Add(aNieuw);
            }

            return lijst;
        }

        public static int InsertTicketType(TicketType t)
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
            par.Value = t.Name;
            command.Parameters.Add(par);

            DbParameter par1 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par1.ParameterName = "Price";
            par1.Value = t.Price;
            command.Parameters.Add(par1);

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "AvailableTickets";
            par2.Value = t.AvailableTickets;
            command.Parameters.Add(par2);

            command.CommandText = "INSERT INTO TicketType VALUES (@Name, @Price, @AvailableTickets)";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int UpdateTicketType(TicketType s)
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

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "Price";
            par2.Value = s.Price;
            command.Parameters.Add(par2);

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "AvailableTickets";
            par3.Value = s.AvailableTickets;
            command.Parameters.Add(par3);

            command.CommandText = "UPDATE TicketType SET Name = @Name, Price = @Price, AvailableTickets = @AvailableTickets WHERE ID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        public static int UpdateAvailableTickets(Ticket s)
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
            par.Value = s.TicketTypeID;
            command.Parameters.Add(par);

            int aantal = s.AvailableTicketsForType - s.Amount;

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "AvailableTickets";
            par3.Value = aantal;
            command.Parameters.Add(par3);

            command.CommandText = "UPDATE TicketType SET AvailableTickets = @AvailableTickets WHERE ID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }
        
    }
}
