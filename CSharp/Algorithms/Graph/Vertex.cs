using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Text;
using System.Linq;

namespace Graph
{
    // https://codereview.stackexchange.com/questions/131583/generic-graph-implementation-in-c/131612
    class Vertex<T>
    {
        public Vertex(T value, params Vertex<T>[] parameters) 
            : this(value, (IEnumerable<Vertex<T>>)parameters) { }

        public Vertex(T value, IEnumerable<Vertex<T>> neighbors = null)
        {
            Value = value;
            Neighbors = neighbors?.ToList() ?? new List<Vertex<T>>();
            IsVisited = false;  // can be omitted, default is false but some
                                // people like to have everything explicitly
                                // initialized
        }

        public T Value { get; }   // can be made writable

        public List<Vertex<T>> Neighbors { get; }

        public bool IsVisited { get; set; }

        public int NeighborCount  => Neighbors.Count; 

        public void AddEdge(Vertex<T> vertex)
        {
            Neighbors.Add(vertex);
        }

        public void AddEdges(params Vertex<T>[] newNeighbors)
        {
            Neighbors.AddRange(newNeighbors);
        }

        public void AddEdges(IEnumerable<Vertex<T>> newNeighbors)
        {
            Neighbors.AddRange(newNeighbors);
        }

        public void RemoveEdge(Vertex<T> vertex)
        {
            Neighbors.Remove(vertex);
        }

        public override string ToString()
        {
            return Neighbors.Aggregate(new StringBuilder($"{Value}: "), (sb, n) => sb.Append($"{n.Value} ")).ToString();
            //return $"{Value}: {(string.Join(" ", Neighbors.Select(n => n.Value)))}";
        }

    }

}