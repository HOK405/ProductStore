using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductStorage.DAL.EF;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Interfaces;
using ProductStorage.DAL.Repositories;
using ProductStorage.Service.Response;
using ProductStorage.Models;
using ProductStorage.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProductStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerProductService _customerProductService;
        public HomeController(ILogger<HomeController> logger, ICustomerProductService customerProductService)
        {
            _logger = logger;
            _customerProductService = customerProductService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _customerProductService.Select();

            return View(response.Data);
            //return View("~/Views/Some/Index.cshtml");
            //return View("~/Views/Some/Index.cshtml", await _customerRepository.Select());
        }

        public IActionResult Privacy()
        {
            return View();
        }
     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
