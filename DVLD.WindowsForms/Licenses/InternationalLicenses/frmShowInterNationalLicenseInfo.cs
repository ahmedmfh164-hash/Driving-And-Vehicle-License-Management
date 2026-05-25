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
    public partial class frmShowInterNationalLicenseInfo : Form
    {
        public frmShowInterNationalLicenseInfo(int IntLicenseID)
        {
            InitializeComponent();
            ucDriverInterNationalLicenseInfo1.GetIntLicenseID(IntLicenseID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmShowInterNationalLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
