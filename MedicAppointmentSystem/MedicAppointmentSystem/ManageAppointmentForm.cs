using System;
using System.Drawing;
using System.Windows.Forms;


namespace MedicAppointmentSystem
{
    public class ManageAppointmentsForm : Form
    {
        private DataGridView dgvAppointments;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private ComboBox cmbFilterDoctor;
        private TextBox txtSearchPatient;
        private Button btnSearch;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnClose;

        public ManageAppointmentsForm()
        {
            InitializeComponent();
            LoadAppointments();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Size = new Size(1000, 600);
            this.Text = "Manage Appointments";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(248, 249, 250);

            // Title
            Label titleLabel = new Label();
            titleLabel.Text = "Manage Appointments";
            titleLabel.Font = new Font("Arial", 16F, FontStyle.Bold);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(300, 30);
            titleLabel.ForeColor = Color.FromArgb(25, 25, 112);
            this.Controls.Add(titleLabel);

            // Filter Panel
            Panel filterPanel = new Panel();
            filterPanel.Location = new Point(20, 60);
            filterPanel.Size = new Size(940, 80);
            filterPanel.BackColor = Color.White;
            filterPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(filterPanel);

            // Date Range Filter
            Label lblFromDate = new Label();
            lblFromDate.Text = "From Date:";
            lblFromDate.Location = new Point(15, 15);
            lblFromDate.Size = new Size(70, 20);
            filterPanel.Controls.Add(lblFromDate);

            dtpFromDate = new DateTimePicker();
            dtpFromDate.Location = new Point(15, 35);
            dtpFromDate.Size = new Size(120, 25);
            dtpFromDate.Format = DateTimePickerFormat.Short;
            dtpFromDate.Value = DateTime.Today.AddDays(-30);
            filterPanel.Controls.Add(dtpFromDate);

            Label lblToDate = new Label();
            lblToDate.Text = "To Date:";
            lblToDate.Location = new Point(150, 15);
            lblToDate.Size = new Size(60, 20);
            filterPanel.Controls.Add(lblToDate);

            dtpToDate = new DateTimePicker();
            dtpToDate.Location = new Point(150, 35);
            dtpToDate.Size = new Size(120, 25);
            dtpToDate.Format = DateTimePickerFormat.Short;
            dtpToDate.Value = DateTime.Today.AddDays(30);
            filterPanel.Controls.Add(dtpToDate);

            // Doctor Filter
            Label lblFilterDoctor = new Label();
            lblFilterDoctor.Text = "Doctor:";
            lblFilterDoctor.Location = new Point(290, 15);
            lblFilterDoctor.Size = new Size(50, 20);
            filterPanel.Controls.Add(lblFilterDoctor);

            cmbFilterDoctor = new ComboBox();
            cmbFilterDoctor.Location = new Point(290, 35);
            cmbFilterDoctor.Size = new Size(150, 25);
            cmbFilterDoctor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilterDoctor.Items.Add("All Doctors");
            cmbFilterDoctor.SelectedIndex = 0;
            filterPanel.Controls.Add(cmbFilterDoctor);

            // Patient Search
            Label lblSearchPatient = new Label();
            lblSearchPatient.Text = "Patient Name:";
            lblSearchPatient.Location = new Point(460, 15);
            lblSearchPatient.Size = new Size(80, 20);
            filterPanel.Controls.Add(lblSearchPatient);

            txtSearchPatient = new TextBox();
            txtSearchPatient.Location = new Point(460, 35);
            txtSearchPatient.Size = new Size(150, 25);
            txtSearchPatient.Text = "Enter patient name...";
            filterPanel.Controls.Add(txtSearchPatient);

            // Search Button
            btnSearch = new Button();
            btnSearch.Text = "Search";
            btnSearch.Location = new Point(630, 33);
            btnSearch.Size = new Size(80, 30);
            btnSearch.BackColor = Color.FromArgb(30, 144, 255);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Click += BtnSearch_Click;
            filterPanel.Controls.Add(btnSearch);

            // DataGridView for appointments
            dgvAppointments = new DataGridView();
            dgvAppointments.Location = new Point(20, 160);
            dgvAppointments.Size = new Size(940, 350);
            dgvAppointments.AllowUserToAddRows = false;
            dgvAppointments.AllowUserToDeleteRows = false;
            dgvAppointments.ReadOnly = true;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.MultiSelect = false;
            dgvAppointments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAppointments.BackgroundColor = Color.White;
            dgvAppointments.BorderStyle = BorderStyle.Fixed3D;

            // Add columns
            dgvAppointments.Columns.Add("AppointmentID", "ID");
            dgvAppointments.Columns.Add("PatientName", "Patient Name");
            dgvAppointments.Columns.Add("DoctorName", "Doctor");
            dgvAppointments.Columns.Add("Specialty", "Specialty");
            dgvAppointments.Columns.Add("AppointmentDate", "Date & Time");
            dgvAppointments.Columns.Add("Notes", "Notes");

            // Set column widths
            dgvAppointments.Columns["AppointmentID"].Width = 50;
            dgvAppointments.Columns["PatientName"].Width = 150;
            dgvAppointments.Columns["DoctorName"].Width = 150;
            dgvAppointments.Columns["Specialty"].Width = 120;
            dgvAppointments.Columns["AppointmentDate"].Width = 150;
            dgvAppointments.Columns["Notes"].Width = 200;

            this.Controls.Add(dgvAppointments);

            // Action Buttons
            btnEdit = new Button();
            btnEdit.Text = "Edit Appointment";
            btnEdit.Location = new Point(650, 530);
            btnEdit.Size = new Size(120, 35);
            btnEdit.BackColor = Color.FromArgb(255, 140, 0);
            btnEdit.ForeColor = Color.White;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Click += BtnEdit_Click;
            this.Controls.Add(btnEdit);

            btnDelete = new Button();
            btnDelete.Text = "Delete";
            btnDelete.Location = new Point(780, 530);
            btnDelete.Size = new Size(80, 35);
            btnDelete.BackColor = Color.FromArgb(220, 20, 60);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Click += BtnDelete_Click;
            this.Controls.Add(btnDelete);

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(880, 530);
            btnClose.Size = new Size(80, 35);
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Click += BtnClose_Click;
            this.Controls.Add(btnClose);

            this.ResumeLayout(false);
        }

