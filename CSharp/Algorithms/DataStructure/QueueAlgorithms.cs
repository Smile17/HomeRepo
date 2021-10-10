using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    class QueueAlgorithms
    {

        # region BFS - Breadth-First Search
        private static Tuple<int, string> BFS(List<int>[] nodes, int i)
        {
            var queue = new Queue<int>(); queue.Enqueue(i);
            var visitedNodes = new HashSet<int>();
            var parent = new Dictionary<int, int>();
            while (queue.Count != 0)
            {
                var e = queue.Dequeue();
                if (!visitedNodes.Contains(e))
                {
                    if (e % 5 == 2) 
                    {
                        var path = e.ToString() + " <- ";
                        var x = e;
                        while(true)
                        {
                            if (x == i) break;
                            x = parent[x];
                            path += x.ToString() + " <- ";
                        }
                        return new Tuple<int, string>(e, path);
                    }
                    nodes[e].ForEach(o => {
                        queue.Enqueue(o);
                        if (!parent.ContainsKey(o))
                            parent.Add(o, e);
                    });
                    visitedNodes.Add(e);
                }
            }
            return new Tuple<int, string>(-1, "");
        }
        public static void BFSTest()
        {
            var N = 10;
            var nodes = new List<int>[N];
            Random rand = new Random(DateTime.Now.Second);
            for(int i = 0; i < nodes.Length; i++)
            {
                var neigh = new List<int>();
                for(int j = 0; j < N / 2; j++)
                {
                    neigh.Add(rand.Next(N));
                }
                nodes[i] = neigh;
            }
            for(int i = 0; i < nodes.Length; i++)
            {
                Console.WriteLine("{0}: {1}", i, String.Join(' ', nodes[i]));
            }
            var res = BFS(nodes, 0);
            Console.WriteLine(res.Item1 + " " + res.Item2);
        }
        #endregion

        # region Next Greater Element
        // 
        public static int[] Method(int[] arr)
        {
            return arr;
        }
        public static void MethodTest()
        {
            var arr = Helper.GenerateArray(20);
            Console.WriteLine(arr.ToStringExtended());
            var res = Method(arr);
            Console.WriteLine(res);
        }
        #endregion
    }


}