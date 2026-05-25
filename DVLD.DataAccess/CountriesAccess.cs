using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DVLD.DataAccess;

namespace DVLD.DataAccess
{
     public class clsCountriesAccess
    {

        public clsCountriesAccess()
        {
              
        }


        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);
            string query = "Select * from Countries";

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


        public static bool GetCountryByNationalCountryID(int NationalityCountryID, ref string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select * from Countries " +
                "where CountryID=@NationalityCountryID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    CountryName=(string)reader["CountryName"];
                }
                else
                    isFound = false;

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


        public static  int GetCountryID( string CountryName)
        {
            int CountryID = -1;
            SqlConnection connection = new SqlConnection(clsConnectionDatabase.ConnectionString);

            string query = "Select CountryID from Countries " +
                "where CountryName=@CountryName;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if(result!=null&&int.TryParse(result.ToString(),out int insertedID))
                {
                    CountryID= insertedID;
                }
               
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return CountryID;

        }



    }
}
