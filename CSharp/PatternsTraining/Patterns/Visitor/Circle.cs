using System;

namespace Patterns
{
    public class Circle : IShape
    {
        public int X { get; set; }

        public override string ToString()
        {
            return String.Format("{0};{1}", X, Y);
        }

        public int Y { get; set; }

        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }
    }
       
}