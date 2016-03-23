using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication36
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> task = new Task<int>(() => 5);
            task.Start();
            
        }
    }
}
