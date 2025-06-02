using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.CreateOrder
{
	public record CreateOrderCommand(Order Order) : IRequest<Order>;
}


