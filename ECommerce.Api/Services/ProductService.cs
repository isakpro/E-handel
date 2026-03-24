using ECommerce.Api.Repositories;
using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Services
{
    // Gränssnitt som definierar vilka metoder ProductService måste ha
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductDto product);
        Task UpdateProductAsync(ProductDto product);
        Task DeleteProductAsync(int id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        // Hämtar alla produkter via repository
        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Hämtar en specifik produkt via id
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Lägger till en ny produkt - validerar att priset inte är negativt
        public async Task AddProductAsync(ProductDto product)
        {
            if (product.Price < 0)
                throw new ArgumentException("Priset kan inte vara negativt.");

            await _repository.AddAsync(product);
        }

        // Uppdaterar en befintlig produkt
        public async Task UpdateProductAsync(ProductDto product)
        {
            await _repository.UpdateAsync(product);
        }

        // Tar bort en produkt med hjälp av dess id
        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
