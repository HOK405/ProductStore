using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.ViewModels.CustomerProduct
{
    public class CustomerProductViewModel
    {
        public string CustomerName { get; set; }  // Customer table
        public string CustomerPhone { get; set; } // Customer table
        public int OrderedAmount { get; set; }    // CustomerProduct table
        public string ProductName { get; set; }   // Product table
        public int TotalPrice { get; set; }       // CustomerProduct table field ("Amount") * Product table field ("Price")
    }
}
