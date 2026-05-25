using DVLD.DataAccess;
using DVLD.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD.Domain.clsLicense;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.Business
{
    public class clsDetainedLicensesBusiness
    {

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public bool IsReleased { get; set; }
        public int ReleaseApplicationID { get; set; }
        public int CreatedByUserID { get; set; }
        public int ReleasedByUserID { get; set; }
        public DateTime ReleaseDate { get; set; }

        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        public clsDetainedLicensesBusiness()
        {
            this.DetainID=-1;
            this.LicenseID = -1;
            this.ReleaseApplicationID= -1;
            this.DetainDate = DateTime.Now;
            this.ReleaseDate = DateTime.MaxValue;
            this.FineFees = 0;
            this.IsReleased = true;
            this.ReleasedByUserID = -1;
            this.CreatedByUserID = -1;
            Mode = enMode.AddNew;

        }

        public clsDetainedLicensesBusiness(int DetainID, int LicenseID, int ReleaseApplicationID,
            DateTime DetainDate, DateTime ReleaseDate,float FineFees, bool IsReleased,
            int ReleasedByUserID, int CreatedByUserID)
        {
            this.DetainID= DetainID;
            this.LicenseID = LicenseID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.DetainDate= DetainDate;
            this.ReleaseDate = ReleaseDate;
            this.FineFees = FineFees;
            this.IsReleased = IsReleased;
            this.ReleasedByUserID = ReleasedByUserID;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewDetainLicense()
        {
            this.DetainID=clsDetainedLicensesAccess.DetainNewLicense(this.LicenseID, this.CreatedByUserID, this.DetainDate, this.FineFees);

            return (DetainID!=-1);
        }

        private bool _UpdateDetainLicense()
        {
            return clsDetainedLicensesAccess.UpdateDetainLicense(this.DetainID, this.LicenseID, this.ReleaseApplicationID, this.ReleasedByUserID, this.CreatedByUserID, this.DetainDate,
            this.ReleaseDate, this.FineFees, this.IsReleased);
        }

        public static clsDetainedLicensesBusiness FindByDetainID(int DetainID)
        {
            int LicenseID = -1, ReleaseApplicationID = -1, ReleasedByUserID = -1, CreatedByUserID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MaxValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicensesAccess.GetDetainLicenseInfoByDetainID( DetainID,ref LicenseID, ref ReleaseApplicationID, ref ReleasedByUserID, ref CreatedByUserID, ref DetainDate,
            ref ReleaseDate, ref FineFees, ref IsReleased))

                return new clsDetainedLicensesBusiness(DetainID, LicenseID, ReleaseApplicationID, DetainDate,
                ReleaseDate, FineFees, IsReleased, ReleasedByUserID, CreatedByUserID);
            else
                return null;

        }

        public static clsDetainedLicensesBusiness FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, ReleaseApplicationID = -1, ReleasedByUserID = -1, CreatedByUserID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MaxValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicensesAccess.GetDetainLicenseInfoByLicenseID(ref DetainID, LicenseID, ref ReleaseApplicationID, ref ReleasedByUserID, ref CreatedByUserID, ref DetainDate,
            ref ReleaseDate, ref FineFees, ref IsReleased))

                return new clsDetainedLicensesBusiness(DetainID, LicenseID, ReleaseApplicationID, DetainDate,
                ReleaseDate, FineFees, IsReleased, ReleasedByUserID, CreatedByUserID);
            else
                return null;

        }

        public static int FindByLicenseID1(int LicenseID)
        {
            int DetainID = -1, ReleaseApplicationID = -1, ReleasedByUserID = -1, CreatedByUserID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MaxValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetainedLicensesAccess.GetDetainLicenseInfoByLicenseID(ref DetainID, LicenseID, ref ReleaseApplicationID, ref ReleasedByUserID, ref CreatedByUserID, ref DetainDate,
            ref ReleaseDate, ref FineFees, ref IsReleased))

                return 2;
            else
                return 1;

        }

        public static DataTable GetAllDetainLicenses()
        {
            return clsDetainedLicensesAccess.GetAllDetainLicenses();

        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateDetainLicense();

            }

            return false;
        }

        public static bool IsLicenseDetainedByLicenseID(int LicenseID)
        {
            return clsDetainedLicensesAccess.isLicenseDetainedByLicenseID(LicenseID);
        }

        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleasedApplicationID)
        {
            clsDetainedLicensesBusiness DetainedLicense = clsDetainedLicensesBusiness.FindByLicenseID(LicenseID);

            if(DetainedLicense == null)
            {
                return false;
            }

            DetainedLicense.ReleaseApplicationID = ReleasedApplicationID;
            DetainedLicense.IsReleased = true;
            DetainedLicense.ReleaseDate = DateTime.Now;
            DetainedLicense.ReleasedByUserID = ReleasedByUserID;

               if(!DetainedLicense.Save())
                return false;

            return true ;

        }


    }
}