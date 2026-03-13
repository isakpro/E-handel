namespace ECommerce.Shared.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItemDto> Items { get; set; } = new();
    }
}