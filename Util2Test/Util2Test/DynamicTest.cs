using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Dynamic;
using System.Diagnostics;
using System.Reflection;
namespace Util2Test
{
    [TestFixture]
    public class DynamicTest
    {
        [Test]
        public void DynamicAddPropertyTest()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("name", "huhuabo");
            dict.Add("phone", "13631656931");
            StringBuilder sb = new StringBuilder();
            sb.Append("update dbo.Account set ");
            foreach(var pair in dict)
            {
                sb.AppendFormat("{0}='{1}' ", pair.Key, pair.Value);
            }
            sb.AppendFormat("where id = '{0}'", Guid.NewGuid());
            Debug.WriteLine(sb.ToString());
            Assert.IsEmpty(sb.ToString());
           
        }

        [Test]
        public void DateTimeTest()
        {
            Debug.WriteLine(DateTime.MinValue);
        }
        
    }
}
