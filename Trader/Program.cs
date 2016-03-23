using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Trader
{
    class Program
    {
        static void Main(string[] args)
        {
            Hoster.Default.Start();
            Console.WriteLine("Start up");
            Console.Read();
        }
    }
}
