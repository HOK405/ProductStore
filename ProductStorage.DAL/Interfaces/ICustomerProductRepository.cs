using ProductStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.DAL.Interfaces
{
    public interface ICustomerProductRepository : IBaseRepository<CustomerProduct>
    {
        Task<CustomerProduct> GetByIds(int customerId, int productId);
        Task<List<CustomerProduct>> GetByCustomerId(int id);

        Task<List<CustomerProduct>> GetByProductId(int id);

        Task<CustomerProduct> GetByAmount(int amount);

        Task<bool> Update(int customerId, int productId, int newAmount);
    }
}
