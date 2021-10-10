using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Linq;

namespace Recursion
{
    class Tasks
    {
        // Calculate sum of array recursively
        public static int Task1_Sum(Stack<int> stack)
        {
            if (stack.Count() == 0) return 0;
            else return stack.Pop() + Task1_Sum(stack);
        }
        public static void RunTask1()
        {
            var arr = Helper.GenerateArray(5);
            Console.WriteLine(arr.ToStringExtended());
            var stack = new Stack<int>(arr);
            var sum = Task1_Sum(stack);
            Console.WriteLine(sum);
        }

        // Calculate max of array recursively
        public static int Task2_Max(Stack<int> stack)
        {
            if (stack.Count() == 1) return stack.Pop();
            else return Math.Max(stack.Pop(), Task2_Max(stack));
        }
        public static void RunTask2()
        {
            var arr = Helper.GenerateArray(5);
            Console.WriteLine(arr.ToStringExtended());
            var stack = new Stack<int>(arr);
            var res = Task2_Max(stack);
            Console.WriteLine(res);
        }

        // Euclid recursively
        public static int Task3_Euclid(int m, int n)
        {
            // m >= n
            if (m % n == 0) return n;
            else return Task3_Euclid(n, m % n);
        }
        public static void RunTask3()
        {
            int N = 10;
            var arr = Helper.GenerateArray(2* N);
            var arr_m = arr.Take(N).ToArray();
            var arr_n = arr.TakeLast(N).ToArray();
            for (int i = 0; i < N; i++)
            {
                var m = Math.Max(arr_m[i], arr_n[i]);
                var n = arr_m[i] + arr_n[i] - m;
                Console.WriteLine("({0}; {1}) = {2}", m, n, Task3_Euclid(m, n));
            }
        }

        // Binary Search recursively
        public static int Task4_Binary(int[] arr, int e, int low, int high)
        {
            if (low == high)
            {
                if (arr[0] == e) return low;
                else return -1;
            }
            var medium = (low + high) / 2;
            if (arr[medium] == e) return medium;
            if (arr[medium] > e) return Task4_Binary(arr, e, low, medium);
            return Task4_Binary(arr, e, medium, high);
        }
        public static void RunTask4()
        {
            var tmp = Helper.GenerateArray(10).ToList();
            tmp.Sort();
            var arr = tmp.ToArray();
            Console.WriteLine(arr.ToStringExtended());
            var e = Helper.GenerateArray(1)[0];
            Console.WriteLine(e);
            var res = Task4_Binary(arr, e, 0, arr.Length - 1);
            Console.WriteLine(res);
        }

        // Quick Sort recursively
        public static List<int> Task5_QuickSort(List<int> arr)
        {
            if (arr.Count < 2) return arr;
            var less = new List<int>();
            var greater = new List<int>();
            var j = (new Random()).Next(arr.Count);
            var e = arr[j];
            for(int i = 0; i < arr.Count; i++)
            {
                if (i == j) continue;
                if(arr[i] > e) greater.Add(arr[i]);
                else less.Add(arr[i]);
            }
            var res = Task5_QuickSort(less);
            res.Add(e);
            return res.Concat(Task5_QuickSort(greater)).ToList();
        }
        public static void RunTask5()
        {
            var arr = Helper.GenerateArray(20).ToList();
            Console.WriteLine(arr.ToStringExtended());
            var res = Task5_QuickSort(arr);
            arr.Sort();
            Console.WriteLine(arr.ToStringExtended());
            Console.WriteLine(res.ToStringExtended());
        }

        // Quick Sort recursively with less memory
        private static void Swap(ref int a, ref int b)
        {
            var tmp = a; a = b; b = tmp;
        }
        private static int Task6_Partition(int[] arr, int low, int high)
        {
            var j = (new Random()).Next(high - low) + low;
            Swap(ref arr[low], ref arr[j]);
            var i = low; j = high + 1;
            while(true)
            {
                while(arr[++i] < arr[low])
                {
                    if(i == high) break;
                }
                while(arr[--j] >= arr[low])
                {
                    if (j <= low) break;
                }
                if (i >= j) break;
                Swap(ref arr[i], ref arr[j]);
            }
            Swap(ref arr[low], ref arr[j]);
            return j;
        }
        
        public static void Task6_Sort(int[] arr, int low, int high)
        {
            if (low >= high) return;
            var j = Task6_Partition(arr, low, high);

            Task6_Sort(arr, low, j - 1);
            Task6_Sort(arr, j + 1, high);
        }
        public static void RunTask6()
        {
            var arr = Helper.GenerateArray(20);
            var list = new List<int>(arr);
            Console.WriteLine(arr.ToStringExtended());
            Task6_Sort(arr, 0, arr.Length - 1);
            Console.WriteLine(arr.ToStringExtended());
            list.Sort();
            Console.WriteLine(list.ToStringExtended());
        }

        // QuickSort with stack
        public static void Task7_Sort(int[] arr)
        {
            var stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(0, arr.Length - 1));
            while(stack.Count != 0)
            {
                var range = stack.Pop();
                var j = Task6_Partition(arr, range.Item1, range.Item2);
                if (j - 1 - range.Item1 > 1)
                    stack.Push(new Tuple<int, int>(range.Item1, j - 1));
                if (range.Item2 - (j + 1) > 1)
                    stack.Push(new Tuple<int, int>(j + 1, range.Item2));
                
            }
        }
        public static void RunTask7()
        {
            var arr = Helper.GenerateArray(20);
            var list = new List<int>(arr);
            Console.WriteLine(arr.ToStringExtended());
            Task7_Sort(arr);
            Console.WriteLine(arr.ToStringExtended());
            list.Sort();
            Console.WriteLine(list.ToStringExtended());
        }
    }
}