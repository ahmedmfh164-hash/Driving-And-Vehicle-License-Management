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
    public partial class frmManageDetainedLicenses : Form
    {
        private clsLicenseBusiness _License;
        private DataView dv;
        public frmManageDetainedLicenses()
        {
            InitializeComponent();
        }

        private void dgvInternationalLicenses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void _RefreshDetainedLicenses()
        {
            dgvDetainedLicenses.DataSource=clsDetainedLicensesBusiness.GetAllDetainLicenses();

            if (dgvDetainedLicenses.Rows.Count > 0)
            {
                dgvDetainedLicenses.Columns[0].HeaderText="D.ID";
                dgvDetainedLicenses.Columns[0].Width = 80;

                dgvDetainedLicenses.Columns[1].HeaderText="L ID";
                dgvDetainedLicenses.Columns[1].Width = 80;

                dgvDetainedLicenses.Columns[2].HeaderText="N.No.";
                dgvDetainedLicenses.Columns[2].Width = 80;

                dgvDetainedLicenses.Columns[3].HeaderText="Full Name";
                dgvDetainedLicenses.Columns[3].Width = 200;

                dgvDetainedLicenses.Columns[4].HeaderText="Detain Date";
                dgvDetainedLicenses.Columns[4].Width = 100;

                dgvDetainedLicenses.Columns[5].HeaderText="Release Date";
                dgvDetainedLicenses.Columns[5].Width = 100;

                dgvDetainedLicenses.Columns[6].HeaderText="Fine Fees";
                dgvDetainedLicenses.Columns[6].Width = 70;

                dgvDetainedLicenses.Columns[7].HeaderText="ReleaseAppID";
                dgvDetainedLicenses.Columns[7].Width = 80;

                dgvDetainedLicenses.Columns[8].HeaderText="Is Released";
                dgvDetainedLicenses.Columns[8].Width = 80;
            }

            lblDetainedLicensesRecords.Text=dgvDetainedLicenses.Rows.Count.ToString();

        }

        private void frmManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex=0;
            cbIsReleased.Visible=false;
            _RefreshDetainedLicenses();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm=new frmReleaseDetainedLicense();
            frm.ShowDialog();
            _RefreshDetainedLicenses();
        }

        private void PersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _License=clsLicenseBusiness.FindByLicenseID((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);

            frmShowDetailsPerson frm = new frmShowDetailsPerson(_License.DriverInfo.Person_ID);
            frm.ShowDialog();

        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _License=clsLicenseBusiness.FindByLicenseID((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);

            frmShowLicenseHistory frm = new frmShowLicenseHistory(_License.DriverInfo.Person_ID);
            frm.ShowDialog();

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense frm = new frmReleaseDetainedLicense((int)dgvDetainedLicenses.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            _RefreshDetainedLicenses();
        }

        private void dgvDetainedLicenses_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                dgvDetainedLicenses.ContextMenuStrip = null;
                return;
            }

            if (e.Button != MouseButtons.Right)
                return;

            dgvDetainedLicenses.ClearSelection();
            dgvDetainedLicenses.Rows[e.RowIndex].Selected = true;
           

            bool IsReleased=(bool)dgvDetainedLicenses.CurrentRow.Cells[8].Value;

            if( IsReleased )
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            else
                releaseDetainedLicenseToolStripMenuItem.Enabled=true;
         

        }


        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbIsReleased.Visible=false;

            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterBy.Visible=false;

                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    txtFilterBy.Visible=true;
                    break;
                case 7:
                    txtFilterBy.Visible =false;
                    cbIsReleased.Visible=true;
                    cbIsReleased.SelectedIndex = 0;
                    cbIsReleased_SelectedIndexChanged(sender, e);
                    break;


            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            if (dgvDetainedLicenses.DataSource == null) return;

            dv = ((DataTable)dgvDetainedLicenses.DataSource).DefaultView;

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

            if (Column=="NationalNo"||Column=="FullName"||Column=="IsReleased")
            {
                dv.RowFilter =$"{Column} LIKE '{Value}%'";
            }
            else
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
                case 5:
                    txbCharOrDigit_KeyPress(sender, e);
                    break;
                    case 6:
                        txbChar_KeyPress(sender, e);
                    break;

                default:
                    break;
            }
        }




        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsReleased.SelectedIndex)
            {
                case 0:
                    _RefreshDetainedLicenses();
                    break;
                case 1:
                case 2:
                    cbIsReleasedLoadData();
                    break;
                default:
                    break;


            }

        }
        private void cbIsReleasedLoadData()
        {
            if (dgvDetainedLicenses.DataSource == null) return;

            dv = ((DataTable)dgvDetainedLicenses.DataSource).DefaultView;

            string Column = cbFilterBy.SelectedItem.ToString().Replace(" ", "");

            if (cbIsReleased.Text=="All")
            {
                return;
            }


            if (cbIsReleased.Text == "Yes")
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

    }
}
