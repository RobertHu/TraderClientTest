using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace MakeOrderTester
{
    class Program
    {
        static Regex AccountItemRegex=new Regex(@"\s+");
        static void Main(string[] args)
        {
            SettingManager.Default.Initialize();
            if (SettingManager.Default.IsTestStateServer)
            {
                StateServer.Init();
            }
            OrderStatistics.Default.Start();
            foreach (var account in GenerateAccounts())
            {
                TestDemo demo = new TestDemo(account,SettingManager.Default.IsTestStateServer);
                demo.Start();
            }
            Console.ReadKey();
        }

        static IEnumerable<Account> GenerateAccounts()
        {
            string[] lines = File.ReadAllLines("account.txt");
            foreach (var line in lines)
            {
                string[] items = AccountItemRegex.Split(line);
                yield return new Account(items[0], items[1], items[2]);
            }
        }

    }
}
