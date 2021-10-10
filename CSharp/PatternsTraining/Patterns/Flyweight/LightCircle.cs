using System;

namespace Patterns
{
    public class LightCircle : LightShape
    {
        public int X { get; set; }

        public LightCircle(string texture) : base(texture) { }

        public override string ToString()
        {
            return String.Format("{0};{1}", X, Y);
        }

        public int Y { get; set; }

       
    }
       
}