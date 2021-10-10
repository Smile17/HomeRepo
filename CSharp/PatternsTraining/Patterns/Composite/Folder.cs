using System.Collections.Generic;

namespace Patterns
{
    public class Folder : IFileSystemObject
    {
        public List<IFileSystemObject> FileSystemObjects { get; set; }

        public string Name { get; set; }

        public int Size { get; set; } = 0;

        public int SizeFull()
        {
            int s = Size;
            for (int i = 0; i < FileSystemObjects.Count; i++)
            {
                s += FileSystemObjects[i].SizeFull();
            }
            return s;
        }


    }
}