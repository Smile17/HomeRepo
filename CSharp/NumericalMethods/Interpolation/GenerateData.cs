using System;
using System.IO;
using System.Collections.Generic;

namespace Interpolation
{
    public class GenerateData
    {
        public static void Generate(Interpolation.Func f, double a, double b, double step, out double[] x, out double[] y)
        {
            double x0 = a;
            List<double> xi = new List<double>(); List<double> yi = new List<double>();
            while(x0<b)
            {
                xi.Add(x0);
                yi.Add(f(x0));
                x0+=step;
            }
            x = xi.ToArray(); y = yi.ToArray();
        }
        public static void SaveToFile(double[] x, double[] y, string file)
        {
            StreamWriter outp = new StreamWriter(file);
            outp.WriteLine("x_i\ty_i");
            for(int i = 0; i < x.Length; i++)
            {
                outp.WriteLine("{0:f2}\t{1:f4}", x[i], y[i]);
            }
            outp.Close();
        }
    }
}
