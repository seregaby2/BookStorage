namespace BookStorage.WebApi.DTOs.OrderBook
{
	public class OrderBookViewDto
	{
		public Guid BookId { get; set; }
		public string Title { get; set; } = null!;
		public decimal Price { get; set; }
		public int Quantity { get; set; }
	}
}
