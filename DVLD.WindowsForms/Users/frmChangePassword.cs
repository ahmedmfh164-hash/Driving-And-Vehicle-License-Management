using DVLD.Business;
using Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties;
using Guna.UI2.WinForms;
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
    public partial class frmChangePassword : Form
    {
        private clsUserBusiness _User;
        private int _UserID;

        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _User=clsUserBusiness.FindUserByUserID(UserID);
            ucUserInfo1.GetUserID(UserID);
            ucUserInfo1.LoadData();
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void tbCurrentPassword_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(tbCurrentPassword.Text)) || (string.IsNullOrEmpty(tbPassword.Text))
            ||string.IsNullOrEmpty(tbConfirmPassword.Text))
            {
                MessageBox.Show("Some fields were not entered!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(tbPassword.Text!=tbConfirmPassword.Text)
            {
                MessageBox.Show("Password Confirmation doesn't match the password!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User._Password=tbPassword.Text;

            if (_User.Save())
            {
                MessageBox.Show("Password Changed successfully.\nUserName: "+_User._UserName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Password doesn't Saved.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCurrentPassword.Text)&&(clsUserBusiness.FindUserByUserID(_UserID))._Password!=tbCurrentPassword.Text)
            {
                errorProvider1.SetError(tbCurrentPassword, "Current Password is wrong!");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbCurrentPassword, null);
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbPassword.Text)&&(tbPassword.Text==tbCurrentPassword.Text))
            {
                errorProvider1.SetError(tbPassword, "Enter a different password!");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbPassword, null);
        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbConfirmPassword.Text)&&tbConfirmPassword.Text!=tbPassword.Text)
            {
                errorProvider1.SetError(tbConfirmPassword, "Password Confirmation doesn't match the password!");
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(tbConfirmPassword, null);
        }

        private void ShowPassword(Guna2TextBox txtb,Button btn)
        {
            if (txtb.UseSystemPasswordChar)
            {
                txtb.UseSystemPasswordChar=false;
                btn.BackgroundImage=Resources.DisableShowTral2;
            }
            else
            {
                txtb.UseSystemPasswordChar=true;
                btn.BackgroundImage=Resources.EnableShowTeal1;

            }
        }

        private void btnShowCurrentPassword_Click(object sender, EventArgs e)
        {
            ShowPassword(tbCurrentPassword,(Button)sender);
        }

        private void btnShowNewPassword_Click(object sender, EventArgs e)
        {
            ShowPassword(tbPassword,(Button)sender);

        }

        private void btnShowConfirmPassword_Click(object sender, EventArgs e)
        {
            ShowPassword(tbConfirmPassword,(Button)sender);

        }

        private void ucUserInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
