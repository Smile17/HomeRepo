using System;
using System.IO;
namespace Interpolation
{
    public class InterpolationLagrange : Interpolation
    {
        public InterpolationLagrange(double[] x, double[] y):base(x, y){}

        protected override void InitPolynomialCoeficients()
        {
            int N = x0.Length;
            double[] coef = new double[N];
            for (int i = 0; i < N; i++)
            {
                coef[i] = y0[i];
                for (int j = 0; j < N; j++)
                    if (i != j)
                        coef[i] = coef[i] / (x0[i] - x0[j]);
            }
            Coef = coef;
        }
        public override string ToString()
        {
            int N = x0.Length;
            string sum = "";
            for (int i = 0; i < N; i++)
            {
                string add = "";
                for (int j = 0; j < N; j++)
                {
                    if (i != j)
                    {
                        if(x0[j] == 0) add += String.Format("x");
                        if(x0[j] > 0) add += String.Format("(x - {0:f3})", x0[j]);
                        if(x0[j] < 0) add += String.Format("(x - ({0:f3}))", x0[j]);
                        //if((j != (N - 1) && j != 0) && (!(j == (N - 2) && i == (N - 1)))) add += "*";
                    }
                }
                string c = Coef[i] >= 0 ? Coef[i].ToString() : String.Format("({0:f3})", Coef[i]); 
                sum += String.Format("{0}{1}", c, add);
                if(i != (N - 1)) sum += "+";
            }
            return sum;
        }
        public override double Interpolate(double x)
        {
            // Calculate coeficients if they are not calculated yet
            if(Coef == null)
                InitPolynomialCoeficients();
            int N = x0.Length;
            double s = 0, temp; int j, flag = 0;
            for (int i = 0; i < N; i++)
                if (Math.Abs(x - x0[i]) < Interpolation.EPS)
                {
                    s = y0[i];
                    flag = 1;
                }
            if (flag == 0)
            {
                for (int i = 0; i < N; i++)
                {
                    temp = Coef[i];
                    for (j = 0; j < N; j++)
                        if (i != j)
                            temp *= x - x0[j];
                    s += temp;
                }
            }
            return s;
        }

        // Method for testing
        public static void TestInterpolation(double[] x0, double[] y0, Interpolation.Func f)
        {
            StreamWriter outp = new StreamWriter("output_lagrange.txt");
            // Calculate and output Lagrange's poynomial
            InterpolationLagrange lagr = new InterpolationLagrange(x0, y0);
            outp.WriteLine(lagr.ToString());
            outp.WriteLine();
            // Check Lagrange's polynomial in (x0[i], y0[i]) points and points between them
            lagr.CheckInterpolation(f, outp);

            outp.Close();
        }

    }
}
