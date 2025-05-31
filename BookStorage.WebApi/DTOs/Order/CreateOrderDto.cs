using BookStorage.Domain.Enums;
using BookStorage.WebApi.DTOs.OrderBook;

namespace BookStorage.WebApi.DTOs.Order
{
	public class CreateOrderDto
	{
		public Guid CustomerId { get; set; }
		public OrderStatus Status { get; set; }
		public List<CreateOrderBookDto> Books { get; set; } = new();
	}
}
