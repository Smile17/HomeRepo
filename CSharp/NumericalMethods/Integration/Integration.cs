using System;
using System.IO;

namespace Integration
{
    class Integration
    {
        public delegate double Func(double x);
        public static double IntegrateTrapeze(double a, double b, int n, Func f)
        {
            var h = (b - a) / n;
            var sum = 0.5 * (f(a) + f(b));
            for (int i = 1; i < n; i++)
                sum += f(a + i * h);
            return sum * h;
        }
        public static double IntegrateSimson(double a, double b, int n, Func f)
        {
            var h = (b - a) / n;
            var sum = (f(a) + 4 * f(a + h) + f(b));
            for (int i = 1; i < n / 2; i++)
                sum += 2 * f(a + (2 * i) * h) + 4 * f(a + (2 * i + 1) * h);
            return sum * h / 3;
        }

        public static void TestIntegrationTrapeze()
        {
            StreamWriter outp = new StreamWriter("output_trapeze.txt");
            var real = Function.IntFDef();
            outp.WriteLine("Real value: {0}", real);
            outp.WriteLine("n\testimated value\tdifference\tdifference theory");
            for(int i = 100; i <= 1000; i+=100)
            {
                var h = (Function.b - Function.a) / i;
                var res = Integration.IntegrateTrapeze(Function.a, Function.b, i, Function.F);
                outp.WriteLine("{0}\t{1}\t{2}\t{3}", i, res, Math.Abs(real - res), Function.MaxFSecondDerevative() * i * h * h * h / 12);
            }
            outp.Close();
        }
        public static void TestIntegrationSimson()
        {
            StreamWriter outp = new StreamWriter("output_simson.txt");
            var real = Function.IntFDef();
            outp.WriteLine("Real value: {0}", real);
            outp.WriteLine("n\testimated value\tdifference\tdifference theory");
            for(int i = 100; i <= 1000; i+=100)
            {
                var h = (Function.b - Function.a) / i;
                var res = Integration.IntegrateSimson(Function.a, Function.b, i, Function.F);
                outp.WriteLine("{0}\t{1}\t{2}\t{3}", i, res, Math.Abs(real - res), 
                    Function.MaxFThirdDerevative() * (Function.b - Function.a) * h * h * h / 288);
            }
            outp.Close();
        }
    }
}
