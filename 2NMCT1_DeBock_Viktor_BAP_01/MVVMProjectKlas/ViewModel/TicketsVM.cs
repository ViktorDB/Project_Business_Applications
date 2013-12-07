using MVVMProjectKlas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _ticketTypes = TicketType.GetTicketTypes();
        }


        //property toevoegen waaraan we de comboboxen uit de usercontrol tickets aan zullen binden
        private ObservableCollection<TicketType> _ticketTypes;

        public ObservableCollection<TicketType> TicketTypes
        {
            get
            {
                return _ticketTypes;
            }
            set
            {
                _ticketTypes = value;
                OnPropertyChanged("TicketTypes"); // property is gewijzigd
            }
        }
    }
}
