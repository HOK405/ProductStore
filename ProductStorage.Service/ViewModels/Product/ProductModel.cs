using ProductStorage.Service.ViewModels.Customer;
using ProductStorage.Service.ViewModels.CustomerProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.ViewModels.Product
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        public List<CustomerModel> Customers { get; set; } // one product - many customers
        public List<CustomerProductModel> CustomerProducts { get; set; }
    }
}
