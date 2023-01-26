using ProductStorage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.DAL.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> GetByName(string name);
        Task<Customer> GetById(int id);

        Task<bool> Update(int id, Customer newEntity);
    }
}
