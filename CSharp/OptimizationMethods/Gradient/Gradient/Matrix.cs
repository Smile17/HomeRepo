using System;

namespace Gradient
{
    class Matrix
    {
        Double[,] Arr;
        public double this[int x, int y]
        {
            get { return Arr[x, y]; }
            set { Arr[x, y] = value; }
        }

        public Matrix(int Rows, int Columns)
        {
            Arr = new Double[Rows, Columns];
        }
        public Matrix(Matrix M)
        {
            Arr = new Double[M.N, M.M];
            for (int i = 0; i < M.N; i++)
                for (int j = 0; j < M.M; j++)
                {
                    this[i, j] = M[i, j];
                }
        }
        public Matrix(double[,] a)
        {
            Arr = new Double[a.GetUpperBound(0) + 1, a.GetUpperBound(1) + 1];
            for (int i = 0; i < a.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < a.GetUpperBound(1) + 1; j++)
                {
                    this[i, j] = a[i, j];
                }
        }
        public Matrix()
        {
            Arr = new Double[3, 2];
            for (int i = 0; i < Arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < Arr.GetUpperBound(1) + 1; j++)
                {
                    this[i, j] = (i + 1) * (j + 1);
                    //this[i, j] = 0;
                }
                //this[i, i]=1;
            }

        }

        /// <summary>
        /// Rows
        /// </summary>
        public int N { get { return this.Arr.GetUpperBound(0) + 1; } }
        /// <summary>
        /// Columns
        /// </summary>
        public int M { get { return this.Arr.GetUpperBound(1) + 1; } }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.M != b.N) return null;
            Matrix c = new Matrix(a.N, b.M);
            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < a.M; k++) c[i, j] += a[i, k] * b[k, j];
                }
            }
            return c;
        }
        public static Vector operator *(Matrix a, Vector b)
        {
            if (a.M != b.n) return null;
            Vector c = new Vector(a.N);
            for (int i = 0; i < c.n; i++)
            {
                c[i] = 0;
                for (int k = 0; k < a.M; k++) c[i] += a[i, k] * b[k];

            }

            return c;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.N != b.N) return null;
            if (a.M != b.M) return null;
            Matrix c = new Matrix(a.N, b.M);
            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.N != b.N) return null;
            if (a.M != b.M) return null;
            Matrix c = new Matrix(a.N, b.M);
            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }
            return c;
        }

        public static Matrix operator *(double a, Matrix b)
        {
            Matrix c = new Matrix(b.N, b.M);
            for (int i = 0; i < c.N; i++)
            {
                for (int j = 0; j < c.M; j++)
                {
                    c[i, j] = a * b[i, j];
                }
            }
            return c;
        }
        public Matrix GetTransposed()
        {
            Matrix a = new Matrix(M, N);
            for (int i = 0; i < N; i++) for (int j = 0; j < M; j++) a[j, i] = this[i, j];
            return a;
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++) s += String.Format("{0:F4}\t", this[i, j]);
                if (i != N - 1) s += "\n";
            }
            return s;
        }
        public Matrix Copy()
        {
            return new Matrix(this);
        }


        private int MaxAbsColumn(int col)
        {
            int jmax = col;
            for (int i = col; i < this.N; i++)
            {
                if (Math.Abs(this[jmax, col]) < Math.Abs(this[i, col])) jmax = i;
            }
            return jmax;
        }
        private void ReplaceRows(int i, int j)
        {
            Matrix Arr = new Matrix(this);
            for (int k = 0; k < this.M; k++)
            {
                this[i, k] = Arr[j, k];
                this[j, k] = Arr[i, k];
            }
        }
        private void MinusRows(int i, int j, double k) //rows(i)=row(i)-k*rows(j)
        {
            for (int t = 0; t < this.M; t++)
            {
                this[i, t] -= k * this[j, t];
            }

        }


        public Matrix GaussSolve(Matrix RHS)
        {
            if (RHS.N == 0 || RHS.M == 0) return null;
            if (RHS.N != N) return null;
            Matrix Ans = new Matrix(RHS);
            Matrix arr = new Matrix(this);
            int jmax; double k = 0;
            for (int i = 0; i < arr.N; i++)
            {
                jmax = arr.MaxAbsColumn(i);
                arr.ReplaceRows(jmax, i);
                Ans.ReplaceRows(jmax, i);
                for (int j = i + 1; j < arr.M; j++)
                {
                    k = arr[j, i] / arr[i, i];
                    arr.MinusRows(j, i, k);
                    Ans.MinusRows(j, i, k);

                }
            }
            for (int i = arr.M - 1; i >= 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    k = arr[j, i] / arr[i, i];
                    arr.MinusRows(j, i, k);
                    Ans.MinusRows(j, i, k);
                }
            }
            for (int i = 0; i < arr.N; i++)
                for (int j = 0; j < Ans.M; j++)
                {
                    Ans[i, j] = Ans[i, j] / arr[i, i];
                }
            return Ans;
        }

    }
}
