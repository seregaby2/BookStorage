using MediatR;

namespace BookStorage.Application.Mediatr.CustomerMediatr.DeleteCustomer
{
	public record DeleteCustomerCommand(Guid Id) : IRequest<bool>;
}
