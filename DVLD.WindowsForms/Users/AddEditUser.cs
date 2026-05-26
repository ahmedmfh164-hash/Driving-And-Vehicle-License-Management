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
    public partial class frmAddEditUser : Form
    {
        private enum enMode { eAddNew,eUpdate};
       private enMode _Mode = enMode.eAddNew;

       private clsUserBusiness _User;
       private int _UserID;
        private int _PersonID;
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode=enMode.eUpdate;

            tcAddUser.SelectedIndex=1;
            btnBack.Visible=false; 
        }

        public frmAddEditUser()
        {                     
            InitializeComponent();
            _UserID = -1;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_PersonID<=0)
            {
                MessageBox.Show("Cannot go to next step before selecting person. ", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                tcAddUser.SelectedIndex=1;
                btnSave.Enabled=true;
            }
        }


        private void _LoadData()
        {
           
            if (_Mode==enMode.eAddNew)
            {
                _User=new clsUserBusiness();
                lblMode.Text="Add New User";
                return;
            }

            _User=clsUserBusiness.FindUserByUserID(_UserID);

            lblMode.Text="Update Data";
            lblUserID.Text=_UserID.ToString();
            tbUserName.Text=_User._UserName;
            tbPassword.Text=_User._Password.ToString();
            tbConfirmPassword.Text=tbPassword.Text;
            ckIsActive.Checked=_User._IsActive;


        }

        private void SaveDataToObject()
        {
            _User._PersonID=ucPersonInfoWithFilterBy1.GetPersonID;
            _User._UserName=tbUserName.Text;
            _User._Password=tbPassword.Text;
            _User._IsActive=ckIsActive.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(tbUserName.Text)) || (string.IsNullOrEmpty(tbPassword.Text))
              ||string.IsNullOrEmpty(tbConfirmPassword.Text))
            {
                MessageBox.Show("Some fields were not entered!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (_User._UserName!=tbUserName.Text&&clsUserBusiness.IsUserExistByUserName(tbUserName.Text))
            {
                MessageBox.Show("User Name is found!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

                    SaveDataToObject();

            if(_User.Save())
            {
                MessageBox.Show("User Saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblUserID.Text=_User._UserID.ToString();
                _Mode=enMode.eUpdate;
            }
            else
                MessageBox.Show("User doesn't Saved.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);



        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tbConfirmPassword_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void tbPassword_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void UserName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void tcAddUser_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if(MouseButtons==MouseButtons.Left)
            {
                e.Cancel = true;
            }

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            tcAddUser.SelectedIndex=0;
            btnSave.Enabled=true;
        }

        private void tbUserName_Validating(object sender, CancelEventArgs e)
        {
            if (tbUserName.Text==string.Empty)
            {
                errorProvider1.SetError(tbUserName, "This Field required!");
            }
            else
                errorProvider1.SetError(tbUserName, null);


            if (clsUserBusiness.IsUserExistByUserName(tbUserName.Text))
            {
                errorProvider1.SetError(tbUserName, "This User Name is found!Enter another!");
            }
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                errorProvider1.SetError(tbPassword, "This Field required!");
            }
            else
                errorProvider1.SetError(tbPassword, null);

        }

        private void tbConfirmPassword_Validating(object sender, CancelEventArgs e)
        {

            if (!string.IsNullOrEmpty(tbConfirmPassword.Text)&&tbConfirmPassword.Text!=tbPassword.Text)
                errorProvider1.SetError(tbConfirmPassword, "Password Confirmation doesn't match the password!");
            else
                errorProvider1.SetError(tbConfirmPassword, "This Field required!");
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _LoadData();

        }

        private void ShowPassword(Guna2TextBox txtb,Button btn)
        {
            if (txtb.UseSystemPasswordChar)
            {
                txtb.UseSystemPasswordChar=false;
                btn.BackgroundImage=Resources.DisableShowIcon;
            }
            else
            {
                txtb.UseSystemPasswordChar=true;
                btn.BackgroundImage=Resources.EnableShowIcon_jpeg;

            }
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            ShowPassword(tbPassword,(Button)sender);

        }

        private void btnShowConfirmPassword_Click(object sender, EventArgs e)
        {
            ShowPassword(tbConfirmPassword,(Button)sender);

        }

        private int ucPersonInfoWithFilterBy3_OnSearchClick(int arg)
        {
            _PersonID=arg;
            if (clsUserBusiness.IsUserExistByPersonID(_PersonID))
            {
                MessageBox.Show("User is found in system!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            return 1;
        }

    }
}
