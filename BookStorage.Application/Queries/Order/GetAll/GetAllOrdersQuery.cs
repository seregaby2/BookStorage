using MediatR;

namespace BookStorage.Application.Queries.Order.GetAll
{
	public record GetAllOrdersQuery() : IRequest<IEnumerable<GetAllOrderModel>>;
}

