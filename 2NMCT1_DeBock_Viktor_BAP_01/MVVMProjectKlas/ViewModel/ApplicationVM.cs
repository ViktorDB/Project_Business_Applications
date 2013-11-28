using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVMProjectKlas.ViewModel
{
    public class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
            Pages.Add(new MainPageVM());
            Pages.Add(new LineUpVM());
            Pages.Add(new BandsVM());
            Pages.Add(new StaffVM());
            Pages.Add(new TicketsVM());
            Pages.Add(new InstellingenVM());
            CurrentPage = Pages[0];
        }

        private List<IPage> _pages;

        public List<IPage> Pages
        {
            get
            {
                if (_pages == null)
                    _pages = new List<IPage>();
                return _pages;
            }
        }

        private IPage _currentPage;
        public IPage CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        private void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }
}
