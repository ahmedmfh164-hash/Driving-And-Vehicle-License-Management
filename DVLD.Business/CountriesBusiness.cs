using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DVLD.DataAccess;
using DVLD.Domain;

namespace DVLD.Business
{                  
    public class clsCountriesBusiness :clsCountries
    {                     
       
        clsCountriesBusiness(int NationalCountryID,string CountryName)
        {
            this._CountryID = NationalCountryID;
            this._CountryName = CountryName;

        }             

                                 
        public static DataTable GetAllCountries()
        {
            return clsCountriesAccess.GetAllCountries();
        }

        public static clsCountriesBusiness FindCountryByNationalCountryID(int NationalCountryID)
        {
            string CountryName = "";

            if (clsCountriesAccess.GetCountryByNationalCountryID(NationalCountryID, ref CountryName))
            {
                return new clsCountriesBusiness(NationalCountryID, CountryName);
            }
            else
                return null;
        }


        public static int GetCountryID(string CountryName)
        {
            return clsCountriesAccess.GetCountryID(CountryName);
        }





    }
}
