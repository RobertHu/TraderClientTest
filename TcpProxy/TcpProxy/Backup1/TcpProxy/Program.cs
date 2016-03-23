using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
namespace TcpProxy
{
    /// <summary>
    /// by 路过秋天
    /// http://www.cnblogs.com/cyq1162
    /// </summary>
    class Program
    {
        static Dictionary<string, int> dic = new Dictionary<string, int>();
        static void Main(string[] args)
        {
                Listen(8989);
        }
        static void Write(string msg)
        {
            Console.WriteLine(msg);
        }

        static void Listen(int port)
        {
            Write("准备监听端口:" + port);
            TcpListener tcplistener = new TcpListener(port);
            try
            {
                tcplistener.Start();
            }
            catch
            {
                Write("该端口已被占用,请更换端口号!!!");
                ReListen(tcplistener);
            }
            Write("确认:y/n (yes or no):");
            string isOK = Console.ReadLine();
            if (isOK == "y")
            {
                Write("成功监听端口:" + port);
                //侦听端口号 
                Socket socket;
                while (true)
                {
                    socket = tcplistener.AcceptSocket();
                    //并获取传送和接收数据的Scoket实例 
                    Proxy proxy = new Proxy(socket);
                    //Proxy类实例化 
                    Thread thread = new Thread(new ThreadStart(proxy.Run));
                    //创建线程 
                    thread.Start();
                    System.Threading.Thread.Sleep(10);
                    //启动线程 
                }
            }
            else
            {
                ReListen(tcplistener);
            }
        }
        static void ReListen(TcpListener listener)
        {
            if (listener != null)
            {
                listener.Stop();
                listener = null;
            }
            Write("请输入监听端口号:");
            string newPort = Console.ReadLine();
            int port;
            if (int.TryParse(newPort, out port))
            {
                Listen(port);
            }
            else
            {
                ReListen(listener);
            }
        }
    }
}
