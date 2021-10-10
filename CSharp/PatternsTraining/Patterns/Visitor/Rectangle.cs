using System;

namespace Patterns
{
    public class Rectangle : IShape
    {
        public int X { get; set; }

        public int Y { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
        public override string ToString()
        {
            return String.Format("{0};{1}", X, Y);
        }
    }
}