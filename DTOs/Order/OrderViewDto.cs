namespace BookStorage.DTOs.Order
{
    public class OrderViewDto
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
    }
}
