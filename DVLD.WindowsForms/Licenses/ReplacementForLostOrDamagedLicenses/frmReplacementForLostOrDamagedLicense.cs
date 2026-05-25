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
    public partial class frmReplacementForLostOrDamagedLicense : Form
    {
        private clsLicenseBusiness _License;
        private clsLicenseBusiness _ReplacementLicense;
        private int _LicenseID;

        public frmReplacementForLostOrDamagedLicense()
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

          
            lblOldLicenseID.Text=_LicenseID.ToString();

            ucDriverLicenseInfo1.GetLicenseID(_LicenseID);

            llblShowKicenseHistory.Enabled=true;
            btnIssueReplacement.Enabled =true;
        }

        private void _LoadApplicationNewLicenseInfo()
        {
            clsApplicationTypesBusiness AppType = clsApplicationTypesBusiness.FindApplicationTypeByApplicationTypeID(3);

            lblApplcationDate.Text=DateTime.Now.ToShortDateString();
            lblApplicationFees.Text=   AppType.ApplicationFees.ToString();
            lblCreatedBy.Text=clsUserInfo.Username;

        }

        private void label1_Click(object sender, EventArgs e)
        {
                  
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtbLicenseID.Text))
            {
                MessageBox.Show("There is no license,please enter a license id!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_License.IsActive==false)
            {
                MessageBox.Show("Sorry,You cannot Replacement license because license is not active.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to Replacement the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!=DialogResult.Yes)
                return;

            clsLicenseBusiness.enIssueReason IssueReason = clsLicenseBusiness.enIssueReason.DamagedReplacement;

            if (rbLostLicense.Checked)
                IssueReason=clsLicenseBusiness.enIssueReason.LostReplacement;

            _ReplacementLicense = _License.Replace(IssueReason, clsUserInfo.UserID);


           if (_ReplacementLicense!=null)
             {
                 MessageBox.Show("Licensed Replaced Successfully with ID = "+_ReplacementLicense.LicenseID, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 lblReplacementLicenseID.Text=_ReplacementLicense.LicenseID.ToString();
                 lblLicenseReplacementApplicationID.Text=_ReplacementLicense.ApplicationID.ToString();
                 llblShowLicenseInfo.Enabled=true;
                 gbFilter.Enabled=false;
                 btnIssueReplacement.Enabled=false;
                 rbDamagedLicense.Checked=true;
             }
             else
                 MessageBox.Show("Data Is not Saved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void frmReplacementForLostOrDamagedLicense_Load(object sender, EventArgs e)
        {
            llblShowLicenseInfo.Enabled=false;
            llblShowKicenseHistory.Enabled=false;
            btnIssueReplacement.Enabled=false;
            rbDamagedLicense.Checked=true;

            _LoadApplicationNewLicenseInfo();

        }


        private void llblShowKicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory(_License.DriverInfo.Person_ID);
            frm.ShowDialog();
        }

        private void llblShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo(_ReplacementLicense.LicenseID);
            frm.ShowDialog();

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
