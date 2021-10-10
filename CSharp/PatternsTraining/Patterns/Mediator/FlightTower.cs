using System.Collections.Generic;

namespace Patterns
{
    public class FlightTower : ITower
    {
        public List<Component> list { get; set; } = new List<Component>();
        public void notify(Component component, string message)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].NotifeMe)
                {
                    list[i].RecieveMessage(message);
                }
            }
        }
    }
}