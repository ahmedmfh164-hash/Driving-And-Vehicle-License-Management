using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsPeopleAccess
    {


        public static bool FilterByPersonID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref string Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from People " +
                "where PersonID=@PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    NationalNo=(string)reader["NationalNo"];
                    FirstName=(string)reader["FirstName"];
                    SecondName=(string)reader["SecondName"];
                    ThirdName=(string)reader["ThirdName"];
                    LastName=(string)reader["LastName"];
                    DateOfBirth=(DateTime)reader["DateOfBirth"];
                    Gender=(string)reader["Gender"];
                    Address=(string)reader["Address"];
                    Phone=(string)reader["Phone"];
                    Email=(string)reader["Email"];
                    NationalityCountryID=(int)reader["NationalityCountryID"];

                    if (reader["ImagePath"]!=DBNull.Value)
                    {
                        ImagePath=(string)reader["ImagePath"];
                    }
                    else
                        ImagePath="";

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


        public static bool GetPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref string Gender, ref string Address,
            ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from People " +
                "where NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    PersonID=(int)reader["PersonID"];
                    FirstName=(string)reader["FirstName"];
                    SecondName=(string)reader["SecondName"];
                    ThirdName=(string)reader["ThirdName"];
                    LastName=(string)reader["LastName"];
                    DateOfBirth=(DateTime)reader["DateOfBirth"];
                    Gender=(string)reader["Gender"];
                    Address=(string)reader["Address"];
                    Phone=(string)reader["Phone"];
                    Email=(string)reader["Email"];
                    NationalityCountryID=(int)reader["NationalityCountryID"];

                    if (reader["ImagePath"]!=DBNull.Value)
                    {
                        ImagePath=(string)reader["ImagePath"];
                    }
                    else
                        ImagePath="";

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

        public static bool FilterByPhone(ref int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref string Gender, ref string Address,
 string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from People " +
                "where Phone=@Phone";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Phone", Phone);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    NationalNo=(string)reader["NationalNo"];
                    FirstName=(string)reader["FirstName"];
                    SecondName=(string)reader["SecondName"];
                    ThirdName=(string)reader["ThirdName"];
                    LastName=(string)reader["LastName"];
                    DateOfBirth=(DateTime)reader["DateOfBirth"];
                    Gender=(string)reader["Gender"];
                    Address=(string)reader["Address"];
                    PersonID=(int)reader["PersonID"];
                    Email=(string)reader["Email"];
                    NationalityCountryID=(int)reader["NationalityCountryID"];

                    if (reader["ImagePath"]!=DBNull.Value)
                    {
                        ImagePath=(string)reader["ImagePath"];
                    }
                    else
                        ImagePath="";

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

        public static bool FilterByEmail(ref int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
            ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref string Gender, ref string Address,
            ref string Phone, string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from People " +
                "where Email=@Email";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Email", Email);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    NationalNo=(string)reader["NationalNo"];
                    FirstName=(string)reader["FirstName"];
                    SecondName=(string)reader["SecondName"];
                    ThirdName=(string)reader["ThirdName"];
                    LastName=(string)reader["LastName"];
                    DateOfBirth=(DateTime)reader["DateOfBirth"];
                    Gender=(string)reader["Gender"];
                    Address=(string)reader["Address"];
                    Phone=(string)reader["Phone"];
                    PersonID=(int)reader["PersonID"];
                    NationalityCountryID=(int)reader["NationalityCountryID"];

                    if (reader["ImagePath"]!=DBNull.Value)
                    {
                        ImagePath=(string)reader["ImagePath"];
                    }
                    else
                        ImagePath="";

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


        public static DataTable GetAllPerson()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from People";

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


        public static DataTable GetAllPeopleWithLeftJoin()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "select * from People" +
                " left join Countries on Countries.CountryID=People.NationalityCountryID";

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

        public static int AddNewPerson(string NationalNo, string FirstName, string SecondName,
        string ThirdName, string LastName, DateTime DateOfBirth, string Gender, string Address,
       string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"Insert Into People (NationalNo, FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gender,Address,Phone, Email,NationalityCountryID,ImagePath)
                         Values (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gender,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath);
                           Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            
            if (ImagePath!="")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result!=null&&int.TryParse(result.ToString(), out int insertedID))
                {
                    PersonID= insertedID;
                }


            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID,string NationalNo, string FirstName, string SecondName,
 string ThirdName, string LastName, DateTime DateOfBirth, string Gender, string Address,
string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"Update People 
                        set NationalNo=@NationalNo,
                           FirstName=@FirstName,
                           SecondName=@SecondName,
                           ThirdName=@ThirdName,
                           LastName=@LastName,
                           DateOfBirth=@DateOfBirth,
                           Gender=@Gender,
                           Address=@Address,
                           Phone=@Phone,
                           Email=@Email,
                           NationalityCountryID=@NationalityCountryID,
                           ImagePath=@ImagePath
                           where PersonID=@PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            command.Parameters.AddWithValue("@ThirdName", ThirdName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (ImagePath!="")
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);


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

            return (rowsAffected>0);
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"delete from People
                            where PersonID=@PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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

            return (rowsAffected>0);
        }



        public static bool isPersonExistByPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From People Where PersonID=@PersonID";

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


        public static bool isPersonExistByNationalNo(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From People Where NationalNo=@NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

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


        public static bool isPersonExistByPhone(string Phone)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From People Where Phone=@Phone";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Phone", Phone);

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


        public static bool isPersonExistByEmail(string Email)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From People Where Email=@Email";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Email", Email);

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
