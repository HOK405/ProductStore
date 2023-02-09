using Moq;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Interfaces;
using ProductStorage.Service.Implementations;
using ProductStorage.Service.Models.CustomerProduct;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProductStorage.Tests
{
    public class CustomerProductServiceTests
    {
        private readonly CustomerProductService customerProductService;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        public CustomerProductServiceTests()
        {
            customerProductService = new CustomerProductService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnTrue_WhenPassedDataIsOk()
        {
            // Arrange
            var customerProductMock = new CustomerProductModel()
            {
                CustomerId = 1,
                ProductId = 1,
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Create(It.IsAny<CustomerProduct>()))
                            .ReturnsAsync(true);

            //  Act
            var flag = await customerProductService.Create(customerProductMock);

            // Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Create_ShouldReturnFalse_WhenPassedDataIsNull()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Create(null))
                            .ReturnsAsync(false);

            //  Act
            var flag = await customerProductService.Create(null);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Sell_ShouldReturnTrue_WhenProductAmountIsFewerThanOrderAmount()
        {
            // Arrange
            int ID = 1;

            var collectionCustomerProductMock = new List<CustomerProduct>()
            {
                new CustomerProduct()
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 2
                }
            };

            var productMock = new Product()
            {
                ProductId = ID,
                Name = "TestProduct",
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Select())
               .ReturnsAsync(collectionCustomerProductMock);

            _unitOfWorkMock.Setup(x => x.Products.GetById(ID))
               .ReturnsAsync(productMock);

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Update(ID, ID, 1))
                .ReturnsAsync(true);

            _unitOfWorkMock.Setup(x => x.Products.Update(ID, productMock))
                .ReturnsAsync(true);

            //Act
            var flag = await customerProductService.Sell();

            //Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Sell_ShouldReturnFalse_InternalServerError()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Products.GetById(It.IsAny<int>()))
               .ReturnsAsync(()=>null);

            //Act
            var flag = await customerProductService.Sell();

            //Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Sell_ShouldReturnTrue_WhenProductAmountIsLargerThanOrderAmount()
        {
            // Arrange
            int ID = 1;

            var collectionCustomerProductMock = new List<CustomerProduct>()
            {
                new CustomerProduct()
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 1
                }
            };

            var productMock = new Product()
            {
                ProductId = ID,
                Name = "TestProduct",
                Amount = 2
            };

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Select())
               .ReturnsAsync(collectionCustomerProductMock);

            _unitOfWorkMock.Setup(x => x.Products.GetById(ID))
               .ReturnsAsync(productMock);

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Update(ID, ID, 1))
                .ReturnsAsync(true);

            _unitOfWorkMock.Setup(x => x.Products.Update(ID, productMock))
                .ReturnsAsync(true);

            //Act
            var flag = await customerProductService.Sell();

            //Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Select_ShouldReturnCustomerProductViewModel_WhenEntitiesExist()
        {
            //Arrange
            var ID = 1;

            var collectionCustomerProductMock = new List<CustomerProduct>()
            {
                new CustomerProduct()
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 1
                }
            };

            var customerMock = new Customer()
            {
                CustomerID = ID,
                Name = "Tester",
                Phone = "111"
            };

            var productMock = new Product()
            {
                ProductId = ID,
                Name = "TestProduct",
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.Customers.Select())
                           .ReturnsAsync(new List<Customer> { customerMock });

            _unitOfWorkMock.Setup(x => x.Products.Select())
                           .ReturnsAsync(new List<Product> { productMock });


            _unitOfWorkMock.Setup(x => x.CustomerProducts.Select())
                           .ReturnsAsync(collectionCustomerProductMock);

            _unitOfWorkMock.Setup(x => x.Customers.GetById(ID))
                           .ReturnsAsync(customerMock);

            _unitOfWorkMock.Setup(x => x.Products.GetById(ID))
                           .ReturnsAsync(productMock);
            //Act

            var flag = await customerProductService.Select();
            int expectedResult = 1;
            //Assert

            Assert.Equal(expectedResult, flag.Data.Count);
        }

        [Fact]
        public async Task Select_ShouldNotReturnCustomerProductViewModel_InternalServerError()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.Customers.Select())
                           .ReturnsAsync(()=> null);

            // Act
            var flag = await customerProductService.Select();
            //Assert

            Assert.Null(flag.Data);
        }

        [Fact]
        public async Task Select_ShouldNotReturnCustomerProductViewModel_WhenEntitiesNotExist()
        {
            //Arrange
            var ID = 1;

            var collectionCustomerProductMock = new List<CustomerProduct>()
            {
                new CustomerProduct()
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 1
                }
            };

            var customerMock = new Customer();

            var productMock = new Product()
            {
                ProductId = ID,
                Name = "TestProduct",
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.Customers.Select())
                           .ReturnsAsync(new List<Customer> { });

            _unitOfWorkMock.Setup(x => x.Products.Select())
                           .ReturnsAsync(new List<Product> { productMock });

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Select())
                           .ReturnsAsync(collectionCustomerProductMock);

            // Act
            var flag = await customerProductService.Select();
            //Assert

            Assert.Null(flag.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenRecordExists()
        {
            // Arrange
            var ID = 1;

            var customerProductMock = new CustomerProduct()
            {
                CustomerId = ID,
                ProductId = ID,
                Amount = 1
            };

            int newAmount = 2;

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByIds(ID, ID))
                                            .ReturnsAsync(customerProductMock);

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Update(ID, ID, newAmount))
                                   .ReturnsAsync(true);
            // Act
            var flag = await customerProductService.Update(ID, ID, newAmount);

            // Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenNewAmountIsNull()
        {
            // Arrange
            var ID = 1;

            var customerProductMock = new CustomerProduct()
            {
                CustomerId = ID,
                ProductId = ID,
                Amount = 1
            };

            int newAmount = -2;
            var newCustomerMock = new CustomerProduct();

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByIds(ID, ID))
                                            .ReturnsAsync(customerProductMock);

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Update(ID, ID, newAmount))
                                   .ReturnsAsync(false);
            // Act
            var flag = await customerProductService.Update(ID, ID, newAmount);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenRecordIsNull()
        {
            // Arrange
            var ID = 1;

            var customerProductMock = new CustomerProduct()
            {
                CustomerId = ID,
                ProductId = ID,
                Amount = 1
            };
            int newAmount = 2;
            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByIds(It.IsAny<int>(), It.IsAny<int>()))
                                            .ReturnsAsync(() => null);

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Update(It.IsAny<int>(), It.IsAny<int>(), newAmount))
                                   .ReturnsAsync(true);
            // Act
            var flag = await customerProductService.Update(ID, ID, newAmount);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenRecordExists()
        {
            // Arrange
            var customerID = 1;
            var productID = 1;

            var customerProductMock = new CustomerProduct()
            {
                CustomerId = customerID,
                ProductId = productID,
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByIds(customerID, productID))
                                .ReturnsAsync(customerProductMock);

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Delete(customerProductMock))
                                               .ReturnsAsync(true);
            // Act
            var flag = await customerProductService.Delete(customerID, productID);
            //Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenRecordDoesNotExist()
        {
            // Arrange
            var customerID = 1;
            var productID = 1;

            var customerProductMock = new CustomerProduct();

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByIds(It.IsAny<int>(), It.IsAny<int>()))
                                            .ReturnsAsync(() => null);

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Delete(customerProductMock))
                                               .ReturnsAsync(false);
            // Act
            var flag = await customerProductService.Delete(customerID, productID);
            //Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task GetCustomerProducts_ShouldReturnIEnumerableCustomerProduct_WhenCustomersExist()
        {
            // Arrange
            int countId = 2;
            var collectionMock = new List<CustomerProduct>()
            {
                new CustomerProduct()
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 1
                },
                new CustomerProduct()
                {
                    CustomerId = 2,
                    ProductId = 2,
                    Amount = 2
                }
            };

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Select())
                .ReturnsAsync(collectionMock);

            // Act
            var products = await customerProductService.GetCustomerProducts();

            //Assert
            Assert.Equal(countId, products.Data.Count());
        }

        [Fact]
        public async Task GetCustomerProducts_ShouldNotReturnIEnumerableCustomerProducts_WhenRecordsDoNotExist()
        {
            // Arrange
            var emptyCollectionMock = new List<CustomerProduct>();

            _unitOfWorkMock.Setup(x => x.CustomerProducts.Select())
                .ReturnsAsync(emptyCollectionMock);

            // Act
            var customers = await customerProductService.GetCustomerProducts();

            //Assert
            Assert.Null(customers.Data);
        }

        [Fact]
        public async Task GetCustomerProducts_ShouldNotReturnIEnumerableCustomerProducts_InternalServerError()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.CustomerProducts.Select())
                .ReturnsAsync(()=> null);

            // Act
            var customers = await customerProductService.GetCustomerProducts();

            //Assert
            Assert.Null(customers.Data);
        }

        [Fact]
        public async Task GetByIds_ShouldReturnCustomerProduct_WhenRecordExists()
        {
            // Arrange
            var customerID = 1;
            var productID = 1;
            var customerMock = new CustomerProduct()
            {
                CustomerId = customerID,
                ProductId = productID,
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByIds(customerID, productID))
                .ReturnsAsync(customerMock);

            // Act
            var customer = await customerProductService.GetByIds(customerID, productID);

            //Assert
            Assert.Equal(customerID, customer.Data.CustomerId);
            Assert.Equal(productID, customer.Data.ProductId);
        }

        [Fact]
        public async Task GetByIds_ShouldNotReturnCustomerProduct_WhenRecordDoesNotExists()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByIds(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var customer = await customerProductService.GetByIds(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            Assert.Null(customer.Data);
        }

        [Fact]
        public async Task GetByCustomerId_ShouldReturnIEnumerableCustomerProduct_WhenRecordsExist()
        {
            // Arrange
            int customerID = 1;
            int countId = 2;
            var collectionMock = new List<CustomerProduct>()
            {
                new CustomerProduct()
                {
                    CustomerId = customerID,
                    ProductId = 1,
                    Amount = 1
                },
                new CustomerProduct()
                {
                    CustomerId = customerID,
                    ProductId = 2,
                    Amount = 2
                }
            };

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByCustomerId(1))
                .ReturnsAsync(collectionMock);

            // Act
            var products = await customerProductService.GetByCustomerId(customerID);

            //Assert
            Assert.Equal(countId, products.Data.Count());
        }
        [Fact]
        public async Task GetByCustomerId_ShouldNotReturnIEnumerableCustomerProduct_WhenRecordsDoNotExist()
        {
            // Arrange
            var emptyCollectionMock = new List<CustomerProduct>();

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByCustomerId(It.IsAny<int>()))
                .ReturnsAsync(emptyCollectionMock);

            // Act
            var customers = await customerProductService.GetByCustomerId(It.IsAny<int>());

            //Assert
            Assert.Null(customers.Data);
        }

        [Fact]
        public async Task GetByCustomerId_ShouldNotReturnIEnumerableCustomerProduct_InternalServerError()
        {
            // Arrange
            var emptyCollectionMock = new List<CustomerProduct>();

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByCustomerId(It.IsAny<int>()))
                .ReturnsAsync(() =>null);

            // Act
            var customers = await customerProductService.GetByCustomerId(It.IsAny<int>());

            //Assert
            Assert.Null(customers.Data);
        }

        [Fact]
        public async Task GetByProductId_ShouldReturnIEnumerableCustomerProduct_WhenRecordsExist()
        {
            // Arrange
            int productID = 1;
            int countId = 2;
            var collectionMock = new List<CustomerProduct>()
            {
                new CustomerProduct()
                {
                    CustomerId = 1,
                    ProductId = productID,
                    Amount = 1
                },
                new CustomerProduct()
                {
                    CustomerId = 2,
                    ProductId = productID,
                    Amount = 2
                }
            };

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByProductId(productID))
                .ReturnsAsync(collectionMock);

            // Act
            var products = await customerProductService.GetByProductId(productID);

            //Assert
            Assert.Equal(countId, products.Data.Count());
        }
        
        [Fact]
        public async Task GetByProductId_ShouldNotReturnIEnumerableCustomerProduct_WhenRecordsDoNotExist()
        {
            // Arrange
            var emptyCollectionMock = new List<CustomerProduct>();

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByProductId(It.IsAny<int>()))
                .ReturnsAsync(emptyCollectionMock);

            // Act
            var customers = await customerProductService.GetByProductId(It.IsAny<int>());

            //Assert
            Assert.Null(customers.Data);
        }

        [Fact]
        public async Task GetByProductId_ShouldNotReturnIEnumerableCustomerProduct_InternalServerError()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.CustomerProducts.GetByProductId(It.IsAny<int>()))
                .ReturnsAsync(()=> null);

            // Act
            var customers = await customerProductService.GetByProductId(It.IsAny<int>());

            //Assert
            Assert.Null(customers.Data);
        }



    }
}
