using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathHelper.Algebra
{
    public class Matrix
    {
        public Double[,] Arr { get; set; }
        #region Size of a matrix
        /// <summary>
        /// Rows
        /// </summary>
        public int N { get { return this.Arr.GetUpperBound(0) + 1; } }
        /// <summary>
        /// Columns
        /// </summary>
        public int M { get { return this.Arr.GetUpperBound(1) + 1; } }
        #endregion
        #region Constructors
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
        //Construct a matrix just for testing
        public Matrix()
        {
            Arr = new Double[3, 2];
            for (int i = 0; i < Arr.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < Arr.GetUpperBound(1) + 1; j++)
                {
                    this[i, j] = (i + 1) * (j + 1);
                }
            }
        }

        public Matrix(string name)
        {
            StreamReader inp = new StreamReader(name);
            string s = inp.ReadLine();
            string[] str = s.Split(' ');
            Arr = new double[Int32.Parse(str[0]), Int32.Parse(str[1])];
            this.Init(inp);
        }
        public Matrix(string name, int N, int M)
        {
            StreamReader inp = new StreamReader(name);
            Arr = new double[N, M];
            this.Init(inp);
        }
        public void Init(StreamReader inp, char[] del = null)
        {
            del = del ?? new char[] {' ', '\t', ';', ','};
            string s = "";
            for (int i = 0; i < this.N; i++)
            {
                s = inp.ReadLine();
                string[] str2 = s.Split(del);
                for (int j = 0; j < this.M; j++)
                {
                    this[i, j] = Double.Parse(str2[j]);
                }
            }
        }
        #endregion
        public static Matrix IMatrix(int L)
        {
            Matrix A = new Matrix(L, L);
            for (int i = 0; i < L; i++)
                A[i, i] = 1;
            return A;
        }
        #region Index support
        public double this[int x, int y]
        {
            get { return Arr[x, y]; }
            set { Arr[x, y] = value; }
        }
        #endregion
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++) s += String.Format("{0:F1}\t", this[i, j]);
                if (i != N - 1) s += "\n";
            }
            return s;
        }
        public Matrix GetTransposed()
        {
            Matrix a = new Matrix(M, N);
            for (int i = 0; i < N; i++) for (int j = 0; j < M; j++) a[j, i] = this[i, j];
            return a;
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
        #region Private methods
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
        #endregion
        /// <summary>
        /// Normalize values of matrix inside each columns
        /// Each values will be inside [0;1] intervals
        /// </summary>
        /// <returns>The matrix with max and min values from each columns. 
        /// Later we can recover an initial matrix using this one</returns>
        public Matrix NormalizeColumns()
        {
            Matrix MaxMin = new Matrix(2, this.M);

            for (int i = 0; i < this.M; i++)
            {
                MaxMin[0, i] = MaxMin[1, i] = this[0, i];

                for (int j = 1; j < this.N; j++)
                {
                    if (this[j, i] > MaxMin[0, i]) MaxMin[0, i] = this[j, i];
                    if (this[j, i] < MaxMin[1, i]) MaxMin[1, i] = this[j, i];
                }
            }
            for (int i = 0; i < this.M; i++)
                for (int j = 0; j < this.N; j++)
                {
                    this[j, i] = (this[j, i] - MaxMin[1, i]) / (MaxMin[0, i] - MaxMin[1, i]);
                }
            return MaxMin;
        }
        /// <summary>
        /// Removes the first column of the matrix
        /// </summary>
        /// <returns>A new matrix without first column</returns>
        public Matrix RemoveFirstColumn()
        {
            Matrix res = new Matrix(this.N, this.M - 1);
            for (int i = 0; i < this.N; i++)
                for (int j = 1; j < this.M; j++)
                    res[i, j - 1] = this[i, j];
            return res;
        }
        /// <summary>
        /// Convert one specific column of matrix to a vector
        /// </summary>
        /// <param name="column">The column number</param>
        /// <returns></returns>
        public Vector ColumnToVector(int column)
        {
            Vector v = new Vector(this.N);
            for (int i = 0; i < v.Length; i++)
                v[i] = this[i, column];
            return v;
        }
        #region Operation overrides
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
            if (a.M != b.Length) return null;
            Vector c = new Vector(a.N);
            for (int i = 0; i < c.Length; i++)
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
        #endregion
        
    }
}
