using ECommerce.Api.Repositories;
using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Services
{ // Gränssnitt som definierar vilka metoder OrderService måste ha
    public interface IOrderService
    { // Metoder för att hämta alla ordrar, hämta en order via id, och lägga till en ny order
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task AddOrderAsync(OrderDto order);
    }

    public class OrderService : IOrderService
    { // OrderService implementerar IOrderService och använder IOrderRepository för att interagera med datalagret. Den innehåller affärslogik relaterad till ordrar, som att hämta alla ordrar, hämta en order via id, och lägga till en ny order.
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