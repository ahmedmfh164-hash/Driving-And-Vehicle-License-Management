using DVLD.Business;
using System;
using System.Windows.Forms;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class UCPersonInfoWithFilterBy : UserControl
    {

       private int _PersonID;
        private string _NationalNo;
        private string _Phone;
        private string _Email;

        public event Func<int,int> OnSearchClick;

        protected virtual void SearchClick(int PersonID)
        {
            Func<int,int> handler = OnSearchClick;
            if (handler != null)
            {
                handler(PersonID);
            }
        }

        public UCPersonInfoWithFilterBy()
        {
            InitializeComponent();
        }

        public int GetPersonID
        {
            get { return _PersonID; }
        }

        public void LoadData(int PersonID)
        {
            _PersonID = PersonID;

            cbFilterBy.SelectedIndex=cbFilterBy.FindString("Person ID");
            txtFilterBy.Visible=true;
            txtFilterBy.Text=_PersonID.ToString();
            ucPersonInfo2.GetPersonID(_PersonID);
            gbFilter.Visible=false;
        }

        private void _RefreshUsers()
        {
            cbFilterBy.SelectedIndex=0;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterBy.Visible=false;
                 
                    break;
                case 1:
                    txtFilterBy.Visible=true;

                    break;
                case 2:
                    txtFilterBy.Visible=true;
                    break;
                case 3:
                    txtFilterBy.Visible=true;
                    break;
                case 4:
                    txtFilterBy.Visible=true;
                    break;
                case 5:
                    txtFilterBy.Visible=true;
                    break;
               
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            string Column = cbFilterBy.SelectedItem.ToString().Replace(" ", "");
            string Value = txtFilterBy.Text;

            if (Column=="None")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFilterBy.Text))
            {
                return;
            }

            if (cbFilterBy.Text == "Person ID")
            {
                if (int.TryParse(Value, out int PersonID))
                {
                    _PersonID = PersonID;
                }
              
            }
            else
            {
                if (cbFilterBy.Text == "National No")
                    _NationalNo=Value;

                if (cbFilterBy.Text == "Phone")
                    _Phone=Value;

                if (cbFilterBy.Text == "Email")
                    _Email=Value;

            }


        }


        private void txbChar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txbDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }


        private void txbCharOrDigit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar)&&!char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {

                case 0:

                    break;
                case 1:
                    txbDigits_KeyPress(sender, e);

                    break;
                case 2:
                    txbCharOrDigit_KeyPress(sender, e);

                    break;
                case 3:
                    txbChar_KeyPress(sender, e);

                    break;
                case 4:
                    txbChar_KeyPress(sender, e);

                    break;
               

            }
        }

        private void AddEditPerson(int PersonID)
        {
            Form frm = new frmAddEditPerson(PersonID);
            frm.ShowDialog();
        }

        private void btnSearchPerson_Click(object sender, EventArgs e)
        {

           int Stop=OnSearchClick?.Invoke(_PersonID)??1;

            if (Stop==0)
                return;

            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    ucPersonInfo2.GetPersonID(_PersonID);
                    break;
                case 2:
                    ucPersonInfo2.GetNationalNo(_NationalNo);
                    break;
                case 3:
                    ucPersonInfo2.GetPhone(_Phone);

                    break;
                case 4:
                    ucPersonInfo2.GetEmail(_Email);
                    break;


            }

            _PersonID=ucPersonInfo2.ReceivePersonID;

        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson(-1);
        }


        private void UCFillterBy_Load(object sender, EventArgs e)
        {
            _RefreshUsers();
        }

        private void guna2TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {


        }

        private void gbFilter_Click(object sender, EventArgs e)
        {

        }

        private void ucPersonInfo2_Load(object sender, EventArgs e)
        {

        }
    }
}
