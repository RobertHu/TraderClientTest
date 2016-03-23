using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
           
            CaculateOneThread();
            Caculate1();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static void Caculate1()
        {
            int[] nums = Enumerable.Range(0, 10000000).ToArray();
            long total = 0;

            Stopwatch st = new Stopwatch();
            // Use type parameter to make subtotal a long, not an int
            st.Reset();
            st.Start();
            Parallel.For<long>(0, nums.Length, () => 0, (j, loop, subtotal) =>
            {
                subtotal += nums[j];
                return subtotal;
            },
                (x) => Interlocked.Add(ref total, x)
            );
            Console.WriteLine("The total is {0}", total);
            st.Stop();
            Console.WriteLine("Concurrent  Time:{0}",st.Elapsed.TotalMilliseconds);
        }

        static void CaculateOneThread()
        {
            int[] nums = Enumerable.Range(0, 10000000).ToArray();
            long total = 0;

            Stopwatch st = new Stopwatch();
            st.Reset();
            st.Start();
            foreach (var item in nums)
            {
                total += item;
            }
            Console.WriteLine("The total is {0}", total);
            st.Stop();
            Console.WriteLine("OneThread  Time:{0}", st.Elapsed.TotalMilliseconds);
        }

    }
}
