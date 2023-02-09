using Moq;
using ProductStorage.DAL.Entities;
using ProductStorage.DAL.Interfaces;
using ProductStorage.Service.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductStorage.Service.Models.Product;
using Xunit;

namespace ProductStorage.Tests
{
    public class ProductServiceTests
    {
        const int ProductId = 1;
        private readonly ProductService _sut;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        public ProductServiceTests()
        {
            _sut = new ProductService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Create_ShouldReturnTrue_WhenPassedDataIsOk()
        {
            // Arrange
            var productModelMock = new ProductModel()
            {
                ProductId = 1,
                Name = "TestProduct",
                Amount = 1
            };
            
            _unitOfWorkMock.Setup(x => x.Products.Create(It.IsAny<Product>()))
                            .ReturnsAsync(true);

            //  Act
            var flag = await _sut.Create(productModelMock);

            // Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Create_ShouldReturnFalse_WhenPassedDataIsNull()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.Products.Create(null))
                            .ReturnsAsync(false);

            //  Act
            var flag = await _sut.Create(null);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange

            var customerMock = new Product()
            {
                ProductId = ProductId,
                Name = "Tester",
                Amount = 1
            };

            var newProductModelMock = new ProductModel()
            {
                ProductId = ProductId,
                Name = "Tester2",
                Amount = 2
            };

            _unitOfWorkMock.Setup(x => x.Products.GetById(1))
                                            .ReturnsAsync(customerMock);

            _unitOfWorkMock.Setup(x => x.Products.Update(ProductId, It.IsAny<Product>()))
                                   .ReturnsAsync(true);
            // Act
            var flag = await _sut.Update(ProductId, newProductModelMock);

            // Assert
            Assert.True(flag.Data);
        }
            
        [Fact]
        public async Task Update_ShouldReturnFalse_WhenNewProductIsNull()
        {
            // Arrange
            var productMock = new Product()
            {
                ProductId = ProductId,
                Name = "Tester",
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.Products.GetById(ProductId))
                                            .ReturnsAsync(productMock);

            _unitOfWorkMock.Setup(x => x.Products.Update(ProductId, null))
                                   .ReturnsAsync(false);
            // Act
            var flag = await _sut.Update(ProductId, null);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Update_ShouldReturnFalse_WhenCustomerIsNull()
        {
            // Arrange
            var newProductMock = new Product()
            {
                ProductId = ProductId,
                Name = "Tester",
                Amount = 1
            };
            
            var newProductModelMock = new ProductModel()
            {
                ProductId = ProductId,
                Name = "Tester",
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.Products.GetById(It.IsAny<int>()))
                                            .ReturnsAsync(() => null);

            _unitOfWorkMock.Setup(x => x.Products.Update(It.IsAny<int>(), newProductMock))
                                   .ReturnsAsync(false);
            // Act
            var flag = await _sut.Update(ProductId, newProductModelMock);

            // Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task Delete_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            var productMock = new Product()
            {
                ProductId = ProductId,
                Name = "TestProduct",
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.Products.GetById(ProductId))
                                            .ReturnsAsync(productMock);

            _unitOfWorkMock.Setup(x => x.Products.Delete(productMock))
                                               .ReturnsAsync(true);
            // Act
            var flag = await _sut.Delete(ProductId);
            //Assert
            Assert.True(flag.Data);
        }

        [Fact]
        public async Task Delete_ShouldReturnFalse_WhenCustomerDoesNotExist()
        {
            // Arrange
            var productMock = new Product();

            _unitOfWorkMock.Setup(x => x.Products.GetById(It.IsAny<int>()))
                                            .ReturnsAsync(() => null);

            _unitOfWorkMock.Setup(x => x.Products.Delete(productMock))
                                               .ReturnsAsync(false);
            // Act
            var flag = await _sut.Delete(ProductId);
            //Assert
            Assert.False(flag.Data);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var productMock = new Product()
            {
                ProductId = ProductId,
                Name = "Lamp",
                Amount = 15
            };

            _unitOfWorkMock.Setup(x => x.Products.GetById(ProductId))
                .ReturnsAsync(productMock);

            // Act
            var product = await _sut.GetById(ProductId);

            //Assert
            Assert.Equal(ProductId, product.Data.ProductId);
        }

        [Fact]
        public async Task GetById_ShouldNotReturnProduct_WhenProductDoesNotExist()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.Products.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var product = await _sut.GetById(It.IsAny<int>());

            //Assert
            Assert.Null(product.Data);
        }

        [Fact]
        public async Task GetProducts_ShouldReturnIEnumerableProduct_WhenProductsExists()
        {
            // Arrange
            int countId = 2;
            var collectionMock = new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    Name = "Lamp",
                    Amount = 15
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Lamp",
                    Amount = 15
                }
            };

            _unitOfWorkMock.Setup(x => x.Products.Select())
                .ReturnsAsync(collectionMock);

            // Act
            var products = await _sut.GetProducts();

            //Assert
            Assert.Equal(countId, products.Data.Count());
        }

        [Fact]
        public async Task GetProducts_ShouldNotReturnIEnumerableProduct_WhenProductsDoNotExist()
        {
            // Arrange
            var emptyCollectionMock = new List<Product>();

            _unitOfWorkMock.Setup(x => x.Products.Select())
                .ReturnsAsync(emptyCollectionMock);

            // Act
            var products = await _sut.GetProducts();

            //Assert
            Assert.Null(products.Data);
        }

        [Fact]
        public async Task GetProducts_ShouldNotReturnIEnumerableProduct_InternalServerError()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Products.Select())
                .ReturnsAsync(()=>null);

            // Act
            var products = await _sut.GetProducts();

            //Assert
            Assert.Null(products.Data);
        }

        [Fact]
        public async Task GetByName_ShouldNotReturnProduct_WhenProductDoesNotExist()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.Products.GetByName(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            // Act
            var product = await _sut.GetByName(It.IsAny<string>());

            //Assert
            Assert.Null(product.Data);
        }

        [Fact]
        public async Task GetByName_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            string productName = "TestProduct";
            var productMock = new Product()
            {
                ProductId = 1,
                Name = productName,
                Amount = 1
            };

            _unitOfWorkMock.Setup(x => x.Products.GetByName(productName))
                    .ReturnsAsync(productMock);

            // Act
            var customer = await _sut.GetByName(productName);

            // Assert
            Assert.Equal(productName, customer.Data.Name);
        }
    }
}
