using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace DiscoveryClientNameSpace
{
    [ServiceContract]
    internal interface ICalculateService
    {
        [OperationContract]
        int Add(int left, int right);
    }

    class Program
    {
        static EndpointAddress FindCalculatorServiceAddress()
        {
            // Create DiscoveryClient
            DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());

            // Find ICalculatorService endpoints            
            FindResponse findResponse = discoveryClient.Find(new FindCriteria(typeof(ICalculateService)));

            if (findResponse.Endpoints.Count > 0)
            {
                return findResponse.Endpoints[0].Address;
            }
            else
            {
                return null;
            }
        }



        static void InvokeCalculatorService(EndpointAddress endpointAddress)
        {
            // Create a client
            var binding = new WSHttpBinding();

            var channelFactory = new ChannelFactory<ICalculateService>(binding);
            var channel = channelFactory.CreateChannel(endpointAddress);
            // Connect to the discovered service endpoint

            Console.WriteLine("Invoking CalculatorService at {0}", endpointAddress);

            int value1 = 100;
            int value2 = 15;

            // Call the Add service operation.
            double result = channel.Add(value1, value2);
            Console.WriteLine("Add({0},{1}) = {2}", value1, value2, result);
            //Closing the client gracefully closes the connection and cleans up resources
        }



        static void Main(string[] args)
        {
            var address = FindCalculatorServiceAddress();
            InvokeCalculatorService(address);
            Console.Read();
        }
    }
}
