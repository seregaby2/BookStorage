using MediatR;

namespace BookStorage.Application.Queries.Book.GetById
{
	public record GetBookByIdQuery(Guid Id) : IRequest<GetByIdBookModel?>;
}
