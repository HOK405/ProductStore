using System.Collections.Generic;

namespace ProductStorage.DAL.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }

        public List<Customer> Customers { get; set; } // one product - many customers
        public List<CustomerProduct> CustomerProducts { get; set; }
    }
}