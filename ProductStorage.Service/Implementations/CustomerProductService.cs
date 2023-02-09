using ProductStorage.DAL.Entities;
using ProductStorage.Service.Response;
using ProductStorage.Service.Enum;
using ProductStorage.DAL.Interfaces;
using ProductStorage.Service.Models.CustomerProduct;
using ProductStorage.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProductStorage.Service.Models;

namespace ProductStorage.Service.Implementations
{
    public class CustomerProductService : ICustomerProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _customerProductMapper;

        public CustomerProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
            var configCustomerProduct = new MapperConfiguration(cfg => cfg.CreateMap<CustomerProduct, CustomerProductModel>().ReverseMap());
            _customerProductMapper = new Mapper(configCustomerProduct);
        }

        public async Task<IBaseResponse<bool>> Create(CustomerProductModel customerProductModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var customerProduct = _customerProductMapper.Map<CustomerProductModel, CustomerProduct>(customerProductModel);
                
                // var customerProduct = new CustomerProduct()
                // {
                //     CustomerId = _customerProductModel.CustomerId,
                //     ProductId = _customerProductModel.ProductId,
                //     Amount = _customerProductModel.Amount,
                // };
                
                baseResponse.Data = await _unitOfWork.CustomerProducts.Create(customerProduct);
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

        public async Task<IBaseResponse<CustomerProductModel>> GetByIds(int customerId, int productId)
        {
            var baseResponse = new BaseResponse<CustomerProductModel>();
            try
            {
                var customerProduct = await _unitOfWork.CustomerProducts.GetByIds(customerId, customerId);

                if (customerProduct == null)
                {
                    baseResponse.Description = "CustomerProduct record not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;

                    return baseResponse;
                }
                
                var customerModel = _customerProductMapper.Map<CustomerProduct, CustomerProductModel>(customerProduct);
                baseResponse.Data = customerModel;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerProductModel>()
                {
                    Description = $"[GetCustomerProductByIds] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(int customerId, int productId)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var customerProduct = await _unitOfWork.CustomerProducts.GetByIds(customerId, productId);

                if (customerProduct == null)
                {
                    baseResponse.Description = "CustomerProduct record not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = await _unitOfWork.CustomerProducts.Delete(customerProduct);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCustomerProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };  
            }
        }

        public async Task<IBaseResponse<IEnumerable<CustomerProductModel>>> GetByCustomerId(int id)
        {
            var baseResponse = new BaseResponse<IEnumerable<CustomerProductModel>>();

            try
            {
                var customerProducts = await _unitOfWork.CustomerProducts.GetByCustomerId(id);

                if (customerProducts.Count == 0)
                {
                    baseResponse.Description = "0 items found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                
                var customerProductModel = _customerProductMapper.Map<List<CustomerProduct>, List<CustomerProductModel>>(customerProducts);
                baseResponse.Data = customerProductModel;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CustomerProductModel>>()
                {
                    Description = $"[GetCustomerById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<CustomerProductModel>>> GetByProductId(int id)
        {
            var baseResponse = new BaseResponse<IEnumerable<CustomerProductModel>>();

            try
            {
                var customerProducts = await _unitOfWork.CustomerProducts.GetByProductId(id);

                if (customerProducts.Count == 0)
                {
                    baseResponse.Description = "0 items found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                
                var customerProductModel = _customerProductMapper.Map<List<CustomerProduct>, List<CustomerProductModel>>(customerProducts);
                baseResponse.Data = customerProductModel;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CustomerProductModel>> ()
                {
                    Description = $"[GetCustomerById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<CustomerProductModel>>> GetCustomerProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<CustomerProductModel>>();

            try
            {
                var customerProducts = await _unitOfWork.CustomerProducts.Select();

                if (customerProducts.Count == 0)
                {
                    baseResponse.Description = "0 items found";
                    baseResponse.StatusCode = StatusCode.ZeroItemsFound;
                    return baseResponse;
                }

                var customerProductModel = _customerProductMapper.Map<List<CustomerProduct>, List<CustomerProductModel>>(customerProducts);
                baseResponse.Data = customerProductModel;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CustomerProductModel>>()
                {
                    Description = $"[GetCustomers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<CustomerProductViewModel>>> Select()
        {
            var baseResponse = new BaseResponse<List<CustomerProductViewModel>>();

            List<CustomerProductViewModel> modelList = new List<CustomerProductViewModel>();

            try
            {
                CustomerProductViewModel model = new CustomerProductViewModel();

                if ((await _unitOfWork.CustomerProducts.Select()).Count == 0 || (await _unitOfWork.Customers.Select()).Count == 0 || (await _unitOfWork.CustomerProducts.Select()).Count == 0)
                {
                    baseResponse.Description = "One of the sources is empty";
                    baseResponse.StatusCode = StatusCode.ZeroItemsFound;
                    return baseResponse;
                }

                foreach (var item in await _unitOfWork.CustomerProducts.Select())
                {
                    model = new CustomerProductViewModel();

                    model.CustomerName = (await _unitOfWork.Customers.GetById(item.CustomerId)).Name;
                    model.CustomerPhone = (await _unitOfWork.Customers.GetById(item.CustomerId)).Phone;
                    model.OrderedAmount = item.Amount;
                    model.ProductName = (await _unitOfWork.Products.GetById(item.ProductId)).Name;
                    model.TotalPrice = item.Amount * (await _unitOfWork.Products.GetById(item.ProductId)).Price;

                    modelList.Add(model);
                }
                baseResponse.Data = modelList;

                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CustomerProductViewModel>>()
                {
                    Description = $"[SelectCustomerProductData] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Sell()
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                foreach (var customerProduct in await _unitOfWork.CustomerProducts.Select())
                {
                    var product = await _unitOfWork.Products.GetById(customerProduct.ProductId);

                    if (customerProduct.Amount > 0 || product.Amount > 0)
                    {
                       if (product.Amount > customerProduct.Amount)
                       {
                           var newProduct = new Product()
                           {
                               Name = product.Name,
                               Price = product.Price,
                               Amount = product.Amount - customerProduct.Amount
                           };

                           await _unitOfWork.CustomerProducts.Update(customerProduct.CustomerId, customerProduct.ProductId, 0);
                           await _unitOfWork.Products.Update(product.ProductId, newProduct);
                       }
                       else
                       {
                           var newProduct = new Product()
                           {
                               Name = product.Name,
                               Price = product.Price,
                               Amount = 0
                           };
                           await _unitOfWork.CustomerProducts.Update(customerProduct.CustomerId, customerProduct.ProductId, customerProduct.Amount - product.Amount);
                           await _unitOfWork.Products.Update(product.ProductId, newProduct);
                       }
                    }
                }
                baseResponse.Data = true;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[Sell] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Update(int customerId, int productId, int newAmount)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var customerProduct = await _unitOfWork.CustomerProducts.GetByIds(customerId, productId);

                if (customerProduct == null)
                {
                    baseResponse.Description = "CustomerProduct not found";
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    return baseResponse;
                }
                if (newAmount < 0)
                {
                    baseResponse.Description = "New amount is below zero";
                    baseResponse.StatusCode = StatusCode.EntityIsNull;
                    return baseResponse;
                }

                baseResponse.Data = await _unitOfWork.CustomerProducts.Update(customerProduct.CustomerId, customerProduct.ProductId, newAmount);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[UpdateCustomerProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
