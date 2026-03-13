using ECommerce.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [Authorize(Roles = "Admin")] // Endast admin kan lägga till (Exempel på skyddad endpoint)
        [HttpPost]
        public IActionResult CreateProduct()
        {
            return Ok("Produkt skapad (Simulerad)");
        }
    }
}