using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace HelperTest
{
   public class LoggerHelper
    {
       public static void Log(string msg)
       {
           Trace.WriteLine(msg);
       }
    }
}
