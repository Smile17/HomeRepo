using System;
using System.Collections.Generic;
using System.Text;

namespace L3
{
    class DemoMatrix
    {
        public static void DemoMatrixConstructors()
        {
            {
                Console.WriteLine("Default constructor:");
                Matrix A = new Matrix();
                Console.WriteLine(A);
            }
            {
                Console.WriteLine("Parameter constructor: Matrix from a file:");
                Matrix A = new Matrix("input.txt");
                Console.WriteLine(A);
                Console.WriteLine("Transposed matrix:");
                A = A.GetTransposed();
                Console.WriteLine(A);
            }
            {
                Console.WriteLine("Copy constructor:");
                Matrix A = new Matrix("input.txt");
                Matrix A2 = new Matrix(A);
                Console.WriteLine(A2);
            }
        }
        public static void DemoMatrixOperator()
        {
            Console.WriteLine("Initial matrix:");
            Console.WriteLine("A1");
            Matrix A1 = new Matrix("input1.txt");
            Console.WriteLine(A1);
            Console.WriteLine("A2");
            Matrix A2 = new Matrix("input2.txt");
            Console.WriteLine(A2);
            Matrix A3;

            // Operator +, +=
            A3 = A1 + A2;
            Console.WriteLine("A1 + A2");
            Console.WriteLine(A3);
            A3 = A1.Copy();
            A3 += A2; // implicity overloaded
            Console.WriteLine("A3 = A1; A3 += A2");
            Console.WriteLine(A3);

            // Operator -, -=
            A3 = A1 - A2;
            Console.WriteLine("A1 - A2 ");
            Console.WriteLine(A3);
            A3 = A1.Copy();
            A3 -= A2; // implicity overloaded
            Console.WriteLine("A3 = A1; A3 -= A2");
            Console.WriteLine(A3);

            // Operator *, *=
            A3 = A1 * A2;
            Console.WriteLine("A1 * A2 ");
            Console.WriteLine(A3);
            A3 = A1.Copy();
            A3 *= A2; // implicity overloaded
            Console.WriteLine("A3 = A1; A3 *= A2");
            Console.WriteLine(A3);

            // Operator /, /=
            A3 = A1 / A2;
            Console.WriteLine("A1 / A2 ");
            Console.WriteLine(A3);
            A3 = A1.Copy();
            A3 /= A2; // implicity overloaded
            Console.WriteLine("A3 = A1; A3 /= A2");
            Console.WriteLine(A3);

            // Operator ++
            A3 = A1.Copy(); A3 = A3++;
            Console.WriteLine("A1++");
            Console.WriteLine(A3);
            // Operator ++
            A3 = A1.Copy(); A3 = ++A3;
            Console.WriteLine("++A1");
            Console.WriteLine(A3);

            // Operator --
            A3 = A1.Copy(); A3 = A3--;
            Console.WriteLine("A1--");
            Console.WriteLine(A3);
            // Operator --
            A3 = A1.Copy(); A3 = --A3;
            Console.WriteLine("--A1");
            Console.WriteLine(A3);

            // Operator >>, <<
            A3 = A1 >> 2;
            Console.WriteLine("A1 >> 2");
            Console.WriteLine(A3);
            A3 = A1 << 2;
            Console.WriteLine("A1 << 2");
            Console.WriteLine(A3);

            // Operator ==
            A3 = A1.Copy();
            Console.WriteLine("A3 == A1?");
            Console.WriteLine(A3 == A1);
            Console.WriteLine("A3 == A2?");
            Console.WriteLine(A3 == A2);
            // Operator !=
            A3 = A1.Copy();
            Console.WriteLine("A3 != A1?");
            Console.WriteLine(A3 == A1);
            Console.WriteLine("A3 != A2?");
            Console.WriteLine(A3 == A2);
        }
    }
}
