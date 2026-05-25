using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD.DataAccess;

namespace DVLD.Business
{
    public class clsLocalDrivingLicenseApplicationBusiness : clsApplicationBusiness
    {
        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClassBusiness LicenseClass;
        public enum enMode { AddNew = 0, Update = 1 };

        enMode Mode = enMode.AddNew;

        public clsLocalDrivingLicenseApplicationBusiness()

        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;

            Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplicationBusiness(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             double PaidFees, int CreatedByUserID, int LicenseClassID)

        {
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID; ;
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClass = clsLicenseClassBusiness.Find(LicenseClassID);
            Mode = enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {

            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicensesAccess.AddNewLocalDrivingLicenseApplication(this.ApplicationID, this.LicenseClassID);
                   
            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {

            return clsLocalDrivingLicensesAccess.UpdateLocalDrivingLicenseApplication
                (
                this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);

        }

        public static clsLocalDrivingLicenseApplicationBusiness FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicensesAccess.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID))
            {
                clsApplicationBusiness Application = clsApplicationBusiness.FindApplicationByApplicationID(ApplicationID);

                if (Application == null)
                    return null;

                return new clsLocalDrivingLicenseApplicationBusiness(
                    LocalDrivingLicenseApplicationID,ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }

        public static clsLocalDrivingLicenseApplicationBusiness FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicensesAccess.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                clsApplicationBusiness Application = clsApplicationBusiness.FindApplicationByApplicationID(ApplicationID);

                return new clsLocalDrivingLicenseApplicationBusiness(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }

        public static clsLocalDrivingLicenseApplicationBusiness FindByLicenseID(int LicenseID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1,ApplicationID=-1;

            if (clsLocalDrivingLicensesAccess.GetLocalDrivingLicenseApplicationInfoByLicenseID
                (LicenseID,ref ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                clsApplicationBusiness Application = clsApplicationBusiness.FindApplicationByApplicationID(ApplicationID);

                return new clsLocalDrivingLicenseApplicationBusiness(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;


        }


        public bool SaveLDLApp()
        {

            base.Mode = (clsApplicationBusiness.enMode)Mode;
            if (!base.Save())
                return false;


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;

                case enMode.Update:

                            return _UpdateLocalDrivingLicenseApplication();

                        }

            return false;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicensesAccess.GetAllLocalDrivingLicenseApplications();
        }

        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
       
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicensesAccess.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);

            if (!IsLocalDrivingApplicationDeleted)
                return false;
         
            IsBaseApplicationDeleted = clsApplicationBusiness.DeleteApplication(this.ApplicationID);
            return IsBaseApplicationDeleted;

        }

        public bool DoesPassTestType(clsTestTypesBusiness.enTestType TestTypeID)

        {
            return clsLocalDrivingLicensesAccess.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestTypesBusiness.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestTypesBusiness.enTestType.VisionTest:
                    return true;

                case clsTestTypesBusiness.enTestType.WrittenTest:

                    return this.DoesPassTestType(clsTestTypesBusiness.enTestType.VisionTest);


                case clsTestTypesBusiness.enTestType.StreetTest:

                    return this.DoesPassTestType(clsTestTypesBusiness.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestTypeID)

        {
            return clsLocalDrivingLicensesAccess.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestTypesBusiness.enTestType TestTypeID)

        {
            return clsLocalDrivingLicensesAccess.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestTypesBusiness.enTestType TestTypeID)
        {
            return clsLocalDrivingLicensesAccess.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestTypeID)

        {
            return clsLocalDrivingLicensesAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestTypeID)

        {
            return clsLocalDrivingLicensesAccess.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) >0;
        }

        public bool AttendedTest(clsTestTypesBusiness.enTestType TestTypeID)

        {
            return clsLocalDrivingLicensesAccess.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestTypeID)
        {

            return clsLocalDrivingLicensesAccess.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypesBusiness.enTestType TestTypeID)
        {
            return clsLocalDrivingLicensesAccess.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public clsTestBusiness GetLastTestPerTestType(clsTestTypesBusiness.enTestType TestTypeID)
        {
            return clsTestBusiness.FindLastTestByPersonIDAndLicenseClassIDAndTestTypeID(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsTestBusiness.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestBusiness.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTestBusiness.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return clsTestBusiness.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriversBusiness Driver = clsDriversBusiness.FindDriverByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                Driver = new clsDriversBusiness();

                Driver.Person_ID= this.ApplicantPersonID;
                Driver.CreatedByUser_ID= CreatedByUserID;

                if (Driver.Save())
                {
                    DriverID= Driver.Driver_ID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID= Driver.Driver_ID;
            }

            clsLicenseBusiness License = new clsLicenseBusiness();
            License.ApplicationID = this.ApplicationID;
            License.DriverID= DriverID;
            License.LicenseClassID = this.LicenseClassID;
            License.IssueDate=DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClass.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClass.ClassFees;
            License.IsActive= true;
            License.IssueReason = clsLicenseBusiness.enIssueReason.FirstTime;
            License.CreatedByUserID= CreatedByUserID;

            if (License.Save())
            {
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() !=-1);
        }

        public int GetActiveLicenseID()
        {
            return clsLicenseBusiness.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

    }
}
