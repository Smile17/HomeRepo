using System;
using System.IO;

namespace Recursion
{
    class Timer
    {
        public static Tuple<long, long> CallTheMethod(Action toCall)
        {
            GC.TryStartNoGCRegion(200000000);
            Console.WriteLine("==========Start============");
            var byte_num_start = GC.GetTotalMemory(false);
            var watch = System.Diagnostics.Stopwatch.StartNew();

            toCall.Invoke();

            watch.Stop();
            var elapsedMs = watch.ElapsedTicks;
            Console.WriteLine(elapsedMs);
            var byte_num = ( GC.GetTotalMemory(false) - byte_num_start );
            Console.WriteLine(byte_num);
            Console.WriteLine("===========End=============");
            GC.EndNoGCRegion();
            GC.Collect();
            return new Tuple<long, long>(elapsedMs, byte_num);
        }
        public static void RunExperimentForOneRecord(StreamWriter outp, params Action[] actions)
        {
            
            for(int i = 0; i < actions.Length; i++)
            {
                var res = CallTheMethod(actions[i]);
                outp.Write(res.Item1 + ";" + res.Item2 + ";");
            }
        }
    }
}
