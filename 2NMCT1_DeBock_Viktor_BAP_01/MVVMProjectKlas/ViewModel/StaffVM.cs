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
    class StaffVM : ObservableObject, IPage
    {
        //properties voor inserten

        public string Name
        {
            get { return "Staff"; }
        }

        public StaffVM()
        { 
            //data ophalen uit database
            //_staff = Contactperson.GetStaff();
            _contactPersonTypes = ContactpersonType.GetContactpersonTypes();
        }

        //property toevoegen waaraan we de listbox uit de usercontrol bands aan zullen binden
        private ObservableCollection<Contactperson> _staff;
        
        public ObservableCollection<Contactperson> Staff
        {
            get
            {
                //return _staff;
                return Contactperson.GetStaff();
            }
            set
            {
                _staff = value;
                OnPropertyChanged("Staff"); // property is gewijzigd
            }
        }

        //property om het geselecteerde item vanuit de listbox te gaan bijhouden
        private Contactperson _staffSelected;
        public Contactperson SelectedStaff
        {
            get
            {
                return _staffSelected;
            }
            set 
            {
                _staffSelected = value; OnPropertyChanged("SelectedStaff");
            }
        }





        private Contactperson _newStaff;
        public Contactperson NewStaff
        {
            get
            {
                return _newStaff;
            }
            set
            {
                _newStaff = value; OnPropertyChanged("NewStaff");
            }
        }

        #region insertProperties

        private string insertid;

        public string InsertID
        {
            get { return insertid; }
            set { insertid = value; }
        }

        private string insertname;

        public string InsertName
        {
            get { return insertname; }
            set { insertname = value; }
        }

        private string insertcompany;

        public string InsertCompany
        {
            get { return insertcompany; }
            set { insertcompany = value; }
        }

        private ContactpersonType insertjobRole;

        public ContactpersonType InsertJobRole
        {
            get { return insertjobRole; }
            set { insertjobRole = value; }
        }

        private string insertcity;

        public string InsertCity
        {
            get { return insertcity; }
            set { insertcity = value; }
        }

        private string insertemail;

        public string InsertEmail
        {
            get { return insertemail; }
            set { insertemail = value; }
        }

        private string insertphone;

        public string InsertPhone
        {
            get { return insertphone; }
            set { insertphone = value; }
        }

        private string insertcellphone;

        public string InsertCellphone
        {
            get { return insertcellphone; }
            set { insertcellphone = value; }
        }

        private string insertadres;

        public string InsertAdres
        {
            get { return insertadres; }
            set { insertadres = value; }
        }

        private int insertgetalType;

        public int InsertGetalType
        {
            get { return insertgetalType; }
            set { insertgetalType = value; }
        }

        #endregion


        //property toevoegen waaraan we de listbox uit de usercontrol bands aan zullen binden
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

        //Contactpersoon toevoegen
        public ICommand InsertStaff
        {
            get
            {
                return new RelayCommand(insertStaff);
            }
        }

        private void insertStaff()
        {
            Contactperson c = new Contactperson() {  Name = InsertName, Company = InsertCompany, GetalType = Convert.ToInt32(InsertJobRole.ID), City = InsertCity, Email = InsertEmail, Phone = InsertPhone, Cellphone = InsertCellphone, Adres = InsertAdres };
            Console.WriteLine(Contactperson.InsertContactperson(c));
            OnPropertyChanged("Staff");
        }

        //Contactpersoon wijzigen
        public ICommand UpdateStaff
        {
            get
            {
                return new RelayCommand(updateStaff);
            }
        }

        private void updateStaff()
        {
            Contactperson c = new Contactperson() { ID = SelectedStaff.ID, Name = SelectedStaff.Name, Company = SelectedStaff.Company, GetalType = Convert.ToInt32(SelectedStaff.JobRole.ID), City = SelectedStaff.City, Email = SelectedStaff.Email, Phone = SelectedStaff.Phone, Cellphone = SelectedStaff.Cellphone, Adres = SelectedStaff.Adres };
            Console.WriteLine(Contactperson.UpdateContactperson(c));
            OnPropertyChanged("Staff");
        }

        //Contactpersoon wijzigen
        public ICommand DeleteStaff
        {
            get
            {
                return new RelayCommand(deleteStaff);
            }
        }

        private void deleteStaff()
        {
            Contactperson c = new Contactperson() { ID = SelectedStaff.ID, Name = SelectedStaff.Name, Company = SelectedStaff.Company, GetalType = Convert.ToInt32(SelectedStaff.JobRole.ID), City = SelectedStaff.City, Email = SelectedStaff.Email, Phone = SelectedStaff.Phone, Cellphone = SelectedStaff.Cellphone, Adres = SelectedStaff.Adres };
            Console.WriteLine(Contactperson.DeleteContactperson(c));
            OnPropertyChanged("Staff");
        }

    }
}
