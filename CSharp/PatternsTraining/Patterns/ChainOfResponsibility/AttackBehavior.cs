using System;

namespace Patterns
{
    public class AttackBehavior : LineBehavior
    {
        protected override bool MakeAttack(int score)
        {
            Console.WriteLine("Aggresive Attack");
            return true;
        }
    }
}