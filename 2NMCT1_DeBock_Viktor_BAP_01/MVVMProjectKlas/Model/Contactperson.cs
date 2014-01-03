using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.DataAnnotations;


namespace MVVMProjectKlas.Model
{
    class Contactperson : IDataErrorInfo
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        //geprobeerd om met data validation te werken maar het wou niet werken
        [Required(ErrorMessage = "De naam is verplicht")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 50 karakters bevatten ")]
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string company;

        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        private ContactpersonType jobRole;

        public ContactpersonType JobRole
        {
            get { return jobRole; }
            set { jobRole = value; }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        private string cellphone;

        public string Cellphone
        {
            get { return cellphone; }
            set { cellphone = value; }
        }

        private string adres;

        public string Adres
        {
            get { return adres; }
            set { adres = value; }
        }

        private int getalType;

        public int GetalType
        {
            get { return getalType; }
            set { getalType = value; }
        }

        //methode om staff uit de database te gaan ophalen

        public static ObservableCollection<Contactperson> GetStaff()
        {
            ObservableCollection<Contactperson> lijst = new ObservableCollection<Contactperson>();

            
            String sSQL = "SELECT * FROM Contactperson";
            DbDataReader reader = Database.GetData(sSQL);

            

            while (reader.Read())
            {
                Contactperson aNieuw = new Contactperson();

                aNieuw.ID = reader["ID"].ToString();
                aNieuw.Company = reader["Company"].ToString();
                aNieuw.Name = reader["Name"].ToString();
                int iGetal = Int32.Parse(reader["Jobrole"].ToString());
                aNieuw.City = reader["City"].ToString();
                aNieuw.Email = reader["Email"].ToString();
                aNieuw.Phone = reader["Phone"].ToString();
                aNieuw.Cellphone = reader["Cellphone"].ToString();
                aNieuw.Adres = reader["Address"].ToString();

                String sSQLJobrole = "SELECT * FROM ContactpersonType WHERE ID = {0}";
                string data = Convert.ToString(iGetal);
                string sSQLJobrole1 = string.Format(sSQLJobrole, data);
                DbDataReader readerContactpersonType = Database.GetData(sSQLJobrole1);

                while (readerContactpersonType.Read())
                {
                    ContactpersonType aNieuwType = new ContactpersonType();
                    aNieuwType.ID = readerContactpersonType["ID"].ToString();
                    aNieuwType.Name = readerContactpersonType["Name"].ToString();


                    aNieuw.JobRole = aNieuwType;
                }
                lijst.Add(aNieuw);
            }

            return lijst;
        }

        //methode om contactpersoon toe te voegen
        public static int InsertContactperson(Contactperson c)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par = DbProviderFactories.GetFactory(provider).CreateParameter();
            par.ParameterName = "Name";
            par.Value = c.Name;
            command.Parameters.Add(par);

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "Company";
            par2.Value = c.Company;
            command.Parameters.Add(par2);

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "Jobrole";
            par3.Value = c.GetalType;
            command.Parameters.Add(par3);

            DbParameter par4 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par4.ParameterName = "City";
            par4.Value = c.City;
            command.Parameters.Add(par4);

            DbParameter par5 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par5.ParameterName = "Email";
            par5.Value = c.Email;
            command.Parameters.Add(par5);

            DbParameter par6 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par6.ParameterName = "Phone";
            par6.Value = c.Phone;
            command.Parameters.Add(par6);

            DbParameter par7 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par7.ParameterName = "Cellphone";
            par7.Value = c.Cellphone;
            command.Parameters.Add(par7);

            DbParameter par8 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par8.ParameterName = "Address";
            par8.Value = c.Adres;
            command.Parameters.Add(par8);

            command.CommandText = "INSERT INTO Contactperson VALUES(@Name,@Company,@JobRole,@City,@Email,@Phone,@Cellphone,@Address)";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }

        //methode om contactpersoon de updaten
        public static int UpdateContactperson(Contactperson c)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par = DbProviderFactories.GetFactory(provider).CreateParameter();
            par.ParameterName = "Name";
            par.Value = c.Name;
            command.Parameters.Add(par);

            DbParameter par2 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par2.ParameterName = "Company";
            par2.Value = c.Company;
            command.Parameters.Add(par2);

            DbParameter par3 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par3.ParameterName = "Jobrole";
            par3.Value = c.GetalType;
            command.Parameters.Add(par3);

            DbParameter par4 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par4.ParameterName = "City";
            par4.Value = c.City;
            command.Parameters.Add(par4);

            DbParameter par5 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par5.ParameterName = "Email";
            par5.Value = c.Email;
            command.Parameters.Add(par5);

            DbParameter par6 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par6.ParameterName = "Phone";
            par6.Value = c.Phone;
            command.Parameters.Add(par6);

            DbParameter par7 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par7.ParameterName = "Cellphone";
            par7.Value = c.Cellphone;
            command.Parameters.Add(par7);

            DbParameter par8 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par8.ParameterName = "Address";
            par8.Value = c.Adres;
            command.Parameters.Add(par8);

            DbParameter par9 = DbProviderFactories.GetFactory(provider).CreateParameter();
            par9.ParameterName = "ID";
            par9.Value = c.ID;
            command.Parameters.Add(par9);

            command.CommandText = "UPDATE Contactperson SET Name=@Name, Company=@Company, JobRole=@JobRole, City=@City, Email=@Email, Phone=@Phone, Cellphone=@Cellphone, Address=@Address WHERE ID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }


        //methode om contactpersoon te verwijderen
        public static int DeleteContactperson(Contactperson c)
        {
            string provider = ConfigurationManager.ConnectionStrings["db_EventManager"].ProviderName;
            string connectionstring = ConfigurationManager.ConnectionStrings["db_EventManager"].ConnectionString;

            DbConnection con = DbProviderFactories.GetFactory(provider).CreateConnection();
            con.ConnectionString = connectionstring;
            con.Open();

            DbCommand command = DbProviderFactories.GetFactory(provider).CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = con;

            DbParameter par = DbProviderFactories.GetFactory(provider).CreateParameter();
            par.ParameterName = "ID";
            par.Value = c.ID;
            command.Parameters.Add(par);

            command.CommandText = "DELETE FROM Contactperson WHERE ID = @ID";
            int affected = command.ExecuteNonQuery();

            con.Close();

            return affected;

        }


        public string Error
        {
            get { return null; }
        }

        public bool IsValid()
        {
            return Validator.TryValidateObject(this, new ValidationContext(this, null, null), null, true);
        }

        public string this[string columnName]
        {
            get
            {
                try
                {
                    object value = this.GetType().GetProperty(columnName).GetValue(this);
                    Validator.ValidateProperty(value, new ValidationContext(this, null, null) { MemberName = columnName });
                }
                catch (ValidationException ex)
                {
                    return ex.Message;
                }
                return String.Empty;
            }
        }
    }
}
