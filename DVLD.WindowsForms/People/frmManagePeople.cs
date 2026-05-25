using DVLD.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class frmManagePeople : Form
    {
        int _PersonID;

        private DataTable _AllPeople;
        private DataTable _dtPeople;
                                          
        public frmManagePeople()
        {
            InitializeComponent();
        }

        private void _LoadPeopleINdgvPeople()
        {
            _AllPeople = clsPeopleBusiness.GetAllPeople();
            _dtPeople = _AllPeople.DefaultView.ToTable(false,
                "PersonID", "NationalNo", "FirstName",
                "SecondName", "ThirdName", "LastName", "Gender", "Email",
                "DateOfBirth", "CountryName", "Address", "Phone");

            dgvPeople.DataSource= _dtPeople;
        }

        private void DefaultCbFilterBy ()
        {
            cbFilterBy.SelectedIndex=0;
        }

        private void _RefreshPeople()
        {
            _LoadPeopleINdgvPeople();

            if(dgvPeople.Rows.Count > 0 )
            {
                dgvPeople.Columns[0].HeaderText="Person ID";
                dgvPeople.Columns[0].Width = 80;

                dgvPeople.Columns[1].HeaderText="National No";
                dgvPeople.Columns[1].Width = 90;

                dgvPeople.Columns[2].HeaderText="First Name";
                dgvPeople.Columns[2].Width = 100;

                dgvPeople.Columns[3].HeaderText="Second Name";
                dgvPeople.Columns[3].Width = 100;

                dgvPeople.Columns[4].HeaderText="Third Name";
                dgvPeople.Columns[4].Width = 100;

                dgvPeople.Columns[5].HeaderText="Last Name";
                dgvPeople.Columns[5].Width = 100;

                dgvPeople.Columns[6].HeaderText="Gender";
                dgvPeople.Columns[6].Width = 70;

                dgvPeople.Columns[7].HeaderText="Email";
                dgvPeople.Columns[7].Width = 132;

                dgvPeople.Columns[8].HeaderText="Date Of Birth";
                dgvPeople.Columns[8].Width = 115;

                dgvPeople.Columns[9].HeaderText="Country Name";
                dgvPeople.Columns[9].Width = 100;

                dgvPeople.Columns[10].HeaderText="Address";
                dgvPeople.Columns[10].Width = 120;

                dgvPeople.Columns[11].HeaderText="Phone";
                dgvPeople.Columns[11].Width = 90;

            }

            DefaultCbFilterBy();
            lblCountRecords.Text=dgvPeople.Rows.Count.ToString();
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
             _RefreshPeople();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetPersonID()
        {
            _PersonID=(int)dgvPeople.CurrentRow.Cells[0].Value;
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            GetPersonID();
            if (clsPeopleBusiness.IsPersonExistByPersonID(_PersonID))
            {
                if (MessageBox.Show("Are you sure want to delete  person ["+_PersonID+"]", "Confirm!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)==DialogResult.OK)
                {
                    if (clsPeopleBusiness.DeletePerson(_PersonID))
                        MessageBox.Show("Person Deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed Deleting this Person!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Person not delete!", "Cancel!", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            _RefreshPeople() ;
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    txtFilterBy.Visible=false;
                    _RefreshPeople();
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    txtFilterBy.Visible=true;
                    break;

            }

            }

      
        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
           
            if (_dtPeople == null) return;

            DataView dv = _dtPeople.DefaultView;

            string Column = cbFilterBy.SelectedItem.ToString().Replace(" ","");
            string Value = txtFilterBy.Text.Replace("'", "''");

            if (Column=="None")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(Value))
            {
                dv.RowFilter = "";
                return;
            }

            if (cbFilterBy.SelectedItem.ToString() == "Person ID")
            {
                if (int.TryParse(Value, out int PersonID))
                {
                    

                    dv.RowFilter =$"{Column} =  {PersonID}";
                }
                else
                {
                    dv.RowFilter = $"{Column} = -1";
                }
            }
            else
            {
                if (cbFilterBy.Text=="Nationality")
                    Column="CountryName";

                dv.RowFilter = $"{Column} LIKE '{Value}%'";
            }


        }


        private void txtbDate_TextChanged(object sender, EventArgs e)
        {

            if (_dtPeople == null) return;

            DataView dv = _dtPeople.DefaultView;

            string Column = dgvPeople.Columns[8].HeaderText.Replace(" ","");
           
            if (string.IsNullOrWhiteSpace(txtbDate.Text))
            {
                dv.RowFilter = "";
                return;
            }

            string Value = txtbDate.Text;

            if (rbDay.Checked||rbMounth.Checked)
                Value = Value.PadLeft(2, '0');


            if (rbDay.Checked)
            {
                dv.RowFilter =$"Convert([{Column}], 'System.String') LIKE '{Value}/%'";
            }
             if (rbMounth.Checked)
            {
                dv.RowFilter =$"Convert([{Column}], 'System.String') LIKE '%/{Value}/%'";
            }
            if(rbYear.Checked)
            {

                dv.RowFilter =$"Convert([{Column}], 'System.String') LIKE '%/{Value}%'";
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

        private bool IsValidEmail(string Email)
        {
            return Regex.IsMatch(Email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (cbFilterBy.SelectedIndex)
            {

                case 0:

                    break;
                case 1:
                case 9:
                    txbDigits_KeyPress(sender, e);
                    break;
                case 2:
                    txbCharOrDigit_KeyPress(sender, e);
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    txbChar_KeyPress(sender, e);
                    break;
                case 10:
                    if (cbFilterBy.Text == "Email")
                    {
                        if (!IsValidEmail(txtFilterBy.Text))
                            return;
                    }

                    break;

            }

        }

        private void AddEditPerson(int PersonID)
        {
            Form frm = new frmAddEditPerson(PersonID);
            frm.ShowDialog();
            _RefreshPeople();
        }


        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEditPerson(-1);
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            AddEditPerson(-1);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPersonID();
            AddEditPerson(_PersonID);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPersonID();
            frmShowDetailsPerson frm=new frmShowDetailsPerson(_PersonID);
            frm.ShowDialog();
        }

        private void txtbDate_KeyPress(object sender, KeyPressEventArgs e)
        {
            txbDigits_KeyPress(sender, e);
        }

        private void btnFindPerson_Click(object sender, EventArgs e)
        {
            frmFindPerson frm=new frmFindPerson();
            frm.ShowDialog();
        }
    }

}
