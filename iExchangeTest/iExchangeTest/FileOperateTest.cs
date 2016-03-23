using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using System.Diagnostics;
using System.Xml.Linq;
namespace iExchangeTest
{
    enum Nons
    {
        SHE,
        HEI,
        ME,
        YOUR
    }
    //[TestFixture]
    //public class FileOperateTest
    //{
    //    [Test]
    //    public void GetContentTest()
    //    {
    //       string content= File.ReadAllText(@"D:\c++Project\WebApplication1\WebApplication1\template.htm");
    //       Debug.Write(content);
    //       Assert.NotNull(content);
    //    }

    //    [Test]
    //    public void EnumTest()
    //    {
    //        Nons target = Nons.ME;
    //        int x = (int)target;
    //        Debug.WriteLine(target);
    //        Debug.WriteLine(x);
    //        Assert.AreEqual(2, x);
          
    //    }

    //    [Test]
    //    public void GeneratedHTMLContentTest()
    //    {
    //        string filePath = @"D:\c++Project\WebApplication1\WebApplication1\template.htm";
    //        XNamespace xnp = XNamespace.Get("http://www.w3.org/1999/xhtml");
    //        string destPath = @"d:\Try\my.htm";
    //        File.Copy(filePath, destPath, true);
    //        XElement root = XElement.Load(destPath);
    //        XElement target = root.Element(xnp + "body");
    //        Assert.IsNotNull(target);
    //        target.SetValue("My  Html");
    //        root.Save(destPath);
    //    }
    //}
}
