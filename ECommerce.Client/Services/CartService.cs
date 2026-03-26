using ECommerce.Shared.DTOs;

namespace ECommerce.Client.Services
{
    public class CartService
    {
        // Den interna listan med varor i kundvagnen
        private readonly List<CartItemDto> _cart = new();

        // Event som komponenter (t.ex. NavMenu) prenumererar på.
        // Anropas varje gång vagnen ändras så att UI:t uppdateras automatiskt.
        public event Action? OnChange;

        // Returnerar en skrivskyddad vy av vagnen så att ingen utanför kan ändra listan direkt
        public IReadOnlyList<CartItemDto> GetCartItems() => _cart.AsReadOnly();

        // Lägger till en produkt. Om den redan finns ökas antalet, annars läggs en ny rad till.
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
            OnChange?.Invoke();
        }

        // Tömmer hela vagnen och meddelar UI:t
        public void ClearCart()
        {
            _cart.Clear();
            OnChange?.Invoke();
        }

        // Returnerar totalt antal varor (summa av alla quantities) — visas i navbar-badgen
        public int GetItemCount() => _cart.Sum(i => i.Quantity);

        // Räknar ut totalpriset för alla varor i vagnen
        public decimal GetTotal() => _cart.Sum(i => i.Price * i.Quantity);
    }
}