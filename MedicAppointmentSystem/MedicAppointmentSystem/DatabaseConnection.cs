using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MedicAppointmentSystem
{
    // =============================================
    // Database Connection Manager using SqlConnection
    // =============================================
    public class DatabaseConnection
    {
        public static string connectionString;

        // Static constructor to read connection string from App.config
        static DatabaseConnection()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["MedicalDB"].ConnectionString;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading connection string: {ex.Message}",
                    "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Property to get connection string
        public static string ConnectionString
        {
            get { return connectionString; }
        }

        // Method to get a new SqlConnection
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Method to test database connection
        public static bool TestConnection()
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection failed: {ex.Message}",
                    "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }

        // Method to check if database exists and has data
        public static bool ValidateDatabase()
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                connection.Open();

                // Check if Doctors table exists and has data
                string query = "SELECT COUNT(*) FROM Doctors";
                SqlCommand command = new SqlCommand(query, connection);

                int doctorCount = (int)command.ExecuteScalar();

                if (doctorCount > 0)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Database connected but no doctors found. Please ensure sample data is loaded.",
                        "Data Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database validation failed: {ex.Message}",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}