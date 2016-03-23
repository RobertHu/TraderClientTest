using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionServerTest
{
    internal sealed class SettingManager
    {
        internal static readonly SettingManager Default = new SettingManager();

        private static readonly ILog Logger = LogManager.GetLogger(typeof(SettingManager));

        private List<string> _gatewayServiceScopes = new List<string>();

        static SettingManager() { }
        private SettingManager() { }

        internal List<string> GatewayServiceScopes
        {
            get { return _gatewayServiceScopes; }
        }

        internal void LoadSettings()
        {
            this.ParseGatewayServiceScopes();
        }

        private void ParseGatewayServiceScopes()
        {
            try
            {
                var scopesValue = ConfigurationManager.AppSettings["GatewayServiceScopes"];
                if (string.IsNullOrEmpty(scopesValue)) return;
                var scopes = scopesValue.Split(';');
                foreach (var scope in scopes)
                {
                    _gatewayServiceScopes.Add(scope);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

    }
}
