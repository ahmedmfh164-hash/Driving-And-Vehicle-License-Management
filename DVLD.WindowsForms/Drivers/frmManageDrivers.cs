using DVLD.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class frmManageDrivers : Form
    {
        private DataTable _AllDrivers;
        public frmManageDrivers()
        {
            InitializeComponent();
        }

        private void _RefreshData()
        {
           _AllDrivers =clsDriversBusiness.GetAllDrivers();
            dgvDrivers.DataSource=_AllDrivers;

            if (dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText="Driver ID";
                dgvDrivers.Columns[0].Width = 100;

                dgvDrivers.Columns[1].HeaderText="Person ID";
                dgvDrivers.Columns[1].Width = 100;

                dgvDrivers.Columns[2].HeaderText="National No.";
                dgvDrivers.Columns[2].Width = 90;

                dgvDrivers.Columns[3].HeaderText="Full Name";
                dgvDrivers.Columns[3].Width = 210;

                dgvDrivers.Columns[4].HeaderText="Created Date";
                dgvDrivers.Columns[4].Width = 120;

                dgvDrivers.Columns[5].HeaderText="Active Licenses";
                dgvDrivers.Columns[5].Width = 170;


            }

            cbFilterBy.SelectedIndex=cbFilterBy.FindString("None");
            lblCountRecords.Text=dgvDrivers.Rows.Count.ToString();

        }

        private void frmManageDrivers_Load(object sender, EventArgs e)
        {
            _RefreshData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void PersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmShowDetailsPerson frm = new frmShowDetailsPerson((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void IssueInternationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueInternationalLicenseApplication frm = new frmIssueInternationalLicenseApplication();
            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicenseHistory frm = new frmShowLicenseHistory((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterBy.Visible=false;
                    _RefreshData();
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    txtFilterBy.Visible=true;
                    break;

            }

        }


        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            if (_AllDrivers == null) return;

            DataView dv = _AllDrivers.DefaultView;

            string Column = cbFilterBy.SelectedItem.ToString().Replace(" ", "");
            string Value = txtFilterBy.Text.Replace("'", "''");

            if (Column=="None")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(Value))
            {
                dv.RowFilter = "";
                return;
            }

            if (Column == "DriverID"||Column == "PersonID"||Column == "ActiveLicenses")
            {
                if (Column=="ActiveLicenses")
                    Column="NumberOfActiveLicenses";

                if (int.TryParse(Value, out int insertedID))
                {


                    dv.RowFilter =$"{Column} =  {insertedID}";
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
                case 2:
                case 6:
                    txbDigits_KeyPress(sender, e);
                    break;
                case 3:
                    txbCharOrDigit_KeyPress(sender, e);
                    break;
                case 4:
                    txbChar_KeyPress(sender, e);
                    break;
                case 5:
                  
                    break;

            }

        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
