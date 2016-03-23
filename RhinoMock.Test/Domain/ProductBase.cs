using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public abstract class ProductBase
    {
        public abstract string Name { get; set; }

        public abstract decimal Price { get; set; }
        public abstract void Save();
    }


    public interface IProduct
    {
        string Name { get; set; }

        decimal Price { get; set; }
    }

    public class ProductManager
    {
        public static void DoublePrice(IProduct product)
        {
            product.Price *= 2;
        }
    }


    public class Rover
    {
        public virtual void Bark(int loudness)
        {

        }

        public virtual void Fetch(int speed)
        {
            throw new Exception("Yikes");
        }
    }

}
