﻿using System;
using System.Text;

namespace SystemOfLinearEquations
{
    public class Vector
    {
        int N;
        public double[] vec;
        public Vector()
        {
            N = 0;
            vec = new double[N];
        }
        public Vector(int M)
        {
            N = M;
            vec = new double[N];
        }
        public Vector(Vector v)
        {
            N = v.N;
            vec = new double[N];
            for (int i = 0; i < N; i++)
                vec[i] = v.vec[i];
        }
        public Vector(params double[] a)
        {
            N = a.Length;
            vec = new double[N];
            for (int i = 0; i < N; i++)
                vec[i] = a[i];
        }

        public double this[int key]
        {
            get
            { return vec[key]; }
            set { vec[key] = value; }
        }
        public Vector Multiple(double K)
        {
            Vector v3 = new Vector(this.n);
            for (int i = 0; i < this.N; i++)
                v3[i] = K * this[i];
            return v3;

        }
        public void Copy(Vector v2)
        {
            for (int i = 0; i < this.N; i++)
                v2[i] = this[i];
        }

        public static Vector operator *(double k, Vector v)
        {
            Vector v3 = new Vector(v.n);
            v3 = v.Multiple(k);
            return v3;
        }
        public static double operator *(Vector v1, Vector v2)
        {
            double S = 0;
            for (int i = 0; i < v1.n; i++)
            { S += v1[i] * v2[i]; }
            return S;
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector v3 = new Vector(v1.n);
            for (int i = 0; i < v1.N; i++)
            { v3[i] = v1[i] + v2[i]; }
            return v3;
        }
        public static Vector operator +(Vector v, double h)
        {
            Vector res = new Vector(v.n);
            for (int i = 0; i < v.N; i++)
            { res[i] = v[i] + h; }
            return res;
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector v3 = new Vector(v1.n);
            for (int i = 0; i < v1.N; i++)
            { v3[i] = v1[i] - v2[i]; }
            return v3;
        }
        public double Norm()
        {
            double s = 0;
            for (int i = 0; i < this.N; i++)
            { s += this[i] * this[i]; }
            return Math.Sqrt(s);
        }
      
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.N; i++)
                sb.Append(String.Format("{0:f5}\t", this[i]));
            return sb.ToString();
        }
        public int n
        {
            get
            {
                return N;
            }
            set
            {
                N = value;
            }
        }
    }
}