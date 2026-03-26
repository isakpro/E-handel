using ECommerce.Shared.DTOs;

namespace ECommerce.Api.Repositories
{ // Gränssnitt som definierar vilka metoder ProductRepository måste ha - 
// det är en kontrakt som alla klasser som implementerar detta gränssnitt måste följa.
    public interface IProductRepository
    { // IProductRepository innehåller metoder för att hämta alla produkter, 
    // hämta en produkt via id, lägga till en ny produkt, uppdatera en befintlig produkt, och ta bort en produkt.
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task AddAsync(ProductDto product);
        Task UpdateAsync(ProductDto product);
        Task DeleteAsync(int id);
    }
}