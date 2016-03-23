using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;

namespace RoslynCodeSearcher
{
    public class Worker
    {
        private CodeSearcher _searcher;
        private BackgroundWorker _worker;
        private string _result;
        private Guid _guid;
        private bool _cancel;

        /// <summary>
        /// Constructor for Worker that instantiates a backgroundworker to do the work in a separate thread.
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <param name="exclude"></param>
        /// <param name="include"></param>
        /// <param name="guid"></param>
        public Worker(SearchType searchType, string searchText, string exclude, string include, Guid guid)
        {
            _guid = guid;
            _searcher = new CodeSearcher(searchType, searchText, exclude, include);

            _worker = new BackgroundWorker();
            _worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

        public void Start()
        {
            //Write an empty text to the tab so if there was any old code in the tab
            //it will be deleted. Not an empty text but a single space,
            //This is on purpose otherwise the text "Nothing found." will be shown and
            //we don't want that.
            TabController.WriteResults(_guid, " ");
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// Cancel means the backgroundworker will finish it's job,
        /// but won't write the results to the tabcontroller anymore.
        /// </summary>
        public void Cancel()
        {
            _cancel = true;
        }

        /// <summary>
        /// If the worker is done, write the results to the Tab where the search was started.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!_cancel)
            {
                TabController.WriteResults(_guid, _result);
                TabController.HideHourGlass(_guid);
            }
        }

        /// <summary>
        /// Search the sourcecode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
           _result = _searcher.Search();
        }
    }
}