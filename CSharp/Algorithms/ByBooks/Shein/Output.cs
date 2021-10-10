using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Linq;
using System.Text;

namespace Shein
{
    public class Output
    {
        public static string ArrayToString<T>(T[] arr)
        {
            return String.Join(' ', arr);
        }
        public static string ArrayToString<T>(T[,] arr)
        {
            string res = "";
            for (int i = 0; i < arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < arr.GetUpperBound(1) + 1; j++)
                    res += arr[i, j] + "\t";
                res += "\n";
            }
            return res;
        }
    }
}