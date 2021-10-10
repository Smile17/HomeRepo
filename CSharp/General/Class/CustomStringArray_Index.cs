using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var e = new CustomStringArray(2);
            Console.WriteLine(e);
            Console.WriteLine("Main diagonal: {0}", e[0]);
            Console.WriteLine("Additional diagonal: {0}", e[3]);
            Console.WriteLine("Vowel count: {0}", e.VowelCount);
        }
    }
    public class CustomStringArray
    {
        public string[,] Arr{get;set;}
        public CustomStringArray(int n)
        {
            Arr = new string[n, n];
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    Arr[i, j] =  CustomStringArray.RandomString(4);
                }
            }
        }
        private static Random rnd = new Random();
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
        public override string ToString()
        {
            string s = "Array:\n";
            var n = Arr.GetUpperBound(1) + 1;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    s += Arr[i, j] + "\t";
                }
                s+="\n";
            }
            return s;
        }
        public string this[int index]
        {
            get
            {
                index = index % 2; // index is equal 0 or 1; 0 - is main diagonal, 1 - additional diagonal 
                string s = "";
                var n = Arr.GetUpperBound(1) + 1;
                for(int i = 0; i < n; i++) // concatenate from top corner to bottom corner
                {
                    s += Arr[i, index == 0 ? i : n - 1 - i];
                }
                return s;
            }
        }
        public int VowelCount
        {
            get
            {
                var count = 0;
                var n = Arr.GetUpperBound(1) + 1;
                for(int i = 0; i < n; i++)
                {
                    for(int j = 0; j < n; j++)
                    {
                        var s = Arr[i, j];
                        for(int t = 0; t < s.Length; t++)
                            if("aeiouAEIOU".IndexOf(s[t]) >= 0)
                                count++;
                    }
                }
                return count;
            }
        }
    }
}
