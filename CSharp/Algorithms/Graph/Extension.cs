using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Linq;


namespace Graph
{
    public static class Extension
    {
        public static char SPACE = '\t';
        public static string ToStringExtended<T>(this T[,] array)
        {
            var res  = String.Empty;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    res += array[i, j].ToString() + SPACE;
                res += Environment.NewLine;
            }
            return res;
        }
        public static string ToStringExtended<T>(this T[] array)
        {
            return String.Join(SPACE, array);
        }
        public static string ToStringExtended<T>(this IList<T> array)
        {
            return String.Join(SPACE, array);
        }
        public static string ToStringExtended(this List<List<int>> array)
        {
            var res = String.Empty;
            for (int i = 0; i < array.Count; i++)
            {
                for(int j = 0; j < array[i].Count; j++)
                {
                    res += array[i][j] + SPACE;
                }
                res += Environment.NewLine;
            }
            return res;
        }
    }
}
