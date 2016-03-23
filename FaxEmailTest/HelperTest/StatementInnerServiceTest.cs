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
    public class StatementInnerServiceTest
    {
        [Test]
        public void Test()
        {
            Statement.StatementInnerService.Default.Initialize(GetData());
            var result = Statement.StatementInnerService.Default.GetData("", false);
            Assert.AreEqual(1, result.Keys.Count);
            Assert.IsTrue(result.ContainsKey("Subject"));
            Assert.AreEqual(3, result["Subject"].Keys.Count);
            LoggerHelper.Log(result["Subject"]["CHT"]);


        }



        public IEnumerable<string> GetData()
        {
            yield return "default|" + Path.Combine(Constants.EmailStyleDir, "data.xml");
            yield return "XYZ|" + Path.Combine(Path.Combine(Constants.EmailStyleDir, "XYZ"), "data.xml");
        }
    }
}
