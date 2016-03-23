using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonUtil
{
    public static class IntExtenstion
    {
        private static int _Mask= 0x000000FF;
        private static int _ByteLength = 8;
        public static byte[] ToCustomerBytes(this int source)
        {
            int part1 = GetTheLowerByte(source);
            source >>= _ByteLength;
            int part2 = GetTheLowerByte(source);
            source >>= _ByteLength;
            int part3 = GetTheLowerByte(source);
            source >>= _ByteLength;
            int part4 = GetTheLowerByte(source);
            return new[]{(byte)part1,(byte)part2,(byte)part3,(byte)part4};
            
        }

        private static int GetTheLowerByte(int source)
        {
            return source & _Mask;

        }
    

    }
}
