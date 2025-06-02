using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.DeleteOrder
{
	public record DeleteOrderCommand(Guid Id) : IRequest<bool>;
}
