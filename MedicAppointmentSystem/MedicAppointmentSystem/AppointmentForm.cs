using System;
using System.Drawing;
using System.Windows.Forms;


namespace MedicAppointmentSystem
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

            private void LoadComboBoxData()
            {
                // Sample data - will be replaced with database calls
                cmbDoctor.Items.Add("Dr. Sarah Johnson - Cardiology");
                cmbDoctor.Items.Add("Dr. Michael Brown - Dermatology");
                cmbDoctor.Items.Add("Dr. Emily Davis - Pediatrics");

                cmbPatient.Items.Add("John Smith");
                cmbPatient.Items.Add("Mary Johnson");
                cmbPatient.Items.Add("Robert Davis");
            }

            private void BtnAddNewPatient_Click(object sender, EventArgs e)
            {
                // Open add patient form (to be created)
                MessageBox.Show("Add New Patient feature will be implemented next.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                // Save appointment (database code will go here)
                MessageBox.Show("Appointment booked successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }

            private void BtnCancel_Click(object sender, EventArgs e)
            {
                this.Close();
            }
        }
    }
