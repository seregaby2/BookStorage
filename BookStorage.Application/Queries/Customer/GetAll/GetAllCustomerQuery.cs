using MediatR;

namespace BookStorage.Application.Queries.Customer.GetAll
{
	public record GetAllCustomersQuery() : IRequest<IEnumerable<GetAllCustomerModel>>;
}
