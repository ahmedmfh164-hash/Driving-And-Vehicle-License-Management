using DVLD.Business;
using Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_.Properties;
using Guna.UI2.WinForms;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
     

namespace Full_Real_Project_DrivingAndVehicleLicenseDepartment_DVLD_
{
    public partial class UCAddEditPerson : UserControl
    {
        public enum enMode { AddNew, Update };
        private enMode _Mode;
        int Counter;
        int _PersonID;
        clsPeopleBusiness _Person;


        public event Action<int> OnSaveComplete;
        protected virtual void SavingComplete(int PersonID)
        {
            Action<int> handler = OnSaveComplete;
            if (handler != null)
            {
                handler(PersonID);
            }
        }

        public event Action OnCloseClick;


        public UCAddEditPerson()
        {
            InitializeComponent();

        }

        public void GetPersonID(int PersonID)
        {
            _PersonID = PersonID;
            if (PersonID == -1)
            {
                _Mode= enMode.AddNew;
            }
            else
            { _Mode= enMode.Update; }
        }
        void FillcbCountry()
        {
            cbCountry.DataSource=clsCountriesBusiness.GetAllCountries();
            cbCountry.DisplayMember="CountryName";
            cbCountry.ValueMember="CountryID";
        }

        private void _ResetDefaultValues()
        {
            FillcbCountry();
            cbCountry.SelectedIndex=cbCountry.FindString("Egypt");
            
            txtFirstName.Text="";
            txtSecondName.Text="";
            txtThirdName.Text="";
            txtLastName.Text="";
            txtNationalNo.Text="";
            txtEmail.Text="";
            txtAddress.Text="";
            txtPhone.Text="";
                rbMale.Checked=true;
            pbImage.Image = Resources.Male11 ;

            llRemoveImage.Visible=false;

            dtbDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);


        }

        public void LoadData()
        {
           
            _Person=clsPeopleBusiness.FindPersonByPersonID(_PersonID);

            if (_Person==null)
            {
                MessageBox.Show("No Person with ID = "+_PersonID ,"Person Not Found!",MessageBoxButtons.OK);
                OnCloseClick?.Invoke();
                return;
            }

            txtFirstName.Text=_Person._FirstName;
            txtSecondName.Text=_Person._SecondName;
            txtThirdName.Text=_Person._ThirdName;
            txtLastName.Text=_Person._LastName;
            txtNationalNo.Text=_Person._NationalNo;
            txtEmail.Text=_Person._Email;
            txtAddress.Text=_Person._Address;
            txtPhone.Text=_Person._Phone;

            if (_Person._Gender=="Male")
            {
                rbMale.Checked=true;
            }
            else
                rbFemale.Checked=true;            
              
            dtbDateOfBirth.Value=_Person._DateOfBirth;

            var country = clsCountriesBusiness.FindCountryByNationalCountryID((int)_Person._NationalityCountryID);
            if (country!=null)
            {
                int index = cbCountry.FindString(country._CountryName);
                if (index != -1)
                    cbCountry.SelectedIndex = index;
            }


            if (_Person._ImagePath!="")
            {
                if (File.Exists(_Person._ImagePath))
                {
                    pbImage.ImageLocation=_Person._ImagePath;
                    pbImage.Tag="Custom";
                    llRemoveImage.Visible=true;
                    return;
                }
            }

                if (_Person._Gender=="Male")
                {
                    pbImage.Image=Resources.Male11;
                }
                else
                    pbImage.Image= Resources.Female1;


            llRemoveImage.Visible=false;

        }

        public void DefaultAndLoadData()
        {
            _ResetDefaultValues();
            if (_Mode== enMode.AddNew)
            {
                _Person=new clsPeopleBusiness();
            }
            else
                LoadData();

        }

