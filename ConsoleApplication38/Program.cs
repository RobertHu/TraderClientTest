using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ConsoleApplication38
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo4();
        }

        static void Demo4()
        {
            var broadcastBlock = new BroadcastBlock<double>(null);
            broadcastBlock.Post(Math.PI);
            broadcastBlock.Post(2.3);
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(broadcastBlock.Receive());
            }
        }

        async static Task Demo3()
        {
            var bufferBlock = new BufferBlock<int>();
            for (int i = 0; i < 3; i++)
            {
                await bufferBlock.SendAsync(i);
            }

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(await bufferBlock.ReceiveAsync());
            }

        }

        static void Demo2()
        {
            var bufferBlock = new BufferBlock<int>();
            for (int i = 0; i < 3; i++)
            {
                bufferBlock.Post(i);
            }
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(bufferBlock.Receive());
            }
        }


        static void Demo1()
        {
            var throwIfNegative = new ActionBlock<int>(n =>
            {
                Console.WriteLine("n = {0}", n);
                if (n < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
            });
            throwIfNegative.Post(0);
            throwIfNegative.Post(-1);
            throwIfNegative.Post(1);
            throwIfNegative.Post(1);
            throwIfNegative.Post(-2);
            throwIfNegative.Complete();
            try
            {
                throwIfNegative.Completion.Wait();
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                    {
                        Console.WriteLine("Encountered {0}: {1}",
                            e.GetType().Name, e.Message);
                        return true;
                    });
            }
        }

    }

}
