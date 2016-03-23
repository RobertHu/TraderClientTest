using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LazyIterator
{
    interface IChain<T> : IEnumerable<T>
    {
        IChain<T> Next { get; }
        T Value { get; }
    }

    abstract class AbstractChain<T> : IChain<T>
    {
        public abstract IChain<T> Next { get; }
        public abstract T Value { get; }
        public IEnumerator<T> GetEnumerator()
        {
            IChain<T> current = this;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    class LazyChain<T> : AbstractChain<T>
    {
        IChain<T> next;
        T value;
        IEnumerator<T> enumerator;
        LazyChain(T value, IEnumerator<T> enumerator)
        {
            this.value = value;
            this.enumerator = enumerator;
        }

        public static IChain<T> create(IEnumerable<T> enumerable)
        {
            return create(enumerable.GetEnumerator());
        }

        public static IChain<T> create(IEnumerator<T> enumerator)
        {
            if (enumerator.MoveNext())
            {
                return new LazyChain<T>(enumerator.Current, enumerator);
            }
            return null;
        }

        public override T Value
        {
            get
            {
                return value;
            }
        }

        public override IChain<T> Next
        {
            get
            {
                if (enumerator != null)
                {
                    next = create(enumerator);
                    enumerator = null;
                }
                return next;
            }
        }

    }


    class Program
    {

        static IEnumerable<int> GetNumbers()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
        }

        static void Main(string[] args)
        {
            IChain<int> intChain = LazyChain<int>.create(GetNumbers());
            IChain<int> savedChain = null;
            for (int i = 0; intChain != null; i++)
            {
                if (intChain.Value == 5)
                {
                    savedChain = intChain;
                }
                intChain = intChain.Next;
            }
            if (savedChain != null)
            {
                foreach (int item in savedChain)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
