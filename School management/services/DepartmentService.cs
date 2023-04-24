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
    internal class DepartmentService
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;

        public Group1 Group1 { get; private set; }

        public ArrayList findAll()
        {
            ArrayList groupList = new ArrayList();

            DataTable dt = new DataTable();
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from department";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {
                Department dep;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                dep = new Department((int)myArray[0], (string)myArray[1]);
                groupList.Add(dep);

            }
            return groupList;


        }

        public ArrayList findByObj2(string name)
        {
            ArrayList groupList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from department where name=@name";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@name", name);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {
                Department group;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                group = new Department((int)myArray[0], (string)myArray[1]);
                groupList.Add(group);

            }
            return groupList;


        }
        public DataTable findAll2()
        {
            ArrayList groupList = new ArrayList();
            DataTable dt = new DataTable();

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from department";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            
            return dt;


        }





        public int count()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM department ", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return Convert.ToInt32(dt2.Rows[0][0].ToString());
        }

        public int count2()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM department where id in (select iddepartment from teacher ) ", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return Convert.ToInt32(dt2.Rows[0][0].ToString());
        }



        public DataTable labels()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT name FROM department where id in (select iddepartment from teacher ) ", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return dt2;
        }
        public DataTable ORD()
        {
            cnx.connexion();
            cnx.cnxOpen();
            MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM teacher   group by iddepartment order by iddepartment asc", cnx.connMaster);
            DataTable dt2 = new DataTable();
            ada2.Fill(dt2);



            return dt2;
        }


    }
}
