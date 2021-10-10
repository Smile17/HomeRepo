using MathHelper.Algebra;
using System;

namespace TMD
{
    class Composition
    {
        public static Matrix MaxMin(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.N, B.M);
            for (int i = 0; i < C.N; i++)
                for (int j = 0; j < C.M; j++)
                {
                    C[i, j] = Math.Min(A[i, 0], B[0, j]);
                    for (int k = 0; k < Math.Min(A.M, B.N); k++)
                    {
                        if (C[i, j] < Math.Min(A[i, k], B[k, j]))
                            C[i, j] = Math.Min(A[i, k], B[k, j]);
                    }
                }
            return C;
        }
        public static Matrix MinMax(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.N, B.M);
            for (int i = 0; i < C.N; i++)
                for (int j = 0; j < C.M; j++)
                {
                    C[i, j] = Math.Max(A[i, 0], B[0, j]);
                    for (int k = 0; k < Math.Min(A.M, B.N); k++)
                    {
                        if (C[i, j] > Math.Max(A[i, k], B[k, j]))
                            C[i, j] = Math.Max(A[i, k], B[k, j]);
                    }
                }
            return C;
        }
        public static Matrix Multiple(Matrix A, Matrix B)
        {
            Matrix C = new Matrix(A.N, B.M);
            for (int i = 0; i < C.N; i++)
                for (int j = 0; j < C.M; j++)
                {
                    C[i, j] = A[i, 0] * B[0, j];
                    for (int k = 0; k < Math.Min(A.M, B.N); k++)
                    {
                        if (C[i, j] < A[i, k] * B[k, j])
                            C[i, j] = A[i, k] * B[k, j];
                    }
                }
            return C;
        }
    }
}
