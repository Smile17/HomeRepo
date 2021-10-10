using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace L11_Files
{
    class ArrayFile
    {
        public double[] Vec { get; set; }
        #region Size of a ArrayFile
        public int Length { get { return this.Length; } }
        #endregion
        #region Constructor
        
        public ArrayFile(string path)
        {
            StreamReader inp = new StreamReader(path);
            var s = inp.ReadLine().Split(' ');
            Vec = new double[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                Vec[i] = Double.Parse(s[i]);
            }
            inp.Close();
        }
        #endregion
        public override string ToString()
        {
            return String.Join(' ', Vec);
        }
        public void Output(string path)
        {
            StreamWriter outp = new StreamWriter(path);
            outp.WriteLine(this);
            outp.Close();
        }
        public void MakeOperation()
        {
            int i = 0; int count = 0;
            for (i = 0; i < Vec.Length && count < 2; i++)
            {
                if (Vec[i] == 0) count++;
            }
            if (count == 2)
            {
                i--;
                // Move an array
                while (i > 0)
                {
                    Vec[i] = Vec[i - 1];
                    i--;
                }
                Vec[0] = 0;
            }
            else {
                Console.WriteLine("Not enough zeros");
            }
        }
        public static void Test()
        {
            var arr = new ArrayFile("input.txt");
            Console.WriteLine("Initial array: {0}", arr);
            arr.MakeOperation();
            arr.Output("output.txt");
            Console.WriteLine("Result array: {0}", arr);
            Console.ReadLine();
        }
    }
}
