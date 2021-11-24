using FileInfo.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace FileInfo.Service
{
    class DataGeneratorService
    {
        public static void GenerateAudio(string path)
        {
            var files = new List<AudioInfo>();
            files.Add(new AudioInfo() { Id = "XD123453", Name = "Til The Light Goes Out", Author = "Lindsey Stirling", Year = 2020, Extension = "mp3", CreatedBy = "anonymous12", Length = 5.42, Price = 3.4, Type = FileType.Audio });
            files.Add(new AudioInfo() { Id = "AD146904", Name = "The Arena", Author = "Lindsey Stirling", Year = 2016, Extension = "mp3", CreatedBy = "anonymous12", Length = 5.02, Price = 6.34, Type = FileType.Audio });
            files.Add(new AudioInfo() { Id = "TR456789", Name = "Toss A Coint To Your Witcher Cover", Author = "Dan Vasc", Year = 2020, Extension = "mp3", CreatedBy = "anonymous12", Length = 3.42, Price = 1.4, Type = FileType.Audio });
            files.Add(new AudioInfo() { Id = "TW876403", Name = "Tor and Ragnarek", Author = "Epidemia", Year = 2021, Extension = "mp3", CreatedBy = "anonymous11", Length = 7.23, Price = 6.4, Type = FileType.Audio });
            files.Add(new AudioInfo() { Id = "KO092341", Name = "Paladin", Author = "Epidemia", Year = 2021, Extension = "mp3", CreatedBy = "anonymous11", Length = 5.42, Price = 3.4, Type = FileType.Audio });
            string json = JsonSerializer.Serialize(files);
            File.WriteAllText(path, json);
        }

        public static void GenerateVideo(string path)
        {
            var files = new List<VideoInfo>();
            files.Add(new VideoInfo() { Id = "TP023944", Name = "Inception", Producer = "Christopher Nolan", MainActor = "DiCaprio", Year = 2010, Extension = "mp4", CreatedBy = "anonymous11", Length = 120, Price = 5, Type = FileType.Video });
            files.Add(new VideoInfo() { Id = "NH231243", Name = "Pirates of the Caribbean: The Curse of the Black Pearl", Producer = "Jerry Bruckheimer", MainActor = "Johnny Depp", Year = 2003, Extension = "mp4", CreatedBy = "anonymous11", Length = 120, Price = 5, Type = FileType.Video });
            files.Add(new VideoInfo() { Id = "RE235932", Name = "Titanic", Producer = "James Cameron", MainActor = "DiCaprio", Year = 1997, Extension = "mp4", CreatedBy = "anonymous12", Length = 100, Price = 4, Type = FileType.Video });
            string json = JsonSerializer.Serialize(files);
            File.WriteAllText(path, json);
        }
        public static List<AudioInfo> ReadAudioFromFile(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<AudioInfo>>(json);
        }
        public static List<VideoInfo> ReadVideoFromFile(string path)
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<VideoInfo>>(json);
        }
    }
}
