using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class User
    {

        private int id;
        private String login;
        private string password;
        private string role;

        public User(int id, string login, string password, string role)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.role = role;
        }
        public User()
        {

        }
        public User( string login, string password, string role)
        {
           
            this.login = login;
            this.password = password;
            this.role = role;
        }

        public int Id { get => id; set => id = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }
        public string Role { get => role; set => role = value; }
    }
}
