using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Collections;
using System.ComponentModel;

namespace RoslynCodeSearcher
{
    //Not a real Factory, in the sense that it doesn't return the Worker object to the caller.
    public static class WorkerFactory
    {
        private static List<Worker> _workerList = new List<Worker>();

        /// <summary>
        /// Create a new worker, add it to the workerlist and start it.
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <param name="exclude"></param>
        /// <param name="include"></param>
        /// <param name="guid"></param>
        public static void Start(SearchType searchType, string searchText, string exclude, string include, Guid guid)
        {
            Worker worker;

            worker = new Worker(searchType, searchText, exclude, include, guid);
            _workerList.Add(worker);

            worker.Start();
        }

        /// <summary>
        /// Select a worker from the workerlist with a certain Guid.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        private static Worker SelectWorker(Guid guid)
        {
            var selectWorker = from worker in _workerList
                               where worker.Guid == guid
                               select worker;

            if (selectWorker != null && selectWorker.Count() == 1)
            {
                return (Worker)selectWorker.First();
            }

            return null;
        }

        /// <summary>
        /// If a tab is deleted the accompanying worker must be cancelled.
        /// It won't be killed, but the results will not be written to a tab anymore.
        /// If it's not needed anymore, doesn't matter because they will be cleaned up once the program quits.
        /// </summary>
        /// <param name="guid">The unique identifier of the worker</param>
        public static void Delete(Guid guid)
        {
            Worker selectWorker = SelectWorker(guid);

            //Does the worker exist in the workerlist?
            //Because, if a tab is deleted, but a worker was not started for that tab,
            //there is no worker to delete.
            if (selectWorker != null)
            {
                selectWorker.Cancel();
                _workerList.Remove(selectWorker);
            }
        }
    }
}