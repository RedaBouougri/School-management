using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class Module
    {
        private int id;
        private string name;
        private int hours;
        private int idbranch;

        public Module() { }
        public Module(int id, string name, int hours, int idbranch)
        {
            this.id = id;
            this.name = name;
            this.hours = hours;
            this.idbranch = idbranch;
        }
        public Module(string name, int hours, int idbranch)
        {
            this.idbranch = idbranch;
            this.name = name;
            this.hours = hours;
           
        }
        override
         public String ToString()
        {
            return this.name;
        }



        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Hours { get => hours; set => hours = value; }
        public int Idbranch { get => idbranch; set => idbranch = value; }
    }
}
