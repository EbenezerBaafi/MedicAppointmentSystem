using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MedicAppointmentSystem
{
    // =============================================
    // Data Models for binding
    // =============================================
    public class Doctor 
    {
        public int DoctorID { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public bool Availability { get; set; }

        // For ComboBox display
        public override string ToString()
        {
            return $"{FullName} - {Specialty}";
        }
    }

    public class Patient
    {
        public int PatientID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        // For ComboBox display
        public override string ToString()
        {
            return FullName;
        }
    }

    // =============================================
    // Doctor Data Access using SqlCommand and ExecuteReader
    // =============================================
    public class DoctorDataAccess : DatabaseConnection
    {
        // Retrieve list of doctors using CommandType.Text and ExecuteReader
        public static List<Doctor> GetAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                // Create connection
                connection = DatabaseConnection.GetConnection();
                connection.Open();

                // Create command with CommandType.Text
                string query = "SELECT DoctorID, FullName, Specialty, Availability FROM Doctors ORDER BY FullName";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                // Execute command and get reader
                reader = command.ExecuteReader();

                // Read data from reader
                while (reader.Read())
                {
                    Doctor doctor = new Doctor();
                    doctor.DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID"));
                    doctor.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    doctor.Specialty = reader.GetString(reader.GetOrdinal("Specialty"));
                    doctor.Availability = reader.GetBoolean(reader.GetOrdinal("Availability"));

                    doctors.Add(doctor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving doctors: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure resources are closed properly
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (command != null)
                    command.Dispose();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return doctors;
        }

        // Retrieve available doctors only
        public static List<Doctor> GetAvailableDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();

                string query = "SELECT DoctorID, FullName, Specialty, Availability FROM Doctors WHERE Availability = 1 ORDER BY FullName";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Doctor doctor = new Doctor();
                    doctor.DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID"));
                    doctor.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    doctor.Specialty = reader.GetString(reader.GetOrdinal("Specialty"));
                    doctor.Availability = reader.GetBoolean(reader.GetOrdinal("Availability"));

                    doctors.Add(doctor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving available doctors: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (command != null)
                    command.Dispose();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return doctors;
        }

        // Get doctors by specialty
        public static List<Doctor> GetDoctorsBySpecialty(string specialty)
        {
            List<Doctor> doctors = new List<Doctor>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();

                string query = "SELECT DoctorID, FullName, Specialty, Availability FROM Doctors WHERE Specialty = @Specialty AND Availability = 1 ORDER BY FullName";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                // Add parameter to prevent SQL injection
                command.Parameters.Add("@Specialty", SqlDbType.VarChar).Value = specialty;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Doctor doctor = new Doctor();
                    doctor.DoctorID = reader.GetInt32(reader.GetOrdinal("DoctorID"));
                    doctor.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    doctor.Specialty = reader.GetString(reader.GetOrdinal("Specialty"));
                    doctor.Availability = reader.GetBoolean(reader.GetOrdinal("Availability"));

                    doctors.Add(doctor);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving doctors by specialty: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (command != null)
                    command.Dispose();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return doctors;
        }

        // Get unique specialties for filter dropdown
        public static List<string> GetSpecialties()
        {
            List<string> specialties = new List<string>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();

                string query = "SELECT DISTINCT Specialty FROM Doctors WHERE Availability = 1 ORDER BY Specialty";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    specialties.Add(reader.GetString(reader.GetOrdinal("Specialty")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving specialties: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (command != null)
                    command.Dispose();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return specialties;
        }
    }

    // =============================================
    // Patient Data Access using SqlCommand and ExecuteReader
    // =============================================
    public class PatientDataAccess
    {
        // Retrieve patient information using CommandType.Text and ExecuteReader
        public static List<Patient> GetAllPatients()
        {
            List<Patient> patients = new List<Patient>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();

                string query = "SELECT PatientID, FullName, Email FROM Patients ORDER BY FullName";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Patient patient = new Patient();
                    patient.PatientID = reader.GetInt32(reader.GetOrdinal("PatientID"));
                    patient.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    patient.Email = reader.GetString(reader.GetOrdinal("Email"));

                    patients.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving patients: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (command != null)
                    command.Dispose();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return patients;
        }

        // Search patients by name
        public static List<Patient> SearchPatientsByName(string searchTerm)
        {
            List<Patient> patients = new List<Patient>();
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;

            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();

                string query = "SELECT PatientID, FullName, Email FROM Patients WHERE FullName LIKE @SearchTerm ORDER BY FullName";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                // Add parameter with wildcard
                command.Parameters.Add("@SearchTerm", SqlDbType.VarChar).Value = "%" + searchTerm + "%";

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Patient patient = new Patient();
                    patient.PatientID = reader.GetInt32(reader.GetOrdinal("PatientID"));
                    patient.FullName = reader.GetString(reader.GetOrdinal("FullName"));
                    patient.Email = reader.GetString(reader.GetOrdinal("Email"));

                    patients.Add(patient);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching patients: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();

                if (command != null)
                    command.Dispose();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return patients;
        }

        // Check if email already exists
        public static bool EmailExists(string email)
        {
            bool exists = false;
            SqlConnection connection = null;
            SqlCommand command = null;

            try
            {
                connection = DatabaseConnection.GetConnection();
                connection.Open();

                string query = "SELECT COUNT(*) FROM Patients WHERE Email = @Email";
                command = new SqlCommand(query, connection);
                command.CommandType = CommandType.Text;

                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;

                int count = (int)command.ExecuteScalar();
                exists = count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking email: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (command != null)
                    command.Dispose();

                if (connection != null && connection.State == ConnectionState.Open)
                    connection.Close();
            }

            return exists;
        }
    }
}