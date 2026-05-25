using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsUserAccess
    {


        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "select UserID,People.PersonID," +
        "FullName=People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName," +
        " UserName, IsActive from Users Left join People on Users.PersonID=People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static DataTable GetAllUsersActives()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "select UserID,People.PersonID," +
        "FullName=People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName," +
        " UserName, IsActive from Users Left join People on Users.PersonID=People.PersonID" +
        "where IsActive=1";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static DataTable GetAllUsersNotActives()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "select UserID,People.PersonID," +
        "FullName=People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName," +
        " UserName, IsActive from Users Left join People on Users.PersonID=People.PersonID" +
        "where IsActive=0";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static bool FindUserByUserID(int UserID, ref int PersonID, ref string FullName, ref string UserName, ref string Password, ref bool IsActive)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "select UserID,People.PersonID," +
                "FullName=People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName," +
                "UserName,Password,IsActive" +
                " from Users left join People on Users.PersonID=People.PersonID " +
                "where UserID=@UserID; ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    PersonID=(int)reader["PersonID"];
                    FullName=(string)reader["FullName"];
                    UserName=(string)reader["UserName"];
                    Password=(string)reader["Password"];
                    IsActive=(bool)reader["IsActive"];

                }
                else
                    isFound=false;

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound= false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;


        }


        public static bool FindUserByPersonID(ref int UserID,int PersonID, ref string FullName, ref string UserName, ref string Password, ref bool IsActive)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "select UserID,People.PersonID," +
                "FullName=People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName," +
                " UserName,Password,IsActive" +
                " from Users left join People on Users.PersonID=People.PersonID " +
                "where Users.PersonID=@PersonID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    UserID=(int)reader["UserID"];
                    FullName=(string)reader["FullName"];
                    UserName=(string)reader["UserName"];
                    Password=(string)reader["Password"];
                    IsActive=(bool)reader["IsActive"];

                }
                else
                    isFound=false;

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound= false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;


        }


        public static bool FindUserByUserName(ref int UserID,ref int PersonID, ref string FullName, string UserName, ref string Password, ref bool IsActive)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "select UserID,Users.PersonID,FullName=People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName," +
                " UserName, Password, IsActive" +
                " from Users Left join People on Users.PersonID=People.PersonID"+
                " where UserName=@UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    FullName=(string)reader["FullName"];
                    UserID=(int)reader["UserID"];
                    PersonID=(int)reader["PersonID"];
                    Password=(string)reader["Password"];
                    IsActive=(bool)reader["IsActive"];

                }
                else
                    isFound=false;

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound= false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }


        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "Insert Into Users (PersonID," +
        " UserName, Password, IsActive ) " +
        "Values(@PersonID,@UserName,@Password,@IsActive);" +
        "select SCOPE_IDENTITY();";
                

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password",Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object result= command.ExecuteScalar();

                if (result!=null&&int.TryParse(result.ToString(),out int insertedID))
                {
                      UserID= (int)insertedID;
                }
                
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return UserID;

        }

        public static bool UpdateUser(int UserID,string FullName, int PersonID, string UserName, string Password, bool IsActive)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = @"Update Users " +
                           " set UserName=@UserName," +
                           " Password=@Password," +
                           " IsActive=@IsActive  " +
                           "where UserID=@UserID";
                          
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                
                   rowsAffected=command.ExecuteNonQuery();

               
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected>0);

        }

        public static bool DeleteUser( int UserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "Delete From Users where UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
               
                rowsAffected= command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected> 0);

        }

        public static bool ChangePassword(int UserID, string Password)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = @"Update Users " +
                           " set Password=@Password," +
                           "where UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                rowsAffected=command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected>0);

        }

        public static bool isUserExistByUserID(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "Select Found=1 From Users Where UserID=@UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound=reader.HasRows;
                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool isUserExistByPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "Select Found=1 From Users Where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound=reader.HasRows;
                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool isUserExistByUserName(string UserName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = "Select Found=1 From users Where UserName=@UserName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound=reader.HasRows;
                reader.Close();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }











    }
}
