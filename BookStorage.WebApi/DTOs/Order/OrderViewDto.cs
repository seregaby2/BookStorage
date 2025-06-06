using BookStorage.Domain.Enums;
using BookStorage.WebApi.DTOs.Customer;
using BookStorage.WebApi.DTOs.OrderBook;

namespace BookStorage.WebApi.DTOs.Order
{
	public class OrderViewDto
	{
		public Guid Id { get; init; }
		public decimal TotalAmount { get; init; }
		public DateTime OrderDate { get; init; }
		public Guid CustomerId { get; init; }
		public OrderStatus Status { get; init; }
		public required CustomerViewDto Customer { get; init; }
		public required List<OrderBookViewDto> Books { get; init; }
	}
}
