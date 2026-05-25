using DVLD.Business;
using DVLD.Core;
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

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class frmLogin : Form
    {
        clsUserBusiness _user;
        
        public frmLogin()
        {
            InitializeComponent();

        }

        public void LoadData()
        {
            clsUserInfo.LoadLoginData();
            if (clsUserInfo.isRememberMe)
            {
                tbUserName.Text=clsUserInfo.Username;
               tbPassword.Text=clsUserInfo.Password;
                ckRememberMe.Checked=true;
            }
            else
                ckRememberMe.Checked= false;
           

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
               LoadData();
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {


        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void ckRememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void _SaveLoginData()
        {
                clsUserInfo.SaveLoginData(tbUserName.Text.Trim(),tbPassword.Text.Trim(), ckRememberMe.Checked);
            clsUserInfo.UserID=_user._UserID;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbUserName.Text)||string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("Some fields were not entered!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _user=clsUserBusiness.FindUserByUserName(tbUserName.Text.Trim());

            if (_user==null)
            {
                MessageBox.Show("Invalid Username/Password!","Wrong Credentials",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            if (!_user._IsActive)
            {
                MessageBox.Show("Cannot this user enter the system!", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_user._Password!=tbPassword.Text)
            {
                MessageBox.Show("Invalid Username/Password!", "Wrong Credentials", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _SaveLoginData();

            frmMain frm = new frmMain(_user._UserID);
            frm.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbPassword.UseSystemPasswordChar)
            {
                tbPassword.UseSystemPasswordChar=false;
                btnShowPassword.BackgroundImage=Resources.DisableShow1;
            }
            else
            {
                tbPassword.UseSystemPasswordChar=true;
                btnShowPassword.BackgroundImage=Resources.EnableShow3;

            }
        }
    }

}
