using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace MakeOrderTester
{
    class OrderStatistics
    {
        public static OrderStatistics Default = new OrderStatistics();
        private List<double> _costList = new List<double>();
        private object _lock = new object();
        private ShowLogClient _client;
        private string Indentity = string.Empty;

        private OrderStatistics()
        {
        }

        private void Initialize()
        {
            Indentity = SettingManager.Default.IsTestStateServer ? "StateServer" : "TraderServer";
            _client = new ShowLogClient(SettingManager.Default.StatisticsServer.HostName, SettingManager.Default.StatisticsServer.Port);
        }

        public void Start()
        {
            Initialize();
            Thread thread = new Thread(Process)
            {
                IsBackground = true
            };
            thread.Start();
        }


        public void Add(double costTime)
        {
            lock (_lock)
            {
                _costList.Add(costTime);
            }
        }

        private void Process()
        {
            while (true)
            {
                Thread.Sleep(SettingManager.Default.ShowStatisticsInterval);
                lock (_lock)
                {
                    if (_costList.Count() == 0) continue;
                    double maxCost = _costList.Max();
                    double minCost = _costList.Min();
                    double averageCost = _costList.Sum() / _costList.Count();
                    double totalCost = _costList.Sum();
                    string content = string.Format("{0} {1} accounts make orders to {6}, cost time: max={2}, min={3}, average={4}, total={5}", DateTime.Now.ToStandrandFormat(), _costList.Count(), maxCost, minCost, averageCost, totalCost, this.Indentity);
                    _client.Write(content);
                    _costList.Clear();
                }
            }
        }

    }
}
