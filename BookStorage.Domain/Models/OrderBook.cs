namespace BookStorage.Domain.Models
{
	public class OrderBook
	{
		public Guid OrderId { get; set; }
		public Order Order { get; set; } = null!;

		public Guid BookId { get; set; }
		public Book Book { get; set; } = null!;

		public int Quantity { get; set; } = 1;
	}
}
