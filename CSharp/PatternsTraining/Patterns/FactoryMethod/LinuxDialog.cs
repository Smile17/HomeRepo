namespace Patterns
{
    public class LinuxDialog : ICreator
    {
        public IItem CreateButton()
        {
            return new LinuxButton();
        }
    }
}