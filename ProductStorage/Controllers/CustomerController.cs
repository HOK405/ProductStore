using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Interfaces;
using ProductStorage.DAL.Repositories;
using System.Threading.Tasks;

namespace ProductStorage.Controllers
{
    public class CustomerController : Controller
    {
        //IUnitOfWork unitOfWork;

        //public CustomerController(IUnitOfWork unitOfWork)
        //{
        //    this.unitOfWork = unitOfWork;
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetCustomers()
        //{
        //    var customerI = await unitOfWork.Customers.GetById(1);

        //    return View(await unitOfWork.Customers.Select());

        //    return View("~/Views/Some/Index.cshtml");
        //    return View("~/Views/Some/Index.cshtml", await _customerRepository.Select());
        //}
    }
}
