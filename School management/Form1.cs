using MySql.Data.MySqlClient;
using School_management.db;
using System.Data;

namespace School_management
{
    public partial class Form1 : Form
    {

         Connexion cnx = new Connexion();
        MySqlDataAdapter ada;
       
        DataTable dt;
        int currRowIndex;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = txtlogin.Text;
            string password = txtpass.Text;
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada = new MySqlDataAdapter("SELECT COUNT(*) FROM user WHERE login='" + login+ "' AND password ='" + password + "'", cnx.connMaster);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            if (txtlogin.Text=="" && txtpass.Text=="") 
            {
                MessageBox.Show("Please fill all fields");
            }
            else if (dt.Rows[0][0].ToString() == "1")
            {
                cnx.connexion();
                cnx.cnxOpen();
                MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT login,role FROM user WHERE login='" + login + "' AND password ='" + password + "'", cnx.connMaster);
                DataTable dt2 = new DataTable();
                ada2.Fill(dt2);

                MainForm main = new MainForm();
                main.login = dt2.Rows[0][0].ToString();
                if (!dt2.Rows[0][1].ToString().Equals("Admin"))
                {
                    main.iconButton6.Visible = false;
                }
                Statistics statistics = new Statistics();
                main.panel3.Controls.Add(statistics);
                statistics.BringToFront();
                statistics.Show();
                label2.Text = "Home";
                main.Show();
                main.label3.Text = login;

                this.Hide();
                cnx.cnxClose();
            }
            else
            {
                MessageBox.Show("incorrect login or pass,try again ");
                txtlogin.Clear();
                txtpass.Clear();
                txtlogin.Focus();

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtlogin.Clear();
            txtpass.Clear();
            txtlogin.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}