using ProductStorage.DAL.Entities;
using ProductStorage.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<IEnumerable<Product>>> GetProducts();

        Task<IBaseResponse<Product>> GetById(int id);

        Task<IBaseResponse<Product>> GetByName(string Name);

        Task<IBaseResponse<bool>> Delete(int id);

        Task<IBaseResponse<bool>> Create(Product customer);

        Task<IBaseResponse<bool>> Update(int id, Product newEntity);
    }
}
