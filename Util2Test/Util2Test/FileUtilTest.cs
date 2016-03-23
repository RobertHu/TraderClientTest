using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
namespace Util2Test
{
    [TestFixture]
    public class FileUtilTest
    {
        [Test]
        public void FileHiddenTest()
        {
            var path = @"D:\Teams\iExchangeCollection\iExchange3 Team\iExchange3\FaxAndEmailService\UserStorage.xml";
            FileInfo fi = new FileInfo(path);
            fi.Attributes =fi.Attributes|FileAttributes.Hidden;

        }
    }
}
