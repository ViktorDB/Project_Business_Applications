using System;
using System.Collections.Generic;
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

        private int amout;

        public int Amount
        {
            get { return amout; }
            set { amout = value; }
        }
        
        
    }
}
