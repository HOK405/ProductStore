using ProductStorage.DAL.Entities;
using ProductStorage.Service.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductStorage.Service.Models.Customer;

namespace ProductStorage.Service.Interfaces
{
    public interface ICustomerService
    {
        Task<IBaseResponse<IEnumerable<CustomerModel>>> GetCustomers();

        Task<IBaseResponse<CustomerModel>> GetById(int id);

        Task<IBaseResponse<CustomerModel>> GetByName(string name);

        Task<IBaseResponse<bool>> Delete(int id);

        Task<IBaseResponse<bool>> Create(CustomerModel customer);

        Task<IBaseResponse<bool>> Update(int id, CustomerModel newCustomer);
    }
}
