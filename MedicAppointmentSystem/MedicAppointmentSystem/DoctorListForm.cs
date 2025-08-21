using MedicAppointmentSystem;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MedicalAppointmentSystem
{
    public class DoctorListForm : Form
    {
        private DataGridView dgvDoctors;
        private ComboBox cmbSpecialty;
        private CheckBox chkAvailableOnly;
        private Button btnRefresh;
        private Button btnClose;

        private List<Doctor> allDoctors; // Store all doctors for filtering

        public DoctorListForm()
        {
            InitializeComponent();
            LoadSpecialties();
            LoadDoctors();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.Size = new Size(800, 500);
            this.Text = "Doctor Directory";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(248, 249, 250);

            // Title
            Label titleLabel = new Label();
            titleLabel.Text = "Doctor Directory";
            titleLabel.Font = new Font("Arial", 16F, FontStyle.Bold);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Size = new Size(200, 30);
            titleLabel.ForeColor = Color.FromArgb(25, 25, 112);
            this.Controls.Add(titleLabel);

            // Filter Panel
            Panel filterPanel = new Panel();
            filterPanel.Location = new Point(20, 60);
            filterPanel.Size = new Size(740, 60);
            filterPanel.BackColor = Color.White;
            filterPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(filterPanel);

            // Specialty Filter
            Label lblSpecialty = new Label();
            lblSpecialty.Text = "Filter by Specialty:";
            lblSpecialty.Location = new Point(15, 20);
            lblSpecialty.Size = new Size(120, 20);
            filterPanel.Controls.Add(lblSpecialty);

            cmbSpecialty = new ComboBox();
            cmbSpecialty.Location = new Point(135, 18);
            cmbSpecialty.Size = new Size(150, 25);
            cmbSpecialty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSpecialty.SelectedIndexChanged += CmbSpecialty_SelectedIndexChanged;
            filterPanel.Controls.Add(cmbSpecialty);

            // Available Only Filter
            chkAvailableOnly = new CheckBox();
            chkAvailableOnly.Text = "Available Doctors Only";
            chkAvailableOnly.Location = new Point(300, 20);
            chkAvailableOnly.Size = new Size(160, 20);
            chkAvailableOnly.Checked = true;
            chkAvailableOnly.CheckedChanged += ChkAvailableOnly_CheckedChanged;
            filterPanel.Controls.Add(chkAvailableOnly);

            // Refresh Button
            btnRefresh = new Button();
            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(480, 15);
            btnRefresh.Size = new Size(80, 30);
            btnRefresh.BackColor = Color.FromArgb(30, 144, 255);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Click += BtnRefresh_Click;
            filterPanel.Controls.Add(btnRefresh);

            // DataGridView for doctors
            dgvDoctors = new DataGridView();
            dgvDoctors.Location = new Point(20, 140);
            dgvDoctors.Size = new Size(740, 280);
            dgvDoctors.AllowUserToAddRows = false;
            dgvDoctors.AllowUserToDeleteRows = false;
            dgvDoctors.ReadOnly = true;
            dgvDoctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoctors.MultiSelect = false;
            dgvDoctors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDoctors.BackgroundColor = Color.White;
            dgvDoctors.BorderStyle = BorderStyle.Fixed3D;

            // Add columns
            dgvDoctors.Columns.Add("DoctorID", "ID");
            dgvDoctors.Columns.Add("FullName", "Doctor Name");
            dgvDoctors.Columns.Add("Specialty", "Specialty");
            dgvDoctors.Columns.Add("Availability", "Available");

            // Set column widths
            dgvDoctors.Columns["DoctorID"].Width = 60;
            dgvDoctors.Columns["FullName"].Width = 250;
            dgvDoctors.Columns["Specialty"].Width = 200;
            dgvDoctors.Columns["Availability"].Width = 100;

            this.Controls.Add(dgvDoctors);

            // Close Button
            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(680, 440);
            btnClose.Size = new Size(80, 35);
            btnClose.BackColor = Color.FromArgb(108, 117, 125);
            btnClose.ForeColor = Color.White;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Click += BtnClose_Click;
            this.Controls.Add(btnClose);

            this.ResumeLayout(false);
        }

        // Load specialties for filter dropdown using ExecuteReader
        private void LoadSpecialties()
        {
            try
            {
                cmbSpecialty.Items.Clear();
                cmbSpecialty.Items.Add("All Specialties");

                // Get specialties from database using SqlCommand and ExecuteReader
                List<string> specialties = DoctorDataAccess.GetSpecialties();

                foreach (string specialty in specialties)
                {
                    cmbSpecialty.Items.Add(specialty);
                }

                cmbSpecialty.SelectedIndex = 0; // Select "All Specialties" by default
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading specialties: {ex.Message}",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Bind data to DataGridView using data from ExecuteReader
        private void LoadDoctors()
        {
            try
            {
                dgvDoctors.Rows.Clear();

                // Get doctors from database using SqlCommand and ExecuteReader
                if (chkAvailableOnly.Checked)
                {
                    allDoctors = DoctorDataAccess.GetAvailableDoctors();
                }
                else
                {
                    allDoctors = DoctorDataAccess.GetAllDoctors();
                }

                // Apply specialty filter if selected
                List<Doctor> filteredDoctors = allDoctors;
                if (cmbSpecialty.SelectedIndex > 0) // Not "All Specialties"
                {
                    string selectedSpecialty = cmbSpecialty.SelectedItem.ToString();
                    filteredDoctors = allDoctors.Where(d => d.Specialty == selectedSpecialty).ToList();
                }

                // Bind data to DataGridView
                foreach (Doctor doctor in filteredDoctors)
                {
                    string availabilityText = doctor.Availability ? "Yes" : "No";
                    dgvDoctors.Rows.Add(doctor.DoctorID, doctor.FullName, doctor.Specialty, availabilityText);
                }

                // Update status
                this.Text = $"Doctor Directory - {filteredDoctors.Count} doctors found";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}",
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbSpecialty_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDoctors(); // Reload with filter applied
        }

        private void ChkAvailableOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadDoctors(); // Reload with availability filter
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadSpecialties(); // Refresh specialty dropdown
            LoadDoctors();     // Refresh doctor list
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}