using System.Collections.Generic;

namespace Patterns
{
    public class FolderDepthIterator : IFileSystemObjectIterator
    {
        private List<IFileSystemObject> Stack { get; set; } = new List<IFileSystemObject>();
        private IEnumerator<IFileSystemObject> StackEnum { get; set; }
        public IFileSystemObject Folder { get; set; }
        public IFileSystemObject Current { get; set; }
        public FolderDepthIterator(IFileSystemObject folder)
        {
            Folder = folder;
            AddToStack(Folder);
            StackEnum = Stack.GetEnumerator();
            StackEnum.MoveNext();

        }
        private void AddToStack(IFileSystemObject folder)
        {
            Stack.Add(folder);
            if (folder.FileSystemObjects == null) return;
            for (int i = 0; i < folder.FileSystemObjects.Count; i++)
            {
                AddToStack(folder.FileSystemObjects[i]);
            }
        }


        public IFileSystemObject GetNext()
        {
            Current = StackEnum.Current;
            StackEnum.MoveNext();
            return Current;
        }

        public bool HasNext()
        {
            return StackEnum.Current!=null;
        }
    }
}