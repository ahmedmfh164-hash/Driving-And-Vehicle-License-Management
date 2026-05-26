using DVLD.Business;
using DVLD.Domain;
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
    public partial class frmInterNationalLicenseApplications : Form
    {
       private DataView dv;

        public frmInterNationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void _RefreshData()
        {
            dgvInternationalLicenses.DataSource=clsInternationalBusiness.GetAllInterNationalLicenses();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText="Int License ID";
                dgvInternationalLicenses.Columns[0].Width = 100;

                dgvInternationalLicenses.Columns[1].HeaderText="Application ID";
                dgvInternationalLicenses.Columns[1].Width = 100;

                dgvInternationalLicenses.Columns[2].HeaderText="Driver ID";
                dgvInternationalLicenses.Columns[2].Width = 100;

                dgvInternationalLicenses.Columns[3].HeaderText="L License ID";
                dgvInternationalLicenses.Columns[3].Width = 100;

                dgvInternationalLicenses.Columns[4].HeaderText="Issue Date";
                dgvInternationalLicenses.Columns[4].Width = 170;

                dgvInternationalLicenses.Columns[5].HeaderText="Expiration Date";
                dgvInternationalLicenses.Columns[5].Width = 170;

                dgvInternationalLicenses.Columns[6].HeaderText="Is Active";
                dgvInternationalLicenses.Columns[6].Width = 100;


            }

            lblInternationalLicensesRecords.Text=dgvInternationalLicenses.Rows.Count.ToString();


        }

        private void frmInterNationalLicenseApplications_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex=0;
            cbIsActive.Visible=false;
            _RefreshData();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriversBusiness.FindDriverByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).Person_ID;
            frmShowLicenseHistory frm = new frmShowLicenseHistory(PersonID);
            frm.ShowDialog();

        }

        private void PersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriversBusiness.FindDriverByDriverID((int)dgvInternationalLicenses.CurrentRow.Cells[2].Value).Person_ID;

            frmShowDetailsPerson frm = new frmShowDetailsPerson(PersonID);
            frm.ShowDialog();

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
          frmShowInterNationalLicenseInfo frm=new frmShowInterNationalLicenseInfo((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void btnIssueInternationalLicense_Click(object sender, EventArgs e)
        {
            frmIssueInternationalLicenseApplication frm = new frmIssueInternationalLicenseApplication();
            frm.ShowDialog();
           _RefreshData();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterBy.Visible=false;
                    cbIsActive.Visible=false;
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    txtFilterBy.Visible=true;
                    break;
                case 5:
                    txtFilterBy.Visible =false;
                    cbIsActive.Visible=true;
                    cbIsActive.SelectedIndex = 0;
                    cbIsActive_SelectedIndexChanged(sender, e);
                    break;


            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            if (dgvInternationalLicenses.DataSource == null) return;

             dv = ((DataTable)dgvInternationalLicenses.DataSource).DefaultView;

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

            if (Column=="LocalLicenseID")
                Column="IssuedUsingLocalLicenseID";

            if (int.TryParse(Value, out int PersonID))
            {

                dv.RowFilter =$"{Column} =  {PersonID}";
            }
            else
            {
                dv.RowFilter = $"{Column} = -1";
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
                case 2:
                case 3:
                case 4:
                    txbDigits_KeyPress(sender, e);
                    break;
                default:
                    break;
            }
        }




        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsActive.SelectedIndex)
            {
                case 0:
                    _RefreshData();
                    break;
                case 1:
                case 2:
                    cbIsActiveLoadData();
                    break;
                default:
                    break;


            }

        }
        private void cbIsActiveLoadData()
        {
            if (dgvInternationalLicenses.DataSource == null) return;

             dv = ((DataTable)dgvInternationalLicenses.DataSource).DefaultView;

            string Column = cbFilterBy.SelectedItem.ToString().Replace(" ", "");

            if (cbIsActive.Text=="All")
            {
                return;
            }


            if (cbIsActive.Text == "Yes")
            {
               // dv.RowFilter =$"{Column} = {true}";
                dv.RowFilter =$"{Column} =  true";
            }
            else
            {
               // dv.RowFilter = $"{Column} = {false}";
                dv.RowFilter = $"{Column} = false";
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
