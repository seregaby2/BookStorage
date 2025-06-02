namespace BookStorage.WebApi.DTOs.OrderBook
{
	public class CreateOrderBookDto
	{
		public Guid BookId { get; set; }
		public int Quantity { get; set; }
	}
}
