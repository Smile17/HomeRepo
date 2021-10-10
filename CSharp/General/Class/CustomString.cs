using System;
using System.Collections.Generic;

namespace L3
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomString customString = new CustomString("sad sfsdr ertet");
            Console.WriteLine(customString);
            customString.ToCamelCase();
            Console.WriteLine(customString);

            Console.WriteLine("Text: ");
            Text txt = new Text();
            txt.Add("1 asdsd sdffs df");
            txt.Add("2 werrwe wre gfd");
            Console.WriteLine(txt);
            Console.WriteLine("Remove first line: ");
            txt.RemoveAt(0);
            Console.WriteLine(txt);
            Console.WriteLine("Clear text: ");
            txt.Clear();
            Console.WriteLine(txt);

            txt.Add("1 asdsd sdffs df");
            txt.Add("2 werrwe wre gfd");
            txt.Add("SA werrwe wre gfd");
            Console.WriteLine("To camel case: ");
            txt.ToCamelCase();
            Console.WriteLine(txt);
            Console.WriteLine("Key of the text: {0}", txt.Key());
            Console.WriteLine("Lines with length equal 16: {0}", txt.Count(16));


        }
    }

    public class CustomString
    {
        public string Str {get;set;}

        # region Constructors
        public CustomString(string s)
        {
            Str = s;
        }

        # endregion

        // Methods
        public override string ToString()
        {
            return Str;
        }
        public void ToCamelCase()
        {
            var words = Str.Split(' ');
            for(int i = 0; i< words.Length; i++)
            {
                words[i] = (words[i][0]).ToString().ToUpper() + words[i].Substring(1);
            }
            Str = String.Join(' ', words);
        }
    }

    public class Text
    {
        List<CustomString> Lines {get;set;}
        public Text()
        {
            Lines = new List<CustomString>();
        }

        public override string ToString()
        {
            return String.Join("\n", Lines);
        }

        public void Add(string s)
        {
            Lines.Add(new CustomString(s));
        }
        public void RemoveAt(int index)
        {
            Lines.RemoveAt(index);
        }
        public void Clear()
        {
            Lines.Clear();
        }
        public void ToCamelCase()
        {
            for(int i = 0; i < Lines.Count; i++)
            {
                Lines[i].ToCamelCase();
            }
        }
        public string Key()
        {
            char[] key = new char[Lines.Count];
            for(int i = 0; i < Lines.Count; i++)
            {
                key[i] = Lines[i].Str[0];
            }
            return String.Join("", key);
        }
        public int Count(int length)
        {
            int res = 0;
            for(int i = 0; i < Lines.Count; i++)
            {
                if (Lines[i].Str.Length == length) res++;
            }
            return res;
        }

    }
}
