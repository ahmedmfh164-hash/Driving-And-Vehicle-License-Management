using DVLD.Business;
using DVLD.Core;
using DVLD.Domain;
using Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DVLD.Business.clsTestTypesBusiness;
using static Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Test.frmScheduleTest;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class frmTakeTest : Form
    {
       

        private clsTestBusiness _Test;
        private clsTestTypesBusiness.enTestType _TestTypeID = clsTestTypesBusiness.enTestType.VisionTest;
        private clsLocalDrivingLicenseApplicationBusiness _LocalDrivingLicenseApplication;
        private int _LDLAppID = -1;
        private clsTestAppointmentBusiness _TestAppointment;
        private int _TestAppointmentID = -1;

        public EventHandler SavedData;

        public frmTakeTest(int LDLAppID,clsTestTypesBusiness.enTestType testTypeID, int TestAppointmentID = -1)
        {
            InitializeComponent();
            _LDLAppID= LDLAppID;
            _TestTypeID= testTypeID;
            _TestAppointmentID=TestAppointmentID;
            
                LoadInfo();

        }

        public void LoadInfo()
        {
            _TestAppointment =clsTestAppointmentBusiness.Find(_TestAppointmentID);

            _LocalDrivingLicenseApplication =  clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID(_LDLAppID);

            lblDLAppID.Text =_TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _LocalDrivingLicenseApplication.LicenseClass.ClassName;
            lblName.Text = _LocalDrivingLicenseApplication.ApplicantFullName;
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(_TestTypeID).ToString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            dtpDate.MinDate = DateTime.Now;

            if (_TestAppointment.IsLocked==false)
            {
                _Test=new clsTestBusiness();
                lblMessageResult.Visible= false;
            }
            else
            {

                _Test=clsTestBusiness.FindTestByTestAppointmentID(_TestAppointmentID);

                lblTestID.Text=_Test.TestID.ToString();
                txtbNotes.Text=_Test.Notes.ToString();

                if (_Test.TestResult)
                    rbPass.Checked=true;
                else
                    rbFail.Checked=true;

                rbPass.Enabled=false;
                rbFail.Enabled=false;

                dtpDate.Enabled=false;
                btnSave.Enabled=false;
                       txtbNotes.Enabled=false;

                lblMessageResult.Visible=true;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!rbPass.Checked&&!rbFail.Checked)
            {
                MessageBox.Show("Some fields are not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Test.TestAppointmentID=_TestAppointment.TestAppointmentID;

            _Test.Notes=txtbNotes.Text;
            _Test.CreatedByUserID=clsUserInfo.UserID;

            if (rbPass.Checked)
                _Test.TestResult=true;
            else
                _Test.TestResult=false;

           

            if (MessageBox.Show("Are you sure want to save? After that you cannot change the Pass/Fail results after you save?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                if (_Test.Save())
                {
                    MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   SavedData?.Invoke(this,EventArgs.Empty);
                }
                else
                    MessageBox.Show("Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
                MessageBox.Show("Data Is not Saved Successfully.", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();

        }

        private void gbTestType_Enter(object sender, EventArgs e)
        {

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            switch (_TestTypeID)
            {
                case clsTestTypesBusiness.enTestType.VisionTest:
                    ImageTestType.Image=Resources.Vision_512;
                    break;
                case clsTestTypesBusiness.enTestType.WrittenTest:
                    ImageTestType.Image=Resources.Written_Test_512;
                    break;
                case clsTestTypesBusiness.enTestType.StreetTest:
                    ImageTestType.Image=Resources.driving_test_512;
                    break;

            }


        }
    }
}
