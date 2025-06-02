using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.GetByIdOrder
{
	public record GetOrderByIdQuery(Guid Id) : IRequest<Order?>;
}

