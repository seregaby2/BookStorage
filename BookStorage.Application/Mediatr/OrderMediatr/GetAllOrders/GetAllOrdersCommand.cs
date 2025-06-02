
using BookStorage.Domain.Models;
using MediatR;

namespace BookStorage.Application.Mediatr.OrderMediatr.GetAllOrders
{
	public record GetAllOrdersQuery() : IRequest<IEnumerable<Order>>;
}

