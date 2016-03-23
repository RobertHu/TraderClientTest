using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication22
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = "source";
            Modify(out source);
            Console.Out.WriteLine(source);
        }
        static void Modify(out string source)
        {
            source = "modified";
        }
    }
}
