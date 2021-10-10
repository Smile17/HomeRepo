using MathHelper.Algebra;
using MathHelper.Excel;
using System;

namespace TMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix("A.txt");
            Matrix B = new Matrix("B.txt");
            Matrix C = new Matrix("C.txt");
            ExcelMatrix.Table("tmp1",Composition.Multiple(A,A), Composition.Multiple(B, B));
            ExcelMatrix.Table("tmp2", Composition.MaxMin(A, B), Composition.MinMax(A, B),Composition.Multiple(A, B));
            ExcelMatrix.Table("tmp3", Composition.Multiple(C, C));
            Console.WriteLine("Process is completed");
            Console.ReadLine();
        }
    }
}
