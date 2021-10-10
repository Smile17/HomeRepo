using System;
using System.IO;

namespace Shein
{
    public class Estimator
    {
        public static void Estimate(Action method, StreamWriter outp, string name = "")
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            method();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            if(name != "")
                outp.WriteLine($"{name}: {elapsedMs}");
            else
                outp.WriteLine($"{elapsedMs}");
        }
        public static long Estimate(Action method)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            method();
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }
        

        
    }
}
