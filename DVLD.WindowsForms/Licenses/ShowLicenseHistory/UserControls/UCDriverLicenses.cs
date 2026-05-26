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
    public partial class UCDriverLicenses : UserControl
    {
        private int _PersonID;

        public UCDriverLicenses()
        {
            InitializeComponent();
        }

        public void GetPersonID(int PersonID)
        {
            _PersonID = PersonID;
        }

        private void LoadLocalDrivingLicenses()
        {                               
            dgvLocalLicensesHistory.DataSource=(DataTable)clsLicenseBusiness.GetDriverLicensesByPersonID(_PersonID);

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText="License ID";
                dgvLocalLicensesHistory.Columns[0].Width = 120;

                dgvLocalLicensesHistory.Columns[1].HeaderText="Application ID";
                dgvLocalLicensesHistory.Columns[1].Width = 120;

                dgvLocalLicensesHistory.Columns[2].HeaderText="Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 260;

                dgvLocalLicensesHistory.Columns[3].HeaderText="Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 160;

                dgvLocalLicensesHistory.Columns[4].HeaderText="Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 160;

                dgvLocalLicensesHistory.Columns[5].HeaderText="Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 70;
            }

            lblLocalLicensesRecords.Text=dgvLocalLicensesHistory.Rows.Count.ToString();


            dgvInternationalLicensesHistory.DataSource=(DataTable)clsInternationalBusiness.FindByPersonID(_PersonID);

            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText="Int License ID";

                dgvInternationalLicensesHistory.Columns[1].HeaderText="Application ID";

                dgvInternationalLicensesHistory.Columns[2].HeaderText="Driver ID";

                dgvInternationalLicensesHistory.Columns[3].HeaderText="LocalLicense ID";

                dgvInternationalLicensesHistory.Columns[4].HeaderText="Issue Date";

                dgvInternationalLicensesHistory.Columns[5].HeaderText="Expiration Date";

                dgvInternationalLicensesHistory.Columns[6].HeaderText="Is Active";

            }
            lblInterNationalLicenseRecords.Text=dgvInternationalLicensesHistory.Rows.Count.ToString();



        }

        private void UCDriverLicenses_Load(object sender, EventArgs e)
        {
           LoadLocalDrivingLicenses();

        }

        private void tpLocalLicenses_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void InternationalLicenseHistorytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowInterNationalLicenseInfo frm = new frmShowInterNationalLicenseInfo((int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowDriverLicenseInfo frm = new frmShowDriverLicenseInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
