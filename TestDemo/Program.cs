using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isequal = (243 & (int)Math.Pow(2, 0)) == Math.Pow(2, 0);

            //DoParalle();
            ////DoUseTask();
            //Console.Read();

        }

        static void DoParalle()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = -1;

            int minWorker, minIOC;
            ThreadPool.GetMinThreads(out minWorker, out minIOC);
            ThreadPool.SetMinThreads(minWorker * 128, minIOC * 128);

            int maxWorker, maxIOC;
            ThreadPool.GetMaxThreads(out maxWorker, out maxIOC);
            ThreadPool.SetMaxThreads(maxWorker * 8, maxIOC * 8);

            Parallel.For(1, 3000, options ,i =>
                {
                    long result = 0;
                    for (int j = 1; j < 1000000; j++)
                    {
                        result *= j;
                    }
                });
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        static void DoUseTask()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            //TaskScheduler.Current.MaximumConcurrencyLevel = 
            List<Task> taskList = new List<Task>(3000);
            for (int i = 0; i < 3000; i++)
            {
                Task task = new Task(() =>
                    {
                        Thread.Sleep(5);
                    });

                task.Start();
                taskList.Add(task);
            }
            Task.WaitAll(taskList.ToArray());
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        static void DoUseThread()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            List<ManualResetEvent> eventList = new List<ManualResetEvent>(3000);
            for (int i = 0; i < 3000; i++)
            {
                ManualResetEvent resetEvent = new ManualResetEvent(false);
                eventList.Add(resetEvent);
                Thread thread = new Thread(obj =>
                    {
                        ManualResetEvent event2 = (ManualResetEvent)obj;
                        Thread.Sleep(5);
                        event2.Set();
                    });
                thread.Start(resetEvent);
            }
            WaitHandle.WaitAll(eventList.ToArray());
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

    }
}
