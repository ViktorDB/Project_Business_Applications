using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using GalaSoft.MvvmLight.Command;
using MVVMProjectKlas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        //property toevoegen waaraan we de combobox uit de usercontrol tickets aan zullen binden
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

        //property toevoegen waaraan we de selected ticket aan zullen binden
        private Ticket selectedTicket;

        public Ticket SelectedTicket
        {
            get
            {
                return selectedTicket;
            }
            set
            {
                selectedTicket = value;
                OnPropertyChanged("SelectedTicket"); // property is gewijzigd
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
            try
            {
                TicketType tt = new TicketType() { ID = ReserveerTicketCombobox.ID, Name = ReserveerTicketCombobox.Name, AvailableTickets = ReserveerTicketCombobox.AvailableTickets, Price = ReserveerTicketCombobox.Price, AantalTickets = ReserveerTicketAantal };

                VoorlopigeTicketsLijst.Add(tt);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
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
            try
            {
                VoorlopigeTicketsLijst.Remove(SelectedTicketTypeListbox);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
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
            try
            {
                ObservableCollection<Ticket> ReservatieLijst = new ObservableCollection<Ticket>();
                //TicketType ReservatieLijn = new TicketType();

                for (int i = 0; i < VoorlopigeTicketsLijst.Count(); i++)
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
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //Ticket exporteren
        public ICommand ExportTickets
        {
            get
            {
                return new RelayCommand(exportTickets);
            }
        }

        private void exportTickets()
        {
            try
            {
                string filename = "ticket_export/" + SelectedTicket.Ticketholder + "_" + SelectedTicket.TicketTypeNaam + ".docx";
                File.Copy("template.docx", filename, true);
                WordprocessingDocument newdoc = WordprocessingDocument.Open(filename, true);
                IDictionary<String, BookmarkStart> bookmarks = new Dictionary<String, BookmarkStart>();
                foreach (BookmarkStart bms in newdoc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
                {
                    bookmarks[bms.Name] = bms;
                }
                bookmarks["TicketHolder"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(SelectedTicket.Ticketholder)), bookmarks["TicketHolder"]);
                bookmarks["TicketHolderEmail"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(SelectedTicket.TicketholderEmail)), bookmarks["TicketHolderEmail"]);
                bookmarks["TicketType"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(SelectedTicket.TicketTypeNaam)), bookmarks["TicketType"]);
                bookmarks["Aantal"].Parent.InsertAfter<DocumentFormat.OpenXml.Wordprocessing.Run>(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(SelectedTicket.Amount.ToString())), bookmarks["Aantal"]);
                newdoc.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }

        }


    }
}
