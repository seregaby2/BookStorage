namespace BookStorage.WebApi.DTOs.OrderBook
{
	public class UpdateOrderBookDto
	{
		public Guid BookId { get; set; }
		public int Quantity { get; set; }
	}
}
