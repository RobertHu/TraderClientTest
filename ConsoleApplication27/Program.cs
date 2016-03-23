using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication27
{
    class Demo
    {
        public Demo(string msg)
        {
            this.Msg = msg;
        }

        public string Msg { get; private set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Demo demo = null;
            Initialize(ref demo);
            Console.Out.WriteLine(demo.Msg);
        }
        static void Initialize(ref Demo demo)
        {
            demo = new Demo("robert");
        }

    }
}
