using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactionServerTester.TypeExtension
{
    internal static class DateTimeExtension
    {
        internal static string ToStandardString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
