using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TraderClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int testCount = 5000;
            ClientManager manager = new ClientManager();
            Agent.MsgAnalysis.Default.Start();
            for (int i = 1; i <= testCount; i++)
            {
                TcpClient client = new TcpClient("10.2.10.1", 7777);
                manager.Add(client);
            }
            Console.Read();
        }
    }

    public class ClientManager
    {
        private object _SycLock = new object();
        private List<Agent.Client> _Clients = new List<Agent.Client>();

        public ClientManager()
        {
            Thread thread = new Thread(WriteMsg);
            thread.Start();
        }


        private byte[] GetMsg()
        {
            String msg = "I'm a Client,aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] data = Encoding.ASCII.GetBytes(msg);
            return data;
        }


        private void WriteMsg()
        {
            byte[] data = GetMsg();
            while (true)
            {
                Thread.Sleep(1000);
                lock (this._SycLock)
                {
                    Parallel.ForEach(this._Clients, client =>
                    {
                        client.Send(data);
                    });
                }
            }

        }


        public void Add(TcpClient socket)
        {
            lock (_SycLock)
            {
                var client = new Agent.Client(socket);
                client.Start();
                this._Clients.Add(client);
            }
        }
    }
}
