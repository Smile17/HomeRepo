using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using NAudio.Wave;
using OxfordDictionariesAPI;

namespace PronunciationUploader
{
    /// <summary>
    /// Upload files from the dictionary and merge them into 1 mp3 file
    /// </summary>
    class AudioUploader
    {
        public OxfordDictionaryClient Client { get; set; }
        public string FileName { get; set; }
        private string[] Words { get; set; }

        public AudioUploader(OxfordDictionaryClient client, string file)
        {
            Client = client;
            FileName = file;
            Words = File.ReadAllLines(FileName);
        }

        public void Upload()
        {
            var folder = Path.GetDirectoryName(FileName);
            var fileName = Path.GetFileNameWithoutExtension(FileName);

            //Create output folder
            folder = folder + "//results//";
            Directory.CreateDirectory(folder);
            folder = folder + fileName;
            Directory.CreateDirectory(folder);

            //Get files
            for (int i = 0; i < Words.Length; i++)
            {
                var word = Words[i].Split(';').First().Replace(' ', '_');
                var searchResult = Client.SearchEntries(word, CancellationToken.None).Result;
                try
                {
                    var audioFileUrl = searchResult.Results[0].LexicalEntries[0].Pronunciations[0].AudioFile;
                    if (audioFileUrl == null)
                    {
                        audioFileUrl = searchResult.Results[0].LexicalEntries[0].Pronunciations[1].AudioFile;
                    }
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(audioFileUrl, String.Format("{0}//{1}_{2}.mp3", folder, i + 10, word));
                        Thread.Sleep(2000);
                    }
                }
                catch
                {
                }
            }

            //Merge files into 1
            var filePathes = Directory.GetFiles(folder);
            var outp = File.Create(Path.GetDirectoryName(FileName) + "//results//" + fileName + ".mp3");
            AudioUploader.Combine(filePathes, outp);
            outp.Close();
        }
        private static void Combine(string[] inputFiles, Stream output)
        {
            foreach (string file in inputFiles)
            {
                Mp3FileReader reader = new Mp3FileReader(file);
                if ((output.Position == 0) && (reader.Id3v2Tag != null))
                {
                    output.Write(reader.Id3v2Tag.RawData, 0, reader.Id3v2Tag.RawData.Length);
                }
                Mp3Frame frame;
                while ((frame = reader.ReadNextFrame()) != null)
                {
                    output.Write(frame.RawData, 0, frame.RawData.Length);
                }
            }
        }
    }
}
