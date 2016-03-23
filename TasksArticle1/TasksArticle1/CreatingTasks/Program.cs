using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatingTasks
{
    /// <summary>
    /// This demo shows how to create various tasks
    /// using tasks that return values and take in state
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {

            // *****************************************************************
            // OPTION 1 : Create a Task using an inline action
            // *****************************************************************
            Task<List<int>> taskWithInLineAction = new Task<List<int>>(() =>
                {
                    List<int> ints = new List<int>();
                    for (int i = 0; i < 1000; i++)
                    {
                        ints.Add(i);
                    }
                    return ints;
                    
                });


            // *****************************************************************
            // OPTION 2 : Create a Task that calls an actual method that returns a string
            // *****************************************************************
            Task<string> taskWithInActualMethodAndState = 
                new Task<string>(new Func<object, string>(PrintTaskObjectState),
                "This is the Task state, could be any object");

            // *****************************************************************
            // OPTION 3 : Create and start a Task that returns List<int> using Task.Factory
            // *****************************************************************
            Task<List<int>> taskWithFactoryAndState = Task.Factory.StartNew<List<int>>((stateObj) =>
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                }
                return ints;
            }, 2000);



            taskWithInLineAction.Start();
            taskWithInActualMethodAndState.Start();



            //wait for all Tasks to finish
            Task.WaitAll(new Task[] 
            { 
                taskWithInLineAction, 
                taskWithInActualMethodAndState, 
                taskWithFactoryAndState 
            });

            //print results for taskWithInLineAction
            var taskWithInLineActionResult = taskWithInLineAction.Result;
            Console.WriteLine(string.Format(
                "The task with inline Action<T> returned a Type of {0}, with {1} items",
            taskWithInLineActionResult.GetType(), taskWithInLineActionResult.Count));
            taskWithInLineAction.Dispose();

            //print results for taskWithInActualMethodAndState
            var taskWithInActualMethodResult = taskWithInActualMethodAndState.Result;
            Console.WriteLine(string.Format(
                "The task which called a Method returned '{0}'",
            taskWithInActualMethodResult.ToString()));
            taskWithInActualMethodAndState.Dispose();

            //print results for taskWithFactoryAndState
            var taskWithFactoryAndStateResult = taskWithFactoryAndState.Result;
            Console.WriteLine(string.Format(
                "The task with Task.Factory.StartNew<List<int>> returned a Type of {0}, with {1} items",
            taskWithFactoryAndStateResult.GetType(), taskWithFactoryAndStateResult.Count));
            taskWithFactoryAndState.Dispose();

            Console.WriteLine("All done, press Enter to Quit");
        
            Console.ReadLine();
        }


        private static string PrintTaskObjectState(object state)
        {
            Console.WriteLine(state.ToString());
            return "***WOWSERS***";
        }
    }
}
