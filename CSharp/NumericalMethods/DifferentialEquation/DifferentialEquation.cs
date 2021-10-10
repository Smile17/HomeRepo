using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DifferentialEquation
{
    public class DifferentialEquation
    {
        public delegate double Func(double x, double y);
        public delegate double FuncAns(double x);
        double h; double[] x, y;
        Func F;
        public DifferentialEquation(double x0, double y0, int n, Func f)
        {
            x = new double[n + 1];
            y = new double[n + 1];
            x[0] = x0; y[0] = y0; h = 1.0 / (double)n;
            F = f;
        }

        public void EvaluateEuler()
        {
            for (int i = 0; i < x.Length - 1; i++)
            {
                var k = F(x[i], y[i]);
                y[i + 1] = y[i] + h / 2 * (k + F(x[i]+h, y[i] + h * k));
                x[i + 1] = x[i] + h;
            }
        }

        public void EvaluateRungeThirdOrder()
        {
            for (int i = 0; i < x.Length - 1; i++)
            {
                double k1, k2, k3;
                k1 = F(x[i], y[i]);
                k2 = F(x[i] + h / 2, y[i] + h * k1 / 2);
                k3 = F(x[i] + h / 2, y[i] + h * (2* k2 -k1));
                y[i + 1] = y[i] + h / 6 * (k1 + 4 * k2 + k3);
                x[i + 1] = x[i] + h;
            }
        }
        public void Print(StreamWriter outp, FuncAns Ans)
        {
            outp.WriteLine("x[i]\ty[i]\tAns(y[i])\tDifference");
            double avr_error = 0;
            for (int i = 0; i < x.Length; i++)
            {
                var x_real = Ans(y[i]);
                avr_error += x[i]-x_real;
                outp.WriteLine("{0}\t{1}\t{2}\t{3}", x[i], y[i], x_real, x[i]-x_real);
            }
            outp.WriteLine();
            outp.WriteLine("Average error: {0}", avr_error / x.Length);
        }
    }
}
