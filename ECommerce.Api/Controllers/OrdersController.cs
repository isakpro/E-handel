using System.Security.Claims;
using ECommerce.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMyOrders()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = new List<OrderDto>();

            if (userEmail == "admin@test.com")
            {
                orders.Add(new OrderDto { OrderId = 101, OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 1499 });
                orders.Add(new OrderDto { OrderId = 102, OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 399 });
            }
            else
            {
                orders.Add(new OrderDto { OrderId = 205, OrderDate = DateTime.Now.AddDays(-1), TotalAmount = 5499 });
            }
            return Ok(orders);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDto order)
        {
            order.OrderId = new Random().Next(100, 999);
            order.OrderDate = DateTime.Now;
            return Ok(order);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllOrders()
        {
            var orders = new List<OrderDto>
            {
                new OrderDto { OrderId = 101, OrderDate = DateTime.Now.AddDays(-2), TotalAmount = 1499 },
                new OrderDto { OrderId = 102, OrderDate = DateTime.Now.AddDays(-10), TotalAmount = 399 },
                new OrderDto { OrderId = 205, OrderDate = DateTime.Now.AddDays(-1), TotalAmount = 5499 },
                new OrderDto { OrderId = 206, OrderDate = DateTime.Now.AddDays(-3), TotalAmount = 899 }
            };
            return Ok(orders);
        }
    }
}