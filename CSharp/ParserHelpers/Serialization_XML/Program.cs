using System;
using System.IO;

namespace XML
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person(){Name = "Name1", Surname = "Surname1", Year = 1993};
            XmlSerialization.Serialize(p, "person.xml");
            var p2 = (Person)XmlSerialization.Deserialize<Person>("person.xml");
            Console.WriteLine(p2);

            BinarySerialization.Serialize(p, "person.dat");
            p2 = (Person)BinarySerialization.Deserialize<Person>("person.dat");
            Console.WriteLine(p2);

        }
    }
}
