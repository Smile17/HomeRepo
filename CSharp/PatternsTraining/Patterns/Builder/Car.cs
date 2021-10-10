using System;

namespace Patterns
{
    public class Car
    {
        public string Skeleton { get; set; }
        public string Door { get; set; }
        public string Color { get; set; }

        public override string ToString()
        {
            return String.Format("Skeleton: {0}, Door: {1}, Color: {2}", Skeleton, Door, Color);
        }
    }
}