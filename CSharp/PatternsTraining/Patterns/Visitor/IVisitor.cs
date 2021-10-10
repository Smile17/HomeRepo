namespace Patterns
{
    public interface IVisitor
    {
        void Visit(Circle circle);
        void Visit(Rectangle rectangle);
        void Visit(Triangle triangle);
    }
}