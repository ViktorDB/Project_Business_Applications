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
            //_genres = Genre.GetGenres();
            //_ticketTypes = TicketType.GetTicketTypes();
            //_contactPersonTypes = ContactpersonType.GetContactpersonTypes();
            //_stages = Stage.GetStages();
        }

        //property toevoegen waaraan we de combobox uit de usercontrol instellingen aan zullen binden
        private ObservableCollection<Genre> _genres;

        public ObservableCollection<Genre> Genres
        {
            get
            {
                //return _genres;
                return Genre.GetGenres();
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
               // return _ticketTypes;
                return TicketType.GetTicketTypes();
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
                //return _contactPersonTypes;
                return ContactpersonType.GetContactpersonTypes();
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
                //return _stages;
                return Stage.GetStages();
            }
            set
            {
                _stages = value;
                OnPropertyChanged("Stages"); // property is gewijzigd
            }
        }

        #region insertstage and updatestage

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

        #region insertgenre and updategenre

        private string insertGenreName;

        public string InsertGenreName
        {
            get { return insertGenreName; }
            set { insertGenreName = value; }
        }

        public Genre updateGenreCombobox;

        public Genre UpdateGenreCombobox
        {
            get { return updateGenreCombobox; }
            set { updateGenreCombobox = value; }
        }

        private string updateGenreName;

        public string UpdateGenreName
        {
            get { return updateGenreName; }
            set { updateGenreName = value; }
        }

        #endregion

        #region insertstaff and updatestaff

        private string insertStaffName;

        public string InsertStaffName
        {
            get { return insertStaffName; }
            set { insertStaffName = value; }
        }

        public ContactpersonType updateStaffCombobox;

        public ContactpersonType UpdateStaffCombobox
        {
            get { return updateStaffCombobox; }
            set { updateStaffCombobox = value; }
        }

        private string updateStaffName;

        public string UpdateStaffName
        {
            get { return updateStaffName; }
            set { updateStaffName = value; }
        }

        #endregion

        #region insertstaff and updatestaff

        private string insertTicketName;

        public string InsertTicketName
        {
            get { return insertTicketName; }
            set { insertTicketName = value; }
        }

        private double insertTicketPrice;

        public double InsertTicketPrice
        {
            get { return insertTicketPrice; }
            set { insertTicketPrice = value; }
        }

        private int insertTicketNumber;

        public int InsertTicketNumber
        {
            get { return insertTicketNumber; }
            set { insertTicketNumber = value; }
        }

        private TicketType updateTicketCombobox;

        public TicketType UpdateTicketCombobox
        {
            get { return updateTicketCombobox; }
            set { updateTicketCombobox = value; }
        }

        private string updateTicketName;

        public string UpdateTicketName
        {
            get { return updateTicketName; }
            set { updateTicketName = value; }
        }

        private double updateTicketPrice;

        public double UpdateTicketPrice
        {
            get { return updateTicketPrice; }
            set { updateTicketPrice = value; }
        }

        private int updateTicketNumber;

        public int UpdateTicketNumber
        {
            get { return updateTicketNumber; }
            set { updateTicketNumber = value; }
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
            try
            {
                Stage s = new Stage() { Name = InsertStageName };
                Console.WriteLine(Stage.InsertStage(s));
                OnPropertyChanged("Stages");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
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
            try
            {
                Stage s = new Stage() { ID = UpdateStageCombobox.ID, Name = UpdateStageName };
                Console.WriteLine(Stage.UpdateStage(s));
                OnPropertyChanged("Stages");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //Genre toevoegen
        public ICommand InsertGenre
        {
            get
            {
                return new RelayCommand(insertGenre);
            }
        }

        private void insertGenre()
        {
            try
            {
                Genre g = new Genre() { Name = InsertGenreName };
                Console.WriteLine(Genre.InsertGenre(g));
                OnPropertyChanged("Genres");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //Genre wijzigen
        public ICommand UpdateGenre
        {
            get
            {
                return new RelayCommand(updateGenre);
            }
        }

        private void updateGenre()
        {
            try
            {
                Genre g = new Genre() { ID = UpdateGenreCombobox.ID, Name = UpdateGenreName };
                Console.WriteLine(Genre.UpdateGenre(g));
                OnPropertyChanged("Genres");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //staff toevoegen
        public ICommand InsertStaff
        {
            get
            {
                return new RelayCommand(insertStaff);
            }
        }

        private void insertStaff()
        {
            try
            {
                ContactpersonType s = new ContactpersonType() { Name = InsertStaffName };
                Console.WriteLine(ContactpersonType.InsertStaff(s));
                OnPropertyChanged("ContactPersonTypes");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //staff wijzigen
        public ICommand UpdateStaff
        {
            get
            {
                return new RelayCommand(updateStaff);
            }
        }

        private void updateStaff()
        {
            try
            {
                ContactpersonType c = new ContactpersonType() { ID = UpdateStaffCombobox.ID, Name = UpdateStaffName };
                Console.WriteLine(ContactpersonType.UpdateStaff(c));
                OnPropertyChanged("ContactPersonTypes");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //TicketType toevoegen
        public ICommand InsertTicketType
        {
            get
            {
                return new RelayCommand(insertTicketType);
            }
        }

        private void insertTicketType()
        {
            try
            {
                TicketType t = new TicketType() { Name = InsertTicketName, Price = InsertTicketPrice, AvailableTickets = InsertTicketNumber };
                Console.WriteLine(TicketType.InsertTicketType(t));
                OnPropertyChanged("TicketTypes");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //TicketType wijzigen
        public ICommand UpdateTicketType
        {
            get
            {
                return new RelayCommand(updateTicketType);
            }
        }

        private void updateTicketType()
        {
            try
            {
                TicketType t = new TicketType() { ID = UpdateTicketCombobox.ID, Name = UpdateTicketName, Price = UpdateTicketPrice, AvailableTickets = UpdateTicketNumber };
                Console.WriteLine(TicketType.UpdateTicketType(t));
                OnPropertyChanged("TicketTypes");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }
    }
}
