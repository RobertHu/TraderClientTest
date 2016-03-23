using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmail.Helper;
using System.IO;
using System.Diagnostics;
namespace Util2Test
{
    [TestFixture]
    public class BalanceInnerService
    {
        private Balance.BalanceInnerService service;
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.service = new Balance.BalanceInnerService(ConstantString.DataPath, ConstantString.MacroPath);
        }

        [Test]
        public void TranslateTest()
        {
            string language = "ENG";
            string path = Path.Combine(ConstantString.FaxStyleDir, string.Format("Balance.Style.{0}.txt", language));
            string[] parameters = {string.Empty,"Bill","Jobs","HKD","888.88",DateTime.Now.ToString(),"Ominicare" };
            string result = this.service.Translate(path, language, parameters, false);
            Debug.WriteLine(result);
            Assert.IsNotNull(result);
        }
    }
}
