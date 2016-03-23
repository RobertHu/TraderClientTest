using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace UncaughtExceptionInWinFormsDemoApp
{
    /// <summary>
    /// This example shows an uncaught Exception from a Task within a WinForms app
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartTask_Click(object sender, EventArgs e)
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
                        InvalidOperationException ex = new InvalidOperationException("oh no its > 100");
                        ex.Source = "taskWithFactoryAndState";
                        throw ex;
                    }
                }
                return ints;
            }, 2000);

            //wait on the task, but do not use Wait() method
            //doing it this way will cause aany unhandled Exception to remain unhandled
            while (!taskWithFactoryAndState.IsCompleted)
            {
                Thread.Sleep(500);
            }

            if (!taskWithFactoryAndState.IsFaulted)
            {
                lstResults.DataSource = taskWithFactoryAndState.Result;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                AggregateException taskEx = taskWithFactoryAndState.Exception;
                foreach (Exception ex in taskEx.InnerExceptions)
                {
                    sb.AppendLine(string.Format("Caught exception '{0}'", ex.Message));
                }
                MessageBox.Show(sb.ToString());
            }

            //All done with Task now so Dispose it
            taskWithFactoryAndState.Dispose();

        }
}
}
