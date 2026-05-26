using DVLD.Business;
using Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class UCPersonInfo : UserControl
    {
        clsPeopleBusiness _Person;
        int _PersonID;

        public UCPersonInfo()
        {
            InitializeComponent();
        }

        public int ReceivePersonID
        {
            get { return _PersonID; }
        }

        public void GetPersonID(int PersonID)
        {
            _Person=clsPeopleBusiness.FindPersonByPersonID(PersonID);
            LoadData();

        }

        public void GetNationalNo(string NationalNo)
        {
            _Person=clsPeopleBusiness.FindPersonByNationalNo(NationalNo);
            LoadData();

        }

        public void GetPhone(string Phone)
        {
            _Person=clsPeopleBusiness.FindPersonByPhone(Phone);
            LoadData();

        }

        public void GetEmail(string Email)
        {
            _Person=clsPeopleBusiness.FindPersonByEmail(Email);
            LoadData();

        }

        public void LoadData()
        {

            if (_Person == null)
            {
                MessageBox.Show("Person Not Found!", "Failed", MessageBoxButtons.OKCancel);
                return;
            }

            _PersonID=_Person._PersonID;

            lblPersonID.Text=_Person._PersonID.ToString();
            lblFullName.Text=_Person._FirstName+" "+_Person._SecondName+" "+_Person._ThirdName+" "+_Person._LastName;
            lblNationalNo.Text=_Person._NationalNo;
            lblGender.Text=_Person._Gender;
            lblEmail.Text=_Person._Email;
            lblPhone.Text=_Person._Phone;
            lblDateOfBirth.Text=_Person._DateOfBirth.ToShortDateString().ToString();
            lblAddress.Text=_Person._Address;

            var Country = clsCountriesBusiness.FindCountryByNationalCountryID(_Person._NationalityCountryID);
            if (Country!=null)
            {
                lblCountry.Text=Country._CountryName;
            }

            llblEditPersonInfo.Visible = true;

            if (_Person._ImagePath!="")
            {
                if (File.Exists(_Person._ImagePath))
                {
                    pbImage.Load(_Person._ImagePath);
                    return;
                }
            }

            if (_Person._Gender=="Male")
            {
                pbImage.Image=Resources.Male3;
            }
            else
                pbImage.Image= Resources.Female5;


        }

        private void UCShowDetails_Load(object sender, EventArgs e)
        {
           
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPerson frm = new frmAddEditPerson(_Person._PersonID);
            frm.ShowDialog();
        }

        private void UCPersonInfo_Load(object sender, EventArgs e)
        {
         
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
