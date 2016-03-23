using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Trader
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public sealed class CommandCollectService : Protocal.CommandCollectServiceBase
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CommandCollectService));

        protected override void HandleMarketCommand(Protocal.MarketCommand command)
        {
            if (command is Protocal.UpdateInstrumentTradingStatusMarketCommand)
            {
                var tradingStatusCommand = (Protocal.UpdateInstrumentTradingStatusMarketCommand)command;
                Logger.InfoFormat("Receive update instrument trading status command\n: {0}", tradingStatusCommand.ToString());
            }
        }

        protected override void HandleQuotationCommand(Protocal.QuotationCommand command)
        {
            throw new NotImplementedException();
        }

        protected override void HandleSettingCommand(Protocal.SettingCommand command)
        {
            throw new NotImplementedException();
        }

        protected override void HandleTradingCommand(Protocal.TradingCommand tradingCommand)
        {
            Logger.InfoFormat("Receive TradingCommand:   {0}", tradingCommand.Content);
        }
    }
}
