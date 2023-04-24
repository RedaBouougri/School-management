using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class Absence
    {

        private int id;
        private DateTime date;
        private int idbranch;
        private int idgroup;
        private int idmodule;
        private int idteacher;
        private int idstudent;
        private string status;



        public Absence(int id, DateTime date, int idbranch, int idgroup, int idteacher, int idstudent, string status, int idmodule )
        {
            this.id = id;
            this.date = date;
            this.idbranch = idbranch;
            this.idgroup = idgroup;
            this.idteacher = idteacher;
            this.idstudent = idstudent;
            this.status = status;
            this.idmodule = idmodule;
        }

        public Absence( DateTime date, int idbranch, int idgroup, int idteacher, int idstudent, string status, int idmodule)
        {
           
            this.date = date;
            this.idbranch = idbranch;
            this.idgroup = idgroup;
            this.idteacher = idteacher;
            this.idstudent = idstudent;
            this.status = status;
            this.idmodule = idmodule;

        }
        public int Id { get => id; set => id = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Idbranch { get => idbranch; set => idbranch = value; }
        public int Idgroup { get => idgroup; set => idgroup = value; }
        public int Idteacher { get => idteacher; set => idteacher = value; }
        public int Idstudent { get => idstudent; set => idstudent = value; }
        public string Status { get => status; set => status = value; }
        public int Idmodule { get => idmodule; set => idmodule = value; }
    }
}
