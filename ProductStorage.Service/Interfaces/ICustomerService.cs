using ProductStorage.DAL.Entities;
using ProductStorage.Service.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<IBaseResponse<IEnumerable<Customer>>> GetCustomers();

        Task<IBaseResponse<Customer>> GetById(int id);

        Task<IBaseResponse<Customer>> GetByName(string Name);

        Task<IBaseResponse<bool>> Delete(int id);

        Task<IBaseResponse<bool>> Create(Customer customer);

        Task<IBaseResponse<bool>> Update(int id, Customer newCustomer);
    }
}
