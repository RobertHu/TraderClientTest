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
    public class PasswordResetTest
    {
        private Password.PasswordInnerService service;
        [TestFixtureSetUp]
        public void Initailize()
        {
            this.service = new Password.PasswordInnerService(ConstantString.DataPath, ConstantString.MacroPath);
        }

        [Test]
        public void Translate()
        {
            string language = "CHT";
            string path = Path.Combine(ConstantString.FaxStyleDir, string.Format("Pwd.Style.{0}.txt", language));
            string[] parameters = {string.Empty,"Robert","Robert Hu","12345678","Omnicare" };
            string result = this.service.Translate(path, parameters);
            Debug.WriteLine(result);
            Assert.IsNotNull(result);
        }
    }
}
