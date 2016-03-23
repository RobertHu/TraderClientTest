using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace Util2Test
{

    enum myData
    {
        None,
        First,
        Second,
        Third
    }

    [TestFixture]
    public class DefaultKeyWordsTest
    {
        [Test]
        public void GUIDTest()
        {
            var expected = default(Guid);
            Assert.AreEqual(Guid.Empty, expected);
        }

        [Test]
        public void DefualtEnumValueTest()
        {
            var data = default(myData);
            Assert.AreEqual(myData.None, data);
        }

        [Test]
        public void EncodeCotentTest()
        {
            string input = "风险级别从1，变为2";
            string pattern = "[[a-zA-Z0-9,.!?，。？、！<>{}]";
            Regex regex=new Regex(pattern);
            StringBuilder sb = new StringBuilder();
            foreach (var item in input)
            {
                if (regex.IsMatch(item+string.Empty)==false)
                {
                    Debug.WriteLine(item);
                    sb.Append(UnicodeEncode(item));
                }
                else
                {
                    sb.Append(item);
                }
            }
            Debug.WriteLine(sb.ToString());
            Assert.AreEqual(input, sb.ToString());

        }


        [Test]
        public void HTMLEncodeTest()
        {
            string target1 = "网";
           // string target2 = "頁";
            byte[] bytes1 = Encoding.Unicode.GetBytes(target1);
            //byte[] bytes2 = Encoding.Unicode.GetBytes(target2);
            int result1 = ConvertLittleEndian(bytes1);
            //int result2 = ConvertLittleEndian(bytes2);
            Debug.WriteLine(string.Format("网Unicodes  {0}", result1));
            //Debug.WriteLine(string.Format("页Unicodes  {0}", result2));
            Assert.AreEqual(32178, result1);
            //Assert.AreEqual(38913, result2);
        }

        [Test]
        public void IpPortRangeTest()
        {
            int i = 2 << 15;
            Debug.WriteLine(i);
            Assert.AreEqual(65536, i);
        }


        [Test]
        public void DateTimeCombineTest()
        {
            string date = "2012-07-25";
            string time = "16:12";
            string datetime = string.Format("{0} {1}",date,time);
            DateTime actrue;
            bool result = DateTime.TryParse(datetime,out actrue);
            Assert.IsTrue(result);
            Assert.AreEqual(datetime, actrue.ToString());
        }



        private string UnicodeEncode(char input)
        {
            byte[] byteArray = Encoding.Unicode.GetBytes(new char[]{input});
            int result = ConvertLittleEndian(byteArray);
            return DecorateUnicodeChar(result.ToString());
        }


        private string DecorateUnicodeChar(string input)
        {
            return string.Format("%26%23{0}%3B",input);
        }


        private int ConvertLittleEndian(byte[] array)
        {
            int pos = 0;
            int result = 0;
            foreach (byte by in array)
            {
                Debug.WriteLine(by);
                result |= (int)(by << pos);
                //Debug.WriteLine(result);
                Debug.WriteLine(pos);
                pos += 8;
                
            }
            return result;
        }




    }
}
