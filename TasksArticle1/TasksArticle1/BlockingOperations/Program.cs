using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace BlockingOperations
{
    /// <summary>
    /// This example shows that by using the main thread is blocked whenever it calls one of
    /// the Task trigger methods/properties, such as Wait() and the Result property
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            // create the task
            Task<List<int>> taskWithFactoryAndState1 = Task.Factory.StartNew<List<int>>((stateObj) =>
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                }
                return ints;
            }, 10000);


            taskWithFactoryAndState1.Wait();
            taskWithFactoryAndState1.Dispose();
            Console.WriteLine("I am only run AFTER the taskWithFactoryAndState has run");

            Stopwatch watch = new Stopwatch();
            watch.Start();
            // create the task
            Task<List<int>> taskWithFactoryAndState2 = Task.Factory.StartNew<List<int>>((stateObj) =>
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                    Thread.Sleep(10000);
                }
                return ints;
            }, 1);

            Console.WriteLine(string.Format("Waiting for taskWithFactoryAndState2, have waited {0}ms", 
                watch.ElapsedMilliseconds));

            
            var result = taskWithFactoryAndState2.Result;
            Console.WriteLine(string.Format("Finshed waiting for taskWithFactoryAndState2, have waited {0}ms", 
                watch.ElapsedMilliseconds));

            taskWithFactoryAndState2.Dispose();
            Console.WriteLine("I am only run AFTER the taskWithFactoryAndState has run");

            Console.WriteLine("All done, press Enter to Quit");

            Console.ReadLine();

        }
    }
}
