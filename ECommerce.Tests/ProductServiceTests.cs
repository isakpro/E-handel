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

        // Testar att GetProductByIdAsync returnerar rätt produkt när ett giltigt id skickas in.
        [Fact]
        public async Task GetProductByIdAsync_ExistingId_ReturnsProduct()
        {
            // Arrange
            var expectedProduct = new ProductDto { Id = 1, Name = "Test", Price = 100 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _service.GetProductByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.Name);
        }

        // Testar att GetProductByIdAsync returnerar null när produkten inte finns.
        [Fact]
        public async Task GetProductByIdAsync_NonExistingId_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((ProductDto?)null);

            var result = await _service.GetProductByIdAsync(99);

            Assert.Null(result);
        }

        // Testar att AddProductAsync anropar repository exakt en gång när en giltig produkt läggs till.
        [Fact]
        public async Task AddProductAsync_ValidProduct_CallsRepository()
        {
            var product = new ProductDto { Name = "Ny", Price = 100 };

            await _service.AddProductAsync(product);

            _mockRepo.Verify(r => r.AddAsync(It.IsAny<ProductDto>()), Times.Once);
        }

        // Testar att AddProductAsync kastar ArgumentException när priset är negativt.
        [Fact]
        public async Task AddProductAsync_NegativePrice_ThrowsArgumentException()
        {
            var product = new ProductDto { Name = "Ogiltig", Price = -10 };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddProductAsync(product));
        }

        // Testar att GetProductsAsync returnerar alla produkter från repository.
        [Fact]
        public async Task GetProductsAsync_ReturnsList()
        {
            var products = new List<ProductDto>
            {
                new ProductDto { Id = 1 }, new ProductDto { Id = 2 }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var result = await _service.GetProductsAsync();

            Assert.Equal(2, result.Count());
        }

        // Testar att UpdateProductAsync anropar repository med rätt produkt.
        [Fact]
        public async Task UpdateProductAsync_ValidProduct_CallsRepository()
        {
            var product = new ProductDto { Id = 1, Name = "Uppdaterad", Price = 50 };

            await _service.UpdateProductAsync(product);

            _mockRepo.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        // Testar att DeleteProductAsync anropar repository med rätt id.
        [Fact]
        public async Task DeleteProductAsync_ValidId_CallsRepository()
        {
            await _service.DeleteProductAsync(1);

            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        // Testar att GetProductByIdAsync bara anropar repository en gång per anrop.
        [Fact]
        public async Task GetProductByIdAsync_CallsRepositoryOnce()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(new ProductDto { Id = 5 });

            await _service.GetProductByIdAsync(5);

            _mockRepo.Verify(r => r.GetByIdAsync(5), Times.Once);
        }

        // Testar att GetProductsAsync returnerar en tom lista när inga produkter finns i repository.
        [Fact]
        public async Task GetProductsAsync_EmptyList_ReturnsEmpty()
        {
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<ProductDto>());

            var result = await _service.GetProductsAsync();

            Assert.Empty(result);
        }

        // Testar att AddProductAsync inte kastar något undantag när en giltig produkt med korrekt pris skickas in.
        [Fact]
        public async Task AddProductAsync_ValidProduct_DoesNotThrow()
        {
            var product = new ProductDto { Name = "Giltig", Price = 199 };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<ProductDto>())).Returns(Task.CompletedTask);

            var exception = await Record.ExceptionAsync(() => _service.AddProductAsync(product));

            Assert.Null(exception);
        }
    }
}
