using System;

namespace MathHelper.Algebra.Testing
{
    class PolynomTesting
    {
        public static void StrAsPoly()
        {
            Polynom poly = new Polynom(2,4,0,-3,2);
            Console.WriteLine(poly);
        }
    }
}
