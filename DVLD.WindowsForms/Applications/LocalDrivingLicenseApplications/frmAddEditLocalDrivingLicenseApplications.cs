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
    public partial class frmAddEditLocalDrivingLicenseApplications : Form
    {
        private enum enMode { eAddNew, eUpdate };
        enMode _Mode = enMode.eAddNew;

        clsLocalDrivingLicenseApplicationBusiness _LDLApp;
        int _LDLAppID;
        bool allowChange = false;
        clsLicenseClassBusiness _LicenseClass;

        public frmAddEditLocalDrivingLicenseApplications(int LDLAppID)
        {
            InitializeComponent(); 
            _LDLAppID = LDLAppID;
            _Mode=enMode.eUpdate;

            allowChange = true;
            tcNewLocalLicenseApp.SelectedIndex=1;
            allowChange=false;
            btnBack.Visible=false;
        }

        public frmAddEditLocalDrivingLicenseApplications()
        {
            InitializeComponent();
            _LDLAppID = -1;
            _Mode=enMode.eAddNew;
        }

        private void _LoadData()
        {
            cbLicenseCLass.SelectedIndex=0;
            lblApplicationDate.Text=DateTime.Now.ToString();
            lblCreatedBy.Text=clsUserInfo.Username;

            if (_Mode==enMode.eAddNew)
            {
                _LDLApp=new clsLocalDrivingLicenseApplicationBusiness();
                lblMode.Text="New Local Driving License Application";
                return;
            }

            _LDLApp=clsLocalDrivingLicenseApplicationBusiness.FindByLocalDrivingAppLicenseID(_LDLAppID);

            lblMode.Text="Update Data";
            lblDLApplicationID.Text=_LDLAppID.ToString();
            lblApplicationDate.Text=_LDLApp.ApplicationDate.ToString();

            cbLicenseCLass.SelectedIndex=cbLicenseCLass.FindString(_LDLApp.LicenseClass.ClassName.ToString());


        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            allowChange = true;
            tcNewLocalLicenseApp.SelectedIndex=1;
            btnSave.Enabled=true;
            allowChange = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            int LicenseClassID = clsLicenseClassBusiness.Find(cbLicenseCLass.Text).LicenseClassID;


            int ActiveApplicationID = clsApplicationBusiness.GetActiveApplicationIDForLicenseClass(ucPersonInfoWithFilterBy1.GetPersonID(), clsApplicationBusiness.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseCLass.Focus();
                return;
            }


            if (clsLicenseBusiness.IsLicenseExistByPersonID(ucPersonInfoWithFilterBy1.GetPersonID(), LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose different driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LDLApp.ApplicantPersonID = ucPersonInfoWithFilterBy1.GetPersonID();
            _LDLApp.ApplicationTypeID = 1;
            _LDLApp.ApplicationStatus = clsApplicationBusiness.enApplicationStatus.New;
            _LDLApp.LastStatusDate = DateTime.Now;
            _LDLApp.PaidFees = Convert.ToDouble(lblFees.Text);
            _LDLApp.LicenseClassID= LicenseClassID;
            _LDLApp.ApplicationDate=DateTime.Now;

            _LDLApp.CreatedByUserID=clsUserInfo.UserID;

          
            if (_LDLApp.SaveLDLApp())
            {
                MessageBox.Show("Data Saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDLApplicationID.Text=_LDLApp.LocalDrivingLicenseApplicationID.ToString();
                _Mode=enMode.eUpdate;
            }
            else
                MessageBox.Show("Data doesn't Saved.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            allowChange = true;
            tcNewLocalLicenseApp.SelectedIndex=0;
            btnSave.Enabled=true;
            allowChange = false;
        }

      

        private void frmAddEditLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _LicenseClass= clsLicenseClassBusiness.Find(cbLicenseCLass.Text);

                         lblFees.Text=_LicenseClass.ClassFees.ToString();

        }
    }
}
