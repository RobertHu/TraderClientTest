using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmailService;
using FaxAndEmail.Repository;
using FaxAndEmail.Helper;
using FaxAndEmail.Repository.Util;
using iExchange.Common;
using FaxAndEmailService.Services;
namespace Util2Test
{
    [TestFixture]
    public class PasswordResetServiceTest
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            SessionManager.InitializeLocalSessionFactory("data source=ws0301;initial catalog=Fax;user id=sa;password=Omni1234;Connect Timeout=300");
            SessionManager.InitializeV3SessionFactory("data source=ws0301;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=300");
            SMSServer.SMSManager.Default.Initialize(PathUtil.GetFilePath("Setting.xml"));
        }

        [Test]
        public void StartTest()
        {
            PasswordResetInfo info=new PasswordResetInfo(Guid.Parse("CCA3961B-21B3-451D-9525-3DC5EB7A7C05"),"RobertHu","1213545");
            ProcessPasswordResetService.Default.Process(info);
        }
    }
}
