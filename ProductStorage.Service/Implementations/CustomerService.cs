using ProductStorage.DAL.Entities;
using ProductStorage.Service.Enum;
using ProductStorage.DAL.Interfaces;
using ProductStorage.DAL.Repositories;
using ProductStorage.Service.Response;
using ProductStorage.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBaseResponse<bool>> Create(Customer customerViewModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var customer = new Customer()
                {
                    Name = customerViewModel.Name,
                    Phone = customerViewModel.Phone
                };

                baseResponse.Data = await _unitOfWork.Customers.Create(customer);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[Create customer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var customer = await _unitOfWork.Customers.GetById(id);

                if (customer == null)
                {
                    baseResponse.Description = "Customer not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = await _unitOfWork.Customers.Delete(customer);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCustomer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Customer>>> GetCustomers()
        {
            var baseResponse = new BaseResponse<IEnumerable<Customer>>();

            try
            {
                var customers = await _unitOfWork.Customers.Select();

                if (customers.Count == 0)
                {
                    baseResponse.Description = "0 items found";
                    baseResponse.StatusCode = StatusCode.ZeroItemsFound;
                    return baseResponse;
                }

                baseResponse.Data = customers;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Customer>>()
                {
                    Description = $"[GetCustomers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Customer>> GetById(int id)
        {
            var baseResponse = new BaseResponse<Customer>();

            try
            {
                var customer = await _unitOfWork.Customers.GetById(id);

                if (customer == null)  
                {
                    baseResponse.Description = "Customer not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }                      
                baseResponse.Data = customer;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Customer>()
                {
                    Description = $"[GetCustomerById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Customer>> GetByName(string name)
        {
            var baseResponse = new BaseResponse<Customer>();

            try
            {
                var customer = await _unitOfWork.Customers.GetByName(name);

                if (customer == null) 
                {
                    baseResponse.Description = "Customer not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }                
                baseResponse.Data = customer;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Customer>()
                {
                    Description = $"[GetCustomerByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Update(int id, Customer newEntity)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var customer = await _unitOfWork.Customers.GetById(id);

                if (customer == null)
                {
                    baseResponse.Description = "Customer not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                if (newEntity == null)
                {
                    baseResponse.Description = "New entity is null";
                    baseResponse.StatusCode = StatusCode.EntityIsNull;
                    return baseResponse;
                }

                baseResponse.Data = await _unitOfWork.Customers.Update(customer.CustomerID, newEntity);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[UpdateCustomer] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }
    }
}
