using DVLD.Business;
using Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class UCDriverLicenseInfo : UserControl
    {
        private int _LicenseID;
        public UCDriverLicenseInfo()
        {
            InitializeComponent();
        }


        public void GetLicenseID(int LicenseID)
        {
            _LicenseID = LicenseID;
                   _LoadData();
        }

        private void _LoadData()
        {            
            clsLicenseBusiness License = clsLicenseBusiness.FindByLicenseID(_LicenseID);
                         
            if (License==null)
            {
                MessageBox.Show("License not found!");
                return;                                      
            }

            clsPeopleBusiness Person = clsPeopleBusiness.FindPersonByPersonID(License.DriverInfo.Person_ID);

            lblClass.Text=License.LicenseClassInfo.ClassName;
            lblFullName.Text=License.DriverInfo.Full_Name;
            lblLicenseID.Text=License.LicenseID.ToString();
            lblNationalNo.Text=License.DriverInfo.National_No;
            lblDriverID.Text=License.DriverID.ToString();
            lblExpirationDate.Text=License.ExpirationDate.ToShortDateString();
            lblIssueDate.Text=License.IssueDate.ToShortDateString();
            lblIssueReason.Text=License.IssueReason.ToString();
            lblGendor.Text=Person._Gender.ToString();
            lblDateOfBirth.Text=Person._DateOfBirth.ToShortDateString();

            if (License.IsActive)
                lblIsActive.Text="Yes";
            else
                lblIsActive.Text="No";

            if (clsDetainedLicensesBusiness.IsLicenseDetainedByLicenseID(_LicenseID))
                lblIsDetained.Text="Yes";
            else
                lblIsDetained.Text="No";

            if (License.Notes=="")
                lblNotes.Text="No Notes";
            else
                lblNotes.Text=License.Notes.ToString();


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

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void UCDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            



        }
    }
}
