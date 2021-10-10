namespace Patterns
{
    public class CommandChangeColor : ITextCommand
    {
        public string Color { get; set; }

        public void Execute(Text text)
        {
            text.Color = Color;
        }
    }
}