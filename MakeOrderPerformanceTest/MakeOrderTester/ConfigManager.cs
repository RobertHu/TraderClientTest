using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MakeOrderTester
{
    class SettingManager
    {
        public class HostPort
        {
            public HostPort(string hostName, int port)
            {
                this.HostName = hostName;
                this.Port = port;
            }
            public string HostName { get; private set; }
            public int Port { get; private set; }
        }
        public static SettingManager Default = new SettingManager();
        private SettingManager() { }
        public void Initialize()
        {
            this.StateServerUrl = GetSetting("StateServerUrl");
            this.IsTestStateServer = int.Parse(GetSetting("IsTestStateServer")) == 1 ? true : false;
            this.TraderServer = new HostPort(GetSetting("TraderServerHostName"), int.Parse(GetSetting("TraderServerPort")));
            this.StatisticsServer = new HostPort(GetSetting("StatisticsServerHostName"), int.Parse(GetSetting("StatisticsServerPort")));
            this.MakeOrderInterval = TimeSpan.FromSeconds(int.Parse(GetSetting("MakeOrderInterval")));
            this.ShowStatisticsInterval = TimeSpan.FromSeconds(int.Parse(GetSetting("ShowStatisticsInterval")));
        }

        private string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public string StateServerUrl { get; private set; }
        public bool IsTestStateServer { get; private set; }
        public HostPort TraderServer { get; private set; }
        public HostPort StatisticsServer { get; private set; }
        public TimeSpan MakeOrderInterval { get; private set; }
        public TimeSpan ShowStatisticsInterval { get; private set; }
    }
}
