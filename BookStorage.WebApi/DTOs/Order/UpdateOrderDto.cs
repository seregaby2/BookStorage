using BookStorage.Domain.Enums;
using BookStorage.WebApi.DTOs.OrderBook;

namespace BookStorage.WebApi.DTOs.Order
{
	public class UpdateOrderDto
	{
		public Guid? CustomerId { get; set; }
		public OrderStatus? Status { get; set; }
		public List<UpdateOrderBookDto>? Books { get; set; }
	}
}
