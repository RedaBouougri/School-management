using Microsoft.VisualBasic.Logging;
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
    internal class TeacherService
    {


        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;
        private int i;

        public Group1 Group1 { get; private set; }

        public DataTable findAll()
        {
            ArrayList studentList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  t.id,t.firstname,t.lastname,t.cin,t.email,t.dateofbirth,t.telephone,d.name as 'department',t.speciality from teacher t,department d   where t.iddepartment=d.id ";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);


            return dt;


        }

        public ArrayList findAll2()
        {
            ArrayList studentList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  id,firstname from teacher";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            Object[] myArray = new Object[5];
            foreach (DataRow row in dt.Rows)
            {
                Teacher teacher;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                teacher = new Teacher((int)myArray[0], (string)myArray[1]);
                studentList.Add(teacher);




            }
            return studentList;
        }
        public int count() { 
        cnx.connexion();
                cnx.cnxOpen();
                MySqlDataAdapter ada2 = new MySqlDataAdapter("SELECT count(*) FROM teacher ", cnx.connMaster);
        DataTable dt2 = new DataTable();
        ada2.Fill(dt2);

               
       
            return Convert.ToInt32(dt2.Rows[0][0].ToString());
        }

    }
}
