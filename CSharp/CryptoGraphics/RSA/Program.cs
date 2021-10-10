using System;

namespace CryptoGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new RSA(41, 37);
            //var ciphedText = service.Encrypt("ABCabc");
            //var initialText = service.Decrypt(ciphedText);
            //Console.WriteLine(initialText);

            RSA.TestRSA("inp.txt");

            Console.WriteLine("Done...");
        }
    }
}
