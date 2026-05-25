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
    public class clsDriversBusiness:clsDrivers
    {
       
        public clsDriversBusiness()
        {
            this.Person_ID = -1;
            this.National_No="";
            this.Full_Name="";
            this.Driver_ID=-1;
            this.Created_Date = DateTime.Now;
            this.CreatedByUser_ID=-1;
        }

        private clsDriversBusiness(int DriverID, int PersonID,string FullName,
        string NationalNo,DateTime CreatedDate,int NumberOfActiveLicenses,int CreatedByUserID)
        {
            Person_ID = PersonID;
            this.National_No = NationalNo;
            Full_Name = FullName;
            Driver_ID = DriverID;
            NumberOf_ActiveLicenses = NumberOfActiveLicenses;
            Created_Date = CreatedDate;
            CreatedByUser_ID=CreatedByUserID;

        }


        private bool _AddNewDriver()
        {
            this.Driver_ID=clsDriversAccess.AddNewDriver(this.Person_ID, this.Full_Name, this.National_No,this.Created_Date,this.CreatedByUser_ID);
            return (this.Driver_ID!=-1);
        }


        public static DataTable GetAllDrivers()
        {
            return clsDriversAccess.GetAllDrivers();
        }

        public static clsDriversBusiness FindDriverByDriverID(int DriverID)
        {
            int CreatedByUserID = -1,PersonID=-1, NumberOfActiveLicenses=0;
            string NationalNo = "", FullName = "";
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversAccess.FilterByDriverID(DriverID, ref PersonID, ref FullName,
         ref NationalNo, ref CreatedDate, ref NumberOfActiveLicenses))
            {
                return new clsDriversBusiness(DriverID,PersonID,FullName,NationalNo,CreatedDate,NumberOfActiveLicenses,CreatedByUserID);
            }
            else
                return null;

        }

        public static clsDriversBusiness FindDriverByPersonID(int PersonID)
        {
            int CreatedByUserID = -1, DriverID = -1, NumberOfActiveLicenses = 0;
            string NationalNo = "", FullName = "";
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversAccess.FilterByPersonID(ref DriverID,PersonID, ref FullName,
         ref NationalNo, ref CreatedDate, ref NumberOfActiveLicenses))
            {
                return new clsDriversBusiness(DriverID, PersonID, FullName, NationalNo, CreatedDate, NumberOfActiveLicenses, CreatedByUserID);
            }
            else
                return null;

        }

        public static clsDriversBusiness FindDriverByNationalNo(string NationalNo)
        {
            int CreatedByUserID = -1, PersonID = -1, NumberOfActiveLicenses = 0, DriverID = -1;
            string FullName = "";
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversAccess.GetDriverByNationalNo(ref DriverID, ref PersonID, ref FullName,
         NationalNo, ref CreatedDate, ref NumberOfActiveLicenses))
            {
                return new clsDriversBusiness(DriverID, PersonID, FullName, NationalNo, CreatedDate, NumberOfActiveLicenses, CreatedByUserID);

            }
            else
                return null;

        }

        public static clsDriversBusiness FindDriverByFullName(string FullName)
        {
            int CreatedByUserID = -1, PersonID = -1, NumberOfActiveLicenses = 0, DriverID = -1;
            string NationalNo = "";
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversAccess.FilterByFullName(ref DriverID, ref PersonID,FullName,
        ref NationalNo, ref CreatedDate, ref NumberOfActiveLicenses))
            {
                return new clsDriversBusiness(DriverID, PersonID, FullName, NationalNo, CreatedDate, NumberOfActiveLicenses, CreatedByUserID);

            }
            else
                return null;

        }

        public static clsDriversBusiness FindDriverByNumberOfActiveLicenses(int NumberOfActiveLicenses)
        {
            int CreatedByUserID = -1, PersonID = -1, DriverID = -1;
            string FullName = "",NationalNo="";
            DateTime CreatedDate = DateTime.Now;

            if (clsDriversAccess.FilterByNumberOfActiveLicenses(ref DriverID, ref PersonID, ref FullName,
        ref NationalNo, ref CreatedDate, NumberOfActiveLicenses))
            {
                return new clsDriversBusiness(DriverID, PersonID, FullName, NationalNo, CreatedDate, NumberOfActiveLicenses, CreatedByUserID);

            }
            else
                return null;

        }

        public static bool DeleteDriver(int DriverID)
        {
            return clsDriversAccess.DeleteDriver(DriverID);
        }

        public static bool IsDriverExistByDriverID(int DriverID)
        {
            return clsDriversAccess.isDriverExistByDriverID(DriverID);
        }

        public static bool IsDriverExistByNationalNo(string NationalNo)
        {
            return clsDriversAccess.isDriverExistByNationalNo(NationalNo);
        }

        public static bool IsDriverExistByPersonID(int PersonID)
        {
            return clsDriversAccess.isDriverExistByPersonID(PersonID);
        }

        public bool Save()
        {

            if (_AddNewDriver())
            {
                return true;
            }


            return false;
        }





    }
}
