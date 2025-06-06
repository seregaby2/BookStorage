using MediatR;

namespace BookStorage.Application.Commands.Order.Delete
{
	public record DeleteOrderCommand(Guid Id) : IRequest<bool>;
}
