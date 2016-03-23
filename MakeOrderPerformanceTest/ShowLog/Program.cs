using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net.Sockets;
using System.Net;

namespace ShowLog
{


    class Program
    {
        static byte[] Buf = new byte[1024];
        static byte[] SendContent = Encoding.ASCII.GetBytes("Received");
        static string LogFileName = "log.txt";
        static void Log(string content)
        {
            using (FileStream fs = new FileStream(LogFileName, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(content);
                }
            }
        }

        public static void Main()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 9999;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);
                server.Start();
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    NetworkStream stream = client.GetStream();
                    while (true)
                    {
                        try
                        {
                            int len = stream.Read(Buf, 0, Buf.Length);
                            string data = System.Text.Encoding.ASCII.GetString(Buf, 0, len);
                            Console.WriteLine(data);
                            Log(data);
                            stream.BeginWrite(SendContent, 0, SendContent.Length, ar =>
                            {
                                try
                                {
                                    stream.EndWrite(ar);
                                }
                                catch
                                {
                                }
                            }, null);
                        }
                        catch
                        {
                            client.Close();
                            break;
                        }
                    }
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
    }
}
