using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.UpdateCustomer
{
	public record UpdateCustomerCommand(Guid id, Customer customer) : IRequest<Customer>;
}