        private void UCAddEditPerson_Load(object sender, EventArgs e)
        {
            DefaultAndLoadData();
        }

        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            Guna2TextBox Temp = (Guna2TextBox)sender;

            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This field is required!");
            }
            else
                errorProvider1.SetError(Temp, null);
        }

        private void CheckTextBoxesIsNullOrEmpty()
        {
             Counter = 0;
            errorProvider1.Clear();

            foreach (Control ctrl in Controls)
            {
                if (string.IsNullOrWhiteSpace(ctrl.Text)&&ctrl is Guna2TextBox)
                {
                    if (ctrl != txtEmail&&ctrl!=txtThirdName)
                    {
                        errorProvider1.SetError(ctrl, "This field is required!");
                        Counter++;
                    }

                }
            }
        }

        private void SaveDataToObject()
        {
            _Person._NationalNo=txtNationalNo.Text.Trim();
            _Person._FirstName=txtFirstName.Text.Trim();
            _Person._SecondName=txtSecondName.Text.Trim();

            if (txtThirdName.Text!="")
                _Person._ThirdName=txtThirdName.Text.Trim();
            else
                _Person._ThirdName="";

            _Person._LastName=txtLastName.Text.Trim();
            _Person._NationalNo=txtNationalNo.Text.Trim();

            if (txtEmail.Text!="")
                _Person._Email=txtEmail.Text.Trim();
            else
                _Person._Email="";

            _Person._Address=txtAddress.Text.Trim();

            _Person._Phone=txtPhone.Text.Trim();

            if (rbMale.Checked)
            {
                _Person._Gender="Male";
            }
            else
            {
                _Person._Gender="Female";
            }
        }

       private void  RemoveOldImageAndSaveNew()
        {
            //Remove old Image
            try
            {
                if (!DVLD.Core.clsImageHelper.DeleteImage(_Person._ImagePath))
                {

                }

            }
            catch
            {
                MessageBox.Show($"Failed to load {_Person._ImagePath}");

            }

            //save Image
            if (pbImage.Tag.ToString()!="Default")
            {
                _Person._ImagePath = DVLD.Core.clsImageHelper.SaveImage(pbImage.Image, _Person._NationalNo);

            }
            else
                _Person._ImagePath="";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_Person == null)
            {
                MessageBox.Show("Person object is not initialized!");
                return;
            }

            CheckTextBoxesIsNullOrEmpty();

            if (Counter>0)
            {
                MessageBox.Show("Some fields are not valid! put the mouse over the red icon(s) to see the error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveDataToObject();

            RemoveOldImageAndSaveNew();

            _Person._DateOfBirth=dtbDateOfBirth.Value;

            _Person._NationalityCountryID = clsCountriesBusiness.GetCountryID(cbCountry.Text.Trim());

            if (_Person.Save())
            {
                MessageBox.Show("Person saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data is not saved successfully!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

            _Mode=enMode.Update;

            _PersonID=_Person._PersonID;

            /*if (OnSaveComplete != null)
                SavingComplete(_PersonID);*/
            OnSaveComplete?.Invoke(_PersonID);

            OnCloseClick?.Invoke();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;
                MessageBox.Show("Selected Image is:" + selectedFilePath);

                if(pbImage.ImageLocation==null)
                pbImage.ImageLocation= selectedFilePath;
                else
                {

                    pbImage.ImageLocation= selectedFilePath;
                }
                    pbImage.Tag="Custom";
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            OnCloseClick?.Invoke();
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation==null)
            {
                pbImage.Image= Resources.Female1;
                llRemoveImage.Visible= false;
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation==null)
              {
                pbImage.Image= Resources.Male11;
                llRemoveImage.Visible= false;
            }
        }

      

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                    if (pbImage.Image!= Resources.Male11&&pbImage.Image!=Resources.Female1)
                    {
                        if (rbMale.Checked)
                            pbImage.Image= Resources.Male11;
                        else
                            pbImage.Image= Resources.Female1;
                    pbImage.Tag="Default";
                    }

            }
            catch { 
            }

            llRemoveImage.Visible= false;

        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);

        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);
            if (txtNationalNo.Text.Trim()!=_Person._NationalNo&&clsPeopleBusiness.IsPersonExistByNationalNo(txtNationalNo.Text.Trim())) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National No is used for anther person!");
            }
            else
              { errorProvider1.SetError(txtNationalNo, null); }


        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim()!=_Person._Email&&clsPeopleBusiness.IsPersonExistByEmail(txtEmail.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "This Email is used for anther person!");
            }
            else
            { errorProvider1.SetError(txtEmail, null); }


        }


        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);

        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            ValidateEmptyTextBox(sender, e);

        }

        private void dtbDateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
