using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaxAndEmail.Helper;
using NUnit.Framework;
using System.Diagnostics;
using FaxAndEmailService.Services;
using System.Threading;
using FaxAndEmail.Repository;
namespace iExchangeTest
{
    [TestFixture]
    public class StatementSettingTest
    {
        private string path = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService\Setting.xml";
        [TestFixtureSetUp]
        public void Init()
        {
            StatementSetting.StatementSettingManager.Default.Initialize(path);
            string isNeedNotify = StatementSetting.StatementSettingManager.Default.Get("NeedNotify");
            string time = StatementSetting.StatementSettingManager.Default.Get("Time");
            Debug.WriteLine(isNeedNotify);
            Debug.WriteLine(time);
            ProcessStatementResetService.Default.Initialize(isNeedNotify, time);
            SessionManager.InitializeLocalSessionFactory("data source=ws0301;initial catalog=Fax;user id=sa;password=Omni1234;Connect Timeout=300");
            SessionManager.InitializeV3SessionFactory("data source=ws0301;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=300");
        }

        [Test]
        public void Test()
        {
            //string statement = StatementSetting.StatementSettingManager.Default.GetStatementType();
            //Debug.WriteLine(statement);
            //Assert.IsNotEmpty(statement);
            //string proc = StatementSetting.StatementSettingManager.Default.GetStatementProcedure();
            //Debug.WriteLine(proc);
            //Assert.IsNotEmpty(proc);

            while (true)
            {
                Thread.Sleep(5000);
            }
        }

        [Test]
        public void TestBalanceGetCurrency()
        {
            string name = ProcessBalanceConfirmService.Default.GetCurrencyNameByCode("ddf", false);
            Assert.IsEmpty(name);
        }


        [Test]
        public void TestNotify()
        {
            var day = DateTime.Now.AddDays(1);
            ProcessStatementResetService.Default.Process(day);
        }
    }
}
