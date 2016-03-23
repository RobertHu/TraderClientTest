using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderClient.Reset
{
    internal sealed class ResetManager
    {
        internal static readonly ResetManager Default = new ResetManager();

        static ResetManager() { }
        private ResetManager() { }

        internal void DoReset(Protocal.Test.IServerService serverService)
        {
            Guid instrumentId = Guid.Parse("2E42C798-97E7-4702-AFBA-0E6ABA0575D6");
            DateTime tradeDay = DateTime.Now.Date;
            string ask = "1.21";
            string bid = "1.22";
        }

        internal void DoAccountSystemReset(Protocal.Test.IServerService serverService)
        {
            var accountId = Guid.Parse("606D464F-4B7A-4481-97D0-00CF23C8DAE9");
            var tradeDay = new DateTime(2015, 10, 30);
            serverService.DoAccountSystemReset(accountId, tradeDay);
        }

    }
}
