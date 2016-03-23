using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakeOrderTester
{
    static class PacketConstants
    {
        public const int HeadLength = 6;
        public const int SettingIndex = 0;
        public const int SessionLengthIndex = 1;
        public const int ContentLengthIndex = 2;
        public const int InvokeIdLength = 36;
        public const int ContentLengthBytesCount = 4;
    }
}
