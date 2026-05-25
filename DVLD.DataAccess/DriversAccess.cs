using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsDriversAccess
    {


        public static bool FilterByDriverID(int DriverID, ref int PersonID, ref string FullName, 
         ref string NationalNo, ref DateTime CreatedDate,ref int NumberOfActiveLicenses)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from Drivers_View " +
                "where DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    NationalNo=(string)reader["NationalNo"];
                    FullName=(string)reader["FullName"];
                    CreatedDate=(DateTime)reader["CreatedDate"];
                    PersonID=(int)reader["PersonID"];
                    NumberOfActiveLicenses=(int)reader["NumberOfActiveLicenses"];

                   
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

        public static bool FilterByPersonID(ref int DriverID, int PersonID, ref string FullName,
        ref string NationalNo, ref DateTime CreatedDate, ref int NumberOfActiveLicenses)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from Drivers_View " +
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
                    FullName=(string)reader["FullName"];
                    CreatedDate=(DateTime)reader["CreatedDate"];
                    DriverID=(int)reader["DriverID"];
                    NumberOfActiveLicenses=(int)reader["NumberOfActiveLicenses"];


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

        public static bool GetDriverByNationalNo(ref int DriverID, ref int PersonID, ref string FullName,
         string NationalNo, ref DateTime CreatedDate, ref int NumberOfActiveLicenses)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from Drivers_View " +
                "where NationalNo=@NationalNo;"   ;

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
                    FullName=(string)reader["FullName"];
                    CreatedDate=(DateTime)reader["CreatedDate"];
                    DriverID=(int)reader["DriverID"];
                    NumberOfActiveLicenses=(int)reader["NumberOfActiveLicenses"];

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

        public static bool FilterByFullName(ref int DriverID, ref int PersonID,string FullName,
         ref string NationalNo, ref DateTime CreatedDate, ref int NumberOfActiveLicenses)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from Drivers_View " +
                "where FullName=@FullName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FullName", FullName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    NationalNo=(string)reader["NationalNo"];
                    CreatedDate=(DateTime)reader["CreatedDate"];
                    DriverID=(int)reader["DriverID"];
                    PersonID=(int)reader["PersonID"];
                    NumberOfActiveLicenses=(int)reader["NumberOfActiveLicenses"];

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

        public static bool FilterByNumberOfActiveLicenses(ref int DriverID, ref int PersonID, ref string FullName,
         ref string NationalNo, ref DateTime CreatedDate, int NumberOfActiveLicenses)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from Drivers_View " +
                "where NumberOfActiveLicenses=@NumberOfActiveLicenses";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NumberOfActiveLicenses", NumberOfActiveLicenses);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    NationalNo=(string)reader["NationalNo"];
                    FullName=(string)reader["FullName"];
                    CreatedDate=(DateTime)reader["CreatedDate"];
                    DriverID=(int)reader["DriverID"];
                    PersonID=(int)reader["PersonID"];

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


        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select *from Drivers_View";

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


        public static int AddNewDriver (int PersonID,string FullName,
        string NationalNo,DateTime CreatedDate,int CreatedByUserID)
        {
            int DriverID = -1;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"Insert Into Drivers (PersonID,CreatedByUserID,CreatedDate)
                         Values (@PersonID,@CreatedByUserID,@CreatedDate);
                           Select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
    
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result!=null&&int.TryParse(result.ToString(), out int insertedID))
                {
                    DriverID= insertedID;
                }


            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return DriverID;
        }

      /*  public static bool UpdateDriver(int DriverID,string FullName)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.connectionString);

            string query = @"Update People 
                        set NationalNo=@NationalNo,
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
*/
        public static bool DeleteDriver(int DriverID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"delete from Drivers_View where DriverID=@DriverID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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



        public static bool isDriverExistByDriverID(int DriverID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From Drivers Where DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

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


        public static bool isDriverExistByNationalNo(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From Drivers_View Where NationalNo=@NationalNo";

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


        public static bool isDriverExistByPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From Drivers Where PersonID=@PersonID";

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



    }
}
