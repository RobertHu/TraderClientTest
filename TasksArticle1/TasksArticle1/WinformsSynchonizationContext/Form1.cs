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

namespace WinformsSynchonizationContext
{
    /// <summary>
    /// This demo shows how to use the Winforms 
    /// SynchronizationContext when working with Tasks
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDoIt_Click(object sender, EventArgs e)
        {
            Task taskWithFactoryAndState1 = Task.Factory.StartNew<List<int>>((stateObj) =>
            {
                // This is not run on the UI thread.
                List<int> ints = new List<int>();
                for (int i = 0; i < (int)stateObj; i++)
                {
                    ints.Add(i);
                }
                return ints;
            }, 10000).ContinueWith(ant =>
            {
                //updates UI no problem as we are using correct SynchronizationContext
                lstBox.DataSource = ant.Result;
            }, TaskScheduler.FromCurrentSynchronizationContext());


        }
    }
}
