using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMProjectKlas.Model
{
    class ContactpersonType
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        //methode om bands uit de database te gaan ophalen

        public static ObservableCollection<ContactpersonType> GetContactpersonTypes()
        {
            ObservableCollection<ContactpersonType> lijst = new ObservableCollection<ContactpersonType>();

            String sSQL = "SELECT * FROM ContactpersonType";
            DbDataReader reader = Database.GetData(sSQL);
            while (reader.Read())
            {
                ContactpersonType aNieuw = new ContactpersonType();

                aNieuw.ID = reader["ID"].ToString();
                aNieuw.Name = reader["Name"].ToString();
                lijst.Add(aNieuw);
            }

            return lijst;
        }
        
    }
}
