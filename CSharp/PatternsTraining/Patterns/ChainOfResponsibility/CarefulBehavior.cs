using System;

namespace Patterns
{
    public class CarefulBehavior : LineBehavior
    {
        public int Score { get; set; }
        protected override bool MakeAttack(int score)
        {
            if (2 * Score > score)
            {
                Console.WriteLine("Careful Attack");
            }
            return 2 * Score > score;
        }
    }
}