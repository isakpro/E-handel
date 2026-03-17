using ECommerce.Shared.DTOs;

namespace ECommerce.Client.Services
{
    public class CartService
    {
        private readonly List<CartItemDto> _cart = new();

        // Ett event som säger till UI:t att uppdatera sig när vagnen ändras
        public event Action? OnChange; 

        public IReadOnlyList<CartItemDto> GetCartItems() => _cart.AsReadOnly();

        public int GetCartItemCount() => _cart.Sum(i => i.Quantity);

        public void AddToCart(ProductDto product)
        {
            var existingItem = _cart.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _cart.Add(new CartItemDto 
                { 
                    ProductId = product.Id, 
                    ProductName = product.Name, 
                    Price = product.Price, 
                    Quantity = 1 
                });
            }
            OnChange?.Invoke(); // Uppdatera UI
        }

        public void ClearCart()
        {
            _cart.Clear();
            OnChange?.Invoke();
        }

        public decimal GetTotal() => _cart.Sum(i => i.Price * i.Quantity);
    }
}