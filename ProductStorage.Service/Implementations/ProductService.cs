using ProductStorage.DAL.Entities;
using ProductStorage.Service.Enum;
using ProductStorage.DAL.Interfaces;
using ProductStorage.Service.Response;
using ProductStorage.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductStorage.Service.Implementations
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IBaseResponse<bool>> Create(Product productModel)
        {
            var baseResponse = new BaseResponse<bool>();
            try
            {
                var product = new Product()
                {
                    Name = productModel.Name,
                    Price = productModel.Price,
                    Amount = productModel.Amount,
                };
              
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

        public async Task<IBaseResponse<IEnumerable<Product>>> GetProducts()
        {
            var baseResponse = new BaseResponse<IEnumerable<Product>>();

            try
            {
                var products = await _unitOfWork.Products.Select();

                if (products.Count == 0)
                {
                    baseResponse.Description = "0 items found";
                    baseResponse.StatusCode = StatusCode.ZeroItemsFound;
                    return baseResponse;
                }

                baseResponse.Data = products;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> GetById(int id)
        {
            var baseResponse = new BaseResponse<Product>();

            try
            {
                var product = await _unitOfWork.Products.GetById(id);

                if (product == null)
                {
                    baseResponse.Description = "Product not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = product;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[GetProductById] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> GetByName(string name)
        {
            var baseResponse = new BaseResponse<Product>();

            try
            {
                var product = await _unitOfWork.Products.GetByName(name);

                if (product == null)
                {
                    baseResponse.Description = "Product not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = product;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[GetProductByName] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Update(int id, Product newEntity)
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
                if (newEntity == null)
                {
                    baseResponse.Description = "New entity is null";
                    baseResponse.StatusCode = StatusCode.EntityIsNull;
                    return baseResponse;
                }

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
