using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.GetAllCustomers
{
	public record GetAllCustomersQuery() : IRequest<IEnumerable<Customer>>;
}
