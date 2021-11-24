using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FileInfo.Model
{
    public class FileInformation
    {
        /*

        5. У проекті з інтерфейсом Windows Forms використати колекцію LinkedList<T>. 
        Реалізувати на формі перегляд елементів колекції у прямому і зворотному напрямах. Реалізувати функції, як у консольній версії.
          */
        public string Id { get; set; } // code
        public string Name { get; set; }
        public string CreatedBy { get; set; } // who created the file
        public double Length { get; set; }
        public string Extension { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }
        public FileType Type { get; set; }
        public virtual void InputData() 
        {
            Console.WriteLine("Id: ");
            Id = Console.ReadLine();
            Console.WriteLine("Name: ");
            Name = Console.ReadLine();
            Console.WriteLine("Created By: ");
            CreatedBy = Console.ReadLine();
            Console.WriteLine("Lenght: ");
            Double.TryParse(Console.ReadLine(), out double Length);
            Console.WriteLine("Extension: ");
            Extension = Console.ReadLine();
            Console.WriteLine("Year: ");
            Int32.TryParse(Console.ReadLine(), out int Year);
            Console.WriteLine("Price: ");
            Double.TryParse(Console.ReadLine(), out double Price);
        }
    }
}
