using DVLD.Business;
using DVLD.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class UCDrivingLicenseApplicationInfo : UserControl
    {
        int _DLAppID;
        clsLocalDrivingLicenseApplicationBusiness License;
        public UCDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        public void GetDLAppID(int DLAppID)
        {
            _DLAppID = DLAppID;
            LoadData();
        }

        private void LoadData()
        {
            License = clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID(_DLAppID);

            if (License == null)
            {
                MessageBox.Show("License cannot be null!");
                return;
            }
            
            lblDLAppID.Text=_DLAppID.ToString();

            lblAppliedForLicense.Text=License.LicenseClass.ClassName;

            lblPassedTests.Text=License.GetPassedTestCount()+"/3";

               
            lblID.Text=License.ApplicationID.ToString();
            lblFees.Text=License.PaidFees.ToString();
            lblStatus.Text=License.StatusText;

           if(License.ApplicationTypeInfo==null)
            License.ApplicationTypeInfo=clsApplicationTypesBusiness.FindApplicationTypeByApplicationTypeID(License.ApplicationTypeID);
           
            lblType.Text=License.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicant.Text=License.ApplicantFullName;
            lblDate.Text=License.ApplicationDate.ToString();
            lblStatusDate.Text=License.LastStatusDate.ToString();
            lblCreatedBy.Text=clsUserInfo.Username;

        }

        private void UCDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void llblViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDetailsPerson frm = new frmShowDetailsPerson(License.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
