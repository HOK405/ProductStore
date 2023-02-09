using ProductStorage.DAL.Entities;
using ProductStorage.Service.Enum;
using ProductStorage.DAL.Interfaces;
using ProductStorage.Service.Response;
using ProductStorage.Service.Interfaces;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Threading.Tasks;
using ProductStorage.Service.Models.Product;
using System.Linq;
using System.Text;

namespace ProductStorage.Service.Implementations
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private readonly Mapper _productMapper;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var configProduct = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductModel>().ReverseMap());
            _productMapper = new Mapper(configProduct);
        }

        public async Task<IBaseResponse<bool>> Create(ProductModel productModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                // var product = new Product()
                // {
                //     Name = productModel.Name,
                //     Price = productModel.Price,
                //     Amount = productModel.Amount,
                // };
                var product = _productMapper.Map<ProductModel, Product>(productModel);
              
                baseResponse.Data = await _unitOfWork.Products.Create(product);
                return baseResponse;
            }
            catch (Exception ex)
            {

                return new BaseResponse<bool>()
                {
                    Description = $"[Create product] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(int id)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var product = await _unitOfWork.Products.GetById(id);

                if (product == null)
                {
                    baseResponse.Description = "Product not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = await _unitOfWork.Products.Delete(product);

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<ProductModel>>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<ProductModel>>();

            try
            {
                var products = await _unitOfWork.Products.Select();
                var customerModels = _productMapper.Map<List<Product>, List<ProductModel>>(products);
                
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
                return new BaseResponse<IEnumerable<ProductModel>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProductModel>> GetById(int id)
        {
            var baseResponse = new BaseResponse<ProductModel>();

            try
            {
                var product = await _unitOfWork.Products.GetById(id);

                if (product == null)
                {
                    baseResponse.Description = "Product not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                var productModel = _productMapper.Map<Product, ProductModel>(product);
                baseResponse.Data = productModel;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductModel>()
                {
                    Description = $"[GetProductById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProductModel>> GetByName(string name)
        {
            var baseResponse = new BaseResponse<ProductModel>();

            try
            {
                var product = await _unitOfWork.Products.GetByName(name);

                if (product == null)
                {
                    baseResponse.Description = "Product not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                
                var productModel = _productMapper.Map<Product, ProductModel>(product);
                baseResponse.Data = productModel;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductModel>()
                {
                    Description = $"[GetProductByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Update(int id, ProductModel productModel)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var product = await _unitOfWork.Products.GetById(id);

                if (product == null)
                {
                    baseResponse.Description = "Product not found";
                    baseResponse.StatusCode = StatusCode.ProductNotFound;
                    return baseResponse;
                }
                if (productModel == null)
                {
                    baseResponse.Description = "New entity is null";
                    baseResponse.StatusCode = StatusCode.EntityIsNull;
                    return baseResponse;
                }

                var newEntity = _productMapper.Map<ProductModel, Product>(productModel);
                
                baseResponse.Data = await _unitOfWork.Products.Update(product.ProductId, newEntity);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[UpdateProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
