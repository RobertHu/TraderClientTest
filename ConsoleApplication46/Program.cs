using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TransactionServerTester.TypeExtension;

namespace TransactionServerTester
{
    internal class XElementBuilder
    {
        private XElement _result;
        internal XElementBuilder(string name)
        {
            _result = new XElement(name);
        }

        internal XElement Result
        {
            get
            {
                return _result;
            }
        }

        internal void AppendAttributes(Dictionary<string, object> dict)
        {
            foreach (var eachPair in dict)
            {
                this.AppendAttributeAndValue(eachPair.Key, eachPair.Value);
            }
        }

        internal void AppendAttributeAndValue(string name, object value)
        {
            _result.SetAttributeValue(name, value);
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            TransactionServerService.ServerServiceClient client = new TransactionServerService.ServerServiceClient();
            var accountIds = new List<Guid>()
            {
                Guid.Parse("A11AABB6-2DC2-42D5-A914-00928CD5FEE8")
            };
            string initData = client.GetInitData(accountIds.ToArray());
            Console.WriteLine(initData);
            Console.Read();

        }

        static Protocal.TransactionData CreateTransactionData()
        {
            Protocal.TransactionData tranData = new Protocal.TransactionData();
            tranData.Id = Guid.NewGuid();
            tranData.InstrumentId = Guid.Parse("33C4C6E2-E33C-4A21-A01A-35F4EC647890");
            tranData.AccountId = Guid.Parse("B940D4B7-4A4E-46DF-8EA4-77B0C3CC1A6B");
            tranData.Type = iExchange.Common.TransactionType.Single;
            tranData.SubType = iExchange.Common.TransactionSubType.None;
            tranData.OrderType = iExchange.Common.OrderType.Limit;
            tranData.ExpireType = iExchange.Common.ExpireType.Day;
            var baseTime = DateTime.Now;
            tranData.BeginTime = baseTime;
            tranData.EndTime = baseTime.AddMinutes(30);
            tranData.SubmitTime = baseTime;
            tranData.SubmitorId = Guid.Parse("CB58B47D-A705-42DD-9308-6C6B26CE79A7");
            tranData.Orders = new List<Protocal.OrderData>();
            var order = CreateOrderData();
            tranData.Orders.Add(order);
            return tranData;
        }


        static Protocal.OrderData CreateOrderData()
        {
            var orderData = new Protocal.OrderData()
            {
                Id = Guid.NewGuid(),
                IsOpen = true,
                IsBuy = true,
                SetPrice = "0.8300",
                Lot = 3,
                LotBalance = 3,
                TradeOption = iExchange.Common.TradeOption.Better,
            };
            return orderData;
        }


        static string GetXml()
        {
            FileStream fileStream = new FileStream("source.xml", FileMode.Open, FileAccess.Read);
            var result = XElement.Load(fileStream);
            return result.ToString();
        }
    }
}
