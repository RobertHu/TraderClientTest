using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace TCPServer
{
    class Program
    {
        private static byte[] buffer=new byte[4996];
        static void Main(string[] args)
        {
            Console.WriteLine("Server is running..");
            IPAddress ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
            TcpListener listener = new TcpListener(ip, 8500);
            listener.Start();
            Console.WriteLine("Start Listening...");
            listener.BeginAcceptTcpClient(Callback, listener);
            Console.ReadKey();
        }

        static void Callback(IAsyncResult ar)
        {
            Array.Clear(Program.buffer, 0, Program.buffer.Length);
            var listener = ar.AsyncState as TcpListener;
            var client = listener.EndAcceptTcpClient(ar);
            var stream = client.GetStream();
            string content = "Server response...";
            var buf = Encoding.ASCII.GetBytes(content);
            stream.Write(buf, 0, buf.Length);
            stream.BeginRead(buffer, 0, buffer.Length, m =>
            {
                int length = stream.EndRead(m);
                string target = Encoding.ASCII.GetString(buffer, 0,length);
                Console.WriteLine(target);
                listener.BeginAcceptTcpClient(Callback, listener);
            }, null);
           
        }
    }
}
