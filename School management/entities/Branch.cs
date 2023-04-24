using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class Branch
    {
        private int id;
        private string name;
        private int duration;
        private int idteacher;
        private int iddepartement;

        override
        public String ToString()
        {
            return this.name;
        }
        public Branch(int id, string name, int duration)
        {
            this.id = id;
            this.name = name;
            this.duration = duration;
            
        }
        public Branch( string name, int duration)
        {
           
            this.name = name;
            this.duration = duration;

        }

        public Branch(int id, string name, int duration, int idteacher, int iddepartement)
        {
            this.id = id;
            this.name = name;
            this.duration = duration;
            this.idteacher = idteacher;
            this.iddepartement = iddepartement;
        }
        public Branch( string name, int duration, int idteacher, int iddepartement)
        {
            
            this.name = name;
            this.duration = duration;
            this.idteacher = idteacher;
            this.iddepartement = iddepartement;
        }

        public Branch()
        {
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Duration { get => duration; set => duration = value; }
        public int Idteacher { get => idteacher; set => idteacher = value; }
        public int Iddepartement { get => iddepartement; set => iddepartement = value; }
    }
}
