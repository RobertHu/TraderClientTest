using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication13
{
    class Program
    {
        static void Main(string[] args)
        {
            double input = 155555.34;
            var target = string.Format("{0:n2}", input);
            Console.WriteLine(target);
            Console.Read();
        }

        static void NotifyRiskLevelChanged()
        {
            FaxEmailService.RiskLevelChangedInfo info = new FaxEmailService.RiskLevelChangedInfo();
            info.AccountID = new Guid("C61428B3-518F-4EC6-A098-4080DBD28420");
            info.ClientName = "ts8";
            info.AccountName = "ts88";
            info.CurrencyCode = "HKD";
            info.Equity = 1254.56;
            info.InitialMargin = 12456.12;
            info.RiskLevel = 2;
            info.TradeDay = DateTime.Now;
            FaxEmailService.FaxEmailService service = new FaxEmailService.FaxEmailService();
            service.NotifyRiskLevelChanged(info);
        }

        static void NotifyPwdReset()
        {
            FaxEmailService.FaxEmailService service = new FaxEmailService.FaxEmailService();
            FaxEmailService.PasswordResetInfo info = new FaxEmailService.PasswordResetInfo();
            info.CustomerID = new Guid("13A399B2-F6C1-4494-8C26-EE225F20C592");
            info.LoginName = "李探花";
            info.Password = "forget";
            service.NotifyPasswordReset(info);
        }
    }
}
