using System;

namespace Interpolation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate x_i and y_i and save them to file data.txt
            double[] x, y;
            GenerateData.Generate(Function.F, Function.a, Function.b, Function.step, out x, out y);
            GenerateData.SaveToFile(x, y, "data.txt");

            InterpolationLagrange.TestInterpolation(x, y, Function.F);
            InterpolationNewton.TestInterpolation(x, y, Function.F);
        }
    }
}
