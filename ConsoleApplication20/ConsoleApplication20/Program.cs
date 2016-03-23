using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication20
{
    class Session
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Session session1 = new Session { Name="xxxx" };
            Session session2 = session1;
            session2 = null;
            if (session1 == null)
            {
                Console.WriteLine("session1 is null");
            }
            else
            {
                Console.WriteLine("session1 is  not null");
            }
        }
    }
}
