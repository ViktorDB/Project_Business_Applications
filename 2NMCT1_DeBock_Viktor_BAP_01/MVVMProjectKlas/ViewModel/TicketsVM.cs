using GalaSoft.MvvmLight.Command;
using MVVMProjectKlas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMProjectKlas.ViewModel
{
    class TicketsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Tickets"; }
        }

        public TicketsVM()
        {
            //data ophalen uit database
            //_tickets = Ticket.GetTickets();
            //_ticketTypes = TicketType.GetTicketTypes();
            VoorlopigeTicketsLijst = new ObservableCollection<TicketType>();
            GereserveerdeTickets = new ObservableCollection<Ticket>();
        }

        private ObservableCollection<Ticket> _tickets;

        public ObservableCollection<Ticket> Tickets
        {
            get
            {
                //return _tickets;
                return Ticket.GetTickets();
            }
            set
            {
                _tickets = value;
                OnPropertyChanged("Tickets"); // property is gewijzigd
            }
        }

        //property toevoegen waaraan we de comboboxen uit de usercontrol tickets aan zullen binden
        private ObservableCollection<TicketType> _ticketTypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get
            {
                //return _ticketTypes;
                return TicketType.GetTicketTypes();
            }
            set
            {
                _ticketTypes = value;
                OnPropertyChanged("TicketTypes"); // property is gewijzigd
            }
        }

        #region reserveer tickets

        private String reserveerTicketName;

        public String ReserveerTicketName
        {
            get { return reserveerTicketName; }
            set { reserveerTicketName = value; }
        }

        private string reserveerTicketEmail;

        public string ReserveerTicketEmail
        {
            get { return reserveerTicketEmail; }
            set { reserveerTicketEmail = value; }
        }

        private int reserveerTicketAantal;

        public int ReserveerTicketAantal
        {
            get { return reserveerTicketAantal; }
            set { reserveerTicketAantal = value; }
        }

        private TicketType reserveerTicketCombobox;

        public TicketType ReserveerTicketCombobox
        {
            get { return reserveerTicketCombobox; }
            set { reserveerTicketCombobox = value; }
        }

        private ObservableCollection<TicketType> voorlopigeTicketsLijst;

        public ObservableCollection<TicketType> VoorlopigeTicketsLijst
        {
            get { return voorlopigeTicketsLijst; }
            set { voorlopigeTicketsLijst = value; }
        }

        private TicketType selectedTicketTypeListbox;

        public TicketType SelectedTicketTypeListbox
        {
            get { return selectedTicketTypeListbox; }
            set { selectedTicketTypeListbox = value; }
        }

        private ObservableCollection<Ticket> gereserveerdeTickets;

        public ObservableCollection<Ticket> GereserveerdeTickets
        {
            get { return gereserveerdeTickets; }
            set { gereserveerdeTickets = value; }
        }
        
        

        #endregion

        //Tickets Toevoegen aan Voorlopige lijst
        public ICommand AddTickets
        {
            get
            {
                return new RelayCommand(addTickets);
            }
        }

        private void addTickets()
        {
            TicketType tt = new TicketType() {ID = ReserveerTicketCombobox.ID, Name = ReserveerTicketCombobox.Name, AvailableTickets = ReserveerTicketCombobox.AvailableTickets, Price = ReserveerTicketCombobox.Price, AantalTickets = ReserveerTicketAantal};

            VoorlopigeTicketsLijst.Add(tt);
        }

        //Tickets verwijderen aan Voorlopige lijst
        public ICommand DeleteTickets
        {
            get
            {
                return new RelayCommand(deleteTickets);
            }
        }

        private void deleteTickets()
        {
            VoorlopigeTicketsLijst.Remove(SelectedTicketTypeListbox);
        }

        //Tickets reserveren
        public ICommand ReserveerTickets
        {
            get
            {
                return new RelayCommand(reserveerTickets);
            }
        }

        private void reserveerTickets()
        {
            ObservableCollection<Ticket> ReservatieLijst = new ObservableCollection<Ticket>();
            //TicketType ReservatieLijn = new TicketType();

            for (int i = 0; i < VoorlopigeTicketsLijst.Count(); i++ )
            {
                Ticket ReservatieLijn = new Ticket() { Ticketholder = ReserveerTicketName, Amount = VoorlopigeTicketsLijst[i].AantalTickets, TicketholderEmail = ReserveerTicketEmail, TicketTypeID = VoorlopigeTicketsLijst[i].ID, TicketTypeNaam = VoorlopigeTicketsLijst[i].Name, AvailableTicketsForType = VoorlopigeTicketsLijst[i].AvailableTickets };
                GereserveerdeTickets.Add(ReservatieLijn);
                Console.WriteLine(TicketType.UpdateAvailableTickets(ReservatieLijn));
            }

            foreach (Ticket ticketje in GereserveerdeTickets)
            {
                Console.WriteLine(Ticket.InsertTicket(ticketje));    
            }
            OnPropertyChanged("Tickets");
            OnPropertyChanged("TicketTypes");
        }

    }
}
