using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Linq;
using System.Threading;


namespace BagProblem
{
    public class BagProblem
    {
        // Bag Problem: Items can be taken only once
        public static int Task1(List<Item> items, int bagSize)
        {
            int[,] dyn = new int[items.Count, bagSize + 1];
            List<Item>[,] path = new List<Item>[items.Count, bagSize + 1];
            for(int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                for (int j = 0; j < bagSize + 1; j++)
                {
                    
                    // We cannot take this item
                    if (j < item.Weight) 
                        if (i > 0)
                        {
                            dyn[i, j] = dyn[i - 1, j];
                            path[i, j] = new List<Item>(path[i - 1, j]);
                        }
                        else
                        {
                            dyn[i, j] = 0;
                            path[i, j] = new List<Item>();
                        }
                    else
                    {
                        if (i > 0)
                        {
                            var add = item.Price + dyn[i - 1, j - item.Weight];
                            if(add > dyn[i - 1, j])
                            {
                                dyn[i, j] = add;
                                path[i, j] = (new List<Item>(path[i - 1, j - item.Weight]));
                                path[i, j].Add(item);
                            }
                            else{
                                dyn[i, j] = dyn[i - 1, j];
                                path[i, j] = new List<Item>(path[i - 1, j]);
                            }
                        }
                        else
                        {
                            dyn[i, j] = item.Price;
                            path[i, j] = new List<Item>();
                            path[i, j].Add(item);
                        }
                    }
                }
            }
            Console.WriteLine(dyn.ToStringExtended());
            Console.WriteLine(path[items.Count - 1, bagSize - 1].ToStringExtended());
            return dyn[items.Count - 1, bagSize - 1];
        }
        public static void TestTask1()
        {
            // Generate items
            int N = 5;
            var arrId = Helper.GenerateStringArray(N);
            var arr = Helper.GenerateArray(N);
            Thread.Sleep(2000);
            var weight = Helper.GenerateArray(N);
            var items = new List<Item>();
            for (int i = 0; i < N; i++)
            {
                items.Add(new Item(){ID = arrId[i], Price = 10 * arr[i], Weight = weight[i]});
            }
            Console.WriteLine(items.ToStringExtended());

            // Generate bag size
            Random randNum = new Random(DateTime.Now.Second);
            //int bagSize = randNum.Next(N * 10);
            int bagSize = 10;

            Console.WriteLine(bagSize);
            var res = Task1(items, bagSize);
            Console.WriteLine(res);
        }

        // Find the biggest common continuous substring of a and b
        public static int Task2(string a, string b)
        {
            var dyn = new int[a.Length, b.Length];
            var path = new Point[a.Length, b.Length];
            for(int i = 0; i < a.Length; i++)
            {
                for(int j = 0; j < b.Length; j++)
                {
                    int x = 0;
                    Point parent = new Point(){X = -1, Y = -1};
                    if (!(i == 0 || j == 0)) 
                    {
                        x = dyn[i - 1, j - 1];
                        parent = new Point(){X = i - 1, Y = j - 1};
                    }
                    if(a[i] == b[j])
                        dyn[i, j] = x + 1;
                    else
                    {
                        dyn[i, j] = 0;
                        parent = new Point(){X = -1, Y = -1};
                    }
                    path[i, j] = parent;
                }
            }
            Console.WriteLine(dyn.ToStringExtended());
            Console.WriteLine(path.ToStringExtended());
            var arr = dyn.Cast<int>();
            var max = arr.Max();
            // Find path
            var c = Array.IndexOf(arr.ToArray(), max);
            var root = new Point(){X = c / b.Length, Y = c % b.Length};

            var node = root;
            while(node.X > 0 && node.Y > 0)
            {
                Console.WriteLine(node);
                node = path[node.X, node.Y];
            }

            Console.WriteLine();
            node = root;
            //Find path another way
            for(int i = 0; i <= max; i++)
            {
                Console.WriteLine(node);
                node.X--;
                node.Y--;
            }
            
            return max;

        }
        public static void TestTask2()
        {
            var res = Task2("afishxx", "ahuish");
            Console.WriteLine(res);
        }

        // Find the biggest common subsequense of 2 strings
        public static int Task3(string a, string b)
        {
            var dyn = new int[a.Length, b.Length];
            int i = 0; int j = 0;
            for(i = 0; i < a.Length; i++)
            {
                for(j = 0; j < b.Length; j++)
                {
                    int x = 0;
                    int y = 0;
                    if (i != 0) x = dyn[i - 1, j];
                    if (j != 0) y = dyn[i, j - 1];
                    dyn[i, j] = Math.Max(x, y);
                    if(a[i] == b[j])
                        dyn[i, j]++;
                }
            }
            Console.WriteLine(dyn.ToStringExtended());

            i = a.Length - 1; j = b.Length - 1;
            while (i + j >= 0)
            {
                int x = 0;
                int y = 0;
                if (i != 0) x = dyn[i - 1, j];
                if (j != 0) y = dyn[i, j - 1];

                if (a[i] == b[j])
                {
                    Console.WriteLine("{0}; {1}; {2}", i, j, a[i]);
                    i--; j--;
                }
                else{
                    if (i > 0)
                    if (dyn[i, j] == dyn[i - 1, j])
                    {
                        i--;
                    }
                    else{ j--;}
                    else{
                        if (dyn[i, j] == dyn[i, j - 1])
                        {
                            j--;
                        }
                        else{ i--;}
                    }
                }
            }


            return dyn[a.Length - 1, b.Length - 1];

        }
        public static void TestTask3()
        {
            var res = Task3("afxxish", "ahish");
            Console.WriteLine(res);
        }

        // Find the biggest increasing sequense in the array
        public static int Task4(int[] arr)
        {
            var dyn = new int[arr.Length];
            var path = new int[arr.Length];
            for(int i = 0; i < dyn.Length; i++)
            {
                //int res = 0;
                int j_max = -1;
                for(int j = 0; j < i; j++)
                {
                    if(arr[i] > arr[j] && j_max < 0) j_max = j;
                    else
                    if(arr[i] > arr[j] && dyn[j_max] < dyn[j]) j_max = j;
                }
                path[i] = j_max;
                if(j_max >= 0) dyn[i] = dyn[j_max] + 1;
                else dyn[i] = 1;

            }
            Console.WriteLine("DYN: " + dyn.ToStringExtended());

            var max = dyn.Max();
            // Find path
            var c = Array.IndexOf(dyn.ToArray(), max);
            while (c >= 0)
            {
                Console.Write(arr[c] + "\t");
                c = path[c];
            }
            Console.WriteLine();

            return max;

        }
        public static void TestTask4()
        {
            var arr = Helper.GenerateArray(10);
            Console.WriteLine(arr.ToStringExtended());
            var res = Task4(arr);
            Console.WriteLine(res);
        }
    }
}
