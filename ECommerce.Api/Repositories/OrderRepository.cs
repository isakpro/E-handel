using ECommerce.Shared.DTOs;
// Gränssnitt som definierar vilka metoder OrderRepository måste ha -
namespace ECommerce.Api.Repositories
{ // IOrderRepository innehåller metoder för att hämta alla ordrar, hämta en order via id, och lägga till en ny order.
    public class OrderRepository : IOrderRepository
    { // OrderRepository implementerar IOrderRepository och använder en enkel in-memory lista för att lagra ordrar. 
        private readonly List<OrderDto> _orders = new()
        { // Några fördefinierade ordrar i vår "databas" så att vi har något att hämta när API:et startar.
            new OrderDto { OrderId = 101, TotalAmount = 1499 },
            new OrderDto { OrderId = 102, TotalAmount = 899 },
            new OrderDto { OrderId = 103, TotalAmount = 349 }
        };

        public Task<IEnumerable<OrderDto>> GetAllAsync()
        { // Returnerar alla ordrar i vår "databas". I en riktig applikation skulle du hämta detta från en riktig databas.
            return Task.FromResult(_orders.AsEnumerable());
        }

        public Task<OrderDto?> GetByIdAsync(int id)
        { // Hittar en order med det angivna id:t i vår "databas". I en riktig applikation skulle du hämta detta från en riktig databas.
            return Task.FromResult(_orders.FirstOrDefault(o => o.OrderId == id));
        }

        public Task AddAsync(OrderDto order)
        { // Lägger till en ny order i vår "databas". I en riktig applikation skulle du spara detta i en riktig databas.
            _orders.Add(order);
            return Task.CompletedTask;
        }
    }
}