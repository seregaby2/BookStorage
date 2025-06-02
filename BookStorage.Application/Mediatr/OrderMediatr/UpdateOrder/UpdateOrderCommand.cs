using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.UpdateOrder
{
	public record UpdateOrderCommand(Guid id, Order Order) : IRequest<Order>;
}

