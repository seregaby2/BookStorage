using MediatR;

namespace BookStorage.Application.Commands.Customer.Create
{
	public record CreateCustomerCommand(CreateCustomerModel Customer) : IRequest<CreateCustomerModel>;
}
