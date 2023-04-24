using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class StudentVo
    {
        private int id;
        private string firstName;
        private string lastName;
        private string cin;
        private string cne;
        private string email;
    
        private DateTime dateofbirth;
        private int telephone;
        private String branch;
        private string address;
        private string group;
        private int idgroup;
        private int idbranch;

        public StudentVo()
        {

        }
        public StudentVo(int id, string firstName, string lastName, string cin, string cne, string email, DateTime dateofbirth, int telephone, string branch, string address, string group, int idgroup , int idbranch )
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.cin = cin;
            this.cne = cne;
            this.email = email;

            this.dateofbirth = dateofbirth;
            this.telephone = telephone;
            this.branch = branch;
            this.address = address;
            this.group = group;
            this.idgroup = idgroup;
            this.idbranch = idbranch;
        }

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Cin { get => cin; set => cin = value; }
        public string Cne { get => cne; set => cne = value; }
        public string Email { get => email; set => email = value; }
       
        public DateTime Dateofbirth { get => dateofbirth; set => dateofbirth = value; }
        public int Telephone { get => telephone; set => telephone = value; }
        public string Branch { get => branch; set => branch = value; }
        public string Address { get => address; set => address = value; }
        public string Group { get => group; set => group = value; }
    }
}
