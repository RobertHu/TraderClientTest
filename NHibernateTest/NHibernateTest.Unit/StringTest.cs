using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Diagnostics;
namespace NHibernateTest.Unit
{
    [TestFixture]
    public class StringTest
    {
        [Test]
        public void TrimTest()
        {
            string target = "[afdsfdsfddc,";
            string result = target.Trim(' ', '[', ']', '\\', ',');
            Console.WriteLine(result);
            Assert.AreEqual(target, result);
        }

        [Test]
        public void ExamineGenericType()
        {
            string typeName ="System.Collections.Generic.List`1[System.Int32],System.Collections.Generic.Dictionary`2[System.Int32,System.Int32]";
           // Console.WriteLine(typeName);
            foreach (var item in GenericTypesArguments(typeName, 2))
            {
                Console.WriteLine(item);
            }
            Assert.That(typeName != null);

        }

        private static IEnumerable<string> GenericTypesArguments(string s, int cardinality)
        {
            Debug.Assert(cardinality != 0);
            Debug.Assert(string.IsNullOrEmpty(s) == false);
            Debug.Assert(s.Length > 0);

            int startIndex = 0;
            while (cardinality > 0)
            {
                var sb = new StringBuilder(s.Length);
                int bracketCount = 0;

                for (int i = startIndex; i < s.Length; i++)
                {
                    switch (s[i])
                    {
                        case '[':
                            bracketCount++;
                            continue;

                        case ']':
                            if (--bracketCount == 0)
                            {
                                string item = s.Substring(startIndex + 1, i - startIndex - 1);
                                yield return item;
                                sb = new StringBuilder(s.Length);
                                startIndex = i + 2; // 2 = '[' + ']'
                            }
                            break;

                        default:
                            sb.Append(s[i]);
                            continue;
                    }
                }

                if (bracketCount != 0)
                {
                    throw new ArgumentException(string.Format("The brackets are unbalanced in the type name: {0}", s));
                }
                if (sb.Length > 0)
                {
                    var result = sb.ToString();
                    startIndex += result.Length;
                    yield return result.TrimStart(' ', ',');
                }
                cardinality--;
            }
        }


        
    }
}
