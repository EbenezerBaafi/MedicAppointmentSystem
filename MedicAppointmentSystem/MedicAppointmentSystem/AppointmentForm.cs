using MedicAppointmentSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    public class AppointmentForm : Form
    {
        private ComboBox cmbDoctor;
        private ComboBox cmbPatient;
        private DateTimePicker dtpAppointmentDate;
        private DateTimePicker dtpAppointmentTime;
        private TextBox txtNotes;
        private Button btnSave;
        private Button btnCancel;
        private Button btnAddNewPatient;

        private List<Doctor> availableDoctors;
        private List<Patient> allPatients;

        public AppointmentForm()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Size = new Size(500, 450);
            this.Text = "Book New Appointment";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(248, 249, 250);

            // Title
            Label titleLabel = new Label();
            titleLabel.Text = "Book New Appointment";
            titleLabel.Font = new Font("Arial", 16F, FontStyle.Bold);
            titleLabel.Location = new Point(30, 20);
            titleLabel.Size = new Size(300, 30);
            titleLabel.ForeColor = Color.FromArgb(25, 25, 112);
            this.Controls.Add(titleLabel);

            // Doctor Selection
            Label lblDoctor = new Label();
            lblDoctor.Text = "Select Doctor:";
            lblDoctor.Location = new Point(30, 70);
            lblDoctor.Size = new Size(100, 20);
            this.Controls.Add(lblDoctor);

            cmbDoctor = new ComboBox();
            cmbDoctor.Location = new Point(30, 95);
            cmbDoctor.Size = new Size(300, 25);
            cmbDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDoctor.DisplayMember = "DisplayText"; // Show doctor name and specialty
            cmbDoctor.ValueMember = "DoctorID";      // Use DoctorID as value
            this.Controls.Add(cmbDoctor);

            // Patient Selection
            Label lblPatient = new Label();
            lblPatient.Text = "Select Patient:";
            lblPatient.Location = new Point(30, 135);
            lblPatient.Size = new Size(100, 20);
            this.Controls.Add(lblPatient);

            cmbPatient = new ComboBox();
            cmbPatient.Location = new Point(30, 160);
            cmbPatient.Size = new Size(220, 25);
            cmbPatient.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPatient.DisplayMember = "FullName"; // Show patient name
            cmbPatient.ValueMember = "PatientID";   // Use PatientID as value
            this.Controls.Add(cmbPatient);

            btnAddNewPatient = new Button();
            btnAddNewPatient.Text = "Add New";
            btnAddNewPatient.Location = new Point(260, 160);
            btnAddNewPatient.Size = new Size(70, 25);
            btnAddNewPatient.BackColor = Color.FromArgb(40, 167, 69);
            btnAddNewPatient.ForeColor = Color.White;
            btnAddNewPatient.FlatStyle = FlatStyle.Flat;
            btnAddNewPatient.Click += BtnAddNewPatient_Click;
            this.Controls.Add(btnAddNewPatient);

            // Appointment Date
            Label lblDate = new Label();
            lblDate.Text = "Appointment Date:";
            lblDate.Location = new Point(30, 205);
            lblDate.Size = new Size(120, 20);
            this.Controls.Add(lblDate);

            dtpAppointmentDate = new DateTimePicker();
            dtpAppointmentDate.Location = new Point(30, 230);
            dtpAppointmentDate.Size = new Size(150, 25);
            dtpAppointmentDate.Format = DateTimePickerFormat.Short;
            dtpAppointmentDate.MinDate = DateTime.Today;
            this.Controls.Add(dtpAppointmentDate);

            // Appointment Time
            Label lblTime = new Label();
            lblTime.Text = "Time:";
            lblTime.Location = new Point(200, 205);
            lblTime.Size = new Size(50, 20);
            this.Controls.Add(lblTime);

            dtpAppointmentTime = new DateTimePicker();
            dtpAppointmentTime.Location = new Point(200, 230);
            dtpAppointmentTime.Size = new Size(130, 25);
            dtpAppointmentTime.Format = DateTimePickerFormat.Time;
            dtpAppointmentTime.ShowUpDown = true;
            this.Controls.Add(dtpAppointmentTime);

            // Notes
            Label lblNotes = new Label();
            lblNotes.Text = "Notes (Optional):";
            lblNotes.Location = new Point(30, 270);
            lblNotes.Size = new Size(120, 20);
            this.Controls.Add(lblNotes);

            txtNotes = new TextBox();
            txtNotes.Location = new Point(30, 295);
            txtNotes.Size = new Size(300, 60);
            txtNotes.Multiline = true;
            txtNotes.ScrollBars = ScrollBars.Vertical;
            this.Controls.Add(txtNotes);

            // Save Button
            btnSave = new Button();
            btnSave.Text = "Book Appointment";
            btnSave.Location = new Point(130, 375);
            btnSave.Size = new Size(120, 35);
            btnSave.BackColor = Color.FromArgb(34, 139, 34);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            // Cancel Button
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(270, 375);
            btnCancel.Size = new Size(80, 35);
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);

            this.ResumeLayout(false);
        }

        // Load ComboBox data from database using ExecuteReader
        private void LoadComboBoxData()
        {
            try
            {
                // Load available doctors using SqlCommand and ExecuteReader
                availableDoctors = DoctorDataAccess.GetAvailableDoctors();

                // Create custom display objects for doctors
                var doctorDisplayList = new List<object>();
                foreach (Doctor doctor in availableDoctors)
                {
                    doctorDisplayList.Add(new
                    {
                        DoctorID = doctor.DoctorID,
                        DisplayText = $"{doctor.FullName} - {doctor.Specialty}"
                    });
                }

                cmbDoctor.DataSource = doctorDisplayList;

                if (cmbDoctor.Items.Count > 0)
                    cmbDoctor.SelectedIndex = 0;

                // Load all patients using SqlCommand and ExecuteReader
                allPatients = PatientDataAccess.GetAllPatients();
                cmbPatient.DataSource = allPatients;

                if (cmbPatient.Items.Count > 0)
                    cmbPatient.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddNewPatient_Click(object sender, EventArgs e)
        {
            // Create a simple input dialog for new patient
            using (AddPatientDialog dialog = new AddPatientDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Refresh the patient list
                    LoadComboBoxData();

                    // Select the newly added patient
                    foreach (Patient patient in allPatients)
                    {
                        if (patient.FullName == dialog.PatientName)
                        {
                            cmbPatient.SelectedValue = patient.PatientID;
                            break;
                        }
                    }
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (cmbDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a doctor.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPatient.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Combine date and time
            DateTime appointmentDateTime = dtpAppointmentDate.Value.Date + dtpAppointmentTime.Value.TimeOfDay;

            // Check if appointment is in the past
            if (appointmentDateTime < DateTime.Now)
            {
                MessageBox.Show("Appointment cannot be scheduled in the past.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected values
            int doctorID = (int)cmbDoctor.SelectedValue;
            int patientID = (int)cmbPatient.SelectedValue;
            string notes = txtNotes.Text.Trim();

            // Save appointment (this will be implemented in the next step)
            MessageBox.Show($"Ready to save appointment:\nDoctor ID: {doctorID}\nPatient ID: {patientID}\nDate: {appointmentDateTime}\nNotes: {notes}",
                "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TODO: Implement actual database save in next step
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    // Simple dialog for adding new patients
    public class AddPatientDialog : Form
    {
        private TextBox txtPatientName;
        private TextBox txtPatientEmail;
        private Button btnSave;
        private Button btnCancel;

        public string PatientName { get; private set; }
        public string PatientEmail { get; private set; }

        public AddPatientDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(400, 250);
            this.Text = "Add New Patient";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Title
            Label titleLabel = new Label();
            titleLabel.Text = "Add New Patient";
            titleLabel.Font = new Font("Arial", 14F, FontStyle.Bold);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(200, 25);
            this.Controls.Add(titleLabel);

            // Patient Name
            Label lblName = new Label();
            lblName.Text = "Full Name:";
            lblName.Location = new Point(20, 60);
            lblName.Size = new Size(100, 20);
            this.Controls.Add(lblName);

            txtPatientName = new TextBox();
            txtPatientName.Location = new Point(20, 85);
            txtPatientName.Size = new Size(340, 25);
            this.Controls.Add(txtPatientName);

            // Patient Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(20, 125);
            lblEmail.Size = new Size(100, 20);
            this.Controls.Add(lblEmail);

            txtPatientEmail = new TextBox();
            txtPatientEmail.Location = new Point(20, 150);
            txtPatientEmail.Size = new Size(340, 25);
            this.Controls.Add(txtPatientEmail);

            // Save Button
            btnSave = new Button();
            btnSave.Text = "Add Patient";
            btnSave.Location = new Point(180, 190);
            btnSave.Size = new Size(100, 30);
            btnSave.BackColor = Color.FromArgb(34, 139, 34);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            // Cancel Button
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(290, 190);
            btnCancel.Size = new Size(80, 30);
            btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            btnCancel.ForeColor = Color.White;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += BtnCancel_Click;
            this.Controls.Add(btnCancel);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtPatientName.Text))
            {
                MessageBox.Show("Please enter patient name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPatientEmail.Text))
            {
                MessageBox.Show("Please enter patient email.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Basic email validation
            if (!txtPatientEmail.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if email already exists
            if (PatientDataAccess.EmailExists(txtPatientEmail.Text.Trim()))
            {
                MessageBox.Show("A patient with this email already exists.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PatientName = txtPatientName.Text.Trim();
            PatientEmail = txtPatientEmail.Text.Trim();

            // TODO: Save to database (will be implemented in next step)
            MessageBox.Show("Patient will be added to database in next implementation step.",
                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}