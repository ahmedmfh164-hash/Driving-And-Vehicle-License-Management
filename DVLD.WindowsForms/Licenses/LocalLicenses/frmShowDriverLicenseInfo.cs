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
    public partial class frmShowDriverLicenseInfo : Form
    {
        public frmShowDriverLicenseInfo(int LicenseID)
        {
            InitializeComponent();

            ucDriverLicenseInfo1.GetLicenseID(LicenseID);

        }


        private void frmShowDriverLicenseInfo_Load(object sender, EventArgs e)
        {


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ucDriverLicenseInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
