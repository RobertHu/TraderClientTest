using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlClient;

namespace TraderClient
{
    internal sealed class Account
    {
        public Guid AccountId { get; set; }
        public string AccountCode { get; set; }
    }

    internal static class DapperTest
    {
        public static void Test()
        {
            using (SqlConnection conn = new SqlConnection("data source=localhost;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=60"))
            {
                conn.Open();
                var results = conn.Query<Account>("select ID as AccountId, Code as AccountCode from dbo.Account");
                Console.WriteLine(results.Count());
                var first = results.First();
                Console.WriteLine(string.Format("id={0}, code={1}", first.AccountId, first.AccountCode));
            }
        }

        public static void TestExecuteSql()
        {
            using (SqlConnection conn = new SqlConnection("data source=localhost;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=60"))
            {
                conn.Open();
                var results = conn.Query<Guid>(@"DECLARE @id UNIQUEIDENTIFIER
SELECT @id= ID  FROM dbo.Account where Code = '507257'
SELECT @id");
                Console.WriteLine(results.Single());
            }
        }

    }
}
