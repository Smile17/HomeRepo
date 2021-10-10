using System;
using System.IO;
namespace Interpolation
{
    public class InterpolationNewton : Interpolation
    {
        public InterpolationNewton(double[] x, double[] y):base(x, y){}
        protected override void InitPolynomialCoeficients()
        {
            int N = x0.Length;
            double[] coef = new double[N];
            double a, temp; int j;
            coef[0] = y0[0];
            for (int i = 1; i < N; i++)
            {
                a = 1;
                temp = y0[i];
                for (j = 0; j < i; j++)
                {
                    temp -= a * coef[j];
                    a *= x0[i] - x0[j];
                }
                coef[i] = temp / a;
            }
            Coef = coef;
        }
        public override string ToString()
        {
            int N = x0.Length;
            string sum = "";
            string add = "";
            for (int i = 0; i < N; i++)
            {
                string c = Coef[i] >= 0 ? Coef[i].ToString() : String.Format("({0:f3})", Coef[i]); 
                sum += String.Format("{0}{1}", c, add);
                if(i != (N - 1)) sum += "+";

                if(x0[i] == 0) add += String.Format("x");
                if(x0[i] > 0) add += String.Format("(x - {0:f3})", x0[i]);
                if(x0[i] < 0) add += String.Format("(x - ({0:f3}))", x0[i]);
                //if((j != (N - 1) && j != 0) && (!(j == (N - 2) && i == (N - 1)))) add += "*";
                
            }
            return sum;
        }
        public override double Interpolate(double x)
        {
            // Calculate coeficients if they are not calculated yet
            if(Coef == null)
                InitPolynomialCoeficients();
            int N = x0.Length;
            
            double s = Coef[0];
            double a = 1;
            int flag = 0;
            for (int i = 0; i < N; i++)
                if (Math.Abs(x - x0[i]) < Interpolation.EPS)
                {
                    s = y0[i];
                    flag = 1;
                }
            if (flag == 0)
            {
                for (int i = 1; i < N; i++)
                {
                    a *= x - x0[i - 1];
                    s += Coef[i] * a;
                }
            }
            return s;
        }

        // Method for testing
        public static void TestInterpolation(double[] x0, double[] y0, Interpolation.Func f)
        {
            StreamWriter outp = new StreamWriter("output_newton.txt");
            // Calculate and output Newtons's poynomial
            InterpolationNewton newt = new InterpolationNewton(x0, y0);
            outp.WriteLine(newt.ToString());
            outp.WriteLine();
            // Check Newton's polynomial in (x0[i], y0[i]) points and points between them
            double x, y, y_interpolated; 

            newt.CheckInterpolation(f, outp);
            
            outp.WriteLine("Interpolation for x4 + 0.021:");
            outp.WriteLine("x;y_interpolated;y_real;difference");
            x = x0[4] + 0.021; y = f(x);
            y_interpolated = newt.Interpolate(x);
            outp.WriteLine("{0}\t{1}\t{2}\t{3}", x, y_interpolated, y, y_interpolated - y);
            outp.WriteLine("Interpolation for x7 - 0,0146:");
            outp.WriteLine("x;y_interpolated;y_real;difference");
            x = x0[7] - 0.0146; y = f(x);
            y_interpolated = newt.Interpolate(x);
            outp.WriteLine("{0}\t{1}\t{2}\t{3}", x, y_interpolated, y, y_interpolated - y);


            outp.Close();
        }
        
    }
}
