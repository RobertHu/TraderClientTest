using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                LingerOption lingerOption = new LingerOption(true, 0);
                server.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, lingerOption);
                server.Start();
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    //client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger, lingerOption);
                    Console.WriteLine("Connected!");
                    Task task = new Task(AcceptClient, client);
                    task.Start();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        static void AcceptClient(object state)
        {
            TcpClient client = (TcpClient)state;
            Byte[] bytes = new Byte[256];
            String data = null;
            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();


            // Loop to receive all the data sent by the client. 
            int len = stream.Read(bytes, 0, bytes.Length);
            // Translate data bytes to a ASCII string.
            data = System.Text.Encoding.ASCII.GetString(bytes, 0, len);
            Console.WriteLine("Received: {0}", data);

            // Process the data sent by the client.
            data = data.ToUpper();

            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

            // Send back a response.
            stream.Write(msg, 0, msg.Length);
            Console.WriteLine("Sent: {0}", data);
        }


    }
}
