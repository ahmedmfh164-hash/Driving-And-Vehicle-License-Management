using DVLD.DataAccess;
using System.Data;
using DVLD.Domain;

namespace DVLD.Business
{
   public class clsUserBusiness :clsUsers
    {

        public clsUserBusiness()
        {
            this._UserID=-1;
            this._PersonID=-1;
            this._FullName="";
            this._UserName="";
            this._Password="";
            this._IsActive=false;

            _Mode=enMode.AddNew;
        }

        public clsUserBusiness(int UserID, string UserName, string Password, int PersonID, string FullName, bool IsActive)
        {
                  _UserID = UserID;
            _UserName = UserName;
            _Password = Password;
            _PersonID = PersonID;
            _FullName = FullName;
            _IsActive = IsActive;

            _Mode= enMode.Update;
        }


        private bool _AddNewUser()
        {
            this._UserID=clsUserAccess.AddNewUser( this._PersonID, this._UserName, this._Password, this._IsActive);
            return (this._UserID!=-1);
        }

        private bool _UpdateUser()
        {
            return clsUserAccess.UpdateUser(this._UserID,this._FullName, this._PersonID, this._UserName, this._Password, this._IsActive);
        }



        public static DataTable GetAllUsers()
        {
            return clsUserAccess.GetAllUsers();
        }

        public static DataTable GetAllUsersActives()
        {
            return clsUserAccess.GetAllUsersActives();
        }

        public static DataTable GetAllUsersNotActives()
        {
            return clsUserAccess.GetAllUsersNotActives();
        }

        public static clsUserBusiness FindUserByUserID(int UserID)
        {
            int PersonID = -1;
            string FullName = "", UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserAccess.FindUserByUserID(UserID, ref PersonID, ref FullName, ref UserName, ref Password, ref IsActive))
            {
                return new clsUserBusiness(UserID, UserName, Password, PersonID, FullName,  IsActive);
            }
            else
                return null;

        }

        public static clsUserBusiness FindUserByPersonID(int PersonID)
        {
            int UserID = -1;
            string FullName = "", UserName = "", Password = "";
            bool IsActive = false;

            if (clsUserAccess.FindUserByPersonID(ref UserID,PersonID, ref FullName, ref UserName, ref Password, ref IsActive))
            {
                return new clsUserBusiness(UserID, UserName, Password, PersonID, FullName, IsActive);
            }
            else
                return null;

        }

        public static clsUserBusiness FindUserByUserName(string Username)
        {
            int UserID = -1;
            int PersonID = -1;
            string FullName = "", Password = "";
            bool IsActive = false;

            if (clsUserAccess.FindUserByUserName(ref UserID, ref PersonID, ref FullName,Username, ref Password, ref IsActive))
            {
                return new clsUserBusiness(UserID, Username, Password, PersonID, FullName, IsActive);
            }
            else
                return null;

        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserAccess.DeleteUser(UserID);
        }

        public static bool IsUserExistByUserID(int UserID)
        {
            return clsUserAccess.isUserExistByUserID(UserID);
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUserAccess.isUserExistByPersonID(PersonID);
        }

        public static bool IsUserExistByUserName(string UserName)
        {
            return clsUserAccess.isUserExistByUserName(UserName);
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _Mode=enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateUser();
                    break;

            }

            return false;
        }











    }

}

