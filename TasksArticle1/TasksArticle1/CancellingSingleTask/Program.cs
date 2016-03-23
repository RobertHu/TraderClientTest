using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CancellingSingleTask
{
    /// <summary>
    /// This example shows how to cancel a single Task
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // create the cancellation token source
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            // create the cancellation token
            CancellationToken token = tokenSource.Token;

            // create the task
            Task<List<int>> taskWithFactoryAndState = Task.Factory.StartNew<List<int>>((stateObj) =>
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                    token.ThrowIfCancellationRequested();
                    Console.WriteLine("taskWithFactoryAndState, creating Item: {0}", i);
                }
                return ints;
            }, 2000, token);



            // write out the cancellation detail of each task
            Console.WriteLine("Task cancelled? {0}", taskWithFactoryAndState.IsCanceled);

            // cancel the second token source
            tokenSource.Cancel();

            if (!taskWithFactoryAndState.IsCanceled && !taskWithFactoryAndState.IsFaulted)
            {
                //since we want to use one of the Trigger method (ie Result), we must catch
                //any AggregateException that occurs
                try
                {
                    if (!taskWithFactoryAndState.IsFaulted)
                    {
                        Console.WriteLine(string.Format("managed to get {0} items", 
                            taskWithFactoryAndState.Result.Count));
                    }
                }
                catch (AggregateException aggEx)
                {
                    foreach (Exception ex in aggEx.InnerExceptions)
                    {
                        Console.WriteLine(string.Format("Caught exception '{0}'", ex.Message));
                    }
                }
                finally
                {
                    taskWithFactoryAndState.Dispose();
                }
            }
            else
            {
                Console.WriteLine("Task cancelled? {0}", taskWithFactoryAndState.IsCanceled);

            }


            // wait for input before exiting
            Console.WriteLine("Main method complete. Press enter to finish.");
            Console.ReadLine();
        }
    }
}
