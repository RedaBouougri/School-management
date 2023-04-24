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
using Image = System.Drawing.Image;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace School_management
{


    public partial class Teachers : UserControl
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;
        byte[] img;
        DataTable dt;
        int currRowIndex;

        DepartmentService departmentService = new DepartmentService();
        TeacherService teacherService = new TeacherService();
        public Teachers()
        {
            InitializeComponent();
            comboBox1.DataSource = departmentService.findAll();
            load();
        }
        public void load()
        {

           
            guna2DataGridView1.DataSource = teacherService.findAll();
        }


        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            byte[] image = img;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox12.Text == "" )
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {

                    Department dep = (Department)comboBox1.SelectedItem;
                    Teacher teacher = new Teacher(textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text,image, guna2DateTimePicker1.Value.Date,Convert.ToInt32( textBox12.Text), textBox4.Text, dep.Id);
                    
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                 
                    textBox12.Clear();
                    pictureBox1.Image = null;


                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin,email,pic,dateofbirth,telephone,iddepartment,speciality)" + "VALUES(@firstname,@lastname,@cin,@email,@pic,@dateofbirth,@telephone,@iddepartment,@speciality)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@firstname", teacher.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", teacher.LastName);
                    cmd.Parameters.AddWithValue("@email", teacher.Email);
                    
                    cmd.Parameters.Add("@pic", MySqlDbType.Blob);
                    cmd.Parameters["@pic"].Value = image;
                    cmd.Parameters.AddWithValue("@cin", teacher.Cin);
                    
                    cmd.Parameters.AddWithValue("@dateofbirth", teacher.Dateofbirth);
                    cmd.Parameters.AddWithValue("@iddepartment", teacher.Iddepartement);
                    cmd.Parameters.AddWithValue("@speciality", teacher.Speciality);
                    cmd.Parameters.AddWithValue("@telephone", teacher.Telephone);
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

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Image image;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "All Files |*.*|JPG|*.jpg|PNG|*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = System.Drawing.Image.FromFile(ofd.FileName);
                    image = System.Drawing.Image.FromFile(ofd.FileName);
                    var ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] i = ms.ToArray();
                    img = i;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this teacher ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox12.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    var ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] i = ms.ToArray();
                    img = i;

                    Department dep = (Department)comboBox1.SelectedItem;

                    Teacher teacher = new Teacher(textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text, img, guna2DateTimePicker1.Value.Date, Convert.ToInt32(textBox12.Text), textBox4.Text, dep.Id);

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE teacher SET firstname= @firstname, lastname=@lastname, email=@email, cin=@cin,dateofbirth=@dateofbirth, iddepartment=@iddepartment,telephone=@telephone ,pic=@pic,speciality=@speciality" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@firstname", teacher.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", teacher.LastName);
                    cmd.Parameters.AddWithValue("@email", teacher.Email);

                    cmd.Parameters.Add("@pic", MySqlDbType.Blob);
                    cmd.Parameters["@pic"].Value = img;
                    cmd.Parameters.AddWithValue("@cin", teacher.Cin);

                    cmd.Parameters.AddWithValue("@dateofbirth", teacher.Dateofbirth);
                    cmd.Parameters.AddWithValue("@iddepartment", teacher.Iddepartement);
                    cmd.Parameters.AddWithValue("@speciality", teacher.Speciality);
                    cmd.Parameters.AddWithValue("@telephone", teacher.Telephone);





                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    
                    textBox12.Clear();
                    pictureBox1.Image = null;
                    load();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this teacher", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM teacher WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
              
                textBox12.Clear();
                load();
            }
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
                            string request = "select  t.firstname,t.lastname,t.cin,t.email,d.name as 'department',t.speciality from teacher t,department d   where t.iddepartment=d.id ";
                            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
                            MySqlDataReader dr = cmd.ExecuteReader();

                            PdfPTable pTable = new PdfPTable(dr.FieldCount);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;






                            PdfPCell pCell = new PdfPCell(new Phrase("firstName"));

                            pTable.AddCell(pCell);

                            PdfPCell pCell1 = new PdfPCell(new Phrase("Lastname"));

                            pTable.AddCell(pCell1);
                            PdfPCell pCell3 = new PdfPCell(new Phrase("cin"));

                            pTable.AddCell(pCell3);
                            


                            PdfPCell pCell6 = new PdfPCell(new Phrase("mail"));

                            pTable.AddCell(pCell6);



                            PdfPCell pCell2 = new PdfPCell(new Phrase("department"));

                            pTable.AddCell(pCell2);
                            PdfPCell pCell8 = new PdfPCell(new Phrase("speciality"));

                            pTable.AddCell(pCell8);

                           



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

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

            textBox1.Text = row.Cells["dataGridViewTextBoxColumn1"].Value.ToString();
            textBox2.Text = row.Cells["dataGridViewTextBoxColumn2"].Value.ToString();
            textBox3.Text = row.Cells["dataGridViewTextBoxColumn3"].Value.ToString();
            textBox4.Text = row.Cells["dataGridViewTextBoxColumn8"].Value.ToString();
            textBox5.Text = row.Cells["dataGridViewTextBoxColumn4"].Value.ToString();
            textBox12.Text = row.Cells["dataGridViewTextBoxColumn5"].Value.ToString();

            guna2DateTimePicker1.Value = Convert.ToDateTime(row.Cells["dataGridViewTextBoxColumn7"].Value);

            cnx.connexion();
            cnx.cnxOpen();
            String query = "select iddepartment from teacher where cin='" + textBox3.Text.ToString() + "'";
            MySqlCommand cmd = new MySqlCommand(query, cnx.connMaster);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            int id1 = (int)table.Rows[0][0];

            comboBox1.DataSource  = departmentService.findByObj2(row.Cells["dataGridViewTextBoxColumn6"].Value.ToString());

            cnx.cnxClose();
            cnx.connexion();
            cnx.cnxOpen();
            String query2 = "select pic from teacher where cin='" + textBox3.Text.ToString() + "'";
            MySqlCommand cmd2 = new MySqlCommand(query2, cnx.connMaster);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd2);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);
            byte[] imgg = (byte[])table2.Rows[0][0];
            MemoryStream ms = new MemoryStream(imgg);

            pictureBox1.Image = System.Drawing.Image.FromStream(ms);
            cnx.cnxClose();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  t.id,t.firstname,t.lastname,t.cin,t.email,d.name as 'department',t.speciality from teacher t,department d   where t.iddepartment=d.id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<TeacherVo> list = new List<TeacherVo>();
            while (dr.Read())
            {
                TeacherVo cl = new TeacherVo();
                cl.FirstName = dr[1].ToString();
                cl.LastName = dr[2].ToString();
                cl.Cin = dr[3].ToString();
                cl.Email = dr[4].ToString();
                cl.Departement = dr[5].ToString();
                cl.Speciality = dr[6].ToString();
                
                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.FirstName, x.LastName, x.Cin, x.Email, x.Departement, x.Speciality }).Where(x => x.FirstName.Contains(textBox6.Text) | x.LastName.Contains(textBox6.Text) | x.Cin.Contains(textBox6.Text) | x.Email.Contains(textBox6.Text) | x.Departement.Contains(textBox6.Text) | x.Speciality.Contains(textBox6.Text)).ToList();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  t.id,t.firstname,t.lastname,t.cin,t.email,d.name as 'department',t.speciality from teacher t,department d   where t.iddepartment=d.id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<TeacherVo> list = new List<TeacherVo>();
            while (dr.Read())
            {
                TeacherVo cl = new TeacherVo();
                cl.FirstName = dr[1].ToString();
                cl.LastName = dr[2].ToString();
                cl.Cin = dr[3].ToString();
                cl.Email = dr[4].ToString();
                cl.Departement = dr[5].ToString();
                cl.Speciality = dr[6].ToString();

                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.FirstName, x.LastName, x.Cin, x.Email, x.Departement, x.Speciality }).Where(x => x.FirstName.Contains(textBox6.Text) | x.LastName.Contains(textBox6.Text) | x.Cin.Contains(textBox6.Text) | x.Email.Contains(textBox6.Text) | x.Departement.Contains(textBox6.Text) | x.Speciality.Contains(textBox6.Text)).ToList();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
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
                            string request = "select  t.firstname,t.lastname,t.cin,t.email,d.name as 'department',t.speciality from teacher t,department d   where t.iddepartment=d.id ";
                            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
                            MySqlDataReader dr = cmd.ExecuteReader();

                            PdfPTable pTable = new PdfPTable(dr.FieldCount);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;






                            PdfPCell pCell = new PdfPCell(new Phrase("firstName"));

                            pTable.AddCell(pCell);

                            PdfPCell pCell1 = new PdfPCell(new Phrase("Lastname"));

                            pTable.AddCell(pCell1);
                            PdfPCell pCell3 = new PdfPCell(new Phrase("cin"));

                            pTable.AddCell(pCell3);



                            PdfPCell pCell6 = new PdfPCell(new Phrase("mail"));

                            pTable.AddCell(pCell6);



                            PdfPCell pCell2 = new PdfPCell(new Phrase("department"));

                            pTable.AddCell(pCell2);
                            PdfPCell pCell8 = new PdfPCell(new Phrase("speciality"));

                            pTable.AddCell(pCell8);





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
                                Paragraph header = new Paragraph("Teachers");
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

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            byte[] image = img;
            String email = textBox5.Text.ToString();
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox12.Text == "")
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (email == "" || !(email.Contains("@")))
            {

                DialogResult dialogClose = MessageBox.Show("Please enter a valid mail format", "Invalid mail format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {

                    Department dep = (Department)comboBox1.SelectedItem;
                    Teacher teacher = new Teacher(textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text, image, guna2DateTimePicker1.Value.Date, Convert.ToInt32(textBox12.Text), textBox4.Text, dep.Id);

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();

                    textBox12.Clear();
                    pictureBox1.Image = null;


                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin,email,pic,dateofbirth,telephone,iddepartment,speciality)" + "VALUES(@firstname,@lastname,@cin,@email,@pic,@dateofbirth,@telephone,@iddepartment,@speciality)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@firstname", teacher.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", teacher.LastName);
                    cmd.Parameters.AddWithValue("@email", teacher.Email);

                    cmd.Parameters.Add("@pic", MySqlDbType.Blob);
                    cmd.Parameters["@pic"].Value = image;
                    cmd.Parameters.AddWithValue("@cin", teacher.Cin);

                    cmd.Parameters.AddWithValue("@dateofbirth", teacher.Dateofbirth);
                    cmd.Parameters.AddWithValue("@iddepartment", teacher.Iddepartement);
                    cmd.Parameters.AddWithValue("@speciality", teacher.Speciality);
                    cmd.Parameters.AddWithValue("@telephone", teacher.Telephone);
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

        private void guna2Button9_Click(object sender, EventArgs e)
        {  
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this teacher ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {
                String email = textBox5.Text.ToString();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox12.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else if (email == "" || !(email.Contains("@")))
                {

                    DialogResult dialogClose = MessageBox.Show("Please enter a valid mail format", "Invalid mail format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] i = ms.ToArray();
                    img = i;

                    Department dep = (Department)comboBox1.SelectedItem;

                    Teacher teacher = new Teacher(textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text, img, guna2DateTimePicker1.Value.Date, Convert.ToInt32(textBox12.Text), textBox4.Text, dep.Id);

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE teacher SET firstname= @firstname, lastname=@lastname, email=@email, cin=@cin,dateofbirth=@dateofbirth, iddepartment=@iddepartment,telephone=@telephone ,pic=@pic,speciality=@speciality" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@firstname", teacher.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", teacher.LastName);
                    cmd.Parameters.AddWithValue("@email", teacher.Email);

                    cmd.Parameters.Add("@pic", MySqlDbType.Blob);
                    cmd.Parameters["@pic"].Value = img;
                    cmd.Parameters.AddWithValue("@cin", teacher.Cin);

                    cmd.Parameters.AddWithValue("@dateofbirth", teacher.Dateofbirth);
                    cmd.Parameters.AddWithValue("@iddepartment", teacher.Iddepartement);
                    cmd.Parameters.AddWithValue("@speciality", teacher.Speciality);
                    cmd.Parameters.AddWithValue("@telephone", teacher.Telephone);





                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();

                    textBox12.Clear();
                    pictureBox1.Image = null;
                    load();
                }
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this teacher", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM teacher WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();

                textBox12.Clear();
                load();
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Image image;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "All Files |*.*|JPG|*.jpg|PNG|*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = System.Drawing.Image.FromFile(ofd.FileName);
                    image = System.Drawing.Image.FromFile(ofd.FileName);
                    var ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] i = ms.ToArray();
                    img = i;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  t.id,t.firstname,t.lastname,t.cin,t.email,d.name as 'department',t.speciality from teacher t,department d   where t.iddepartment=d.id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<TeacherVo> list = new List<TeacherVo>();
            while (dr.Read())
            {
                TeacherVo cl = new TeacherVo();
                cl.FirstName = dr[1].ToString();
                cl.LastName = dr[2].ToString();
                cl.Cin = dr[3].ToString();
                cl.Email = dr[4].ToString();
                cl.Departement = dr[5].ToString();
                cl.Speciality = dr[6].ToString();

                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.FirstName, x.LastName, x.Cin, x.Email, x.Departement, x.Speciality }).Where(x => x.FirstName.Contains(textBox6.Text) | x.LastName.Contains(textBox6.Text) | x.Cin.Contains(textBox6.Text) | x.Email.Contains(textBox6.Text) | x.Departement.Contains(textBox6.Text) | x.Speciality.Contains(textBox6.Text)).ToList();
        }

        private void Teachers_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.DataSource = departmentService.findAll();
        }
    }
}
