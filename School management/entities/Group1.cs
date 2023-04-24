using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_management.entities
{
    internal class Group1
    {
        private int id;
        private String name;
        private int idbranch;

        public Group1()
        {

        }

        public Group1(int id, String name, int idbranch)
        {
            this.id = id;
            this.name = name;
            this.idbranch = idbranch;
        }
        public Group1(String name, int idbranch)
        {
           
            this.name = name;
            this.idbranch = idbranch;
        }

       
        public int Id { get => id; set => id = value; }
        public String Name { get => name; set => name = value; }
        public int Idbranch { get => idbranch; set => idbranch = value; }

        override
         public String ToString()
        {
            return this.name;
        }

    }
}
