using Protocal.Physical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderClient.Physical
{
    internal static class DeliveryManager
    {
        internal static DeliveryRequestData CreateDeliveryRequest(Guid accountId, Guid instrumentId, Guid openOrderId, Guid changeCurrencyId)
        {
            var result = new DeliveryRequestData
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                InstrumentId = instrumentId,
                DeliveryAddressId = null,
                RequireLot = 1,
                ChargeCurrencyId = changeCurrencyId,
                Charge = 10m,
                RequireQuantity = 2,
                OrderRelations = new List<DeliveryRequestOrderRelationData>(),
                Specifications = new List<DeliveryRequestSpecificationData>()
            };

            result.OrderRelations.Add(new DeliveryRequestOrderRelationData
            {
                DeliveryRequestId = result.Id,
                OpenOrderId = openOrderId,
                DeliveryQuantity = 2,
                DeliveryLot = 1
            });

            result.Specifications.Add(new DeliveryRequestSpecificationData
            {
                Quantity = 2,
                Size = 2
            });

            return result;

        }
    }
}
