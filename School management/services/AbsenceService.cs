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
    internal class AbsenceService

    {
        Connexion cnx = new Connexion();
        MySqlDataAdapter ada;

        
        int currRowIndex;
        public DataTable findByIds(int idgroup,int idbranch)
        {
            ArrayList absenceList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  id,firstname,lastname,cne from student where idgroup=@idgroup and idbranch=@idbranch ";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@idbranch", idbranch);
            cmd.Parameters.AddWithValue("@idgroup", idgroup);
            DataTable dt = new DataTable();
            dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cnx.cnxClose();

            return dt;


        }

        public DataTable findPre(int idgroup, int idbranch,int idteacher,int idmodule,DateTime date)
        {
            ArrayList absenceList = new ArrayList();


            cnx.connexion();
            cnx.cnxOpen();
            string request = "select  a.id,s.firstname,s.lastname,s.cne,a.absence from absence a,student  s where a.idstudent=s.id and a.idgroup=@idgroup and a.idbranch=@idbranch  and a.idteacher=@idteacher and a.idmodule=@idmodule and a.date=@date";
            MySqlCommand cmd = new MySqlCommand(request, cnx.connMaster);
            cmd.Parameters.AddWithValue("@idbranch", idbranch);
            cmd.Parameters.AddWithValue("@idgroup", idgroup);
            cmd.Parameters.AddWithValue("@idteacher", idteacher);
            cmd.Parameters.AddWithValue("@idmodule", idmodule);
            cmd.Parameters.AddWithValue("@date", date);
            DataTable dt = new DataTable();
            dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);

            cnx.cnxClose();
            return dt;


        }

    }
}
