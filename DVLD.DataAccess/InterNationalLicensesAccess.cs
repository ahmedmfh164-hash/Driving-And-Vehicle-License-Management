using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsInterNationalLicensesAccess
    {

        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID, ref int ApplicationID,ref int DriverID,
               ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate,ref DateTime ExpirationDate,
               ref bool IsActive,ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);


            string query = "SELECT * FROM InternationalLicenses WHERE  InternationalLicenseID= @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID=(int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate=(DateTime)reader["IssueDate"];
                    ExpirationDate=(DateTime)reader["ExpirationDate"];
                    IsActive=(bool)reader["IsActive"];
                    CreatedByUserID=(int)reader["CreatedByUserID"];
                }
                else
                {
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetInternationalLicenseInfoByApplicationID(
        ref int InternationalLicenseID, int ApplicationID, ref int DriverID,
               ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,
               ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID=(int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate=(DateTime)reader["IssueDate"];
                    ExpirationDate=(DateTime)reader["ExpirationDate"];
                    IsActive=(bool)reader["IsActive"];
                    CreatedByUserID=(int)reader["CreatedByUserID"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetInternationalLicenseInfoByDriverID(
       ref int InternationalLicenseID,ref int ApplicationID,int DriverID,
              ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,
              ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "  select *from InternationalLicenses where InternationalLicenses.DriverID=@DriverID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;

                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    IssuedUsingLocalLicenseID=(int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate=(DateTime)reader["IssueDate"];
                    ExpirationDate=(DateTime)reader["ExpirationDate"];
                    IsActive=(bool)reader["IsActive"];
                    CreatedByUserID=(int)reader["CreatedByUserID"];

                }
                else
                {
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetInternationalLicenseInfoByPersonID( int PersonID)
        {
            DataTable dt=new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "select  InternationalLicenseID ,ApplicationID ,InternationalLicenses.DriverID" +
                " ,IssuedUsingLocalLicenseID ,IssueDate ,ExpirationDate ,IsActive" +
                " from InternationalLicenses inner join Drivers on Drivers.DriverID=InternationalLicenses.DriverID" +
                "  where Drivers.PersonID=@PersonID" +
                " Order By IsActive Desc, ExpirationDate Desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

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

        public static DataTable GetAllInternationalLicenseApplications()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"SELECT InternationalLicenseID
                            ,ApplicationID
                            ,DriverID
                            ,IssuedUsingLocalLicenseID
                            ,IssueDate
                            ,ExpirationDate
                            ,IsActive
                            FROM InternationalLicenses";

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

        public static int AddNewInternationalLicense(int ApplicationID,int DriverID,
               int IssuedUsingLocalLicenseID,DateTime IssueDate,DateTime ExpirationDate,
               bool IsActive, int CreatedByUserID)
        {

            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"INSERT INTO InternationalLicenses (ApplicationID,DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive,CreatedByUserID)
                             VALUES (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }


            return InternationalLicenseID;
        }


        public static bool UpdateInternationalLicenseApplication(int InternationalLicenseID,int ApplicationID, int DriverID,
               int IssuedUsingLocalLicenseID, DateTime IssueDate,DateTime ExpirationDate,
               bool IsActive, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"Update  InternationalLicenses  
                            set ApplicationID = @ApplicationID,
                                DriverID = @DriverID,
                                IssuedUsingLocalLicenseID=@IssuedUsingLocalLicenseID,
                                 IssueDate=@IssueDate,
                                 ExpirationDate=@ExpirationDate,
                                 IsActive=@IsActive 
                            where InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }


            return InternationalLicenseID;
        }

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"
    SELECT    InternationalLicenseID, ApplicationID,
                IssuedUsingLocalLicenseID , IssueDate, 
                ExpirationDate, IsActive
    from InternationalLicenses where DriverID=@DriverID
        order by ExpirationDate desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static bool DeleteInternationalLicenseApplication(int InternationalLicenseID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"Delete InternationalLicenses 
                                where InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }

        public static int GetActiveIntLicenseIDByLicenseID(int LicenseID)
        {
            int IntLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"SELECT  InternationalLicenses.InternationalLicenseID from InternationalLicenses
            WHERE InternationalLicenses.IssuedUsingLocalLicenseID=@LicenseID And IsActive=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    IntLicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }


            return IntLicenseID;
        }

    }
}
