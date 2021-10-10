using System;
using System.IO;

namespace SystemOfLinearEquations
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix("input_matrix.txt");
            Matrix b = new Matrix("input_b.txt");
            Matrix res;
            string steps ="";
            StreamWriter outp = new StreamWriter("output.txt");
            outp.WriteLine("Method of main components:");
            A.GaussSolveModified(b, out res, ref steps, true);
            outp.WriteLine(steps);
            outp.WriteLine();

            StreamWriter outp_itc = new StreamWriter("output_zeidel_iterations.txt");
            outp_itc.WriteLine("eps;iteration number");
            outp.WriteLine("Method of Zeidel:");

            double eps = 0.1;
            for(int i = 0; i < 14; i++)
            {
                outp.WriteLine("EPS: {0}", eps);
                int itc = 0;
                A.ZaidelSolve(b, out res, true, ref steps, ref itc, true, eps);
                outp.WriteLine(steps);
                outp.WriteLine();
                outp_itc.WriteLine("{0};{1}", eps, itc);
                eps /= 10;
            }
            outp.Close();
            outp_itc.Close();
        }
    }
}
