namespace BookStorage.Application.Commands.Order.Create
{
	public class BookOrderItemModel
	{
		public Guid BookId { get; init; }
		public int Quantity { get; init; }
	}
}
