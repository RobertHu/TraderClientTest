using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication32
{
    public interface IFeeProvider
    {
        decimal Commission { get; }
        decimal Levy { get; }
    }

    public interface IPhysicalFeeProvider : IFeeProvider
    {
        decimal AdministrationFee { get; }
    }


    public class FeeProvider : IFeeProvider
    {

        decimal IFeeProvider.Commission
        {
            get { return 1m; }
        }

        decimal IFeeProvider.Levy
        {
            get { return 2m; }
        }
    }

    public class PhysicalFeeProvider : FeeProvider, IPhysicalFeeProvider
    {
        decimal IPhysicalFeeProvider.AdministrationFee
        {
            get { return 2.1m; }
        }

        decimal IFeeProvider.Commission
        {
            get { return 2.2m; }
        }

        decimal IFeeProvider.Levy
        {
            get { return 2.3m; }
        }
    }

    class Program
    {
        private struct Items
        {
            public decimal Item1;
            public decimal Item2;
            public bool isBuy;
        }

        static void Main(string[] args)
        {
            Items items = new Items(); 
            items.isBuy |= true;
            items.Item1 += 2;
            items.Item2 += 3;
        }
    }
}
