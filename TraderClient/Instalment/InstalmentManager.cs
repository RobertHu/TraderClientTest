using Protocal.Physical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderClient.Instalment
{
    internal static class InstalmentManager
    {
        internal static void PrePay(Protocal.Test.IServerService serverService)
        {
            Guid submitorId = Guid.Parse("52213886-68b0-4edf-8c75-04acd6e7e474");
            Guid accountId = Guid.Parse("a11aabb6-2dc2-42d5-a914-00928cd5fee8");
            Guid currencyId = Guid.Parse("0da665b5-9aa5-49d7-a301-048f1428ca4a");
            decimal sumSourcePaymentAmount = 40m;
            decimal sumSourceTerminateFee = 100m;
            TerminateData terminateData = new TerminateData
            {
                OrderId = Guid.Parse("24d69b98-3b02-4658-9836-b31a8bc2534b"),
                TerminateFee = 100m,
                Amount = 40m,
                SourceTerminateFee = 100m,
                SourceCurrencyId = Guid.Parse("0da665b5-9aa5-49d7-a301-048f1428ca4a"),
                SourceAmount = 40m,
                IsPayOff = true,
                CurrencyRate = 1m
            };
            serverService.InstalmentPayoff(Guid.NewGuid(), accountId, currencyId, sumSourcePaymentAmount, sumSourceTerminateFee, terminateData);
        }

    }
}
