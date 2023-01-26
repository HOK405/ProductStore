using ProductStorage.DAL.Entities;
using ProductStorage.Service.Response;
using ProductStorage.Service.ViewModels.CustomerProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.Interfaces
{
    public interface ICustomerProductService
    {
        Task<IBaseResponse<bool>> Create(CustomerProduct customer);

        Task<IBaseResponse<IEnumerable<CustomerProduct>>> GetCustomerProducts();

        Task<IBaseResponse<CustomerProduct>> GetByIds(int customerId, int productId);

        Task<IBaseResponse<IEnumerable<CustomerProduct>>> GetByCustomerId(int id);

        Task<IBaseResponse<IEnumerable<CustomerProduct>>> GetByProductId(int id);

        Task<IBaseResponse<bool>> Delete(int customerId, int productId);

        Task<IBaseResponse<List<CustomerProductViewModel>>> Select();

        Task<IBaseResponse<bool>> Update(int customerId, int productId, int newAmount);

        Task<IBaseResponse<bool>> Sell();
    }
}
