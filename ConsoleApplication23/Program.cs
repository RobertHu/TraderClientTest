using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication23
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> items = new List<string>
            {
                "One"
            };
            var target = items.Aggregate((s1, s2) =>
            {
                if (s1.Length < s2.Length)
                {
                    return s2;
                }
                return s1;
            });
            Console.WriteLine(target);
        }
    }
}
