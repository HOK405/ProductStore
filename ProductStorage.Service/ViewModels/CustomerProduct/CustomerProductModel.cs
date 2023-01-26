using ProductStorage.Service.ViewModels.Customer;
using ProductStorage.Service.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductStorage.Service.ViewModels.CustomerProduct
{
    public class CustomerProductModel
    {
        public int CustomerId { get; set; }
        public CustomerModel Customer { get; set; }
        public int ProductId { get; set; }
        public ProductModel Pruduct { get; set; }
        public int Amount { get; set; }
    }
}
