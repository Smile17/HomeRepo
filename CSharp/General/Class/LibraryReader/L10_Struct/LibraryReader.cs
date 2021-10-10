using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L10_Struct
{
    public struct LibraryReader
    {
        public string Id { get; set; }
        public string Name { get; set; }// Surname, name, father name
        public string Address { get; set; }
        public string Phone { get; set; }
        public LibraryReader(string id, string name, string address, string phone)
        {
            Id = id; Name = name; Address = address; Phone = phone;
        }
    }
}
