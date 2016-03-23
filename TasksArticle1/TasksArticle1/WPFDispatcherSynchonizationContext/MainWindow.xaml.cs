using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;

namespace WPFDispatcherSynchonizationContext
{
    /// <summary>
    /// This demo shows how to use the WPF Dispatcher 
    /// SynchronizationContext when working with Tasks
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDoIt_Click(object sender, RoutedEventArgs e)
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
                lstBox.ItemsSource = ant.Result;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
