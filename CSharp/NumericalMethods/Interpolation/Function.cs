using System;

namespace Interpolation
{
    public class Function
    {
        public static double a = 0.6;
        public static double b = 1.1;
        public static double step = (b-a)/10;
        public static double F(double x)
        {
            return 0.5 * x * x + Math.Cos(2*x);
        }
    }
}
