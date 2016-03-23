using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtil
{
   public static class ByteArrayExtension
    {
       private static int _ByteLength = 8;
       public static int ToCustomerInt(this byte[] source)
       {
           if (source == null) throw new ArgumentNullException("Source");
           if (source.Length != 4) throw new ArgumentOutOfRangeException("source.Length");
           int result = 0;
           for (int i = 0; i < source.Length; i++)
           {
              result += source[i] << (i*_ByteLength);
           }
           return result;
       }
    }
}
