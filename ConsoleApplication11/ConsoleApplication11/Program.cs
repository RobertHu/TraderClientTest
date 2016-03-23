using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication11
{
    class Program
    {
        static  void Main(string[] args)
        {
            unsafe
            {
                int[] data ={1,2,3,4,5,6,7,8,9,10 };
                fixed (int* pdata = data)
                for (int i = 0; i < data.Length; i++)
                {
                    Console.Write(string.Format("{0}   ", *(pdata + i)));
                }
            }
            Console.Read();
        }
    }
}
