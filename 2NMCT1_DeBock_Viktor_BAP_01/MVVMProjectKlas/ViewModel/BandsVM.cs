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
            InsertGenres = new ObservableCollection<Genre>();

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
                //OnPropertyChanged("Bands"); // property is gewijzigd
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

        //property om nieuwe band in op te slaan
        private Band _newBand;
        public Band NewBand
        {
            get
            {
                return _newBand;
            }
            set
            {
                _newBand = value; OnPropertyChanged("NewBand");
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

        //property om het geselecteed genre op te halen
        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get
            {
                return _selectedGenre;
            }
            set
            {
                _selectedGenre = value; OnPropertyChanged("SelectedGenre");
            }
        }

        private Genre _selectedGenreListbox;
        public Genre SelectedGenreListbox
        {
            get
            {
                return _selectedGenreListbox;
            }
            set
            {
                _selectedGenreListbox = value; OnPropertyChanged("SelectedGenreListbox");
            }
        }

        #region insertband properties

        private string insertPicture;

        public string InsertPicture
        {
            get { return insertPicture; }
            set { insertPicture = value; }
        }

        private string insertName;

        public string InsertName
        {
            get { return insertName; }
            set { insertName = value; }
        }

        private string insertDescription;

        public string InsertDescription
        {
            get { return insertDescription; }
            set { insertDescription = value; }
        }

        private string insertFacebook;

        public string InsertFacebook
        {
            get { return insertFacebook; }
            set { insertFacebook = value; }
        }

        private string insertTwitter;

        public string InsertTwitter
        {
            get { return insertTwitter; }
            set { insertTwitter = value; }
        }

        private ObservableCollection<Genre> insertGenres;

        public ObservableCollection<Genre> InsertGenres
        {
            get { return insertGenres; }
            set { insertGenres = value; }
        }

        public ObservableCollection<String> insertgenreGetallen;

        public ObservableCollection<String> InsertGenreGetallen
        {
            get { return insertgenreGetallen; }
            set { insertgenreGetallen = value; }
        }

        private int insertStandardGenreGetal;

        public int InsertStandardGenreGetal
        {
            get { return insertStandardGenreGetal; }
            set { insertStandardGenreGetal = value; }
        }
        
        #endregion

        //Genre Toevoegen aan band
        public ICommand AddGenreToBand
        {
            get
            {
                return new RelayCommand(addGenreToBand);
            }
        }

        private void addGenreToBand()
        {
            if (SelectedBand.Genres == null)
            {
                SelectedBand.Genres.Add(SelectedGenre);
            }
            else
            {
                if (SelectedBand.Genres.Where(Genres => Genres.ID == SelectedGenre.ID).Any() == false)
                {
                    SelectedBand.Genres.Add(SelectedGenre);
                }   
            }
        }

        //Genre Toevoegen aan band (insert)
        public ICommand AddInsertGenreToBand
        {
            get
            {
                return new RelayCommand(addInsertGenreToBand);
            }
        }

        ObservableCollection<Genre> nieuwgenre = new ObservableCollection<Genre>();

        private void addInsertGenreToBand()
        {
            if (InsertGenres.Count == 0)
            {
                InsertGenres.Add(SelectedGenre);
            }

            else
            {
                if (InsertGenres.Where(Genres => Genres.ID == SelectedGenre.ID).Any() == false)
                {
                    InsertGenres.Add(SelectedGenre);
                }
            }

        }

        //InsertGenre Verwijderen van band
        public ICommand DeleteInsertGenreFromBand
        {
            get
            {
                return new RelayCommand(deleteInsertGenreFromBand);
            }
        }

        private void deleteInsertGenreFromBand()
        {
            InsertGenres.Remove(SelectedGenreListbox);
        }

        //Genre Verwijderen van band
        public ICommand DeleteGenreFromBand
        {
            get
            {
                return new RelayCommand(deleteGenreFromBand);
            }
        }

        private void deleteGenreFromBand()
        {
                SelectedBand.Genres.Remove(SelectedGenreListbox);
        }

        //Band wijzigen
        public ICommand UpdateBand
        {
            get
            {
                return new RelayCommand(updateBand);
            }
        }

        private void updateBand()
        {
            ObservableCollection<String> GenreIdGetallen = new ObservableCollection<string>();
            foreach(Genre genre in SelectedBand.Genres)
            {
                string getal = genre.ID;
                GenreIdGetallen.Add(getal);
            }

            
            Band b = new Band() { ID = SelectedBand.ID, Description = SelectedBand.Description, Name = SelectedBand.Name, Picture = "ImageSource", Facebook = SelectedBand.Facebook, Twitter = SelectedBand.Facebook, GenreGetallen = GenreIdGetallen, StandardGenreGetal = 1 };
            Console.WriteLine(Band.UpdateBand(b));

            Console.WriteLine(Band.DeleteAllBandGenres(b));

            foreach (string genreGetal in GenreIdGetallen)
            {
                Console.WriteLine(Band.UpdateBandGenre(b, genreGetal));
            }
        }

        //Contactpersoon toevoegen
        public ICommand InsertBand
        {
            get
            {
                return new RelayCommand(insertBand);
            }
        }

        private void insertBand()
        {
            ObservableCollection<String> GenreIdGetallen = new ObservableCollection<string>();
            foreach (Genre genre in InsertGenres)
            {
                string getal = genre.ID;
                GenreIdGetallen.Add(getal);
            }


            Band b = new Band() { Name = insertName, Description = insertDescription, Facebook = InsertFacebook, Twitter = InsertTwitter, Picture = "ImageSource", GenreGetallen = GenreIdGetallen, StandardGenreGetal = 1};
            Bands.Add(b);
            Console.WriteLine(Band.InsertBand(b));
            Console.WriteLine(Band.GetLastRowId());

            foreach (string genreGetal in GenreIdGetallen)
            {
                Console.WriteLine(Band.InsertBandGenre(b, genreGetal));
            }
        }


    }
}
