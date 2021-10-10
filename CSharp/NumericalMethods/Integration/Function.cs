using System;

namespace Integration
{
    public class Function
    {
        public static double a = 0;
        public static double b = 2;
        public static double F(double x)
        {
            return x * x * x * x + x * x * x + 7 * x * x - 3 * x - 1;
        }
        public static double MaxFSecondDerevative()
        {
            // 12x^2 + 6x + 14, x0 = 2
            return 74;
        }
        public static double MaxFThirdDerevative()
        {
            // 24x + 6
            return 54;
        }
        public static double MaxFFourthDerevative()
        {
            // 24
            return 24;
        }
        public static double IntF(double x)
        {
            return x * x * x * x * x / 5 + x * x * x * x / 4 + 7 * x * x * x / 3 - 3 * x * x / 2 - 1 * x;
        }
        public static double IntFDef()
        {
            return IntF(b) - IntF(a);
        }
    }
}
