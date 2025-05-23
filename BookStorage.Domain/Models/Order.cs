using System.ComponentModel.DataAnnotations;

namespace BookStorage.Domain.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
