using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task AddAsync(ProductDto product);
        Task UpdateAsync(ProductDto product);
        Task DeleteAsync(int id);
    }
}