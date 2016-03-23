using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace DiscoveryServer
{

    [ServiceContract]
    internal interface ICalculateService
    {
        [OperationContract]
        int Add(int left, int right);
    }

    internal sealed class CalculateService : ICalculateService
    {
        public int Add(int left, int right)
        {
            return left + right;
        }
    }






    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri(string.Format("http://{0}:8000/discovery/scenarios/calculatorservice/{1}/", System.Net.Dns.GetHostName(), Guid.NewGuid().ToString()));

            // Create a ServiceHost for the CalculatorService type.
            using (ServiceHost serviceHost = new ServiceHost(typeof(CalculateService), baseAddress))
            {
                // add calculator endpoint
                serviceHost.AddServiceEndpoint(typeof(ICalculateService), new WSHttpBinding(), string.Empty);

                // ** DISCOVERY ** //
                // make the service discoverable by adding the discovery behavior
                serviceHost.Description.Behaviors.Add(new ServiceDiscoveryBehavior());

                // ** DISCOVERY ** //
                // add the discovery endpoint that specifies where to publish the services
                serviceHost.AddServiceEndpoint(new UdpDiscoveryEndpoint());

                // Open the ServiceHost to create listeners and start listening for messages.
                serviceHost.Open();

                // The service can now be accessed.
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.ReadLine();
            }

        }
    }
}
