using System;
using System.Collections.Generic;

namespace Patterns
{
    public class FileSystemIteratorDemo
    {
        public static void MainFileSystemIteratorDemo(string[] args)
        {
            Folder folder = new Folder()
            {
                FileSystemObjects = new List<IFileSystemObject>() {
                    new File() {Size = 5, Name = "File1" },
                    new File() {Size = 8, Name = "File2" }
                },
                Name = "Folder1"
            };
            Folder folder2 = new Folder()
            {
                FileSystemObjects = new List<IFileSystemObject>() {
                    new File() {Size = 15, Name = "File3" },
                    new File() {Size = 6, Name = "File4" }
                },
                Name = "Folder2"
            };
            Folder folder3 = new Folder()
            {
                FileSystemObjects = new List<IFileSystemObject>() {
                    new File() {Size = 2, Name = "File5" },
                    folder, folder2
                },
                Name = "Folder3"
            };
            FolderDepthIterator iterator = new FolderDepthIterator(folder3);
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext().Name);
            }
        }
    }
}