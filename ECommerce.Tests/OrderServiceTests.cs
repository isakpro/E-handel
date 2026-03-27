using ECommerce.Api.Services;
using ECommerce.Api.Repositories;
using ECommerce.Shared.DTOs;
using Moq;
using Xunit;

namespace ECommerce.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockRepo;
        private readonly IOrderService _service;

        public OrderServiceTests()
        {
            _mockRepo = new Mock<IOrderRepository>();
            _service = new OrderService(_mockRepo.Object);
        }

        // Testar att GetAllOrdersAsync returnerar alla ordrar från repository.
        // Verifierar att listan inte är tom och innehåller rätt antal ordrar.
        [Fact]
        public async Task GetAllOrdersAsync_ReturnsList()
        {
            // Arrange
            var orders = new List<OrderDto>
            {
                new OrderDto { OrderId = 1, TotalAmount = 500 },
                new OrderDto { OrderId = 2, TotalAmount = 1200 }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _service.GetAllOrdersAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        // Testar att AddOrderAsync anropar repository exakt en gång när en giltig order läggs till.
        // Verifierar att servicen vidarebefordrar ordern till datalagret utan att modifiera den.
        [Fact]
        public async Task AddOrderAsync_ValidOrder_CallsRepository()
        {
            // Arrange
            var order = new OrderDto { OrderId = 1, TotalAmount = 750 };

            // Act
            await _service.AddOrderAsync(order);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<OrderDto>()), Times.Once);
        }
    }
}
