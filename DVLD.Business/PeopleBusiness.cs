using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DVLD.DataAccess;
using DVLD.Domain;

namespace DVLD.Business
{
    public class clsPeopleBusiness :clsPeople
    {
       
        public clsPeopleBusiness()
        {
            this._PersonID = -1;
            this._NationalNo="";
            this._FirstName="";
            this._SecondName="";
            this._ThirdName="";
            this._LastName="";
            this._Address="";
            this._Email="";
            this._DateOfBirth = DateTime.Now;
            this._Gender="Male";
            this._Phone="";
            this._NationalityCountryID=-1;
            _Mode=enMode.AddNew;
        }

        private clsPeopleBusiness(int personID, string NationalNo, string FirstName, string SecondName,
            string ThirdName, string LastName, DateTime DateOfBirth, string Gender, string Address,
           string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            _PersonID = personID;
            _NationalNo = NationalNo;
            _FirstName = FirstName;
            _SecondName= SecondName;
            _ThirdName = ThirdName;
            _LastName = LastName;
            _Address = Address;
            _Phone = Phone;
            _Email= Email;
            _ImagePath = ImagePath;
            _DateOfBirth = DateOfBirth;
            _Gender= Gender;
            _NationalityCountryID= NationalityCountryID;

            _Mode=enMode.Update;
        }


        private bool _AddNewPerson()
        {
            this._PersonID=clsPeopleAccess.AddNewPerson(this._NationalNo,this._FirstName, this._SecondName, this._ThirdName
                , this._LastName, this._DateOfBirth, this._Gender, this._Address, this._Phone, this._Email, this._NationalityCountryID, this._ImagePath);
            return (this._PersonID!=-1);
        }

        private bool _UpdatePerson()
        {
            return clsPeopleAccess.UpdatePerson(this._PersonID, this._NationalNo, this._FirstName, this._SecondName, this._ThirdName
                , this._LastName, this._DateOfBirth, this._Gender, this._Address, this._Phone, this._Email, this._NationalityCountryID, this._ImagePath);
        }


        public static DataTable GetAllPerson()
        {
            return clsPeopleAccess.GetAllPerson();
        }

        public static DataTable GetAllPeople()
        {
            return (clsPeopleAccess.GetAllPeopleWithLeftJoin());
        }

        public static clsPeopleBusiness FindPersonByPersonID(int PersonID)
        {

            int NationalityCountryID = 0;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = ""
             , Gender = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleAccess.FilterByPersonID(PersonID, ref NationalNo, ref FirstName, ref SecondName,
            ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address,
            ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeopleBusiness(PersonID, NationalNo, FirstName, SecondName,
             ThirdName, LastName, DateOfBirth, Gender, Address,
             Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;

        }

        public static clsPeopleBusiness FindPersonByNationalNo(string NationalNo)
        {
            int NationalityCountryID = 0, PersonID=-1;
             string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = ""
             , Gender = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleAccess.GetPersonByNationalNo( NationalNo, ref PersonID, ref FirstName, ref SecondName,
            ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address,
            ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeopleBusiness(PersonID, NationalNo, FirstName, SecondName,
             ThirdName, LastName, DateOfBirth, Gender, Address,
             Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;

        }

        public static clsPeopleBusiness FindPersonByPhone(string Phone)
        {
            int PersonID = -1;
            int NationalityCountryID = 0;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = ""
             , Gender = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleAccess.FilterByPhone(ref PersonID, ref NationalNo, ref FirstName, ref SecondName,
            ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address,
             Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeopleBusiness(PersonID, NationalNo, FirstName, SecondName,
             ThirdName, LastName, DateOfBirth, Gender, Address,
             Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;

        }

        public static clsPeopleBusiness FindPersonByEmail(string Email)
        {
            int PersonID = -1;
            int NationalityCountryID = 0;
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = ""
             , Gender = "", Phone = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;

            if (clsPeopleAccess.FilterByEmail(ref PersonID, ref NationalNo, ref FirstName, ref SecondName,
            ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address,
            ref Phone, Email, ref NationalityCountryID, ref ImagePath))
            {
                return new clsPeopleBusiness(PersonID, NationalNo, FirstName, SecondName,
             ThirdName, LastName, DateOfBirth, Gender, Address,
             Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;

        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPeopleAccess.DeletePerson(PersonID);
        }

        public static bool IsPersonExistByPersonID(int PersonID)
        {
            return clsPeopleAccess.isPersonExistByPersonID(PersonID);
        }

        public static bool IsPersonExistByNationalNo(string NationalNo)
        {
            return clsPeopleAccess.isPersonExistByNationalNo(NationalNo);
        }

        public static bool IsPersonExistByPhone(string Phone)
        {
            return clsPeopleAccess.isPersonExistByPhone(Phone);
        }

        public static bool IsPersonExistByEmail(string Email)
        {
            return clsPeopleAccess.isPersonExistByEmail(Email);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _Mode=enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                   return _UpdatePerson();
                    break;

            }

            return false;
        }

      


    }


};
