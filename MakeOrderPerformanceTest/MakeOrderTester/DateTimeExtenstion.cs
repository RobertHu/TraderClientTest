using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeOrderTester
{
    static class DateTimeExtenstion
    {
        public static string ToStandrandFormat(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
