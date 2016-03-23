using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaxAndEmail.Helper;
using NUnit.Framework;
namespace HelperTest
{
    [TestFixture]
    public class SettingConfigTest
    {
        [Test]
        public void TimeTest()
        {
            StatementSetting.StatementSettingManager.Default.Initialize(Constants.SettingPath);
            var result = StatementSetting.StatementSettingManager.Default.Get("Time");
            Assert.NotNull(result);
            LoggerHelper.Log(result);
        }
    }
}
