using MySql.Data.MySqlClient;
using School_management.db;
using School_management.entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace School_management.services
{
    internal class GroupService
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

       
        int currRowIndex;

        public Group1 Group1 { get; private set; }

        public ArrayList findAll()
        {
            ArrayList groupList = new ArrayList();
            

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from groupe";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {
                Group1 group;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                group = new Group1((int)myArray[0], (string)myArray[1], (int)myArray[2]);
                groupList.Add(group);

            }
            return groupList;


        }

        public DataTable findAll2()
        {
            ArrayList groupList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select g.id,g.name,b.name as 'branch' from groupe g ,branch b where g.idbranch=b.id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;


        }

        public ArrayList findByBranchId(int id)
        {
            ArrayList groupList = new ArrayList();

            
            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from groupe where idbranch=@id";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {
                Group1 group;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                group = new Group1((int)myArray[0], (string)myArray[1], (int)myArray[2]);
                groupList.Add(group);

            }
            return groupList;


        }

        public ArrayList findByObj2(string name)
        {
            ArrayList groupList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from groupe where name=@name";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@name", name);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;
            Object[] myArray = new Object[3];
            foreach (DataRow row in dt.Rows)
            {
                Group1 group;
                i = 0;
                foreach (var item in row.ItemArray)
                {
                    myArray[i] = item;
                    i++;
                }
                group = new Group1((int)myArray[0], (string)myArray[1], (int)myArray[2]);
                groupList.Add(group);

            }
            return groupList;


        }
        public Group1 findByObj(string name)
        {
            ArrayList groupList = new ArrayList();
            Group1 group = new Group1();

            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from groupe where name=@name";
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
                group = new Group1(Convert.ToInt32(dt.Rows[0][0]), dt.Rows[0][1].ToString(), Convert.ToInt32(dt.Rows[0][0]));


            }
            return group;


        }



    }
}
