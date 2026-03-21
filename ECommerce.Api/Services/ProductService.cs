using ECommerce.Api.Repositories;
using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Services
{
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

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddProductAsync(ProductDto product)
        {
            if (product.Price < 0)
                throw new ArgumentException("Priset kan inte vara negativt.");

            await _repository.AddAsync(product);
        }

        public async Task UpdateProductAsync(ProductDto product)
        {
            await _repository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}