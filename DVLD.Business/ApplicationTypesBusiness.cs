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
    public class clsApplicationTypesBusiness :clsApplicationTypes
    {
        
        public clsApplicationTypesBusiness(int ApplicationTypeID, string ApplicationTypeTitle, double ApplicationFees)
        {
            this.ApplicationTypeID=ApplicationTypeID;
            this.ApplicationTypeTitle= ApplicationTypeTitle;
            this.ApplicationFees=ApplicationFees;

        }

        private bool _UpdateApplicationType()
        {
            return clsApplicationTypesAccess.UpdateApplicationType( this.ApplicationTypeID,this.ApplicationTypeTitle, this.ApplicationFees);
        }


        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypesAccess.GetAllApplicationTypes();
        }

        public static clsApplicationTypesBusiness FindApplicationTypeByApplicationTypeID(int ApplicationTypeID)
        {
            string ApplicationTypeTitle = "";
            Double ApplicationFees = 0;

            if (clsApplicationTypesAccess.FindApplicationTypeByApplicationTypeID(ApplicationTypeID, ref ApplicationTypeTitle, ref ApplicationFees))
            {
                return new clsApplicationTypesBusiness(ApplicationTypeID, ApplicationTypeTitle, ApplicationFees);
            }
            else
                return null;

        }

        public static bool IsApplicationTypeExistByApplicationTypeID(int ApplicationTypeID)
        {
            return clsApplicationTypesAccess.isApplicationTypeExistByApplicationTypeID(ApplicationTypeID);
        }


        public bool Save()
        {
          return _UpdateApplicationType();
        }







    }



}
