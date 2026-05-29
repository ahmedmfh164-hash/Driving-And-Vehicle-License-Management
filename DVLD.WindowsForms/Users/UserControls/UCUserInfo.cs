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
    public partial class UCUserInfo : UserControl
    {
        clsUserBusiness _User;
         int _UserID;
        public UCUserInfo()
        {
            InitializeComponent();

        }

        public void GetUserID(int UserID)
        {
            _UserID=UserID;
            LoadData();
        }

        private void lblUserID_Click(object sender, EventArgs e)
        {

        }

        public void LoadData()
        {
            _User=clsUserBusiness.FindUserByUserID(_UserID);

            if(_User==null)
            {
                MessageBox.Show("User is null");
                return;
            }

            ucPersonInfo1.GetPersonID(_User.PersonID);
            ucPersonInfo1.LoadData();

            lblUserID.Text = _UserID.ToString();
            lblUserName.Text=_User.UserName;

            if (_User.IsActive==true)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text="No";

        }

        private void UCUserInfo_Load(object sender, EventArgs e)
        {
           
        }

        private void ucPersonInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
