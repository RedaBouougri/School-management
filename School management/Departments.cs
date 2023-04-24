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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using Branch = School_management.entities.Branch;

namespace School_management
{
    public partial class Departments : UserControl
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;
        public MainForm mainform;
        BranchService branchService = new BranchService();
        DepartmentService departmentService = new DepartmentService();  
        GroupService groupService = new GroupService();
        public Departments()
        {
            InitializeComponent();
            comboBox1.DataSource = branchService.findAll();
            loadDep();
            loadGRP();
        }

        public void loadDep()
        {
            guna2DataGridView1.DataSource = departmentService.findAll2();
        }
        public void loadGRP()
        {
            guna2DataGridView2.DataSource = groupService.findAll2();
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Departments branches = new Departments();
            mainform.panel3.Controls.Add(branches);
            branches.BringToFront();
            branches.Show();
            branches.mainform = this.mainform;
            mainform.label2.Text = "Departments and Groupes";
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" )
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {

                    Department department = new Department(textBox1.Text);

                    textBox1.Clear();
                




                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO department (name)" + "VALUES(@name)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@name", department.Name);
                 



                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    loadDep();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

            textBox1.Text = row.Cells[1].Value.ToString();
           

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this department ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox1.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {


                    Department department = new Department(textBox1.Text);

                    textBox1.Clear();

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE department SET name= @name" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@name", department.Name);
                   






                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();

                    loadDep();
                }
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this department", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM department WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox1.Clear();
                


                loadDep();
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Branch branch = (Branch)comboBox1.SelectedItem;

                    Group1 group = new Group1(textBox4.Text, branch.Id);

                    textBox4.Clear();
                    




                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO groupe (name,idbranch)" + "VALUES(@name,@idbranch)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@name", group.Name);
                    
                    cmd.Parameters.AddWithValue("@idbranch", group.Idbranch);



                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    loadGRP();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView2.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

            textBox4.Text = row.Cells[1].Value.ToString();
            
            comboBox1.DataSource = branchService.findByObj2(row.Cells[2].Value.ToString());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this groupe ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if ( textBox4.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    Branch branch = (Branch)comboBox1.SelectedItem;

                    Group1 group = new Group1(textBox4.Text, branch.Id);

                    textBox4.Clear();


                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE groupe SET name= @name,idbranch=idbranch" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@name", group.Name);

                    cmd.Parameters.AddWithValue("@idbranch", group.Idbranch);

                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();

                    loadGRP();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this groupe", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM groupe WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
            
                textBox4.Clear();


                loadGRP();
            }
        }
    }
}
