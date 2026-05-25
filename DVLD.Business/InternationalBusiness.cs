using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD.Business.clsApplicationBusiness;

namespace DVLD.Business
{
    public class clsInternationalBusiness:clsApplicationBusiness
    {
        public int InterNationalLicenseID { set; get; }
        public int DriverID {  set; get; }
        public DateTime IssueDate {  set; get; }
        public DateTime ExpirationDate {  set; get; }
        public bool IsActive {  set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsInternationalBusiness()

        {
            this.InterNationalLicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.CreatedByUserID = -1;
            this.ExpirationDate=DateTime.Now;
            this.IsActive = false;
            this.IssueDate = DateTime.Now;
            this.IssuedUsingLocalLicenseID=-1;

            Mode = enMode.AddNew;

        }

        private clsInternationalBusiness(int InternationalLicense_ID, int ApplicationID, int DriverID,
               int IssuedUsingLocalLicenseID, DateTime IssuedDate, DateTime ExpirationDate,
               bool IsActive, int CreatedByUserID,int ApplicantPersonID,string FullName)

        {
            this.InterNationalLicenseID=InternationalLicense_ID;
            this.ApplicationID = ApplicationID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssuedDate;
            this.DriverID = DriverID;
            this.IsActive = IsActive;
            this.ExpirationDate = ExpirationDate;
            this.CreatedByUserID = CreatedByUserID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicantFullName = FullName;
            Mode = enMode.Update;
        }

        private bool _AddNewInterNationalLicenseApplication()
        {

            this.InterNationalLicenseID = clsInterNationalLicensesAccess.AddNewInternationalLicense(this.ApplicationID,this.DriverID,
               this.IssuedUsingLocalLicenseID,this.IssueDate,this.ExpirationDate,
               this.IsActive,this.CreatedByUserID);

            return (this.InterNationalLicenseID != -1);
        }

        private bool _UpdateInterNationalLicenseApplication()
        {

            return clsInterNationalLicensesAccess.UpdateInternationalLicenseApplication(this.InterNationalLicenseID,this.ApplicationID, this.DriverID,
               this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);

        }

        public static clsInternationalBusiness FindByInterNationalLicenseID(int InterNationalLicenseID)
        {
            int ApplicationID = -1, IssuedUsingLocalLicenseID = -1,DriverID=-1,CreatedByUserID=-1;
            DateTime IssueDate = DateTime.Now,ExpirationDate = DateTime.Now;
            bool IsActive=false;

            if (clsInterNationalLicensesAccess.GetInternationalLicenseInfoByID
                (InterNationalLicenseID,ref ApplicationID,ref DriverID,ref IssuedUsingLocalLicenseID,ref IssueDate,ref ExpirationDate,ref IsActive,ref CreatedByUserID))
            {

                clsApplicationBusiness Application = clsApplicationBusiness.FindApplicationByApplicationID(ApplicationID);

                if (Application == null)
                    return null;

                return new clsInternationalBusiness(InterNationalLicenseID, ApplicationID, DriverID,
               IssuedUsingLocalLicenseID, IssueDate,ExpirationDate,
                IsActive, CreatedByUserID,Application.ApplicantPersonID,Application.ApplicantFullName);
            }
            else
                return null;


        }

        public static clsInternationalBusiness FindByApplicationID(int ApplicationID)
        {
            int InterNationalLicenseID = -1, IssuedUsingLocalLicenseID = -1, DriverID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;

            if (clsInterNationalLicensesAccess.GetInternationalLicenseInfoByApplicationID
                (ref InterNationalLicenseID,ApplicationID, ref DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {

                clsApplicationBusiness Application = clsApplicationBusiness.FindApplicationByApplicationID(ApplicationID);

                if (Application == null)
                    return null;

                return new clsInternationalBusiness(InterNationalLicenseID, ApplicationID, DriverID,
              IssuedUsingLocalLicenseID, IssueDate, ExpirationDate,
               IsActive, CreatedByUserID,Application.ApplicantPersonID, Application.ApplicantFullName);
            }
            else
                return null;


        }

        public static clsInternationalBusiness FindByDriverID(int DriverID)
        {
            int InterNationalLicenseID = -1, IssuedUsingLocalLicenseID = -1, ApplicationID = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;

            if (clsInterNationalLicensesAccess.GetInternationalLicenseInfoByDriverID
                (ref InterNationalLicenseID,ref ApplicationID, DriverID, ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {

                clsApplicationBusiness Application = clsApplicationBusiness.FindApplicationByApplicationID(ApplicationID);

                if (Application == null)
                    return null;

                return new clsInternationalBusiness(InterNationalLicenseID, ApplicationID, DriverID,
              IssuedUsingLocalLicenseID, IssueDate, ExpirationDate,
               IsActive, CreatedByUserID, Application.ApplicantPersonID, Application.ApplicantFullName);
            }
            else
                return null;


        }

        public static DataTable FindByPersonID(int PersonID)
        {
           return clsInterNationalLicensesAccess.GetInternationalLicenseInfoByPersonID(PersonID);
         
        }


        public bool Save()
        {
            base.Mode = (clsApplicationBusiness.enMode)Mode;
            if (!base.Save())
                return false;


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInterNationalLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:

                    return _UpdateInterNationalLicenseApplication();

            }

            return false;
        }

        public static DataTable GetAllInterNationalLicenses()
        {
            return clsInterNationalLicensesAccess.GetAllInternationalLicenseApplications();
        }

        public bool Delete()
        {
            bool IsInterNationalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsInterNationalDrivingApplicationDeleted = clsInterNationalLicensesAccess.DeleteInternationalLicenseApplication(this.InterNationalLicenseID);

            if (!IsInterNationalDrivingApplicationDeleted)
                return false;

            IsBaseApplicationDeleted = clsApplicationBusiness.DeleteApplication(this.ApplicationID);
            return IsBaseApplicationDeleted;

        }

        public static int IsIntLicenseExistByLicenseID(int LicenseID)
        {
            return (GetActiveIntLicenseIDByLicenseID(LicenseID));
        }

        public static int GetActiveIntLicenseIDByLicenseID(int LicenseID)
        {

            return clsInterNationalLicensesAccess.GetActiveIntLicenseIDByLicenseID(LicenseID);

        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInterNationalLicensesAccess.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInterNationalLicensesAccess.GetDriverInternationalLicenses(DriverID);
        }

    }
}
