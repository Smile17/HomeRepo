using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Text;
using System.Linq;

namespace Graph
{
    class VertexHelper
    {
        public static Dictionary<int, List<int>> GenerateGraph(int N, double frequency = 0.3)
        {
            var vertices = new Dictionary<int, List<int>>();
            var M = (int)(N * frequency);
            var rand = new Random(DateTime.Now.Second);
            for(int i = 0;i < N; i++)
            {
                vertices.Add(i, GenerateNeighbors(N, M, i, rand));
            }
            return vertices;
        }
        public static string GraphToString(Dictionary<int, List<int>> vertices)
        {
            var s = "";
            foreach (var key in vertices.Keys)
            {
                s += key + ": " + String.Join(',', vertices[key]) + '\n';
            }
            return s;
        }
        public static List<int> GenerateNeighbors(int N, int M, int i, Random rand)
        {
            
            var neighbors = new HashSet<int>();
            while (neighbors.Count < M)
            {
                var e = rand.Next(N);
                if (e != i)
                    neighbors.Add(e);
            }
            return neighbors.ToList();
        }

    }

}