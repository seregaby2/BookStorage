using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.CreateCustomer
{
	public record CreateCustomerCommand(Customer customer) : IRequest<Customer>;
}
