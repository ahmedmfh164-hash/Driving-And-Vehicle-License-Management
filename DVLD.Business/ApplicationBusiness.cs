using System;
using System.Data;
using DVLD.Business;
using DVLD.DataAccess;
using DVLD.Domain;


namespace DVLD.Business
{
    public class clsApplicationBusiness:clsApplications
    {

        public clsApplicationTypesBusiness ApplicationTypeInfo;

        public clsUserBusiness CreatedByUserInfo;

        public override string ApplicantFullName
        {
            get
            {
                return clsPeopleBusiness.FindPersonByPersonID(ApplicantPersonID).FullName;
            }
        }

        public clsApplicationBusiness()

        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }

        private clsApplicationBusiness(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             double PaidFees, int CreatedByUserID)

        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationTypesBusiness.FindApplicationTypeByApplicationTypeID(this.ApplicationTypeID);
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUserBusiness.FindUserByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {

            this.ApplicationID = clsApplicationAccess.AddNewApplication(
                this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {

            return clsApplicationAccess.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate,
                this.ApplicationTypeID, (byte)this.ApplicationStatus,
                this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

        }

        public static clsApplicationBusiness FindApplicationByApplicationID(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            byte ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            double PaidFees = 0; int CreatedByUserID = -1;

            bool IsFound = clsApplicationAccess.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID,ref ApplicationDate,
              ref ApplicationTypeID,ref ApplicationStatus, ref LastStatusDate,ref PaidFees, ref CreatedByUserID);

            if (IsFound)
                return new clsApplicationBusiness(ApplicationID, ApplicantPersonID,ApplicationDate, ApplicationTypeID,
                       (enApplicationStatus)ApplicationStatus, LastStatusDate,PaidFees, CreatedByUserID);
            else
                return null;
        }

        public bool Cancel()

        {
            return clsApplicationAccess.UpdateStatus(ApplicationID, 2);
        }

        public bool SetComplete()

        {
            return clsApplicationAccess.UpdateStatus(ApplicationID, 3);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateApplication();

            }

            return false;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            return clsApplicationAccess.DeleteApplication(ApplicationID);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationAccess.IsApplicationExist(ApplicationID);
        }

        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationAccess.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }

        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }

        public static int GetActiveApplicationID(int PersonID, clsApplicationBusiness.enApplicationType ApplicationTypeID)
        {
            return clsApplicationAccess.GetActiveApplicationID(PersonID, (int)ApplicationTypeID);
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplicationBusiness.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationAccess.GetActiveApplicationIDForLicenseClass(PersonID, (int)ApplicationTypeID, LicenseClassID);
        }

        public int GetActiveApplicationID(clsApplicationBusiness.enApplicationType ApplicationTypeID)
        {
            return GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

    }
}
