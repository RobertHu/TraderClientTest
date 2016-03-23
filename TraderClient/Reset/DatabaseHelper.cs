using iExchange.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderClient.Reset
{
    public static class DatabaseHelper
    {
        private sealed class DataParams
        {
            internal DataParams(System.Data.CommandType type, Dictionary<string, object> sqlParams)
            {
                this.Type = type;
                this.SqlParams = sqlParams;
            }

            internal System.Data.CommandType Type { get; private set; }
            internal Dictionary<string, object> SqlParams { get; private set; }
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof(DatabaseHelper));

        public static DataSet GetInitData(string transactionServerID, string connectionString, DateTime? tradeDay = null)
        {
            Dictionary<string, object> sqlParams = new Dictionary<string, object>()
            {
                {"@transactionServerID", transactionServerID},
                {"@tradeDay", tradeDay==null? null : (object)tradeDay.Value}
            };
            DataParams dataParams = new DataParams(CommandType.StoredProcedure, sqlParams);
            return GetDataCommon("dbo.P_GetInitDataForTransactionServer", connectionString, GetTableNames(), dataParams);
        }

        internal static DataSet GetTransactionServerData(string connectionString)
        {
            return GetDataCommon("[Trading].[GetTransactionServerData]", connectionString, new string[] { "OrderInstalment", "OrderDayHistory" }, null);
        }


        internal static DataSet GetHistorySettings(DateTime tradeDay, string connectionString, string[] tableNames)
        {
            Dictionary<string, object> sqlParams = new Dictionary<string, object>()
            {
                {"@tradeDay", tradeDay}
            };
            DataParams dataParams = new DataParams(CommandType.StoredProcedure, sqlParams);
            return GetDataCommon("Trading.[GetSettingsHistory]", connectionString, tableNames, dataParams);
        }

        internal static DataSet GetInstrumentHistorySettings(Guid instrumentId, Guid accountId, DateTime tradeDay, string connectionString)
        {
            Dictionary<string, object> sqlParams = new Dictionary<string, object>()
            {
                {"@accountId", accountId},
                {"@instrumentId", instrumentId},
                {"@tradeDay", tradeDay}
            };
            DataParams dataParams = new DataParams(CommandType.StoredProcedure, sqlParams);
            return GetDataCommon("Trading.[GetInstrumentSettingsHistory]", connectionString, null, dataParams);
        }


        private static string[] GetTableNames()
        {
            return new string[]{
													 "TradeDay",
													   "SystemParameter",
													   "Currency",
                                                       "CurrencyRate",
													   "Instrument",
                                                       "QuotePolicyDetail",
													   "TradingTime",
													   "TradePolicy",
													   "TradePolicyDetail",
                                                       "InstalmentPolicy",
                                                       "InstalmentPolicyDetail",
                                                       "SpecialTradePolicy",
					                                   "SpecialTradePolicyDetail",
                                                       "VolumeNecessary",
                                                       "VolumeNecessaryDetail",
                                                       "PhysicalPaymentDiscount",
                                                       "PhysicalPaymentDiscountDetail",
                                                       "DealingPolicy",
                                                       "DealingPolicyDetail",
                                                       "BinaryOptionBetType",
                                                       "BinaryOptionPolicyDetail",
													   "Customer",
													   "Account",
                                                       "UnclearDeposit",
                                                       "DayQuotation",
													   "OriginQuotation",
													   "OverridedQuotation",
													   "AccountEx",
													   "AccountBalance",
													   "Transaction",
													   "Order",
                                                       "OrderPLNotValued",
													   "OrderRelation",
                                                       "SettlementPrice",
                                                       "DeliveryRequest",
                                                       "DeliveryRequestOrderRelation",
                                                       "PriceAlert",
                                                       "OrderInstalment"
                                                       //"OrderDayHistory"
												   };
        }

        public static DataSet GetOrganizationAndOrderTypeData(string connectionString)
        {
            return GetDataCommon("select ID, Code from dbo.Organization select ID, Code from dbo.OrderType", connectionString, new string[] { "Organization", "OrderType" }, null);
        }

        private static DataSet GetDataCommon(string sql, string connectionString, string[] tableNames, DataParams dataParams)
        {
            DataSet result = GetData(sql, connectionString, dataParams);
            CheckTableNames(result, tableNames);
            SetTableNames(result, tableNames);
            return result;
        }

        private static void SetTableNames(DataSet data, string[] tableNames)
        {
            if (tableNames == null) return;
            for (int i = 0; i < data.Tables.Count; i++)
            {
                data.Tables[i].TableName = tableNames[i];
            }
        }

        private static DataSet GetData(string sql, string connectionString, DataParams dataParams)
        {
            SqlCommand command = new SqlCommand(sql);
            if (dataParams != null)
            {
                command.CommandType = dataParams.Type;
                foreach (var eachPair in dataParams.SqlParams)
                {
                    command.Parameters.AddWithValue(eachPair.Key, eachPair.Value);
                }
            }
            command.Connection = new SqlConnection(connectionString);
            var adapter = new SqlDataAdapter(command);
            var result = new DataSet();
            adapter.Fill(result);
            return result;
        }


        private static void CheckTableNames(DataSet data, string[] tableNames)
        {
            if (tableNames == null) return;
            if (data.Tables.Count < tableNames.Length)
            {
                throw new TransactionException(TransactionError.DatabaseDataIntegralityViolated);
            }
        }


    }
}
