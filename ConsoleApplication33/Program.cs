using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication33
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"A", 1},
                {"B",2}
            };
            int target;
            dict.TryGetValue("B", out target);
        }
    }
}
