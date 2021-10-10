namespace Patterns
{
    public abstract class LineBehavior : ILineBehavior
    {
        public ILineBehavior Next { get; set; }

        public bool Attack(int score)
        {
            if (MakeAttack(score)) return true;
            return Next.Attack(score);
        }
        protected abstract bool MakeAttack(int score);

        public void SetNext(ILineBehavior next)
        {
            Next = next;
        }
    }
}