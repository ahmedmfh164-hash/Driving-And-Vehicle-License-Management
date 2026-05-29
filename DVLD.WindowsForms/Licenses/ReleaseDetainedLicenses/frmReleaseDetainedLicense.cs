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
    public partial class frmReleaseDetainedLicense : Form
    {
        private clsLicenseBusiness _License;
        private int _ReleasedApplicationID;
        private int _LicenseID;
        public EventHandler ReleasedDetain;
        public frmReleaseDetainedLicense()
        {
            InitializeComponent();

        }

        public frmReleaseDetainedLicense(int LicenseID)
        {
            InitializeComponent();
            _LicenseID = LicenseID;

            txtbLicenseID.Text = LicenseID.ToString();

            LoadApplicationTypeData();
            LoadDataOnClickSearch();

            gbFilter.Enabled=false;
        }

        public void LoadDataOnClickSearch()
        {
            if (string.IsNullOrEmpty(txtbLicenseID.Text))
            {
                MessageBox.Show("There is no license,please enter a license id!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

            lblLicenseID.Text=_LicenseID.ToString();

            ucDriverLicenseInfo1.GetLicenseID(_LicenseID);



            if (_License.IsLicenseExpired())
            {
                MessageBox.Show("Selected license is expired ,it will expire.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!clsDetainedLicensesBusiness.IsLicenseDetainedByLicenseID(_LicenseID))
            {
                MessageBox.Show("Selected license is not Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblDetainID.Text=_License.DetainedInfo.DetainID.ToString();
            llblShowKicenseHistory.Enabled=true;
            btnRelease.Enabled =true;
            lblFineFees.Text=clsDetainedLicensesBusiness.FindByLicenseID(_LicenseID).FineFees.ToString();
            lblTotalFees.Text=(Convert.ToDouble(lblFineFees.Text)+Convert.ToDouble(lblApplicationFees.Text)).ToString();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _LicenseID=Convert.ToInt32(txtbLicenseID.Text);
            LoadDataOnClickSearch();
        }

        private void LoadApplicationTypeData()
        {
            clsApplicationTypesBusiness AppType = clsApplicationTypesBusiness.FindApplicationTypeByApplicationTypeID(5);

            lblDetainDate.Text=DateTime.Now.ToShortDateString();
            lblCreatedBy.Text=clsUserInfo.Username;
            lblApplcationDate.Text =DateTime.Now.ToShortDateString();
            lblApplicationFees.Text = AppType.ApplicationFees.ToString();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {

            if (_License.IsActive==false)
            {
                MessageBox.Show("Sorry,You cannot issue Renew because license is not active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to issue the license? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!=DialogResult.Yes)
                return;


            if (_License.ReleaseDetainedLicense(clsUserInfo.UserID, ref _ReleasedApplicationID))
            {
                MessageBox.Show("Detained Licensed Released Successfully.", "Detained License Released", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblApplicationID.Text = _ReleasedApplicationID.ToString();
                ReleasedDetain?.Invoke(this, EventArgs.Empty);
                llblShowLicenseInfo.Enabled=true;
                gbFilter.Enabled=false;
                btnRelease.Enabled=false;
            }
            else
                MessageBox.Show("Data Is not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(_License.LicenseID);
            frm.ShowDialog();

        }

        private void llblShowKicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(_License.DriverInfo.Person_ID);
            frm.ShowDialog();
        }

        private void frmReleasedDetainLicense_Load(object sender, EventArgs e)
        {
            if (gbFilter.Enabled)
            {
                LoadApplicationTypeData();
                llblShowKicenseHistory.Enabled=false;
                btnRelease.Enabled=false;
            }
            llblShowLicenseInfo.Enabled=false;
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
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
