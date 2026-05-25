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
    public partial class frmShowLicenseHistory : Form
    {
        public frmShowLicenseHistory(int PersonID)
        {
            InitializeComponent();

            ucPersonInfo1.GetPersonID(PersonID);
                              
            ucDriverLicenses1.GetPersonID(PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmShowLicenseHistory_Load(object sender, EventArgs e)
        {




        }

    }
}
