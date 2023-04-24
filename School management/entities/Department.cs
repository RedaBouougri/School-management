using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class Department
    {

        private int id;
        private string name;

        public Department(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Department( string name)
        {
            
            this.name = name;
        }
        override
         public String ToString()
        {
            return this.name;
        }



        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }
}
