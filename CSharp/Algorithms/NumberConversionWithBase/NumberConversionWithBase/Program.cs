using System;

namespace NumberConversionWithBase
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                int x = 164;
                Console.WriteLine("Initial number:{0}", x);
                var x2 = NumberConvertor.Convert10ToP(x, 2);
                Console.WriteLine("Converted number with base 2:{0}", x2); // 10100100
                var x8 = NumberConvertor.Convert10ToP(x, 8);
                Console.WriteLine("Converted number with base 8:{0}", x8);
                var x16 = NumberConvertor.Convert10ToP(x, 16);
                Console.WriteLine("Converted number with base 16:{0}", x16); // A4
            }

            {
                string s = "01000011";
                Console.WriteLine("Initial number with base 2:{0}", s);
                var s10 = NumberConvertor.ConvertPTo10(s, 2);
                Console.WriteLine("Converted number with base 10:{0}", s10);
                var s8 = NumberConvertor.ConvertPToQ(s, 2, 8);
                Console.WriteLine("Converted number with base 8:{0}", s8);
                var s16 = NumberConvertor.ConvertPToQ(s, 2, 16);
                Console.WriteLine("Converted number with base 16:{0}", s16);
            }

            {
                string s = "95";
                Console.WriteLine("Initial number with base 16:{0}", s);
                var s10 = NumberConvertor.ConvertPTo10(s, 16);
                Console.WriteLine("Converted number with base 10:{0}", s10);
                var s8 = NumberConvertor.ConvertPToQ(s, 16, 8);
                Console.WriteLine("Converted number with base 8:{0}", s8);
                var s2 = NumberConvertor.ConvertPToQ(s, 16, 2);
                Console.WriteLine("Converted number with base 16:{0}", s2);
            }



            // 95


            Console.ReadLine();
        }
    }
}
