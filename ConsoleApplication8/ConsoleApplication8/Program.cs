using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;
namespace ConsoleApplication8
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadDemo demo = new ThreadDemo();
            demo.Call1();
            Console.Read();
        }
        static void ExecutionContentTest()
        {
            CallContext.LogicalSetData("name", "Robert");
            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("name={0}", CallContext.LogicalGetData("name")));
            ExecutionContext.SuppressFlow();
            ThreadPool.QueueUserWorkItem(state => Console.WriteLine("name={0}", CallContext.LogicalGetData("name")));
            ExecutionContext.RestoreFlow();
        }
        static void CancellSourceTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            ThreadPool.QueueUserWorkItem(o => Count(cts.Token, 1000));
            Console.WriteLine("Press any key to cancel");
            Console.ReadLine();
            cts.Cancel();

        }

        private static void Count(CancellationToken token, int countTo)
        {
            for (int count = 0; count < countTo; count++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Count is cancelled");
                    break;
                }
                Console.WriteLine(count);
                Thread.Sleep(200);
            }
            Console.WriteLine("Count is done");
        }
    }
}
