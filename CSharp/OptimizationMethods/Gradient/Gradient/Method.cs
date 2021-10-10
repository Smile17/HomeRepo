using System;
using System.IO;

namespace Gradient
{
    class Method
    {
        #region Gradient Classical
        public static void GradDivisionStep(MyFunction.Func F, Vector v0, double alpha, double lamda, double eps, StreamWriter outp)
        {
            Vector vk = new Vector(v0);
            outp.Write("\t");
            for (int j = 0; j < vk.n; j++)
                outp.Write("x[{0}] \t", j);
            outp.WriteLine("F\t\t\t ||xk+1-xk||\t\t\t\t alpha");
            int i = 0;
            Vector vk1 = new Vector(vk);
            outp.WriteLine("{0}\t {1} {2}\t", i, vk1, F(vk1));
            do
            {
                vk1.Copy(vk);   //vk=vk1
                var grad = MyFunction.grad(vk, 0.0001, F);
                vk1 = vk - (alpha) * (grad);
                while (F(vk1) - F(vk) > 0)
                {
                    alpha = alpha * lamda;
                    grad = MyFunction.grad(vk, 0.0001, F);
                    vk1 = vk - (alpha / grad.Norm()) * (grad);
                }
                i++;
                outp.WriteLine("{0}\t {1} {2}\t {3}\t\t\t{4}", i, vk1, F(vk1), (vk1 - vk).Norm(), alpha);
            }
            while ((i < 1000) && ((vk1 - vk).Norm() > eps));
            outp.WriteLine("   Result: vec={0}  F={1}", vk1, F(vk1));
        }

        public static void GradPermanentStep(MyFunction.Func F, Vector v0, double alpha, double eps, StreamWriter outp)
        {
            Vector vk = new Vector(v0);
            outp.Write("\t");
            for (int j = 0; j < vk.n; j++)
                outp.Write("x[{0}] \t", j);
            outp.WriteLine("F\t\t\t ||xk+1-xk||\t\t\t\t alpha");
            int i = 0;
            Vector vk1 = new Vector(vk);
            outp.WriteLine("{0}\t {1} {2}\t", i, vk1, F(vk1));
            do
            {
                vk1.Copy(vk);   //vk=vk1
                var grad = MyFunction.grad(vk, 0.0001, F);
                vk1 = vk - (alpha) * (grad);
                i++;
                outp.WriteLine("{0}\t {1} {2}\t {3}", i, vk1, F(vk1), (vk1 - vk).Norm());
            }
            while ((i < 1000) && ((vk1 - vk).Norm() > eps));

            outp.WriteLine("   Result: vec={0}  F={1}", vk1, F(vk1));
        }

        public static void GradFormulaStep(MyFunction.Func F, Vector v0, double eps, StreamWriter outp)
        {
            Vector vk = new Vector(v0);
            outp.Write("\t");
            for (int j = 0; j < vk.n; j++)
                outp.Write("x[{0}] \t", j);
            outp.WriteLine("F\t\t\t ||xk+1-xk||\t\t\t\t alpha");
            int i = 0;
            Vector vk1 = new Vector(vk);
            outp.WriteLine("{0}\t {1} {2}\t", i, vk1, F(vk1));
            do
            {
                vk1.Copy(vk);   //vk=vk1
                var grad = MyFunction.grad(vk, 0.0001, F);
                var alpha = 1.0 / (i + 1);
                vk1 = vk - (alpha) * (grad);
                i++;
                outp.WriteLine("{0}\t {1} {2}\t {3}", i, vk1, F(vk1), (vk1 - vk).Norm());
            }
            while ((i < 1000) && ((vk1 - vk).Norm() > eps));

            outp.WriteLine("   Result: vec={0}  F={1}", vk1, F(vk1));
        }
        #endregion

        #region Gradient General
        public static void GradGeneralDivisionStep(MyFunction.Func F, Vector v0, double alpha, double lamda, double eps, StreamWriter outp)
        {
            Vector vk = new Vector(v0);
            outp.Write("\t");
            for (int j = 0; j < vk.n; j++)
                outp.Write("x[{0}] \t", j);
            outp.WriteLine("F\t\t\t ||xk+1-xk||\t\t\t\t alpha");
            int i = 0;
            Vector vk1 = new Vector(vk);
            outp.WriteLine("{0}\t {1} {2}\t", i, vk1, F(vk1));
            do
            {
                vk1.Copy(vk);   //vk=vk1
                var grad = MyFunction.grad(vk, 0.0001, F);
                vk1 = vk - (alpha / grad.Norm()) * (grad);
                while (F(vk1) - F(vk) > 0)
                {
                    alpha = alpha * lamda;
                    grad = MyFunction.grad(vk, 0.0001, F);
                    vk1 = vk - (alpha / grad.Norm()) * (grad);
                }
                i++;
                outp.WriteLine("{0}\t {1} {2}\t {3}\t\t\t{4}", i, vk1, F(vk1), (vk1 - vk).Norm(), alpha);
            }
            while ((i < 1000) && ((vk1 - vk).Norm() > eps));
            outp.WriteLine("   Result: vec={0}  F={1}", vk1, F(vk1));
        }

        public static void GradGeneralPermanentStep(MyFunction.Func F, Vector v0, double alpha, double eps, StreamWriter outp)
        {
            Vector vk = new Vector(v0);
            outp.Write("\t");
            for (int j = 0; j < vk.n; j++)
                outp.Write("x[{0}] \t", j);
            outp.WriteLine("F\t\t\t ||xk+1-xk||\t\t\t\t alpha");
            int i = 0;
            Vector vk1 = new Vector(vk);
            outp.WriteLine("{0}\t {1} {2}\t", i, vk1, F(vk1));
            do
            {
                vk1.Copy(vk);   //vk=vk1
                var grad = MyFunction.grad(vk, 0.0001, F);
                vk1 = vk - (alpha / grad.Norm()) * (grad);
                i++;
                outp.WriteLine("{0}\t {1} {2}\t {3}", i, vk1, F(vk1), (vk1 - vk).Norm());
            }
            while ((i < 1000) && ((vk1 - vk).Norm() > eps));

            outp.WriteLine("   Result: vec={0}  F={1}", vk1, F(vk1));
        }

        public static void GradGeneralFormulaStep(MyFunction.Func F, Vector v0, double eps, StreamWriter outp)
        {
            Vector vk = new Vector(v0);
            outp.Write("\t");
            for (int j = 0; j < vk.n; j++)
                outp.Write("x[{0}] \t", j);
            outp.WriteLine("F\t\t\t ||xk+1-xk||\t\t\t\t alpha");
            int i = 0;
            Vector vk1 = new Vector(vk);
            outp.WriteLine("{0}\t {1} {2}\t", i, vk1, F(vk1));
            do
            {
                vk1.Copy(vk);   //vk=vk1
                var grad = MyFunction.grad(vk, 0.0001, F);
                var alpha = 1.0 / (i + 1);
                vk1 = vk - (alpha / grad.Norm()) * (grad);
                i++;
                outp.WriteLine("{0}\t {1} {2}\t {3}", i, vk1, F(vk1), (vk1 - vk).Norm());
            }
            while ((i < 1000) && ((vk1 - vk).Norm() > eps));

            outp.WriteLine("   Result: vec={0}  F={1}", vk1, F(vk1));
        }
        #endregion
    }
}

