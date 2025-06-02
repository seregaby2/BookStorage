namespace BookStorage.Domain.Models
{
	public class OrderBook
	{
		public Guid OrderId { get; set; }
		public required Order Order { get; set; }

		public Guid BookId { get; set; }
		public required Book Book { get; set; }

		public int Quantity { get; set; } = 1;
	}
}
