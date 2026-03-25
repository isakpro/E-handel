using ECommerce.Shared.DTOs;
// Gränssnitt som definierar vilka metoder ProductRepository måste ha -
namespace ECommerce.Api.Repositories
{ // IProductRepository innehåller metoder för att hämta alla produkter, 
// hämta en produkt via id, lägga till en ny produkt, uppdatera en befintlig produkt, och ta bort en produkt.
    public class ProductRepository : IProductRepository
    { // ProductRepository implementerar IProductRepository och använder en enkel in-memory lista för att lagra produkter.
private readonly List<ProductDto> _products = new()
{ // Några fördefinierade produkter i vår "databas" så att vi har något att hämta när API:et startar.
    new ProductDto { Id = 1, Name = "Fotbollsskor", Price = 899, Description = "Lätta och snabba skor för gräs och konstgräs.", ImageUrl = "https://img01.ztat.net/article/spp-media-p1/1f3247dac45743eb9d56ee927546a66b/5f4488bddf024b409f2aa6afcae51771.jpg?imwidth=1800&filter=packshot" },
    new ProductDto { Id = 2, Name = "Basketboll", Price = 349, Description = "Officiell storlek och vikt, passar inomhus och utomhus.", ImageUrl = "https://cdn.xxl.se/filespin/634116e1a15f48e59188b003deed05a2?quality=75&bgcolor=efefef&resize=1920%2C1920" },
    new ProductDto { Id = 3, Name = "Träningströja", Price = 299, Description = "Andningsbar och snabbtorkande tröja för alla sporter.", ImageUrl = "https://cdn-product.stadium.com/eb200337-aa76-4ae2-a7e3-75cfa1116b37/5120ee65-61f7-45ef-aed4-85a829049959/0eNilqh89TljlEJkqcEf0cUk2/b4GQ8CM372BvjXIWTpoPHLvHG.png?w=2000&h=2000&f=webp&targetFileName=390923_107.webp" },
    new ProductDto { Id = 4, Name = "Träningsshorts", Price = 199, Description = "Bekväma shorts med fickor, passar träning och match.", ImageUrl = "https://cdn-product.stadium.com/eb200337-aa76-4ae2-a7e3-75cfa1116b37/eef4be97-535d-43fa-8b96-77354c57b405/mUaN1ZEAJpHwfKANbWPt8k2j2/bthqEkwR4DNgQP2XYBd1YK9xt.png?w=2000&h=2000&f=webp&targetFileName=390934_102.webp" },
    new ProductDto { Id = 5, Name = "Träningsväska", Price = 449, Description = "Rymlig väska med flera fack, perfekt för gymmet.", ImageUrl = "https://thumblr.uniid.it/product/213283/ad1a750d17d0.jpg" }
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