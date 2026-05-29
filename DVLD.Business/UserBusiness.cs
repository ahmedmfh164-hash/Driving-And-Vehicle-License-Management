using DVLD.DataAccess;
using System.Data;
using DVLD.Domain;

namespace DVLD.Business
{
   public class clsUserBusiness :clsUsers
    {

        public clsUserBusiness()
        {
            this.UserID=-1;
            this.PersonID=-1;
            this.FullName="";
            this.UserName="";
            this.Password="";
            this.IsActive=false;

            Mode=enMode.AddNew;
        }

        public clsUserBusiness(int UserID, string UserName, string Password, int PersonID, string FullName, bool IsActive)
        {
            base.UserID = UserID;
            base.UserName = UserName;
            base.Password = Password;
            base.PersonID = PersonID;
            base.FullName = FullName;
            base.IsActive = IsActive;

            Mode= enMode.Update;
        }


        private bool _AddNewUser()
        {
            this.UserID=clsUserAccess.AddNewUser( this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID!=-1);
        }

        private bool _UpdateUser()
        {
            return clsUserAccess.UpdateUser(this.UserID,this.FullName, this.PersonID, this.UserName, this.Password, this.IsActive);
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
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode=enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _UpdateUser();
                   

            }

            return false;
        }











    }

}

