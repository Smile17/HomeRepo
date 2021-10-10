using System;
using System.Text;
using System.Collections.Generic;
using System.Numerics;
using System.IO;
namespace CryptoGraphics
{
    public class RSA
    {
        // D, N are open key; E is secret key
        public int P {get;set;}
        public int Q {get;set;}
        public int N {get;}
        public int Phi {get;}
        public int D {get;}
        public int E {get;}
        private Random rnd = new Random();
        private RandomBigInteger rndBig = new RandomBigInteger();
        public RSA(int p, int q)
        {
            if(IsPrimeNumber(p) && IsPrimeNumber(q))
            {
                P = p; Q = q; 
            }
            else
            {
                P = 5; Q = 7;
            }
            N = P * Q;
            Phi = (P - 1) * (Q - 1);
            int one = 0; int X;
            do
            {
                D = rnd.Next(1, Phi - 1); if(D % 2 == 0) D++;
                // D * E = 1 mod(Phi) <=> D * E + Phi * X = 1
                (one, E, X) = GCD(D, Phi); 
                E = E % Phi; if(E < 0) E += Phi;
                var check = (E * D) % Phi;
                if(check != 1)
                {
                    Console.WriteLine("Something went wrong in finding secret key... D = {0}, Phi = {1}, GCD = {2}", D, Phi, one);
                }
            }
            while(one != 1);
        }
        private bool IsPrimeNumber(int p)
        {
            if(p == 1) return false;
            if(p == 2) return true;
            for(int i = 2; i < p/2; i++)
                if(p % i == 0) return false;
            return true;
        }
        public BigInteger GetRandom(int length)
        {
            byte[] data = new byte[length];
            rnd.NextBytes(data);
            return new BigInteger(data);
        }
        // Miller-Rubin primarily test
        public bool IsPrimeNumberMillerRubin(BigInteger p, int k = 2)
        {
            if((p == 1) || (p == 4)) return false;
            if((p == 2) || (p == 3) || (p == 5)) return true;
            BigInteger m = p - 1; long t = 0;
            while(m % 2 == 0)
            {
                m /= 2; t++;    
            }
            for(int i = 0; i < k; i++)
            {
                bool isPrime = false;
                //BigInteger a = rnd.Next(2, p - 2);
                BigInteger a = rndBig.NextBigInteger(2, p - 2);
                BigInteger u = BigInteger.ModPow(a, m, p);
                if(u == 1)
                    isPrime = true;
                else
                for(int j = 0; j < t; j++)
                {
                    // u = a^(m*2^j)
                    if( (u == -1) || (u == (p - 1)) )
                    {
                        isPrime = true;
                        break;
                    }
                    u  = (u * u) % p;
                }
                if(!isPrime)
                {
                    //Console.WriteLine("{0} is not prime; {1} is witnessed it", p, a);
                    return false;
                }
                isPrime = false;
            }
            return true;
        }

        public (int, int, int) GCD(int a, int b)
        {
            var (a1, b1) = (a, b);
            int x1, y1, x2, y2, tmp; x1 = 1; y1 = 0; x2 = 0; y2 = 1;
            if(a < b)
            {
                (a, b) = (b, a);
                //tmp = a; a = b; b = tmp;
                x1 = 0; y1 = 1; x2 = 1; y2 = 0;
            }
            while(b != 0)
            {
                var q = a / b;
                var r = a % b;
                tmp = x2; x2 = x1 - q * x2; x1 = tmp;
                tmp = y2; y2 = y1 - q * y2; y1 = tmp;
                a = b; b = r;
            }
            if((a1 * x1 + b1 * y1) != a)
            {
                Console.WriteLine("Something went wrong in GCA with input values: {0}; {1}", a, b);
            }
            return (a, x1, y1);
        }
        public BigInteger[] Encrypt(string s)
        {
            // Convert the string into a byte array.
            byte[] arr = Encoding.UTF8.GetBytes(s);
            BigInteger[] res = new BigInteger[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                //res[i] = BigInteger.Pow(arr[i], D) % N;
                res[i] = BigInteger.ModPow(arr[i], D, N);
            }
            return res;
        }
        
        public string Decrypt(BigInteger[] arr)
        {
            byte[] res = new byte[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                //var a = (BigInteger.Pow(arr[i], E) % N);
                var a = BigInteger.ModPow(arr[i], E, N);
                res[i] = (byte)a;
            }
            return Encoding.UTF8.GetString(res);
        }
        // Methods for testing
        public static void TestRSA(string inputFile)
        {
            var path = "outp.txt";
            var s = File.ReadAllText(inputFile);
            Console.WriteLine("Initial text: {0}...", s.Substring(0, Math.Min(10, s.Length))); // To check whether we open the file and look at first characters
            RSA service = new RSA(37,41);
            var ciphedText = service.Encrypt(s);
            var outp = new StreamWriter(path);
            outp.WriteLine(String.Join(" ", ciphedText));
            outp.Close();

            var lines =  File.ReadAllText(path).Split(' ');
            var arr = new BigInteger[lines.Length];
            for(int i = 0; i < lines.Length; i++)
                arr[i] = BigInteger.Parse(lines[i]);

            var deciphedText = service.Decrypt(arr);
            Console.WriteLine("Deciphed text: \n{0}", deciphedText);
        }
    }
}
