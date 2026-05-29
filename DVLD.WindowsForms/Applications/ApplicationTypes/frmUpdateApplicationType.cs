using DVLD.Business;
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
    public partial class frmUpdateApplicationType : Form
    {
        int _ID;
        clsApplicationTypesBusiness _ApplicationType;
        public EventHandler SavedChanging;

        public frmUpdateApplicationType(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             if(string.IsNullOrEmpty(txtbTitle.Text)||string.IsNullOrEmpty(texbFees.Text))
            {
                MessageBox.Show("Some fields are not valid! put the mouse over the red icon(s) to see the error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

             _ApplicationType.ApplicationTypeTitle = txtbTitle.Text;

            if(double.TryParse(texbFees.Text,out double ApplicationFees))
                _ApplicationType.ApplicationFees=ApplicationFees;

            if (_ApplicationType.Save())
            {
                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
               SavedChanging?.Invoke(this, EventArgs.Empty);
            }
            else
                MessageBox.Show("Error: Data is not saved successfully!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txbChar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
      !char.IsWhiteSpace(e.KeyChar) &&
      !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txbDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            Guna2TextBox txt = (Guna2TextBox)sender;

            if (!char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }


        private void texbTitle_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbTitle.Text))
            {
                errorProvider1.SetError(txtbTitle, "This Field required!");
            }
            else
            {
                errorProvider1.SetError(txtbTitle, "");
            }
        }

        private void texbFees_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(texbFees.Text))
            {
                errorProvider1.SetError(texbFees, "This Field required!");
            }
            else
            {
                errorProvider1.SetError(texbFees, "");
            }
        }

        private void txtbTitle_KeyPress(object sender, KeyPressEventArgs e)
        {

          //  txbChar_KeyPress(sender, e);
        }

        private void texbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            txbDigits_KeyPress(sender, e);
        }

        private void _LoadData()
        {


            _ApplicationType=clsApplicationTypesBusiness.FindApplicationTypeByApplicationTypeID(_ID);

            if (_ApplicationType==null)
            {
                MessageBox.Show("Application Type object is not initialized!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblID.Text= _ID.ToString();
            txtbTitle.Text=_ApplicationType.ApplicationTypeTitle;
            texbFees.Text=_ApplicationType.ApplicationFees.ToString();
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            _LoadData();

        }
    }
}
