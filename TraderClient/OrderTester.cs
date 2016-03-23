using Protocal.Physical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderClient.OrderBLL;

namespace TraderClient
{
    internal static class OrderTester
    {
        internal static Protocal.TransactionData CreatePhysicalInstalmentTran(Guid accountId, Guid instrumentId, string price, decimal lot, bool isOpen, bool isBuy, iExchange.Common.PhysicalTradeSide tradeSide,Guid submitorId)
        {
            var instalmentParameter = new InstalmentParameter(Guid.Parse("0817D587-C929-4F77-BCC7-38F2B8ACD793"), 0, iExchange.Common.InstalmentType.EqualPrincipal, iExchange.Common.InstalmentFrequence.Month, iExchange.Common.RecalculateRateType.NextMonth, 2);
            return TransactionBLL.TransactionManager.Default.CreateOpenPhysicalInstalmentSpotTran(accountId, instrumentId, price, lot, isBuy, tradeSide, PhysicalType.Instalment, instalmentParameter, submitorId);
        }

        internal static Protocal.TransactionData CreatePhysicalPrePayTran(Guid accountId, Guid instrumentId, string price, decimal lot, bool isOpen, bool isBuy, iExchange.Common.PhysicalTradeSide tradeSide,Guid submitorId)
        {
            var instalmentParameter = new InstalmentParameter(Guid.Parse("0817d587-c929-4f77-bcc7-38f2b8acd793"), 10m, iExchange.Common.InstalmentType.EqualInstallment, iExchange.Common.InstalmentFrequence.TillPayoff, iExchange.Common.RecalculateRateType.NextMonth, 1);
            return TransactionBLL.TransactionManager.Default.CreateOpenPhysicalInstalmentSpotTran(accountId, instrumentId, price, lot, isBuy, tradeSide, PhysicalType.PrePayment, instalmentParameter, submitorId);
        }
    }
}
