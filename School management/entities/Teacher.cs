using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class Teacher
    {
        private int id;
        private string firstName;
        private string lastName;
        private string cin;
       
        private string email;
        private byte[] pic;
        private DateTime dateofbirth;
        private int telephone;
        private string speciality;
        private int iddepartement;

        public Teacher(int id,string firstName)
        {
            this.id = id;
            this.firstName = firstName;
        }
        public Teacher(int id, string firstName, string lastName, string cin, string email, byte[] pic, DateTime dateofbirth, int telephone, string speciality, int iddepartement)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.cin = cin;
            this.email = email;
            this.pic = pic;
            this.dateofbirth = dateofbirth;
            this.telephone = telephone;
            this.speciality = speciality;
            this.Iddepartement = iddepartement;
        }
        public Teacher(string firstName, string lastName, string cin, string email, byte[] pic, DateTime dateofbirth, int telephone, string speciality, int iddepartement)
        {
           
            this.firstName = firstName;
            this.lastName = lastName;
            this.cin = cin;
            this.email = email;
            this.pic = pic;
            this.dateofbirth = dateofbirth;
            this.telephone = telephone;
            this.speciality = speciality;
            this.Iddepartement = iddepartement;
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
        public string Email { get => email; set => email = value; }
        public byte[] Pic { get => pic; set => pic = value; }
        public DateTime Dateofbirth { get => dateofbirth; set => dateofbirth = value; }
        public int Telephone { get => telephone; set => telephone = value; }
        public string Speciality { get => speciality; set => speciality = value; }
        public int Iddepartement { get => iddepartement; set => iddepartement = value; }
    }
}
