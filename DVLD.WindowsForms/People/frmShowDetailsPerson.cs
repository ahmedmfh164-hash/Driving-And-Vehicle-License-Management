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
    public partial class frmShowDetailsPerson : Form
    {
        int _PersonID;
        public frmShowDetailsPerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            ucPersonInfo1.GetPersonID(_PersonID);

        }

        private void frmShowDetailsPerson_Load(object sender, EventArgs e)
        {

        }

      
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
