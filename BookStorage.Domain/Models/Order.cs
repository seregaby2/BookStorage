using System.ComponentModel.DataAnnotations;

namespace BookStorage.Domain.Models
{
	public class Order
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime OrderDate { get; set; }

		public Guid CustomerId { get; set; }
		public required Customer Customer { get; set; }
		public required List<Book> Books { get; set; }
	}
}
