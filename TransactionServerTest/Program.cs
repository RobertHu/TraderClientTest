using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace TransactionServerTest
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            Facade.Default.Start();
            Hoster.Default.Start();
            Logger.Info("Started");
            Console.WriteLine("TransactionServerTest Start");
            Console.Read();
        }
    }
}
