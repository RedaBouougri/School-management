using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class TeacherVo
    {
        private int id;
        private string firstName;
        private string lastName;
        private string cin;

        private string email;
       
        private DateTime dateofbirth;
        private int telephone;
        private string speciality;
        private string departement;

        public TeacherVo()
        {

        }
        public TeacherVo(int id, string firstName, string lastName, string cin, string email,  DateTime dateofbirth, int telephone, string speciality, string departement)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.cin = cin;
            this.email = email;
            
            this.dateofbirth = dateofbirth;
            this.telephone = telephone;
            this.speciality = speciality;
            this.departement = departement;
        }

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Cin { get => cin; set => cin = value; }
        public string Email { get => email; set => email = value; }
        public DateTime Dateofbirth { get => dateofbirth; set => dateofbirth = value; }
        public int Telephone { get => telephone; set => telephone = value; }
        public string Speciality { get => speciality; set => speciality = value; }
        public string Departement { get => departement; set => departement = value; }
    }
}
