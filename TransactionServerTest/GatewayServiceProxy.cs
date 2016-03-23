using iExchange.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionServerTest
{
    internal sealed class GatewayServiceProxy : Protocal.Communication.ProxyBase<Protocal.IGatewayService>
    {
        internal static readonly GatewayServiceProxy Default = new GatewayServiceProxy();
        static GatewayServiceProxy() { }
        private GatewayServiceProxy() { }

        internal TransactionError Place(Protocal.TransactionData tranData)
        {
            try
            {
                if (!this.IsProxyInitialized())
                {
                    return TransactionError.RuntimeError;
                }
                return _proxy.Place(tranData);

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return TransactionError.RuntimeError;
            }
        }

        internal string GetInitializeData(List<Guid> accountIds)
        {
            try
            {
                if (!this.IsProxyInitialized())
                {
                    return string.Empty;
                }
                return _proxy.GetInitializeData(accountIds);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return string.Empty;
            }
        }

        public override void ServiceDiscoveryHandle(List<System.ServiceModel.EndpointAddress> addresses)
        {
            base.ServiceDiscoveryHandle(addresses);
            Logger.Info("Discovery gateway service");
            Console.WriteLine("Discovery gateway service");
        }


    }
}
