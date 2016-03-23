using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TraderServerTest
{
    class Program
    {

        static void Main(string[] args)
        {

            //CustomerEvent.MyEventSource e1 = new CustomerEvent.MyEventSource();
            //e1.Started += (o,e) => Console.WriteLine(e.Message);
            //e1.Start();
            //e1.Start();
            //Console.Read();


            TcpListener tcpListener = new TcpListener(IPAddress.Any, 7777);
            tcpListener.Start();
            Agent.MsgAnalysis.Default.Start();
            System.Console.WriteLine("Server Started");
            ClientManager clientManager = new ClientManager();

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                clientManager.Add(client);
            }
        }
    }
}
