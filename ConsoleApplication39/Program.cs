using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication39
{
    class Test
    {
        public static string x = EchoAndReturn("In type initializer");
        static Test() { }
        public static string EchoAndReturn(string s)
        {
            Console.WriteLine(s);
            return s;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Main");
            Test.EchoAndReturn("Echo!");
            Console.WriteLine("After echo");
            string y = Test.x;
            if (y != null)
            {
                Console.WriteLine("After field access");
            }
        }
    }
}
