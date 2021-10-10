using System;
using OxfordDictionariesAPI;

namespace PronunciationUploader
{
    class Program
    {
        static void Main(string[] args)
        {
            //Input data
            var file = args.Length > 0 ? args[0] : "examples//test.txt";
            //Oxford client
            // https://developer.oxforddictionaries.com/admin/applications
            OxfordDictionaryClient _client = new OxfordDictionaryClient("f738ea6e", "1971fd1be3386c36288c67d3524c827e");
            
            //(new AudioUploader(_client, file)).Upload();
            (new AudioLinkUploader(_client, file)).Upload();


            Console.WriteLine("Done...");
            Console.ReadLine();
        }

        
    }
}
