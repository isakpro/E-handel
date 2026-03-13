using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<ProductDto> _products = new()
        {
            new ProductDto { Id = 1, Name = "Laptop", Price = 9999, Description = "Kraftfull laptop." },
            new ProductDto { Id = 2, Name = "Mus", Price = 499, Description = "Trådlös mus." }
        };

        public Task<IEnumerable<ProductDto>> GetAllAsync() 
        {
            return Task.FromResult(_products.AsEnumerable());
        }
        
        public Task<ProductDto?> GetByIdAsync(int id) 
        {
            return Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        }

        public Task AddAsync(ProductDto product)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ProductDto product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Description = product.Description;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _products.Remove(product);
            }
            return Task.CompletedTask;
        }
    }
}