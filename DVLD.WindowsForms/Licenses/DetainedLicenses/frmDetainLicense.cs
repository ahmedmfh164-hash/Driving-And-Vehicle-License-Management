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
    public partial class frmDetainLicense : Form
    {
        private clsLicenseBusiness _License;
        private int _DetainLicenseID;
        private int _LicenseID;
        public EventHandler DetainLicense;

        public frmDetainLicense()
        {
            InitializeComponent();
        }
     

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbLicenseID.Text))
            {
                MessageBox.Show("There is no license,please enter a license id!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

            if (_License.IsLicenseExpired())
            {
                MessageBox.Show("Selected license is expired ,it will expire.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsDetainedLicensesBusiness.IsLicenseDetainedByLicenseID(_LicenseID))
            {
                MessageBox.Show("Selected license already is Detained.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblLicenseID.Text=_LicenseID.ToString();

            ucDriverLicenseInfo1.GetLicenseID(_LicenseID);

            llblShowKicenseHistory.Enabled=true;
            btnDetain.Enabled =true;


        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
           
            if (_License.IsActive==false)
            {
                MessageBox.Show("Sorry,You cannot issue Renew because license is not active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

         

            if(string.IsNullOrEmpty(txtbFineFees.Text))
            {
                errorProvider1.SetError(txtbFineFees, "You should enter fine fees.");
                return;
            }

            if (MessageBox.Show("Are you sure you want to issue the license? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!=DialogResult.Yes)
                return;

            _DetainLicenseID = _License.Detain( Convert.ToSingle(txtbFineFees.Text),clsUserInfo.UserID);

            if (_DetainLicenseID!=-1)
            {
                MessageBox.Show("Licensed Detained Successfully with ID = "+_DetainLicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDetainID.Text=_DetainLicenseID.ToString();
                DetainLicense?.Invoke(this, EventArgs.Empty);
                llblShowLicenseInfo.Enabled=true;
                gbFilter.Enabled=false;
                btnDetain.Enabled=false;
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

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {

            llblShowLicenseInfo.Enabled=false;
            llblShowKicenseHistory.Enabled=false;
            btnDetain.Enabled=false;
            lblDetainDate.Text=DateTime.Now.ToShortDateString();
            lblCreatedBy.Text=clsUserInfo.Username;
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
