using System.Collections.Generic;

namespace Patterns
{
    public class File : IFileSystemObject
    {
        public int Size { get; set; } = 0;
        public string Name { get; set; }
        public List<IFileSystemObject> FileSystemObjects { get; set; } = null;

        public int SizeFull()
        {
            return Size;
        }
    }
}