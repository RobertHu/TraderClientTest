using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Test2
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteToFile();
            ReadFromFile();
            Console.Read();
        }

        private static void ReadFromFile()
        {
            DataSet ds = new DataSet();
            ds.ReadXml("my.xml",XmlReadMode.ReadSchema);
            foreach (DataRow dr in ds.Tables["Account"].Rows)
            {
                Console.WriteLine(string.Format("{0}   {1}", (Guid)dr["ID"], (string)dr["Code"]));
            }


            foreach (DataRow dr in ds.Tables["Instrument"].Rows)
            {
                Console.WriteLine(string.Format("{0}   {1}", (Guid)dr["ID"], (string)dr["Code"]));
            }
        }
        private static void WriteToFile()
        {
            DataTable accountTable = new DataTable("Account");
            accountTable.Columns.Add("ID", typeof(Guid));
            accountTable.Columns.Add("Code", typeof(string));

            accountTable.PrimaryKey = new DataColumn[] { accountTable.Columns["ID"] };

            DataTable instrumentTable = new DataTable("Instrument");
            instrumentTable.Columns.Add("ID", typeof(Guid));
            instrumentTable.Columns.Add("Code", typeof(string));
            instrumentTable.PrimaryKey = new DataColumn[] { instrumentTable.Columns["ID"] };

            DataSet ds = new DataSet("data");
            ds.Tables.Add(accountTable);
            ds.Tables.Add(instrumentTable);

            var accountData = Enumerable.Range(0, 10).Select(m => string.Format("account{0}", m));

            var instrumentData = Enumerable.Range(0, 10).Select(m => string.Format("instrument{0}", m));

            foreach (var item in accountData)
            {
                DataRow dr = accountTable.NewRow();
                dr["ID"] = Guid.NewGuid();
                dr["Code"] = item;
                accountTable.Rows.Add(dr);
            }

            foreach (var item in instrumentData)
            {
                DataRow dr = instrumentTable.NewRow();
                dr["ID"] = Guid.NewGuid();
                dr["Code"] = item;
                instrumentTable.Rows.Add(dr);
            }

            ds.WriteXml("my.xml",XmlWriteMode.WriteSchema);

        }
    }
}
