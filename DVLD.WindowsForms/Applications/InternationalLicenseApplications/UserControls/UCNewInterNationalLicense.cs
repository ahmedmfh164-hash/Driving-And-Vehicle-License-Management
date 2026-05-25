using DVLD.Business;
using DVLD.Core;
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
    public partial class UCNewInterNationalLicense : UserControl
    {
        private int _LicenseID;

        public UCNewInterNationalLicense()
        {
            InitializeComponent();
        }


        public void GetLicenseID(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LoadData();

        }

        public void GetApplicationIDAndIntLicenseID(int IntLicenseID,int ApplicationID)
        {
            lblIntLicenseApplicationID.Text=ApplicationID.ToString();
            lblIntLicenseID.Text=IntLicenseID.ToString();

        }


        private void _LoadData()
        {
           
         
            ucDriverLicenseInfo1.GetLicenseID(_LicenseID);

            lblLocLicenseID.Text=_LicenseID.ToString();


        }

        private void _LoadApplicationInfo()
        {
            clsApplicationTypesBusiness AppType = clsApplicationTypesBusiness.FindApplicationTypeByApplicationTypeID(6);

            lblApplcationDate.Text=DateTime.Now.ToShortDateString();
            lblIssueDate.Text=DateTime.Now.ToShortDateString();
            lblExpirationDate.Text=DateTime.Now.AddYears(1).ToShortDateString();
            lblFees.Text=   AppType.ApplicationFees.ToString();
            lblCreatedBy.Text=clsUserInfo.Username;

        }

        private void UCNewInterNationalLicense_Load(object sender, EventArgs e)
        {
               _LoadApplicationInfo();


        }

        private void ucDriverLicenseInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
