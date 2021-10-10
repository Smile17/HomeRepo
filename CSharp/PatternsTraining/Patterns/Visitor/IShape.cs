namespace Patterns
{
    public interface IShape
    {
        void Accept(IVisitor v);
    }
}