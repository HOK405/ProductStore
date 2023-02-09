using ProductStorage.DAL.Entities;
using ProductStorage.Service.Enum;
using ProductStorage.DAL.Interfaces;
using ProductStorage.Service.Response;
using ProductStorage.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductStorage.Service.Models.Customer;

namespace ProductStorage.Service.Implementations
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork _unitOfWork;
        private readonly Mapper _сustomerMapper;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            var configCustomer = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerModel>().ReverseMap());
            _сustomerMapper = new Mapper(configCustomer);
        }

        public async Task<IBaseResponse<bool>> Create(CustomerModel customerModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var customer = _сustomerMapper.Map<CustomerModel, Customer>(customerModel);
                
                // var customer = new Customer()
                // {
                //     Name = customerModel.Name,
                //     Phone = customerModel.Phone
                // };

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

        public async Task<IBaseResponse<IEnumerable<CustomerModel>>> GetCustomers()
        {
            var baseResponse = new BaseResponse<IEnumerable<CustomerModel>>();

            try
            {
                var customers = await _unitOfWork.Customers.Select();
                var customerModels = _сustomerMapper.Map<List<Customer>, List<CustomerModel>>(customers);
                
                if (customerModels.Count == 0)
                {
                    baseResponse.Description = "0 items found";
                    baseResponse.StatusCode = StatusCode.ZeroItemsFound;
                    return baseResponse;
                }

                baseResponse.Data = customerModels;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CustomerModel>>()
                {
                    Description = $"[GetCustomers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<CustomerModel>> GetById(int id)
        {
            var baseResponse = new BaseResponse<CustomerModel>();

            try
            {
                var customer = await _unitOfWork.Customers.GetById(id);

                if (customer == null)  
                {
                    baseResponse.Description = "Customer not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                var customerModel = _сustomerMapper.Map<Customer, CustomerModel>(customer);
                baseResponse.Data = customerModel;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerModel>()
                {
                    Description = $"[GetCustomerById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<CustomerModel>> GetByName(string name)
        {
            var baseResponse = new BaseResponse<CustomerModel>();

            try
            {
                var customer = await _unitOfWork.Customers.GetByName(name);
                if (customer == null) 
                {
                    baseResponse.Description = "Customer not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }                
                
                var customerModel = _сustomerMapper.Map<Customer, CustomerModel>(customer);
                baseResponse.Data = customerModel;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerModel>()
                {
                    Description = $"[GetCustomerByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Update(int id, CustomerModel customerModel)
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
                if (customerModel == null)
                {
                    baseResponse.Description = "New entity is null";
                    baseResponse.StatusCode = StatusCode.EntityIsNull;
                    return baseResponse;
                }

                var newEntity = _сustomerMapper.Map<CustomerModel, Customer>(customerModel);
                
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
