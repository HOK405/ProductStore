
using System.Collections.Generic;

namespace ProductStorage.DAL.Entities
{
    public  class Customer
    {
        public int CustomerID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public List<Product> Products { get; set; } // one customer - many products
        public List<CustomerProduct> CustomerProducts { get; set; }
    }
}
