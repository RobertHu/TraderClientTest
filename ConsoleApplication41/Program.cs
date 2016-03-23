using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConsoleApplication41
{
    class Program
    {
        static void Main(string[] args)
        {
            var actionBlock = new TransformBlock<int,Task>(m =>{
                Console.WriteLine(string.Format("Inner {0}", m));
               return new Task(() => Console.WriteLine(m));
            });
            // Post several messages to the block. 
            for (int i = 0; i < 3; i++)
            {
                actionBlock.Post(i * 10);
            }
            var actionBlock2 = new ActionBlock<Task>(m => m.Start());
            actionBlock.LinkTo(actionBlock2);
            Console.Read();
        }
    }
}
