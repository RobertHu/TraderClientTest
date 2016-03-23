using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FaxAndEmail.Helper;
using NUnit.Framework;
using System.IO;
namespace HelperTest
{
    [TestFixture]
    public class PasswordInnerserviceTest
    {
        [Test]
        public void LoadMacroDataTest()
        {
            string dataPath = "default|" + Path.Combine(Constants.EmailStyleDir, "data.xml");
            string smsDataPath ="default|" + Path.Combine(Constants.SmsStyleDir, "data.xml");
            string macroPath = Path.Combine(Constants.EmailStyleDir, "macro.xml");
            Password.PasswordInnerService.Default.Initialize(macroPath, new[] { dataPath }, new[] { smsDataPath });


        }
    }
}
