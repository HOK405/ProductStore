using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Repositories;
using ProductStorage.Service.Implementations;
using ProductStorage.Service.Interfaces;
using ProductStorage.Service.Response;
using ProductStorage_WebApi.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsStorage.Tests
{
    [TestClass]
    public class ProductStorageTests
    {
        [TestMethod]
        public async Task GetById_Returns_One_Customer()
        {
            //Arrange
            var fakeCustomer = A.Dummy<IBaseResponse<Customer>>();
            var dataStore = A.Fake<ICustomerService>();
            A.CallTo(() => dataStore.GetById(1)).Returns(Task.FromResult(fakeCustomer));
            var controller = new CustomerController(dataStore);


            //Act
            var actionResult = await controller.Get(1);

            // Assert
            var result = actionResult as OkObjectResult;
            var returnCustomer = result.Value as BaseResponse<Customer>;
            Assert.IsNull(returnCustomer.Data);
        }
    }
}
