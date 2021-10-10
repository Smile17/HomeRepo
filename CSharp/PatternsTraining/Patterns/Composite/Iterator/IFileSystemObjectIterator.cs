namespace Patterns
{
    public interface IFileSystemObjectIterator
    {
        IFileSystemObject Current { get; set; }
        IFileSystemObject GetNext();
        bool HasNext();
    }
}