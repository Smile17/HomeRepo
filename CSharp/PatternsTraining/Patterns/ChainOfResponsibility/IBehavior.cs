namespace Patterns
{
    public interface ILineBehavior
    {
        ILineBehavior Next { get;}
        void SetNext(ILineBehavior next);

        bool Attack(int score);
    }
}