using FileInfo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FileInfo.Service
{
    public class FileListUploadService
    {
        public static List<FileInformation> ReadFromFile(string path)
        {
            var json = File.ReadAllText(path);
            var objs = JsonSerializer.Deserialize<List<JsonElement>>(json);
            List<FileInformation> res = new List<FileInformation>();
            for (int i = 0; i < objs.Count; i++)
            {
                var obj = objs[i].ToString();
                var type = objs[i].GetProperty("Type").ToString();

                if (type == "0")
                    res.Add(JsonSerializer.Deserialize<AudioInfo>(obj));
                else
                    res.Add(JsonSerializer.Deserialize<VideoInfo>(obj));
            }
            return res;
        }
    }
}
