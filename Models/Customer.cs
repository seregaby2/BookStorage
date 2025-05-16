using System.ComponentModel.DataAnnotations;

namespace BookStorage.Models
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public string Email { get; set; } = "unknown";
        public string Name { get; set; } = "unknown";
        public string PhoneNumber { get; set; } = "unknown";
        public DateTime PublishDate { get; set; }

        public List<Order> Orders { get; set; }
    }
}
