using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FaxAndEmailService;
using FaxAndEmailService.Common;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using FaxAndEmail.Repository;
using FaxAndEmail.Repository.Util;
namespace iExchangeTest
{
    //[TestFixture]
    //public class OrderStyleTest
    //{
    //    [Test]
    //    public void GetContentTest()
    //    {
    //        DateTime executeTime = DateTime.Now;
    //        var accountName = "RobertHu";
    //        var accountCode = "BOHu";
    //        var orderCode = "034455";
    //        var instrumentDesc = "USD/JPY";
    //        decimal buyorSell = 653.25m;
    //        var isOpen = true;
    //        var isBuy = true;
    //        decimal commission = 0.564m;
    //        var organizationName = "Omnicare";
    //        var language = "CHS";
    //        decimal executePrice = 3.56m;
    //        string body ="" //Helper.GeneratedOrderExecutedContent(executeTime, accountName, accountCode, orderCode, instrumentDesc, buyorSell, isOpen, isBuy, commission, organizationName, language, executePrice.ToString());
    //        Debug.WriteLine(body);
    //        Assert.IsNotNull(body);
    //        string filePath = FileUtil.GetEmailPersistenceFilePath(executeTime, FaxAndEmailService.Model.BusinessEnum.OrderExecuted);
    //        Debug.WriteLine(filePath);
    //        Assert.IsNotNull(filePath);
    //        FileUtil.CopyAndSetHTMLContent(filePath, body);

    //    }

    //    [Test]
    //    public void OrderTest()
    //    {
    //        FaxEmailServiceManager.Default.NotifyOrderExecutedDetail(Guid.Parse("F420B7E2-CD75-41DD-A829-A45E5685B88C"));
    //    }

    //    [Test]
    //    public void RiskLevelChangedTest()
    //    {
    //        FaxEmailServiceManager.Default.NotifyRiskLevelChanged(Guid.Parse("1B6346CD-3348-4453-947B-E5BBE046C578"), DateTime.Now, 2);
    //    }

        
    //}
}
