using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using NHibernate;
using FaxAndEmail.V3Model;
using System.Data;
using System.Diagnostics;
namespace Util2Test
{
    [TestFixture]
    public class NhibernateProcedureTest
    {

        [Test]
        public void V3Test()
        {
            //SessionManager.InitializeV3SessionFactory("data source=testdb;initial catalog=iExchange_V3;user id=sa;password=Omni1234;Connect Timeout=300");
            //using (ISession session = SessionManager.OpenV3Session())
            //{
            //    Guid? accountid = new Guid("605E5C29-11F7-480B-98E8-003F29F8C6AA");
            //    var data = session.GetNamedQuery("FaxEmail_GetAccountInfo")
            //               .SetParameter("AccountID", accountid,NHibernateUtil.Guid).Future<AccountOrganizationLanguageInfo>();
            //    foreach (var item in data)
            //    {
            //        Debug.WriteLine(item.Language);
            //    }
            //    Assert.IsNotNull(data);
                
            //}
        }
    }
}
