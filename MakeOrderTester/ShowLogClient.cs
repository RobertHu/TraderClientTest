using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace MakeOrderTester
{
    class ShowLogClient
    {
        private NetworkStream _stream;
        private byte[] _buffer = new byte[1024];
        public ShowLogClient(string hostName, int port)
        {
            TcpClient client = new TcpClient(hostName, port);
            _stream = client.GetStream();
            _stream.BeginRead(this._buffer, 0, this._buffer.Length, ar =>
            {
                try
                {
                    _stream.EndRead(ar);
                }
                catch
                {
                }
            }
            , null);
        }

        public void Write(string message)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            _stream.BeginWrite(bytes, 0, bytes.Length, this.EndWrite, null);
        }
        public void EndWrite(IAsyncResult asyncResult)
        {
            try
            {
                _stream.EndWrite(asyncResult);
            }
            catch
            {
            }
        }

    }
}
