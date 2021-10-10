using System;

namespace XML
{
    [Serializable]
    public class Person
    {
        public string Name {get; set;}
        public string Surname {get; set;}
        public int Year {get; set;}

        public override string ToString()
        {
            return String.Format("Name: {0}, Surname: {1}, Year: {2}", Name, Surname, Year);
        }
    }
}