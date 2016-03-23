using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandlingByReadingTaskValues
{
    /// <summary>
    /// This demo shows ignores the initial try/catch and instead reads the
    /// Task values using the Task.IsFaulted / Task.Exception, and then 
    /// using that to handle the Exceptions
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // create the task
            Task<List<int>> taskWithFactoryAndState = 
                Task.Factory.StartNew<List<int>>((stateObj) =>
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                    if (i > 100)
                    {
                        InvalidOperationException ex = 
                            new InvalidOperationException("oh no its > 100");
                        ex.Source = "taskWithFactoryAndState";
                        throw ex;
                    }
                }
                return ints;
            }, 2000);

            try
            {
                taskWithFactoryAndState.Wait();
                if (!taskWithFactoryAndState.IsFaulted)
                {
                    Console.WriteLine(string.Format("managed to get {0} items", 
                        taskWithFactoryAndState.Result.Count));
                }
            }
            catch (AggregateException aggEx)
            {
                //do nothing
            }
 
            //so just read the Exception from the Task, if its in Faulted state
            if (taskWithFactoryAndState.IsFaulted)
            {
                AggregateException taskEx = taskWithFactoryAndState.Exception;
                foreach (Exception ex in taskEx.InnerExceptions)
                {
                    Console.WriteLine(string.Format("Caught exception '{0}'", ex.Message));
                }

            }

            //All done with Task now so Dispose it
            taskWithFactoryAndState.Dispose();

            Console.WriteLine("All done, press Enter to Quit");
            Console.ReadLine();
        }
    }
}
