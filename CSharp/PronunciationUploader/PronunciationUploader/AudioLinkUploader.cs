using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using NAudio.Wave;
using OxfordDictionariesAPI;

namespace PronunciationUploader
{
    class AudioLinkUploader
    {
        public OxfordDictionaryClient Client { get; set; }
        public string FileName { get; set; }
        private string[] Words { get; set; }

        public AudioLinkUploader(OxfordDictionaryClient client, string file)
        {
            Client = client;
            FileName = file;
            Words = File.ReadAllLines(FileName);
        }

        public void Upload()
        {
            var folder = Path.GetDirectoryName(FileName);
            var fileName = Path.GetFileNameWithoutExtension(FileName);

            // Output file
            var outp = new StreamWriter(folder + "//" + fileName + "_links.txt");

            //Get files
            for (int i = 0; i < Words.Length; i++)
            {
                
                var word = Words[i].Split(';').First().Replace(' ', '_');
                try
                {
                    var searchResult = Client.SearchEntries(word, CancellationToken.None).Result;
                    var audioFileUrl = searchResult.Results[0].LexicalEntries[0].Pronunciations[0].AudioFile;
                    outp.WriteLine(Words[i] + ";" + audioFileUrl);
                }
                catch
                {
                    outp.WriteLine(Words[i] + ";");
                }
            }
            outp.Close();
        }
    }
}
