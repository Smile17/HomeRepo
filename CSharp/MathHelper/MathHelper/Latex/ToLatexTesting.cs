using MathHelper.Algebra;
using System.IO;

namespace MathHelper.Latex
{
    class ToLatexTesting
    {
        public static void MatrixToLatex()
        {
            StreamWriter outp = new StreamWriter("Test 1.txt");
            outp.WriteLine(ToLatex.MatrixToLatex(new Matrix(), null, "Test 1"));
            outp.Close();
        }
        public static void VectorToLatex()
        {
            StreamWriter outp = new StreamWriter("Test 2.txt");
            outp.WriteLine(ToLatex.VectorToLatex("Test 2", "X", "Y", 0, 3, 1, new string[] { "Line 1"}, new Vector(2,4,7,3)));
            outp.Close();
        }

    }
}
