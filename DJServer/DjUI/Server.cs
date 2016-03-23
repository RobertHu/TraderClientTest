using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Windows.Controls;
using System.Text.RegularExpressions;
namespace DjUI
{
    public class Server
    {
        private TcpListener _TcpListener = new TcpListener(IPAddress.Any, 20887);
        public static readonly Server Default = new Server();
        public SynchronizationContext SynchronizationContext { get; set; }
        public WebBrowser WebBrowser { get; set; }
        public string NewsDirectory { get; set; }
        private Server()
        {

        }

        public void Start()
        {
            try
            {
                this._TcpListener.Start();
                while (true)
                {
                    TcpClient tcpClient = this._TcpListener.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(Dowork), tcpClient);
                }
            }
            catch (Exception ex)
            {
                this._TcpListener.Stop();
            }
        }

        private void Dowork(object state)
        {
            TcpClient tcpClient = (TcpClient)state;
            try
            {
                NetworkStream stream = tcpClient.GetStream();
                Byte[] buf = new Byte[1024];
                MemoryStream ms = new MemoryStream();
                int readCount = stream.Read(buf, 0, buf.Length);
                while (readCount > 0)
                {
                    ms.Write(buf, 0, readCount);
                    readCount = stream.Read(buf, 0, buf.Length);
                }
                byte[] result = ms.ToArray();
                string allContent = Encoding.ASCII.GetString(result);
                string news = GetContent(allContent);
                string htmlPath = Path.Combine(this.NewsDirectory, string.Format("{0}.html", Guid.NewGuid().ToString()));
                using (FileStream fs = new FileStream(htmlPath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(news);
                    sw.Flush();
                    sw.Close();
                }

                this.SynchronizationContext.Post(ar =>
                    {
                        this.WebBrowser.Navigate(htmlPath);
                    }, null);
            }
            finally
            {
                tcpClient.Close();
            }
        }

        private string GetContent(string source)
        {
            string pattern = "<fulltext>([\\W\\w]+)</fulltext>";
            Regex regex=new Regex(pattern);
            MatchCollection col=regex.Matches(source);
            Stack<string> stack=new Stack<string>();
            foreach(Match item in col)
            {
                if(item.Success) stack.Push(item.Groups[1].Value);
            }
            string template="<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\">"+
"<html>"+
	"<head>"+
		"<title></title>"+
	"</head>"+
	"<body>{0}"+
	"</body>"+
"</html>";
            return string.Format(template,stack.Pop());
            
        }

    }
}
