using ECommerce.Api.Repositories;
using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(OrderDto order);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddOrderAsync(OrderDto order)
        {
            await _repository.AddAsync(order);
        }
    }
}