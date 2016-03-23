using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MakeOrderTester
{
    class InvokeManager
    {
        private ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();
        private Dictionary<Guid, RequestBlock> _dict = new Dictionary<Guid, RequestBlock>();
        public RequestBlock CreateBlock()
        {
            _readWriteLock.EnterWriteLock();
            try
            {
                Guid invokeId = Guid.NewGuid();
                RequestBlock block = new RequestBlock(invokeId);
                _dict.Add(invokeId, block);
                return block;
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
        }


        public RequestBlock Get(Guid invokeId)
        {
            _readWriteLock.EnterReadLock();
            try
            {
                if (_dict.ContainsKey(invokeId))
                {
                    return _dict[invokeId];
                }
                return null;
            }
            finally
            {
                _readWriteLock.ExitReadLock();
            }
        }


        public void Remove(Guid invokeId)
        {
            _readWriteLock.EnterWriteLock();
            try
            {
                if (_dict.ContainsKey(invokeId))
                {
                    _dict.Remove(invokeId);
                }
            }
            finally
            {
                _readWriteLock.ExitWriteLock();
            }
        }
    }
}
