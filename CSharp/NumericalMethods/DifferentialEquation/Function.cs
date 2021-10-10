using System;

namespace DifferentialEquation
{
    public class Function
    {
        public static double x0 = 0;
        public static double y0 = 2;
        public static double F(double x, double y)
        {
            return 1/(2 * x - y * y);
        }
        public static double AnsF(double y)
        {
            var c = (x0 - (2 * y0 * y0 + 2 * y0 + 1) / 4) / Math.Pow(Math.E, 2 * y0);
            return c * Math.Pow(Math.E, 2 * y) + (2 * y * y + 2 * y + 1) / 4;
        }
    }
}
