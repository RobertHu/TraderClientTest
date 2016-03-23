using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
namespace TraceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            TraceSource source = new TraceSource("SourceTest");
            source.TraceEvent(TraceEventType.Error, 0, "Error");
            source.Flush();
            source.Close();
        }
    }
}
