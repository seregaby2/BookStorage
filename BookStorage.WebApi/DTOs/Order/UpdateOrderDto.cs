using BookStorage.Domain.Enums;
using BookStorage.WebApi.DTOs.OrderBook;

namespace BookStorage.WebApi.DTOs.Order
{
	public class UpdateOrderDto
	{
		public Guid? CustomerId { get; init; }
		public OrderStatus? Status { get; init; }
		public List<UpdateOrderBookDto>? Books { get; init; }
	}
}
