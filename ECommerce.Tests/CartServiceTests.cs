using ECommerce.Client.Services;
using ECommerce.Shared.DTOs;
using Xunit;

namespace ECommerce.Tests
{
    public class CartServiceTests
    {
        private readonly ICartService _cartService;

        public CartServiceTests()
        {
            _cartService = new CartService();
        }

        // Testar att AddToCart lägger till en produkt i vagnen och att antalet ökas
        // korrekt när samma produkt läggs till två gånger (quantity ska bli 2, inte två rader).
        // Moq används inte här eftersom CartService är en enkel in-memory-tjänst utan externa beroenden att mocka.
        [Fact]
        public void AddToCart_SameProductTwice_IncrementsQuantity()
        {
            // Arrange
            var product = new ProductDto { Id = 1, Name = "Fotbollsskor", Price = 899 };

            // Act
            _cartService.AddToCart(product);
            _cartService.AddToCart(product);

            // Assert
            var items = _cartService.GetCartItems();
            Assert.Single(items);
            Assert.Equal(2, items[0].Quantity);
        }

        // Testar att ClearCart tömmer vagnen helt och att GetItemCount returnerar 0 efteråt.
        // Moq används inte här eftersom CartService är en enkel in-memory-tjänst utan externa beroenden att mocka.
        [Fact]
        public void ClearCart_RemovesAllItems()
        {
            // Arrange
            var product = new ProductDto { Id = 2, Name = "Träningshandskar", Price = 299 };
            _cartService.AddToCart(product);

            // Act
            _cartService.ClearCart();

            // Assert
            Assert.Empty(_cartService.GetCartItems());
        }
    }
}
