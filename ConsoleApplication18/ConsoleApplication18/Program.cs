using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication18
{

    class Program
    {
        static List<byte> _MyList = new List<byte>();
        static void Main(string[] args)
        {
            Worker worker = new Worker();
            worker.Do1();
        }
    }

    class Worker
    {
        private object _myLock = new object();
        public void Do1()
        {
            lock (this._myLock)
            {
                Console.WriteLine("do 1");
                Do2();
            }
        }

        public void Do2()
        {
            lock (this._myLock)
            {
                Console.WriteLine("do 2");
            }
        }
    }
}
