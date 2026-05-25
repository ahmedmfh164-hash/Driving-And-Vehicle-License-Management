using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsApplicationTypesAccess
    {
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "select * from ApplicationTypes ";

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


        public static bool FindApplicationTypeByApplicationTypeID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref double ApplicationFees)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "select *from ApplicationTypes where ApplicationTypeID=@ApplicationTypeID; ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    ApplicationTypeTitle=(string)reader["ApplicationTypeTitle"];
                    ApplicationFees=Convert.ToDouble(reader["ApplicationFees"]);

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

        public static bool UpdateApplicationType(int ApplicationTypeID,string ApplicationTypeTitle, double ApplicationFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Update ApplicationTypes set ApplicationTypeTitle=@ApplicationTypeTitle, ApplicationFees=@ApplicationFees " +
                "where ApplicationTypeID=@ApplicationTypeID;";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

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

            return rowsAffected > 0;
        }


        public static bool isApplicationTypeExistByApplicationTypeID(int ApplicationTypeID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From ApplicationTypes Where ApplicationTypeID=@ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

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
