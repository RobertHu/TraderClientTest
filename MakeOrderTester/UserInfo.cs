using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeOrderTester
{
    class UserInfo
    {
        private string _session = string.Empty;
        public string Session
        {
            get
            {
                return _session;
            }
            set
            {
                _session = value;
            }
        }
    }
}
