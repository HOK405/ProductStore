using ProductStorage.DAL.Entities;
using ProductStorage.Service.Response;
using ProductStorage.Service.Models.CustomerProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerProductModel = ProductStorage.Service.Models.CustomerProduct.CustomerProductModel;

namespace ProductStorage.Service.Interfaces
{
    public interface ICustomerProductService
    {
        Task<IBaseResponse<bool>> Create(CustomerProductModel customer);

        Task<IBaseResponse<IEnumerable<CustomerProductModel>>> GetCustomerProducts();

        Task<IBaseResponse<CustomerProductModel>> GetByIds(int customerId, int productId);

        Task<IBaseResponse<IEnumerable<CustomerProductModel>>> GetByCustomerId(int id);

        Task<IBaseResponse<IEnumerable<CustomerProductModel>>> GetByProductId(int id);

        Task<IBaseResponse<bool>> Delete(int customerId, int productId);

        Task<IBaseResponse<List<CustomerProductViewModel>>> Select();

        Task<IBaseResponse<bool>> Update(int customerId, int productId, int newAmount);

        Task<IBaseResponse<bool>> Sell();
    }
}
