using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionServerTest
{
    internal sealed class Facade
    {
        internal static readonly Facade Default = new Facade();
        private Protocal.ServiceFinder<Protocal.IGatewayService> _gateWayServiceFinder;

        static Facade() { }
        private Facade() { }


        internal void Start()
        {
            SettingManager.Default.LoadSettings();
            _gateWayServiceFinder = new Protocal.ServiceFinder<Protocal.IGatewayService>(SettingManager.Default.GatewayServiceScopes);
            _gateWayServiceFinder.ServiceDiscovied += GatewayServiceProxy.Default.ServiceDiscoveryHandle;
            _gateWayServiceFinder.Start();
        }

        internal void Stop()
        {
            _gateWayServiceFinder.ServiceDiscovied -= GatewayServiceProxy.Default.ServiceDiscoveryHandle;
            _gateWayServiceFinder.Stop();
            _gateWayServiceFinder = null;
        }


    }
}
