using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace ConsoleApplication14
{
    class Program
    {
        static void Main(string[] args)
        {
            FaxEmail.FaxEmailService service = new FaxEmail.FaxEmailService();
            service.NotifyResetStatement(new DateTime(2012,8,18));

        }
    }
}
