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
            //_lineUp = LineUp.GetLineUp();
            _festival = Festival.GetFestival();
            _diffLineUpDagen = LineUp.GetLineUpDagen();
            //String.Format("{0:M/d/yyyy}", _diffLineUpDagen); 
            LineUpDagen = new ObservableCollection<DateTime>();
            SelectedAddLineUpDag = Festivals[0].StartData;
            //_stages = Stage.GetStages();
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
            get { 
                //return lineUpDagen; 
                return LineUp.GetLineUpDagen();
            }
            set { lineUpDagen = value; }
        }

        private DateTime selectedDeleteLineUpDag;

        public DateTime SelectedDeleteLineUpDag
        {
            get { return selectedDeleteLineUpDag; }
            set 
            {
                selectedDeleteLineUpDag = value;
                FilterLineUp();
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

        //property toevoegen waaraan we de listbox uit de usercontrol lineup aan zullen binden
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
                //OnPropertyChanged("Bands"); // property is gewijzigd
            }
        }

        //property toevoegen waaraan de we stages kunnen binden
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
                //OnPropertyChanged("Stages"); // property is gewijzigd
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
                //OnPropertyChanged("LineUpDagen"); // property is gewijzigd
            }
        }

        //property toevoegen waaraan we de lineup aan zullen binden
        private ObservableCollection<LineUp> _lineUp;

        public ObservableCollection<LineUp> LineUps
        {
            get
            {
                //return _lineUp;
                return LineUp.GetLineUp();
            }
            set
            {
                _lineUp = value;
                //OnPropertyChanged("LineUp"); // property is gewijzigd
            }
        }

        //property toevoegen waaraan we de lineup aan zullen binden
        private ObservableCollection<LineUp> _lineUpF;

        public ObservableCollection<LineUp> LineUpsF
        {
            get
            {
                return _lineUpF;
                //return LineUp.GetLineUp();
            }
            set
            {
                _lineUpF = value;
                OnPropertyChanged("LineUpF"); // property is gewijzigd
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
                //OnPropertyChanged("Festival"); // property is gewijzigd
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
            try
            {
                DiffLineUpDagen.Remove(SelectedDeleteLineUpDag);
                Console.WriteLine(LineUp.DeleteDayFromLineUp(SelectedDeleteLineUpDag));
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
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
            try
            {
                LineUp b = new LineUp() { ID = SelectedItemLineUp.ID, Date = SelectedItemLineUp.Date, From = SelectedItemLineUp.From, Until = SelectedItemLineUp.Until, StageNummer = SelectedItemLineUp.CStage.ID, BandNummer = SelectedItemLineUp.CBand.ID };
                Console.WriteLine(LineUp.UpdateLineUp(b));
                OnPropertyChanged("LineUps");
                OnPropertyChanged("Stages");
                OnPropertyChanged("LineUpDagen");
                OnPropertyChanged("LineUpsF");
                FilterLineUp();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //Lineup toevoegen
        public ICommand AddLineUp
        {
            get
            {
                return new RelayCommand(addLineUp);
            }
        }

        private void addLineUp()
        {
            try
            {
                LineUp b = new LineUp() { Date = InsertDate, From = InsertFrom, Until = InsertUntil, StageNummer = InsertStage.ID, BandNummer = InsertBand.ID };
                Console.WriteLine(LineUp.InsertLineUp(b));
                OnPropertyChanged("LineUps");
                OnPropertyChanged("Stages");
                OnPropertyChanged("LineUpDagen");
                OnPropertyChanged("LineUpsF");
                FilterLineUp();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }

        //Lineup Deleten
        public ICommand DeleteSelectedLineUpRow
        {
            get
            {
                return new RelayCommand(deleteSelectedLineUpRow);
            }
        }

        private void deleteSelectedLineUpRow()
        {
            try
            {
                LineUp b = new LineUp() { ID = SelectedItemLineUp.ID };
                Console.WriteLine(LineUp.DeleteRowLineUp(b));
                OnPropertyChanged("LineUps");
                OnPropertyChanged("LineUpsF");
                FilterLineUp();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }
        
        //lineup filteren
        private void FilterLineUp()
        {
                ObservableCollection<LineUp> lu = new ObservableCollection<LineUp>();

                foreach (LineUp lineup in LineUps)
                {
                    if (selectedDeleteLineUpDag == lineup.Date)
                    {
                        lu.Add(lineup);
                    }
                }

                _lineUpF = lu;

                OnPropertyChanged("LineUps");
                OnPropertyChanged("LineUpsF");
        }
    }
}
