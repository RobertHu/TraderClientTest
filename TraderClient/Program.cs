using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TraderClient.CacheTest;
using TraderClient.Config;
using TraderClient.OrderBLL;
using TraderClient.Reset;
using TraderClient.TransactionBLL;
using TraderClient.Watch;
using TraderClient.QuotationBll;
using TraderClient.InstrumentBLL;
using System.ServiceModel;
using TraderClient.Instalment;
using TraderClient.Physical;
using iExchange.Common;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace TraderClient
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        private static Protocal.Test.IServerService _transactionClient;
        private static Protocal.IGatewayService _gatewayClient;

        static void Main(string[] args)
        {
            InitializeServiceChannel();
            var instrumentId = Guid.Parse("1DFB99D4-2B76-48B0-9109-0A67265F5B9F");
            QuotationSender.Default.SendQuotation(instrumentId.ToString(), "1.50", "1.51");
            ////ResetManager.Default.DoReset(_transactionClient);
            var accountId = Guid.Parse("C2212781-9F60-4241-A16D-000730A6C653");
            var customerId = Guid.Parse("4E5C7C13-8F29-4F6E-BB69-A48472DFDF7A");
            //var initData = _transactionClient.GetInitData(new List<Guid> { accountId});

            //_tradingTimeManager.InstrumentDayCloseTest();

            //var tran = OrderTester.CreatePhysicalPrePayTran(accountId, instrumentId, "1313.1", 0.1m, true, true, iExchange.Common.PhysicalTradeSide.Buy);
            //var instalmentParameter = new InstalmentParameter(Guid.Parse("0817d587-c929-4f77-bcc7-38f2b8acd793"), 0.152m, iExchange.Common.InstalmentType.EqualInstallment, iExchange.Common.InstalmentFrequence.Month, iExchange.Common.RecalculateRateType.NextMonth,1);
            //var tran = TransactionManager.Default.CreateOpenPhysicalInstalmentSpotTran(accountId, instrumentId, "1.025", 1, true, iExchange.Common.PhysicalTradeSide.Buy, Protocal.Physical.PhysicalType.Instalment, instalmentParameter);
            //_transactionClient.Place(tran);

            //var deliveryRequest = DeliveryManager.CreateDeliveryRequest(accountId, instrumentId, Guid.Parse("60a1877a-74c5-430f-be35-b29eca6931ca"), Guid.Parse("ADF04C39-3837-4858-8849-8C4ADC53D250"));
            //_transactionClient.ApplyDelivery(deliveryRequest);


            //var tran = TransactionManager.Default.CreatePhysicalCloseSportTran(accountId, instrumentId, Guid.Parse("931c1265-e4a8-4495-a54d-7a05f82281de") ,"1.40", 0.3m, false, iExchange.Common.PhysicalTradeSide.Sell,customerId);
            //var tran = TransactionManager.Default.CreatePhysicalOpenSportTran(accountId, instrumentId, "1.30", 0.3m, true, iExchange.Common.PhysicalTradeSide.Buy, customerId);
            //var tran = TransactionManager.Default.CreatePhysicalCloseSportTran(accountId, instrumentId, Guid.Parse("84c8acde-a815-4bbb-9447-b56ec5038c0b"), "1.75", 0.02m, false, iExchange.Common.PhysicalTradeSide.Sell, customerId);
            //var instalmentParameter = new InstalmentParameter(Guid.Parse("403B98A6-5899-489C-9F63-E338B11D3DE7"), 0.3m, iExchange.Common.InstalmentType.EqualInstallment, InstalmentFrequence.TillPayoff, RecalculateRateType.NextMonth, 1);
            //var tran = TransactionManager.Default.CreateOpenPhysicalInstalmentSpotTran(accountId, instrumentId, "1.0989", 0.03m, true, iExchange.Common.PhysicalTradeSide.Buy, Protocal.Physical.PhysicalType.PrePayment, instalmentParameter, customerId);
            //var tran1 = TransactionManager.Default.CreateOpenLimitTran(accountId, instrumentId, "1.50", 2, new TimeSpan(0, 40, 0), true, Guid.Parse("1A3447AF-8BC2-4119-9719-76A26F3E8DE8"));
            //var tran2 = TransactionManager.Default.CreateOpenLimitTran(accountId, instrumentId, "1.50", 2, new TimeSpan(0, 40, 0), true, Guid.Parse("1A3447AF-8BC2-4119-9719-76A26F3E8DE8"));
            //var tran3 = TransactionManager.Default.CreateOpenLimitTran(accountId, instrumentId, "1.50", 2, new TimeSpan(0, 40, 0), true, Guid.Parse("1A3447AF-8BC2-4119-9719-76A26F3E8DE8"));
            //var tran = TransactionManager.Default.CreateSpotTran(accountId, instrumentId, "1.50", 2, true, customerId);
            //OldTransactionServerTest.StateServerTest.TestGetInitData(accountId);
            //OldTransactionServerTest.StateServerTest.TestSportOrder(accountId, instrumentId, customerId, "1.5");
            var tran = TradingEngine.TransactionModule.createSportTranData(accountId, instrumentId, "1.5", 2, true, customerId);
            _transactionClient.Place(tran);

            //var tran = TransactionManager.Default.CreateCloseSpotTran(accountId, instrumentId, Guid.Parse("0d54e3af-4950-43c7-b988-2e8a9c32bfaf"), "1.50", 2, false, customerId);
            //var tran = TransactionManager.Default.CreateOOCOOpenLimitTran(accountId, instrumentId, "1.4433", "1.4404", true, 1, customerId);
            //_transactionClient.Place(tran);
            //var initData = _transactionClient.GetInitData(new List<Guid> { accountId });
            //var now = DateTime.Now;
            //tran.ExecuteTime = new DateTime(now.Year, 1, 28, now.Hour, now.Minute, now.Second);
            //_transactionClient.PlaceHistoryOrder(tran);

            ////TradingTimeManager.InstrumentDayCloseTest(_gatewayClient);
            ////var initData = _transactionClient.GetInitData(new List<Guid> { accountId });
            //_transactionClient.DeleteOrder(accountId, Guid.Parse("7f4688da-c01f-4a59-a0ea-1cb0d12f4c0c"), true);
            Console.WriteLine("Done");
            StopServiceChannel();
            Console.Read();
        }

        static void InitializeServiceChannel()
        {
            _transactionClient = Protocal.ChannelFactory.CreateHttpChannel<Protocal.Test.IServerService>("http://ws0210:8090/TransactionServer/Service");
            _gatewayClient = Protocal.ChannelFactory.CreateHttpChannel<Protocal.IGatewayService>("http://localhost:5060/GatewayService");
            QuotationSender.Default.Initialize(_gatewayClient);
        }

        static void StopServiceChannel()
        {
            CloseChannel(_transactionClient);
            CloseChannel(_gatewayClient);
        }

        static void CloseChannel<T>(T channel) where T : class
        {
            ICommunicationObject communicationObj = channel as ICommunicationObject;
            if (communicationObj != null)
            {
                communicationObj.Close();
            }
        }

        //private static void GetTradingInstruomentStatus(Guid instrumentId)
        //{
        //    GatewayService.GatewayServiceClient client = new GatewayService.GatewayServiceClient();
        //    var status = client.GetTradingInstrumentStatus(instrumentId);

        //    TransactionService.ServerServiceClient tranClient = new TransactionService.ServerServiceClient();
        //    var tranStatus = tranClient.GetInstrumentTradingStatus(instrumentId);

        //}

        private static void UpdateNode()
        {
            XmlDocument doc = new XmlDocument();
            doc.CreateElement("Instrument");
            XmlElement updateNode = doc.DocumentElement;
            //TransactionService.ServerServiceClient tranClient = new TransactionService.ServerServiceClient();
            //tranClient.Update(new XElement("Instrument"));
        }



        private static void GetTradingInfo()
        {
            //TransactionService.ServerServiceClient client = new TransactionService.ServerServiceClient();
            //var tradingInfo = client.GetTradingInfo();
        }


        private static void CommandCollectServiceTest()
        {
            //Hoster.Default.Start();
            //GatewayService.GatewayServiceClient client = new GatewayService.GatewayServiceClient();
            //client.Register("net.tcp://localhost:5444/CommandCollectService", iExchange.Common.AppType.CppTrader);
            //Console.Read();
        }

        private static void GetInitData(Guid accountId)
        {
            //TransactionService.ServerServiceClient client = new TransactionService.ServerServiceClient();
            //var data = client.GetInitData(new Guid[] { accountId });
        }

        private static void PlaceFullPaymentOrderTest()
        {
            //TransactionService.ServerServiceClient client = new TransactionService.ServerServiceClient();
            //client.Place(CreateFullPaymentOrder());
        }


        private static void PlaceDQOrder()
        {
            PlaceDQOrderCommon(Guid.Parse("2eab5b94-1a8e-4604-ac38-6f4453a860a8"), Guid.Parse("299b29d8-0a98-4817-9fba-0b003021916c"), "1.57");
        }

        private static void PlacePhysicalDQOrder()
        {
            var tranData = CreateTranData(Guid.Parse("078B9C4E-C8E1-42AD-ACFB-2652A13F1894"), Guid.Parse("670590DB-8CE6-4676-AC68-7701FFC5410A"));
            var order = new Protocal.Physical.PhysicalOrderData
            {
                Id = Guid.NewGuid(),
                DQMaxMove = 0,
                IsOpen = true,
                IsBuy = true,
                Lot = 1,
                TradeOption = iExchange.Common.TradeOption.Better,
                SetPrice = "0.73",
                PriceTimestamp = DateTime.Now,
                PhysicalTradeSide = iExchange.Common.PhysicalTradeSide.Buy,
                PhysicalType = Protocal.Physical.PhysicalType.FullPayment,
            };
            tranData.Orders = new List<Protocal.OrderData>();
            tranData.Orders.Add(order);
            Logger.WarnFormat("Place order id={0}", order.Id);

            //TransactionService.ServerServiceClient client = new TransactionService.ServerServiceClient();
            //client.Place(tranData);
        }

        private static void PlaceDQOrderCommon(Guid instrumentId, Guid accountId, string price)
        {
            var tran = CreateTranData(accountId, instrumentId);
            var order = new Protocal.OrderData()
            {
                Id = Guid.NewGuid(),
                DQMaxMove = 0,
                IsOpen = true,
                Lot = 1,
                TradeOption = iExchange.Common.TradeOption.Better,
                SetPrice = price,
                PriceTimestamp = DateTime.Now
            };
            tran.Orders = new List<Protocal.OrderData>();
            tran.Orders.Add(order);
            //TransactionService.ServerServiceClient client = new TransactionService.ServerServiceClient();
            //client.Place(tran);
        }

        private static void GetOrderInstalments()
        {
            Guid orderId = Guid.Parse("5D1AEB3B-5C1E-4944-8F4B-00ECF27A9383");
        }

        private static Protocal.TransactionData CreateFullPaymentOrder()
        {
            var tranData = CreateTranData(Guid.Parse("51164EC5-923D-4B08-ADC2-0048A037E3EE"), Guid.Parse("0ADF8B7D-238D-4F29-8B13-14307FDA9701"));
            var order = new Protocal.Physical.PhysicalOrderData
            {
                Id = Guid.NewGuid(),
                DQMaxMove = 0,
                IsOpen = true,
                IsBuy = true,
                Lot = 1,
                TradeOption = iExchange.Common.TradeOption.Better,
                SetPrice = "1.234",
                PriceTimestamp = DateTime.Now,
                PhysicalTradeSide = iExchange.Common.PhysicalTradeSide.Buy,
                PhysicalType = Protocal.Physical.PhysicalType.FullPayment,
            };

            tranData.Orders = new List<Protocal.OrderData>();
            tranData.Orders.Add(order);
            return tranData;
        }

        private static Protocal.TransactionData Create()
        {
            var tranData = CreateTranData(Guid.Parse("51164EC5-923D-4B08-ADC2-0048A037E3EE"), Guid.Parse("0ADF8B7D-238D-4F29-8B13-14307FDA9701"));
            var order = new Protocal.Physical.PhysicalOrderData
            {
                Id = Guid.NewGuid(),
                DQMaxMove = 0,
                IsOpen = true,
                IsBuy = true,
                Lot = 1,
                TradeOption = iExchange.Common.TradeOption.Better,
                SetPrice = "1.234",
                PriceTimestamp = DateTime.Now,
                PhysicalTradeSide = iExchange.Common.PhysicalTradeSide.Buy,
                InstalmentPart = new Protocal.Physical.InstalmentPart
                {
                    InstalmentFrequence = iExchange.Common.InstalmentFrequence.Month,
                    InstalmentPolicyId = Guid.Parse("0817D587-C929-4F77-BCC7-38F2B8ACD793"),
                    Period = 9,
                    InstalmentType = iExchange.Common.InstalmentType.EqualPrincipal,
                    RecalculateRateType = iExchange.Common.RecalculateRateType.NextMonth,
                    DownPayment = 20
                }
            };

            tranData.Orders = new List<Protocal.OrderData>();
            tranData.Orders.Add(order);
            return tranData;

        }

        private static Protocal.TransactionData CreateTranData(Guid accountId, Guid instrumentId)
        {
            return new Protocal.TransactionData()
            {
                Id = Guid.NewGuid(),
                AccountId = accountId,
                BeginTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2),
                InstrumentId = instrumentId,
                OrderType = iExchange.Common.OrderType.SpotTrade,
                SubmitorId = Guid.Parse("CB58B47D-A705-42DD-9308-6C6B26CE79A7"),
                SubmitTime = DateTime.Now,
                Type = iExchange.Common.TransactionType.Single,
                SubType = iExchange.Common.TransactionSubType.None
            };
        }

    }
}
