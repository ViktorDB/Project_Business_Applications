using MVVMProjectKlas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMProjectKlas.ViewModel
{
    class MainPageVM : ObservableObject, IPage
    {
        public MainPageVM()
        { 
            //data ophalen uit database
            _festival = Festival.GetFestival();
        }

        //property toevoegen waaraan we de listbox uit de usercontrol bands aan zullen binden
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
        
    }
}
