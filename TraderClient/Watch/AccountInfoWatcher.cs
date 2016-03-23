using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TraderClient.Watch
{
    internal sealed class AccountInfoWatcher
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AccountInfoWatcher));
        internal static readonly AccountInfoWatcher Default = new AccountInfoWatcher();

        private Guid[] _accounts;

        static AccountInfoWatcher() { }
        private AccountInfoWatcher()
        {
            _accounts = new Guid[]
            {
                Guid.Parse("EF873395-36AD-4C94-B20C-D79119059D0D")
            };
        }


        internal void Start()
        {
            Thread thread = new Thread(this.Watch);
            thread.IsBackground = true;
            thread.Start();
        }

        private void Watch()
        {
            while (true)
            {
                //using (TransactionService.ServerServiceClient client = new TransactionService.ServerServiceClient())
                //{
                //    string initData = client.GetInitData(_accounts);
                //    Logger.Info(initData);
                //}
                Thread.Sleep(10 * 60 * 1000);
            }
        }


    }
}
