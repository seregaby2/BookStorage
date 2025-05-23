using System.ComponentModel.DataAnnotations;

namespace BookStorage.Domain.Models
{
    public class OrderItem
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
