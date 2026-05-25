using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.DataAccess
{
    public class clsTestTypesAccess
    {

        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "select * from TestTypes ";

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


        public static bool FindTestTypeByTestTypeID(int TestTypeID, ref string TestTypeTitle,ref string TestTypeDescription, ref Double TestFees)
        {

            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "select *from TestTypes where TestTypeID=@TestTypeID; ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound=true;
                    TestTypeTitle=(string)reader["TestTypeTitle"];
                    TestTypeDescription=(string)reader["TestTypeDescription"];
                    TestFees=Convert.ToDouble(reader["TestTypeFees"]);

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

        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle,string TestTypeDescription, double TestFees)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Update TestTypes set TestTypeTitle=@TestTypeTitle,TestTypeDescription=@TestTypeDescription, TestTypeFees=@TestTypeFees " +
                "where TestTypeID=@TestTypeID;";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestFees);

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


        public static bool isTestTypeExistByTestTypeID(int TestTypeID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select Found=1 From TestTypes Where TestTypeID=@TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

