using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication9
{
    class Program
    {
        static void Main(string[] args)
        {

            Module1.Publisher pub = new Module1.Publisher("Robert");
            pub.Interest += (o,m) => Console.WriteLine(m);
            pub.Publish("Hello");

            Module1.SampingAgent agent = new Module1.SampingAgent();
            agent.Start();
            agent.Notified += (o, s) => Console.WriteLine(s);
            agent.Post("http://www.baidu.com");
            //agent.Post("http://localhost/FaxAndEmailService/StartupService.asmx");
            Console.ReadKey();
        }

        static void Subscriber(string content)
        {
            System.Console.WriteLine(content);
        }
    }
}
