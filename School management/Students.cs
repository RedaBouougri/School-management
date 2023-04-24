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
using Image = System.Drawing.Image;
using MySqlX.XDevAPI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace School_management
{
    public partial class Students : UserControl
    {
        BranchService branchService = new BranchService();
        GroupService groupService = new GroupService();
        ModulService moduleService = new ModulService();
        StudentService studentService = new StudentService();
        
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt;
        int currRowIndex;
        public Students()
        {
            InitializeComponent();
           
            comboBox1.DataSource = branchService.findAll();
            load();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Branch branch = (Branch)comboBox1.SelectedItem;
            comboBox2.DataSource = groupService.findByBranchId(branch.Id);
        }

        public void load()
        {
            
            
            guna2DataGridView1.DataSource = studentService.findAll();
        }

        

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            byte[] image = img;
            String email = textBox5.Text.ToString();
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox12.Text == "" || textBox10.Text == "" )
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

                    Branch branch = (Branch)comboBox1.SelectedItem;
                    Group1 group = (Group1)comboBox2.SelectedItem;
                    Student etudiant = new Student(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, img, guna2DateTimePicker1.Value.Date,Convert.ToInt32(textBox12.Text), branch.Id, textBox10.Text,group.Id);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox10.Clear();
                    textBox12.Clear();
                    pictureBox1.Image = null;


                    cnx.connexion();
                    cnx.cnxOpen();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO student (firstname,lastname,cin,cne,email,pic,dateofbirth,telephone,idbranch,address,idgroup)" + "VALUES(@firstname,@lastname,@cin,@cne,@email,@pic,@dateofbirth,@telephone,@idbranch,@address,@idgroup)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@firstname", etudiant.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", etudiant.LastName);
                    cmd.Parameters.AddWithValue("@email", etudiant.Email);
                    cmd.Parameters.AddWithValue("@address", etudiant.Address);
                    cmd.Parameters.Add("@pic", MySqlDbType.Blob);
                    cmd.Parameters["@pic"].Value = image;
                    cmd.Parameters.AddWithValue("@cin", etudiant.Cin);
                    cmd.Parameters.AddWithValue("@cne", etudiant.Cne);
                    cmd.Parameters.AddWithValue("@dateofbirth", etudiant.Dateofbirth);
                    cmd.Parameters.AddWithValue("@idgroup", etudiant.Idgroup);
                    cmd.Parameters.AddWithValue("@idbranch", etudiant.Idbranch);
                    cmd.Parameters.AddWithValue("@telephone", etudiant.Telephone);
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

        byte[] img;

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

      
        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this student ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {
                String email = textBox5.Text.ToString();
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox12.Text == "" || textBox10.Text == "")
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

                    Branch branch = (Branch)comboBox1.SelectedItem;
                    Group1 group = (Group1)comboBox2.SelectedItem;
                    Student etudiant = new Student(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, img, guna2DateTimePicker1.Value.Date, Convert.ToInt32(textBox12.Text), branch.Id, textBox10.Text, group.Id);

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE student SET firstname= @firstname, lastname=@lastname, email=@email,address=@address, cin=@cin, cne=@cne,dateofbirth=@dateofbirth, idgroup=@idgroup, idbranch=@idbranch,telephone=@telephone ,pic=@pic" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@firstname", etudiant.FirstName);
                    cmd.Parameters.AddWithValue("@lastname", etudiant.LastName);
                    cmd.Parameters.AddWithValue("@email", etudiant.Email);
                    cmd.Parameters.AddWithValue("@address", etudiant.Address);
                    cmd.Parameters.Add("@pic", MySqlDbType.Blob);
                    cmd.Parameters["@pic"].Value = img;
                    cmd.Parameters.AddWithValue("@cin", etudiant.Cin);
                    cmd.Parameters.AddWithValue("@cne", etudiant.Cne);
                    cmd.Parameters.AddWithValue("@dateofbirth", etudiant.Dateofbirth);
                    cmd.Parameters.AddWithValue("@idgroup", etudiant.Idgroup);
                    cmd.Parameters.AddWithValue("@idbranch", etudiant.Idbranch);
                    cmd.Parameters.AddWithValue("@telephone", etudiant.Telephone);





                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                    textBox10.Clear();
                    textBox12.Clear();
                    pictureBox1.Image = null;
                    load();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this student", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM student WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox10.Clear();
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
                            string request = "select  s.firstname,s.lastname,s.cin,s.cne,s.email,s.telephone,b.name as 'branch',s.address,g.name as 'group' from student s,branch  b,groupe g where s.idbranch=b.id  and s.idgroup=g.id";
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
                            PdfPCell pCell5 = new PdfPCell(new Phrase("cne"));

                            pTable.AddCell(pCell5);
                           

                            PdfPCell pCell6 = new PdfPCell(new Phrase("mail"));

                            pTable.AddCell(pCell6);

                           

                            PdfPCell pCell2 = new PdfPCell(new Phrase("telephone"));

                            pTable.AddCell(pCell2);
                            PdfPCell pCell8 = new PdfPCell(new Phrase("branch"));

                            pTable.AddCell(pCell8);

                            PdfPCell pCell9 = new PdfPCell(new Phrase("address"));

                            pTable.AddCell(pCell9);

                            PdfPCell pCell10 = new PdfPCell(new Phrase("group"));

                            pTable.AddCell(pCell10);



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
                                Paragraph header = new Paragraph("Students");
                                header.Alignment = Element.ALIGN_CENTER;
                                document.Add(header);
                                Paragraph header1 = new Paragraph("       ");
                                header1.Alignment = Element.ALIGN_CENTER;
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

        private void iconButton1_Click(object sender, EventArgs e)
        {
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  s.id,s.firstname,s.lastname,s.cin,s.cne,s.telephone,b.name as 'branch',g.name as 'group' from student s,branch  b,groupe g where s.idbranch=b.id  and s.idgroup=g.id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<StudentVo> list = new List<StudentVo>();
            while (dr.Read())
            {
                StudentVo cl = new StudentVo();
                cl.FirstName = dr[1].ToString();
                cl.LastName = dr[2].ToString();
                cl.Cin = dr[3].ToString();
                cl.Cne = dr[4].ToString();
               
                cl.Telephone = Convert.ToInt32(dr[5].ToString());
                cl.Branch = dr[6].ToString();
                cl.Group = dr[7].ToString();
                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.FirstName, x.LastName, x.Cin, x.Cne,x.Telephone,x.Branch,x.Group }).Where(x => x.FirstName.Contains(textBox6.Text) | x.LastName.Contains(textBox6.Text) | x.Cin.Contains(textBox6.Text) | x.Cne.Contains(textBox6.Text)  | x.Group.Contains(textBox6.Text) | x.Branch.Contains(textBox6.Text)).ToList();
        }

        private void Students_Load(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

            textBox1.Text = row.Cells["dataGridViewTextBoxColumn1"].Value.ToString();
            textBox2.Text = row.Cells["dataGridViewTextBoxColumn2"].Value.ToString();
            textBox3.Text = row.Cells["dataGridViewTextBoxColumn3"].Value.ToString();
            textBox4.Text = row.Cells["dataGridViewTextBoxColumn4"].Value.ToString();
            textBox5.Text = row.Cells["dataGridViewTextBoxColumn5"].Value.ToString();
            textBox12.Text = row.Cells["dataGridViewTextBoxColumn6"].Value.ToString();
            textBox10.Text = row.Cells["dataGridViewTextBoxColumn8"].Value.ToString();
            guna2DateTimePicker1.Value = Convert.ToDateTime(row.Cells["dataGridViewTextBoxColumn9"].Value);

            cnx.connexion();
            cnx.cnxOpen();
            String query = "select idgroup,idbranch from student where cin='" + textBox3.Text.ToString() + "'";
            MySqlCommand cmd = new MySqlCommand(query, cnx.connMaster);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);
            int id1 = (int)table.Rows[0][0];
            int id2 = (int)table.Rows[0][1];
            comboBox1.DataSource = branchService.findByObj2(row.Cells["Column1"].Value.ToString());
            comboBox2.DataSource = groupService.findByObj2(row.Cells["dataGridViewTextBoxColumn7"].Value.ToString());
            cnx.cnxClose();
            cnx.connexion();
            cnx.cnxOpen();
            String query2 = "select pic from student where cin='" + textBox3.Text.ToString() + "'";
            MySqlCommand cmd2 = new MySqlCommand(query2, cnx.connMaster);
            MySqlDataAdapter adapter2 = new MySqlDataAdapter(cmd2);
            DataTable table2 = new DataTable();
            adapter2.Fill(table2);
            byte[] imgg = (byte[])table2.Rows[0][0];
            MemoryStream ms = new MemoryStream(imgg);
            
            
            pictureBox1.Image = System.Drawing.Image.FromStream(ms);
            cnx.cnxClose();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  s.id,s.firstname,s.lastname,s.cin,s.cne,s.telephone,b.name as 'branch',g.name as 'group' from student s,branch  b,groupe g where s.idbranch=b.id  and s.idgroup=g.id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<StudentVo> list = new List<StudentVo>();
            while (dr.Read())
            {
                StudentVo cl = new StudentVo();
                cl.FirstName = dr[1].ToString();
                cl.LastName = dr[2].ToString();
                cl.Cin = dr[3].ToString();
                cl.Cne = dr[4].ToString();

                cl.Telephone = Convert.ToInt32(dr[5].ToString());
                cl.Branch = dr[6].ToString();
                cl.Group = dr[7].ToString();
                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.FirstName, x.LastName, x.Cin, x.Cne, x.Telephone, x.Branch, x.Group }).Where(x => x.FirstName.Contains(textBox6.Text) | x.LastName.Contains(textBox6.Text) | x.Cin.Contains(textBox6.Text) | x.Cne.Contains(textBox6.Text) | x.Group.Contains(textBox6.Text) | x.Branch.Contains(textBox6.Text)).ToList();
        }
        

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  s.id,s.firstname,s.lastname,s.cin,s.cne,s.telephone,b.name as 'branch',g.name as 'group' from student s,branch  b,groupe g where s.idbranch=b.id  and s.idgroup=g.id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<StudentVo> list = new List<StudentVo>();
            while (dr.Read())
            {
                StudentVo cl = new StudentVo();
                cl.FirstName = dr[1].ToString();
                cl.LastName = dr[2].ToString();
                cl.Cin = dr[3].ToString();
                cl.Cne = dr[4].ToString();

                cl.Telephone = Convert.ToInt32(dr[5].ToString());
                cl.Branch = dr[6].ToString();
                cl.Group = dr[7].ToString();
                list.Add(cl);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.FirstName, x.LastName, x.Cin, x.Cne, x.Telephone, x.Branch, x.Group }).Where(x => x.FirstName.Contains(textBox6.Text) | x.LastName.Contains(textBox6.Text) | x.Cin.Contains(textBox6.Text) | x.Cne.Contains(textBox6.Text) | x.Group.Contains(textBox6.Text) | x.Branch.Contains(textBox6.Text)).ToList();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            comboBox1.DataSource = branchService.findAll();
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            Branch branch = (Branch)comboBox1.SelectedItem;
            comboBox2.DataSource = groupService.findByBranchId(branch.Id);
        }
    }
    }
