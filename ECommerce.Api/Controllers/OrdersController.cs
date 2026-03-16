using System.Security.Claims;
using ECommerce.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Viktigt: Skyddar endpointen!
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMyOrders()
        {
            // Hittar e-posten för personen som är inloggad just nu
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = new List<OrderDto>();

            // Returnerar olika fejk-ordrar beroende på vem som är inloggad
            if (userEmail == "admin@test.com")
            {
                orders.Add(new OrderDto { OrderId = 101, OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 1499 });
                orders.Add(new OrderDto { OrderId = 102, OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 399 });
            }
            else
            {
                // Nyskapade användare får dessa ordrar
                orders.Add(new OrderDto { OrderId = 205, OrderDate = DateTime.Now.AddDays(-1), TotalAmount = 5499 });
            }

            return Ok(orders);
        }
    }
}