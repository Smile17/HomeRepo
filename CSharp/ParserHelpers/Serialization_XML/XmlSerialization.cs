using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace XML
{
    public class XmlSerialization
    {
        public static MemoryStream Serialize<T>(T obj)
        {
            MemoryStream outp = new MemoryStream();
            try
            {
                XmlSerializer formatter = new XmlSerializer(obj.GetType());
                formatter.Serialize(outp, obj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return outp;
        }
        public static void Serialize<T>(T obj, string path)
        {
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                (Serialize(obj)).WriteTo(file);
            }
        }
        public static T Deserialize<T>(MemoryStream inp) where T : new()
        {
            inp.Position = 0;
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                return (T)formatter.Deserialize(inp);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new T();
        }
        
        public static T Deserialize<T>(string path) where T : new()
        {
            using (MemoryStream ms = new MemoryStream())
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                file.CopyTo(ms);
                return (T)Deserialize<T>(ms);
            }
        }

    }
}