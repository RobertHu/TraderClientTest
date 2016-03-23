using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpInDepth
{
    class Program
    {
        static void Main(string[] args)
        {
            if (5 == null)
            {
                Console.WriteLine("Ok");
            }

        }

        static void Display(Nullable<int> x)
        {
            Console.WriteLine("HasValue: {0}", x.HasValue);
            if (x.HasValue)
            {
                Console.WriteLine("Value: {0}", x.Value);
                Console.WriteLine("Explict convertion: {0}", (int)x);
            }
            Console.WriteLine("GetValueOrDefault(): {0}", x.GetValueOrDefault());
            Console.WriteLine("GetValueOrDefault(10): {0}", x.GetValueOrDefault(10));
            Console.WriteLine("ToString(): \"{0}\"", x.ToString());
            Console.WriteLine("GetHashCode(): {0}", x.GetHashCode());
        }
    }
}
