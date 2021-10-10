﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SystemOfLinearEquations
{
    // Class deals with a matrix, solves a system, provides with matrix operations
    public class Matrix
    {
        List<List<double>> Arr;
        public static double EPS = 0.0001; 
        
        #region Constructors
        public Matrix(int Rows, int Columns)
        {
            Arr = new List<List<double>>();
            for (int i = 0; i < Rows; i++)
            {
                var tmp = new List<double>();
                for (int j = 0; j < Columns; j++)
                {
                    tmp.Add(0);
                }
                Arr.Add(tmp);
            }
        }
        public Matrix(Matrix M)
        {
            Arr = new List<List<double>>();
            for (int i = 0; i < M.N; i++)
            {
                var tmp = new List<double>();
                for (int j = 0; j < M.M; j++)
                {
                    tmp.Add(M[i, j]);
                }
                Arr.Add(tmp);
            }
        }
        public Matrix(Double[,] a)
        {
            Arr = new List<List<double>>();
            for (int i = 0; i < a.GetUpperBound(0) + 1; i++)
            {
                var tmp = new List<double>();
                for (int j = 0; j < a.GetUpperBound(1) + 1; j++)
                {
                    tmp.Add(a[i, j]);
                }
                Arr.Add(tmp);
            }
        }
        public Matrix()
        {
            int rows = 3; int columns = 4;
            Arr = new List<List<double>>();
            for (int i = 0; i < rows; i++)
            {
                var tmp = new List<double>();
                for (int j = 0; j < columns; j++)
                {
                    tmp.Add((i + 1) * (j + 1));
                }
                Arr.Add(tmp);
            }
        }
        public Matrix(string path)
        {
            string[] lines = File.ReadAllLines(path);  
            Arr = new List<List<double>>();
            foreach (string line in lines)  
            {
                var strs = line.Split(' ');
                var row = new List<double>();
                foreach (string str in strs)
                {
                    double value;
                    Double.TryParse(str, out value);
                    row.Add(value);
                }
                Arr.Add(row);
            }
            var max = Arr.Select(x => x.Count).Max();
            foreach (var row in Arr)
            {
                for (int i = row.Count - 1; i < max - 1; i++) row.Add(0);
            }
        }
        #endregion

        #region Dimensions & Index
        /// <summary>
        /// Rows
        /// </summary>
        public int N { get { return this.Arr.Count; } }
        public int RowsCount { get { return this.Arr.Count; } }
        /// <summary>
        /// Columns
        /// </summary>
        public int M { get { return this.Arr.Count > 0 ? this.Arr[0].Count : 0; } }
        public int ColumnsCount { get { return this.Arr.Count > 0 ? this.Arr[0].Count : 0; } }
        public double this[int x, int y]
        {
            get { return Arr[x][y]; }
            set { Arr[x][y] = value; }
        }

        #endregion

        #region Row / Column operations
        public List<double> GetColumn(int index)
        {
            var res = new List<double>();
            for (int i = 0; i < this.RowsCount; i++)
                res.Add(this[i, index]);
            return res;
        }
        public void AddRow()
        {
            var row = new List<double>();
            for (int i = 0; i < ColumnsCount; i++)
            {
                row.Add(0);
            }
            Arr.Add(row);
        }
        public void RemoveRowAt(int index)
        {
            Arr.RemoveAt(index);
        }
        public void AddColumn()
        {
            for (int i = 0; i < Arr.Count; i++)
                Arr[i].Add(0);
        }
        public void RemoveColumnAt(int index)
        {
            for (int i = 0; i < Arr.Count; i++)
                Arr[i].RemoveAt(index);
        }

        #endregion

        #region Operators
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
        # endregion

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

        public string GaussToString(Matrix RHS)
        {
            var matrixStr = this.ToString().Split('\n');
            var bStr = RHS.ToString().Split('\n');
            for(int i = 0; i < matrixStr.Length; i++)
            {
                matrixStr[i] += " | " + bStr[i];
            }
            return String.Join('\n', matrixStr);
        }

        public int[] GaussSolveModified(Matrix RHS, out Matrix Ans, ref string steps, bool printSteps = false)
        {
            var res = new int[RHS.ColumnsCount];
            Ans = new Matrix(this.M, RHS.ColumnsCount);
            if (RHS.N == 0 || RHS.M == 0) return res;
            if (RHS.N != N) return res;
            Matrix arr = new Matrix(this); Matrix b = new Matrix(RHS);
            int m = this.M; int n = this.N;
            var where = new int[this.M];
            for (int i = 0; i < where.Length; i++) where[i] = -1;
            
            if (printSteps) steps += String.Format("Initial system: \n{0}\n\n", arr.GaussToString(b));
            for (int col = 0, row = 0; col < m && row < n; ++col)
            {
                int jmax = row;
                for (int i = row; i < n; ++i)
                    if (Math.Abs(arr[i, col]) > Math.Abs(arr[jmax, col]))
                        jmax = i; // index of max value in a column

                if (Math.Abs(arr[jmax, col]) < EPS)
                    continue;
                // Replace rows
                arr.ReplaceRows(jmax, row);
                b.ReplaceRows(jmax, row);
                if (printSteps) steps += String.Format("Replace rows ({0}; {1}): \n{2}\n\n", jmax + 1, row + 1, arr.GaussToString(b));
                where[col] = row;
                for (int i = 0; i < n; ++i)
                    if (i != row)
                    {
                        double k = arr[i, col] / arr[row, col];
                        for (int j = col; j < m; ++j)
                            arr[i, j] -= arr[row, j] * k;
                        b.MinusRows(i, row, k);
                        if (printSteps) steps += String.Format("Subtract rows ({0};{1}) with k = {2}: \n{3}\n\n", i + 1, row + 1, k, arr.GaussToString(b));
                    }
                ++row;
            }

            for (int i = 0; i < m; ++i)
                if (where[i] != -1)
                {
                    for (int j = 0; j < Ans.ColumnsCount; j++)
                    {
                        Ans[i, j] = b[where[i], j] / arr[where[i], i];
                        b[where[i], j] = Ans[i, j]; // it is needed just for output
                    }
                    for (int j = 0; j < arr.ColumnsCount; j++) // it is needed just for output
                        arr[where[i], j] /= arr[where[i], i];

                }
            if (printSteps) steps += String.Format("Normalize matrix: \n{0}\n\n", arr.GaussToString(b));

            // Check the answer
            var error = this * Ans - RHS;
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = 1;
                for (int j = 0; j < error.RowsCount; j++)
                    if (Math.Abs(error[j, i]) > EPS)
                    {
                        res[i] = 0; break;
                    }
            }
            if (printSteps)
                for(int i = 0; i < res.Length; i++)
                    if(res[i] == 1)
                        steps += String.Format("Answer {0}: {1}\n", (i + 1), String.Join(" ", Ans.GetColumn(i)));
                    else
                        steps += String.Format("Answer {0}: no solution\n", (i + 1));

            return res;
        }

        // Метод Зейделя
        public int[] ZaidelSolve(Matrix RHS, out Matrix Ans, bool IsZaidel, ref string steps, ref int itc, bool printSteps = false, double EPS = 0)
        {
            if (EPS == 0) EPS = Matrix.EPS; steps = "";
            var res = new int[RHS.ColumnsCount];
            int n=this.N;
            Ans = new Matrix(n, 1);
            if (n != this.M) return null;
            if (RHS.M != 1 || RHS.N!=n) return null;
            Matrix B = new Matrix(n, n);
            for (int i = 0; i < n; i++)
            {
                B.Arr[i][i] = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j) B.Arr[i][j] = -this.Arr[i][j] / this.Arr[i][i];
                }
            }
            itc = 0;
            while (true)
            {
                itc++;
                if (!IsZaidel) 
                { 
                    Ans = B * Ans; 
                    for (int i = 0; i < n; i++) 
                        Ans.Arr[i][0] = RHS.Arr[i][0]/this.Arr[i][i] + Ans.Arr[i][0]; 
                }
                else
                {
                    for (int i = 0; i < n; i++)
                    {
                        Ans.Arr[i][0] = RHS.Arr[i][0]/this.Arr[i][i];
                        for (int j = 0; j < n; j++) 
                            Ans.Arr[i][0] += B.Arr[i][j] * Ans.Arr[j][0];
                    }
                }
                
                var error = this * Ans - RHS;
                //for (int i = 0; i < res.Length; i++)
                {
                    int i = 0;
                    double norm = 0;
                    for (int j = 0; j < error.RowsCount; j++)
                    {
                        norm = Math.Max(norm, Math.Abs(error[j, i]));
                    }
                    //norm = Math.Sqrt(norm) / error.RowsCount;
                    if (norm < EPS)
                    {
                        res[i] = 1; break;
                    }
                    else if(norm > 1/ EPS/EPS/EPS/EPS || itc > 3000)
                    {
                        res[i] = 0; break;
                    }
                        
                }
                
            }
            // Check the answer
            if (printSteps)
            {
                for(int i = 0; i < res.Length; i++)
                    if(res[i] == 1)
                        steps += String.Format("Answer {0}: {1}\n", (i + 1), String.Join(" ", Ans.GetColumn(i)));
                    else
                        steps += String.Format("Answer {0}: no solution\n", (i + 1));
                steps += String.Format("Iteration number: {0}", itc);
            }
                    
            return res;
        }
    }
}