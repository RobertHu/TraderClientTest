using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;

namespace MakeOrderTester
{
    class RequestBlock
    {
        private Guid _invokeId;
        private AutoResetEvent _signal = new AutoResetEvent(false);
        private bool _isError;
        private XElement _result;
        public RequestBlock(Guid invokeId)
        {
            _invokeId = invokeId;
            _isError = false;
        }

        public XElement Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                _isError = _result.Descendants().Any(m => m.Name == "error");
            }
        }

        public Guid InvokeId
        {
            get
            {
                return _invokeId;
            }
        }

        public void Wait()
        {
            _signal.WaitOne();
        }

        public void Wake()
        {
            _signal.Set();
        }


        public bool IsError
        {
            get
            {
                return _isError;
            }
        }
    }
}
