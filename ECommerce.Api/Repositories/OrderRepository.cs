using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<OrderDto> _orders = new()
        {
            new OrderDto { OrderId = 101, TotalAmount = 1499 },
            new OrderDto { OrderId = 102, TotalAmount = 899 },
            new OrderDto { OrderId = 103, TotalAmount = 349 }
        };

        public Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }

        public Task<OrderDto?> GetByIdAsync(int id)
        {
            return Task.FromResult(_orders.FirstOrDefault(o => o.OrderId == id));
        }

        public Task AddAsync(OrderDto order)
        {
            _orders.Add(order);
            return Task.CompletedTask;
        }
    }
}