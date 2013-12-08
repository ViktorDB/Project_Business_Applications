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
    class Ticket
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string ticketholder;

        public string Ticketholder
        {
            get { return ticketholder; }
            set { ticketholder = value; }
        }

        private string ticketholderEmail;

        public string TicketholderEmail
        {
            get { return ticketholderEmail; }
            set { ticketholderEmail = value; }
        }

        private TicketType ticketType;

        public TicketType TicketType
        {
            get { return ticketType; }
            set { ticketType = value; }
        }

        private int amount;

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        private string ticketTypeNaam;

        public string TicketTypeNaam
        {
            get { return ticketTypeNaam; }
            set { ticketTypeNaam = value; }
        }

        private string ticketTypeID;

        public string TicketTypeID
        {
            get { return ticketTypeID; }
            set { ticketTypeID = value; }
        }


        //methode om tickets uit de database te gaan ophalen

        public static ObservableCollection<Ticket> GetTickets()
        {
            ObservableCollection<Ticket> lijst = new ObservableCollection<Ticket>();

            String sSQL = "SELECT *  FROM TICKET INNER JOIN TicketType ON TicketType = TicketType.ID";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                Ticket aNieuw = new Ticket();

                aNieuw.Ticketholder = reader["Ticketholder"].ToString();
                aNieuw.TicketholderEmail = reader["TicketholderEmail"].ToString();
                aNieuw.TicketTypeNaam = reader["Name"].ToString();
                aNieuw.TicketTypeID = reader["TicketType"].ToString();
                aNieuw.Amount = Convert.ToInt32(reader["Amount"]);
                
                lijst.Add(aNieuw);
            }

            return lijst;
        }


        public static int InsertTicket(Ticket b)
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
            par.ParameterName = "Ticketholder";
            par.Value = b.Ticketholder ;
            command.Parameters.Add(par);

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "TicketholderEmail";
            par2.Value = b.TicketholderEmail;
            command.Parameters.Add(par2);

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "TicketType";
            par3.Value = b.TicketTypeID;
            command.Parameters.Add(par3);

            DbParameter par4 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par4.ParameterName = "Amount";
            par4.Value = b.Amount;
            command.Parameters.Add(par4);

            command.CommandText = "INSERT INTO Ticket VALUES (@Ticketholder, @TicketholderEmail, @TicketType, @Amount)";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }
    }
}
