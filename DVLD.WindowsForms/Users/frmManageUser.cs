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
    public partial class frmManageUser : Form
    {
        int _UserID;
        public frmManageUser()
        {
            InitializeComponent();
        }

        private void _RefreshUsers()
        {
            dgvUsers.DataSource=clsUserBusiness.GetAllUsers();

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText="User ID";
                dgvUsers.Columns[0].Width = 70;

                dgvUsers.Columns[1].HeaderText="Person ID";
                dgvUsers.Columns[1].Width = 70;

                dgvUsers.Columns[2].HeaderText="Full Name";
                dgvUsers.Columns[2].Width = 140;

                dgvUsers.Columns[3].HeaderText="User Name";
                dgvUsers.Columns[3].Width = 102;

                dgvUsers.Columns[4].HeaderText="Is Active";
                dgvUsers.Columns[4].Width = 115;

            }

            if (cbFilterBy.SelectedIndex!=5)
                cbFilterBy.SelectedIndex=0;

            lblCountRecords.Text=dgvUsers.Rows.Count.ToString();

        }

        private void frmManageUser_Load(object sender, EventArgs e)
        {
            _RefreshUsers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.SavedUser+=frmManageUser_Load;
            frm.ShowDialog();
            
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.SavedUser+=frmManageUser_Load;
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.SavedUser+=frmManageUser_Load;
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _UserID=(int)dgvUsers.CurrentRow.Cells[0].Value;

            if (_UserID!=clsUserInfo.UserID&&(clsUserInfo.Username!=dgvUsers.CurrentRow.Cells[3].Value.ToString()))
            {
                if (MessageBox.Show("Are you sure want to delete  user ["+_UserID+"]", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
                {
                    if (clsUserBusiness.DeleteUser(_UserID))
                        MessageBox.Show("User Deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed Deleting this User!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("User not delete!", "Cancel!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("User is not deleted due to data connected to it.","Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);

                _RefreshUsers();
        }

       
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterBy.Visible=false;

                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    txtFilterBy.Visible=true;
                    break;
                case 5:
                    cbIsActive.Visible=true;
                    cbIsActive.SelectedIndex = 0;
                    cbIsActive_SelectedIndexChanged(sender, e);
                    break;
                default:
                    break;


            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            if (dgvUsers.DataSource == null) return;

            DataView dv = ((DataTable)dgvUsers.DataSource).DefaultView;

            string Column = cbFilterBy.SelectedItem.ToString().Replace(" ", "");
            string Value = txtFilterBy.Text.Replace("'", "''");

            if (Column=="None")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text))
            {
                dv.RowFilter = "";
                return;
            }

            if (Column == "PersonID"||Column == "UserID")
            {
                if (int.TryParse(Value, out int PersonID))
                {


                    dv.RowFilter =$"{Column} =  {PersonID}";
                }
                else
                {
                    dv.RowFilter = $"{Column} = -1";
                }
            }
            else
            {

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
                    txbDigits_KeyPress(sender, e);

                    break;
                case 2:
                    txbDigits_KeyPress(sender, e);

                    break;
                case 3:
                    txbChar_KeyPress(sender, e);

                    break;
                case 4:
                    txbChar_KeyPress(sender, e);

                    break;
                case 5:
                    
                    break;

                default:
                    break;
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cbIsActiveLoadData()
        {
            if (dgvUsers.DataSource == null) return;

            DataView dv = ((DataTable)dgvUsers.DataSource).DefaultView;

            string Column = cbFilterBy.SelectedItem.ToString().Replace(" ", "");

            if (Column=="All")
            {
                return;
            }


            if (cbIsActive.Text == "Yes")
            {
                    dv.RowFilter =$"{Column} =  {true}";
            }
            else
            {
              
                dv.RowFilter = $"{Column} = {false}";
            }


        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(cbIsActive.SelectedIndex)
            {
                case 0:
                    _RefreshUsers();
                    break;
                    case 1:
                    case 2:
                    cbIsActiveLoadData();
                    break;
                default:
                    break;


            }

        }

        private void ChangePasswordToolStripMeueItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm=new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
