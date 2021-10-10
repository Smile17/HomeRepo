using System;

namespace Patterns
{
    public class LightRectangle : LightShape
    {
        public LightRectangle(string texture) : base(texture) { }
        public int X { get; set; }

        public int Y { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        
        public override string ToString()
        {
            return String.Format("{0};{1}", X, Y);
        }
    }
}