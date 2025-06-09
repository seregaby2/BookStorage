using System.ComponentModel.DataAnnotations;

namespace BookStorage.Domain.Models
{
	public class Customer
	{
		[Key]
		public Guid Id { get; set; }
		public string Email { get; set; } = "unknown";
		public string FirstName { get; set; } = "unknown";
		public string PhoneNumber { get; set; } = "unknown";
		public DateTime PurchaseDate { get; set; }
		public List<Order> Orders { get; set; } = new();
	}
}
