using ECommerce.Shared.DTOs;
// Gränssnitt som definierar vilka metoder OrderRepository måste ha
namespace ECommerce.Api.Repositories
{ // IOrderRepository innehåller metoder för att hämta alla ordrar, hämta en order via id, och lägga till en ny order.
    public interface IOrderRepository
    { // IOrderRepository innehåller metoder för att hämta alla ordrar, hämta en order via id, och lägga till en ny order.
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task AddAsync(OrderDto order);
    }
}