using DVLD.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD.Domain;

namespace DVLD.Business
{
    public class clsTestBusiness:clsTests
    {
        public clsTestAppointmentBusiness TestAppointmentInfo { set; get; }

        public clsTestBusiness()

        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes ="";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        public clsTestBusiness(int TestID, int TestAppointmentID,
            bool TestResult, string Notes, int CreatedByUserID)

        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointmentBusiness.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewTest()
        {

            this.TestID = clsTestAccess.AddNewTest(this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);


            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {

            return clsTestAccess.UpdateTest(this.TestID, this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);
        }

        public static clsTestBusiness FindByTestID(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestAccess.GetTestInfoByID(TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTestBusiness(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }

        public static clsTestBusiness FindLastTestByPersonIDAndLicenseClassIDAndTestTypeID
            (int PersonID, int LicenseClassID, clsTestTypesBusiness.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestAccess.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,ref Notes, ref CreatedByUserID))

                return new clsTestBusiness(TestID,TestAppointmentID, TestResult,Notes, CreatedByUserID);
            else
                return null;

        }

        public static clsTestBusiness FindTestByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1, PersonID = -1,LicenseClassID=-1,TestTypeID = 1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestAccess.GetTestByTestAppointmentID
                (ref PersonID,ref LicenseClassID,ref TestTypeID, ref TestID,
             TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))

                return new clsTestBusiness(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            else
                return null;

        }

        public static DataTable GetAllTests()
        {
            return clsTestAccess.GetAllTests();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestAccess.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }

    }


}

