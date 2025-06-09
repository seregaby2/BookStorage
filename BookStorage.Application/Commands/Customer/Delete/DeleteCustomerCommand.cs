using MediatR;

namespace BookStorage.Application.Commands.Customer.Delete
{
	public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
}
