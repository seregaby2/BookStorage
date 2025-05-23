namespace BookStorage.WebApi.DTOs.OrderItem
{
    public class UpdateOrderItemDto
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
    }
}
