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
    class MainPageVM : ObservableObject, IPage
    {
        public MainPageVM()
        { 
            //data ophalen uit database
            _festival = Festival.GetFestival();
        }

        //property toevoegen waaraan we de listbox uit de usercontrol mainpage aan zullen binden
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

        //property toevoegen waaraan we de textbox uit de usercontrol mainpage aan zullen binden
        private ObservableCollection<Festival> _festival;
        
        public ObservableCollection<Festival> Festivals
        {
            get
            {
                return _festival;
            }
            set
            {
                _festival = value;
                OnPropertyChanged("Festival"); // property is gewijzigd
            }
        }

        public string Name
        {
            get { return "Festival"; }
        }

        //festival wijzigen
        public ICommand UpdateFestival
        {
            get
            {

                return new RelayCommand(updateFestival);
            }
        }

        
        private void updateFestival()
        {
            Festival b = new Festival() { FestivalNaam = Festivals[0].FestivalNaam, FestivalPlaats = Festivals[0].FestivalPlaats, StartData = Festivals[0].StartData, EndData = Festivals[0].EndData };
            try
            {
                Console.WriteLine(Festival.UpdateFestival(b));
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }
        
    }
}
