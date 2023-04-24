using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class Grade
    {

        private int id;
        private int idstudent;
        private int idmodule;
        private int idbranch;
        private int idgroupe;
        private double grade1;
        private int year;
        private string name;
        private string module;
        private string branche;
        private string group;
        private double grade;



        public Grade() { }
        public Grade(int id, int idstudent, int idmodule, double grade,  int idbranch, int idgroupe)
        {
            this.id = id;
            this.idstudent = idstudent;
            this.idmodule = idmodule;
            this.Grade1 = grade;
          
            this.idbranch = idbranch;
            this.idgroupe = idgroupe;
        }
        public Grade( int idstudent, int idmodule, double grade, int idbranch, int idgroupe)
        {
           
            this.idstudent = idstudent;
            this.idmodule = idmodule;
            this.Grade1 = grade;
          
            this.idbranch = idbranch;
            this.idgroupe = idgroupe;
        }


        public int Id { get => id; set => id = value; }
        public int Idstudent { get => idstudent; set => idstudent = value; }
        public int Idmodule { get => idmodule; set => idmodule = value; }
        
        public int Year { get => year; set => year = value; }
        public double Grade1 { get => grade1; set => grade1 = value; }
        public int Idbranch { get => idbranch; set => idbranch = value; }
        public int Idgroupe { get => idgroupe; set => idgroupe = value; }
        public string Name { get => name; set => name = value; }
       
        public string Module { get => module; set => module = value; }
        public string Branche { get => branche; set => branche = value; }
        public string Group { get => group; set => group = value; }
        public double Grade2 { get => grade; set => grade = value; }
    }
}
