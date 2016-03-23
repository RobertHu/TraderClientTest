using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaxAndEmail.Helper;
using NUnit.Framework;
namespace HelperTest
{
    [TestFixture]
    public  class SMSTitleSettingTest
    {
        [Test]
        public void Test()
        {
            SMS.SMSTitleManager.Default.Initialize(Constants.SettingPath);
            var result1 = SMS.SMSTitleManager.Default.Get("PT");
            Assert.AreEqual("sss", result1);
            var result2 = SMS.SMSTitleManager.Default.Get("DEM");
            Assert.AreEqual("ZZZ", result2);
        }
    }
}
