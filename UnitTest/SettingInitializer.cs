using Core.TransactionServer.Agent;
using Core.TransactionServer.Agent.Market;
using Core.TransactionServer.Agent.Settings;
using Core.TransactionServer.Agent.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UnitTest
{
    internal static class SettingInitializer
    {
        private static bool IsLoaded = false;
        private static object _mutex = new bool();

        private static Server _server;

        internal static Server Server
        {
            get
            {
                if (_server == null)
                {
                    _server = new Server("transactionServer", "CacheFiles");
                }
                return _server;
            }
        }

        internal static void LoadSettings()
        {
            lock (_mutex)
            {
                if (IsLoaded) return;
                IsLoaded = true;
                string path = "SettingsFile/setting.xml";
                XElement root = XElement.Load(path);
                ExternalInitializer.Initialize(root);
                ExternalInitializer.InitializeDBConnectionString("data source=ws0310;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=60");
                MarketManager.Default.LoadInstruments();
            }
        }
    }
}
