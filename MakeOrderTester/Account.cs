using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeOrderTester
{
    class Account
    {
        public Account(string id, string code, string customerId)
        {
            this.Id = id;
            this.Code = code;
            this.CustomerId = customerId;
        }

        public string Id { get; private set; }
        public string Code { get; private set; }
        public string CustomerId { get; private set; }
    }
}
