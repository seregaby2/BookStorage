namespace BookStorage.DTOs.Order
{
    public class CreateOrderDto
    {
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
    }
}
