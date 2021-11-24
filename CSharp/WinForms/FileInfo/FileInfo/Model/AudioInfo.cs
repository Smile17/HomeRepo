using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FileInfo.Model
{
    public class AudioInfo : FileInformation
    {
        public string Author { get; set; } // singer
        public override string ToString()
        {
            return String.Format($"Id: {Id}; Name: {Name}; Author: {Author}; Length: {Length}; Year: {Year}; Price: {Price}; Extension: {Extension}; Type: {Type}; CreatedBy: {CreatedBy}");
        }
        public override void InputData() 
        {
            base.InputData();
            Type = FileType.Audio;
            Console.WriteLine("Author: ");
            Author = Console.ReadLine();
        }
        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(AudioInfo);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(AudioInfo);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                if (myPropInfo.PropertyType == typeof(Double))
                {
                    value = Double.Parse(value.ToString());
                }
                if (myPropInfo.PropertyType == typeof(Int32))
                {
                    value = Int32.Parse(value.ToString());
                }
                myPropInfo.SetValue(this, value, null);
            }
        }
    }
}
