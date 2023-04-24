using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.db
{
    internal class Connexion
    {
        public MySqlConnection connMaster;
        public void connexion()
        {
            connMaster = new MySqlConnection("SERVER=localhost; DATABASE=schoolMan; UID=root; PASSWORD=");
        }
        public void cnxOpen()
        {
            connMaster.Open();
        }
        public void cnxClose()
        {
            connMaster.Close();
        }
    }
}
