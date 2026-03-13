using ECommerce.Api.Services;
using ECommerce.Api.Repositories;
using ECommerce.Shared.DTOs;
using Moq;
using Xunit;

namespace ECommerce.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _service = new ProductService(_mockRepo.Object);
        }

        [Fact]
        public void GetProduct_ExistingId_ReturnsProduct()
        {
            // 1. Arrange
            var expectedProduct = new ProductDto { Id = 1, Name = "Test", Price = 100 };
            _mockRepo.Setup(r => r.GetById(1)).Returns(expectedProduct);

            // 2. Act
            var result = _service.GetProductById(1);

            // 3. Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }

        [Fact]
        public void GetProduct_NonExistingId_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetById(99)).Returns((ProductDto)null);
            var result = _service.GetProductById(99);
            Assert.Null(result);
        }

        [Fact]
        public void AddProduct_ValidProduct_CallsRepository()
        {
            var product = new ProductDto { Name = "Ny", Price = 100 };
            _service.AddProduct(product);
            _mockRepo.Verify(r => r.Add(It.IsAny<ProductDto>()), Times.Once);
        }

        [Fact]
        public void AddProduct_NegativePrice_ThrowsArgumentException()
        {
            var product = new ProductDto { Name = "Ogiltig", Price = -10 };
            Assert.Throws<ArgumentException>(() => _service.AddProduct(product));
        }

        [Fact]
        public void GetAllProducts_ReturnsList()
        {
            var products = new List<ProductDto> 
            { 
                new ProductDto { Id = 1 }, new ProductDto { Id = 2 } 
            };
            _mockRepo.Setup(r => r.GetAll()).Returns(products);

            var result = _service.GetAllProducts();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void UpdateProduct_ExistingProduct_UpdatesCorrectly()
        {
            var product = new ProductDto { Id = 1, Name = "Uppdaterad" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(new ProductDto { Id = 1 });

            _service.UpdateProduct(product);

            _mockRepo.Verify(r => r.Update(product), Times.Once);
        }

        [Fact]
        public void UpdateProduct_NonExistingProduct_ThrowsException()
        {
            var product = new ProductDto { Id = 1, Name = "Uppdaterad" };
            _mockRepo.Setup(r => r.GetById(1)).Returns((ProductDto)null);

            Assert.Throws<KeyNotFoundException>(() => _service.UpdateProduct(product));
        }

        [Fact]
        public void DeleteProduct_ExistingProduct_CallsDelete()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns(new ProductDto { Id = 1 });
            _service.DeleteProduct(1);
            _mockRepo.Verify(r => r.Delete(1), Times.Once);
        }

        [Fact]
        public void DeleteProduct_NonExistingProduct_ThrowsException()
        {
            _mockRepo.Setup(r => r.GetById(1)).Returns((ProductDto)null);
            Assert.Throws<KeyNotFoundException>(() => _service.DeleteProduct(1));
        }

        [Fact]
        public void AddProduct_MissingName_ThrowsArgumentException()
        {
            var product = new ProductDto { Name = "", Price = 100 };
            Assert.Throws<ArgumentException>(() => _service.AddProduct(product));
        }
    }
}