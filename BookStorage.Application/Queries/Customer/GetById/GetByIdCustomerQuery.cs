using MediatR;

namespace BookStorage.Application.Queries.Customer.GetById
{
	public record GetCustomerByIdQuery(Guid Id) : IRequest<GetByIdCustomerModel?>;
}
