using ProductStorage.Service.ViewModels.CustomerProduct;
using ProductStorage.Service.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.ViewModels.Customer
{
    public class CustomerModel
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public List<ProductModel> Products { get; set; } // one customer - many products
        public List<CustomerProductModel> CustomerProducts { get; set; }

    }
}
