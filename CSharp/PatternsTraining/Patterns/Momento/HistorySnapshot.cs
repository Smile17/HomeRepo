using System.Collections.Generic;

namespace Patterns
{
    public class HistorySnapshot
    {
        private List<Snapshot> Snapshots { get; set; } = new List<Snapshot>();

        private int ActualSize { get; set; } = 0;
        

        public void Add(Snapshot snapshot)
        {
            if (Snapshots.Count < ActualSize)
            {
                Snapshots.RemoveAt(ActualSize - 1);
            }

            Snapshots.Add(snapshot);
            ActualSize = Snapshots.Count;
        }
        public bool Undo()
        {
            if (ActualSize <= 1) return false;
            ActualSize--;
            Snapshots[ActualSize - 1].Restore();
            return true;

        }
        public bool Redo()
        {
            if (ActualSize == Snapshots.Count) return false;
            ActualSize++;
            Snapshots[ActualSize - 1].Restore();
            return true;
        }
    }
}