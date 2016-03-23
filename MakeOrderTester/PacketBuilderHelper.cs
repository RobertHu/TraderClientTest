using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MakeOrderTester
{
    class PacketBuilderHelper
    {
        private const string RootNode = "Request";
        private const string ArgumentsNode = "Arguments";
        private const string InvokeIdNode = "InvokeId";
        private const string MethodNode = "Method";
        private const int AppType = 7;
        private static string Template = File.ReadAllText("order.txt");

        public static XElement BuildLoginPacket(RequestBlock block,Account account)
        {
            var root = CreateRootNode(block, "TestMakeOrder");
            var args = new Dictionary<string, string>()
            {
                {"UserId", account.CustomerId},
                {"AppType", AppType.ToString()}
            };
            var argNode = CreateArgsNode(args);
            root.Add(argNode);
            return root;
        }

        public static XElement BuildMakeOrderPacket(RequestBlock block,Account account)
        {
            var root = CreateRootNode(block, "Place");
            var node = CreateTransactionElement(account);
            var args = new Dictionary<string, string>()
            {
                {"order",node.ToString()}
            };
            var argNode = CreateArgsNode(args);
            root.Add(argNode);
            return root;
        }


        public static XElement CreateTransactionElement(Account account)
        {
            XElement node = XElement.Parse(Template);
            var beginTime = DateTime.Now;
            var endTime = beginTime.AddMinutes(2);
            var standrandBeginTimeFormatStr = beginTime.ToStandrandFormat();
            node.Attribute("BeginTime").Value = standrandBeginTimeFormatStr;
            node.Attribute("SubmitTime").Value = standrandBeginTimeFormatStr;
            node.Attribute("EndTime").Value = endTime.ToStandrandFormat();
            node.Attribute("AccountID").Value = account.Id;
            node.Attribute("SubmitorID").Value = account.CustomerId;
            node.Attribute("ID").Value = Guid.NewGuid().ToString();
            node.Element("Order").Attribute("PriceTimestamp").Value = beginTime.ToString();
            node.Element("Order").Attribute("ID").Value = Guid.NewGuid().ToString();
            return node;

        }

        private static XElement CreateRootNode(RequestBlock block, string methodName)
        {
            XElement root = new XElement(RootNode);
            root.Add(new XElement(InvokeIdNode, block.InvokeId.ToString()));
            root.Add(new XElement(MethodNode, methodName));
            return root;
        }

        private static XElement CreateArgsNode(Dictionary<string, string> args)
        {
            XElement argsNode = new XElement(ArgumentsNode);
            foreach (var arg in args)
            {
                var node = new XElement(arg.Key, arg.Value);
                argsNode.Add(node);
            }
            return argsNode;
        }

    }
}
