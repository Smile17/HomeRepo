using System;
using System.Numerics;
using System.Collections.Generic;
using System.IO;

namespace Shein
{
    public class Algorithms
    {
        #region Algorithm complexity
        public static BigInteger DoubleLinearComplexity(BigInteger n) // O(n^2)
        {
            var res = 0;
            for(int i = 0; i < n; i++) // (3n + 1 + 2)n + 1 = 3n^2 + 3n + 1
                for(int j = 0; j < n; j++) // 3n + 1
                    res++;
            return res; // O(3n^2 + 3n + 3) = O(3n^2) = O(n^2)
        }
        public static BigInteger LinearComplexity(BigInteger n) // O(n)
        {
            var res = 0;
            for(int i = 0; i < n; i++)
                res++;
            return res;
        }

        public static BigInteger ConstantComplexity(BigInteger n) // O(1)
        {
            var res = 0; res++;
            return res;
        }
        # region Algorithm complexity testing
        public delegate BigInteger LoopComplexityMethod(BigInteger n);
        private static void TestComplexityMethod(BigInteger N, BigInteger step, LoopComplexityMethod method, string name)
        {
            Console.WriteLine(name);
            for(BigInteger i = 0; i < N; i += step)
            {
                var outp = new StreamWriter(Console.OpenStandardOutput());
                var act = new Action(()=> method(i));
                Estimator.Estimate(act, outp, i.ToString());
                outp.Close();
            }
        }
        public static void TestComplexity()
        {
            BigInteger N = new BigInteger(1e+6); 
            BigInteger step = BigInteger.Divide(N, 10);
            TestComplexityMethod(N, step, Algorithms.ConstantComplexity, "Constant complexity:");
            TestComplexityMethod(N, step, Algorithms.LinearComplexity, "Linear complexity:");
            TestComplexityMethod(N, step, Algorithms.ConstantComplexity, "Polynomial complexity:");
        }
        #endregion
        #endregion
        
