using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Trader
{
    internal sealed class Hoster
    {
        internal static readonly Hoster Default = new Hoster();
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Hoster));

        private ServiceHost _commandCollectService;

        static Hoster() { }
        private Hoster() { }

        internal void Start()
        {
            try
            {
                _commandCollectService = new ServiceHost(typeof(CommandCollectService));
                _commandCollectService.Open();
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
                if (_commandCollectService != null)
                {
                    _commandCollectService.Close();
                    _commandCollectService = null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }



    }
}
