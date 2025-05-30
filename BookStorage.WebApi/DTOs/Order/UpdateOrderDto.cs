using BookStorage.Domain.Enums;

namespace BookStorage.WebApi.DTOs.Order
{
	public class UpdateOrderDto
	{
		public Guid CustomerId { get; set; }
		public OrderStatus? Status { get; set; }
		public List<Guid>? BookIds { get; set; }
	}
}
