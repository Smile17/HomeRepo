using LovePdf.Core;
using LovePdf.Model.Task;
using System;
using System.Collections.Generic;
using System.IO;

namespace PdfEditor
{
    class PdfEditorILovePdf
    {
        LovePdfApi Api;
        public PdfEditorILovePdf(LovePdfApi api)
        {
            Api = api;
        }
        public string CompressFile(string folder, string path)
        {
            try
            {
                var taskCompress = Api.CreateTask<CompressTask>();
                // Add files to task for upload
                taskCompress.AddFile(path);
                Console.WriteLine("File has been added");
                // Execute the task
                taskCompress.Process();
                Console.WriteLine("File has been processed");
                // Download the package files
                taskCompress.DownloadFile(folder);
                return "";
            }
            catch
            {
                return path;
            }
        }

        public void CompressFolder(string folder)
        {
            List<string> FallenFile = new List<string>();
            string[] names = Directory.GetFiles(folder);
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine("Start line {0}", i);
                long length = new System.IO.FileInfo(names[i]).Length;
                string res = this.CompressFile(folder, names[i]);
                if (res != "") FallenFile.Add(res);
                long lengthnew = new System.IO.FileInfo(names[i]).Length;
                Console.WriteLine("Old size: {0} New size:{1} Ratio: {2} File:{3}", length, lengthnew, 1.0 * lengthnew / length, names[i]);
            }
            Console.WriteLine("Fallen files: ");
            for (int i = 0; i < FallenFile.Count; i++)
            {
                Console.WriteLine(FallenFile[i]);
            }
        }

    }
}
