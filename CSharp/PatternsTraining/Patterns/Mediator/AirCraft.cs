using System;

namespace Patterns
{
    public class AirCraft : Component
    {
        public string Name { get; set; }
        public AirCraft(ITower tower) : base(tower) { NotifeMe = false; }

        public override void RecieveMessage(string message)
        {
            Console.WriteLine("AirCraft {0} recieved {1}", Name, message);
        }
    }
}