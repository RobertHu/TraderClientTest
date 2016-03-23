using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CLRVIACsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var thread = new Thread(Worker);
            thread.IsBackground = true;
            thread.Start();
        }

        private static void Worker()
        {
            Thread.Sleep(10000);
            Console.WriteLine("Returnning from worker");
        }
    }
}
