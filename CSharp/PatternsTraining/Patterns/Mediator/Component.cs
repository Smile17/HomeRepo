namespace Patterns
{
    public abstract class Component
    {
        public Component(ITower tower)
        {
            Tower = tower;
        }

        public ITower Tower { get; set; }
        public bool NotifeMe { get; set; }

        public void SendMessage(string message)
        {
            Tower.notify(this, message);
        }
        abstract public void RecieveMessage(string message);
        
    }
}