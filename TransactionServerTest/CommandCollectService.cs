using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionServerTest
{
    public sealed class CommandCollectService : Protocal.ICommandCollectService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CommandCollectService));

        public void AddCommand(Protocal.Command command)
        {
            Logger.Info("receive a command");
        }
    }
}
