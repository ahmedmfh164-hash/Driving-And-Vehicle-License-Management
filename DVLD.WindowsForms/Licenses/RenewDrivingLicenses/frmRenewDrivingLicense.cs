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
    public partial class frmRenewDrivingLicense : Form
    {
        private clsLicenseBusiness _License;
        private clsLicenseBusiness _RenewLicense;
        private int _LicenseID;
        public frmRenewDrivingLicense()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {                               
            _LicenseID=Convert.ToInt32(txtbLicenseID.Text);

            if (_LicenseID<=0)
            {
                errorProvider1.SetError(txtbLicenseID, "Value invalid!");
                return;
            }


            _License=clsLicenseBusiness.FindByLicenseID(_LicenseID);

            if (_License==null)
            {
                MessageBox.Show("License is not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_License.IsLicenseExpired())
            {
                MessageBox.Show("Selected license is not yet expired ,it will expire on: "+_License.ExpirationDate, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblOldLicenseID.Text=_LicenseID.ToString();
            lblLicenseFees.Text=_License.PaidFees.ToString();
            lblTotalFees.Text=(Convert.ToDouble(lblLicenseFees.Text)+Convert.ToDouble(lblApplicationFees.Text)).ToString();

            ucDriverLicenseInfo1.GetLicenseID(_LicenseID);

            llblShowKicenseHistory.Enabled=true;
            btnRenew.Enabled =true;
        }

        private void _LoadApplicationNewLicenseInfo()
        {
            clsApplicationTypesBusiness AppType = clsApplicationTypesBusiness.FindApplicationTypeByApplicationTypeID(2);

            lblApplcationDate.Text=DateTime.Now.ToShortDateString();
            lblIssueDate.Text=DateTime.Now.ToShortDateString();
            lblExpirationDate.Text=DateTime.Now.AddYears(1).ToShortDateString();
            lblApplicationFees.Text=   AppType.ApplicationFees.ToString();
            lblCreatedBy.Text=clsUserInfo.Username;

        }


        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbLicenseID.Text))
            {
                MessageBox.Show("There is no license,please enter a license id!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_License.IsActive==false)
            {
                MessageBox.Show("Sorry,You cannot issue Renew because license is not active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!=DialogResult.Yes)
                return;

            _RenewLicense = _License.RenewLicense(txtbNotes.Text, clsUserInfo.UserID);

            if (_RenewLicense!=null)
            {
                MessageBox.Show("Licensed Renewed Successfully with ID = "+_RenewLicense.LicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblRenewedLicenseID.Text=_RenewLicense.LicenseID.ToString();
                lblRenewLicenseApplicationID.Text=_RenewLicense.ApplicationID.ToString();
                llblShowLicenseInfo.Enabled=true;
                gbFilter.Enabled=false;
                btnRenew.Enabled=false;
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
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(_RenewLicense.LicenseID);
            frm.ShowDialog();
        }

        private void frmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            llblShowLicenseInfo.Enabled=false;
            llblShowKicenseHistory.Enabled=false;
            btnRenew.Enabled=false;

            _LoadApplicationNewLicenseInfo();
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
