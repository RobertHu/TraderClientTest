using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpUnsafe
{
    class Program
    {
        static void Main(string[] args)
        {
            unsafe
            {
                int* p = stackalloc int[10];
                for (int i = 0; i < 10; i++)
                {
                    p[i] = 2 * i + 10;
                }
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("{0}", p[i]);
                }
            }
            Console.ReadKey();

        }
    }
}
