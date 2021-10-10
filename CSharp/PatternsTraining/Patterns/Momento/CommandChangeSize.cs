namespace Patterns
{
    public class CommandChangeSize : ITextCommand
    {
        public int Size { get; set; }

        public void Execute(Text text)
        {
            text.Size = Size;
        }
    }
}