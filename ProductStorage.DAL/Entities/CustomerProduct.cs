using System.ComponentModel.DataAnnotations.Schema;


namespace ProductStorage.DAL.Entities
{
    [Table("CustomersProducts")]
    public  class CustomerProduct
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }

    }
}
