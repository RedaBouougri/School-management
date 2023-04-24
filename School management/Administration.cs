using Guna.UI2.WinForms;
using iTextSharp.text.pdf;
using iTextSharp.text;
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

namespace School_management
{
    public partial class Administration : UserControl
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;

        UserService userService = new UserService();
        public Administration()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            guna2DataGridView1.DataSource = userService.findAll();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" )
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {

                    User user = new User(textBox1.Text,textBox2.Text,comboBox1.Text);

                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.Text="";
                   


                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO user (login,password,role)" + "VALUES(@login,@password,@role)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@login", user.Login);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);

                   
                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this user ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {


                    User user = new User(textBox1.Text, textBox2.Text, comboBox1.Text);

                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.Text = "";

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE user SET login= @login, password=@password, role=@role" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@login", user.Login);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);





                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                   
                    load();
                }
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            comboBox1.Text = row.Cells[3].Value.ToString();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this user", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM user WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Text="";
               
                load();
            }
        }

        private void Administration_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count > 0)

            {

                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "PDF (*.pdf)|*.pdf";

                save.FileName = "Result.pdf";

                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)

                {

                    if (File.Exists(save.FileName))

                    {

                        try

                        {

                            File.Delete(save.FileName);

                        }

                        catch (Exception ex)

                        {

                            ErrorMessage = true;

                            MessageBox.Show("Unable to wride data in disk" + ex.Message);

                        }

                    }

                    if (!ErrorMessage)

                    {

                        try

                        {
                            cnx.connexion();
                            cnx.cnxOpen();
                            string request = "select  login,role from user";
                            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
                            MySqlDataReader dr = cmd.ExecuteReader();

                            PdfPTable pTable = new PdfPTable(dr.FieldCount);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;






                            PdfPCell pCell = new PdfPCell(new Phrase("login"));

                            pTable.AddCell(pCell);

                           
                            PdfPCell pCell3 = new PdfPCell(new Phrase("role"));

                            pTable.AddCell(pCell3);







                            while (dr.Read())
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    pTable.AddCell(dr[i].ToString());
                                }
                            }
                            /*foreach (DataGridViewColumn col in dataGridView2.Columns)

                            {

                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));

                                pTable.AddCell(pCell);

                            }

                            foreach (DataGridViewRow viewRow in dataGridView2.Rows)

                            {

                                foreach (DataGridViewCell dcell in viewRow.Cells)

                                {

                                    pTable.AddCell(Convert.ToString(dcell.Value));

                                }

                            }*/

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))

                            {

                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);

                                PdfWriter.GetInstance(document, fileStream);

                                document.Open();

                                document.Add(pTable);

                                document.Close();

                                fileStream.Close();

                            }

                            MessageBox.Show("Data Export Successfully", "info");

                        }

                        catch (Exception ex)

                        {

                            MessageBox.Show("Error while exporting Data" + ex.Message);

                        }

                    }

                }

            }

            else

            {

                MessageBox.Show("No Record Found", "Info");

            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  login,password,role from user";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<User> list = new List<User>();
            while (dr.Read())
            {
                User cl = new User();
                cl.Login = dr[0].ToString();
                cl.Password = dr[1].ToString();
                cl.Role = dr[2].ToString();
               

                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.Login, x.Password, x.Role}).Where(x => x.Login.Contains(textBox6.Text) | x.Password.Contains(textBox6.Text) | x.Role.Contains(textBox6.Text) ).ToList();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  login,password,role from user";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<User> list = new List<User>();
            while (dr.Read())
            {
                User cl = new User();
                cl.Login = dr[0].ToString();
                cl.Password = dr[1].ToString();
                cl.Role = dr[2].ToString();


                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.Login, x.Password, x.Role }).Where(x => x.Login.Contains(textBox6.Text) | x.Password.Contains(textBox6.Text) | x.Role.Contains(textBox6.Text)).ToList();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count > 0)

            {

                SaveFileDialog save = new SaveFileDialog();

                save.Filter = "PDF (*.pdf)|*.pdf";

                save.FileName = "Result.pdf";

                bool ErrorMessage = false;

                if (save.ShowDialog() == DialogResult.OK)

                {

                    if (File.Exists(save.FileName))

                    {

                        try

                        {

                            File.Delete(save.FileName);

                        }

                        catch (Exception ex)

                        {

                            ErrorMessage = true;

                            MessageBox.Show("Unable to wride data in disk" + ex.Message);

                        }

                    }

                    if (!ErrorMessage)

                    {

                        try

                        {
                            cnx.connexion();
                            cnx.cnxOpen();
                            string request = "select  login,role from user";
                            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
                            MySqlDataReader dr = cmd.ExecuteReader();

                            PdfPTable pTable = new PdfPTable(dr.FieldCount);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;






                            PdfPCell pCell = new PdfPCell(new Phrase("login"));

                            pTable.AddCell(pCell);


                            PdfPCell pCell3 = new PdfPCell(new Phrase("role"));

                            pTable.AddCell(pCell3);







                            while (dr.Read())
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    pTable.AddCell(dr[i].ToString());
                                }
                            }
                           

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))

                            {

                                Document document = new Document(PageSize.A4, 8f, 16f, 16f, 8f);
                                
                                PdfWriter.GetInstance(document, fileStream);

                                document.Open();
                                Paragraph header = new Paragraph("User Information");
                                header.Alignment = Element.ALIGN_CENTER;
                                document.Add(header);
                                Paragraph header1 = new Paragraph("          ");
                                header.Alignment = Element.ALIGN_CENTER;
                                document.Add(header1);
                                Paragraph header2 = new Paragraph("        ");
                                header.Alignment = Element.ALIGN_CENTER;
                                document.Add(header2);
                                Paragraph header3 = new Paragraph("        ");
                                header.Alignment = Element.ALIGN_CENTER;
                                document.Add(header3);
                                document.Add(pTable);

                                document.Close();

                                fileStream.Close();

                            }

                            MessageBox.Show("Data Export Successfully", "info");

                        }

                        catch (Exception ex)

                        {

                            MessageBox.Show("Error while exporting Data" + ex.Message);

                        }

                    }

                }

            }

            else

            {

                MessageBox.Show("No Record Found", "Info");

            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {

                    User user = new User(textBox1.Text, textBox2.Text, comboBox1.Text);

                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.Text = "";



                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO user (login,password,role)" + "VALUES(@login,@password,@role)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@login", user.Login);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);


                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this user ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {


                    User user = new User(textBox1.Text, textBox2.Text, comboBox1.Text);

                    textBox1.Clear();
                    textBox2.Clear();
                    comboBox1.Text = "";

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE user SET login= @login, password=@password, role=@role" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@login", user.Login);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@role", user.Role);





                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();

                    load();
                }
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this user", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM user WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox1.Clear();
                textBox2.Clear();
                comboBox1.Text = "";

                load();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  login,password,role from user";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<User> list = new List<User>();
            while (dr.Read())
            {
                User cl = new User();
                cl.Login = dr[0].ToString();
                cl.Password = dr[1].ToString();
                cl.Role = dr[2].ToString();


                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.Login, x.Password, x.Role }).Where(x => x.Login.Contains(textBox6.Text) | x.Password.Contains(textBox6.Text) | x.Role.Contains(textBox6.Text)).ToList();
        }
    }
}
