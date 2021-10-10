﻿using System;

namespace L1
{
    class Program
    {
        static void Main(string[] args)
        {
            Line obj = new Line();
            Console.WriteLine(obj);
            Console.WriteLine("Length: {0}", obj.Length());
            Segment sgm = new Segment();
            Console.WriteLine(sgm);
            Console.WriteLine("Length: {0}", sgm.Length());
            sgm.ReduceSegment(); // Reduce segment 5 times
            Console.WriteLine("Reduced segment: ");
            Console.WriteLine(sgm);
            Console.WriteLine("Length: {0}", sgm.Length());
        }
    }

    public class Line
    {
        public int N {get; protected set;}
        public double[] A { get; set; } // Start point
        public double[] B { get; set; } // End point

        # region Constructors
        public Line(int n = 2)
        {
            N = n;
            A = new double[n];
            B = new double[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                A[i] = rnd.NextDouble();
                B[i] = rnd.NextDouble();
            }
        }
        public Line(double[] a, double[] b)
        {
            A = a; B = b; N = A.Length;
        }

        # endregion

        // Methods
        public override string ToString()
        {
            string res = "A: ";
            for (int i = 0; i < N; i++)
            {
                res += (A[i] + " ");
            }
            res += "\n";
            res += "B: ";
            for (int i = 0; i < N; i++)
            {
                res += (B[i] + " ");
            }
            return res;
        }
        public double Length()
        {
            double res = 0;
            for (int i = 0; i < N; i++)
            {
                res += (B[i] - A[i]) * (B[i] - A[i]);
            }
            return Math.Sqrt(res);
        }

    }

    public class Segment : Line
    {
        # region Constructors
        public Segment(double[] b)
        {
            B = b;
            N = B.Length;
            A = new double[N];
            for(int i = 0; i < N; i++)
            {
                A[i] = 0;
            }
        }
        public Segment(int n = 2)
        {
            N = n;
            A = new double[n];
            B = new double[n];
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
            {
                A[i] = 0;
                B[i] = rnd.NextDouble();
            }
        }
        # endregion
        // Methods
        public double[] GetEndPoint()
        {
            return B;
        }
        public int GetSpaceDimension()
        {
            return N;
        }
    
        public void ReduceSegment(int k = 5)
        {
            for(int i = 0; i < N; i++)
            {
                B[i] /= k;
            }
        }
    }

}