        #region Binary search
        public static int Search(int[] arr, int x) // O(n)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                if(arr[i] == x) return i;
            }
            return -1;
        }
        public static int BinarySearch(int[] arr, int x) // O(log n), arr should be sorted
        {
            return BinarySearch(arr, x, 0, arr.Length - 1);
        }
        public static int BinarySearch(int[] arr, int x, int l, int r) // O(log n), arr should be sorted
        {
            while(r > l)
            {
                int m = (r + l) / 2;
                if(arr[m] == x) return m;
                if(arr[m] > x) r = m - 1;
                else
                    l = m + 1;
            }
            if(arr[r] == x) return r;
            return -1;
        }
        public static int BinarySearchRecursive(int[] arr, int x, int l, int r) // O(log n), arr should be sorted
        {
            if(r < l) return -1;
            int m = (r + l) / 2;
            if(arr[m] == x) return m;
            if(arr[m] > x) return BinarySearchRecursive(arr, x, l, m - 1);
            return BinarySearchRecursive(arr, x, m + 1, r);
        }
        public static int BinarySearchRecursive(int[] arr, int x) // O(log n), arr should be sorted
        {
            return BinarySearchRecursive(arr, x, 0, arr.Length - 1);
        }
        
        # region Binary search testing
        public delegate int BinarySearchMethod(int[] arr, int x);
        private static void TestBinarySearchMethod(int[] arr, int x, BinarySearchMethod method, string name)
        {
            Console.WriteLine(name);
            var idx = method(arr, x);
            Console.WriteLine("index: {0}; value: {1}", idx, idx != -1 ? arr[idx].ToString() : "");
        }
        public static void TestSearchAlgorithms()
        {
            var arr = Generator.GenerateArray(100); Array.Sort(arr);
            int x = Generator.GenerateNumber();
            Console.WriteLine(Output.ArrayToString(arr));
            Console.WriteLine($"x: {x}");
            TestBinarySearchMethod(arr, x, Search, "\nA plain search:");
            TestBinarySearchMethod(arr, x, BinarySearch, "\nA binary search:");
            TestBinarySearchMethod(arr, x, BinarySearchRecursive, "\nA binary recursive search:");
            TestBinarySearchMethod(arr, x, SearchFrequency, "\nA plain search frequency:");
            TestBinarySearchMethod(arr, x, BinarySearchFrequency, "\nA binary search frequency:");
            TestBinarySearchMethod(arr, x, BinarySearchFrequencyWithPositions, "\nA binary search frequency with positions:");
        }
        public static void TestSearchAlgorithmsComplexity()
        {
            long N = (long)(1e+10); 
            long step = N / 10;
            Console.WriteLine("n\tA plain search\tA binary search");
            for(var i = step; i < N; i += step)
            {
                var arr = Generator.GenerateArray(i);
                int x = Generator.GenerateNumber();
                Console.Write(i + "\t");
                {
                    var act = new Action(()=> Algorithms.Search(arr, x));
                    var time = Estimator.Estimate(act);
                    Console.Write(time + "\t");
                }
                {
                    Array.Sort(arr);
                    var act = new Action(()=> Algorithms.BinarySearch(arr, x));
                    var time = Estimator.Estimate(act);
                    Console.Write(time + "\t");
                }
                Console.WriteLine();
            }
        }
        #endregion
        
        #region Binary search problems
      
        #region Problem Statement: Given a sorted array with possible duplicate elements. Find number of occurrences of input ‘key’ in log N time.
        public static int SearchFrequency(int[] arr, int x) // arr should be sorted
        {
            int res = 0;
            for(int i = 0; i < arr.Length; i++)
                if(arr[i] == x) res++;
            return res;
        }

        // Invariant: arr[l] <= x and arr[r] > x 
        public static int GetRightPosition(int[] arr, int x, int l, int r) // arr should be sorted
        {
            while(r - l > 1)
            {
                int m = (r + l) / 2;
                if(arr[m] > x) r = m;
                else l = m;
            }
            return l;
        }
        // Invariant: arr[l] < x and arr[r] >= x 
        public static int GetLeftPosition(int[] arr, int x, int l, int r) // arr should be sorted
        {
            while(r - l > 1)
            {
                int m = (r + l) / 2;
                if(arr[m] < x) l = m;
                else r = m;
            }
            return r;
        }
        public static int BinarySearchFrequencyWithPositions(int[] arr, int x) // arr should be sorted
        {
            var ridx = GetRightPosition(arr, x, 0, arr.Length - 1);
            var lidx = GetLeftPosition(arr, x, 0, ridx);
            // What if the element doesn't exists in the array? 
            // The checks helps to trace that element exists 
            return (arr[lidx] == x && x == arr[ridx])? 
                (ridx - lidx + 1) : 0; 
        }
        public static int BinarySearchFrequency(int[] arr, int x) // arr should be sorted
        {
            var idx = BinarySearch(arr, x);
            if(idx == -1) return -1;
            // Looking for left last index
            int lidx, ridx; lidx = ridx = 0;
            var l = 0; var r = idx;
            if(arr[0] == x) lidx = 0;
            else
                while(r > l)
                {
                    var m = (r + l) / 2;
                    if(arr[m] != x && arr[m + 1] == x)
                    {
                        lidx = m + 1; break;
                    }
                    if(arr[m] == x) r = m;
                    else
                        l = m;
                }
            l = idx; r = arr.Length - 1;
            if(arr[r] == x) ridx = r;
            else
                while(r > l)
                {
                    var m = (r + l) / 2;
                    if(arr[m] != x && arr[m - 1] == x)
                    {
                        ridx = m - 1; break;
                    }
                    if(arr[m] == x) l = m;
                    else
                        r = m;
                }
            return ridx - lidx + 1;
        }
        #endregion

        # region Peak Finder
        // https://courses.csail.mit.edu/6.006/spring11/lectures/lec02.pdf
        // Given an array of integers. Find a peak element in it. An array element is a peak if it is NOT smaller than its neighbours. 
        // For corner elements, we need to consider only one neighbour.
        public static int PeakFinder(int[] arr) // find local minimum - its neighbours are bigger
        {
            int r = arr.Length - 1; int l = 0;
            while(r > l)
            {
                int m = (r + l) / 2;
                // m > 0, we have to check left neighbour, arr[m] < arr[m - 1] means that arr[m] is not a peak, lets find it in [l, m - 1]
                if(m > 0) if(arr[m] < arr[m - 1])
                {
                    r = m - 1; continue;
                }
                if(m < arr.Length - 1) if(arr[m] < arr[m + 1])
                {
                    l = m + 1; continue;
                }
                return m;                
            }
            return r;
        }
        public static Tuple<int, int> PeakFinder2D(int[,] arr) // O(nlog m), n  - rows count, m - columns count
        {
            int cols = arr.GetUpperBound(1) + 1;
            int rows = arr.GetUpperBound(0) + 1;
            int r = cols - 1; int l = 0;
            int max_i = 0;
            while(r > l)
            {
                int m = (r+l) / 2;
                max_i = 0;
                // find max in column
                for(int i = 1; i < rows; i++)
                {
                    if(arr[max_i, m] < arr[i, m]) max_i = i;
                }
                if(m > 0) if(arr[max_i, m] < arr[max_i, m - 1])
                {
                    r = m - 1; continue;
                } 
                if(m < cols) if(arr[max_i, m] < arr[max_i, m + 1])
                {
                    l = m + 1; continue;
                }
                return new Tuple<int, int>(max_i, m);
            }
            return new Tuple<int, int>(max_i, r);
        }

        private static int FindMaxInColumn(int[,] arr, int col, int l_row, int r_row) // return row index
        {
            var idx = l_row;
            for(int i = l_row + 1; i < r_row + 1; i++)
            {
                if(arr[idx, col] < arr[i, col]) idx = i;
            }
            return idx;
        }
        private static int FindMaxInRow(int[,] arr, int row, int l_col, int r_col)
        {
            var idx = l_col;
            for(int i = l_col + 1; i < r_col + 1; i++)
            {
                if(arr[row, idx] < arr[row, i]) idx = i;
            }
            return idx;
        }
        private static Tuple<int, int> SelectMax(int[,] arr, int row1, int col1, int row2, int col2)
        {
            if(arr[row1, col1] < arr[row2, col2]) return new Tuple<int, int>(row2, col2);
            return new Tuple<int, int>(row1, col1);
        }
        public static Tuple<int, int> PeakFinder2DEffective(int[,] arr)
        {
            int cols = arr.GetUpperBound(1) + 1;
            int rows = arr.GetUpperBound(0) + 1;
            int r_cols = cols - 1; int l_cols = 0;
            int r_rows = rows - 1; int l_rows = 0;
            var max_idx = new Tuple<int, int>(0, 0);
            while((r_cols > l_cols) && (r_rows > l_rows))
            {
                int m_cols = (r_cols + l_cols) / 2;
                int m_rows = (r_rows + l_rows) / 2;
                // Horizontal lines
                max_idx = SelectMax(arr, l_rows, FindMaxInRow(arr, l_rows, l_cols, r_cols), m_rows, FindMaxInRow(arr, m_rows, l_cols, r_cols));
                max_idx = SelectMax(arr, max_idx.Item1, max_idx.Item2, r_rows, FindMaxInRow(arr, r_rows, l_cols, r_cols));
                // Vertical lines
                max_idx = SelectMax(arr, max_idx.Item1, max_idx.Item2, FindMaxInColumn(arr, l_cols, l_rows, r_rows), l_cols);
                max_idx = SelectMax(arr, max_idx.Item1, max_idx.Item2, FindMaxInColumn(arr, m_cols, l_rows, r_rows), m_cols);
                max_idx = SelectMax(arr, max_idx.Item1, max_idx.Item2, FindMaxInColumn(arr, r_cols, l_rows, r_rows), r_cols);
                
                if(max_idx.Item2 > 0) if(arr[max_idx.Item1, max_idx.Item2] < arr[max_idx.Item1, max_idx.Item2 - 1]) // left neighbour
                {
                    // move left
                    r_cols = m_cols;
                    if(max_idx.Item1 > m_rows) r_rows = m_rows;
                    else l_rows = m_rows;
                    continue;
                }
                if(max_idx.Item2 < cols - 1) if(arr[max_idx.Item1, max_idx.Item2] < arr[max_idx.Item1, max_idx.Item2 + 1]) // right neighbour
                {
                    // move right
                    l_cols = m_cols;
                    if(max_idx.Item1 > m_rows) r_rows = m_rows;
                    else l_rows = m_rows;
                    continue;

                }
                if(max_idx.Item1 > 0) if(arr[max_idx.Item1, max_idx.Item2] < arr[max_idx.Item1 - 1, max_idx.Item2]) // upper neighbour
                {
                    // move up
                    r_rows = m_rows;
                    if(max_idx.Item2 > m_cols) l_cols = m_cols;
                    else r_cols = m_cols;
                    continue;
                }
                if(max_idx.Item2 < rows - 1) if(arr[max_idx.Item1, max_idx.Item2] < arr[max_idx.Item1 + 1, max_idx.Item2]) // bottom neighbour
                {
                    // move down
                    l_rows = m_rows;
                    if(max_idx.Item2 > m_cols) l_cols = m_cols;
                    else r_cols = m_cols;
                    continue;
                }

                return max_idx;
            }
            return max_idx;
        }
        public static void TestPeakFinderAlgorithms()
        {
            {
                Console.WriteLine("\nPeak finding:");
                var arr = Generator.GenerateArray(22);
                Console.WriteLine("array: {0}", String.Join(' ', arr));
                var idx = PeakFinder(arr);
                Console.WriteLine("index: {0}; value: {1}", idx, idx != -1 ? arr[idx].ToString() : "");

            }
            {
                Console.WriteLine("\nPeak finding:");
                var arr = Generator.GenerateArray(5, 5);
                Console.WriteLine(Output.ArrayToString(arr));
                var idx = PeakFinder2D(arr);
                Console.WriteLine("index: {0},{1}; value: {2}", idx.Item1, idx.Item2, arr[idx.Item1, idx.Item2]);

            }
            {
                Console.WriteLine("\nPeak finding 2D effective:");
                var arr = Generator.GenerateArray(10, 20);
                Console.WriteLine(Output.ArrayToString(arr));
                var idx = PeakFinder2DEffective(arr);
                Console.WriteLine("index: {0},{1}; value: {2}", idx.Item1, idx.Item2, arr[idx.Item1, idx.Item2]);

            }
        }
        #endregion
        // Given an array of n distinct integers sorted in ascending order, write a function that returns a Fixed Point in the array, 
        // if there is any Fixed Point present in array, else returns -1. 
        // Fixed Point in an array is an index i such that arr[i] is equal to i. Note that integers in array can be negative.
        public static int FindFixedPoint(int[] arr) // arr should be sorted with distinct elements
        {
            int r = arr.Length - 1; int l = 0;
            if(arr[r] < r || arr[l] > l) return -1;
            // Invariant arr[r] >= r && arr[l] <= l 
            while(r - l > 1)
            {
                int m = (r + l) / 2;
                if(arr[m] == m) return m;
                if(arr[m] > m) r = m;
                else l = m;
            }
            if(arr[r] == r) return r;
            if(arr[l] == l) return l;
            return -1;
        }
        public static void TestFixedPointAlgorithms()
        {
            Generator.MIN = -10; Generator.MAX = 15;
            var arr = Generator.GenerateArrayWithDistinctElements(10);
            Array.Sort(arr);
            Console.WriteLine("sorted array: {0}", String.Join(' ', arr));
            var idx = FindFixedPoint(arr);
            Console.WriteLine("index: {0}; value: {1}", idx, idx != -1 ? arr[idx].ToString() : "");
        }

        // Given a set S of n integers and another integer x, determines whether or not there exist two elements in S whose sum is exactly x
        public static Tuple<int, int> BinarySearchSum(int[] arr, int x) // arr should be sorted
        {
            for(int i = 0; i < arr.Length; i++)
            {
                var idx = BinarySearch(arr, x - arr[i], i + 1, arr.Length - 1);
                if(idx != -1) return new Tuple<int, int>(i, idx);
            }
            return new Tuple<int, int>(-1, -1);
        }
        public static void TestBinarySearchSum()
        {
            var arr = Generator.GenerateArray(100); Array.Sort(arr);
            int x = Generator.GenerateNumber();
            Console.WriteLine(Output.ArrayToString(arr));
            Console.WriteLine($"x: {x}");
            var idx = BinarySearchSum(arr, x);
            if(idx.Item1 == -1)
                Console.WriteLine("indices: -1; -1");
            else
                Console.WriteLine("indices: {0}; {1}, values: {2}; {3}, sum: {4}, x: {5}", idx.Item1, idx.Item2, arr[idx.Item1], arr[idx.Item2], 
                arr[idx.Item1] + arr[idx.Item2], x);
        }
        #endregion


        #endregion
        
        #region Sorting
        public static void InsertionSort(int[] arr)
        {
            for(int j = 1; j < arr.Length; j++)
            {
                // Invariant: arr[0..j-1] is sorted
                var key = arr[j];
                var idx = j - 1;
                while(idx >= 0 && arr[idx] > key)
                {
                    arr[idx + 1] = arr[idx];
                    idx--;
                }
                arr[idx + 1] = key;
            }
        }
        public static void InsertionSort(int[] arr, int l, int r) // make arr[l..r] sorted
        {
            for(int j = l + 1; j <= r; j++)
            {
                // Invariant: arr[l..j-1] is sorted
                var key = arr[j];
                var idx = j - 1;
                while(idx >= 0 && arr[idx] > key)
                {
                    arr[idx + 1] = arr[idx];
                    idx--;
                }
                arr[idx + 1] = key;
            }
        }
        public static void InsertionSortRecursively(int[] arr)
        {
            InsertionSortRecursively(arr, arr.Length - 1);
        }
        public static void InsertionSortRecursively(int[] arr, int j) // make arr[0..j] sorted
        {
            if (j < 1) return;
            InsertionSortRecursively(arr, j - 1);
            InsertionSortCombineRecursively(arr, j);
        }
        public static void InsertionSortCombineRecursively(int[] arr, int j) // arr[0..j-1] is sorted
        {
            var key = arr[j];
            for(; j > 0 && arr[j - 1] > key; j--)
                arr[j] = arr[j - 1];
            arr[j] = key;
        }
        public static void InsertionSortDescending(int[] arr)
        {
            for(int j = 1; j < arr.Length; j++)
            {
                // Invariant: arr[0..j-1] is sorted
                var key = arr[j];
                var idx = j - 1;
                while(idx >= 0 && arr[idx] < key)
                {
                    arr[idx + 1] = arr[idx];
                    idx--;
                }
                arr[idx + 1] = key;
            }
        }
        public static void InsertionSortWithComparer<T>(T[] arr, Comparison<T> comp = null) where T : IComparable<T>
        {
            if(comp == null) comp = ((i1, i2) => i1.CompareTo(i2));
            for(int j = 1; j < arr.Length; j++)
            {
                // Invariant: arr[0..j-1] is sorted
                var key = arr[j];
                var idx = j - 1;
                while(idx >= 0 && (comp(arr[idx], key) == 1))
                {
                    arr[idx + 1] = arr[idx];
                    idx--;
                }
                arr[idx + 1] = key;
            }
        }
        
        public static void SelectionSort(int[] arr)
        {
            for(int i = 0; i < arr.Length - 1; i++)
            {
                // Find min in arr[i..arr.Length - 1]
                var idx = i;
                for(int j = i + 1; j < arr.Length; j++)
                {
                    if(arr[idx] > arr[j]) idx = j;
                }
                // Swap max value with i th element
                var tmp = arr[idx];
                arr[idx] = arr[i];
                arr[i] = tmp;
            }
        }
        # region Merge Sort With Sentinels
        public static void MergeSortWithSentinels(int[] arr)
        {
            MergeSortWithSentinels(arr, 0, arr.Length - 1);
        }
        private static void MergeSortWithSentinels(int[] arr, int p, int r)
        {
            if(p < r)
            {
                int q = (p + r) / 2;
                MergeSortWithSentinels(arr, p, q);
                MergeSortWithSentinels(arr, q + 1, r);
                MergeSortCombineWithSentinels(arr, p, q, r);
            }
        }
        private static void MergeSortCombineWithSentinels(int[] arr, int p, int q, int r) // arr[p..q], arr[q + 1..r] are sorted, output: arr[p..r] is sorted
        {
            var L = new int[q-p+2];
            var R = new int[r-q+1];
            // Copy arr subarrays
            for(int k = 0; k < L.Length - 1; k++) 
                L[k] = arr[p + k];
            for(int k = 0; k < R.Length - 1; k++)
                R[k] = arr[q + 1+ k];
            L[L.Length - 1] = Int32.MaxValue;
            R[R.Length - 1] = Int32.MaxValue;

            int i, j; i = j = 0;
            // Combine them into arr
            for(int k = p; k <= r; k++)
            {
                if(L[i] < R[j])
                {
                    arr[k] = L[i]; i++;
                }
                else
                {
                    arr[k] = R[j]; j++;
                }
            }
        }
        
        #endregion
        # region Merge Sort
        public static void MergeSort(int[] arr)
        {
            MergeSort(arr, 0, arr.Length - 1);
        }
        private static void MergeSort(int[] arr, int p, int r)
        {
            if(p < r)
            {
                int q = (p + r) / 2;
                MergeSort(arr, p, q);
                MergeSort(arr, q + 1, r);
                MergeSortCombine(arr, p, q, r);
            }
        }
        private static void MergeSortCombine(int[] arr, int p, int q, int r) // arr[p..q], arr[q + 1..r] are sorted, output: arr[p..r] is sorted
        {
            var L = new int[q-p+1];
            var R = new int[r-q];
            // Copy arr subarrays
            for(int s = 0; s < L.Length; s++) 
                L[s] = arr[p + s];
            for(int s = 0; s < R.Length; s++)
                R[s] = arr[q + 1+ s];

            int i, j, k; i = j = 0;
            // Combine them into arr
            for(k = p; (k <= r) && (i < L.Length) && (j < R.Length); k++)
            {
                if(L[i] < R[j])
                {
                    arr[k] = L[i]; i++;
                }
                else
                {
                    arr[k] = R[j]; j++;
                }
            }
            for(;i< L.Length;i++)
            {
                arr[k] = L[i]; k++;
            }
            for(;j< R.Length;j++)
            {
                arr[k] = R[j]; k++;
            }
        }
        #endregion
        #region Merge sort with insertion sort
        public static void MergeSortWithInsertionSort(int[] arr)
        {
            MergeSortWithInsertionSort(arr, 0, arr.Length - 1, (int)Math.Log(arr.Length));
        }
        private static void MergeSortWithInsertionSort(int[] arr, int p, int r, int k)
        {
            if(p - r < k) InsertionSort(arr, p, r);
            else
            if(p < r)
            {
                int q = (p + r) / 2;
                MergeSort(arr, p, q);
                MergeSort(arr, q + 1, r);
                MergeSortCombine(arr, p, q, r);
            }
        }
        #endregion
        
        public static void BubbleSort(int[] arr)
        {
            for(int i = 0; i < arr.Length - 1;i++)
                for(int j = arr.Length - 1; j > i; j--)
                    if(arr[j] < arr[j - 1])
                    {
                        var tmp = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = tmp;
                    }
        }
        # region Sorting testing
        public delegate void SortingMethod(int[] arr);
        private static void TestSortingMethod(SortingMethod method, string name)
        {
            Console.WriteLine(name);
            var arr = Generator.GenerateArray(10);
            Console.WriteLine("initial array: {0}", Output.ArrayToString(arr));
            Array.Sort(arr);
            Console.WriteLine("sorted array: \n{0}", Output.ArrayToString(arr));
            // Shuffle the array
            Random rnd=new Random();
            rnd.Shuffle(arr);
            //Console.WriteLine("array: {0}", Output.ArrayToString(arr));
            method(arr);
            Console.WriteLine("sorted array by method: \n{0}", Output.ArrayToString(arr));
              
            Console.WriteLine();
        }
        
        public static void TestSortingAlgorithms()
        {
            TestSortingMethod(InsertionSort, "Insertion sort:");
            TestSortingMethod(InsertionSortRecursively, "Insertion sort recursively:");
            {
                Console.WriteLine("\nInsertion sort descending:");
                var arr = Generator.GenerateArray(100);
                InsertionSortDescending(arr);
                Console.WriteLine("array: {0}", Output.ArrayToString(arr));
                InsertionSortWithComparer(arr);
                Console.WriteLine("sorted array by method: \n{0}", Output.ArrayToString(arr));
                Array.Sort(arr, new Comparison<int>( 
                  (i1, i2) => i2.CompareTo(i1)));
                Console.WriteLine("sorted array: \n{0}", Output.ArrayToString(arr));
                Console.WriteLine();
            }
            TestSortingMethod(SelectionSort, "Selection sort");
            TestSortingMethod(MergeSort, "Merge sort:");
            TestSortingMethod(MergeSortWithInsertionSort, "Merge sort with insertion sort:");
            TestSortingMethod(BubbleSort, "Bubble sort:");
        }
        
        #endregion
        #region Algorithms based on sorting algorithms
        public static int NumberInversionBasedOnSelectionSort(int[] arr, out List<Tuple<int, int>> inversions) // Count numbers of inversions in array
        {
            var num = 0; inversions = new List<Tuple<int, int>>();
            for(int j = 1; j < arr.Length; j++)
            {
                // Invariant: arr[0..j-1] is sorted
                var key = arr[j];
                var idx = j - 1;
                while(idx >= 0 && arr[idx] > key)
                {
                    arr[idx + 1] = arr[idx];
                    inversions.Add(new Tuple<int, int>(arr[idx], key));
                    num++;
                    idx--;
                }
                arr[idx + 1] = key;
            }
            return num;
        }
        # region Number of Inversions Based On Merge Sort
        public static int NumInversionBasedOnMergeSort;
        public static int NumberInversionBasedOnMergeSort(int[] arr)
        {
            return NumberInversionBasedOnMergeSort(arr, 0, arr.Length - 1);
        }
        private static int NumberInversionBasedOnMergeSort(int[] arr, int p, int r)
        {
            int NumInversionBasedOnMergeSort = 0;
            if(p < r)
            {
                int q = (p + r) / 2;
                NumInversionBasedOnMergeSort += NumberInversionBasedOnMergeSort(arr, p, q);
                NumInversionBasedOnMergeSort += NumberInversionBasedOnMergeSort(arr, q + 1, r);
                NumInversionBasedOnMergeSort += NumberInversionBasedOnMergeSortCombine(arr, p, q, r);
            }
            return NumInversionBasedOnMergeSort;
        }
        private static int NumberInversionBasedOnMergeSortCombine(int[] arr, int p, int q, int r) // arr[p..q], arr[q + 1..r] are sorted, output: arr[p..r] is sorted
        {
            int NumInversionBasedOnMergeSort = 0;
            var L = new int[q-p+1];
            var R = new int[r-q];
            // Copy arr subarrays
            for(int s = 0; s < L.Length; s++) 
                L[s] = arr[p + s];
            for(int s = 0; s < R.Length; s++)
                R[s] = arr[q + 1+ s];

            int i, j, k; i = j = 0;
            // Combine them into arr
            for(k = p; (k <= r) && (i < L.Length) && (j < R.Length); k++)
            {
                if(L[i] <= R[j])
                {
                    arr[k] = L[i]; i++;
                }
                else
                {
                    NumInversionBasedOnMergeSort += L.Length - i;
                    arr[k] = R[j]; j++;
                }
            }
            for(;i< L.Length;i++)
            {
                arr[k] = L[i]; k++;
            }
            for(;j< R.Length;j++)
            {
                arr[k] = R[j]; k++;
            }
            return NumInversionBasedOnMergeSort;
        }
        #endregion
        public static void TestNumberInversionAlgorithms()
        {
            {
                var arr = Generator.GenerateArray(5); var arr2 = new int[arr.Length]; Array.Copy(arr, arr2, arr.Length);
                Console.WriteLine("initial array: {0}", Output.ArrayToString(arr));
                List<Tuple<int, int>> inversions;
                var res = NumberInversionBasedOnSelectionSort(arr, out inversions);
                Console.WriteLine("Number of inversions: {0}", res);
                foreach(var inv in inversions)
                {
                    Console.Write("({0};{1})", inv.Item1, inv.Item2);
                }
                Console.WriteLine();
                res = NumberInversionBasedOnMergeSort(arr2);
                Console.WriteLine("Number of inversions: {0}", res);

            }
            
            
        }
        #endregion
        #endregion
        
        #region Max subarray problem
        public static Tuple<int, int, int> FindMaxSubarrayDynamic(int[] arr) // O(n^2)
        {
            var dp = new int[arr.Length + 1]; // dp[i] = arr[0] + arr[1] + .. arr[i - 1]; dp[0] = 0
                                          // dp[j + 1] - dp[i] = arr[i] + arr[i + 2] + .. arr[j] = sum from i to j
            dp[0] = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                dp[i + 1] = dp[i] + arr[i];
            }
            int left = 0; int right = 0; int max = arr[0];
            for(int i = 0; i < arr.Length; i++)
            {
                for(int j = i; j < arr.Length; j++)
                {
                    if(max < dp[j + 1] - dp[i])
                    {
                        max = dp[j + 1] - dp[i]; left = i; right = j;
                    }
                }
            }
            return new Tuple<int, int, int>(left, right, max);
        }
        public static Tuple<int, int, int> FindMaxSubarrayDynamicEffective(int[] arr) // O(n)
        {
            var dp = new int[arr.Length + 1]; // dp[i] = arr[0] + arr[1] + .. arr[i - 1]; dp[0] = 0
                                          // dp[j + 1] - dp[i] = arr[i] + arr[i + 2] + .. arr[j] = sum from i to j
            dp[0] = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                dp[i + 1] = dp[i] + arr[i];
            }
            int left = 0; int right = 0; int max_sum = arr[0]; int min_idx = 0;
            for(int j = 1; j < arr.Length; j++)
            {
                if(dp[min_idx] > dp[j]) min_idx = j;
                if(max_sum < dp[j + 1] - dp[min_idx])
                {
                    max_sum = dp[j + 1] - dp[min_idx]; left = min_idx; right = j;
                }
            }
            return new Tuple<int, int, int>(left, right, max_sum);
        }
        public static Tuple<int, int, int> FindMaxSubarrayDynamicEffective2(int[] arr) // O(n)
        {
            int left = 0; int right = 0; int max_sum = arr[0];
            int tmp_i = 0; int tmp_sum = arr[0];
            for(int j = 0; j < arr.Length - 1; j++) // Find optimal sum that ends with j + 1 the element
            {
                if(tmp_sum > 0) // If previous sum more than 0 it is better to add it to general sum
                    tmp_sum += arr[j+1];
                else // otherwise max subarray that ends with j + 1 th element consist of 1 element
                {
                    tmp_i = j +1;
                    tmp_sum = arr[j+1];
                }
                if(tmp_sum > max_sum) // save the best result
                {
                    left = tmp_i; right = j + 1; max_sum = tmp_sum;
                }
            }
            return new Tuple<int, int, int>(left, right, max_sum);
        }
        public static Tuple<int, int, int> FindMaxSubarray(int[] arr) // O(nlog n)
        {
            return FindMaxSubarray(arr, 0, arr.Length - 1);
        }
        public static Tuple<int, int, int> FindMaxSubarray(int[] arr, int left, int right)
        {
            if(right == left) return new Tuple<int, int, int>(left, right, arr[left]);
            int mid = (right + left) / 2;
            var res1 = FindMaxSubarray(arr, left, mid);
            var res2 = FindMaxSubarray(arr, mid + 1, right);
            var max = FindMaxCrossingSubarray(arr, left, right, mid);
            if(max.Item3 < res1.Item3) max = res1;
            if(max.Item3 < res2.Item3) max = res2;
            return new Tuple<int, int, int>(max.Item1, max.Item2, max.Item3);
        }
        public static Tuple<int, int, int> FindMaxCrossingSubarray(int[] arr, int left, int right, int mid) // find max subarray with sum that cross mid point
        {
            var sum = 0; 
            int left_sum = Int32.MinValue;
            int left_max = mid;
            for(int i = mid; i >= left; i--)
            {
                sum += arr[i];
                if(sum > left_sum)
                {
                    left_sum = sum;
                    left_max = i;
                }
            }
            sum = 0;
            int right_sum = Int32.MinValue;
            int right_max = mid;
            for(int i = mid + 1; i <= right; i++)
            {
                sum += arr[i];
                if(sum > right_sum)
                {
                    right_sum = sum;
                    right_max = i;
                }
            }
            return new Tuple<int, int, int>(left_max, right_max, left_sum + right_sum);
        }
        # region Find max subarray testing
        public delegate Tuple<int, int, int> FindMaxSubarrayMethod(int[] arr);
        private static void TestFindMaxSubarrayMethod(FindMaxSubarrayMethod method, string name)
        {
            Console.WriteLine(name);
            Generator.MIN = -20;
            var arr = Generator.GenerateArray(25);
            Console.WriteLine("initial array: {0}", Output.ArrayToString(arr));
            /*
            var outp = new StreamWriter(Console.OpenStandardOutput());
            var act = new Action(()=> method(arr));
            Estimator.Estimate(act, outp, name);
            outp.Close();
            */

            var res = method(arr);
            Console.WriteLine("max array: [{0}..{1}] with sum: {2}", res.Item1, res.Item2, res.Item3);
              
            Console.WriteLine();
        }
        
        public static void TestFindMaxSubarrayAlgorithms()
        {
            TestFindMaxSubarrayMethod(FindMaxSubarray, "Recursive method:");
            TestFindMaxSubarrayMethod(FindMaxSubarrayDynamic, "Dynamic method:");
            TestFindMaxSubarrayMethod(FindMaxSubarrayDynamicEffective, "Dynamic method effective:");
            TestFindMaxSubarrayMethod(FindMaxSubarrayDynamicEffective2, "Dynamic method effective:");
            
        }
        
        #endregion
        #endregion
        
        #region Strassen's algorithm for matrices multiplication
        public static int[,] MultiplyMatrices(int[,] A, int[,] B) // O(n^3)
        {
            if(A.GetUpperBound(1) != B.GetUpperBound(0)) return new int[1,1];
            var C = new int[A.GetUpperBound(0) + 1, B.GetUpperBound(1) + 1];
            for(int i = 0; i < C.GetUpperBound(0) + 1; i++)
            {
                for(int j = 0; j < C.GetUpperBound(1) + 1; j++)
                {
                    C[i, j] = 0;
                    for(int k = 0; k < A.GetUpperBound(1) + 1; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j]; 
                    }
                }
            }
            return C;
        }
        # region Strassen's algorithm for matrices multiplication testing
        //public delegate Tuple<int, int, int> FindMaxSubarrayMethod(int[] arr);
        private static void TestMatricesMultiplicationMethod(Func<int[,], int[,], int[,]> method, string name)
        {
            Console.WriteLine(name);
            Generator.MIN = -20;
            var A = Generator.GenerateArray(3, 2); var B = Generator.GenerateArray(2, 4);
            Console.WriteLine("A:\n{0}", Output.ArrayToString(A));
            Console.WriteLine("B:\n{0}", Output.ArrayToString(B));
            var C = method(A, B);
            Console.WriteLine("C:\n{0}", Output.ArrayToString(C));
            Console.WriteLine();
        }
        
        public static void TestMatricesMultiplicationAlgorithms()
        {
            TestMatricesMultiplicationMethod(MultiplyMatrices, "Plain multiplication method:");
            
        }
        #endregion

        #endregion
        
        public static double Pow(double x, int n) // O(log n)
        {
            double res = 1;
            while(n!=0)
            {
                if((n & 1) == 1) res *= x;
                x *= x;
                n = n >> 1;
            }
            return res;
        }
        # region Factorial methods
        // O(N)
        public static BigInteger FactorialNaive(int n)
        {
            var res = 1;
            for(int i = 2; i < n; i++) res *= i;
            return res;
        }
        private static BigInteger FactioralByTreeHelper(int l, int r)
        {
            if(l == r) return l;
            if(l - r == 1) return l * r;
            var m = (l + r) / 2;
            return FactioralByTreeHelper(l, m) * FactioralByTreeHelper(m+1, r);
        }
        public static BigInteger FactorialByTree(int n)
        {
            if (n <= 1) return 1;
            return FactioralByTreeHelper(2, n);
        }

        public static BigInteger FactorialByFactorization(int n)
        {
            var primes = EratosthenesSieve(n);
            double res = 1;
            foreach(var p in primes)
            {
                int pow = 0;
                int m = n / p;
                while(m>0)
                {
                    pow += m;
                    m /= p;
                }
                res *= Math.Pow(p, pow);
            }
            return (BigInteger)res;
        }
        #endregion
        public static List<int> EratosthenesSieve(int N) // Sieve of Eratosthenes - find all prime numbers less eqaul than N; O(n)
        {
            var primes = new List<int>();
            var lp = new int[N+1]; // array of least primes for i;
            for(int i = 0; i < lp.Length; i++) lp[i] = 0;
            for(int i = 2; i < lp.Length; i++)
            {
                if(lp[i] == 0) primes.Add(i);
                for(int j = 0; j < primes.Count; j++)
                {
                    if(primes[j] > i) break;
                    var idx = i * primes[j];
                    if(idx <= N) lp[idx] = primes[j];
                }
            }
            return primes;
        }
    }
}
