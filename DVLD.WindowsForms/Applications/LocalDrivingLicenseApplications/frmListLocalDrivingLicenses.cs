using DVLD.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Domain;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class frmListLocalDrivingLicenses : Form
    {
        DataTable _dtAllLocalDrivingLicense;
        private clsLocalDrivingLicenseApplicationBusiness _LDApp;
        bool IsIssue = false;
        public frmListLocalDrivingLicenses()
        {
            InitializeComponent();
        }

        private void _RefreshLocalDrivingLicense()
        {
            dgvLocalDrivingLicenseApplication.DataSource=clsLocalDrivingLicenseApplicationBusiness.GetAllLocalDrivingLicenseApplications();

            _dtAllLocalDrivingLicense=(DataTable)dgvLocalDrivingLicenseApplication.DataSource;

            if (dgvLocalDrivingLicenseApplication.Rows.Count > 0)
            {
                dgvLocalDrivingLicenseApplication.Columns[0].HeaderText="L.D.L.AppID";
                dgvLocalDrivingLicenseApplication.Columns[0].Width = 50;

                dgvLocalDrivingLicenseApplication.Columns[1].HeaderText="Driving Class";
                dgvLocalDrivingLicenseApplication.Columns[1].Width = 110;

                dgvLocalDrivingLicenseApplication.Columns[2].HeaderText="National No.";
                dgvLocalDrivingLicenseApplication.Columns[2].Width = 45;

                dgvLocalDrivingLicenseApplication.Columns[3].HeaderText="Full Name";
                dgvLocalDrivingLicenseApplication.Columns[3].Width = 100;

                dgvLocalDrivingLicenseApplication.Columns[4].HeaderText="Application Date";
                dgvLocalDrivingLicenseApplication.Columns[4].Width = 80;

                dgvLocalDrivingLicenseApplication.Columns[5].HeaderText="Passed Tests";
                dgvLocalDrivingLicenseApplication.Columns[5].Width = 45;

                dgvLocalDrivingLicenseApplication.Columns[6].HeaderText="Status";
                dgvLocalDrivingLicenseApplication.Columns[6].Width = 70;


            }

            cbFilterBy.SelectedIndex=cbFilterBy.FindString("None");
            lblCountRecords.Text=dgvLocalDrivingLicenseApplication.Rows.Count.ToString();



        }


        private void frmListLocalDrivingLicenses_Load(object sender, EventArgs e)
        {
            _RefreshLocalDrivingLicense();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterBy.Visible=false;
                    _RefreshLocalDrivingLicense();
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    txtFilterBy.Visible=true;
                    break;

            }

        }


        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            if (_dtAllLocalDrivingLicense == null) return;

            DataView dv = _dtAllLocalDrivingLicense.DefaultView;

            string Column = cbFilterBy.SelectedItem.ToString().Replace(" ", "");
            string Value = txtFilterBy.Text.Replace("'", "''");

            if (Column=="None")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(Value))
            {
                dv.RowFilter = "";
                return;
            }

            if (Column == "L.D.L.AppID"||Column=="PassedTests")
            {
                if (cbFilterBy.Text=="L.D.L.AppID")
                    Column="LocalDrivingLicenseApplicationID";
                if (cbFilterBy.Text=="Passed Tests")
                    Column="PassedTestCount";

                if (int.TryParse(Value, out int LDLAppID))
                {

                    dv.RowFilter =$"{Column} =  {LDLAppID}";
                }
                else
                {
                    dv.RowFilter = $"{Column} = -1";
                }
            }
            else
            {
                if (cbFilterBy.Text=="National No.")
                    Column="NationalNo";
                if (cbFilterBy.Text=="Full Name")
                    Column="FullName";


                dv.RowFilter = $"{Column} LIKE '{Value}%'";
            }


        }

        private void txbChar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txbDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }


        private void txbCharOrDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {

                case 0:

                    break;
                case 1:
                case 4:
                    txbDigits_KeyPress(sender, e);
                    break;
                case 2:
                    txbCharOrDigit_KeyPress(sender, e);
                    break;
                case 5:
                case 3:
                    txbChar_KeyPress(sender, e);
                    break;


            }

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDrivingLicenseApplications frm = new frmAddEditLocalDrivingLicenseApplications();
            frm.ShowDialog();
            _RefreshLocalDrivingLicense();

        }


        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure want to cancel this application?", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
            {
                clsLocalDrivingLicenseApplicationBusiness License =
          clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);

                if (License == null)
                {
                    MessageBox.Show("Application is not found.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                License.ApplicationStatus=clsApplicationBusiness.enApplicationStatus.Cancelled;

                if (License.SaveLDLApp())
                    MessageBox.Show("Application Cancelled successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Application not cancelled.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _RefreshLocalDrivingLicense();
            }


        }

        private void _ScheduleTest(clsTestTypes.enTestType TestTypeID)
        {
            frmTestAppointments frm = new frmTestAppointments((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value,TestTypeID);
            frm.ShowDialog();
            _RefreshLocalDrivingLicense();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.VisionTest);

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.WrittenTest);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ScheduleTest(clsTestTypes.enTestType.StreetTest);
        }

        private void dgvLocalDrivingLicenseApplication_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {

            if (e.RowIndex < 0)
            {
                dgvLocalDrivingLicenseApplication.ContextMenuStrip = null;
                return;
            }

            if (e.Button != MouseButtons.Right)
                return;

            dgvLocalDrivingLicenseApplication.ClearSelection();
            dgvLocalDrivingLicenseApplication.Rows[e.RowIndex].Selected = true;
            dgvLocalDrivingLicenseApplication.CurrentCell =
                dgvLocalDrivingLicenseApplication.Rows[e.RowIndex].Cells[0];

            dgvLocalDrivingLicenseApplication.ContextMenuStrip = contextMenuStrip1;

            int PassedTests =
                Convert.ToInt32(dgvLocalDrivingLicenseApplication.Rows[e.RowIndex].Cells[5].Value);


            editApplicationToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelToolStripMenuItem.Enabled = true;

            bool CanIssueLicense = PassedTests >= 3;

            if ((string)dgvLocalDrivingLicenseApplication.Rows[e.RowIndex].Cells[6].Value!="Completed")
            {
                showLicenseToolStripMenuItem.Enabled = false;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled =  CanIssueLicense;
            }
            else
            {
                showLicenseToolStripMenuItem.Enabled=CanIssueLicense;
                issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled =false;
                editApplicationToolStripMenuItem.Enabled = false;
                deleteApplicationToolStripMenuItem.Enabled = false;
                cancelToolStripMenuItem.Enabled = false;
            }

            ScehedueTestsToolMenue.Enabled = true;

            scheduleVisionTestToolStripMenuItem.Enabled = false;
            scheduleWrittenTestToolStripMenuItem.Enabled = false;
            scheduleStreetTestToolStripMenuItem.Enabled = false;

            switch (PassedTests)
            {
                case 0:
                    scheduleVisionTestToolStripMenuItem.Enabled = true;
                    break;

                case 1:
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;
                    break;

                case 2:
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                    break;

                case 3:
                    ScehedueTestsToolMenue.Enabled = false;
                    break;
            }


        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _LDApp=clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);


                if ((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[5].Value==0)
            {
                if (MessageBox.Show("Are you sure want to delete this Application ["+_LDApp.ApplicationID+"]", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
                {
                    if (clsApplicationBusiness.DeleteApplication(_LDApp.ApplicationID))
                        MessageBox.Show("Application Deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed Deleting this application!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Application not delete!", "Cancel!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Cannot delete this application connected with tests!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            _RefreshLocalDrivingLicense();



        }

        private void ScehedueTestsToolMenue_Click(object sender, EventArgs e)
        {


        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDrivingLicenseForTheFirstTime frm = new frmIssueDrivingLicenseForTheFirstTime((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            IsIssue=true;
            _RefreshLocalDrivingLicense();

        }                       


        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value).GetActiveLicenseID();
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplicationBusiness L = clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID((int)dgvLocalDrivingLicenseApplication.CurrentRow.Cells[0].Value);
            frmShowLicenseHistory frm = new frmShowLicenseHistory(L.ApplicantPersonID);
            frm.ShowDialog();

        }
    }
}
