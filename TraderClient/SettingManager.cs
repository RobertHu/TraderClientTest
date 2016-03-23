using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TraderClient
{
    internal sealed class SettingManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SettingManager));

        internal static readonly SettingManager Default = new SettingManager();

        static SettingManager() { }
        private SettingManager() { }

        internal string SettingFilePath { get; private set; }

        internal void Load()
        {
            this.SettingFilePath = this.LoadItem("SetingFilePath");
        }

        private string LoadItem(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return string.Empty;
            }
        }

    }
}
