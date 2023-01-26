using Microsoft.AspNetCore.Mvc;
using ProductStorage.DAL.Entities;
using ProductStorage.Service.Implementations;
using ProductStorage.Service.Interfaces;
using ProductStorage.Service.Response;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace ProductStorage_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = (BaseResponse<IEnumerable<Product>>)await _productService.GetProducts();
            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = (BaseResponse<Product>)await _productService.GetById(id);
            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpGet("[action]/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = (BaseResponse<Product>)await _productService.GetByName(name);
            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Product _product)
        {
            var response = (BaseResponse<bool>)await _productService.Create(_product);

            if (response.Data)
            {
                return Ok("Product record has been created.");
            }
            else
            {
                return NotFound(response.Description);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = (BaseResponse<bool>)await _productService.Delete(id);

            if (response.Data)
            {
                return Ok("Product has been deleted.");
            }
            else
            {
                return NotFound(response.Description);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, Product _product)
        {
            var response = (BaseResponse<bool>)await _productService.Update(id, _product);

            if (response.Data)
            {
                return Ok("Product has been updated.");
            }
            else
            {
                return NotFound(response.Description);
            }
        }
    }
}
