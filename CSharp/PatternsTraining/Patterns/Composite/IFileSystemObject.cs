using System.Collections.Generic;

namespace Patterns
{
    public interface IFileSystemObject
    {
        int Size { get; set; }
        string Name { get; set; }
        List<IFileSystemObject> FileSystemObjects { get; set; }

        int SizeFull();
    }
}