using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FileInfo.Model
{
    public class VideoInfo : FileInformation
    {
        public string Producer { get; set; }
        public string MainActor { get; set; }
        public override string ToString()
        {
            return String.Format($"Id: {Id}; Name: {Name}; Producer: {Producer}; MainActor: {MainActor}; Length: {Length}; Year: {Year}; Price: {Price}; Extension: {Extension}; Type: {Type}; CreatedBy: {CreatedBy}");
        }
        public override void InputData()
        {
            ((FileInformation)this).InputData();
            Type = FileType.Video;
            Console.WriteLine("Producer: ");
            Producer = Console.ReadLine();
            Console.WriteLine("Main Actor: ");
            MainActor = Console.ReadLine();
        }
        public object this[string propertyName]
        {
            get
            {
                // probably faster without reflection:
                // like:  return Properties.Settings.Default.PropertyValues[propertyName] 
                // instead of the following
                Type myType = typeof(VideoInfo);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                return myPropInfo.GetValue(this, null);
            }
            set
            {
                Type myType = typeof(VideoInfo);
                PropertyInfo myPropInfo = myType.GetProperty(propertyName);
                myPropInfo.SetValue(this, value, null);
            }
        }
    }
}
