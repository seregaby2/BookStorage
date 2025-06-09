using BookStorage.Application.Queries.Book.GetAll;
using BookStorage.Application.Queries.Customer.GetAll;
using BookStorage.Domain.Enums;

namespace BookStorage.Application.Queries.Order.GetAll
{
	public class GetAllOrderModel
	{
		public Guid Id { get; init; }
		public decimal TotalAmount { get; init; }
		public DateTime OrderDate { get; init; }
		public Guid CustomerId { get; init; }
		public OrderStatus Status { get; init; }
		public GetAllCustomerModel Customer { get; init; } = default!;
		public List<GetAllBooksModel> Books { get; init; } = new();
	}
}
