using MathHelper.Algebra;
using System;

namespace MathHelper.Analysis
{
    class Function
    {
        public delegate double Func(Vector v);
        public delegate Vector FuncMatrix(Vector v, Matrix B);
        public static double eps = 0.0001;
        #region Functions
        public static double f0(Vector vec)
        {
            return vec[0] * vec[0] + vec[1] * vec[1];
        }
        public static double f1(Vector vec)
        {
            return Math.Max(vec[0] * vec[0], 1);
        }
        public static Func Qudratic(Matrix A, Vector B, double c) //all Qudratic function
        {
            Func F1 = delegate (Vector x)
            {
                return A * x * x + B * x + c;
            };
            return F1;
        }
        #endregion
        //Just as an example of the function creation
        public static Func Composition(Func F, FuncMatrix G, Matrix B)
        {
            Func F1 = delegate (Vector v0)
            {
                return F(G(v0, B));
            };
            return F1;
        }
        #region Math operations
        public static Vector Grad(Vector v, double h, Func F)
        {
            Vector temp = new Vector(v);
            Vector res = new Vector(v.Length);
            for (int i = 0; i < v.Length; i++)
            {
                temp[i] = v[i] + h;
                v[i] = v[i] - h;
                res[i] = (F(temp) - F(v)) / (2 * h);
                v[i] = v[i] + h;
                temp[i] = temp[i] - h;
            }
            return res;
        }
        public static Matrix Gesse(Vector v, double h, Func F)
        {
            Matrix G = new Matrix(v.Length, v.Length);
            Vector hi = new Vector(v.Length);
            Vector hj = new Vector(v.Length);
            for (int i = 0; i < G.N; i++)
            {
                for (int j = 0; j < G.M; j++)
                {
                    hi[i] = h; hj[j] = h;
                    G[i, j] = (F(v + hi + hj) + F(v - hi - hj) - F(v + hi - hj) - F(v - hi + hj)) / (4 * h * h);
                    hi[i] = hj[j] = 0;
                }
            }
            return G;
        }
        #endregion
    }
}
