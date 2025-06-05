using MediatR;

namespace BookStorage.Application.Queries.Order.GetById
{
	public record GetOrderByIdQuery(Guid Id) : IRequest<GetByIdOrderModel?>;
}

