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
    public partial class frmAddEditPerson : Form
    {
        public enum enMode { AddNew, Update };
        private enMode _Mode;

        private int _PersonID;


        public frmAddEditPerson()
        {
            InitializeComponent();
           
        }

        public frmAddEditPerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            if (_PersonID == -1)
            {
                lblAddEditPerson.Text ="Add New Person";
                _Mode= enMode.AddNew;
               ucAddEditPerson1.GetPersonID(_PersonID);

            }
            else
            {
                lblAddEditPerson.Text ="Update Data Person";
                _Mode= enMode.Update;
                lblPersonID.Text=_PersonID.ToString();
                ucAddEditPerson1.GetPersonID(_PersonID);

            }
        }

        private void frmAddEditPerson_Load(object sender, EventArgs e)
        {
           
        }

        private void ucAddEditPerson1_OnSaveComplete(int obj)
        {
            lblPersonID.Text=obj.ToString();
            lblAddEditPerson.Text ="Update Data Person";
            ucAddEditPerson1.DefaultAndLoadData();
        }

        private void ucAddEditPerson1_OnCloseClick()
        {
            this.Close();
          
        }

        private void ucAddEditPerson1_Load(object sender, EventArgs e)
        {

        }
    }
}
