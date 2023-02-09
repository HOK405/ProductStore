using ProductStorage.DAL.Entities;
using ProductStorage.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductStorage.Service.Models.Product;

namespace ProductStorage.Service.Interfaces
{
    public interface IProductService
    {
        Task<IBaseResponse<IEnumerable<ProductModel>>> GetProducts();

        Task<IBaseResponse<ProductModel>> GetById(int id);

        Task<IBaseResponse<ProductModel>> GetByName(string name);

        Task<IBaseResponse<bool>> Delete(int id);

        Task<IBaseResponse<bool>> Create(ProductModel customer);

        Task<IBaseResponse<bool>> Update(int id, ProductModel newEntity);
    }
}
