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
    internal class BranchService
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;

        public Group1 Group1 { get; private set; }

        public ArrayList findAll()
        {
            ArrayList branchList = new ArrayList();
            DataTable dt = new DataTable();

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from branch";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
          
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[5];
            foreach (DataRow row in dt.Rows)
            {
                Branch branch;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                branch = new Branch((int)myArray[0], (string)myArray[1], (int)myArray[2]);  
                branchList.Add(branch);

            }
            return branchList;


        }

        public DataTable findAll2()
        {
            ArrayList branchList = new ArrayList();

            DataTable dt = new DataTable();
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from branch";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            
            da.Fill(dt);

          
           
            return dt;


        }
        public ArrayList findByObj2(string cin)
        {
            ArrayList studentList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  * from branch where name=@name";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@name", cin);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {
                Branch branch = new Branch();
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                branch = new Branch((int)myArray[0], (string)myArray[1], (int)myArray[2]);
                studentList.Add(branch);

            }
            return studentList;

        }
        public Branch findByObj(string name)
        {

            Branch branch = new Branch();

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  * from branch where name=@name";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@name", name);
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
                branch = new Branch((int)myArray[0], (string)myArray[1], (int)myArray[2]);


            }
            return branch;


        }

        public int count()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM branch ", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return Convert.ToInt32(dt2.Rows[0][0].ToString());
        }

        public int count2()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM branch where id in (select idbranch from student )", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return Convert.ToInt32(dt2.Rows[0][0].ToString());
        }



        public DataTable labels()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT name FROM branch where id in (select idbranch from student )", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return dt2;
        }

        public int studs()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM student", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return Convert.ToInt32(dt2.Rows[0][0].ToString());
        }

        public DataTable ORD()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM student   group by idbranch order by idbranch asc", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return dt2;
        }



    }
}
