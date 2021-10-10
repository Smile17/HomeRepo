using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Linq;

namespace Recursion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<List<int>> pyr = Helper.GeneratePyramid(4);
            Console.WriteLine(pyr.ToStringExtended());
            Console.WriteLine(Tasks.Task7(pyr));
        }
    }
}
