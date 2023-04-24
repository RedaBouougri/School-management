using MySql.Data.MySqlClient;
using School_management.db;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.services
{
    internal class GradeService
    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        DataTable dt = new DataTable();
        int currRowIndex;

        public DataTable findAll()
        {
            ArrayList studentList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  gr.id,s.firstname as 'student',s.cin , m.name as 'module',b.name as 'branch',g.name as 'group',gr.grade from student s,branch  b,groupe g ,module m , grade gr where gr.idstudent=s.id and gr.idmodule=m.id and gr.idbranch=b.id and gr.idgroupe=g.id ";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
          

            dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);


            return dt;


        }
        public DataTable find(int idgroup, int idbranch, int idmodule)
        {
            ArrayList studentList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  gr.id,s.firstname as 'student',s.cin , m.name as 'module',b.name as 'branch',g.name as 'group',gr.grade from student s,branch  b,groupe g ,module m , grade gr where gr.idstudent=s.id and gr.idmodule=m.id and gr.idbranch=b.id and gr.idgroupe=g.id and gr.idgroupe=@idgroup and gr.idbranch=@idbranch and gr.idmodule=@idmodule";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@idbranch", idbranch);
            cmd.Parameters.AddWithValue("@idgroup", idgroup);
            cmd.Parameters.AddWithValue("@idmodule", idmodule);
           
            dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);


            return dt;


        }
    }
}
