using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayAgent
{
    class Program
    {
        private static Protocal.IGatewayService _gateWayService;

        static void Main(string[] args)
        {
            Hoster.Default.Start();
            _gateWayService = Protocal.ChannelFactory.CreateHttpChannel<Protocal.IGatewayService>("http://ws0210:5060/GatewayService");
           //_gateWayService.Register("net.tcp://ws0210:5444/CommandCollectService", iExchange.Common.AppType.CppTrader);
            Console.WriteLine("Start");
            Console.Read();
        }
    }
}
