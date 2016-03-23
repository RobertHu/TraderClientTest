using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication40
{
    internal interface IFeeProvider
    {
        decimal Commission { get; }
    }

    class FeeProviderA: IFeeProvider
    {

        public decimal Commission
        {
            get { return 1.0m; }
        }
    }

    class FeeProviderB :FeeProviderA ,IFeeProvider
    {
        public decimal Commission
        {
            get { return 1.5m; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IFeeProvider feeProvider = new FeeProviderA();
            feeProvider = new FeeProviderB();

        }
    }
}
