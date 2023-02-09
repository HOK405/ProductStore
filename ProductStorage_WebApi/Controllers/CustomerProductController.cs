using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using ProductStorage.Service.Interfaces;
using System.Threading.Tasks;
using ProductStorage.Service.Implementations;
using ProductStorage.Service.Models.CustomerProduct;
using ProductStorage.DAL.Entities;
using ProductStorage.Service.Response;
using Microsoft.AspNetCore.Cors;
using CustomerProductModel = ProductStorage.Service.Models.CustomerProduct.CustomerProductModel;

namespace ProductStorage_WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerProductController : ControllerBase
    {
        private readonly ICustomerProductService _customerProductService;

        public CustomerProductController(ICustomerProductService customerProductService)
        {
            _customerProductService = customerProductService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerProductModel _customerProduct)
        {
            var response = (BaseResponse<bool>)await _customerProductService.Create(_customerProduct);

            if (response.Data)
            {
                return Ok("CustomerProduct record has been created.");
            }
            else
            {
                return NotFound(response.Description);
            }
        }

        [HttpDelete("{customerId}/{productId}")]
        public async Task<IActionResult> Delete(int customerId, int productId )
        {
            var response = (BaseResponse<bool>)await _customerProductService.Delete(customerId, productId);

            if (response.Data)
            {
                return Ok("CustomerProduct record has been deleted.");
            }
            else
            {
                return NotFound(response.Description);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = (BaseResponse<IEnumerable<CustomerProductModel>>)await _customerProductService.GetCustomerProducts();
            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpGet("[action]")]
        [EnableCors("CorsPolicyAllHosts")]
        public async Task<IActionResult> Select()
        {
            var response = (BaseResponse<List<CustomerProductViewModel>>)await _customerProductService.Select();

            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpGet("{customerId}/{productId}")]
        public async Task<IActionResult> Get(int customerId, int productId)
        {
            var response = (BaseResponse<CustomerProductModel>)await _customerProductService.GetByIds(customerId, productId);

            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByCustomerId(int id)
        {
            var response = (BaseResponse<IEnumerable<CustomerProductModel>>)await _customerProductService.GetByCustomerId(id);
            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetByProductId(int id)
        {
            var response = (BaseResponse<IEnumerable<CustomerProductModel>>)await _customerProductService.GetByProductId(id);
            if (response.Data == null)
            {
                return NotFound(response.Description);
            }
            return Ok(response.Data);
        }

        [HttpPatch("{customerId}/{productId}/{newAmount}")]
        public async Task<IActionResult> Patch(int customerId, int productId, int newAmount)
        {
            var response = (BaseResponse<bool>)await _customerProductService.Update(customerId, productId, newAmount);

            if (response.Data)
            {
                return Ok("CustomerProduct has been updated.");
            }
            else
            {
                return NotFound(response.Description);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Sell()
        {
            var response = (BaseResponse<bool>)await _customerProductService.Sell();
            if (response.Data)
            {
                return Ok("Products have been sold successfully");
            }
            else
            {
                return NotFound(response.Description);
            }
        }

    }
}