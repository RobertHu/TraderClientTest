using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TraderClient.TransactionBLL;
using TraderClient.TypeExtensions;
using iExchange.Common;

namespace TraderClient.OldTransactionServerTest
{
    internal static class StateServerTest
    {
        public static void TestSportOrder(Guid accountId, Guid instrumentId, Guid customerId, string setPrice)
        {
            var tran = TransactionManager.Default.CreateSpotTran(accountId, instrumentId, setPrice, 2, true, customerId);
            XElement tranNode = TradingEngine.Order.toXmlTran(tran);
            StateServerService.Service service = new StateServerService.Service();
            var token = new Token(customerId, UserType.Customer, AppType.Mobile);
            string tranCode;
            service.Place(token, tranNode.ToXmlNode(), out tranCode);
        }

        public static void TestGetInitData(Guid accountId)
        {
            StateServerService.Service service = new StateServerService.Service();
            var initData = service.GetAccountsForInit(new Guid[] { accountId });
        }

    }
}
