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
    internal class ModulService
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

       
        int currRowIndex;

        public Group1 Group1 { get; private set; }

        public ArrayList findAll()
        {
            ArrayList moduleList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from module";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {
                Module module;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                module = new Module((int)myArray[0], (string)myArray[1], (int)myArray[2], (int)myArray[3]);
                moduleList.Add(module);

            }
            return moduleList;


        }
        public DataTable findAll2()
        {
            ArrayList moduleList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select m.id,m.name,m.hours,b.name as 'branch' from module m,branch b where m.idbranch=b.id ";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

           
            return dt;


        }

        public ArrayList findByBranchId(int id)
        {
            ArrayList modulelist = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from module where idbranch=@id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[4];
            foreach (DataRow row in dt.Rows)
            {
                Module module;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                module = new Module((int)myArray[0], (string)myArray[1], (int)myArray[2], (int)myArray[3]);
                modulelist.Add(module);

            }
            return modulelist;


        }

        public Module findByObj(string name)
        {
           
            Module module = new Module() ;

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from module where name=@name";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@name", name);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[8];
            foreach (DataRow row in dt.Rows)
            {

                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                module = new Module(Convert.ToInt32(dt.Rows[0][0]), dt.Rows[0][1].ToString(), Convert.ToInt32(dt.Rows[0][2]), Convert.ToInt32(dt.Rows[0][3]));


            }
            return module;


        }
        public ArrayList findByObj2(string cin)
        {
            Module module = new Module();
            ArrayList modulelist = new ArrayList();

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from module where name=@name";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@name", cin);
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
                module = new Module((int)myArray[0], (string)myArray[1], (int)myArray[2], (int)myArray[3]);
                modulelist.Add(module);

            }
            return modulelist;


        }







    }
}
