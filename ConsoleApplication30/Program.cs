using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication30
{

    public interface IFeeProvider
    {
        decimal CommissionSum { get; }
        decimal LevySum { get; }
    }

    public interface IPhysicalFeeProvider : IFeeProvider
    {
        decimal AdminFee { get; }
    }

    public class FeeCalculator:IFeeProvider
    {
        decimal IFeeProvider.CommissionSum
        {
            get { return 50m; }
        }

        decimal IFeeProvider.LevySum
        {
            get { return 20m; }
        }
    }

    public class PhysicalFeeCalculator : IPhysicalFeeProvider
    {
        decimal IPhysicalFeeProvider.AdminFee
        {
            get { return 1m; }
        }

        decimal IFeeProvider.CommissionSum
        {
            get { return 2m; }
        }

        decimal IFeeProvider.LevySum
        {
            get { return 3m; }
        }
    }




    class Program
    {
        static void Main(string[] args)
        {
            IFeeProvider feeProvider = new FeeCalculator();
            IFeeProvider physicalFeeProvider = new PhysicalFeeCalculator();
            IPhysicalFeeProvider p2 = physicalFeeProvider as IPhysicalFeeProvider;


        }
    }
}
