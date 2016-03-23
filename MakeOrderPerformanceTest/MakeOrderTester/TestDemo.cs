using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using iExchange.Common;

namespace MakeOrderTester
{
    class TestDemo
    {
        private CommunicationClient _client;
        private UserInfo _userInfo = new UserInfo();
        private InvokeManager _invokeManager = new InvokeManager();
        private Account _account;
        private bool _isTestStateServer;
        public TestDemo(Account account, bool isTestStateServer)
        {
            _isTestStateServer = isTestStateServer;
            _account = account;
            if (!isTestStateServer)
            {
                _client = new CommunicationClient(SettingManager.Default.TraderServer.HostName, SettingManager.Default.TraderServer.Port, _invokeManager);
                Login();
            }
        }

        public void Start()
        {
            Thread thread = new Thread(Process)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void Process()
        {
            while (true)
            {
                MakeOrder();
                Thread.Sleep(SettingManager.Default.MakeOrderInterval);
            }
        }

        private void Login()
        {
            RequestBlock block = _invokeManager.CreateBlock();
            XElement loginRequest = PacketBuilderHelper.BuildLoginPacket(block, _account);
            byte[] loginPacket = PacketBuilder.Build(loginRequest, _userInfo.Session);
            _client.Write(loginPacket);
            block.Wait();
            string session = block.Result.Element("session").Value;
            _invokeManager.Remove(block.InvokeId);
            _userInfo.Session = session;
        }

        private void MakeOrder()
        {
            if (!_isTestStateServer)
            {
                NormalMakeOrder();
            }
            else
            {
                var tran = PacketBuilderHelper.CreateTransactionElement(_account);
                DateTime beginTime = DateTime.Now;
                var transactionError = StateServer.MakeOrder(tran, _account);
                DateTime endTime = DateTime.Now;
                var time = (endTime - beginTime).TotalMilliseconds;
                RecordResult(transactionError.ToString(), time);
            }
        }

        private string MakeMockOrder()
        {
            return TransactionError.OK.ToString();
        }

        private void NormalMakeOrder()
        {
            RequestBlock placeBlock = _invokeManager.CreateBlock();
            XElement placeRequest = PacketBuilderHelper.BuildMakeOrderPacket(placeBlock, _account);
            byte[] placePacket = PacketBuilder.Build(placeRequest, _userInfo.Session);
            _client.Write(placePacket);
            DateTime beginTime = DateTime.Now;
            placeBlock.Wait();
            if (!placeBlock.IsError)
            {
                DateTime endtime = DateTime.Now;
                var result = placeBlock.Result;
                string transactionError = result.Element("transactionError").Value;
                var time = (endtime - beginTime).TotalMilliseconds;
                RecordResult(transactionError, time);
            }
        }

        private void RecordResult(string transactionError, double time)
        {
            if (transactionError == "Action_ShouldAutoFill" || transactionError == "OK")
            {
                OrderStatistics.Default.Add(time);
                Console.WriteLine("{0} account: {1} make order success , cost time: {2}", DateTime.Now.ToStandrandFormat(), _account.Code, time);
            }
            else
            {
                Console.WriteLine("{0}, account: {1} make order failed, reason: {2}", DateTime.Now.ToStandrandFormat(), _account.Code, transactionError);
            }
        }
    }
}
