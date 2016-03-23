using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.ServerServiceClient service = new ServiceReference1.ServerServiceClient();
            Guid orderId = Guid.Parse("5D1AEB3B-5C1E-4944-8F4B-00ECF27A9383");
            var data = service.GetInitData(new Guid[] { Guid.Parse("47faed76-250c-4ce8-b7c8-fb68bf8d0771"), Guid.Parse("078b9c4e-c8e1-42ad-acfb-2652a13f1894") });


        }
    }
}
