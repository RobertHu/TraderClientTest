using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CommonUtil;

namespace MakeOrderTester
{
    class PacketBuilder
    {
        public static byte[] Build(XElement content,string session)
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(content.ToString());
            byte[] contentLengthBytes = contentBytes.Length.ToCustomerBytes();
            byte[] sessionBytes = Encoding.ASCII.GetBytes(session);
            int sessionLength = sessionBytes.Length;
            byte[] packet = new byte[PacketConstants.HeadLength + sessionLength+ contentBytes.Length];
            packet[0] = 0;
            packet[PacketConstants.SessionLengthIndex] = (byte)sessionLength;
            Buffer.BlockCopy(contentLengthBytes, 0, packet, PacketConstants.ContentLengthIndex, contentLengthBytes.Length);
            Buffer.BlockCopy(contentBytes, 0, packet, PacketConstants.HeadLength + sessionLength, contentBytes.Length);
            return packet;
        }
    }
}
