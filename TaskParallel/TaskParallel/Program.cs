using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace TaskParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            int maxCount = 64;
            ManualResetEventSlim[] mres = new ManualResetEventSlim[maxCount];
            Stopwatch sw = new Stopwatch();
            for (int i = 0; i < maxCount; i++)
            {
                mres[i] = new ManualResetEventSlim(false);
            }
            long threadTime;
            long taskTime;
            for (int i = 0; i < maxCount; i++)
            {
                int index = i;
                Thread t= new Thread(s =>
                {
                    //Console.WriteLine(string.Format()
                });
            }
        }
    }
}
