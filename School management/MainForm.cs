using Guna.UI2.WinForms.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace School_management
{
    public partial class MainForm : Form
    {

        public string login;
        public string role;
        public MainForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            Students students = new Students();
            this.panel3.Controls.Add(students);
            students.BringToFront();
            students.Show();
            label2.Text = "Students";
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            Teachers teachers = new Teachers();
            this.panel3.Controls.Add(teachers);
            teachers.BringToFront();
            teachers.Show();
            label2.Text = "Teachers";
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            Grades grades = new Grades();
            this.panel3.Controls.Add(grades);
            grades.BringToFront();
            grades.Show();
            label2.Text = "Grades";
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            Branches branches = new Branches();
            this.panel3.Controls.Add(branches);
            branches.BringToFront();
            branches.Show();
            branches.mainform = this;
            label2.Text = "Branches and Modules";
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            Administration administration = new Administration();
            this.panel3.Controls.Add(administration);
            administration.BringToFront();
            administration.Show();
            label2.Text = "Administration";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            Absense absence = new Absense();
            this.panel3.Controls.Add(absence);
            absence.BringToFront();
            absence.Show();
            label2.Text = "Absence";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void iconButton7_Click(object sender, EventArgs e)
        {
            Statistics statistics = new Statistics();
            this.panel3.Controls.Add(statistics);
            statistics.BringToFront();
            statistics.Show();
            label2.Text = "Statistics";
        }

        private void iconButton7_Click_1(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            this.Hide();
            form.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Statistics statistics = new Statistics();
            this.panel3.Controls.Add(statistics);
            statistics.BringToFront();
            statistics.Show();
            label2.Text = "Home";
        }
    }
}
