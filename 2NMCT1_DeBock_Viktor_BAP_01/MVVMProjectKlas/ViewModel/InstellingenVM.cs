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
    class InstellingenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Instellingen"; }
        }

        public InstellingenVM()
        {
            _genres = Genre.GetGenres();
            _ticketTypes = TicketType.GetTicketTypes();
            _contactPersonTypes = ContactpersonType.GetContactpersonTypes();
            _stages = Stage.GetStages();
        }

        //property toevoegen waaraan we de combobox uit de usercontrol instellingen aan zullen binden
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

        //property toevoegen waaraan we de comboboxen uit de usercontrol instellingen aan zullen binden
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

        //property toevoegen waaraan we de comboboxen uit de usercontrol instellingen aan zullen binden
        private ObservableCollection<ContactpersonType> _contactPersonTypes;

        public ObservableCollection<ContactpersonType> ContactPersonTypes
        {
            get
            {
                return _contactPersonTypes;
            }
            set
            {
                _contactPersonTypes = value;
                OnPropertyChanged("ContactPersonType"); // property is gewijzigd
            }
        }

        //property toevoegen waaraan we de comboboxen uit de usercontrol instellingen aan zullen binden
        private ObservableCollection<Stage> _stages;

        public ObservableCollection<Stage> Stages
        {
            get
            {
                return _stages;
            }
            set
            {
                _stages = value;
                OnPropertyChanged("Stages"); // property is gewijzigd
            }
        }

        #region insertstage

        private string insertStageName;

        public string InsertStageName
        {
            get { return insertStageName; }
            set { insertStageName = value; }
        }

        public Stage updateStageCombobox;

        public Stage UpdateStageCombobox
        {
            get { return updateStageCombobox; }
            set { updateStageCombobox = value; }
        }

        private string updateStageName;

        public string UpdateStageName
        {
            get { return updateStageName; }
            set { updateStageName = value; }
        }
        #endregion

        //Stage toevoegen
        public ICommand InsertStage
        {
            get
            {
                return new RelayCommand(insertStage);
            }
        }

        private void insertStage()
        {
            Stage s = new Stage() { Name = InsertStageName};
            Console.WriteLine(Stage.InsertStage(s));
        }

        //Stage wijzigen
        public ICommand UpdateStage
        {
            get
            {
                return new RelayCommand(updateStage);
            }
        }

        private void updateStage()
        {
            Stage s = new Stage() { ID = UpdateStageCombobox.ID, Name = UpdateStageName };
            Console.WriteLine(Stage.UpdateStage(s));
        }



    }
}
