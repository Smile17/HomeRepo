using System;
using System.IO;
namespace Interpolation
{
    public class Interpolation
    {
        public delegate double Func(double x);
        public static double  EPS = 0.0001;
        public double[] Coef = null;
        public double[] x0, y0;

        public Interpolation(double[] x, double[] y){x0 = x; y0 = y; InitPolynomialCoeficients();}

        protected virtual void InitPolynomialCoeficients(){}
        public virtual double Interpolate(double x){return 0;}

        protected void CheckInterpolation(Interpolation.Func f, StreamWriter outp)
        {
            double x, y, y_interpolated; double avr_error = 0;
            outp.WriteLine("x\ty interpolated\ty real\terror");
            for(int i = 0; i < x0.Length; i++)
            {
                y_interpolated = this.Interpolate(x0[i]);
                outp.WriteLine("{0:f2}\t{1}\t{2}\t{3}", x0[i], y_interpolated, y0[i], y_interpolated - y0[i]);
                avr_error += Math.Abs(y_interpolated - y0[i]);
                if(i < x0.Length - 1)
                {
                    x = (x0[i] + x0[i + 1])/2; y = f(x);
                    y_interpolated = this.Interpolate(x);
                    outp.WriteLine("{0:f2}\t{1}\t{2}\t{3}", x, y_interpolated, y, y_interpolated - y);
                    avr_error += Math.Abs(y_interpolated - y);
                }
            }
            outp.WriteLine("Average error: {0}", avr_error / (2 * x0.Length - 1));
        }
        
    }
}
