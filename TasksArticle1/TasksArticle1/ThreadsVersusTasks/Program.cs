using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsVersusTasks
{
    /// <summary>
    /// Simple example showing Threads Versus Tasks
    /// where we spin up a number of Threads and run a simple
    /// loop in it, and we also do the same with Tasks
    /// 
    /// One thing to examine is the Windows Task Manager
    /// when these things are happening.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            //64 is upper limit for WaitHandle.WaitAll() method
            int maxWaitHandleWaitAllAllowed = 64;
            ManualResetEventSlim[] mres = new ManualResetEventSlim[maxWaitHandleWaitAllAllowed]; 

            for (int i = 0; i < mres.Length; i++)
            {
                mres[i] = new ManualResetEventSlim(false);
            }

            
            long threadTime = 0;
            long taskTime = 0;
            watch.Start();

            //start a new classic Thread and signal the ManualResetEvent when its done
            //so that we can snapshot time taken, and 

            for (int i = 0; i < mres.Length; i++)
            {
                int idx = i;
                Thread t = new Thread((state) =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.WriteLine(string.Format("Thread : {0}, outputing {1}",
                            state.ToString(), j.ToString()));
                    }
                    mres[idx].Set();
                });
                t.Start(string.Format("Thread{0}", i.ToString()));
            }

           
            WaitHandle.WaitAll( (from x in mres select x.WaitHandle).ToArray());


    
            threadTime = watch.ElapsedMilliseconds;
            watch.Reset();
   
            for (int i = 0; i < mres.Length; i++)
            {
                mres[i].Reset();
            }

            watch.Start();

            for (int i = 0; i < mres.Length; i++)
            {
                int idx = i;
                Task task = Task.Factory.StartNew((state) =>
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            Console.WriteLine(string.Format("Task : {0}, outputing {1}",
                                state.ToString(), j.ToString()));
                        }
                        mres[idx].Set();
                    }, string.Format("Task{0}", i.ToString()));
            }

            WaitHandle.WaitAll((from x in mres select x.WaitHandle).ToArray());
            taskTime = watch.ElapsedMilliseconds;
            Console.WriteLine("Thread Time waited : {0}ms", threadTime);
            Console.WriteLine("Task Time waited : {0}ms", taskTime);

            for (int i = 0; i < mres.Length; i++)
            {
                mres[i].Reset();
            }
            Console.WriteLine("All done, press Enter to Quit");

            Console.ReadLine();

        }
    }
}
