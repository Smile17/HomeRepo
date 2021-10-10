using System;
using System.IO;

namespace DifferentialEquation
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter outp = new StreamWriter("output.txt");
            {
                DifferentialEquation de = new DifferentialEquation(Function.x0, Function.y0, 100, Function.F);
                de.EvaluateEuler(); outp.WriteLine("Euler 0.01:"); de.Print(outp, Function.AnsF);
            }
            {
                DifferentialEquation de = new DifferentialEquation(Function.x0, Function.y0, 1000, Function.F);
                de.EvaluateEuler(); outp.WriteLine("Euler 0.001:"); de.Print(outp, Function.AnsF);
            }
            {
                DifferentialEquation de = new DifferentialEquation(Function.x0, Function.y0, 100, Function.F);
                de.EvaluateRungeThirdOrder(); outp.WriteLine("RungeThirdOrder 0.01:"); de.Print(outp, Function.AnsF);
            }
            {
                DifferentialEquation de = new DifferentialEquation(Function.x0, Function.y0, 1000, Function.F);
                de.EvaluateRungeThirdOrder(); outp.WriteLine("RungeThirdOrder 0.001:"); de.Print(outp, Function.AnsF);
            }

            outp.Close();
        }
    }
}
