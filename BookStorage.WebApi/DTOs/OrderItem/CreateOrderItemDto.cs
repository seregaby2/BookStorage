namespace BookStorage.WebApi.DTOs.OrderItem
{
    public class CreateOrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
    }
}
