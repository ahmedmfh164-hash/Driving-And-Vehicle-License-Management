using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD.DataAccess
{
    public class clsDetainedLicensesAccess
    {

        public static bool GetDetainLicenseInfoByDetainID(int DetainID,ref int LicenseID, ref int ReleaseApplicationID, ref int ReleasedByUserID,ref int CreatedByUserID, ref DateTime DetainDate,
                ref DateTime ReleaseDate, ref float FineFees, ref bool IsReleased)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "SELECT *FROM DetainedLicenses where DetainedLicenses.DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;
                    LicenseID=(int)reader["LicenseID"];
                    CreatedByUserID=(int)reader["CreatedByUserID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseApplicationID"]!=DBNull.Value)
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                    if (reader["ReleasedByUserID"]!=DBNull.Value)
                        ReleasedByUserID  = (int)reader["ReleasedByUserID"];


                    if (reader["ReleaseDate"]!=DBNull.Value)
                        ReleaseDate =(DateTime)reader["ReleaseDate"];

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


        public static bool GetDetainLicenseInfoByLicenseID(ref int DetainID,int LicenseID, ref int ReleaseApplicationID, ref int ReleasedByUserID,ref int CreatedByUserID, ref DateTime DetainDate,
                ref DateTime ReleaseDate, ref float FineFees, ref bool IsReleased)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "SELECT *FROM DetainedLicenses where DetainedLicenses.LicenseID=@LicenseID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    isFound = true;
                    DetainID=(int)reader["DetainID"];
                    CreatedByUserID=(int)reader["CreatedByUserID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    IsReleased = (bool)reader["IsReleased"];

                    if (reader["ReleaseApplicationID"]!=DBNull.Value)
                    ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

                    if (reader["ReleasedByUserID"]!=DBNull.Value)
                        ReleasedByUserID  = (int)reader["ReleasedByUserID"];

                
                    if (reader["ReleaseDate"]!=DBNull.Value)
                        ReleaseDate =(DateTime)reader["ReleaseDate"];



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

        public static int DetainNewLicense( int LicenseID, int CreatedByUserID, DateTime DetainDate, float FineFees)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "INSERT INTO DetainedLicenses (LicenseID,DetainDate,FineFees,CreatedByUserID) " +
                "VALUES (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID)" +
                " SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if(result !=null&&int.TryParse(result.ToString(),out int insertedID))
                    {
                    DetainID=insertedID;
                }



            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                connection.Close();
            }

            return DetainID;

        }


        public static bool UpdateDetainLicense(int DetainID,int LicenseID, int ReleaseApplicationID, int ReleasedByUserID, int CreatedByUserID, DateTime DetainDate,
               DateTime ReleaseDate, float FineFees, bool IsReleased)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @"UPDATE DetainedLicenses
                               SET LicenseID=@LicenseID,
                               DetainDate=@DetainDate,
                               FineFees=@FineFees,
                               CreatedByUserID=@CreatedByUserID,
                               IsReleased=@IsReleased,
                               ReleaseDate=@ReleaseDate,
                               ReleasedByUserID =@ReleasedByUserID,
                               ReleaseApplicationID=@ReleaseApplicationID
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@ReleaseDate", ReleaseDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
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

        public static DataTable GetAllDetainLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = @" SELECT DetainID,DetainedLicenses.LicenseID,People.NationalNo " +
                " ,FullName=People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName," +
                " DetainDate,ReleaseDate ,FineFees,ReleaseApplicationID,IsReleased FROM DetainedLicenses\r\n  " +
                " inner join Licenses on Licenses.LicenseID=DetainedLicenses.LicenseID inner join Drivers on Drivers.DriverID=Licenses.DriverID" +
                " inner join People on People.PersonID=Drivers.PersonID";

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


        public static bool isLicenseDetainedByLicenseID(int LicenseID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "SELECT Found=1 FROM DetainedLicenses where DetainedLicenses.LicenseID=@LicenseID And DetainedLicenses.IsReleased=0;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();
                object result= command.ExecuteScalar();

                if (result != null)
                    isFound = true;

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
















    }
}
