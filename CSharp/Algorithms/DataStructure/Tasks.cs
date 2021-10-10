using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace DataStructure
{
    class Tasks
    {

        # region Find the biggest common divisor
        public static Int64 Euclid(Int64 m, Int64 n)
        {
            // m should be bigger than n
            if (m < n)
            {
                var tmp = n;
                n = m;
                m = tmp;
            }
            while(m % n != 0)
            {
                var r = m % n;
                m = n;
                n = r;
            }
            return n;
        }
        public static Int64 DivisorCount(Int64 n)
        {
            if (n == 1) return 1;
            var count = 1;
            //var m = (Int64)Math.Ceiling(Math.Pow(Math.E, Int64.Log(n) / 2));
            var m = Math.Ceiling(Math.Sqrt(n));
            for(Int64 i = 2; i < m; i++)
            {
                if (n % i == 0) count++;
            }
            count = 2 * count;
            if (n == m * m) count++;
            return count;
        }
        public static void EuclidTest()
        {
            var res = Euclid(22, 33);
            Console.WriteLine(res);
            Console.WriteLine(DivisorCount(12));
        }
        #endregion
       
    }


}