using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management
{
    internal class Student
    {

        private int id;
        private string firstName;
        private string lastName;
        private string cin;
        private string cne;
        private string email;
        private byte[] pic;
        private DateTime dateofbirth;
        private int telephone;
        private int idbranch;
        private string address;
        private int idgroup;

        public Student()
        {

        }
        public Student(int id, string firstName, string lastName)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public Student(int id, string firstName, string lastName, string cin, string cne, string email, byte[] pic, DateTime dateofbirth, int telephone, int idbranch, string address, int idgroup)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.cin = cin;
            this.cne = cne;
            this.email = email;
            this.pic = pic;
            this.dateofbirth = dateofbirth;
            this.telephone = telephone;
            this.idbranch = idbranch;
            this.address = address;
            this.idgroup = idgroup;
            this.Idgroup = idgroup;
        }
        public Student( string firstName, string lastName, string cin, string cne, string email, byte[] pic, DateTime dateofbirth, int telephone, int idbranch, string address, int idgroup)
        {
           
            this.firstName = firstName;
            this.lastName = lastName;
            this.cin = cin;
            this.cne = cne;
            this.email = email;
            this.pic = pic;
            this.dateofbirth = dateofbirth;
            this.telephone = telephone;
            this.idbranch = idbranch;
            this.address = address;
            this.Idgroup = idgroup;
        }

        override
        public String ToString()
        {
            return this.firstName;
        }
        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Cin { get => cin; set => cin = value; }
        public string Cne { get => cne; set => cne = value; }
        public string Email { get => email; set => email = value; }
        public byte[] Pic { get => pic; set => pic = value; }
        public DateTime Dateofbirth { get => dateofbirth; set => dateofbirth = value; }
        public int Telephone { get => telephone; set => telephone = value; }
        public int Idbranch { get => idbranch; set => idbranch = value; }
        public string Address { get => address; set => address = value; }
        public int Idgroup { get => idgroup; set => idgroup = value; }
    }
}