using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Test
{

    public interface IProductService
    {
        bool SaveProduct(Product p);
        int Quntity { get; }
    }
    public class Product
    {
        public string Name { get; set; }
    }


}
