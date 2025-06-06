using BookStorage.Domain.Enums;

namespace BookStorage.Application.Commands.Order.Create
{
	public class CreateOrderModel
	{
		public Guid CustomerId { get; init; }
		public OrderStatus Status { get; init; }
		public List<BookOrderItemModel> OrderBook { get; init; } = new();
	}
}
