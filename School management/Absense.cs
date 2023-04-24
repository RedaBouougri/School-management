using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.VisualBasic.ApplicationServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Reflection;
using Branch = School_management.entities.Branch;
using Module = School_management.entities.Module;

namespace School_management
{
    public partial class Absense : UserControl
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt;
        Boolean abs;
        int currRowIndex;
        GroupService groupservice = new GroupService();
        BranchService branchService = new BranchService();
        ModulService modulService = new ModulService();
        AbsenceService absenceService = new AbsenceService();
        TeacherService teacherService = new TeacherService();
        public Absense()
        {
            InitializeComponent();
            comboBox2.DataSource = branchService.findAll();
            comboBox4.DataSource = teacherService.findAll2();
            guna2DataGridView1.AllowUserToAddRows = false;
           

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Branch branch = (Branch)comboBox2.SelectedItem;
           
            comboBox3.DataSource = groupservice.findByBranchId(branch.Id);
            comboBox1.DataSource = modulService.findByBranchId(branch.Id);
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

            Branch branch = (Branch)comboBox2.SelectedItem;
            Group1 group = (Group1)comboBox3.SelectedItem;
            guna2DataGridView1.DataSource = absenceService.findByIds(group.Id,branch.Id);
            guna2Button2.Enabled = false;


        }
        private void guna2Button6_Click(object sender, EventArgs e)
        {


            Branch branch = (Branch)comboBox2.SelectedItem;
            Group1 group = (Group1)comboBox3.SelectedItem;
            Module module = (Module)comboBox1.SelectedItem;
            Teacher teacher = (Teacher)comboBox4.SelectedItem;
            guna2DataGridView1.DataSource = absenceService.findPre(group.Id, branch.Id, teacher.Id,module.Id,guna2DateTimePicker1.Value.Date);
            guna2Button2.Enabled = true;
        }


        private void guna2Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Branch branch = (Branch)comboBox2.SelectedItem;
                Group1 group = (Group1)comboBox3.SelectedItem;
                Module module = (Module)comboBox1.SelectedItem;
                Teacher teacher = (Teacher)comboBox4.SelectedItem;

