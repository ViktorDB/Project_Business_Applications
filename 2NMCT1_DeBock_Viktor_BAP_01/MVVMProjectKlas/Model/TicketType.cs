using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
    }
}
