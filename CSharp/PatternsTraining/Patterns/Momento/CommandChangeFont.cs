namespace Patterns
{
    public class CommandChangeFont : ITextCommand
    {
        public string Font { get; set; }
        public void Execute(Text text)
        {
            text.Font = Font;
        }
    }
}