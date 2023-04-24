using Bunifu.Charts.WinForms;
using Guna.Charts.WinForms;
using MySql.Data.MySqlClient;
using School_management.db;
using School_management.services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Bunifu.UI.WinForms.BunifuSnackbar;

namespace School_management
{

   
    public partial class Statistics : UserControl

    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;
        private int i;
        TeacherService teacherService = new TeacherService();
        StudentService studentService = new StudentService();
        BranchService branchService = new BranchService();
        DepartmentService departmentService = new DepartmentService();  
        
        public Statistics()
        {
            
            InitializeComponent();
            label2.Text = teacherService.count().ToString();
            label3.Text = studentService.count().ToString();
            label5.Text = branchService.count().ToString();


            loadChart1();
            loadChart2();


            // Add the BarSeries to the plot
            /*var plt = new ScottPlot.Plot(600, 400);

            double[] values = { 778, 43, 283, 76, 184 };
            string[] labels = { "C#", "JAVA", "Python", "F#", "PHP" };
            var pie = formsPlot2.Plot.AddPie(values);
            pie.SliceLabels = labels;
            pie.ShowPercentages = true;
            pie.ShowValues = true;
            pie.ShowLabels = true;
            formsPlot2.Refresh();*/

        }

        public void loadChart1()
        {
            double[] dataX = new double[branchService.count2()];//branchService.count() hiya nombre ta3 les filieres li 3ndi
            double[] dataY = new double[branchService.count2()];//branchService.count() hiya nombre ta3 les filieres li 3ndi
            var plt = new ScottPlot.Plot(610, 404);
            // plt.AddScatter(dataX, dataY);
            String[] labels = new String[branchService.count2()];//branchService.count() hiya nombre ta3 les filieres li 3ndi
            DataTable dtx = branchService.labels();// kanjib biha les filieres info math ..... hadi hiya X axis
            DataTable dtx2 = branchService.ORD();// kanjib biha nbr d'etudiant f chaque filiere hadi hiya y axis
            for (i = 0; i < branchService.count2(); i++)
            {
                dataY[i] = i;
            }
            for (i = 0; i < branchService.count2(); i++)
            {
                labels[i] = dtx.Rows[i][0].ToString();
            }

            for (i = 0; i < branchService.count2(); i++)
            {
               
               
                    dataX[i] = Convert.ToDouble(dtx2.Rows[i][0]);
                
            }
            //string[] labels = { "PHP", "JS", "C++", "GO", "VB" };
            formsPlot1.Plot.AddBar(dataX, dataY);

            formsPlot1.Plot.XTicks(dataY, labels);
            formsPlot1.Plot.SetAxisLimits(yMin: 0);
            formsPlot1.Refresh();

        }

        public void loadChart2()
        {
            double[] dataX = new double[departmentService.count2()];//branchService.count() hiya nombre ta3 les filieres li 3ndi
            double[] dataY = new double[departmentService.count2()];//branchService.count() hiya nombre ta3 les filieres li 3ndi
            var plt = new ScottPlot.Plot(610, 404);
            // plt.AddScatter(dataX, dataY);
            String[] labels = new String[departmentService.count2()];//branchService.count() hiya nombre ta3 les filieres li 3ndi
            DataTable dtx = departmentService.labels();// kanjib biha les filieres info math ..... hadi hiya X axis
            DataTable dtx2 = departmentService.ORD();// kanjib biha nbr d'etudiant f chaque filiere hadi hiya y axis
            for (i = 0; i < departmentService.count2(); i++)
            {
                dataY[i] = i;
            }
            for (i = 0; i < departmentService.count2(); i++)
            {
                labels[i] = dtx.Rows[i][0].ToString();
            }

            for (i = 0; i < departmentService.count2(); i++)
            {

                dataX[i] = Convert.ToDouble(dtx2.Rows[i][0]);

            }
            //string[] labels = { "PHP", "JS", "C++", "GO", "VB" };
            formsPlot2.Plot.AddBar(dataX, dataY);

            formsPlot2.Plot.XTicks(dataY, labels);
            formsPlot2.Plot.SetAxisLimits(yMin: 0);
            formsPlot2.Refresh();

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       

        private void gunaChart1_Load_1(object sender, EventArgs e)
        {

        }

        private void Statistics_Load(object sender, EventArgs e)
        {

        }

        private void formsPlot1_Load(object sender, EventArgs e)
        {

        }
    }
}
