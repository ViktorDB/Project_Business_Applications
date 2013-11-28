using MVVMProjectKlas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMProjectKlas.ViewModel
{
    class BandsVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Bands"; }
        }

        public BandsVM()
        { 
            //data ophalen uit database
            _bands = Band.GetBands();
            _genres = Genre.GetGenres();
        }

        //property toevoegen waaraan we de listbox uit de usercontrol bands aan zullen binden
        private ObservableCollection<Band> _bands;
        
        public ObservableCollection<Band> Bands
        {
            get
            {
                return _bands;
            }
            set
            {
                _bands = value;
                OnPropertyChanged("Bands"); // property is gewijzigd
            }
        }

        //property om het geselecteerde item vanuit de listbox te gaan bijhouden
        private Band _bandSelected;
        public Band SelectedBand
        {
            get
            {
                return _bandSelected;
            }
            set 
            {
                _bandSelected = value; OnPropertyChanged("SelectedBand");
            }
        }

        //property toevoegen waaraan we de listbox uit de usercontrol bands aan zullen binden
        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get
            {
                return _genres;
            }
            set
            {
                _genres = value;
                OnPropertyChanged("Genres"); // property is gewijzigd
            }
        }



    }
}
