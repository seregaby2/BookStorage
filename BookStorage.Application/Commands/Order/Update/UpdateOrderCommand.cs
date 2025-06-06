using MediatR;

namespace BookStorage.Application.Commands.Order.Update
{
	public record UpdateOrderCommand(Guid id, UpdateOrderModel Order) : IRequest<UpdateOrderModel>;
}

