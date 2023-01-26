using Castle.Core.Resource;
using Moq;
using ProductStorage.DAL.EF;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Interfaces;
using ProductStorage.DAL.Repositories;
using ProductStorage.Service.Implementations;
using ProductStorage.Service.Interfaces;
using ProductStorage.Service.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace ProductStorage.Tests
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _sut;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        public CustomerServiceTests()
        {
            _sut = new CustomerService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnTrue_WhenPassedDataIsOk()
        {
            // Arrange
            var customerMock = new Customer()
            {
                CustomerID = 1,
                Name = "Tester",
                Phone = "111"
            };
            _unitOfWorkMock.Setup(x => x.Customers.Create(It.IsAny<Customer>()))
                            .ReturnsAsync(true);

            //  Act
            var flag = await _sut.Create(customerMock);

            // Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Create_ShouldReturnFalse_WhenPassedDataIsNull()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.Customers.Create(null))
                            .ReturnsAsync(false);

            //  Act
            var flag = await _sut.Create(null);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenCustomerExists()
        {
            // Arrange
            var customerID = 1;

            var customerMock = new Customer()
            {
                CustomerID = customerID,
                Name = "Tester",
                Phone = "111"
            };

            _unitOfWorkMock.Setup(x => x.Customers.GetById(customerID))
                                            .ReturnsAsync(customerMock);

            _unitOfWorkMock.Setup(x => x.Customers.Delete(customerMock))
                                               .ReturnsAsync(true);
            // Act
            var flag = await _sut.Delete(customerID);
            //Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenCustomerDoesNotExist()
        {
            // Arrange
            var customerID = 1;

            var customerMock = new Customer();

            _unitOfWorkMock.Setup(x => x.Customers.GetById(It.IsAny<int>()))
                                            .ReturnsAsync(() => null);

            _unitOfWorkMock.Setup(x => x.Customers.Delete(customerMock))
                                               .ReturnsAsync(false);
            // Act
            var flag = await _sut.Delete(customerID);
            //Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenCustomerExists()
        {
            // Arrange
            var customerID = 1;

            var customerMock = new Customer()
            {
                CustomerID = customerID,
                Name = "Tester",
                Phone = "111"
            };

            var newCustomerMock = new Customer()
            {
                CustomerID = customerID,
                Name = "Tester",
                Phone = "222"
            };

            _unitOfWorkMock.Setup(x => x.Customers.GetById(customerID))
                                            .ReturnsAsync(customerMock);

            _unitOfWorkMock.Setup(x => x.Customers.Update(customerID, newCustomerMock))
                                   .ReturnsAsync(true);
            // Act
            var flag = await _sut.Update(customerID, newCustomerMock);

            // Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenNewCustomerIsNull()
        {
            // Arrange
            var customerID = 1;

            var customerMock = new Customer()
            {
                CustomerID = customerID,
                Name = "Tester",
                Phone = "111"
            };

            var newCustomerMock = new Customer();

            _unitOfWorkMock.Setup(x => x.Customers.GetById(customerID))
                                            .ReturnsAsync(customerMock);

            _unitOfWorkMock.Setup(x => x.Customers.Update(customerID, null))
                                   .ReturnsAsync(false);
            // Act
            var flag = await _sut.Update(customerID, null);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenCustomerIsNull()
        {
            // Arrange
            var customerID = 1;

            var newCustomerMock = new Customer()
            {
                CustomerID = customerID,
                Name = "Tester",
                Phone = "111"
            };

            _unitOfWorkMock.Setup(x => x.Customers.GetById(It.IsAny<int>()))
                                            .ReturnsAsync(() => null);

            _unitOfWorkMock.Setup(x => x.Customers.Update(It.IsAny<int>(), newCustomerMock))
                                   .ReturnsAsync(false);
            // Act
            var flag = await _sut.Update(customerID, newCustomerMock);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task GetByName_ShouldNotReturnCustomer_WhenCustomerDoesNotExist()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.Customers.GetByName(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            // Act
            var customer = await _sut.GetByName(It.IsAny<string>());

            //Assert
            Assert.Null(customer.Data);
        }

        [Fact]
        public async Task GetByName_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            string customerName = "Tester";
            var customerMock = new Customer()
            {
                CustomerID = 1,
                Name = customerName,
                Phone = "111"
            };

            _unitOfWorkMock.Setup(x => x.Customers.GetByName(customerName))
                    .ReturnsAsync(customerMock);

            // Act
            var customer = await _sut.GetByName(customerName);

            // Assert
            Assert.Equal(customerName, customer.Data.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var customerID = 1;
            var customerMock = new Customer()
            {
                CustomerID = customerID,
                Name = "Tester",
                Phone = "111"
            };

            _unitOfWorkMock.Setup(x => x.Customers.GetById(customerID))
                .ReturnsAsync(customerMock);

            // Act
            var customer = await _sut.GetById(customerID);

            //Assert
            Assert.Equal(customerID, customer.Data.CustomerID);
        }

        [Fact]
        public async Task GetById_ShouldNotReturnCustomer_WhenCustomerDoesNotExists()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.Customers.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var customer = await _sut.GetById(It.IsAny<int>());

            //Assert
            Assert.Null(customer.Data);
        }


        [Fact]
        public async Task GetCustomers_ShouldReturnNull_InternalServerError()
        {
            // Arrange
            var collectionMock = new List<Customer>()
            {
                new Customer()
                {
                    CustomerID = 1,
                    Name = "Tester1",
                    Phone = "0662892345"
                },
                new Customer()
                {
                    CustomerID = 2,
                    Name = "Tester2",
                    Phone = "09839472382"
                }
            };
            _unitOfWorkMock.Setup(x => x.Customers.Select())
                .ReturnsAsync(()=> null);

            // Act
            var products = await _sut.GetCustomers();

            //Assert
            Assert.Null(products.Data);
        }

        [Fact]
        public async Task GetCustomers_ShouldReturnIEnumerableCustomer_WhenCustomersExist()
        {
            // Arrange
            int countId = 2;
            var collectionMock = new List<Customer>()
            {
                new Customer()
                {
                    CustomerID = 1,
                    Name = "Tester1",
                    Phone = "0662892345"
                },
                new Customer()
                {
                    CustomerID = 2,
                    Name = "Tester2",
                    Phone = "09839472382"
                }
            };

            _unitOfWorkMock.Setup(x => x.Customers.Select())
                .ReturnsAsync(collectionMock);

            // Act
            var products = await _sut.GetCustomers();

            //Assert
            Assert.Equal(countId, products.Data.Count());
        }

        [Fact]
        public async Task GetCustomers_ShouldNotReturnIEnumerableCustomer_WhenCustomersDoNotExist()
        {
            // Arrange
            var emptyCollectionMock = new List<Customer>();

            _unitOfWorkMock.Setup(x => x.Customers.Select())
                .ReturnsAsync(emptyCollectionMock);

            // Act
            var customers = await _sut.GetCustomers();

            //Assert
            Assert.Null(customers.Data);
        }

    }
}
