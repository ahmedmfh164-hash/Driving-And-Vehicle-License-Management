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
    public partial class frmIssueInternationalLicenseApplication : Form
    {
       private clsInternationalBusiness _IntLicense;
       private clsLicenseBusiness _License;
        private int _LicenseID;
        public EventHandler IssuedLicense;
        public frmIssueInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            llblShowLicenseInfo.Enabled=false;
            llblShowKicenseHistory.Enabled=false;
            btnIssue.Enabled=false;
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
             _LicenseID=Convert.ToInt32(txtbLicenseID.Text);

            if(_LicenseID<=0)
            {
                errorProvider1.SetError(txtbLicenseID,"Value invalid!");
                return;
            }


            _License=clsLicenseBusiness.FindByLicenseID(_LicenseID);

            if (_License==null)
            {
                MessageBox.Show("License is not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int IntLicenseID=clsInternationalBusiness.IsIntLicenseExistByLicenseID(_LicenseID);

            if(IntLicenseID!=-1)
            {
                MessageBox.Show("Person already have an active international license with id = "+IntLicenseID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ucNewInterNationalLicense1.GetLicenseID(_LicenseID);

            llblShowKicenseHistory.Enabled=true;
            btnIssue.Enabled =true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if(string.IsNullOrEmpty(txtbLicenseID.Text))
            {
                MessageBox.Show("There is no license,please enter a license id!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_License.IsActive==false)
            {
                MessageBox.Show("Sorry,You cannot issue license because license is not active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!=DialogResult.Yes)
                return;

            _IntLicense=new clsInternationalBusiness();
            _IntLicense.ApplicantPersonID=_License.DriverInfo.Person_ID;
            _IntLicense.ApplicantFullName=_License.DriverInfo.Full_Name;
            _IntLicense.ApplicationID=_License.ApplicationID;
            _IntLicense.ApplicationDate=DateTime.Now;
            _IntLicense.ApplicationTypeID=6;
            _IntLicense.DriverID=_License.DriverID;
            _IntLicense.ExpirationDate=DateTime.Now.AddYears(1);
            _IntLicense.IssuedUsingLocalLicenseID=_License.LicenseID;
            _IntLicense.PaidFees=clsApplicationTypesBusiness.FindApplicationTypeByApplicationTypeID(_IntLicense.ApplicationTypeID).ApplicationFees;
            _IntLicense.IsActive=true;
            _IntLicense.ApplicationStatus=DVLD.Domain.clsApplications.enApplicationStatus.Completed;
            _IntLicense.CreatedByUserID=clsUserInfo.UserID;


            if (_IntLicense.Save())
            {
                MessageBox.Show("Data Saved Successfully with InterNational License ID = "+_IntLicense.InterNationalLicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ucNewInterNationalLicense1.GetApplicationIDAndIntLicenseID(_IntLicense.InterNationalLicenseID,_IntLicense.ApplicationID);
                 IssuedLicense?.Invoke(this,EventArgs.Empty);
                llblShowLicenseInfo.Enabled=true;
                gbFilter.Enabled=false;
                btnIssue.Enabled=false;
            }
            else
                MessageBox.Show("Data Is not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

          

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llblShowKicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {                                  
            frmShowLicenseHistory frm = new frmShowLicenseHistory(_License.DriverInfo.Person_ID);
            frm.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInterNationalLicenseInfo frm = new frmShowInterNationalLicenseInfo(_IntLicense.InterNationalLicenseID);
            frm.ShowDialog();

        }

        private void gbFilter_Enter(object sender, EventArgs e)
        {

        }

        private void txbDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txtbLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            txbDigits_KeyPress(sender, e);
        }
    }
}
