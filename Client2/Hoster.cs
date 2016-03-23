using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Protocal.Communication;
using log4net;

namespace TraderClient
{
    internal sealed class Hoster
    {
        internal static readonly Hoster Default = new Hoster();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Hoster));

        private ServiceHost _commandCollectionServiceHost;

        static Hoster() { }
        private Hoster() { }

        internal void Start()
        {
            try
            {
                _commandCollectionServiceHost = new ServiceHost(typeof(CommandCollectService));
                _commandCollectionServiceHost.AddDiscoveryFunction();
                _commandCollectionServiceHost.Open();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        internal void Stop()
        {
            try
            {
                _commandCollectionServiceHost.Close();
                _commandCollectionServiceHost = null;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

    }
}