                foreach (DataGridViewRow row in guna2DataGridView1.Rows) {

                    Boolean abs = Convert.ToBoolean(row.Cells["Absence"].Value);
                    string status="";
                    if (abs ) { status = "Absent"; }
                    if (!abs ) { status = "Present"; }
                    int idstudent = Convert.ToInt32(row.Cells["id"].Value);

                   Absence absence = new Absence(guna2DateTimePicker1.Value.Date, branch.Id, group.Id,teacher.Id, idstudent, status,module.Id);

                    

                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO absence (date,idbranch,idgroup,idteacher,idstudent,status,idmodule,absence)" + "VALUES(@date,@idbranch,@idgroup,@idteacher,@idstudent,@status,@idmodule,@bool)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@date", absence.Date);
                    cmd.Parameters.AddWithValue("@idbranch", absence.Idbranch);
                    cmd.Parameters.AddWithValue("@idgroup", absence.Idgroup);
                    cmd.Parameters.AddWithValue("@idteacher", absence.Idteacher);
                    cmd.Parameters.AddWithValue("@idstudent", absence.Idstudent);
                    cmd.Parameters.AddWithValue("@status", absence.Status);
                    cmd.Parameters.AddWithValue("@idmodule", absence.Idmodule);
                    cmd.Parameters.AddWithValue("@bool", abs);


                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this absence ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {
                string status="";

                if (!abs) { status = "Absent"; }
                if (abs) { status = "Present"; }

                cnx.connexion();
                    cnx.cnxOpen();

                    MySqlCommand cmd = cnx.connMaster.CreateCommand();

                    cmd.CommandText = "UPDATE absence SET status= @status, absence=@absence" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@absence", !abs);
                    
                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();

                 
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.guna2DataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);

             abs = Convert.ToBoolean(row.Cells["Absence"].Value);
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
                            Branch branch = (Branch)comboBox2.SelectedItem;
                            Group1 group = (Group1)comboBox3.SelectedItem;
                            Module module = (Module)comboBox1.SelectedItem;
                            Teacher teacher = (Teacher)comboBox4.SelectedItem;
                            cnx.connexion();
                            cnx.cnxOpen();
                            string request = "select  s.firstname,s.lastname,s.cne,a.status from absence a,student  s where a.idstudent=s.id and a.idgroup=@idgroup and a.idbranch=@idbranch  and a.idteacher=idteacher and a.idmodule=@idmodule and a.date=@date";
                            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
                            cmd.Parameters.AddWithValue("@idbranch", branch.Id);
                            cmd.Parameters.AddWithValue("@idgroup", group.Id);
                            cmd.Parameters.AddWithValue("@idteacher", teacher.Id);
                            cmd.Parameters.AddWithValue("@idmodule", module.Id);
                            cmd.Parameters.AddWithValue("@date", guna2DateTimePicker1.Value.Date);
                           
                            MySqlDataReader dr = cmd.ExecuteReader();

                            PdfPTable pTable = new PdfPTable(dr.FieldCount);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            PdfPCell pCell = new PdfPCell(new Phrase("Firstname"));

                            pTable.AddCell(pCell);


                            PdfPCell pCell3 = new PdfPCell(new Phrase("Lastname"));

                            pTable.AddCell(pCell3);

                            PdfPCell pCell4 = new PdfPCell(new Phrase("Cne"));

                            pTable.AddCell(pCell4);

                            PdfPCell pCell5 = new PdfPCell(new Phrase("Pre/Abs"));

                            pTable.AddCell(pCell5);

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
                            Branch branch = (Branch)comboBox2.SelectedItem;
                            Group1 group = (Group1)comboBox3.SelectedItem;
                            Module module = (Module)comboBox1.SelectedItem;
                            Teacher teacher = (Teacher)comboBox4.SelectedItem;
                            cnx.connexion();
                            cnx.cnxOpen();
                            string request = "select  s.firstname,s.lastname,s.cne,a.status from absence a,student  s where a.idstudent=s.id and a.idgroup=@idgroup and a.idbranch=@idbranch  and a.idteacher=idteacher and a.idmodule=@idmodule and a.date=@date";
                            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
                            cmd.Parameters.AddWithValue("@idbranch", branch.Id);
                            cmd.Parameters.AddWithValue("@idgroup", group.Id);
                            cmd.Parameters.AddWithValue("@idteacher", teacher.Id);
                            cmd.Parameters.AddWithValue("@idmodule", module.Id);
                            cmd.Parameters.AddWithValue("@date", guna2DateTimePicker1.Value.Date);

                            MySqlDataReader dr = cmd.ExecuteReader();

                            PdfPTable pTable = new PdfPTable(dr.FieldCount);

                            pTable.DefaultCell.Padding = 2;

                            pTable.WidthPercentage = 100;

                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            PdfPCell pCell = new PdfPCell(new Phrase("Firstname"));

                            pTable.AddCell(pCell);


                            PdfPCell pCell3 = new PdfPCell(new Phrase("Lastname"));

                            pTable.AddCell(pCell3);

                            PdfPCell pCell4 = new PdfPCell(new Phrase("Cne"));

                            pTable.AddCell(pCell4);

                            PdfPCell pCell5 = new PdfPCell(new Phrase("Pre/Abs"));

                            pTable.AddCell(pCell5);

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
                                Paragraph header = new Paragraph("Absence");
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

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            try
            {
                Branch branch = (Branch)comboBox2.SelectedItem;
                Group1 group = (Group1)comboBox3.SelectedItem;
                Module module = (Module)comboBox1.SelectedItem;
                Teacher teacher = (Teacher)comboBox4.SelectedItem;

                foreach (DataGridViewRow row in guna2DataGridView1.Rows)
                {

                    Boolean abs = Convert.ToBoolean(row.Cells["Absence"].Value);
                    string status = "";
                    if (abs) { status = "Absent"; }
                    if (!abs) { status = "Present"; }
                    int idstudent = Convert.ToInt32(row.Cells["id"].Value);

                    Absence absence = new Absence(guna2DateTimePicker1.Value.Date, branch.Id, group.Id, teacher.Id, idstudent, status, module.Id);



                    cnx.connexion();
                    cnx.cnxOpen();
                    //MySqlCommand cmd = new MySqlCommand("INSERT INTO teacher (firstname,lastname,cin)" + "VALUES('vv','vv','vv')", cnx.connMaster);
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO absence (date,idbranch,idgroup,idteacher,idstudent,status,idmodule,absence)" + "VALUES(@date,@idbranch,@idgroup,@idteacher,@idstudent,@status,@idmodule,@bool)", cnx.connMaster);
                    cmd.Parameters.AddWithValue("@date", absence.Date);
                    cmd.Parameters.AddWithValue("@idbranch", absence.Idbranch);
                    cmd.Parameters.AddWithValue("@idgroup", absence.Idgroup);
                    cmd.Parameters.AddWithValue("@idteacher", absence.Idteacher);
                    cmd.Parameters.AddWithValue("@idstudent", absence.Idstudent);
                    cmd.Parameters.AddWithValue("@status", absence.Status);
                    cmd.Parameters.AddWithValue("@idmodule", absence.Idmodule);
                    cmd.Parameters.AddWithValue("@bool", abs);


                    cmd.ExecuteNonQuery();
                    cnx.cnxClose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("Do you really want to modify this absence ", "Modify", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {
                string status = "";

                if (!abs) { status = "Absent"; }
                if (abs) { status = "Present"; }

                cnx.connexion();
                cnx.cnxOpen();

                MySqlCommand cmd = cnx.connMaster.CreateCommand();

                cmd.CommandText = "UPDATE absence SET status= @status, absence=@absence" +
                    " WHERE id=" + currRowIndex;
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@absence", !abs);

                cmd.ExecuteNonQuery();
                cnx.cnxClose();


            }
        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {

            Branch branch = (Branch)comboBox2.SelectedItem;
            Group1 group = (Group1)comboBox3.SelectedItem;
            Module module = (Module)comboBox1.SelectedItem;
            Teacher teacher = (Teacher)comboBox4.SelectedItem;
            guna2DataGridView1.DataSource = absenceService.findPre(group.Id, branch.Id, teacher.Id, module.Id, guna2DateTimePicker1.Value.Date);
        
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

            Branch branch = (Branch)comboBox2.SelectedItem;
            Group1 group = (Group1)comboBox3.SelectedItem;
            guna2DataGridView1.DataSource = absenceService.findByIds(group.Id, branch.Id);
           
        }
    }
}
