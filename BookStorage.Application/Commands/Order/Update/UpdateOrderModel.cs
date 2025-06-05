using BookStorage.Application.Commands.Order.Create;
using BookStorage.Domain.Enums;

namespace BookStorage.Application.Commands.Order.Update
{
	public class UpdateOrderModel
	{
		public Guid CustomerId { get; init; }
		public OrderStatus Status { get; init; }
		public List<BookOrderItemModel> OrderBook { get; init; } = new();
	}
}
