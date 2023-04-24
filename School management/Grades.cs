using Guna.UI2.WinForms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using School_management.db;
using School_management.entities;
using School_management.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using Branch = School_management.entities.Branch;
using Module = System.Reflection.Module;

namespace School_management
{
    public partial class Grades : UserControl
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;

        StudentService studentService = new StudentService();
        GroupService groupservice = new GroupService();
        BranchService branchService = new BranchService();
        ModulService modulService = new ModulService();
        GradeService gradeService = new GradeService();

        public Grades()
        {
            InitializeComponent();
            comboBox2.DataSource = branchService.findAll();
            load();
            //comboBox5.DataSource = studentService.findByObj2("ja188311");
        }

        public void load()
        {

              entities.Module module = (entities.Module)comboBox1.SelectedItem;
              Branch branch = (Branch)comboBox2.SelectedItem;
              Group1 group1 = (Group1)comboBox3.SelectedItem;

              guna2DataGridView1.DataSource = gradeService.find(group1.Id, branch.Id, module.Id);
           
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
                            Branch branch = (Branch)comboBox2.SelectedItem;
                            Group1 group = (Group1)comboBox3.SelectedItem;
                            entities.Module module = (entities.Module)comboBox1.SelectedItem;
                            string request = "select  s.firstname as 'student',s.cin , m.name as 'module',b.name as 'branch',g.name as 'group',gr.grade from student s,branch  b,groupe g ,module m , grade gr where gr.idstudent=s.id and gr.idmodule=m.id and gr.idbranch=b.id and gr.idgroupe=g.id and gr.idgroupe=@idgroup and gr.idbranch=@idbranch and gr.idmodule=@idmodule ";
                            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
                            cmd.Parameters.AddWithValue("@idbranch", branch.Id);
                            cmd.Parameters.AddWithValue("@idgroup", group.Id);
                            cmd.Parameters.AddWithValue("@idmodule", module.Id);
                            MySqlDataReader dr = cmd.ExecuteReader();

                            PdfPTable pTable = new PdfPTable(dr.FieldCount);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;






                            PdfPCell pCell = new PdfPCell(new Phrase("student"));

                            pTable.AddCell(pCell);


                            PdfPCell pCell3 = new PdfPCell(new Phrase("cin"));

                            pTable.AddCell(pCell3);

                            PdfPCell pCell4 = new PdfPCell(new Phrase("module"));

                            pTable.AddCell(pCell4);


                            PdfPCell pCell5 = new PdfPCell(new Phrase("branch"));

                            pTable.AddCell(pCell5);

                            PdfPCell pCell6 = new PdfPCell(new Phrase("group"));

                            pTable.AddCell(pCell6);

                            PdfPCell pCell7 = new PdfPCell(new Phrase("grade"));

                            pTable.AddCell(pCell7);







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
                                Paragraph header = new Paragraph("Grades");
                                header.Alignment = Element.ALIGN_CENTER;
                                document.Add(header);
                                Paragraph header1 = new Paragraph("    module :   "+ comboBox1.Text+"     "+ "group :    " + comboBox3.Text+"     "+ "branch :     " + comboBox2.Text);
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            comboBox4.Text = "";
            comboBox3.Text = "";
            comboBox1.Text = "";
            Branch branch = (Branch)comboBox2.SelectedItem;
         
            comboBox3.DataSource = groupservice.findByBranchId(branch.Id);
            comboBox1.DataSource = modulService.findByBranchId(branch.Id);
            Group1 group1 = (Group1)comboBox3.SelectedItem;
           comboBox4.DataSource = studentService.findByBranchId(branch.Id, group1.Id);


        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Text = "";
            Branch branch = (Branch)comboBox2.SelectedItem;
            Group1 group1 = (Group1)comboBox3.SelectedItem;
           comboBox4.DataSource = studentService.findByBranchId(branch.Id,group1.Id);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Text = "";
            Branch branch = (Branch)comboBox2.SelectedItem;
            Group1 group1 = (Group1)comboBox3.SelectedItem;
            comboBox4.DataSource = studentService.findByBranchId(branch.Id, group1.Id);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            load();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" )
            {
                DialogResult dialogClose = MessageBox.Show("Please fill all the fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                try
                {
                    Branch branch = (Branch)comboBox2.SelectedItem;
                    Group1 group = (Group1)comboBox3.SelectedItem;
                    entities.Module  module = (entities.Module)comboBox1.SelectedItem;
                    Student student = (Student)comboBox4.SelectedItem;

                    

                       

                        Grade grade = new Grade(student.Id, module.Id,Convert.ToDouble( textBox1.Text.ToString()), branch.Id, group.Id);



                        cnx.connexion();
                        cnx.cnxOpen();
                        //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO grade (idstudent,idmodule,idbranch,idgroupe,grade)" + "VALUES(@idstudent,@idmodule,@idbranch,@idgroupe,@grade)", cnx.connMaster);
                        cmd.Parameters.AddWithValue("@idstudent", grade.Idstudent);
                        cmd.Parameters.AddWithValue("@idmodule", grade.Idmodule);
                        cmd.Parameters.AddWithValue("@idbranch", grade.Idbranch);
                        cmd.Parameters.AddWithValue("@idgroupe", grade.Idgroupe);
                        cmd.Parameters.AddWithValue("@grade", grade.Grade1);
             


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

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

            textBox1.Text = row.Cells["dataGridViewTextBoxColumn5"].Value.ToString();
           /* comboBox3.DataSource = groupservice.findByObj2(row.Cells["dataGridViewTextBoxColumn4"].Value.ToString());
             comboBox2.DataSource = branchService.findByObj2(row.Cells["dataGridViewTextBoxColumn3"].Value.ToString());
             comboBox1.DataSource = modulService.findByObj2(row.Cells["dataGridViewTextBoxColumn2"].Value.ToString());*/
            comboBox4.DataSource = studentService.findByObj2(row.Cells["cin"].Value.ToString());

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this grade ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox1.Text == "" )
                {
                    DialogResult dialogClose = MessageBox.Show("Please fill all fields", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    Branch branch = (Branch)comboBox2.SelectedItem;
                    Group1 group = (Group1)comboBox3.SelectedItem;
                    entities.Module module = (entities.Module)comboBox1.SelectedItem;
                    Student student = (Student)comboBox4.SelectedItem;


                    Grade grade = new Grade(student.Id, module.Id, Convert.ToDouble(textBox1.Text.ToString()), branch.Id, group.Id);

                    cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE grade SET idstudent= @idstudent, idmodule=@idmodule, idbranch=@idbranch, idgroupe=@idgroupe,grade=@grade" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@idstudent", grade.Idstudent);
                    cmd.Parameters.AddWithValue("@idmodule", grade.Idmodule);
                    cmd.Parameters.AddWithValue("@idbranch", grade.Idbranch);
                    cmd.Parameters.AddWithValue("@idgroupe", grade.Idgroupe);
                    cmd.Parameters.AddWithValue("@grade", grade.Grade1);

                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                    textBox1.Clear();
                    load();

                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            DialogResult dialogDelete = MessageBox.Show("Do you really want to delete this grade", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {

                cnx.connexion();
                cnx.cnxOpen();
                MySqlCommand cmd = cnx.connMaster.CreateCommand();
                cmd.CommandText = "DELETE FROM grade WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                cnx.cnxClose();
                textBox1.Clear();
               
                load();
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Branch branch = (Branch)comboBox2.SelectedItem;
            Group1 group = (Group1)comboBox3.SelectedItem;
            entities.Module module = (entities.Module)comboBox1.SelectedItem;
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  gr.id,s.firstname as 'student',s.cin , m.name as 'module',b.name as 'branch',g.name as 'group',gr.grade from student s,branch  b,groupe g ,module m , grade gr where gr.idstudent=s.id and gr.idmodule=m.id and gr.idbranch=b.id and gr.idgroupe=g.id and gr.idgroupe=@idgroup and gr.idbranch=@idbranch and gr.idmodule=@idmodule";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@idmodule", group.Id);
            cmd.Parameters.AddWithValue("@idbranch", branch.Id);
            cmd.Parameters.AddWithValue("@idgroup", module.Id);
            //  MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            MySqlDataReader dr = cmd.ExecuteReader();
            //da.Fill(dataTable5);
            List<Grade> list = new List<Grade>();
            while (dr.Read())
            {
                Grade gr = new Grade();
                gr.Name = dr[1].ToString();
                gr.Module= dr[2].ToString();
                gr.Branche= dr[3].ToString();
                gr.Group = dr[4].ToString();
                gr.Grade2= Convert.ToDouble(dr[6]);




                list.Add(gr);
            }

            guna2DataGridView1.DataSource = list.Select(x => new { x.Name ,x.Module,x.Branche,x.Group,x.Grade2 }).Where(x => x.Name.Contains(textBox6.Text) ).ToList();
        }

        private void comboBox4_Click(object sender, EventArgs e)
        {
            Branch branch = (Branch)comboBox2.SelectedItem;
            Group1 group1 = (Group1)comboBox3.SelectedItem;
            comboBox4.DataSource = studentService.findByBranchId(branch.Id, group1.Id);
        }
    }
}
