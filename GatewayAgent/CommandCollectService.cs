using Protocal.TradingInstrument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
namespace GatewayAgent
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal sealed class CommandCollectService : Protocal.ICommandCollectService
    {
        public void AddCommand(Protocal.Command command)
        {
            Console.WriteLine(string.Format("Receive a command , type = {0}", command.GetType()));

            if (command.Type == Protocal.CommandType.Quotation)
            {
                Protocal.QuotationCommand quotationCommand = command as Protocal.QuotationCommand;
                var overridedQs = quotationCommand.OverridedQs;
                var originQs = quotationCommand.OriginQs;
            }
        }


        public string Test()
        {
            return "TEST";
        }
    }
}
