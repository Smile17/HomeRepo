namespace Patterns
{
    public class UnitBehaviorDemo
    {
        public static void MainDemo(string[] args)
        {
            LineBehavior beh = new AttackBehavior();
            beh.SetNext(null);
            beh = new DefaultBehavior() { Score = 30, Next = beh };
            beh = new CarefulBehavior() { Score = 100, Next = beh };
            beh.Attack(20);
            beh.Attack(200);
        }
    }
}