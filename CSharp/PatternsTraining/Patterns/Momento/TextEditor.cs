using System.Collections.Generic;

namespace Patterns
{
    public class TextEditor
    {
        HistorySnapshot History = new HistorySnapshot();
        public Text Text { get; private set; }
        public TextEditor(Text text)
        {
            Text = text;
            History.Add(new Snapshot(this, Text));
        }

        public override string ToString()
        {
            return Text.ToString();
        }

        public void Undo()
        {
            History.Undo();
        }
        public void Redo()
        {
            History.Redo();
        }

        public void Execute(List<ITextCommand> comm)
        {
            for (int i = 0; i < comm.Count; i++)
            {
                comm[i].Execute(Text);
            }
            History.Add(new Snapshot(this, Text));
        }
        public void Restore(Text text)
        {
            Text = text;
        }
    }
}