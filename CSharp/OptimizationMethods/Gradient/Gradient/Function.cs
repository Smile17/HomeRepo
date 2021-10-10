namespace Gradient
{
    class MyFunction
    {
        public static double eps = 0.0001;
        public static double f0(Vector vec)
        {
            return vec[0] * vec[0] + vec[1] * vec[1];
        }
        public static Func Qudratic(Matrix A, Vector B, double c) //all Qudratic function
        {
            Func F1 = delegate (Vector x)
            {
                return A*x*x+B*x+c;
            };
            return F1;
        }
        

        public delegate double Func(Vector v);
        public delegate Vector Func2(Vector v, Matrix B);

        public static Vector G1(Vector v0, Matrix B)
        {
            Vector v1 = B * v0;
            return v1;
        }

        public static Func Composition(Func F, Func2 G, Matrix B)
        {
            Func F1 = delegate (Vector v0)
              {
                  return F(G(v0, B));
              };
            return F1;
        }

        public static Func Change(Vector r, Func F)
        {
            Func F1 = delegate (Vector v0)
            {
                Vector v1 = new Vector(v0);
                for (int i = 0; i < v0.n; i++)
                    v1[i] = v1[i] / r[i];
                return F(v1);
            };
            return F1;
        }
        
        
        public static Vector grad(Vector vec, double h, Func F)
        {
            Vector temp = new Vector(vec);
            Vector res = new Vector(vec.n);
            for (int i = 0; i < vec.n; i++)
            {
                temp[i] = vec[i] + h;
                vec[i] = vec[i] - h;
                res[i] = (F(temp) - F(vec)) / (2 * h);
                vec[i] = vec[i] + h;
                temp[i] = temp[i] - h;
            }
            return res;
        }
        public static Matrix Gesse(Vector vec, double h, Func F)
        {
            Matrix G = new Matrix(vec.n, vec.n);
            Vector hi = new Vector(vec.n);
            Vector hj = new Vector(vec.n);
            for (int i = 0; i < G.N; i++)
            {
                for (int j = 0; j < G.M; j++)
                {
                    hi[i] = h; hj[j] = h;
                    G[i, j] = (F(vec + hi + hj) + F(vec - hi - hj) - F(vec + hi - hj) - F(vec - hi + hj)) / (4 * h * h);
                    hi[i] = hj[j] = 0;
                }
            }
            return G;

        }
    }
}
