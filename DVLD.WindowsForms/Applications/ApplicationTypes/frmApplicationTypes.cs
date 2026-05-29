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
    public partial class frmApplicationTypes : Form
    {
        public frmApplicationTypes()
        {
            InitializeComponent();
        }

        private void _RefreshData()
        {
            dgvApplicationTypes.DataSource=clsApplicationTypesBusiness.GetAllApplicationTypes();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText="ID";
                dgvApplicationTypes.Columns[0].Width = 20;

                dgvApplicationTypes.Columns[1].HeaderText="Title";
                dgvApplicationTypes.Columns[1].Width = 80;

                dgvApplicationTypes.Columns[2].HeaderText="Fees";
                dgvApplicationTypes.Columns[2].Width = 100;

            }


            lblCountRecords.Text=dgvApplicationTypes.Rows.Count.ToString();


        }

        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshData();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateApplicationType frm=new frmUpdateApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.SavedChanging+=UpdateData;
            frm.ShowDialog();
            
        }

        private void UpdateData(object  sender, EventArgs e)
        {
            _RefreshData();
        }

    }
}
