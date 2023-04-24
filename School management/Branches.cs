using MySql.Data.MySqlClient;
using School_management.db;
using School_management.entities;
using School_management.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bunifu.UI.WinForms.BunifuSnackbar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using Branch = School_management.entities.Branch;

namespace School_management
{
    public partial class Branches : UserControl

    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;
        public MainForm mainform;
        BranchService branchService = new BranchService();
        ModulService modulService = new ModulService();
        public Branches()
        {
            InitializeComponent();
            comboBox1.DataSource = branchService.findAll();
            loadBR();
            loadMD();
        }

        public void loadBR()
        {
            guna2DataGridView1.DataSource= branchService.findAll2();
        }
        public void loadMD()
        {
            guna2DataGridView2.DataSource = modulService.findAll2();
        }


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Departments branches = new Departments();
            mainform.panel3.Controls.Add(branches);
            branches.BringToFront();
            branches.Show();
            mainform.label2.Text = "Departments and Groupes";
            branches.mainform = this.mainform;
        }

        private void Branches_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Branches branches = new Branches();
            mainform.panel3.Controls.Add(branches);
            branches.BringToFront();
            branches.Show();
            branches.mainform = this.mainform;
            mainform.label2.Text = "Branches and Modules";
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" )
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {

                    Branch branch = new Branch(textBox1.Text,Convert.ToInt16( textBox2.Text));

                    textBox1.Clear();
                    textBox2.Clear();
                   



                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO branch (name,duration)" + "VALUES(@name,@duration)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@name", branch.Name);
                    cmd.Parameters.AddWithValue("@duration", branch.Duration);
                   


                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    loadBR();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "" || textBox3.Text == "")
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Branch branch = (Branch)comboBox1.SelectedItem;

                    Module module = new Module(textBox4.Text, Convert.ToInt16(textBox3.Text), branch.Id);

                    textBox4.Clear();
                    textBox3.Clear();




                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO module (name,hours,idbranch)" + "VALUES(@name,@hours,@idbranch)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@name", module.Name);
                    cmd.Parameters.AddWithValue("@hours", module.Hours);
                    cmd.Parameters.AddWithValue("@idbranch",module.Idbranch);



                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    loadMD();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this branch ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {


                    Branch branch = new Branch(textBox1.Text, Convert.ToInt16(textBox2.Text));

                    textBox1.Clear();
                    textBox2.Clear();

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE branch SET name= @name, duration=@duration" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@name", branch.Name);
                    cmd.Parameters.AddWithValue("@duration", branch.Duration);






                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();

                    loadBR();
                }
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this branch", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM branch WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox1.Clear();
                textBox2.Clear();


                loadBR();
            }
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView2.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

            textBox4.Text = row.Cells[1].Value.ToString();
            textBox3.Text = row.Cells[2].Value.ToString();
            comboBox1.DataSource = branchService.findByObj2(row.Cells[3].Value.ToString());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this module ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox3.Text == "" || textBox4.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    Branch branch = (Branch)comboBox1.SelectedItem;

                    Module module = new Module(textBox4.Text, Convert.ToInt16(textBox3.Text), branch.Id);

                    textBox4.Clear();
                    textBox3.Clear();

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE module SET name= @name, hours=@hours ,idbranch=idbranch" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@name", module.Name);
                    cmd.Parameters.AddWithValue("@hours", module.Hours);
                    cmd.Parameters.AddWithValue("@idbranch", module.Idbranch);

                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();

                    loadMD();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this module", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM module WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox3.Clear();
                textBox4.Clear();


                loadMD();
            }
        }
    }
}
