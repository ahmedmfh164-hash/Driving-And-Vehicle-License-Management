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
    public partial class frmIssueDrivingLicenseForTheFirstTime : Form
    {
       private clsLocalDrivingLicenseApplicationBusiness _LDLApp;
        private int _DLAppID;
        public frmIssueDrivingLicenseForTheFirstTime(int DLAppID)
        {
            InitializeComponent();
            _DLAppID= DLAppID;
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmIssueDrivingLicenseForTheFirstTime_Load(object sender, EventArgs e)
        {
            _LDLApp = clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID(_DLAppID);

            if (_LDLApp ==null)
            {

                MessageBox.Show("No Application with ID=" + _DLAppID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!_LDLApp.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LDLApp.GetActiveLicenseID();
            if (LicenseID !=-1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ucDrivingLicenseApplicationInfo1.GetDLAppID(_DLAppID);


        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            int LicenseID=_LDLApp.IssueLicenseForTheFirstTime(txtbNotes.Text, clsUserInfo.UserID);

              if (LicenseID!=-1)
                  MessageBox.Show("Data Saved Successfully with License ID = "+LicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
              else          
                  MessageBox.Show("Data Is not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
  

        }

        private void ucDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
