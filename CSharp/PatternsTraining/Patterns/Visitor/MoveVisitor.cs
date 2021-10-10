namespace Patterns
{
    public class MoveVisitor : IVisitor
    {
        public int X { get; set; }
        public int Y { get; set; }
        public void Visit(Circle circle)
        {
            circle.X += X;
            circle.Y += Y;
        }

        public void Visit(Rectangle rectangle)
        {
            rectangle.X += X;
            rectangle.Y += Y;
        }

        public void Visit(Triangle triangle)
        {
            triangle.X1 += X;
            triangle.Y1 += Y;
            triangle.X2 += X;
            triangle.Y2 += Y;
            triangle.X3 += X;
            triangle.Y3 += Y;
        }
    }
}