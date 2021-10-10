namespace Patterns
{
    public class Snapshot
    {
        private Text Text { get; }
        private TextEditor Editor { get; }
        public Snapshot(TextEditor editor, Text text)
        {
            Editor = editor;
            Text = (Text)text.Clone();
        }
        public void Restore()
        {
            Editor.Restore(Text);
        }

        
    }
}