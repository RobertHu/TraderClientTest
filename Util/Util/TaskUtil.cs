using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Util
{
   public static class TaskUtil
    {
        public static Task TaskHelper<T>(Func<T> mainLogin, Action<Task<T>> continueAction)
        {
            Task task = Task.Factory.StartNew<T>(mainLogin)
                      .ContinueWith(continueAction, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }

        public static Task TaskHelper(Action mainLogin, Action<Task> continueAction)
        {
            Task task = Task.Factory.StartNew(mainLogin)
                      .ContinueWith(continueAction, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }
        public static Task TaskHelper(Action<object> mainLogin, Action<Task> continueAction, object state)
        {
            Task task = Task.Factory.StartNew(mainLogin, state)
                      .ContinueWith(continueAction, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            return task;

        }

        public static Task TaskHelper<T>(Func<object, T> mainLogin, Action<Task<T>> continueAction, object state)
        {
            Task task = Task.Factory.StartNew<T>(mainLogin, state)
                      .ContinueWith(continueAction, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
            return task;
        }
    }
}
