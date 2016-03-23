using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using System.Web;
namespace DJServer
{
    public class DJNewsServer
    {
        private DJNewsServer()
        {
        }
        public static readonly DJNewsServer Default = new DJNewsServer();
        private TcpListener _EngListener;
        private TcpListener _ChsListener;
        private TcpListener _ChtListener;

        public void Initialize(int engPort, int chsPort, int chtPort)
        {
            int lowerBound = 0;
            int upperBound = 65536;
            if (engPort < lowerBound || engPort > upperBound)
            {
                throw new ArgumentOutOfRangeException("engPort", "not a valid port");
            }
            if (chsPort < lowerBound || chsPort > upperBound)
            {
                throw new ArgumentOutOfRangeException("chsPort", "not a valid port");
            }
            if (chtPort < lowerBound || chtPort > upperBound)
            {
                throw new ArgumentOutOfRangeException("chtPort", "not a valid port");
            }
            this._EngListener = new TcpListener(IPAddress.Any, engPort);
            this._ChsListener = new TcpListener(IPAddress.Any, chsPort);
            this._ChtListener = new TcpListener(IPAddress.Any, chtPort);
        }

        public void Start()
        {

            Thread engThread = new Thread(DoEngWork);
            engThread.Start();

            Thread chsThread = new Thread(DoChsWork);
            chsThread.Start();

            Thread chtThread = new Thread(DoChtWork);
            chtThread.Start();
        }

        public void Stop()
        {
            this._EngListener.Stop();
            this._ChsListener.Stop();
            this._ChtListener.Stop();
        }

        private void DoEngWork()
        {
            DoworkHelper(this._EngListener, "Eng");
        }

        private void DoChsWork()
        {
            DoworkHelper(this._ChsListener, "Chs");
        }

        private void DoChtWork()
        {
            DoworkHelper(this._ChtListener, "Cht");
        }


        private void DoworkHelper(TcpListener tcpListener, string language)
        {
            try
            {
                tcpListener.Start();
                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(delegate(object state)
                    {
                        TcpClient client = (TcpClient)state;
                        try
                        {
                            HTMLHelper.Log(language + " News coming.....--------------------------", true);
                            string content = GetAllData(tcpClient,language);
                            List<string> xmls = GetXml(content);
                            foreach (string xml in xmls)
                            {
                                News news = ParseNews(xml);
                                HTMLHelper.Log(news.Title);
                                HTMLHelper.Log(news.Content);
                                HTMLHelper.Log("--------------------------", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            client.Close();
                            HTMLHelper.LogInfo(ex.ToString(), true);
                        }

                    }, tcpClient);
                }
            }
            catch (Exception ex)
            {
                HTMLHelper.LogInfo(ex.ToString(), true);
                tcpListener.Stop();
            }
        }



        private string GetAllData(TcpClient client,string language)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                Byte[] buf = new Byte[1024];
                MemoryStream ms = new MemoryStream();
                int readCount = stream.Read(buf, 0, buf.Length);
                while (readCount > 0)
                {
                    ms.Write(buf, 0, readCount);
                    readCount = stream.Read(buf, 0, buf.Length);
                }
                byte[] result = ms.ToArray();
                string allContent=string.Empty;
                if (language == "Eng")
                {
                    allContent = Encoding.ASCII.GetString(result);
                }
                else
                {
                    allContent = Encoding.UTF8.GetString(result);
                    HTMLHelper.LogInfo(allContent, true);
                    //HTMLHelper.LogInfo(Encoding.UTF8.GetString(result), true);
                }
                return allContent;
            }
            catch (Exception ex)
            {
                HTMLHelper.LogInfo(ex.ToString(),true);
                return string.Empty;
            }
            finally
            {
                client.Close();
            }
        }

        private List<string> GetXml(string content)
        {
            string headPattern = "(<\\?xml[\\W\\w]+?\\?>)";
            Regex headRegex = new Regex(headPattern);
            Match match = headRegex.Match(content);
            Debug.Assert(match.Success, "Can't found xml declare");
            string xmlDeclare = match.Groups[1].Value;
            string pattern = "(<root>[\\W\\w]+?</root>)";
            Regex rootRegex = new Regex(pattern);
            MatchCollection matches = rootRegex.Matches(content);
            List<string> xmlList = new List<string>();
            foreach (Match m in matches)
            {
                if (m.Success == false) continue;
                xmlList.Add(xmlDeclare + m.Groups[1].Value);
            }
            Debug.Assert(xmlList.Count > 0, "Can't fount root element");
            return xmlList;
        }

        private News ParseNews(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlElement root = doc.DocumentElement;
            string titlePath = "/root/DJStory/title";
            string contentPath = "/root/DJStory/fulltext";
            string title = root.SelectSingleNode(titlePath).InnerText;
            string content = root.SelectSingleNode(contentPath).InnerText;
            title = HttpUtility.HtmlDecode(title);
            content = HttpUtility.HtmlDecode(content);
            News news = new News();
            news.Title = HTMLHelper.StripHTML(title);
            news.Content = HTMLHelper.StripHTML(content);
            return news;
        }

        private class News
        {
            public string Title { get; set; }
            public string Content { get; set; }
        }

    }


}
