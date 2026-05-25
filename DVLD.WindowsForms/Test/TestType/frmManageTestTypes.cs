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
    public partial class frmManageTestTypes : Form
    {
        public frmManageTestTypes()
        {
            InitializeComponent();
        }


        private void _RefreshData()
        {
            dgvTestTypes.DataSource=clsTestTypesBusiness.GetAllTestTypes();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText="ID";
                dgvTestTypes.Columns[0].Width = 20;

                dgvTestTypes.Columns[1].HeaderText="Title";
                dgvTestTypes.Columns[1].Width = 50;

                dgvTestTypes.Columns[2].HeaderText="Description";
                dgvTestTypes.Columns[2].Width = 100;

                dgvTestTypes.Columns[3].HeaderText="Fees";
                dgvTestTypes.Columns[3].Width = 100;

            }


            lblCountRecords.Text=dgvTestTypes.Rows.Count.ToString();


        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmManageTestTypes_Load(object sender, EventArgs e)
        {
               _RefreshData();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateTestTypes frm = new frmUpdateTestTypes((int)dgvTestTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshData();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
