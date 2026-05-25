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
    public class clsTestTypesBusiness:clsTestTypes
    {
       
        public clsTestTypesBusiness()

        {
            this.TestTypeID = enTestType.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeDescription = "";
            this.TestFees = 0;
            Mode = enMode.AddNew;

        }
        public clsTestTypesBusiness(enTestType TestTypeID, string TestTypeTitle,string TestTypeDescription, double TestFees)
        {
            this.TestTypeID=TestTypeID;
            this.TestTypeTitle= TestTypeTitle;
            this.TestTypeDescription= TestTypeDescription;
            this.TestFees=TestFees;

        }

        private bool _UpdateTestType()
        {
            return clsTestTypesAccess.UpdateTestType((int)this.TestTypeID, this.TestTypeTitle,this.TestTypeDescription, this.TestFees);
        }


        public static DataTable GetAllTestTypes()
        {
            return clsTestTypesAccess.GetAllTestTypes();
        }

        public static clsTestTypesBusiness FindTestTypeByTestTypeID(enTestType TestTypeID)
        {
            string TestTypeTitle = "";
            string TestTypeDescription = "";
            Double TestFees = 0;

            if (clsTestTypesAccess.FindTestTypeByTestTypeID((int)TestTypeID, ref TestTypeTitle,ref TestTypeDescription, ref TestFees))
            {
                return new clsTestTypesBusiness((enTestType)TestTypeID, TestTypeTitle, TestTypeDescription, TestFees);
            }
            else
                return null;

        }

        public static bool IsTestTypeExistByTestTypeID(int TestTypeID)
        {
            return clsTestTypesAccess.isTestTypeExistByTestTypeID(TestTypeID);
        }


        public bool Save()
        {
            return _UpdateTestType();
        }


    }
}
