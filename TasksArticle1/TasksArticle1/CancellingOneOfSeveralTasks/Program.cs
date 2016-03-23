using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CancellingOneOfSeveralTasks
{
    /// <summary>
    /// Shows how to cancel one Task out of several running tasks
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            CancellationTokenSource tokenSource1 = new CancellationTokenSource();
            CancellationToken token1 = tokenSource1.Token;

            Task<List<int>> taskWithFactoryAndState1 = 
                Task.Factory.StartNew<List<int>>((stateObj) =>
            {
                
                List<int> ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                    token1.ThrowIfCancellationRequested();
                    Console.WriteLine("taskWithFactoryAndState1, creating Item: {0}", i);
                }
                return ints;
            }, 2000, token1);



            CancellationTokenSource tokenSource2 = new CancellationTokenSource();
            CancellationToken token2 = tokenSource2.Token;

            Task<List<int>> taskWithFactoryAndState2 = 
                Task.Factory.StartNew<List<int>>((stateObj) =>
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                    token2.ThrowIfCancellationRequested();
                    Console.WriteLine("taskWithFactoryAndState2, creating Item: {0}", i);
                }
                return ints;
            }, 15, token2);


            // cancel the 1st token source
            tokenSource1.Cancel();

            //examine taskWithFactoryAndState1
            try
            {
                Console.WriteLine("taskWithFactoryAndState1 cancelled? {0}",
                    taskWithFactoryAndState1.IsCanceled);

                //we did not cancel taskWithFactoryAndState1, so print it's result count
                Console.WriteLine("taskWithFactoryAndState1 results count {0}",
                    taskWithFactoryAndState1.Result.Count);

                Console.WriteLine("taskWithFactoryAndState1 cancelled? {0}",
                    taskWithFactoryAndState1.IsCanceled);
            }
            catch (AggregateException aggEx1)
            {
                PrintException(taskWithFactoryAndState1, aggEx1, "taskWithFactoryAndState1");
            }


            //examine taskWithFactoryAndState2
            try
            {
                Console.WriteLine("taskWithFactoryAndState2 cancelled? {0}",
                    taskWithFactoryAndState2.IsCanceled);

                //we did not cancel taskWithFactoryAndState2, so print it's result count
                Console.WriteLine("taskWithFactoryAndState2 results count {0}",
                    taskWithFactoryAndState2.Result.Count);

                Console.WriteLine("taskWithFactoryAndState2 cancelled? {0}",
                    taskWithFactoryAndState2.IsCanceled);
            }
            catch (AggregateException aggEx2)
            {
                PrintException(taskWithFactoryAndState2, aggEx2, "taskWithFactoryAndState2");
            }

            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();

        }


        private static void PrintException(Task task, AggregateException agg, string taskName)
        {
            foreach (Exception ex in agg.InnerExceptions)
            {
                Console.WriteLine(string.Format("{0} Caught exception '{1}'", taskName, ex.Message));
            }
            Console.WriteLine("{0} cancelled? {1}",taskName, task.IsCanceled);
        }

    }
}
