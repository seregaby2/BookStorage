namespace BookStorage.WebApi.DTOs.OrderItem
{
    public class OrderItemViewDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
    }
}
