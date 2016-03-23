using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using CommonUtil;
using System.Collections.Concurrent;

namespace MakeOrderTester
{
    class CommunicationClient:IDisposable
    {
        private SslStream _sslStream;
        private const int BufferSize = 4096;
        private byte[] _buffer = new byte[BufferSize];
        private byte[] _headBuffer = new byte[PacketConstants.HeadLength];
        private WriteQueue _writeQueue;
        private PacketParser _packetParser;

        public CommunicationClient(string hostName, int port,InvokeManager invokeManager)
        {
            _packetParser = new PacketParser(invokeManager);
            TcpClient client = new TcpClient(hostName, port);
            _sslStream = new SslStream(client.GetStream(), false, ValidateServerCertificate, null);
            try
            {
                _sslStream.AuthenticateAsClient(hostName);
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine(ex.Message);
                _sslStream.Close();
            }
            _writeQueue = new WriteQueue(this);
            Thread readThread = new Thread(this.Read)
            {
                IsBackground=true
            };
            readThread.Start();
        }

        public void Write(byte[] packet)
        {
            _writeQueue.Enqueue(packet);
        }

        private void BeginWrite(byte[] packet)
        {
            _sslStream.BeginWrite(packet, 0, packet.Length, this.EndWrite, null);
        }

        private void EndWrite(IAsyncResult asyncResult)
        {
            try
            {
                _sslStream.EndWrite(asyncResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void Read()
        {
            while (true)
            {
                int readedHeadCount = 0;
                while (readedHeadCount != PacketConstants.HeadLength)
                {
                    readedHeadCount += _sslStream.Read(this._headBuffer, readedHeadCount, PacketConstants.HeadLength - readedHeadCount);
                }
                int packetLength = GetPacketLength();
                int contentLength = packetLength - PacketConstants.HeadLength;
                if (_buffer.Length < contentLength)
                {
                    _buffer = new byte[contentLength];
                }
                int readedContentCount = 0;
                while (readedContentCount != contentLength)
                {
                    readedContentCount += _sslStream.Read(_buffer, readedContentCount, contentLength - readedContentCount);
                }
                byte[] packet = new byte[packetLength];
                Buffer.BlockCopy(_headBuffer, 0, packet, 0, PacketConstants.HeadLength);
                Buffer.BlockCopy(_buffer, 0, packet, PacketConstants.HeadLength, contentLength);
                _packetParser.Enqueue(packet);
            }
        }

        private int GetPacketLength()
        {
            int sessionLenght = _headBuffer[PacketConstants.SessionLengthIndex];
            byte[] contentLengthBytes = new byte[PacketConstants.ContentLengthBytesCount];
            Buffer.BlockCopy(_headBuffer, PacketConstants.ContentLengthIndex, contentLengthBytes, 0, PacketConstants.ContentLengthBytesCount);
            int contentLength = contentLengthBytes.ToCustomerInt();
            int packetLength = PacketConstants.HeadLength + sessionLenght + contentLength;
            return packetLength;
        }


        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public void Dispose()
        {
            _sslStream.Close();
        }

        private class WriteQueue
        {
            private ConcurrentQueue<byte[]> _packetQueue = new ConcurrentQueue<byte[]>();
            private AutoResetEvent _writeEvent = new AutoResetEvent(false);
            private CommunicationClient _client;
            public WriteQueue(CommunicationClient client)
            {
                _client = client;
                Thread writeThread = new Thread(ProcessPacket)
                {
                    IsBackground=true
                };
                writeThread.Start();
            }

            public void Enqueue(byte[] packet)
            {
                _packetQueue.Enqueue(packet);
                _writeEvent.Set();
            }


            private void ProcessPacket()
            {
                while (true)
                {
                    _writeEvent.WaitOne();
                    while (!_packetQueue.IsEmpty)
                    {
                        byte[] packet;
                        if (_packetQueue.TryDequeue(out packet))
                        {
                            _client.BeginWrite(packet);
                        }
                    }
                }
            }
        }

    }
}