        private void LoadAppointments()
        {
            // Sample data - will be replaced with database calls
            dgvAppointments.Rows.Clear();
            dgvAppointments.Rows.Add("1", "John Smith", "Dr. Sarah Johnson", "Cardiology", "2024-08-20 09:00 AM", "Regular checkup");
            dgvAppointments.Rows.Add("2", "Mary Johnson", "Dr. Michael Brown", "Dermatology", "2024-08-20 10:30 AM", "Skin consultation");
            dgvAppointments.Rows.Add("3", "Robert Davis", "Dr. Emily Davis", "Pediatrics", "2024-08-20 11:00 AM", "Child vaccination");
            dgvAppointments.Rows.Add("4", "Linda Wilson", "Dr. James Wilson", "Orthopedics", "2024-08-21 02:00 PM", "Knee pain examination");
            dgvAppointments.Rows.Add("5", "William Brown", "Dr. Lisa Anderson", "Gynecology", "2024-08-21 03:30 PM", "Annual exam");
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            // Search and filter logic will be implemented when connecting to database
            LoadAppointments();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to edit.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Get selected appointment ID
            int appointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);

            // Open edit form (will be created later)
            MessageBox.Show($"Edit appointment ID: {appointmentID}\n\nEdit functionality will be implemented next.",
                "Edit Appointment", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string patientName = dgvAppointments.SelectedRows[0].Cells["PatientName"].Value.ToString();
            string doctorName = dgvAppointments.SelectedRows[0].Cells["DoctorName"].Value.ToString();
            string appointmentDate = dgvAppointments.SelectedRows[0].Cells["AppointmentDate"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete this appointment?\n\n" +
                $"Patient: {patientName}\n" +
                $"Doctor: {doctorName}\n" +
                $"Date: {appointmentDate}",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Delete logic will be implemented when connecting to database
                int appointmentID = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentID"].Value);

                // Remove from grid for now
                dgvAppointments.Rows.RemoveAt(dgvAppointments.SelectedRows[0].Index);

                MessageBox.Show("Appointment deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

