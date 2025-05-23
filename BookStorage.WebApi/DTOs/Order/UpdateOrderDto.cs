namespace BookStorage.WebApi.DTOs.Order
{
    public class UpdateOrderDto
    {
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
    }
}
