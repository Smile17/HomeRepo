using System;
using System.IO;

namespace MathHelper.Algebra
{
    public class Vector : ICloneable
    {
        public double[] Vec { get; set; }
        #region Size of a vector
        public int Length { get { return this.Length; } }
        #endregion
        #region Constructors
        public Vector()
        {
            Vec = new double[0];
        }
        public Vector(int size)
        {
            Vec = new double[size];
        }
        public Vector(Vector v)
        {
            Vec = new double[v.Length];
            for (int i = 0; i < v.Length; i++)
                Vec[i] = v.Vec[i];
        }
        public Vector(StreamReader inp)
        {
            string line = inp.ReadLine();
            Vec = new double[Int32.Parse(line)];
            for (int i = 0; i < Length; i++)
            {
                Vec[i] = Double.Parse(inp.ReadLine());
            }
        }
        public Vector(params double[] a)
        {
            //N = a.Length;
            Vec = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
                Vec[i] = a[i];
        }
        #endregion
        #region Clone & Copy
        public object Clone()
        {
            Vector v = new Vector(this.Length);
            for (int i = 0; i < this.Length; i++)
                v[i] = this[i];
            return v;
        }
        public void Copy(Vector v)//v=this
        {
            for (int i = 0; i < this.Length; i++)
                v[i] = this[i];
        }
        public Vector Copy(int K, int N)
        {
            Vector V = new Vector(N - K);
            for (int i = 0; i < V.Length; i++)
                V[i] = this[K + i];
            return V;
        }
        #endregion
        #region Index support
        public double this[int key]
        {
            get
            { return Vec[key]; }
            set { Vec[key] = value; }
        }
        #endregion

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < this.Length; i++)
                s += String.Format("{0}\n", this[i]);
            return s;
        }
        #region Statistics
        public double Variance()
        {
            int n = this.Length;
            double s = this.Sum() / n;
            double v = 0;
            for (int i = 0; i < n; i++)
            { v += Vec[i] * Vec[i]; }
            v = v / n - s * s;
            return v * n / (n - 1);
        }
        public double Norm()
        {
            double s = 0;
            for (int i = 0; i < this.Length; i++)
            { s = s + this[i] * this[i]; }
            return Math.Sqrt(s);
        }
        public double Sum()
        {
            double s = 0;
            for (int i = 0; i < this.Length; i++)
            { s = s + this[i]; }
            return s;
        }
        public static double ResidualMaxAbs(Vector a, Vector b)
        {
            double res = Math.Abs(a[0] - b[0]);
            for (int i = 1; i < a.Length; i++)
                if (res < Math.Abs(a[i] - b[i])) res = Math.Abs(a[i] - b[i]);
            return res;
        }
        public double Mean()
        {
            return this.Sum() / (double)this.Length;
        }
        public static double Correlation(Vector a, Vector b)
        {
            double s = a * b / (double)a.Length;
            s = s - a.Mean() * b.Mean();
            return s;
        }
        public static double CorrelationCoeficient(Vector a, Vector b)
        {
            return Correlation(a, b) / (double)Math.Sqrt((double)(a.Variance() * b.Variance()));
        }
        public double Min()
        {
            double min = this[0];
            for (int i = 1; i < this.Length; i++)
            {
                if (min > this[i]) min = this[i];
            }
            return min;
        }
        public double Max()
        {
            double max = this[0];
            for (int i = 1; i < this.Length; i++)
            {
                if (max < this[i]) max = this[i];
            }
            return max;
        }
        #endregion
        public Vector Pow(int n)
        {
            Vector v = new Vector(this.Length);
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = Math.Pow(this[i], n);
            }
            return v;
        }
        public static Vector MultiplyByCoordinates(Vector a, Vector b)
        {
            Vector v = new Vector(a.Length);
            for (int i = 0; i < v.Length; i++)
                v[i] = a[i] * b[i];
            return v;
        }
        #region Operation overrides
        public static Vector operator *(double k, Vector v)
        {
            return v.Multiple(k);
        }
        private Vector Multiple(double K)
        {
            Vector v = new Vector(this.Length);
            for (int i = 0; i < this.Length; i++)
                v[i] = K * this[i];
            return v;

        }
        public static double operator *(Vector v1, Vector v2)
        {
            double S = 0;
            for (int i = 0; i < v1.Length; i++)
            { S += v1[i] * v2[i]; }
            return S;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector vec = new Vector(v1.Length);
            for (int i = 0; i < v1.Length; i++)
            { vec[i] = v1[i] + v2[i]; }
            return vec;
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector vec = new Vector(v1.Length);
            for (int i = 0; i < v1.Length; i++)
            { vec[i] = v1[i] - v2[i]; }
            return vec;
        }
        #endregion
        

        
    }
}
