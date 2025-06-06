using MediatR;

namespace BookStorage.Application.Commands.Customer.Update
{
	public record UpdateCustomerCommand(Guid id, UpdateCustomerModel Customer) : IRequest<UpdateCustomerModel>;
}
