using MedicAppointmentSystem;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MedicAppointmentSystem
{
    public class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Medical Appointment Booking System";
            this.BackColor = Color.FromArgb(240, 248, 255);

            // Title Label
            Label titleLabel = new Label();
            titleLabel.Text = "Medical Appointment Booking System";
            titleLabel.Font = new Font("Arial", 18F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(25, 25, 112);
            titleLabel.Size = new Size(500, 35);
            titleLabel.Location = new Point(50, 30);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(titleLabel);

            // Subtitle Label
            Label subtitleLabel = new Label();
            subtitleLabel.Text = "Welcome! Select an option below to get started.";
            subtitleLabel.Font = new Font("Arial", 10F);
            subtitleLabel.ForeColor = Color.FromArgb(70, 70, 70);
            subtitleLabel.Size = new Size(400, 20);
            subtitleLabel.Location = new Point(100, 75);
            subtitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(subtitleLabel);

            // View Doctors Button
            Button btnViewDoctors = new Button();
            btnViewDoctors.Text = "View Available Doctors";
            btnViewDoctors.Font = new Font("Arial", 11F, FontStyle.Regular);
            btnViewDoctors.Size = new Size(200, 50);
            btnViewDoctors.Location = new Point(200, 130);
            btnViewDoctors.BackColor = Color.FromArgb(30, 144, 255);
            btnViewDoctors.ForeColor = Color.White;
            btnViewDoctors.FlatStyle = FlatStyle.Flat;
            btnViewDoctors.FlatAppearance.BorderSize = 0;
            btnViewDoctors.Cursor = Cursors.Hand;
            btnViewDoctors.Click += BtnViewDoctors_Click;
            this.Controls.Add(btnViewDoctors);

            // Book Appointment Button
            Button btnBookAppointment = new Button();
            btnBookAppointment.Text = "Book New Appointment";
            btnBookAppointment.Font = new Font("Arial", 11F, FontStyle.Regular);
            btnBookAppointment.Size = new Size(200, 50);
            btnBookAppointment.Location = new Point(200, 195);
            btnBookAppointment.BackColor = Color.FromArgb(34, 139, 34);
            btnBookAppointment.ForeColor = Color.White;
            btnBookAppointment.FlatStyle = FlatStyle.Flat;
            btnBookAppointment.FlatAppearance.BorderSize = 0;
            btnBookAppointment.Cursor = Cursors.Hand;
            btnBookAppointment.Click += BtnBookAppointment_Click;
            this.Controls.Add(btnBookAppointment);

            // Manage Appointments Button
            Button btnManageAppointments = new Button();
            btnManageAppointments.Text = "Manage Appointments";
            btnManageAppointments.Font = new Font("Arial", 11F, FontStyle.Regular);
            btnManageAppointments.Size = new Size(200, 50);
            btnManageAppointments.Location = new Point(200, 260);
            btnManageAppointments.BackColor = Color.FromArgb(255, 140, 0);
            btnManageAppointments.ForeColor = Color.White;
            btnManageAppointments.FlatStyle = FlatStyle.Flat;
            btnManageAppointments.FlatAppearance.BorderSize = 0;
            btnManageAppointments.Cursor = Cursors.Hand;
            btnManageAppointments.Click += BtnManageAppointments_Click;
            this.Controls.Add(btnManageAppointments);

            // Exit Button
            Button btnExit = new Button();
            btnExit.Text = "Exit Application";
            btnExit.Font = new Font("Arial", 10F);
            btnExit.Size = new Size(120, 30);
            btnExit.Location = new Point(240, 330);
            btnExit.BackColor = Color.FromArgb(220, 20, 60);
            btnExit.ForeColor = Color.White;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.Cursor = Cursors.Hand;
            btnExit.Click += BtnExit_Click;
            this.Controls.Add(btnExit);

            this.ResumeLayout(false);
        }

        private void BtnViewDoctors_Click(object sender, EventArgs e)
        {
            DoctorListForm doctorForm = new DoctorListForm();
            doctorForm.ShowDialog();
        }

        private void BtnBookAppointment_Click(object sender, EventArgs e)
        {
            AppointmentForm appointmentForm = new AppointmentForm();
            appointmentForm.ShowDialog();
        }

        private void BtnManageAppointments_Click(object sender, EventArgs e)
        {
            ManageAppointmentsForm manageForm = new ManageAppointmentsForm();
            manageForm.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}