using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace ConsoleApplication8
{
  public class ThreadDemo
    {
      private readonly object objBlock = new object();
      public void Call1()
      {
          lock (this.objBlock)
          {
              Console.WriteLine("Step1");
              ThreadPool.QueueUserWorkItem(o =>
              {
                  lock (this.objBlock)
                  {
                      Console.WriteLine("Step2");
                  }
              });
              Console.ReadLine();
              
          }
      }
    }
}
