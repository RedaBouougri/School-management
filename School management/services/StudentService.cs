using MySql.Data.MySqlClient;
using School_management.db;
using School_management.entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.services
{
    internal class StudentService
    {

        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;

        public Group1 Group1 { get; private set; }

        public DataTable findAll()
        {
            ArrayList studentList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  s.id,s.firstname,s.lastname,s.cin,s.cne,s.email,s.dateofbirth,s.telephone,b.name as 'branch',s.address,g.name as 'group' from student s,branch  b,groupe g where s.idbranch=b.id  and s.idgroup=g.id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

           
            return dt;


        }

        public ArrayList findByBranchId(int id,int id2)
        {
            ArrayList studentlist = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select id,firstname,lastname from student where idbranch=@id and idgroup=@id2";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@id2", id2);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[4];
            foreach (DataRow row in dt.Rows)
            {
                Student student;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                student = new Student((int)myArray[0], (string)myArray[1], (string)myArray[2]);
                studentlist.Add(student);

            }
            return studentlist;


        }
        public Student findByObj(string cin)
        {
            
            Student student = new Student();

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  id,firstname,lastname from student where cin=@cin";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@cin", cin);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {

                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                student = new Student((int)myArray[0], (string)myArray[1], (string)myArray[2]);


            }
            return student;


        }

        public ArrayList findByObj2(string cin)
        {
            ArrayList studentList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select id,firstname,lastname from student where cin=@cin";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@cin", cin);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {
                Student student;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                student = new Student((int)myArray[0], (string)myArray[1], (string)myArray[2]);
                studentList.Add(student);

            }
            return studentList;

        }
        public int count()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM student ", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return Convert.ToInt32(dt2.Rows[0][0].ToString());
        }


    }
}
