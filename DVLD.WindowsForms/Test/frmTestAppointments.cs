using DVLD.Business;
using Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties;
using Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Test;
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
using DVLD.Domain;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class frmTestAppointments : Form
    {
       private int _DLAppID;
        private clsTestTypes.enTestType _TestTypeID = clsTestTypes.enTestType.VisionTest;
        clsLocalDrivingLicenseApplicationBusiness License;
        public frmTestAppointments(int DLAppID,clsTestTypes.enTestType TestTypeID)
        {
            InitializeComponent();
            _DLAppID=DLAppID;
            _TestTypeID=TestTypeID;
            ucDrivingLicenseApplicationInfo1.GetDLAppID(_DLAppID);
            License=clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID( _DLAppID );

        }

        private void _RefreshData()
        {
            dgvAppointments.DataSource=clsTestAppointmentBusiness.GetApplicationTestAppointmentsPerTestType(_DLAppID, _TestTypeID);
                 lblCountRecords.Text=dgvAppointments.Rows.Count.ToString();

        }                                     

        private void frmScheduleVisionTest_Load(object sender, EventArgs e)
        {
            switch(_TestTypeID)
            {
                case clsTestTypes.enTestType.VisionTest:
                    pbMode.Image=Resources.Vision_512;
                    lblModeTest.Text="Vision Test Appointments";
                    break;
                case clsTestTypes.enTestType.WrittenTest:
                    pbMode.Image=Resources.Written_Test_512;
                    lblModeTest.Text="Written Test Appointments";
                    break;
                case clsTestTypes.enTestType.StreetTest:
                    pbMode.Image=Resources.driving_test_512;
                    lblModeTest.Text="Street Test Appointments";
                    break;

            }


            _RefreshData();
        }

        private void ucDrivingLicenseApplicationInfo1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _AddEditTestAppointment()
        {
            if (License.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already have an active appointment for this test, You cannot add new appointment", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsTestBusiness LastTest = License.GetLastTestPerTestType(_TestTypeID);

            if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_DLAppID, _TestTypeID);
                frm1.ShowDialog();
                return;
            }

            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, you can only retake failed test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest(_DLAppID, _TestTypeID);
            frm2.ShowDialog();

        }

        private void btnAddTest_Click(object sender, EventArgs e)
        {
            _AddEditTestAppointment();
            _RefreshData();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest(_DLAppID, _TestTypeID, (int)dgvAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            _RefreshData();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm= new frmTakeTest(_DLAppID, _TestTypeID, (int)dgvAppointments.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshData();
        }
    }
}
