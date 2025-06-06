using MediatR;

namespace BookStorage.Application.Commands.Order.Create
{
	public record CreateOrderCommand(CreateOrderModel Order) : IRequest<CreateOrderModel>;
}


