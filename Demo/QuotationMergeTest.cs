using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Demo
{
    [TestFixture]
    public class QuotationMergeTest
    {
        [Test]
        public void MergeTest()
        {
            int sequence = 1;

            var cmd1 = new Command(DataType.Command, sequence++);
            var cmd2 = new Command(DataType.Quotation, sequence++);
            var cmd3 = new Command(DataType.Command, sequence++);
            var cmd4 = new Command(DataType.Quotation, sequence++);
            var cmd5 = new Command(DataType.Command, sequence++);

            var queue = new Queue<Command>();
            var quotationQueue = new List<Command>();

            queue.Enqueue(cmd1);
            queue.Enqueue(cmd2);
            queue.Enqueue(cmd3);
            queue.Enqueue(cmd4);
            queue.Enqueue(cmd5);

            Assert.AreEqual(5, queue.Count);
            Assert.AreEqual(0, quotationQueue.Count);

            var otherCommandQueue = new List<Command>();

            while (queue.Count > 0)
            {
                if (queue.Peek().Type == DataType.Quotation)
                {
                    quotationQueue.Add(queue.Dequeue());
                }

                if (queue.Peek().Type == DataType.Command)
                {
                    otherCommandQueue.Add(queue.Dequeue());
                }
            }

            if (otherCommandQueue.Count > 0)
            {
                foreach (var command in otherCommandQueue)
                {
                    queue.Enqueue(command);
                }
            }

            Assert.AreEqual(2, quotationQueue.Count);

            Assert.AreEqual(2, quotationQueue[0].Sequence);
            Assert.AreEqual(4, quotationQueue[1].Sequence);

            Assert.AreEqual(3, queue.Count);

            Assert.AreEqual(1, queue.Dequeue().Sequence);
            Assert.AreEqual(3, queue.Dequeue().Sequence);
            Assert.AreEqual(5, queue.Dequeue().Sequence);




        }
    }


    internal enum DataType
    {
        Command,
        Quotation
    }


    internal sealed class Command
    {
        internal Command(DataType type, int sequence)
        {
            this.Type = type;
            this.Sequence = sequence;
        }

        internal DataType Type { get; private set; }

        internal int Sequence { get; private set; }

    }

}
