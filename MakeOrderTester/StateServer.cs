using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iExchange.Common.Client;
using iExchange.Common;
using System.Threading;
using System.Net;
using System.Xml.Linq;
using System.Xml;

namespace MakeOrderTester
{
    class StateServer
    {
        private static StateServerService _service;
        public static void Init()
        {
            Console.WriteLine("begin initialize state server");
            _service = new StateServerService();
            _service.Url = SettingManager.Default.StateServerUrl;
            StateServerReadyCheck(_service);
        }

        private static void StateServerReadyCheck(StateServerService stateServer)
        {
            try
            {
                var token = new Token(Guid.Empty,UserType.System,AppType.TradingConsole);
                stateServer.Register(token, "net.tcp://ws0210:9001/AsyncSslServer/Service/CommandCollectService");
                stateServer.HelloWorld();
                Console.WriteLine("state server initialized");
            }
            catch (WebException webException)
            {
                if (webException.Status == WebExceptionStatus.Timeout)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    StateServerReadyCheck(stateServer);
                }
            }
        }

        public static TransactionError MakeOrder(XElement tranEle, Account account)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(tranEle.ToString());
            var tran = doc.FirstChild;
            string tranCode = string.Empty;
            DateTime beginTime = DateTime.Parse(tran.Attributes["BeginTime"].Value);
            DateTime endTime = DateTime.Parse(tran.Attributes["EndTime"].Value);
            DateTime submitTime = DateTime.Parse(tran.Attributes["SubmitTime"].Value);
            OrderType orderType = (OrderType)XmlConvert.ToInt32(tran.Attributes["OrderType"].Value);

            if ((beginTime > DateTime.Now || beginTime < (DateTime.Now.AddMilliseconds(-3)))
                && orderType != OrderType.MarketOnOpen && orderType != OrderType.MarketOnClose)
            {
                TimeSpan span = endTime - beginTime;
                submitTime = beginTime = DateTime.Now;
                tran.Attributes["BeginTime"].Value = XmlConvert.ToString(beginTime, "yyyy-MM-dd HH:mm:ss");
                tran.Attributes["SubmitTime"].Value = XmlConvert.ToString(submitTime, "yyyy-MM-dd HH:mm:ss");

                if (endTime <= DateTime.Now)
                {
                    endTime = beginTime + span;
                    tran.Attributes["EndTime"].Value = XmlConvert.ToString(endTime, "yyyy-MM-dd HH:mm:ss");
                }
            }

            foreach (XmlNode child in tran.ChildNodes)
            {
                if (child.Name == "Order")
                {
                    if (child.Attributes["Extension"] != null
                        && child.Attributes["Extension"].Value.StartsWith("IfDone"))
                    {
                        string oldValue = child.Attributes["Extension"].Value;
                        XmlDocument document = new XmlDocument();
                        XmlElement element = document.CreateElement("IfDone");
                        string[] items = oldValue.Split(new char[] { ' ' });
                        foreach (string item in items)
                        {
                            if (item != "IfDone")
                            {
                                string[] keyValue = item.Split(new char[] { '=' });
                                if (keyValue[0] == "LimitPrice" || keyValue[0] == "StopPrice")
                                {
                                    element.SetAttribute(keyValue[0], keyValue[1]);
                                }
                            }
                        }
                        child.Attributes["Extension"].Value = element.OuterXml;
                    }
                }
            }
            var myToken = new Token(Guid.Empty, UserType.Customer, AppType.TradingConsole);
            myToken.Language = "ENG";
            myToken.UserID = Guid.Parse(account.CustomerId);
            TransactionError error = _service.Place(myToken, tran, out tranCode);
            return error;
        }
    }
}
