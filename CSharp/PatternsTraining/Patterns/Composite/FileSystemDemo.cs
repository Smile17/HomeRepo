using System;
using System.Collections.Generic;

namespace Patterns
{
    public class FileSystemDemo
    {
        public static void MainFileSystemDemo(string[] args)
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
            Console.WriteLine("Size foler1 {0}", folder.SizeFull());
            Console.WriteLine("Size foler2 {0}", folder2.SizeFull());
            Console.WriteLine("Size foler3 {0}", folder3.SizeFull());

        }
    }
}