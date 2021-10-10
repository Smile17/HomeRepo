using System;

namespace Patterns
{
    public class Plane : Component
    {
        public int Number { get; set; }
        public Plane(ITower tower) : base(tower) { NotifeMe = true; }

        public override void RecieveMessage(string message)
        {
            Console.WriteLine("Plane {0} recieved {1}", Number, message);
        }
    }
}