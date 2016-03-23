using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpTraderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TradingService.TradingServiceClient client = new TradingService.TradingServiceClient();
            //client.Login("usd08", "12345678", "", "ENG", 1, "", "");
            //client.GetInitData("1", "652E6381-ED64-4AB7-A0BC-704C871D83A3");
            client.LoginOut("1");
        }
    }
}
