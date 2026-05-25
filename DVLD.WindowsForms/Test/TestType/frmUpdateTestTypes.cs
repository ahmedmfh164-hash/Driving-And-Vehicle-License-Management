using DVLD.Business;
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
    public partial class frmUpdateTestTypes : Form
    {
        int _ID;
        clsTestTypesBusiness _TestType;
        public frmUpdateTestTypes(int ID)
        {
            InitializeComponent();
            _ID = ID;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtbTitle.Text)||string.IsNullOrEmpty(texbFees.Text)||string.IsNullOrEmpty(txtbDiscription.Text))
            {
                MessageBox.Show("Some fields are not valid! put the mouse over the red icon(s) to see the error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.TestTypeTitle = txtbTitle.Text;

            _TestType.TestTypeDescription=txtbDiscription.Text;

            if (double.TryParse(texbFees.Text, out double TestFees))
                _TestType.TestFees=TestFees;

            if (_TestType.Save())
            {
                MessageBox.Show("Data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data is not saved successfully!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = (TextBox)sender;

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
                errorProvider1.SetError(Temp, null);
        }

        private void txbMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;

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

        private void texbFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            txbMoney_KeyPress(sender, e);
        }

        private void _LoadData()
        {


            _TestType=clsTestTypesBusiness.FindTestTypeByTestTypeID((clsTestTypesBusiness.enTestType)_ID);

            if (_TestType==null)
            {
                MessageBox.Show("Test Type object is not initialized!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblID.Text= _ID.ToString();
            txtbTitle.Text=_TestType.TestTypeTitle;
            txtbDiscription.Text=_TestType.TestTypeDescription;
            texbFees.Text=_TestType.TestFees.ToString();
        }

        private void frmUpdateTestTypes_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void txtbTitle_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);

        }

        private void txtbDiscription_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void texbFees_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void texbFees_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
