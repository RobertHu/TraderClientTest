using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionServerTest
{
    public sealed class GatewayWebService: Protocal.IGatewayService
    {
        public string GetInitializeData(List<Guid> accountIds)
        {
            return GatewayServiceProxy.Default.GetInitializeData(accountIds);
        }

        public iExchange.Common.TransactionError Place(Protocal.TransactionData transaction)
        {
            return GatewayServiceProxy.Default.Place(transaction);
        }

        public string Test()
        {
            throw new NotImplementedException();
        }
    }
}
