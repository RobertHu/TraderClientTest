using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using iExchange.Common;
using log4net;
using FlexInterface.Common;
using FlexInterface.Common;

using FlexInterface.Common;

using FlexInterface.Common;

using FlexInterface.Common;
using FlexInterface.Common;
using FlexInterface.Common;
using FlexInterface.Common;


namespace FlexInterfaceService
{
    public class AccountService
    {
        private ILog _Log = LogManager.GetLogger(typeof(AccountService));
        public string[] GetAllAccountCode()
        {
            DataSet ds = DataAccess.GetData("select MT4LoginId from dbo.Account order by MT4LoginId", ConfigHelper.ConnectionString);
            List<string> list = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(((int)dr["MT4LoginId"]).ToString());
                list.Add(((int)dr["MT4LoginId"]).ToString()); list.Add(((int)dr["MT4LoginId"]).ToString());
                list.Add(((int)dr["MT4LoginId"]).ToString());
                list.Add(((int)dr["MT4LoginId"]).ToString());
              
            }
            return list.ToArray();

        }

        public int? GetExchangePlAccount()
        {
            string sql = "select top 1 MT4LoginId from dbo.AccountingMapping where MappingType=0";
            var obj = DataAccess.ExecuteScalar(sql, ConfigHelper.ConnectionString);
            if (obj == null)
            {
                _Log.WarnFormat("GetExchangePlAccount  sql:{0}", sql);
                return null;
            }
            return (int)obj;
        }

        public Dictionary<string, List<Tuple<int,string>>> GetAllAccountGroupWithAccounts()
        {
            string sql = "select g.Code,a.MT4LoginId,AccountCode=a.Code from dbo.[Group] g join dbo.GroupMembership gms on g.ID=gms.GroupID join dbo.Account a on a.ID=gms.MemberID where g.GroupType='Account' group by g.Code,a.MT4LoginId,a.Code order by g.Code,a.MT4LoginId";
            DataSet ds = DataAccess.GetData(sql, ConfigHelper.ConnectionString);
            Dictionary<string, List<Tuple<int, string>>> dict = new Dictionary<string, List<Tuple<int, string>>>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string groupCode = (string)dr["Code"];
                int mt4loginId = (int)dr["MT4LoginId"];
                string accountCode = (string)dr["AccountCode"];
                if (!dict.ContainsKey(groupCode))
                {
                    dict[groupCode] = new List<Tuple<int, string>>();
                }
                dict[groupCode].Add(Tuple.Create(mt4loginId,accountCode));
            }
            
            return dict;
        }

        public int? GetAccountingMapping(Tuple<int, Guid, Guid> key)
        {
            try
            {
                string sql = string.Format("select MT4LoginId from dbo.AccountingMapping where MappingType='{0}' and InstrumentId='{1}' and CurrencyId='{2}'", key.Item1, key.Item2, key.Item3);
                var obj = DataAccess.ExecuteScalar(sql, ConfigHelper.ConnectionString);
                if (obj == null)
                {
                    _Log.WarnFormat("GetAccountingMapping  sql:{0}", sql);
                    return null;
                }
                return (int)obj;
            }
            catch (Exception e)
            {
                _Log.ErrorFormat("GetAccountingMapping {0}", e);
                return null;
            }
        }


        public string[] GetAllFundNo(DateTime fromTradeDay, DateTime toTradeDay, AccountInfo mt4Login)
        {
            string sql = string.Format("dbo.Flex_GetDepositCode '{0}','{1}','{2}','{3}','{4}'", fromTradeDay, toTradeDay, mt4Login.AccountStr, mt4Login.FromAccountNo, mt4Login.ToAccountNo);
            this._Log.InfoFormat("GetAllFundNo sql:{0}", sql);
            DataSet ds = DataAccess.GetData(sql, ConfigHelper.ConnectionString);
            this._Log.InfoFormat("GetAllFundNo count:{0}", ds.Tables[0].Rows.Count);
            List<string> list = new List<string>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add((string)dr["Code"]);
            }
            return list.ToArray();
        }
    }
}