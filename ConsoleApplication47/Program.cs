using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication47
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal rate0 = -2;

            for (int i = 0; i < 5; i++)
            {
                decimal rate = 2;
                if (rate != rate0)
                {
                    Calculate(i, rate0 = rate);
                }
            }
        }

        private static decimal Calculate(int value, decimal rate)
        {
            return rate * value;
        }

    }


}
