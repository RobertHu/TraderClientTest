using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderClient.Reset
{
    internal sealed class DBRepository
    {
        internal static readonly DBRepository Default = new DBRepository();

        private string _conncstr = "data source=ws0202;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=60";

        static DBRepository() { }
        private DBRepository() { }

        internal DataSet GetInitData(string transactionServerName)
        {
            return DatabaseHelper.GetInitData(transactionServerName, _conncstr) ;
        }

        internal DataSet GetTransactionServerData()
        {
            return DatabaseHelper.GetTransactionServerData(_conncstr);
        }

        internal DataSet GetOrganizationAndOrderTypeData()
        {
            return DatabaseHelper.GetOrganizationAndOrderTypeData(_conncstr);
        }

        internal DataSet GetHistorySettings(DateTime tradeDay)
        {
            string[] tableNames = {
                                      "Currency",
                                      "CurrencyRate",
                                      "QuotePolicyDetail",
                                      "TradePolicy",
                                      "TradePolicyDetail",
                                      "DealingPolicyDetail",
                                      "VolumeNecessary",
                                      "VolumeNecessaryDetail",
                                      "InstalmentPolicyDetail",
                                      "Customer",
                                      "Account",
                                      "Instrument",
                                      "TradeDay"
                                 };
            return DatabaseHelper.GetHistorySettings(tradeDay, _conncstr,  tableNames);
        }

        internal DataRow GetTradeDaySettingData(Guid instrumentId, Guid accountId, DateTime tradeDay)
        {
            var data = DatabaseHelper.GetInstrumentHistorySettings(instrumentId, accountId, tradeDay, _conncstr);
            return data.Tables[0].Rows[0];
        }


    }
}
