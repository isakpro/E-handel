using ECommerce.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Viktigt: Hela denna controller kräver att man är inloggad!
    public class OrdersController : ControllerBase
    {
        // En temporär lista i minnet för att lagra ordrar (byt till databas sen)
        private static readonly List<OrderDto> _mockDatabaseOrders = new();

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDto newOrder)
        {
            if (newOrder == null || !newOrder.Items.Any())
                return BadRequest("Ordern är tom.");

            // I en riktig app hämtar du användarens ID via User.Identity
            newOrder.OrderId = _mockDatabaseOrders.Count + 1;
            newOrder.OrderDate = DateTime.Now;
            
            _mockDatabaseOrders.Add(newOrder);

            return Ok(newOrder);
        }

        [HttpGet]
        public IActionResult GetMyOrders()
        {
            // Här skulle man normalt sett filtrera på inloggad användares ID
            // Men för test-syfte returnerar vi alla skapade ordrar:
            return Ok(_mockDatabaseOrders.OrderByDescending(o => o.OrderDate));
        }
    }
}