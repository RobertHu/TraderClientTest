using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication15
{
    class Program
    {
        static void Main(string[] args)
        {
            FaxEmail.FaxEmailService serivce = new FaxEmail.FaxEmailService();
            FaxEmail.BalanceInfo bi = new FaxEmail.BalanceInfo();
            bi.AccountID = Guid.Parse("6415FEB4-2258-4835-8E45-B3C540A8D897");
            bi.AccountCode = "Tests01";
            bi.Amount = 20.00;
            bi.ClientName = "Robert";
            bi.CurrencyCode = "HKD";
            bi.TradeDay = DateTime.Now;
            serivce.NotifyBalanceChanged(bi);
        }
    }
}
