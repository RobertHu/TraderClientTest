using log4net;
using Newtonsoft.Json;
using Protocal.TradingInstrument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
namespace TraderClient
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    //internal sealed class CommandCollectService : Protocal.ICommandCollectService
    //{
    //    private static readonly ILog Logger = LogManager.GetLogger(typeof(CommandCollectService));

    //    public void AddCommand(Protocal.Command command)
    //    {
    //        Logger.InfoFormat("Receive a command , type = {0}", command.GetType());

    //        if (command.Type == Protocal.CommandType.UpdateInstrumentStatus)
    //        {
    //            var status = JsonConvert.DeserializeObject<InstrumentsTradingStatus>(command.Content);
    //            Logger.InfoFormat("trade day = {0}", status.TradeDay);

    //            foreach (var eachState in status.States)
    //            {
    //                StringBuilder sb = new StringBuilder(50);
    //                var state = eachState.Key;
    //                var instruments = eachState.Value;
    //                sb.AppendFormat("state = {0}{1}", state.ToString(), Environment.NewLine);
    //                foreach (var eachInstrument in instruments)
    //                {
    //                    sb.AppendFormat("{0}  ", eachInstrument.ToString());
    //                }
    //                Logger.Info(sb.ToString());
    //            }
    //        }
    //        else if (command.Type == Protocal.CommandType.Quotation)
    //        {
    //            Protocal.QuotationCommand quotationCommand = command as Protocal.QuotationCommand;
    //            var overridedQs = quotationCommand.OverridedQs;
    //            var originQs = quotationCommand.OriginQs;
    //        }
    //    }


    //    public string Test()
    //    {
    //        return "TEST";
    //    }
    //}
}
