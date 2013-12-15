using GalaSoft.MvvmLight.Command;
using MVVMProjectKlas.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace MVVMProjectKlas.ViewModel
{
    class LineUpVM : ObservableObject, IPage
    {
        public LineUpVM()
        {
            _lineUp = LineUp.GetLineUp();
            _festival = Festival.GetFestival();
            _diffLineUpDagen = LineUp.GetLineUpDagen();
            //String.Format("{0:M/d/yyyy}", _diffLineUpDagen); 
            LineUpDagen = new ObservableCollection<DateTime>();
            SelectedAddLineUpDag = Festivals[0].StartData;
            _stages = Stage.GetStages();
            _bands = Band.GetBands();
        }

        public string Name
        {
            get { return "Line Up"; }
        }

        #region properties

        private ObservableCollection<DateTime> lineUpDagen;

        public ObservableCollection<DateTime> LineUpDagen
        {
            get { return lineUpDagen; }
            set { lineUpDagen = value; }
        }

        private DateTime selectedDeleteLineUpDag;

        public DateTime SelectedDeleteLineUpDag
        {
            get { return selectedDeleteLineUpDag; }
            set 
            {
                selectedDeleteLineUpDag = value;
            }
        }
        
        public DateTime SelectedAddLineUpDag { get; set; }

        #endregion 

        #region insertprops
        public DateTime InsertDate { get; set; }
        public Stage InsertStage { get; set; }
        public Band InsertBand { get; set; }
        public string InsertFrom { get; set; }
        public string InsertUntil { get; set; }
        #endregion

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

        //property toevoegen waaraan de we stages kunnen binden
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

        //property toevoegen waaraan we de geselecteerd lineup aan zullen binden
        private LineUp _selectedItemLineUp;

        public LineUp SelectedItemLineUp
        {
            get
            {
                return _selectedItemLineUp;
            }
            set
            {
                _selectedItemLineUp = value;
                OnPropertyChanged("SelectedItemLineUp"); // property is gewijzigd
            }
        }


        //property toevoegen waaraan we de lineup dagen aan zullen binden
        private ObservableCollection<DateTime> _diffLineUpDagen;

        public ObservableCollection<DateTime> DiffLineUpDagen
        {
            get
            {
                return _diffLineUpDagen;
            }
            set
            {
                _diffLineUpDagen = value;
                OnPropertyChanged("LineUpDagen"); // property is gewijzigd
            }
        }

        //property toevoegen waaraan we de lineup aan zullen binden
        private ObservableCollection<LineUp> _lineUp;

        public ObservableCollection<LineUp> LineUps
        {
            get
            {
                return _lineUp;
            }
            set
            {
                _lineUp = value;
                OnPropertyChanged("LineUp"); // property is gewijzigd
            }
        }


        //property toevoegen waaraan we de Datepicker aan zullen binden
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

        //Dag toevoegen aan LineUp
        public ICommand AddLineUpDag
        {
            get
            {
                return new RelayCommand(addLineUpDag);
            }
        }

        private void addLineUpDag()
        {
            DiffLineUpDagen.Add(SelectedAddLineUpDag);
        }

        //Dag verwijderen aan LineUp
        public ICommand DeleteLineUpDag
        {
            get
            {
                return new RelayCommand(deleteLineUpDag);
            }
        }

        private void deleteLineUpDag()
        {
            DiffLineUpDagen.Remove(SelectedDeleteLineUpDag);
        }

        //Lineup wijzigen
        public ICommand WijzigLineUp
        {
            get
            {
                return new RelayCommand(wijziglineUp);
            }
        }

        private void wijziglineUp()
        {
            LineUp b = new LineUp() { ID = SelectedItemLineUp.ID, Date = SelectedItemLineUp.Date, From = SelectedItemLineUp.From, Until = SelectedItemLineUp.Until, StageNummer = SelectedItemLineUp.CStage.ID, BandNummer = SelectedItemLineUp.CBand.ID };
            Console.WriteLine(LineUp.UpdateLineUp(b));
        }

        //Lineup wijzigen
        public ICommand AddLineUp
        {
            get
            {
                return new RelayCommand(addLineUp);
            }
        }

        private void addLineUp()
        {
            LineUp b = new LineUp() {  Date = InsertDate, From = InsertFrom, Until = InsertUntil, StageNummer = InsertStage.ID, BandNummer = InsertBand.ID };
            Console.WriteLine(LineUp.InsertLineUp(b));
        }
        
    }
}
