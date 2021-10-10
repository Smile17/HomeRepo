using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Linq;
using System.Text;

namespace Graph
{
    public class Helper
    {
        public static string[] GenerateStringArray(int n, int size = 3, bool lowerCase = true)
        {
            var arr = Enumerable
                .Repeat("", n)
                .Select(i => RandomString(size, lowerCase))
                .ToArray();
            return arr;
        }
        // Generate a random string with a given size  
        public static string RandomString(int size, bool lowerCase)  
        {  
            StringBuilder builder = new StringBuilder();  
            Random random = new Random();  
            char ch;  
            for (int i = 0; i < size; i++)  
            {  
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));  
                builder.Append(ch);  
            }  
            if (lowerCase)  
                return builder.ToString().ToLower();  
            return builder.ToString();  
        }  
        # region Generate integer arrays
        public static int MIN = 2;
        public static int MAX = 20;
        public static int[] GenerateArray(int n)
        {
            Random randNum = new Random(DateTime.Now.Second);
            int[] arr = Enumerable
                .Repeat(0, n)
                .Select(i => randNum.Next(MIN, MAX))
                .ToArray();
            return arr;
        }
        public static int[,] GenerateArray(int n, int m)
        {
            Random randNum = new Random(DateTime.Now.Second);
            var arr = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                 arr[i, j] = randNum.Next(MIN, MAX);
            }
            return arr;
        }
        public static List<List<int>> GeneratePyramid(int height)
        {
            var list = new List<List<int>>();
            Random randNum = new Random(DateTime.Now.Second);
            for(int i = 0; i< height; i++)
            {
                var subList = new List<int>();
                for(int j = 0; j <= i; j++)
                {
                    subList.Add(randNum.Next(MIN, MAX));
                }
                list.Add(subList);
            }
            return list;
        }
        #endregion
    }
}
