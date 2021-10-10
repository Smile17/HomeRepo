namespace Patterns
{
    public class WinDialog : ICreator
    {
        public IItem CreateButton()
        {
            return new WinButton();
        }
    }
}