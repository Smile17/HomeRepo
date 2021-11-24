using FileInfo.Model;
using FileInfo.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = FileListUploadService.ReadFromFile("audio_video.json");
            DataGeneratorService.GenerateAudio("audio.json");
            //List<AudioInfo> audioList = DataGeneratorService.ReadAudioFromFile("audio.json");
            List<AudioInfo> audioList = list.OfType<AudioInfo>().ToList();
            DataGeneratorService.GenerateVideo("video.json");
            //List<VideoInfo> videoList = DataGeneratorService.ReadVideoFromFile("video.json");
            List<VideoInfo> videoList = list.OfType<VideoInfo>().ToList();
            int idx = 0;
            AudioInfo audio;
            VideoInfo video;
            Console.WriteLine();
            Console.WriteLine("Menu options:\n" +
                "0 - Exit\n" +
                "1 - Show Audio Collection\n" +
                "2 - Add Audio Item\n" +
                "3 - Remove Audio Item By Index\n" +
                "4 - Edit Audio Item\n" +
                "5 - Show Video Collection\n" +
                "6 - Add Video Item\n" +
                "7 - Remove Video Item By Index\n" +
                "8 - Edit Video Item\n" +
                "9 - Show Video Names\n" +
                "10 - Find Audio Items by Author\n" +
                "11 - Find Video Items by name\n" +
                "12 - Find Video Items by producer");
            while (true)
            {
                var op = Console.ReadLine();

                if (op == "0")
                    break;
                switch (op)
                {
                    case "1":
                        Console.WriteLine(String.Join('\n', audioList));
                        break;
                    case "2":
                        audio = new AudioInfo();
                        audio.InputData();
                        audioList.Add(audio);
                        break;
                    case "3":
                        Console.Write("Input index:");
                        idx = Int32.Parse(Console.ReadLine());
                        audioList.RemoveAt(idx);
                        break;
                    case "4":
                        Console.Write("Choose item by index to edit:");
                        idx = Int32.Parse(Console.ReadLine());
                        audio = audioList[idx];
                        Console.WriteLine(audio);
                        Console.WriteLine("Input changes in format: field name:\"value\" or 0 for exit");
                        while (true)
                        {
                            var editOp = Console.ReadLine();
                            if (editOp == "0")
                                break;
                            var values = editOp.Split(":");
                            values[1].Replace("\"", "");
                            audio[values[0]] = values[1];
                        }
                        break;

                    case "5":
                        Console.WriteLine(String.Join('\n', videoList));
                        break;
                    case "6":
                        video = new VideoInfo();
                        video.InputData();
                        videoList.Add(video);
                        break;
                    case "7":
                        Console.Write("Input index:");
                        idx = Int32.Parse(Console.ReadLine());
                        videoList.RemoveAt(idx);
                        break;
                    case "8":
                        Console.Write("Choose item by index to edit:");
                        idx = Int32.Parse(Console.ReadLine());
                        video = videoList[idx];
                        Console.WriteLine(video);
                        Console.WriteLine("Input changes in format: field name:\"value\" or 0 for exit");
                        while (true)
                        {
                            var editOp = Console.ReadLine();
                            if (editOp == "0")
                                break;
                            var values = editOp.Split(":");
                            values[1].Replace("\"", "");
                            video[values[0]] = values[1];
                        }
                        break;
                    case "9":
                        var names = videoList.Select(x => new { x.Name, x.Price}).OrderBy(g => g.Price);
                        Console.WriteLine("Video names:");
                        Console.WriteLine(String.Join('\n', names));
                        break;
                    case "10":
                        Console.Write("Input author of audio:");
                        var author = Console.ReadLine();
                        var audiosByAuthor = audioList.Where(x => x.Author == author);
                        Console.WriteLine(String.Join('\n', audiosByAuthor));
                        break;
                    case "11":
                        Console.Write("Name:");
                        var name = Console.ReadLine();
                        var videosByName = videoList.Where(x => x.Name.StartsWith(name));
                        Console.WriteLine(String.Join('\n', videosByName));
                        break;
                    case "12":
                        Console.Write("Producer:");
                        var producer = Console.ReadLine();
                        var videosByProducer = videoList.Where(x => x.Producer == producer);
                        Console.WriteLine(String.Join('\n', videosByProducer));
                        break;
                }
            }
        }
    }
}
