using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberConversionWithBase
{
    class NumberConvertor
    {
        static Dictionary<int, string> numbersToString = new Dictionary<int, string>(){
                {0, "0"},{1, "1"},{2, "2"},{3, "3"},{4, "4"},{5, "5"},
                {6, "6"},{7, "7"},{8, "8"},{9, "9"},{10, "A"},{11, "B"},
                {12, "C"},{13, "D"},{14, "E"},{15, "F"},
            };
        static Dictionary<string, int> stringToNumbers = numbersToString.ToDictionary(x => x.Value, x => x.Key);
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string Convert10ToP(int x, int p)
        {
            var arr = Convert10ToP_Array(x, p);
            var res = ConvertArrayToString(arr);
            return res;
        }
        private static List<int> Convert10ToP_Array(int x, int p)
        {
            var res = new List<int>();
            while (x >= p)
            {
                res.Add(x % p);
                x /= p;
            }
            res.Add(x);
            res.Reverse();
            return res;
        }
        private static string ConvertArrayToString(List<int> arr)
        {
            string res = "";
            for (int i = 0; i < arr.Count(); i++)
            {
                res += numbersToString[arr[i]];
            }
            return res;
        }

        public static int ConvertPTo10(string s, int p)
        {
            var arr = ConvertStringToArray(s);
            var res = ConvertPTo10_Array(arr, p);
            return res;
        }
        private static int ConvertPTo10_Array(List<int> arr, int p)
        {
            int res = arr[0];
            for (int i = 1; i < arr.Count(); i++)
            {
                res = res * p + arr[i];
            }
            
            return res;
        }
        private static List<int> ConvertStringToArray(string s)
        {
            var res = new List<int>();
            for (int i = 0; i < s.Length; i++)
            {
                res.Add(stringToNumbers[s[i].ToString()]);
            }
            return res;
        }

        public static string ConvertPToQ(string x, int p, int q)
        {
            var y = ConvertPTo10(x, p);
            var res = Convert10ToP(y, q);
            return res;
        }

    }
}
