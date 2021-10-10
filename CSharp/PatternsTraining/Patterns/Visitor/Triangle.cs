using System;

namespace Patterns
{
    public class Triangle : IShape
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int X3 { get; set; }
        public int Y3 { get; set; }
        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
        public override string ToString()
        {
            return String.Format("{0};{1}", X1, Y1);
        }
    }
}