using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Protocal.Communication;

namespace TransactionServerTest
{
    internal sealed class Hoster
    {
        internal static readonly Hoster Default = new Hoster();

        private ServiceHost _commandCollectServiceHoster;
        private ServiceHost _gatewayWebServiceHoster;

        static Hoster() { }
        private Hoster() { }

        internal void Start()
        {
            _commandCollectServiceHoster = new ServiceHost(typeof(CommandCollectService));
            _commandCollectServiceHoster.AddDiscoveryFunction();
            _commandCollectServiceHoster.Open();

            _gatewayWebServiceHoster = new ServiceHost(typeof(GatewayWebService));
            _gatewayWebServiceHoster.Open();
        }

        internal void Stop()
        {
            _commandCollectServiceHoster.Close();
            _commandCollectServiceHoster = null;

            _gatewayWebServiceHoster.Close();
            _gatewayWebServiceHoster = null;
        }

    }
}
