using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task AddAsync(OrderDto order);
    }
}