using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<ProductDto> _products = new()
{
    new ProductDto { Id = 1, Name = "Fotbollsskor", Price = 899, Description = "Lätta och snabba skor för gräs och konstgräs." },
    new ProductDto { Id = 2, Name = "Basketboll", Price = 349, Description = "Officiell storlek och vikt, passar inomhus och utomhus." },
    new ProductDto { Id = 3, Name = "Träningströja", Price = 299, Description = "Andningsbar och snabbtorkande tröja för alla sporter." },
    new ProductDto { Id = 4, Name = "Träningsshorts", Price = 199, Description = "Bekväma shorts med fickor, passar träning och match." },
    new ProductDto { Id = 5, Name = "Träningsväska", Price = 449, Description = "Rymlig väska med flera fack, perfekt för gymmet." }
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