using ProductStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.DAL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetByName(string name);
        Task<Product> GetById(int id);

        Task<bool> Update(int id, Product newEntity);
    }
}
