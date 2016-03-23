using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleUsingExHandle
{
    /// <summary>
    /// This demo shows how to use the AggregateException.Handle() method
    /// within a try/catch
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            // create the task
            Task<List<int>> taskWithFactoryAndState = Task.Factory.StartNew<List<int>>((stateObj) =>
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
                aggEx.Handle(HandleException);
            }
            finally
            {
                taskWithFactoryAndState.Dispose();
            }

            Console.WriteLine("All done, press Enter to Quit");

            Console.ReadLine();

        }



        private static bool HandleException(Exception ex)
        {
            if (ex is InvalidOperationException)
            {
                Console.WriteLine(string.Format("Caught exception '{0}'", ex.Message));
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
