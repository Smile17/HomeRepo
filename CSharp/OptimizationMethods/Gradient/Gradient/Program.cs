using System;
using System.IO;


namespace Gradient
{
    class Program
    {
        static void Main(string[] args)
        {
            var eps = 0.0001;
            StreamWriter outp = new StreamWriter("Method_Results_Classical.txt");
            StreamWriter outp2 = new StreamWriter("Method_Results_General.txt");
            Vector v0 = new Vector(1, 2);
            double[,] arr = new double[,] { { 3, 0.5 }, { 0.5, 1 } };
            Matrix A = new Matrix(arr);
            Vector B = new Vector(1, 5);
            double c = 0;
            MyFunction.Func F = MyFunction.Qudratic(A, B, c);
            Method.GradDivisionStep(F, v0, 5, 0.5, eps, outp);
            Method.GradGeneralFormulaStep(F, v0, eps, outp2);

            outp.Close();
            outp2.Close();
            Console.WriteLine("Done ...");
            Console.ReadLine();
        }
    }
}
