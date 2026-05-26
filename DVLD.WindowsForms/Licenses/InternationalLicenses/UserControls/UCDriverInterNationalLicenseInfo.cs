using DVLD.Business;
using Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class UCDriverInterNationalLicenseInfo : UserControl
    {
        private int _IntLicenseID;

        public UCDriverInterNationalLicenseInfo()
        {
            InitializeComponent();
        }

        public void GetIntLicenseID(int IntLicenseID)
        {
            _IntLicenseID = IntLicenseID;
            _LoadData();
        }

        private void _LoadData()
        {
            clsInternationalBusiness IntLicense = clsInternationalBusiness.FindByInterNationalLicenseID(_IntLicenseID);

            if (IntLicense==null)
            {
                MessageBox.Show("License not found!");
                return;
            }


            clsPeopleBusiness Person = clsPeopleBusiness.FindPersonByPersonID(IntLicense.ApplicantPersonID);

            lblInternationalLicenseID.Text=_IntLicenseID.ToString();
            lblApplicationID.Text=IntLicense.ApplicationID.ToString();
            lblFullName.Text=Person.FullName.ToString();
            lblLocalLicenseID.Text=IntLicense.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text=Person._NationalNo;
            lblDriverID.Text=IntLicense.DriverID.ToString();
            lblExpirationDate.Text=IntLicense.ExpirationDate.ToShortDateString();

            if (IntLicense.IsActive)
                lblIsActive.Text="Yes";
            else
                lblIsActive.Text="No";

                lblIssueDate.Text=IntLicense.IssueDate.ToShortDateString();
            lblGendor.Text=Person._Gender.ToString();
            lblDateOfBirth.Text=Person._DateOfBirth.ToShortDateString();

            if (string.IsNullOrEmpty(Person._ImagePath)||!File.Exists(Person._ImagePath))
            {
                if (Person._Gender.ToLower()=="male")
                    pbPersonImage.Image=Resources.Male11;
                else
                    pbPersonImage.Image = Resources.Female1;
            }
            else
                pbPersonImage.ImageLocation=Person._ImagePath;


        }


        private void UCDriverInterNationalLicenseInfo_Load(object sender, EventArgs e)
        {



        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
