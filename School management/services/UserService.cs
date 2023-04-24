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
    internal class UserService
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;

       

        public DataTable findAll()
        {
           


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select * from user  ";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);


            return dt;


        }

    }
}
