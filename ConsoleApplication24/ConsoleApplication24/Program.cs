using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication24
{
    class Program
    {
        static void Main(string[] args)
        {
            float f1 = 0.01f;
            double d1 = 0.01d;
            bool isgreat = f1 < d1;
            Console.WriteLine(isgreat);
        }
    }
}
