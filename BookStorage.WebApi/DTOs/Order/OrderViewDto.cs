using BookStorage.Domain.Enums;
using BookStorage.WebApi.DTOs.Book;
using BookStorage.WebApi.DTOs.Customer;

namespace BookStorage.WebApi.DTOs.Order
{
	public class OrderViewDto
	{
		public Guid Id { get; set; }
		public decimal TotalAmount { get; set; }
		public DateTime OrderDate { get; set; }
		public Guid CustomerId { get; set; }
		public OrderStatus Status { get; set; }
		public required CustomerViewDto Customer { get; set; }
		public required List<BookViewDto> Books { get; set; }
	}
}
