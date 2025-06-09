using BookStorage.Domain.Enums;
using BookStorage.WebApi.DTOs.OrderBook;

namespace BookStorage.WebApi.DTOs.Order
{
	public class CreateOrderDto
	{
		public Guid CustomerId { get; init; }
		public OrderStatus Status { get; init; }
		public List<CreateOrderBookDto> Books { get; init; } = new();
	}
}

/*Refactor this class to use init-only 
 benefits:
	- not changing DTO after creation.
	- support Equal, GetHasCode
	- compact and readable code.
 */
