using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace MakeOrderTester
{
    class PacketParser
    {
        private ConcurrentQueue<byte[]> _packetQueue = new ConcurrentQueue<byte[]>();
        private AutoResetEvent _parseEvent = new AutoResetEvent(false);
        private InvokeManager _invokeManager;
        public PacketParser(InvokeManager invokeManager)
        {
            _invokeManager = invokeManager;
            Thread thread = new Thread(Process)
            {
                IsBackground=true
            };
            thread.Start();
        }

        public void Enqueue(byte[] packet)
        {
            _packetQueue.Enqueue(packet);
            _parseEvent.Set();
        }

        private void Process()
        {
            while (true)
            {
                _parseEvent.WaitOne();
                while (!_packetQueue.IsEmpty)
                {
                    byte[] packet;
                    if (_packetQueue.TryDequeue(out packet))
                    {
                        if (packet[0] == 1)
                        {
                            continue;
                        }
                        int sessionLength = packet[PacketConstants.SessionLengthIndex];
                        int offset = PacketConstants.HeadLength + sessionLength;
                        string content = Encoding.UTF8.GetString(packet, offset, packet.Length - offset);
                        XElement contentXEle = XElement.Parse(content);
                        Guid invokeId =Guid.Parse(contentXEle.Element("InvokeId").Value);
                        var block = _invokeManager.Get(invokeId);
                        block.Result = contentXEle;
                        block.Wake();
                    }
                }
            }
        }
    }
}
