using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TraderServerTest
{
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
            return new byte[800];
            
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
                System.Console.WriteLine("Add a client");
                var client = new Agent.Client(socket);
                client.Start();
                this._Clients.Add(client);
            }
        }



    }
}
