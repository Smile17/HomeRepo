using System;

namespace Patterns
{
    public class DefaultBehavior : LineBehavior
    {
        public int Score { get; set; }
        
        protected override bool MakeAttack(int score)
        {
            if (Score > score)
            {
                Console.WriteLine("Default Attack");
            }
            return Score > score;
        }
    }
}